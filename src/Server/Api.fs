namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open System

open MeetingRoom.Shared
open MeetingRoom.Infrastructure.MeetingRoomReader
open MeetingRoom.Utils
open System.Data
open FSharpPlus.Data

module Route =
    let Definition (connection:IDbConnection) = router {
        get "/api/meetingrooms" (fun next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>()) // todo: rts
                let meetingRooms = getAllMeetingRooms()
                return! json (Reader.run meetingRooms connection) next ctx
            })
        getf "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>())  // todo: rts
                let meetingRoom = getMeetingRoom (System.Guid.Parse(id))

                return! json (Reader.run meetingRoom connection ) next ctx
            })
        deletef "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                let result = deleteMeetingRoom (Guid(id))
                return! ctx.WriteJsonAsync (Reader.run result connection)
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (Reader.run (updateMeetingRoom meetingRoom) connection)
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (Reader.run ( insertMeetingRoom newMeetingRoom) connection)
            }
        )
    }