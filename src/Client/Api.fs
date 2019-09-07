namespace UI

open Thoth.Fetch
open Thoth.Json
open System

open Shared

module Api =

    let getAllMeetingRooms ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:string)
        = Fetch.fetchAs<MeetingRoom option> ("api/meetingrooms/" + id.ToString())

    let encode meetingRoom =
        Encode.object [
            "Id", Encode.guid meetingRoom.Id
            "Name", Encode.string meetingRoom.Name
            "Code", Encode.option Encode.string meetingRoom.Code
        ]

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.put ("api/meetingrooms/", (encode meetingRoom), Decode.int)

    let createMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.post ("api/meetingrooms/new", (encode meetingRoom), Decode.int)


    let deleteMeetingRoom (id : Guid) =
        let url = sprintf "api/meetingrooms/%s" (id.ToString())
        Fetch.delete(url, null, Decode.int)


