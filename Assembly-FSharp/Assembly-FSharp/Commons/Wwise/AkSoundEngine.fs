namespace Styx.Commons.Wwise

open UnityEngine

type SoundEngine () =
    inherit MonoBehaviour()
    
    [<SerializeField>]
    let mutable soundEvent: WwiseSoundEvent = Unchecked.defaultof<WwiseSoundEvent>

    //*********************** SINGLETON *******************************************
    static let mutable instance: SoundEngine = Unchecked.defaultof<SoundEngine>
    static member Instance =
        instance
    //******************************************************************************
    [<RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)>]
    member this.Awake() =
        instance = this

    [<RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)>]
    member this.PostEvent(event: string, gameObject: GameObject) =
        soundEvent.Invoke(event, gameObject)
