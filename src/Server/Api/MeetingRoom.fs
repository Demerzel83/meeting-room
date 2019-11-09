namespace Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open FSharpPlus.Data

open MeetingRoom.Shared
open MeetingRoom.Infrastructure

module MeetingRoom =
    let getHandlers connection =
        choose [
          POST >=> route "/api/meetingrooms/new" >=>
            fun next context ->
                task {
                    let! newMeetingRoom = Controller.getModel<MeetingRoom> context
                    let result = Reader.run ( MeetingRoomReader.insert newMeetingRoom) connection

                    return! context.WriteJsonAsync result
                }

          GET >=> route "/api/meetingrooms" >=>
            fun next context ->
              task {
                    let meetingRooms = MeetingRoomReader.getAll()
                    let result = Reader.run meetingRooms connection

                    return! json result next context
                }
          GET >=> routef "/api/meetingrooms/%i" (fun id ->
            fun next context ->
                task {
                    let meetingRoom =
                        id
                        |> MeetingRoomReader.get

                    let result = Reader.run meetingRoom connection

                    return! json result next context
                }
          )
          PUT >=> route "/api/meetingrooms/" >=>
            fun next context ->
               task {
                    let! meetingRoom = Controller.getModel<MeetingRoom> context
                    let result = Reader.run (MeetingRoomReader.update meetingRoom) connection

                    return! context.WriteJsonAsync result
                }

          DELETE >=> routef "/api/meetingrooms/%i" (fun id ->
            fun next context ->
              task {
                    let changes =
                        id
                        |> MeetingRoomReader.delete
                    let result = Reader.run changes connection

                    return! context.WriteJsonAsync result
                })
        ]