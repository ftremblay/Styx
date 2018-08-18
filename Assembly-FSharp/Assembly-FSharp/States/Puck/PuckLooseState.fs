namespace Styx.States.Puck

open UnityEngine

open RageCure.StateUtils
open Styx.Entities.PuckModule
open Styx.Managers
open Styx.Entities.PlayerModule
open Styx.Entities


type PuckLooseState () =
    inherit MonoBehaviour ()

    let updateToPuckState (state: PlayerState)  =
        PlayerModule.reduce PlayerModule.Message.UpdateToCarryPuck state

    let updateToCarried (state: PuckState) =
        PuckModule.reduce PuckModule.Message.UpdateToCarried state

    interface IState<Puck> with

        override this.Enter (entity: Puck) =
            ()
        override this.Execute (entity: Puck) =
            ()
        override this.FixedExecute (entity: Puck) =
            ()
        override this.Exit (entity: Puck) =
            ()

    member this.OnTriggerEnter(other: Collider) =
        if other.gameObject.layer = LayerMask.NameToLayer "Player" then
            PuckManager.Instance.GetPuck()
            |> updateToCarried
            |> focusPuck
            |> PuckManager.Instance.UpdatePuck

            other.gameObject.GetComponent<PlayerId>().Value
            |> PlayerManager.Instance.GetPlayerState
            |> updateToPuckState
            |> focusPlayer
            |> PlayerManager.Instance.UpdatePlayer 
            |> ignore
            
            

