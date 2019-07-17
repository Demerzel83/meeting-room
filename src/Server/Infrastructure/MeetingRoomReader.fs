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
