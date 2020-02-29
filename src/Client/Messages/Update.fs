namespace UI.Messages

open System
open Elmish
open Elmish.Navigation
open Fulma.Elmish

open  MeetingRoom.Shared
open UI.Model
open UI.Api.MeetingRoom
open UI.Api
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

    let fetchMeetingRooms =
        Cmd.OfPromise.perform getAllMeetingRooms () MeetingRoomsFetched

    let loadReservations =
        Cmd.OfPromise.perform Reservation.getAll () ReservationsLoaded

    let updateReservation reservation =
        Cmd.OfPromise.perform Reservation.update reservation (always LoadReservations)

    let createReservation reservation =
        Cmd.OfPromise.perform Reservation.create reservation (always LoadReservations)

    let deleteReservation id =
        Cmd.OfPromise.perform Reservation.delete id (always LoadReservations)

    let loadUsers =
        Cmd.OfPromise.perform User.getAll () UsersLoaded

    let updateUser user =
        Cmd.OfPromise.perform User.update user (always LoadUsers)

    let createUser user =
        Cmd.OfPromise.perform User.create user (always LoadUsers)

    let deleteUser id =
        Cmd.OfPromise.perform User.delete id (always LoadUsers)

    let updateMeeting meetingRoom =
        updatemr meetingRoom


    let init _ : Model * Cmd<Msg> =
        let initialModel:Model =
            {   Page = Page.MeetingRoomList ;
                MeetingRooms = [] ;
                Users = [] ;
                Reservations = [] ;
                Loading = true ;
                LoadingData = false;
                MeetingRoomId = None ;
                ShowListMeetingRooms = false;
                ShowListUsers = false;
                MeetingRoom = {
                    Id = 0 ;
                    Name = "" ;
                    Code = None };
                UserId = None;
                ReservationId = None;
                User = {
                    Id = 0;
                    Name = None;
                    Surname = None;
                    Email = "" };
                Reservation = {
                    Id = 0;
                    MeetingRoom = {
                        Id = 0;
                        Name = "";
                        Code = None;
                        };
                    User = {
                        Id = 0;
                        Email = "";
                        Name = None;
                        Surname = None
                    };
                    From = DateTime.Now;
                    To = DateTime.Now }
                DatePickerFromState = DatePicker.Types.defaultState;
                DatePickerToState = DatePicker.Types.defaultState
            }

        initialModel, loadMeetingRoom

    let loadAllReservations model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadReservations

    let loadAllUsers model =
        let modelLoading = { model  with Loading = true }
        modelLoading, loadUsers

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | LoadReservations ->
            loadAllReservations currentModel
        | ReservationsLoaded reservations ->
            let modelWithReservations = { currentModel with Reservations = reservations; Loading = false; Page = Page.ReservationList }
            modelWithReservations, Cmd.none
        | LoadUsers ->
            loadAllUsers currentModel
        | UsersLoaded users ->
            let modelWithUsers = { currentModel with Users = users; Loading = false; Page = Page.UserList }
            modelWithUsers, Cmd.none
        | NewMeetingRoom ->
            currentModel, Navigation.newUrl "#/meetingroomNew"
        | LoadMeetingRooms ->
            init()
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
        | SaveUser ->
            currentModel, (updateUser currentModel.User)
        | SaveNewUser ->
            currentModel, (createUser currentModel.User)
        | MeetingRoomNameUpdated name ->
            let newMeetingRoom = { currentModel.MeetingRoom  with Name = name }

            { currentModel with MeetingRoom = newMeetingRoom }, Cmd.none
        | MeetingRoomCodeUpdated code ->
            let newMeetingRoom =  { currentModel.MeetingRoom  with Code = Some code }

            { currentModel with MeetingRoom = newMeetingRoom }, Cmd.none
        | MeetingRoomUpdated meetingRoom ->
            let updatedReservation =
                { currentModel.Reservation  with MeetingRoom = meetingRoom }
            { currentModel with Reservation =  updatedReservation; ShowListMeetingRooms = false }, Cmd.none
        | UserUpdated user ->
            let updatedReservation = { currentModel.Reservation  with User = user }
            { currentModel with Reservation = updatedReservation }, Cmd.none

        | FromUpdated (state, date) ->
            let updatedReservation =  { currentModel.Reservation  with From = Option.defaultValue DateTime.Now date; }
            { currentModel with Reservation = updatedReservation; DatePickerFromState = state }, Cmd.none

        | ToUpdated (state, date) ->
            let updatedReservation =  { currentModel.Reservation  with To = Option.defaultValue DateTime.Now date; }
            { currentModel with Reservation = updatedReservation; DatePickerToState = state }, Cmd.none
        | SaveReservation ->
            currentModel, (updateReservation currentModel.Reservation)
        | SaveNewReservation ->
            currentModel, (createReservation currentModel.Reservation)
        | FetchFailure _ -> { currentModel with Loading = false }, Cmd.none
        | FetchMeetingRoomSuccess mr -> { currentModel with MeetingRoom = mr; Loading = false }, Cmd.none
        | FetchUserSuccess user ->
            { currentModel with User = user }, Cmd.none
        | FetchReservationSuccess reservation ->
            { currentModel with Reservation = reservation }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel:Model = {
                Page = Page.MeetingRoomList;
                MeetingRooms = meetingRooms;
                Users = [];
                Reservations = [];
                Loading = false;
                LoadingData = false;
                MeetingRoomId = None;
                MeetingRoom = { Id = 0; Name = ""; Code = None };
                ShowListMeetingRooms= false;
                ShowListUsers= false;
                UserId = None;
                ReservationId = None;
                User = {
                    Id = 0;
                    Name = None;
                    Surname = None;
                    Email = "" };
                Reservation = {
                    Id = 0;
                    MeetingRoom = {
                        Id = 0;
                        Name = "";
                        Code = None
                    }
                    User = {
                        Id = 0;
                        Email = "";
                        Name = None;
                        Surname = None;
                    }
                    From = DateTime.Now;
                    To = DateTime.Now };
                    DatePickerFromState =  DatePicker.Types.defaultState;
                    DatePickerToState = DatePicker.Types.defaultState
            }
            nextModel, Navigation.newUrl "#/meetingroomList"
        | DeleteReservation id ->
            currentModel, (deleteReservation id)
        | DeleteUser id ->
            currentModel, (deleteUser id)
        | UserNameUpdated userName ->
            let updatedUser = { currentModel.User  with Name = Some userName }
            { currentModel with User = updatedUser }, Cmd.none
        | EmailUpdated email ->
            let updatedUser = { currentModel.User  with Email = email }
            { currentModel with User = updatedUser }, Cmd.none
        | SurnameUpdated surname ->
            let updatedUser = { currentModel.User  with Surname = Some surname }
            { currentModel with User = updatedUser }, Cmd.none