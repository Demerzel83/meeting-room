namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open System

open MeetingRoom.Shared
open MeetingRoom.Infrastructure.MeetingRoomReader
open System.Data
open FSharpPlus.Data

module Route =
    let Definition (connection:IDbConnection) = router {
        get "/api/meetingrooms" (fun next ctx ->
            task {
                let meetingRooms = getAllMeetingRooms()
                let result = Reader.run meetingRooms connection

                return! json result next ctx
            })
        getf "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                let meetingRoom =
                    id
                    |> System.Guid.Parse
                    |> getMeetingRoom

                let result = Reader.run meetingRoom connection

                return! json result next ctx
            })
        deletef "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                let changes =
                    id
                    |> Guid
                    |> deleteMeetingRoom
                let result = Reader.run changes connection

                return! ctx.WriteJsonAsync result
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run (updateMeetingRoom meetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run ( insertMeetingRoom newMeetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
    }