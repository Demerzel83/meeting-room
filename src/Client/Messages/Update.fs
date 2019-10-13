namespace UI.Messages

open System
open Elmish
open Elmish.Navigation

open  MeetingRoom.Shared
open UI.Model
open UI.Api.MeetingRoom
open UI.Api.Reservation
open UI.Api.User
open UI.Messages.Type
open UI.Parser.Type

module Update =
    let always arg = fun _ -> arg

    let deleteMeetingRoomReload id =
        Cmd.OfPromise.perform deleteMeetingRoom id (always LoadMeetingRooms)

    let createMeeting meetingRoom =
        Cmd.OfPromise.perform createMeetingRoom meetingRoom (always LoadMeetingRooms)

    let updatemr meetingRoom =
        Cmd.OfPromise.perform updateMeetingRoom meetingRoom (always LoadMeetingRooms)

    let loadMeetingRoom =
        Cmd.OfPromise.perform getAllMeetingRooms () InitialListLoaded

    let loadReservation =
        Cmd.OfPromise.perform getAllReservations () ReservationsLoaded

    let loadUser =
        Cmd.OfPromise.perform getAllUsers () UsersLoaded

    let updateMeeting meetingRoom =
        match meetingRoom with
        | None -> Cmd.ofMsg LoadMeetingRooms
        | Some mr -> updatemr mr


    let init _ : Model * Cmd<Msg> =
        let initialModel =
            {   Page = Page.MeetingRoomList ;
                MeetingRooms = [] ;
                Users = [] ;
                Reservations = [] ;
                MeetingRoom = None ;
                Loading = true ;
                MeetingRoomId = None ;
                NewMeetingRoom =
                {   Id = 0 ;
                    Name = "" ;
                    Code = None } }

        initialModel, loadMeetingRoom

    let loadReservations model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadReservation

    let loadUsers model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadUser

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | LoadReservations ->
            loadReservations currentModel
        | ReservationsLoaded reservations ->
            let modelWithReservations = { currentModel with Reservations = reservations; Loading = false; Page = Page.ReservationList }
            modelWithReservations, Cmd.none
        | LoadUsers ->
            loadUsers currentModel
        | UsersLoaded users ->
            let modelWithUsers = { currentModel with Users = users; Loading = false; Page = Page.UserList }
            modelWithUsers, Cmd.none
        | NewMeetingRoom ->
            currentModel, Navigation.newUrl "#/meetingroomNew"
        | LoadMeetingRooms ->
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
                | None -> { Name = name; Code = None; Id = 0 }
                | Some mr -> { mr  with Name = name }

            { currentModel with MeetingRoom = (Some newMeetingRoom) }, Cmd.none
        | CodeUpdated code ->
            let newMeetingRoom =
                match currentModel.MeetingRoom with
                | None -> { Name = ""; Code = Some code; Id = 0 }
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
        | FetchMeetingRoomSuccess mr -> { currentModel with MeetingRoom = mr }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel = {
                Page = Page.MeetingRoomList;
                MeetingRooms = meetingRooms;
                Users = [];
                Reservations = [];
                MeetingRoom = None;
                Loading = false;
                MeetingRoomId = None;
                NewMeetingRoom = { Id = 0; Name = ""; Code = None }
            }
            nextModel, Navigation.newUrl "#/meetingroomList"
        | DeleteReservation(_) -> failwith "Not Implemented"
        | DeleteUser(_) -> failwith "Not Implemented"