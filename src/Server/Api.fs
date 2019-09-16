namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open System

open MeetingRoom.Shared
open MeetingRoom.Infrastructure.MeetingRoomReader
open MeetingRoom.Utils
open MeetingRoom.Utils.Sql
open System.Data

module Route =
    let Definition (connection:IDbConnection) = router {
        get "/api/meetingrooms" (fun next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>()) // todo: rts
                let meetingRooms = getAllMeetingRooms()
                return! json (execute connection meetingRooms) next ctx
            })
        getf "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>())  // todo: rts
                let meetingRoom = getMeetingRoom (System.Guid.Parse(id))

                return! json (execute connection meetingRoom) next ctx
            })
        deletef "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                let result = deleteMeetingRoom (Guid(id))
                return! ctx.WriteJsonAsync (execute connection result )
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (execute  connection (updateMeetingRoom meetingRoom))
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (execute connection ( insertMeetingRoom newMeetingRoom))
            }
        )
    }