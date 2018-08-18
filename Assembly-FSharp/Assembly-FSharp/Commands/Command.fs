namespace Styx.Commands

open UnityEngine

[<AbstractClass>]
type Command () =
    inherit MonoBehaviour()

    abstract member Execute: unit -> unit

