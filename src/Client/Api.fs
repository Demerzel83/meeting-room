namespace UI

open Thoth.Fetch
open Thoth.Json
open System

open Shared

module Api =

    let meetingRoomDecoder =
        Decode.object (fun get ->
            { Id = get.Required.Field "id" Decode.guid
              Name = get.Required.Field "name" Decode.string
              Code = get.Optional.Field "code" Decode.string
            }
        )

    let initialList ()
        = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

    let getMeetingRoom (id:string)
        = Fetch.fetchAs<MeetingRoom option> ("http://localhost:8080/api/meetingrooms/" + id.ToString())

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        let data =
            Encode.object [
                "id", Encode.guid meetingRoom.Id
                "name", Encode.string meetingRoom.Name
                "code", Encode.option Encode.string meetingRoom.Code
            ]

        Fetch.put ("http://localhost:8080/api/meetingrooms/", data, meetingRoomDecoder)

    let createMeetingRoom (meetingRoom:MeetingRoom) =
        let data =
            Encode.object [
                "name", Encode.string meetingRoom.Name
                "code", Encode.option Encode.string meetingRoom.Code
            ]

        Fetch.post ("http://localhost:8080/api/meetingrooms/new", data, meetingRoomDecoder)


    let deleteMeetingRoom (id : Guid) =
        let url = sprintf "http://localhost:8080/api/meetingrooms/%s" (id.ToString())
        Fetch.delete(url, null, Decode.int)


