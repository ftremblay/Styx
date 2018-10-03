namespace Styx.States.Player

open UnityEngine

open Styx.Commons.Casters
open Styx.Entities.PlayerModule
open Styx.Managers
open Styx.Entities
open RageCure.StateUtils
open Styx.Models
open RageCure.Utils

type PlayerPassState () =
    inherit PlayerSkatingState ()

    [<SerializeField>]
    let mutable sphereCaster: SphereCaster = Unchecked.defaultof<SphereCaster>

    let calculateVelocity player (movementModel: MovementModel) (transformModel: TransformModel) (rigidbodyModel: RigidbodyModel) =
        let Sc = 60.f
        let Sr = movementModel.LinearSpeed
        let Pc = player.transformModel.Transform.position
        let Pr = transformModel.Transform.position
        let D = Pc - Pr
        let Vr = rigidbodyModel.Rigidbody.velocity
        let d = D.magnitude

        let a = (Sc * Sc) - (Sr * Sr)
        let b = 2.f * Vector3.Dot(D, Vr)
        let c = -(d * d)

        let t = (-b + Mathf.Sqrt(Mathf.Abs((b*b) - (4.f * a * c)))) / (2.f * a)

        let Pi = Pr + (Vr * t)
        let vc = ((Pi - Pc) / t)
        vc
    
    let passSpeed = 30.f

    let getVelocityWhenTheresNoTargetPlayer (player: Player) =
        let forward = player.transformModel.Transform.forward
        if forward.magnitude = 0.f then
            //get all allies players
            let allies = PlayerManager.Instance.GetPlayersFromTeam player.team
            //Calculate distance between players and current passing player
            let playersDistance =
                List.map (fun p -> (p, (p.transformModel.Transform.position - player.transformModel.Transform.position).magnitude)) allies
            //take the smallest distance, if it is less or equal to a certain distance, pass to that player using the calculateVelocity
            
            forward
        else
            forward * passSpeed

    let handlePass (player: Player) =
        let currentHitObject = sphereCaster.Cast(player)
        if currentHitObject <> null then
            let index = currentHitObject.GetComponent<PlayerId>().Value
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
                player.transformModel.Transform.forward * passSpeed
            |> PuckManager.Instance.SetVelocity 
            |> ignore
        else
            player.transformModel.Transform.forward * passSpeed
            |> PuckManager.Instance.SetVelocity 
            |> ignore
        player
        |> PlayerManager.Instance.UpdatePlayer
        |> ignore

    member this.ChangePlayerStateToNormal(player:Player) =
        PlayerManager.Instance.GetPlayerState(player.id.Value)
        |> (reduce Message.UpdateToNormal)
        |> ignore

    interface IState<Player> with
        member this.Execute(player: Player): unit = 
            base.Execute(player)

        member this.Exit(player: Player): unit = 
            base.Exit(player)
            handlePass(player)

        member this.FixedExecute(player: Player): unit = 
            base.FixedExecute(player)

        member this.Enter (player: Player) =
            base.Enter(player)
            this.ChangePlayerStateToNormal(player)
            
            


