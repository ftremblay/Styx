namespace Styx.States.Player

open UnityEngine

open Styx.Entities.PlayerModule
open RageCure.StateUtils
open RageCure.Commons.Utils

type PlayerSkatingState () =
    inherit State<Player> ()

    member this.Enter (player: Player) =
        ()

    member this.Execute (player: Player) =
        player.inputs.horizontalAxis.Execute()
        player.inputs.verticalAxis.Execute()

    member this.FixedExecute (player: Player) =
        let move =
            (player.inputs.verticalAxis.Value * Vector3.forward + player.inputs.horizontalAxis.Value * Vector3.right)
            |> Vector3Utils.normalizeIfNot
            |> this.transform.InverseTransformDirection
            |> Vector3Utils.projectOnPlane Vector3.up
        let forwardAmount =
            move.z
        let turnAmount =
            Mathf.Atan2 (move.x, move.z)
        this.transform.Rotate(0.f, turnAmount * player.movementModel.RotationSpeed * Time.deltaTime, 0.f)
        player.rigidbodyModel.Rigidbody.velocity <- player.rigidbodyModel.Rigidbody.velocity + ((this.transform.forward * player.movementModel.LinearSpeed * Time.deltaTime) * forwardAmount)
        player.animatorModel.SetFloat "Forward Amount" forwardAmount

    member this.Exit (player: Player) =
        ()

