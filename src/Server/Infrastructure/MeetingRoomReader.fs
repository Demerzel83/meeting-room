namespace MeetingRoom.Infrastructure

open MeetingRoomDb
open MeetingRoom.Shared
open FSharpPlus.Data
open System.Data

module MeetingRoomReader =

    let getAll (): Reader<IDbConnection, MeetingRoom list> =
        Reader getAll

    let get id : Reader<IDbConnection, MeetingRoom option> =
        Reader
         (get id)

    let insert (meetingRoom:MeetingRoom): Reader<IDbConnection, int> =
        Reader (insert meetingRoom)

    let update (meetingRoom:MeetingRoom): Reader<IDbConnection, int>  =
        Reader (update meetingRoom)

    let delete  (id:int): Reader<IDbConnection, int>  =
        Reader (delete id)
