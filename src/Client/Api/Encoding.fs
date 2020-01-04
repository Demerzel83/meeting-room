namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared

module Enconding =

    let encodeUser (user:User)
        = Encode.object
            [
                "Id", Encode.int user.Id
                "Name", Encode.option Encode.string user.Name
                "Surname", Encode.option Encode.string user.Surname
                "Email", Encode.string user.Email
            ]

    let encodeMeetingRoom (meetingRoom:MeetingRoom) =
        Encode.object [
            "Id", Encode.int meetingRoom.Id
            "Name", Encode.string meetingRoom.Name
            "Code", Encode.option Encode.string meetingRoom.Code
        ]

    let encodeReservation (meetingRoom:Reservation) =
            Encode.object [
                "Id", Encode.int meetingRoom.Id
                "MeetingRoom", (encodeMeetingRoom meetingRoom.MeetingRoom)
                "User",  (encodeUser meetingRoom.User)
                "From", Encode.datetime meetingRoom.From
                "To", Encode.datetime meetingRoom.To
            ]