namespace MeetingRoom.Shared
open System

type MeetingRoom =
      { Id : int
        Name : string
        Code : string option }

type User =
      { Id : int
        Name : string option
        Surname : string option
        Email : string }

type Reservation =
      { Id : int
        MeetingRoomId : int
        UserId : int
        From : DateTimeOffset
        To : DateTimeOffset }