namespace RageCure.StateUtils

type IState<'a> =
    abstract member Enter: 'a -> unit
    abstract member Execute: 'a -> unit
    abstract member FixedExecute: 'a -> unit
    abstract member Exit: 'a -> unit
