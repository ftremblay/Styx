namespace Styx.Commons.Utils

module LayersUtils =
    
    type Layer =
    | Player

    let toString (layer: Layer) =
        match layer with
        | Player -> "Player"