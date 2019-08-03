open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Infrastructure.MeetingRoomReader
open Microsoft.FSharp.Collections

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let webApp = router {
    get "/api/meetingrooms" (fun next ctx ->
        task {
            Dapper.SqlMapper.AddTypeHandler (Utils.Dapper.OptionHandler<string>())
            let meetingRooms = getAllMeetingRooms ()
            return! json (meetingRooms |> List.ofSeq) next ctx
        })
    getf "/api/meetingrooms/%s" (fun id next ctx ->
        task {
            Dapper.SqlMapper.AddTypeHandler (Utils.Dapper.OptionHandler<string>())
            let meetingRooms = getMeetingRoom (System.Guid.Parse(id))
            return! json meetingRooms next ctx
        })
}

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app