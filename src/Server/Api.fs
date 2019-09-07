namespace Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Infrastructure.MeetingRoomReader
open Microsoft.FSharp.Collections
open Shared
open System

module Route =
    let Definition connection = router {
        get "/api/meetingrooms" (fun next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Utils.Dapper.OptionHandler<string>())
                let meetingRooms = getAllMeetingRooms connection
                return! json (meetingRooms |> List.ofSeq) next ctx
            })
        getf "/api/meetingrooms/%s" (fun id next ctx ->
            task {
                Dapper.SqlMapper.AddTypeHandler (Utils.Dapper.OptionHandler<string>())
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