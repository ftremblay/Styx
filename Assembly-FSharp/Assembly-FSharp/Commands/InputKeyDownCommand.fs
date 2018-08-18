namespace Styx.Commands

open UnityEngine

type InputKeyDownCommand() =
    inherit Command ()

    [<SerializeField>]
    let mutable inputKey: string = ""
    [<SerializeField>]
    let mutable isPressed: bool = false

    member this.IsPressed = isPressed

    override this.Execute () =
        isPressed <- (Input.GetButtonDown inputKey)

