namespace Styx.States.Puck

open UnityEngine

open RageCure.StateUtils
open Styx.Entities.PuckModule


type PuckCarriedState () =
    inherit MonoBehaviour ()

    interface IState<Puck> with

        override this.Enter (puck: Puck) =
            ()
        override this.Execute (puck: Puck) =
            ()
        override this.FixedExecute (puck: Puck) =
            ()
        override this.Exit (puck: Puck) =
            ()
