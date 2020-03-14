namespace UI.Messages

open System
open Elmish
open Elmish.Navigation

open MeetingRoom.Shared
open UI.Model
open UI.Api.MeetingRoom
open UI.Api
open UI.Messages.Type
open UI.Parser.Type

module Update =
    let always arg = fun _ -> arg

    let deleteMeetingRoomReload id =
        Cmd.OfPromise.either deleteMeetingRoom id (fun a -> LoadMeetingRooms) (fun error -> FetchFailure ("Error", error))

    let createMeeting meetingRoom =
        Cmd.OfPromise.perform createMeetingRoom meetingRoom (always LoadMeetingRooms)

    let updatemr meetingRoom =
        Cmd.OfPromise.perform updateMeetingRoom meetingRoom (always LoadMeetingRooms)

    let loadMeetingRooms =
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
        let initialModel = getDefaultStatus()
        initialModel, loadMeetingRooms

    let loadAllReservations model =
        let modelLoading = { model  with LoadingPage = true; ShowListMeetingRooms = false; ShowListUsers = false }
        modelLoading, loadReservations

    let loadAllUsers model =
        let modelLoading = { model  with LoadingPage = true }
        modelLoading, loadUsers

    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        // Reservations
        | LoadReservations ->
            loadAllReservations currentModel
        | ReservationsLoaded reservations ->
            let modelWithReservations = { currentModel with Reservations = reservations; LoadingPage = false; Page = Page.ReservationList }
            modelWithReservations, Cmd.none
        | UserUpdated user ->
            let updatedReservation = { currentModel.Reservation  with User = user }
            { currentModel with Reservation = updatedReservation; ShowListUsers = false }, Cmd.none
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
        | FetchReservationSuccess reservation ->
            { currentModel with Reservation = reservation }, Cmd.none
        | DeleteReservation id ->
            currentModel, (deleteReservation id)
        | MeetingRoomUpdated meetingRoom ->
            let updatedReservation =
                { currentModel.Reservation  with MeetingRoom = meetingRoom }
            { currentModel with Reservation =  updatedReservation; ShowListMeetingRooms = false }, Cmd.none
        | UsersClicked ->
            if currentModel.Users.Length = 0 then
                { currentModel with ShowListUsers = false }, loadUsers
            else
                { currentModel with ShowListUsers = true }, Cmd.none
        // Users
        | LoadUsers ->
            loadAllUsers currentModel
        | UsersLoaded users ->
            let modelWithUsers = { currentModel with Users = users; LoadingPage = false }
            modelWithUsers, Cmd.none
        | SaveUser ->
            let newModel = { currentModel with Page = Page.UserList; LoadingPage = true}
            newModel,  seq { updateUser currentModel.User; Navigation.newUrl "#/userList" } |> Cmd.batch
        | SaveNewUser ->
            currentModel, (createUser currentModel.User)
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
        | FetchUserSuccess user ->
            { currentModel with User = user }, Cmd.none
        // Meeting Room
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
        // common
        | FetchFailure (message, error) -> { currentModel with LoadingPage = false; Error = Some (message, error) }, Cmd.none
        | ClearError -> { currentModel with Error = None }, Cmd.none
        | InitialListLoaded meetingRooms->
            let nextModel = { getDefaultStatus() with MeetingRooms = meetingRooms; LoadingPage = false }
            nextModel, Navigation.newUrl "#/meetingroomList"

