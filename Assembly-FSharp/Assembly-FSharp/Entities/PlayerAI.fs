namespace Styx.Entities

module PlayerAIModule =
    open Styx.Models
    open RageCure.StateUtils
    
    type PlayerAI =
        { id                : PlayerId
        ; rigidbodyModel    : RigidbodyModel
        ; movementModel     : MovementModel
        ; animatorModel     : AnimatorModel
        ; transformModel    : TransformModel
        }

    type States =
        { playerAISkateBetweenTwoWaypoints : IState<PlayerAI>
        }

    type PlayerAIState =
        { playerAI      : PlayerAI
        ; states        : States
        ; stateMachine  : StateMachine<PlayerAI>
        }

    

