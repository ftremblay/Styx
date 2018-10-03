namespace Styx.Models

open UnityEngine

[<CreateAssetMenu(fileName = "Movement Model", menuName = "Models/Movement model")>]
type MovementModel() =
    inherit ScriptableObject()

    [<SerializeField>]
    let mutable linearSpeed: float32 = 15.f
    [<SerializeField>]
    let mutable rotationSpeed: float32 = 500.f

    member this.LinearSpeed 
        with get() = linearSpeed
        and set(v) = linearSpeed <- v
    member this.RotationSpeed
        with get() = rotationSpeed
        and set(v) = rotationSpeed <- v


