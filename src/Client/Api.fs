namespace UI

open Thoth.Fetch
open Thoth.Json
open System

open  MeetingRoom.Shared

module Api =

    let getAllMeetingRooms ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:int)
        = Fetch.fetchAs<MeetingRoom option> ("api/meetingrooms/" + (id.ToString()))

    let encode meetingRoom =
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


