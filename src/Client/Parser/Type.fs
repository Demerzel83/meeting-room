namespace UI.Parser

module Type =
    [<RequireQualifiedAccess>]
    type Page =
      | MeetingRoomNew
      | MeetingRoomList
      | MeetingRoom of string
      | ReservationList
      | UserList