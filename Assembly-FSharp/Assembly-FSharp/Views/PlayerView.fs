namespace Styx.Views

open UnityEngine

open Styx.Managers
open Styx.Entities.PlayerModule
open Styx.Models
open Styx.Commands
open RageCure.StateUtils
open Styx.States.Player
open Styx.Entities

type PlayerView () =
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

    // *********************** INPUTS ***********************************
    [<SerializeField>]
    let mutable horizontalAxis: InputAxisCommand = Unchecked.defaultof<InputAxisCommand>
    [<SerializeField>]
    let mutable verticalAxis: InputAxisCommand =  Unchecked.defaultof<InputAxisCommand>
    [<SerializeField>]
    let mutable shootAxis: InputAxisCommand =  Unchecked.defaultof<InputAxisCommand>
    [<SerializeField>]
    let mutable passKeyDown: InputKeyDownCommand =  Unchecked.defaultof<InputKeyDownCommand>
    // *******************************************************************

    // *************************** STATES ******************************************
    [<SerializeField>]
    let mutable playerCarryPuckState: PlayerCarryPuckState =  Unchecked.defaultof<PlayerCarryPuckState>
    [<SerializeField>]
    let mutable playerNormalState: PlayerNormalState = Unchecked.defaultof<PlayerNormalState>
    [<SerializeField>]
    let mutable playerPassState: PlayerPassState = Unchecked.defaultof<PlayerPassState>
    [<SerializeField>]
    let mutable playerWristShotState: PlayerWristShotState = Unchecked.defaultof<PlayerWristShotState>
    // ******************************************************************************

    [<SerializeField>]
    let mutable opponentsGoal: Transform = null

    let mutable playerState: PlayerState = Unchecked.defaultof<PlayerState>

    member this.Start () =
        rigidbodyModel.Rigidbody <- this.GetComponent<Rigidbody>()
        animatorModel.Animator <- this.GetComponent<Animator>()
        transformModel.Transform <- this.GetComponent<Transform>()
        playerState <- 
            { player = 
                { id = playerId
                ; rigidbodyModel = rigidbodyModel
                ; movementModel = movementModel
                ; animatorModel = animatorModel
                ; transformModel = transformModel
                ; inputs = 
                    { horizontalAxis = horizontalAxis
                    ; verticalAxis = verticalAxis
                    ; shootAxis = shootAxis
                    ; passKeyDown = passKeyDown
                    }
                ; opponentsGoal = opponentsGoal
                }
            ; states = 
                { playerCarryPuck   = playerCarryPuckState
                ; playerNormal      = playerNormalState
                ; playerPass        = playerPassState
                ; playerWristShot   = playerWristShotState
                }
            ; stateMachine = new StateMachine<Player>(playerNormalState)
            }
        PlayerManager.Instance.Register playerState

    member this.Update () =
        playerState.stateMachine.Update(playerState.player)

    member this.FixedUpdate() =
        rigidbodyModel.Update()
        playerState.stateMachine.FixedUpdate(playerState.player)
        
    