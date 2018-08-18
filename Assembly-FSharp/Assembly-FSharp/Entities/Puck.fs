namespace Styx.Entities

module PuckModule =
    open RageCure.StateUtils
    open Styx.Models

    type Message =
    | UpdateToLoose
    | UpdateToCarried

    type Puck =
        { rigidbodyModel    : RigidbodyModel
        ; transformModel    : TransformModel
        }

    type States =
        { puckLoose     : IState<Puck>
        ; puckCarried   : IState<Puck>
        }

    type PuckState = 
        { puck: Puck
        ; states : States
        ; stateMachine : StateMachine<Puck>
        }

    let focusPuck (puckState: PuckState) =
        puckState.puck

    let reduce (msg: Message) (puckState: PuckState) =
        match msg with
        | UpdateToLoose     -> puckState.stateMachine.ChangeState puckState.states.puckLoose    |> ignore; puckState 
        | UpdateToCarried   -> puckState.stateMachine.ChangeState puckState.states.puckCarried  |> ignore; puckState

    