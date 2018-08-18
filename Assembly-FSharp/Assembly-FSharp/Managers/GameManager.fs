namespace Styx.Managers

open UnityEngine
open Styx.Commons.Wwise

type GameManager () =
    inherit MonoBehaviour ()

    //*********************** SINGLETON ***************************************
    static let mutable instance: GameManager = Unchecked.defaultof<GameManager>
    static member Instance
        with get() = instance
    //*************************************************************************

    member this.Awake () = 
        instance <- this

    member this.Start() =
        SoundEngine.Instance.PostEvent("StartGame", this.gameObject)