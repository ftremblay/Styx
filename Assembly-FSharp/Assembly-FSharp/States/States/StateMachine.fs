namespace Styx.States

open UnityEngine

type StateMachine<'a> (entity: 'a, initState: State<'a>) =

    let mutable entity: 'a = entity
    
    let mutable currentState: State<'a> = initState
    let mutable maybePreviousState: State<'a> option = None
    let mutable maybeGlobalState: State<'a> option = None
    
    let executeGlobalState (maybeGlobalState: State<'a> option) =
        match maybeGlobalState with
        | None -> entity
        | Some gs -> entity |> gs.Execute

    member this.Update() =
        match maybeGlobalState with
        | Some globalState -> 
            entity |> globalState.Execute |> currentState.Execute |> ignore
        | _ -> 
            entity |> currentState.Execute |> ignore

    member this.FixedUpdate() =
        match maybeGlobalState with
        | Some globalState -> 
            entity |> globalState.FixedExecute |> currentState.FixedExecute |> ignore
        | _ -> 
            entity |> currentState.FixedExecute |> ignore

    member this.ChangeState (newState: State<'a>) =
        previousState <- currentState
        match currentState with
        | None -> ()
        | Some cs -> cs.Exit entity
            


    
