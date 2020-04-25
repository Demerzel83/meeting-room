namespace Reservations.Core.Types

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
        From : DateTime
        To : DateTime
        MeetingRoom : MeetingRoom
        User : User }