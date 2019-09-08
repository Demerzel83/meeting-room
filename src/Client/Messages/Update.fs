namespace UI.Messages

open System
open Elmish
open Elmish.Navigation

open  MeetingRoom.Shared
open UI.Model
open UI.Api
open UI.Messages.Type
open UI.Parser.Type

module Update =
    let always arg = fun _ -> arg

    let deleteMeetingRoomReload id =
        Cmd.OfPromise.perform deleteMeetingRoom id (always LoadMeetingRoomList)

    let createMeeting meetingRoom =
        Cmd.OfPromise.perform createMeetingRoom meetingRoom (always LoadMeetingRoomList)

    let updatemr meetingRoom =
        Cmd.OfPromise.perform updateMeetingRoom meetingRoom (always LoadMeetingRoomList)

    let loadMeetingRoom =
        Cmd.OfPromise.perform getAllMeetingRooms () InitialListLoaded

    let updateMeeting meetingRoom =
        match meetingRoom with
        | None -> Cmd.ofMsg LoadMeetingRoomList
        | Some mr -> updatemr mr


    let init _ : Model * Cmd<Msg> =
        let initialModel =
            {   Page = Page.List ;
                MeetingRooms = [] ;
                MeetingRoom = None ;
                Loading = true ;
                MeetingRoomId = None ;
                NewMeetingRoom =
                {   Id = Guid.Empty ;
                    Name = "" ;
                    Code = None } }

        initialModel, loadMeetingRoom

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | NewMeetingRoom ->
            currentModel, Navigation.newUrl "#new"
        | LoadMeetingRoomList ->
            init()
        | DeleteMeetingRoom id ->
            currentModel, (deleteMeetingRoomReload id)
        | SaveNewMeetingRoom ->
            currentModel, (createMeeting currentModel.NewMeetingRoom)
        | SaveMeetingRoom ->
            currentModel, (updateMeeting currentModel.MeetingRoom )
        | NameUpdated name ->
            let newMeetingRoom =
                match currentModel.MeetingRoom with
                | None -> { Name = name; Code = None; Id = Guid.Empty }
                | Some mr -> { mr  with Name = name }

            { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none
        | CodeUpdated code ->
            let newMeetingRoom =
                match currentModel.MeetingRoom with
                | None -> { Name = ""; Code = Some code; Id = Guid.Empty }
                | Some mr -> { mr  with Code = Some code }

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
            let nextModel = {
                Page = Page.List;
                MeetingRooms = meetingRooms;
                MeetingRoom = None;
                Loading = false;
                MeetingRoomId = None;
                NewMeetingRoom = { Id = Guid.NewGuid(); Name = ""; Code = None }
            }
            nextModel, Navigation.newUrl "#list"