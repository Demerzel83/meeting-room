namespace UI.Messages

open System
open Elmish

open MeetingRoom.Shared
open UI.Model
open UI.Api
open UI.Parser.Type

module UpdateReservations =
    let always arg = fun _ -> arg

    let loadReservations =
        Cmd.OfPromise.perform Reservation.getAll () ReservationsLoaded

    let updateReservation reservation =
        Cmd.OfPromise.perform Reservation.update reservation (always LoadReservations)

    let createReservation reservation =
        Cmd.OfPromise.perform Reservation.create reservation (always LoadReservations)

    let deleteReservation id =
        Cmd.OfPromise.perform Reservation.delete id (always LoadReservations)

    let loadAllReservations model =
        let modelLoading = { model  with LoadingPage = true; ShowListMeetingRooms = false; ShowListUsers = false }
        modelLoading, loadReservations

    let update (msg : ReservationsMessages) (currentModel : Model) : Model * Cmd<ReservationsMessages> =
        match msg with
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


