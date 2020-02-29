namespace UI

open Fulma.Elmish

open  MeetingRoom.Shared
open UI.Parser.Type

module Model =
    type Model =
        {   Page : Page

            Users : User list
            UserId : string option
            User:  User
            ShowListUsers : bool

            Reservations : Reservation list
            ReservationId : string option
            Reservation :  Reservation
            ShowListMeetingRooms : bool

            Loading : bool
            LoadingData : bool

            MeetingRooms : MeetingRoom list
            MeetingRoomId : string option
            MeetingRoom :  MeetingRoom

            DatePickerFromState : DatePicker.Types.State
            DatePickerToState : DatePicker.Types.State }