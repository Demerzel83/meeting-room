namespace UI

open Thoth.Fetch
open Thoth.Json
open System

open Shared

module Api =

    let getAllMeetingRooms ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:string)
        = Fetch.fetchAs<MeetingRoom option> ("http://localhost:8080/api/meetingrooms/" + id.ToString())

    let encode meetingRoom =
        Encode.object [
            "Id", Encode.guid meetingRoom.Id
            "Name", Encode.string meetingRoom.Name
            "Code", Encode.option Encode.string meetingRoom.Code
        ]

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.put ("http://localhost:8080/api/meetingrooms/", (encode meetingRoom), Decode.int)

    let createMeetingRoom (meetingRoom:MeetingRoom) =
        Fetch.post ("http://localhost:8080/api/meetingrooms/new", (encode meetingRoom), Decode.int)


    let deleteMeetingRoom (id : Guid) =
        let url = sprintf "http://localhost:8080/api/meetingrooms/%s" (id.ToString())
        Fetch.delete(url, null, Decode.int)


