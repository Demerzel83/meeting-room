namespace UI

open  MeetingRoom.Shared
open UI.Parser.Type

module Model =
    type Model =
        {   Page : Page
            MeetingRooms : MeetingRoom list
            Users : User list
            Reservations : Reservation list
            Loading : bool
            MeetingRoomId : string option
            MeetingRoom :  MeetingRoom option
            NewMeetingRoom : MeetingRoom }