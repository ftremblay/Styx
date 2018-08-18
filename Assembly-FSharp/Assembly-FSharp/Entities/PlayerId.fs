namespace Styx.Entities

open UnityEngine
open System

type PlayerId () =
    inherit MonoBehaviour() 

    [<SerializeField>]
    let mutable id: Guid = Guid.NewGuid()

    member this.Value
        with get() = id