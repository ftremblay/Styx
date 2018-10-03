namespace Styx.Models

open UnityEngine

[<CreateAssetMenu(fileName = "Transform Model", menuName = "Models/Transform model")>]
type TransformModel() =
    inherit ScriptableObject()

    [<SerializeField>]
    let mutable transform: Transform = null

    member this.Transform
        with get() = transform
        and set(v) = transform <- v



