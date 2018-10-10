namespace Styx.Managers

open UnityEngine

type RagdollState =
        | Ragdolled
        | Animated

type RagdollManager () =
    inherit MonoBehaviour ()

    let mutable state: RagdollState = Animated
    [<SerializeField>]
    let mutable rigidbodies: Rigidbody[] = [||]

    [<SerializeField>]
    let mutable animator: Animator = Unchecked.defaultof<Animator>
    [<SerializeField>]
    let mutable rbody: Rigidbody = Unchecked.defaultof<Rigidbody>
    [<SerializeField>]
    let mutable mainCollider: CapsuleCollider = Unchecked.defaultof<CapsuleCollider>

    let setKinematic (value:bool) (rigidbody: Rigidbody) =
        rigidbody.isKinematic <- value


    [<SerializeField>]
    member this.Ragdolled
        with get() = (state = Ragdolled)
        and set(v) = 
            match v with
            | true ->
                Array.iter (setKinematic false) rigidbodies
                rbody.isKinematic <- true
                animator.enabled <- false
                mainCollider.enabled <- false
                state <- Ragdolled
            | false ->
                Array.iter (setKinematic true) rigidbodies
                rbody.isKinematic <- false
                animator.enabled <- true
                mainCollider.enabled <- true
                state <- Animated
                

    member this.Start() =
        rigidbodies <- this.GetComponentsInChildren<Rigidbody>()
        this.Ragdolled <- false


    member this.FixedUpdate() =
        this.Ragdolled <- not animator.isActiveAndEnabled