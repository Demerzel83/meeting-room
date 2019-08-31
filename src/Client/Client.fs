module Client

open Elmish
open Elmish.React

open UI.Model
open UI.Messages.Type
open UI.Parser.Url
open UI.List
open UI.Api
open UI.Messages.Update


let urlUpdate (result:Page option) (model:Model) =
  match result with
  | Some (MeetingRoom id) ->
      { model with Page = (MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom id FetchSuccess (fun ex -> FetchFailure (id,ex))

  | Some page ->
      { model with Page = page }, []

  | None -> { model with Page = List }, []

#if DEBUG
open Elmish.Debug
#endif
open Elmish.Navigation
open Elmish.UrlParser

let test location = parseHash pageParser location

// Program.mkProgram init update view
Program.mkProgram init update view
// |> Program.toNavigable test urlUpdate
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
