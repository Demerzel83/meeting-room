namespace Shared

open System

type MeetingRoom =
      { Id : Guid
        Name: string
        Code: string option }