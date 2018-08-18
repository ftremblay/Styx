namespace Styx.Views

open UnityEngine
open Styx.Entities
open Styx.Models
open Styx.States.PlayerAI
open Styx.Entities.PlayerAIModule
open RageCure.StateUtils
open Styx.Managers

type PlayerAIView () =
    inherit MonoBehaviour ()

    [<SerializeField>]
    let mutable playerId: PlayerId = Unchecked.defaultof<PlayerId>

    [<SerializeField>]
    let mutable rigidbodyModel: RigidbodyModel = Unchecked.defaultof<RigidbodyModel>
    [<SerializeField>]
    let mutable movementModel: MovementModel = Unchecked.defaultof<MovementModel>
    [<SerializeField>]
    let mutable animatorModel: AnimatorModel = Unchecked.defaultof<AnimatorModel>
    [<SerializeField>] 
    let mutable transformModel: TransformModel = Unchecked.defaultof<TransformModel>

    [<SerializeField>]
    let mutable playerAISkateBetweenTwoWaypoints: PlayerAISkateBetweenTwoWaypoints = Unchecked.defaultof<PlayerAISkateBetweenTwoWaypoints>

    let mutable playerAIState: PlayerAIState = Unchecked.defaultof<PlayerAIState>

    member this.Start () =
        rigidbodyModel.Rigidbody <- this.GetComponent<Rigidbody>()
        animatorModel.Animator <- this.GetComponent<Animator>()
        transformModel.Transform <- this.GetComponent<Transform>()
        playerAIState <-
            { playerAI = 
                { id                = playerId
                ; rigidbodyModel    = rigidbodyModel
                ; movementModel     = movementModel
                ; animatorModel     = animatorModel
                ; transformModel    = transformModel
                }
            ; states = 
                { playerAISkateBetweenTwoWaypoints = playerAISkateBetweenTwoWaypoints
                }
            ; stateMachine = new StateMachine<PlayerAI>(playerAISkateBetweenTwoWaypoints)
            }
        PlayerAIManager.Instance.Register playerAIState

    member this.Update () =
        playerAIState.stateMachine.Update(playerAIState.playerAI)

    member this.FixedUpdate () =
        rigidbodyModel.Update()
        playerAIState.stateMachine.FixedUpdate(playerAIState.playerAI)