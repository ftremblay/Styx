namespace Styx.Commons

open UnityEngine

type IgnoreCollisionWithPlayers () =
    inherit MonoBehaviour ()

    [<SerializeField>]
    let mutable mainCollider: Collider = Unchecked.defaultof<Collider>

    member this.OnCollisionEnter (collision: Collision) =
        if collision.gameObject.layer = LayerMask.NameToLayer "Player" then
            Physics.IgnoreCollision(collision.collider, mainCollider)
        