namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared

module Reservation =

    let getAll ()
        = Fetch.fetchAs<Reservation list> "/api/reservations"

    let get (id:string)
        = Fetch.fetchAs<Reservation> ("api/reservations/" + id)

    let encode (meetingRoom:Reservation) =
        Encode.object [
            "Id", Encode.int meetingRoom.Id
            "MeetingRoomId", Encode.int meetingRoom.MeetingRoomId
            "UserId", Encode.int meetingRoom.UserId
            "From", Encode.datetime meetingRoom.From
            "To", Encode.datetime meetingRoom.To
        ]

    let update (reservation:Reservation) =
        Fetch.put ("api/reservations/", (encode reservation), Decode.int)

    let create (reservation:Reservation) =
        Fetch.post ("api/reservations/new", (encode reservation), Decode.int)

    let delete (id : int) =
        let url = sprintf "api/reservations/%d" id
        Fetch.delete(url, null, Decode.int)




