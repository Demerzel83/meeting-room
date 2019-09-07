namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Microsoft.FSharp.Collections
open System

open MeetingRoom.Shared
open MeetingRoom.Infrastructure.MeetingRoomReader
open MeetingRoom.Utils

module Route =
    let Definition connection = router {
        get "/api/meetingrooms" (fun next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>()) // todo: rts
                let meetingRooms = getAllMeetingRooms connection
                return! json meetingRooms next ctx
            })
        getf "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Dapper.OptionHandler<string>())  // todo: rts
                let meetingRooms = getMeetingRoom connection (System.Guid.Parse(id))
                return! json meetingRooms next ctx
            })
        deletef "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                let result = deleteMeetingRoom connection (Guid(id))
                return! ctx.WriteJsonAsync result
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (updateMeetingRoom connection meetingRoom)
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                return! ctx.WriteJsonAsync (insertMeetingRoom connection newMeetingRoom)
            }
        )
    }