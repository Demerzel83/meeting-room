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
        dp.Add("Email", user.Email)
        dp.Add("Surname", Option.defaultValue null user.Surname)

        connection.Execute("
            INSERT INTO [dbo].[Users]
               ([Name]
               ,[Email]
               ,[Surname])
         VALUES (@Name,@Email,@Surname)", dp)


    let update (user:User) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", user.Id)
        dp.Add("Name", user.Name)
        dp.Add("Email", user.Email)
        dp.Add("Surname", Option.defaultValue null user.Surname)

        connection.Execute("
            UPDATE [dbo].[Users]
             SET [Name] = @Name
               ,[Email] = @Email
               ,[Surname] = @Surname
             WHERE [Id] = @Id", dp)


    let delete (id:int)  (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", id)

        connection.Execute("
            DELETE FROM [dbo].[Users]
             WHERE [Id] = @Id", dp)
