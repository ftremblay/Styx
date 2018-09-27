namespace RageCure.StateUtils

open System.Collections.Concurrent

type StateMachine<'a> (currentState: IState<'a>) =

    let mutable currentState: IState<'a> = currentState
    let mutable previousState: IState<'a> option = None
    let mutable globalState: IState<'a> option = None

    member this.CurrentState
        with get() = currentState

    member this.Update(entity: 'a) =
        match globalState with
        | Some gs -> gs.Execute entity; currentState.Execute entity
        | None -> currentState.Execute entity
        
    member this.FixedUpdate(entity: 'a) =
        match globalState with
        | Some gs -> gs.FixedExecute entity; currentState.Execute entity
        | None -> currentState.FixedExecute entity

    member this.ChangeState (newState: IState<'a>) (entity: 'a) =
        previousState <- Some currentState
        currentState.Exit entity
        currentState <- newState
        currentState.Enter entity

    member this.RevertToPreviousState (entity: 'a) =
        match previousState with
        | None -> ()
        | Some ps -> (this.ChangeState ps entity)

    member this.IsInState (state: IState<'a>) =
        state = currentState
    
