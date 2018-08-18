namespace Styx.Entities

open Styx.Models

module PlayerModule =
    open Styx.Commands
    open UnityEngine
    open RageCure.StateUtils

    type Message =
    | UpdateToNormal
    | UpdateToCarryPuck
    | UpdateToPass
    | UpdateToWristShot

    type Inputs =
        { horizontalAxis    : InputAxisCommand    
        ; verticalAxis      : InputAxisCommand    
        ; shootAxis         : InputAxisCommand    
        ; passKeyDown       : InputKeyDownCommand 
        }

    type Player = 
        { id                : PlayerId           
        ; rigidbodyModel    : RigidbodyModel     
        ; movementModel     : MovementModel      
        ; animatorModel     : AnimatorModel      
        ; transformModel    : TransformModel     
        ; inputs            : Inputs             
        ; opponentsGoal     : Transform   
        }

    type States =
        { playerCarryPuck   : IState<Player>
        ; playerNormal      : IState<Player>
        ; playerPass        : IState<Player>
        ; playerWristShot   : IState<Player>
        }

    type PlayerState =
        { player        : Player          
        ; states        : States
        ; stateMachine  : StateMachine<Player>
        }

    let focusPlayer (state: PlayerState) =
        state.player

    let reduce (msg: Message) (state: PlayerState) =
        match msg with
        | UpdateToNormal    -> state.stateMachine.ChangeState (state.states.playerNormal) (state.player)      ; state
        | UpdateToCarryPuck -> state.stateMachine.ChangeState (state.states.playerCarryPuck) (state.player)   ; state
        | UpdateToPass      -> state.stateMachine.ChangeState (state.states.playerPass) (state.player)        ; state
        | UpdateToWristShot -> state.stateMachine.ChangeState (state.states.playerWristShot) (state.player)   ; state
