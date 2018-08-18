namespace Styx.States.Player

open Styx.Entities.PlayerModule
open RageCure.StateUtils

type PlayerNormalState () =
    inherit PlayerSkatingState ()

    interface IState<Player> with

        member this.Enter (player: Player) =
            base.Enter(player)

        member this.Execute (player: Player) =
            base.Execute(player)

        member this.FixedExecute (player: Player) =
            base.FixedExecute(player)

        member this.Exit (player: Player) =
            base.Exit(player)