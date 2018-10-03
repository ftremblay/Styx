namespace Styx.Models

open UnityEngine

[<CreateAssetMenu(fileName = "Animator Model", menuName = "Models/Animator model")>]
type AnimatorModel () =
    inherit ScriptableObject ()

    [<SerializeField>]
    let mutable animator: Animator = null

    member this.Animator
        with set(v) = animator <- v

    member this.SetTrigger (trigger: string) =
        animator.SetTrigger(trigger)

    member this.SetFloat (key: string) (value: float32) =
        animator.SetFloat (key, value)
