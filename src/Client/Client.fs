module Client

open Elmish
open Elmish.React
open Thoth.Fetch
open Thoth.Json

open Shared
open Parser.Url
open UI.Model
open UI.Messages
open UI.List
open UI.Edit


let meetingRoomDecoder =
    Decode.object (fun get ->
        { Id = get.Required.Field "id" Decode.guid
          Name = get.Required.Field "name" Decode.string
          Code = get.Optional.Field "code" Decode.string
        }
    )

let initialList ()
    = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

let getMeetingRoom (id:string)
    = Fetch.fetchAs<MeetingRoom option> ("http://localhost:8080/api/meetingrooms/" + id.ToString())

let updateMeetingRoom (meetingRoom:MeetingRoom) =
    let data =
        Encode.object [
            "id", Encode.guid meetingRoom.Id
            "name", Encode.string meetingRoom.Name
            "code", Encode.option Encode.string meetingRoom.Code
        ]

    Fetch.put ("http://localhost:8080/api/meetingrooms/", data, meetingRoomDecoder)

let urlUpdate (result:Page option) (model:Model) =
  match result with
  | Some (MeetingRoom id) ->
      { model with Page = (MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom id FetchSuccess (fun ex -> FetchFailure (id,ex))

  | Some page ->
      { model with Page = page }, []

  | None -> { model with Page = List }, []


let init () : Model * Cmd<Msg> =
    let initialModel = { Page = List ; MeetingRooms = []; MeetingRoom = None; Loading = true; MeetingRoomId = None }
    let loadCountCmd =
        // Cmd.OfPromise.perform initialList () InitialListLoaded
        Cmd.OfPromise.perform getMeetingRoom ("608f3ba0-27d0-4cc3-8f12-5b9bd9951fe5") FetchSuccess
    initialModel, loadCountCmd

let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.MeetingRooms, msg with
    | _, SaveMeetingRoom ->
        Option.map updateMeetingRoom currentModel.MeetingRoom |> ignore
        currentModel, Cmd.none
    | _, NameUpdated name ->
        let newMeetingRoom =
            match currentModel.MeetingRoom with
            | None ->{ Name = name; Code = None; Id = System.Guid.Empty }
            | Some mr -> { mr  with Name = name}

        { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none
    | _, CodeUpdated code ->
        let newMeetingRoom =
            match currentModel.MeetingRoom with
            | None ->{ Name = ""; Code = Some code; Id = System.Guid.Empty }
            | Some mr -> { mr  with Code = Some code}

        { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none

    | _, FetchFailure _ -> { currentModel with MeetingRoom = None }, Cmd.none
    | _, FetchSuccess mr -> { currentModel with MeetingRoom = mr }, Cmd.none
    | _, InitialListLoaded meetingRooms->
        let nextModel = { Page = List; MeetingRooms = meetingRooms; MeetingRoom = None; Loading = false; MeetingRoomId = None }
        nextModel, Cmd.none
    | _ -> currentModel, Cmd.none


#if DEBUG
open Elmish.Debug
#endif
open Elmish.Navigation
open Elmish.UrlParser

let test location = parseHash pageParser location

// Program.mkProgram init update view
Program.mkProgram init update editForm
// |> Program.toNavigable test urlUpdate
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
