namespace Styx.Managers

open UnityEngine
open Styx.Entities.PuckModule

type PuckManager () =
    inherit MonoBehaviour()
    
    let mutable puckState: PuckState = Unchecked.defaultof<PuckState>

    //*********************** SINGLETON ***************************************
    static let mutable instance: PuckManager = Unchecked.defaultof<PuckManager>
    static member Instance
        with get() = instance
    //*************************************************************************

    member this.Awake () = 
        if instance <> Unchecked.defaultof<PuckManager> && instance <> this then
            Object.Destroy (this.gameObject)
        else
            instance <- this

    member this.Register (newPuck: PuckState) =
        puckState <- newPuck
        puckState

    member this.GetPuck () =
        puckState
        
    member this.UpdatePuck (updatedPuck: Puck) =
        puckState <- { puckState with puck = updatedPuck }

    member this.SetVelocity (velocity: Vector3) =
        puckState.puck.rigidbodyModel.Rigidbody.velocity <- velocity
        puckState
