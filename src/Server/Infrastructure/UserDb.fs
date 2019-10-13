namespace User.Infrastructure

open Dapper

open MeetingRoom.Utils.Dapper
open MeetingRoom.Shared
open System.Data

module UserDb =

    let getAllUsers (connection:IDbConnection) =
            dapperQuery<User> connection "SELECT Id, Name, Surname, Email FROM dbo.Users"
            |> List.ofSeq


    // let getMeetingRoom (id:int) (connection:IDbConnection)  =
    //         let dp = DynamicParameters()
    //         dp.Add("Id", id)

    //         let mr =
    //             dapperParametrizedQuery<MeetingRoom> connection "SELECT Id, Name, Code FROM dbo.MeetingRooms WHERE Id = @Id" dp
    //             |> List.ofSeq

    //         match mr with
    //         | [mro] -> Some mro
    //         | _ -> None


    // let insertMeetingRoom  (meetingRoom:MeetingRoom) (connection:IDbConnection) =
    //     let dp = DynamicParameters()
    //     dp.Add("Name", meetingRoom.Name)
    //     dp.Add("Code", Option.defaultValue null meetingRoom.Code)

    //     connection.Execute("
    //         INSERT INTO [dbo].[MeetingRooms]
    //            ([Name]
    //            ,[Code])
    //      VALUES (@Name,@Code)", dp)


    // let updateMeetingRoom (meetingRoom:MeetingRoom) (connection:IDbConnection) =
    //     let dp = DynamicParameters()
    //     dp.Add("Id", meetingRoom.Id)
    //     dp.Add("Name", meetingRoom.Name)
    //     dp.Add("Code", Option.defaultValue null meetingRoom.Code)

    //     connection.Execute("
    //         UPDATE [dbo].[MeetingRooms]
    //          SET [Name] = @Name
    //            ,[Code] = @Code
    //          WHERE [Id] = @Id", dp)


    // let deleteMeetingRoom (id:int)  (connection:IDbConnection) =
    //     let dp = DynamicParameters()
    //     dp.Add("Id", id)

    //     connection.Execute("
    //         DELETE FROM [dbo].[MeetingRooms]
    //          WHERE [Id] = @Id", dp)
