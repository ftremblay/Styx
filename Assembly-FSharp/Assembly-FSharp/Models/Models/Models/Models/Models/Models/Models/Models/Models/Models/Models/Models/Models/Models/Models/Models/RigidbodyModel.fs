namespace Styx.Models

open UnityEngine

[<CreateAssetMenu(fileName = "Rigidbody Model", menuName = "Models/Rigidbody model")>]
type RigidbodyModel () =
    inherit ScriptableObject ()

    [<SerializeField>]
    let mutable rigidbody: Rigidbody = Unchecked.defaultof<Rigidbody>

    [<SerializeField>]
    let mutable drag        : float32 = 2.f
    [<SerializeField>]
    let mutable angularDrag : float32 = 0.8f
    [<SerializeField>]
    let mutable mass        : float32 = 1.f

    member this.Rigidbody
        with get() = rigidbody
        and set(v) = rigidbody <- v
    member this.Drag
        with get() = drag
        and set(v) = drag <- v
    member this.AngularDrag
        with get() = angularDrag
        and set(v) = angularDrag <- v
    member this.Mass
        with get() = mass
        and set(v) = mass <- v

    member this.Update() =
        rigidbody.drag          <- drag
        rigidbody.angularDrag   <- angularDrag
        rigidbody.mass          <- mass

