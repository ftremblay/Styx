namespace Styx.Commons.Casters

open UnityEngine

[<CreateAssetMenu(fileName = "Pass Cast", menuName = "Casts/Pass Cast")>]
type SphereCast () =
    inherit ScriptableObject()

    [<SerializeField>]
    let mutable radius: float32 = 0.f
    [<SerializeField>]
    let mutable maxDistance: float32 = 0.f
    [<SerializeField>]
    let mutable mask: LayerMask = Unchecked.defaultof<LayerMask>
    [<SerializeField>]
    let mutable originOffset: Vector3 = Vector3.zero

    member this.Radius
        with get() = radius
    member this.MaxDistance
        with get() = maxDistance
    member this.Mask
        with get() = mask
    member this.OriginOffset
        with get() = originOffset



