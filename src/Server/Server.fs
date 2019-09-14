open System.IO
open Saturn
open MeetingRoom.Api
open MeetingRoom.Utils.Sql

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv
    |> Option.map uint16
    |> Option.defaultValue 8085us

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router (Route.Definition (new System.Data.SqlClient.SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")))
    memory_cache
    use_static publicPath
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app
