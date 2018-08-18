namespace Styx.Commons.Casters

open System.Runtime.InteropServices
open UnityEngine

type SphereCaster () =
    inherit MonoBehaviour ()

    [<SerializeField>]
    let mutable currentHitObject: GameObject = null
    [<SerializeField>]
    let mutable castEveryFrame: bool = true
    [<SerializeField>]
    let mutable sphereCast: SphereCast = Unchecked.defaultof<SphereCast>

    let mutable origin: Vector3     = Vector3.zero
    let mutable direction: Vector3  = Vector3.zero

    let mutable currentHitDistance: float32 = 0.f

    member this.CurrentHitObject 
        with get() = currentHitObject


    member this.Cast () =
        origin      <- this.transform.position + sphereCast.OriginOffset
        direction   <- this.transform.forward
        let mutable hit: RaycastHit = RaycastHit()
        if Physics.SphereCast (origin, sphereCast.Radius, direction, &hit, sphereCast.MaxDistance, (int) sphereCast.Mask, QueryTriggerInteraction.UseGlobal) then
            currentHitObject <- hit.transform.gameObject
            currentHitDistance <- hit.distance
        else
            currentHitDistance <- sphereCast.MaxDistance
            currentHitObject <- null
        currentHitObject

    member this.FixedUpdate() =
        if castEveryFrame then
            this.Cast()
            |> ignore

    member this.OnDrawGizmosSelected() =
        Gizmos.color <- Color.red
        Debug.DrawLine (origin, origin + direction * currentHitDistance)
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereCast.Radius)
        