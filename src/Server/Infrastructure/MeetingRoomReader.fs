namespace Infrastructure

open System.Data.SqlClient
open Utils.Dapper
open Shared


module MeetingRoomReader =

    let getAllMeetingRooms () =
        let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")

        let test = connection |> dapperQuery<MeetingRoom>
        let result = test "SELECT Id, Name, Code FROM dbo.MeetingRooms"
        result

    let getMeetingRoom id =
        let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")
        let dp = Dapper.DynamicParameters()
        dp.Add("Id", id)
        let mr =
            dapperParametrizedQuery<MeetingRoom> connection "SELECT Id, Name, Code FROM dbo.MeetingRooms WHERE Id = @Id" dp
            |> List.ofSeq
        match mr with
        | [mro] -> Some mro
        | _ -> None

