namespace UI.Messages

open System
open Elmish

open Shared
open UI.Model
open UI.Api
open UI.Messages.Type
open UI.Parser.Url

module Update =

    let deleteMeetingRoomReload (id:Guid) =
        Cmd.OfPromise.perform deleteMeetingRoom (id) (fun _ -> MeetingRoomDeleted)

    let init () : Model * Cmd<Msg> =
        let initialModel = { Page = List ; MeetingRooms = []; MeetingRoom = None; Loading = true; MeetingRoomId = None; NewMeetingRoom = { Id = System.Guid.Empty; Name= ""; Code = None} }
        let loadCountCmd =
            Cmd.OfPromise.perform initialList () InitialListLoaded
            // Cmd.OfPromise.perform getMeetingRoom ("608f3ba0-27d0-4cc3-8f12-5b9bd9951fe5") FetchSuccess
        initialModel, loadCountCmd

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | MeetingRoomDeleted ->
            init()
        | DeleteMeetingRoom id ->
            currentModel, (deleteMeetingRoomReload id)
        | SaveNewMeetingRoom ->
            createMeetingRoom currentModel.NewMeetingRoom |> ignore
            currentModel, Cmd.none
        | SaveMeetingRoom ->
            Option.map updateMeetingRoom currentModel.MeetingRoom |> ignore
            currentModel, Cmd.none
        | NameUpdated name ->
            let newMeetingRoom =
                match currentModel.MeetingRoom with
                | None ->{ Name = name; Code = None; Id = System.Guid.Empty }
                | Some mr -> { mr  with Name = name}

            { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none
        | CodeUpdated code ->
            let newMeetingRoom =
                match currentModel.MeetingRoom with
                | None ->{ Name = ""; Code = Some code; Id = System.Guid.Empty }
                | Some mr -> { mr  with Code = Some code}

            { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none
        | NewNameUpdated name ->
            let newMeetingRoom =
                 { currentModel.NewMeetingRoom  with Name = name}

            { currentModel with NewMeetingRoom = newMeetingRoom }, Cmd.none
        | NewCodeUpdated code ->
            let newMeetingRoom =
               { currentModel.NewMeetingRoom  with Code = Some code}

            { currentModel with NewMeetingRoom = newMeetingRoom }, Cmd.none
        | FetchFailure _ -> { currentModel with MeetingRoom = None }, Cmd.none
        | FetchSuccess mr -> { currentModel with MeetingRoom = mr }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel = { Page = List; MeetingRooms = meetingRooms; MeetingRoom = None; Loading = false; MeetingRoomId = None; NewMeetingRoom = { Id = System.Guid.Empty; Name = ""; Code = None} }
            nextModel, Cmd.none
        | _ -> currentModel, Cmd.none