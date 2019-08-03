module Client

open Elmish
open Elmish.React
open Thoth.Fetch

open Shared
open Parser.Url
open UI.Model
open UI.Messages
open UI.List

let initialList () = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

let getMeetingRoom (id:string) = Fetch.fetchAs<MeetingRoom option> ("/api/meetingrooms/" + id.ToString())

let urlUpdate (result:Page option) (model:Model) =
  match result with
  | Some (MeetingRoom id) ->
      { model with Page = (MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom id FetchSuccess (fun ex -> FetchFailure (id,ex))

  | Some page ->
      { model with Page = page }, []

  | None -> { model with Page = List }, []


let init () : Model * Cmd<Msg> =
    let initialModel = { Page = List ; MeetingRooms = []; MeetingRoom = None }
    let loadCountCmd =
        Cmd.OfPromise.perform initialList () InitialListLoaded
    initialModel, loadCountCmd

let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.MeetingRooms, msg with
    | _, FetchFailure _ -> { currentModel with MeetingRoom = None }, Cmd.none
    | _, FetchSuccess mr -> { currentModel with MeetingRoom = mr }, Cmd.none
    | _, InitialListLoaded meetingRooms->
        let nextModel = { Page = List; MeetingRooms = meetingRooms; MeetingRoom = None }
        nextModel, Cmd.none
    | _ -> currentModel, Cmd.none


#if DEBUG
open Elmish.Debug
#endif
open Elmish.Navigation
open Elmish.UrlParser

let test location = parseHash pageParser location

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
