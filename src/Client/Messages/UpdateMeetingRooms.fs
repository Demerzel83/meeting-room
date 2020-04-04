namespace UI.Messages

open Elmish
open Elmish.Navigation

open MeetingRoom.Shared
open UI.Model
open UI.Api.MeetingRoom

module UpdateMeetingRooms =
    let always arg = fun _ -> arg

    let deleteMeetingRoomReload id =
        // Cmd.OfPromise.either deleteMeetingRoom id (fun a -> LoadMeetingRooms) (fun error -> FetchFailure ("Error", error))
        Cmd.OfPromise.perform deleteMeetingRoom id (fun a -> LoadMeetingRooms)

    let createMeeting meetingRoom =
        Cmd.OfPromise.perform createMeetingRoom meetingRoom (always LoadMeetingRooms)

    let updatemr meetingRoom =
        Cmd.OfPromise.perform updateMeetingRoom meetingRoom (always LoadMeetingRooms)

    let loadMeetingRooms =
        Cmd.OfPromise.perform getAllMeetingRooms () InitialListLoaded

    let fetchMeetingRooms =
        Cmd.OfPromise.perform getAllMeetingRooms () MeetingRoomsFetched

    let updateMeeting meetingRoom =
        updatemr meetingRoom

    let update (msg : MeetingRoomMessages) (currentModel : Model) : Model * Cmd<MeetingRoomMessages> =
        match msg with
        | NewMeetingRoom ->
            currentModel, Navigation.newUrl "#/meetingroomNew"
        | LoadMeetingRooms ->
            { currentModel with LoadingPage = true}, loadMeetingRooms
        | FetchMeetingRooms ->
            let nextModel = { currentModel with LoadingData = true }
            nextModel, fetchMeetingRooms
        | MeetingRoomsFetched meetingRooms ->
            let nextModel = { currentModel with LoadingData = false; MeetingRooms = meetingRooms }
            nextModel, Cmd.none
        | MeetingRoomClicked ->
            let nextModel = { currentModel with ShowListMeetingRooms = true }
            nextModel, Cmd.none
        | DeleteMeetingRoom id ->
            currentModel, (deleteMeetingRoomReload id)
        | SaveNewMeetingRoom ->
            currentModel, (createMeeting currentModel.MeetingRoom)
        | SaveMeetingRoom ->
            currentModel, (updateMeeting currentModel.MeetingRoom )
        | MeetingRoomNameUpdated name ->
            let newMeetingRoom = { currentModel.MeetingRoom  with Name = name }
            { currentModel with MeetingRoom = newMeetingRoom }, Cmd.none
        | MeetingRoomCodeUpdated code ->
            let newMeetingRoom =  { currentModel.MeetingRoom  with Code = Some code }
            { currentModel with MeetingRoom = newMeetingRoom }, Cmd.none

        | FetchMeetingRoomSuccess mr -> { currentModel with MeetingRoom = mr; LoadingPage = false }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel = { getDefaultStatus() with MeetingRooms = meetingRooms; LoadingPage = false }
            nextModel, Navigation.newUrl "#/meetingroomList"

