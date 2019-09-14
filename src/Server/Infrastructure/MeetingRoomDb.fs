namespace MeetingRoom.Infrastructure

open System.Data.SqlClient
open Dapper
open System

open MeetingRoom.Utils.Dapper
open MeetingRoom.Shared

module MeetingRoomDb =

    let getAllMeetingRooms connection =
            dapperQuery<MeetingRoom> connection "SELECT Id, Name, Code FROM dbo.MeetingRooms"
            |> List.ofSeq


    let getMeetingRoom (id:Guid) connection  =
            let dp = DynamicParameters()
            dp.Add("Id", id)

            let mr =
                dapperParametrizedQuery<MeetingRoom> connection "SELECT Id, Name, Code FROM dbo.MeetingRooms WHERE Id = @Id" dp
                |> List.ofSeq

            match mr with
            | [mro] -> Some mro
            | _ -> None


    let insertMeetingRoom  meetingRoom (connection:SqlConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", Guid.NewGuid())
        dp.Add("Name", meetingRoom.Name)
        dp.Add("Code", Option.defaultValue null meetingRoom.Code)

        connection.Execute("
            INSERT INTO [dbo].[MeetingRooms]
               ([Id]
               ,[Name]
               ,[Code])
         VALUES (@Id, @Name,@Code)", dp)


    let updateMeetingRoom meetingRoom (connection:SqlConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", meetingRoom.Id)
        dp.Add("Name", meetingRoom.Name)
        dp.Add("Code", Option.defaultValue null meetingRoom.Code)

        connection.Execute("
            UPDATE [dbo].[MeetingRooms]
             SET [Name] = @Name
               ,[Code] = @Code
             WHERE [Id] = @Id", dp)


    let deleteMeetingRoom (id:Guid)  (connection:SqlConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", id)

        connection.Execute("
            DELETE FROM [dbo].[MeetingRooms]
             WHERE [Id] = @Id", dp)
