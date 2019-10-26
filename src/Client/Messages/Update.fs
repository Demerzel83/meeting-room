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

    let loadReservations =
        Cmd.OfPromise.perform getAll () ReservationsLoaded

    let updateReservation reservation =
        match reservation with
        | None -> loadReservations
        | Some r -> Cmd.OfPromise.perform update r (always LoadReservations)

    let createReservation reservation =
        Cmd.OfPromise.perform create reservation (always LoadReservations)

    let loadUser =
        Cmd.OfPromise.perform getAllUsers () UsersLoaded

    let updateMeeting meetingRoom =
        match meetingRoom with
        | None -> Cmd.ofMsg LoadMeetingRooms
        | Some mr -> updatemr mr


    let init _ : Model * Cmd<Msg> =
        let initialModel:Model =
            {   Page = Page.MeetingRoomList ;
                MeetingRooms = [] ;
                Users = [] ;
                Reservations = [] ;
                MeetingRoom = None ;
                Loading = true ;
                MeetingRoomId = None ;
                NewMeetingRoom = {
                    Id = 0 ;
                    Name = "" ;
                    Code = None };
                UserId = None;
                ReservationId = None;
                Reservation = None;
                User = None;
                NewUser = {
                    Id = 0;
                    Name = None;
                    Surname = None;
                    Email = "" };
                NewReservation = {
                    Id = 0;
                    MeetingRoomId = 0;
                    UserId = 0;
                    From = DateTime.Now;
                    To = DateTime.Now }}

        initialModel, loadMeetingRoom

    let loadAllReservations model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadReservations

    let loadUsers model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadUser

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | LoadReservations ->
            loadAllReservations currentModel
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
        | MeetingRoomUpdated meetingRoomId ->
            let updatedReservation =
                match currentModel.Reservation with
                | None -> { From =  DateTime.Now; To = DateTime.Now; MeetingRoomId = meetingRoomId |> int ; Id = 0; UserId = 0 }
                | Some r -> { r  with MeetingRoomId = meetingRoomId |> int }
            { currentModel with Reservation = Some updatedReservation }, Cmd.none
        | UserUpdated userId ->
            let updatedReservation =
                match currentModel.Reservation with
                | None -> { From =  DateTime.Now; To = DateTime.Now; MeetingRoomId = 0 ; Id = 0; UserId = userId |> int }
                | Some r -> { r  with UserId = userId |> int }
            { currentModel with Reservation = Some updatedReservation }, Cmd.none

        | FromUpdated from ->
            let updatedReservation =
                match currentModel.Reservation with
                | None -> { From = from |> DateTime.Parse; To = DateTime.Now; MeetingRoomId = 0 ; Id = 0; UserId = 0 }
                | Some r -> { r  with From = from |> DateTime.Parse; }
            { currentModel with Reservation = Some updatedReservation }, Cmd.none

        | ToUpdated toDate ->
            let updatedReservation =
                match currentModel.Reservation with
                | None -> { To = toDate |> DateTime.Parse; From = DateTime.Now; MeetingRoomId = 0 ; Id = 0; UserId = 0 }
                | Some r -> { r  with To = toDate |> DateTime.Parse; }
            { currentModel with Reservation = Some updatedReservation }, Cmd.none
        | SaveReservation ->
            currentModel, (updateReservation currentModel.Reservation)
        | SaveNewReservation ->
            currentModel, (createReservation currentModel.NewReservation)
        | FetchFailure _ -> { currentModel with MeetingRoom = None }, Cmd.none
        | FetchMeetingRoomSuccess mr -> { currentModel with MeetingRoom = mr }, Cmd.none
        | FetchUserSuccess user -> { currentModel with User = user }, Cmd.none
        | FetchReservationSuccess reservation -> { currentModel with Reservation = reservation }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel:Model = {
                Page = Page.MeetingRoomList;
                MeetingRooms = meetingRooms;
                Users = [];
                Reservations = [];
                MeetingRoom = None;
                Loading = false;
                MeetingRoomId = None;
                NewMeetingRoom = { Id = 0; Name = ""; Code = None };
                 UserId = None;
                ReservationId = None;
                Reservation = None;
                User = None;
                NewUser = {
                    Id = 0;
                    Name = None;
                    Surname = None;
                    Email = "" };
                NewReservation = {
                    Id = 0;
                    MeetingRoomId = 0;
                    UserId = 0;
                    From = DateTime.Now;
                    To = DateTime.Now }}
            nextModel, Navigation.newUrl "#/meetingroomList"
        | DeleteReservation(_) -> failwith "Not Implemented"
        | DeleteUser(_) -> failwith "Not Implemented"