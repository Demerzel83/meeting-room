namespace User.Infrastructure

open Dapper

open MeetingRoom.Utils.Dapper
open MeetingRoom.Shared
open System.Data

module UserDb =

    let getAll (connection:IDbConnection) =
            dapperQuery<User> connection "SELECT Id, Name, Surname, Email FROM dbo.Users"
            |> List.ofSeq


    let get (id:int) (connection:IDbConnection)  =
            let dp = DynamicParameters()
            dp.Add("Id", id)

            let mr =
                dapperParametrizedQuery<User> connection "SELECT Id, Name, Surname, Email FROM dbo.Users WHERE Id = @Id" dp
                |> List.ofSeq

            match mr with
            | [mro] -> Some mro
            | _ -> None


    let insert  (user:User) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Name", user.Name)
        dp.Add("Surname", Option.defaultValue null user.Surname)
        dp.Add("Email", user.Email)

        connection.Execute("
            INSERT INTO [dbo].[Users]
               ([Name]
               ,[Surname]
               ,[Email])
         VALUES (@Name,@Surname,@Email)", dp)


    let update (user:User) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", user.Id)
        dp.Add("Name", user.Name)
        dp.Add("Surname", Option.defaultValue null user.Surname)
        dp.Add("Email", user.Email)

        connection.Execute("
            UPDATE [dbo].[Users]
             SET [Name] = @Name
               ,[Surname] = @Surname
               ,[Email] = @Email
             WHERE [Id] = @Id", dp)


    let delete (id:int)  (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", id)

        connection.Execute("
            DELETE FROM [dbo].[Users]
             WHERE [Id] = @Id", dp)
