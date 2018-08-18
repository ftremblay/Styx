namespace Styx.Managers

open UnityEngine
open System
open System.Collections.Generic

open Styx.Entities.PlayerModule

type PlayerManager () =
    inherit MonoBehaviour () 

    let mutable players = new Dictionary<Guid, PlayerState>()
    
    //*********************** SINGLETON ***************************************
    static let mutable instance: PlayerManager = Unchecked.defaultof<PlayerManager>
    static member Instance
        with get() = instance
    //*************************************************************************

    member this.Awake () = 
        instance <- this

    member this.GetPlayerState (index: Guid) =
        players.Item(index)

    member this.GetPlayer (index: Guid) =
        if players.ContainsKey(index) then
            Some (players.Item(index).player)
        else
            None

    member this.UpdatePlayer(entity: Player) =
        players.Item(entity.id.Value) <- { players.Item(entity.id.Value) with player = entity }
        entity

    member this.Register (state: PlayerState) =
        players.Add (state.player.id.Value, state)
        state
   