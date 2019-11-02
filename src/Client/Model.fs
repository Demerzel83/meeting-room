namespace UI

open  MeetingRoom.Shared
open UI.Parser.Type

module Model =
    type Model =
        {   Page : Page

            Users : User list
            UserId : string option
            User:  User
            Reservations : Reservation list
            ReservationId : string option
            Reservation :  Reservation
            Loading : bool
            MeetingRooms : MeetingRoom list
            MeetingRoomId : string option
            MeetingRoom :  MeetingRoom }