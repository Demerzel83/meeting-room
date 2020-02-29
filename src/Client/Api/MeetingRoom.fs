namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared

module MeetingRoom =

    let getAllMeetingRooms ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:string)
        = Fetch.fetchAs<MeetingRoom> ("api/meetingrooms/" + id)

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.put ("api/meetingrooms/", (Enconding.encodeMeetingRoom meetingRoom), Decode.int)

    let createMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.post ("api/meetingrooms/new", (Enconding.encodeMeetingRoom meetingRoom), Decode.int)

    let deleteMeetingRoom (id : int) =
        let url = sprintf "api/meetingrooms/%d" id
        Fetch.delete(url, null, Decode.int)
