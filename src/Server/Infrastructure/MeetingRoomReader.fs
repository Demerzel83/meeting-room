namespace Infrastructure

open System.Data.SqlClient
open Utils.Dapper
open Shared
open Dapper
open System


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

    let insertMeetingRoom (meetingRoom:MeetingRoom) =
        let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")
        let dp = DynamicParameters()
        dp.Add("Id", Guid.NewGuid())
        dp.Add("Name", meetingRoom.Name)
        dp.Add("Code", meetingRoom.Code)

        connection.Execute("
            INSERT INTO [dbo].[MeetingRooms]
               ([Id]
               ,[Name]
               ,[Code])
         VALUES (@Id, @Name,@Code)", dp)

    let updateMeetingRoom (id:Guid) (meetingRoom:MeetingRoom) =
        let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")
        let dp = DynamicParameters()
        dp.Add("Id", meetingRoom.Id)
        dp.Add("Name", meetingRoom.Name)
        dp.Add("Code", Option.defaultValue null meetingRoom.Code)

        connection.Execute("
            UPDATE [dbo].[MeetingRooms]
             SET [Name] = @Name
               ,[Code] = @Code
             WHERE [Id] = @Id", dp)


    let deleteMeetingRoom (id:Guid) =
        let connection = new SqlConnection ("Server=localhost;Database=MeetingRooms;Trusted_Connection=True;")
        let dp = DynamicParameters()
        dp.Add("Id", id)

        connection.Execute("
            DELETE FROM [dbo].[MeetingRooms]
             WHERE [Id] = @Id", dp)
