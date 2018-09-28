namespace Styx.Views

open UnityEngine
open Styx.States.Puck
open Styx.Models
open RageCure.StateUtils
open Styx.Entities.PuckModule
open Styx.Managers

type PuckView () =
    inherit MonoBehaviour ()

    [<SerializeField>]
    let mutable movementModel: MovementModel = Unchecked.defaultof<MovementModel>
    [<SerializeField>]
    let mutable rigidbodyModel: RigidbodyModel = Unchecked.defaultof<RigidbodyModel>
    [<SerializeField>]
    let mutable transformModel: TransformModel = Unchecked.defaultof<TransformModel>

    // **************************************** STATES ***************************************
    [<SerializeField>]
    let mutable puckLooseState: PuckLooseState = Unchecked.defaultof<PuckLooseState>
    [<SerializeField>]
    let mutable puckCarriedState: PuckCarriedState = Unchecked.defaultof<PuckCarriedState>
    // ***************************************************************************************

    let mutable puckState: PuckState = Unchecked.defaultof<PuckState>

    member this.Start() =
        rigidbodyModel.Rigidbody <- this.GetComponent<Rigidbody>()
        transformModel.Transform <- this.GetComponent<Transform>()
        puckState <-
            { puck =
                { rigidbodyModel = rigidbodyModel
                ; transformModel = transformModel
                }
            ; states = 
                { puckLoose     = puckLooseState
                ; puckCarried   = puckCarriedState
                }
            ; stateMachine = new StateMachine<Puck>(puckLooseState)
            }

        puckState
        |> PuckManager.Instance.Register

    member this.Update() =
        puckState.stateMachine.Update(puckState.puck)
        Debug.Log puckState.stateMachine.CurrentState

    member this.FixedUpdate() =
        rigidbodyModel.Update()
        puckState.stateMachine.FixedUpdate(puckState.puck)
