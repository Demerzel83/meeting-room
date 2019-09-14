namespace MeetingRoom.Utils

module Reader =
    type Reader<'environment,'a> = Reader of ('environment -> 'a)

    /// Evaluate the action with a given environment
    /// Sqlenvironment -> Reader<'a> -> 'a
    let run environment (Reader action) =
        let resultOfAction = action environment
        resultOfAction

    /// ('a -> 'b) -> Reader<'a> -> Reader<'b>
    let map f action =
        let newAction environment =
            let x = run environment action
            f x
        Reader newAction

    /// 'a -> Reader<'a>
    let retn x =
        let newAction _ =
            x
        Reader newAction

    /// Reader<('a -> 'b)> -> Reader<'a> -> Reader<'b>
    let apply fAction xAction =
        let newAction environment =
            let f = run environment fAction
            let x = run environment xAction
            f x
        Reader newAction

    /// ('a -> Reader<'b>) -> Reader<'a> -> Reader<'b>
    let bind f xAction =
        let newAction environment =
            let x = run environment xAction
            run environment (f x)
        Reader newAction