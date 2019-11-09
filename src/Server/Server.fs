open System.IO
open Saturn
open Giraffe
open System.Data.SqlClient

open Api

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath = Path.GetFullPath "../Client/public"

let port =
    "SERVER_PORT"
    |> tryGetEnv
    |> Option.map uint16
    |> Option.defaultValue 8085us


let routes =
    let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")
    choose [
        MeetingRoom.getHandlers connection;
        User.getHandlers connection;
        Reservation.getHandlers connection
    ]

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router routes
    memory_cache
    use_static publicPath
    use_json_serializer(Thoth.Json.Giraffe.ThothSerializer())
    use_gzip
}

run app
