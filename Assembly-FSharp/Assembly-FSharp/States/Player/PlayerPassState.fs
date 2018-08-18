﻿namespace Styx.States.Player

open UnityEngine

open Styx.Commons.Casters
open Styx.Entities.PlayerModule
open Styx.Managers
open Styx.Entities
open RageCure.StateUtils
open Styx.Models

type PlayerPassState () =
    inherit PlayerSkatingState ()

    [<SerializeField>]
    let mutable sphereCaster: SphereCaster = Unchecked.defaultof<SphereCaster>

    let calculateVelocity player (movementModel: MovementModel) (transformModel: TransformModel) (rigidbodyModel: RigidbodyModel) =
        let Sc = 0.f
        let Sr = movementModel.LinearSpeed
        let Pc = player.transformModel.Transform.position
        let Pr = transformModel.Transform.position
        let D = Pc - Pr
        let Vr = rigidbodyModel.Rigidbody.velocity
        let d = D.magnitude

        let a = (Sc * Sc) - (Sr * Sr)
        let b = 2.f * Vector3.Dot(D, Vr)
        let c = -(d * d)

        let t = (-b - Mathf.Sqrt(Mathf.Abs((b*b) - (4.f * a * c)))) / (2.f * a)

        let Pi = Pr + (Vr * t)
        let vc = ((Pi - Pc) / t)
        Debug.Log(vc)
        vc
    

    let handlePass (player: Player) =
        if sphereCaster.CurrentHitObject <> null then
            let index = sphereCaster.CurrentHitObject.GetComponent<PlayerId>().Value
            let otherPlayer = PlayerManager.Instance.GetPlayer index
            let aiPlayer = PlayerAIManager.Instance.GetPlayerAI index
                
            match otherPlayer, aiPlayer  with
            | Some otherPlayer, None -> 
                calculateVelocity player otherPlayer.movementModel otherPlayer.transformModel otherPlayer.rigidbodyModel
            | None, Some otherPlayer ->
                calculateVelocity player otherPlayer.movementModel otherPlayer.transformModel otherPlayer.rigidbodyModel
            | Some otherPlayer, _ ->
                calculateVelocity player otherPlayer.movementModel otherPlayer.transformModel otherPlayer.rigidbodyModel
            | None, None ->
                player.transformModel.Transform.forward * 10.f
            |> (*) 1.2f
            |> PuckManager.Instance.SetVelocity 
            |> ignore
        else
            player.transformModel.Transform.forward * 10.f
            |> PuckManager.Instance.SetVelocity 
            |> ignore
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
            player.animatorModel.SetTrigger("Pass")
            PlayerManager.Instance.GetPlayerState(player.id.Value)
            |> reduce Message.UpdateToNormal
            |> ignore

            handlePass player
            |> PlayerManager.Instance.UpdatePlayer
            |> ignore

