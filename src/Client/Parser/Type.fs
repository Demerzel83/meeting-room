namespace UI.Parser

module Type =
    [<RequireQualifiedAccess>]
    type Page =
      | MeetingRoomNew
      | ReservationNew
      | UserNew
      | MeetingRoomList
      | MeetingRoom of string
      | User of string
      | Reservation of string
      | ReservationList
      | UserList