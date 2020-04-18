namespace UI.Messages

open Elmish

open UI.Model
open UI.Messages.Type

module Update =
    let update (msg : Msg<'T>) (currentModel : Model) : Model * Cmd<Msg<'T>> =
        match msg with
        | Msg UserMessages um -> UpdateUsers.update um currentModel
        | Msg ReservationsMessages rm -> UpdateReservations.update rm currentModel
        | msg MeetingRoomMessages mr -> UpdateMeetingRooms.update mr currentModel

