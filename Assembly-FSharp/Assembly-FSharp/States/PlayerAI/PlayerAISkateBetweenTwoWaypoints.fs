namespace Styx.States.PlayerAI

open RageCure.Commons.Utils
open RageCure.StateUtils
open UnityEngine

open Styx.Entities.PlayerAIModule

type PlayerAISkateBetweenTwoWaypoints () =
    inherit State<PlayerAI>()

    [<SerializeField>]
    let mutable waypoints: Transform[] = null

    let mutable currentIndex: int = 0

    let updateIndex () =
        if currentIndex = waypoints.Length - 1 then
            currentIndex <- 0
        else
            currentIndex <- currentIndex + 1


    interface IState<PlayerAI> with

        member this.Execute(player: PlayerAI) =
            if Vector3Utils.isNear player.transformModel.Transform.position waypoints.[currentIndex].position then
                updateIndex()

        member this.Exit(player: PlayerAI) =
            ()

        member this.FixedExecute(player: PlayerAI) =
            let direction =
                (waypoints.[currentIndex].position - player.transformModel.Transform.position)
                |> Vector3Utils.normalizeIfNot
                |> this.transform.InverseTransformDirection
                |> Vector3Utils.projectOnPlane Vector3.up

            let forwardAmount =
                direction.z
            let turnAmount =
                Mathf.Atan2 (direction.x, direction.z)
            this.transform.Rotate(0.f, turnAmount * player.movementModel.RotationSpeed * Time.deltaTime, 0.f)
            player.rigidbodyModel.Rigidbody.velocity <- player.rigidbodyModel.Rigidbody.velocity + ((this.transform.forward * player.movementModel.LinearSpeed * Time.deltaTime) * forwardAmount)
            player.animatorModel.SetFloat "Forward Amount" forwardAmount

        member this.Enter(player: PlayerAI) =
            ()