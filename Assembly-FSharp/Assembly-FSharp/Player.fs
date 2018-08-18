namespace Styx

module Player =

    type State =
    | WithoutPuck
    | WithPuck
    | Shooting
    | Passing
    | Knockout

    type Model =
        { currentState: State
        }

    let init() =
        { currentState = WithoutPuck
        }