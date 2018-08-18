namespace Styx.Commons.Casters

open UnityEngine
open Styx.Entities
open Styx.Entities.PlayerModule
open Styx.Managers

type SphereCaster () =
    inherit MonoBehaviour ()

    [<SerializeField>]
    let mutable currentHitObject: GameObject = null
    [<SerializeField>]
    let mutable castEveryFrame: bool = true
    [<SerializeField>]
    let mutable sphereCast: SphereCast = Unchecked.defaultof<SphereCast>

    let mutable origin       : Vector3  = Vector3.zero
    let mutable direction    : Vector3  = Vector3.zero
    let mutable timestamp    : float32  = 0.f
    
    let mutable currentHitDistance  : float32   = 0.f

    member this.CurrentHitObject 
        with get() = currentHitObject

    member this.Cast (player: Player) =
        origin      <- this.transform.position + sphereCast.OriginOffset
        direction   <- (player.inputs.verticalAxis.Value * Vector3.forward + player.inputs.horizontalAxis.Value * Vector3.right)
        let mutable hit: RaycastHit = RaycastHit()
        if Physics.SphereCast (origin, sphereCast.Radius, direction, &hit, sphereCast.MaxDistance, (int) sphereCast.Mask, QueryTriggerInteraction.UseGlobal) then
            currentHitObject <- hit.transform.gameObject
            currentHitDistance <- hit.distance
            timestamp <- Time.time + 1.f
        currentHitObject


    member this.OnDrawGizmosSelected() =
        Gizmos.color <- Color.red
        Debug.DrawLine (origin, origin + direction * currentHitDistance)
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereCast.Radius)
        