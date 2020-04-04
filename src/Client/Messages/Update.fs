namespace UI.Messages

open Elmish

open UI.Model
open UI.Messages.Type

module Update =
    let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
        match msg with
        | UserMessages -> UpdateUsers.update (m :> UserMessages) currentModel
        | ReservationsMessages -> UpdateReservations.update (msg:> ReservationsMessages) currentModel
        | MeetingRoomMessages -> UpdateMeetingRooms.update (msg:> MeetingRoomMessages) currentModel

