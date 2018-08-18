namespace Styx.Commons.Wwise

open UnityEngine.Events
open UnityEngine

[<System.Serializable>]
type WwiseSoundEvent () =
    inherit UnityEvent<string, GameObject> ()

