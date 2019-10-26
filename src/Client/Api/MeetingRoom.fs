namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared

module MeetingRoom =

    let getAllMeetingRooms ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:string)
        = Fetch.fetchAs<MeetingRoom option> ("api/meetingrooms/" + id)

    let encode (meetingRoom:MeetingRoom) =
        Encode.object [
            "Id", Encode.int meetingRoom.Id
            "Name", Encode.string meetingRoom.Name
            "Code", Encode.option Encode.string meetingRoom.Code
        ]

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.put ("api/meetingrooms/", (encode meetingRoom), Decode.int)

    let createMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.post ("api/meetingrooms/new", (encode meetingRoom), Decode.int)


    let deleteMeetingRoom (id : int) =
        let url = sprintf "api/meetingrooms/%d" id
        Fetch.delete(url, null, Decode.int)

