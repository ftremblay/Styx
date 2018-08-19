namespace Styx.States.Player

open UnityEngine
open Styx.Entities.PlayerModule
open Styx.Managers
open RageCure.StateUtils
open System

type PlayerCarryPuckState () =
    inherit PlayerSkatingState ()

    [<SerializeField>]
    let mutable puckJointAnchor: GameObject = null
    let mutable joint: HingeJoint = null
    let mutable playerId: Guid = Unchecked.defaultof<Guid>

    // ****************************************** WRIST SHOT ********************************************
    let isWristShooting (shot: float32) (player: Player) =
        (player.opponentsGoal.position.z > 0.f && shot > 0.7f) || (player.opponentsGoal.position.z < 0.f && shot < -0.7f)

    let handleWristShot (player: Player) =
        let shot = player.inputs.shootAxis.Value
        if isWristShooting shot player then
            PlayerManager.Instance.GetPlayerState(player.id.Value)
            |> reduce Message.UpdateToWristShot
            |> focusPlayer
            |> PlayerManager.Instance.UpdatePlayer
        else
            player
    // **************************************************************************************************

    // ****************************************** PASS **************************************************
    let handlePass (player: Player) =
        if player.inputs.passKeyDown.IsPressed then
            player.animatorModel.SetTrigger("Front pass")
            player
        else
            player
    // **************************************************************************************************

    let releasePuck () =
        if joint <> null then
            Object.Destroy joint

    member this.Pass () =
        PlayerManager.Instance.GetPlayerState(playerId)
            |> reduce Message.UpdateToPass
            |> focusPlayer
            |> PlayerManager.Instance.UpdatePlayer

    interface IState<Player> with

        member this.Enter (player: Player) =
            base.Enter(player)
            playerId <- player.id.Value
            let puckState = PuckManager.Instance.GetPuck()
            joint                                               <- puckJointAnchor.AddComponent<HingeJoint>()
            puckState.puck.transformModel.Transform.position    <- puckJointAnchor.transform.position
            joint.connectedBody                                 <- puckState.puck.rigidbodyModel.Rigidbody

        member this.Execute (player: Player) =
            base.Execute(player)
            player.inputs.shootAxis.Execute()
            player.inputs.passKeyDown.Execute()
            player
            |> handleWristShot
            |> handlePass 
            |> ignore

        member this.FixedExecute (player: Player) =
            base.FixedExecute(player)
            if joint = null then
                PlayerManager.Instance.GetPlayerState(player.id.Value)
                |> reduce Message.UpdateToNormal
                |> ignore

                player
                |> PlayerManager.Instance.UpdatePlayer
                |> ignore

        member this.Exit (player: Player) =
            base.Exit(player)
            releasePuck()
        
