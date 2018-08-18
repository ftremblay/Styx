namespace Styx.Managers

open UnityEngine
open System.Collections.Generic
open System

open Styx.Entities.PlayerAIModule

type PlayerAIManager () =
    inherit MonoBehaviour ()

    let mutable players = new Dictionary<Guid, PlayerAIState>()

    // **************************************** SINGLETON ****************************************
    static let mutable instance: PlayerAIManager = Unchecked.defaultof<PlayerAIManager>
    static member Instance
        with get() = instance
    // *******************************************************************************************

    member this.Awake () =
        instance <- this

    member this.GetPlayerAIState (index: Guid) =
        players.Item(index)

    member this.GetPlayerAI (index: Guid) =
        if players.ContainsKey(index) then
            Some (players.Item(index).playerAI)
        else
            None

     member this.UpdatePlayer(entity: PlayerAI) =
        players.Item(entity.id.Value) <- { players.Item(entity.id.Value) with playerAI = entity }

    member this.Register (state: PlayerAIState) =
        players.Add (state.playerAI.id.Value, state)
        state