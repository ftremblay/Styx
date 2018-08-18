namespace Styx.States.Player

open UnityEngine
open RageCure.Commons.Utils

open Styx.Entities.PlayerModule
open Styx.Managers
open RageCure.StateUtils

type PlayerWristShotState () =
    inherit PlayerSkatingState ()

    member this.LookAtWherePlayerShoot (goalPosition: Vector3) (direction: Vector3) =
        this.transform.LookAt goalPosition
        direction

    member this.PushPuck (player: Player) (direction: Vector3) =
        PuckManager.Instance.SetVelocity (direction * 30.f + new Vector3(0.f, 0.4f, 0.f)) |> ignore

    member this.Shoot (player: Player) =
        (player.opponentsGoal.position - this.transform.position)
        |> Vector3Utils.normalize 
        |> this.LookAtWherePlayerShoot player.opponentsGoal.position
        |> this.PushPuck player
        player
    
    interface IState<Player> with
        member this.Execute(player: Player): unit = 
            base.Execute(player)

        member this.Exit(player: Player): unit = 
            base.Exit(player)

        member this.FixedExecute(player: Player): unit = 
            base.FixedExecute(player)

        member this.Enter (player: Player) =
            base.Enter(player)
            player.animatorModel.SetTrigger("Wrist shot")
            PlayerManager.Instance.GetPlayerState(player.id.Value)
            |> reduce Message.UpdateToNormal
            |> ignore

            player
            |> this.Shoot
            |> PlayerManager.Instance.UpdatePlayer 
            |> ignore
    


    
