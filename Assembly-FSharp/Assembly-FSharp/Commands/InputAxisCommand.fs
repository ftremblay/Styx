namespace Styx.Commands

open UnityEngine

type InputAxisCommand() =
    inherit Command ()

    [<SerializeField>]
    let mutable inputAxis: string = ""
    [<SerializeField>]
    let mutable value: float32 = 0.f

    member this.Value = value

    override this.Execute () =
        value <- Input.GetAxis inputAxis

    

