namespace Styx.States

open UnityEngine

[<AbstractClass>]
type State<'a> () =
    inherit MonoBehaviour()
    
    abstract member Enter: 'a -> 'a
    abstract member Execute: 'a -> 'a
    abstract member FixedExecute: 'a -> 'a
    abstract member Exit: 'a -> 'a
    

