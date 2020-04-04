namespace UI.Messages

open Elmish

open UI.Model

module UpdateSystem =
    let update (msg : SystemMessages) (currentModel : Model) : Model * Cmd<SystemMessages> =
        match msg with
        | FetchFailure (message, error) -> { currentModel with LoadingPage = false; Error = Some (message, error) }, Cmd.none
        | ClearError -> { currentModel with Error = None }, Cmd.none
