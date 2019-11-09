namespace Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn

open MeetingRoom.Shared
open System.Data
open FSharpPlus.Data
open User.Infrastructure

module User =
    let getHandlers (connection:IDbConnection) =
            choose [
                GET >=> routef "/api/users/%i" (fun id next ctx ->
                    task {
                        let user =
                            id
                            |> UserReader.get

                        let result = Reader.run user connection

                        return! json result next ctx
                    })
                GET >=> route "/api/users" >=> (fun next ctx ->
                    task {
                        let users = UserReader.getAll()
                        let result = Reader.run users connection
                        return! json result next ctx
                    }
                )
                DELETE >=> routef "/api/users/%i" (fun id next ctx ->
                    task {
                        let deleted =
                            id
                            |> UserReader.delete

                        let result = Reader.run deleted connection

                        return! json result next ctx
                    })
                PUT >=> route "/api/users" >=> (fun next ctx ->
                    task {
                        let! user = Controller.getModel<User> ctx
                        let result = Reader.run (UserReader.update user) connection

                        return! ctx.WriteJsonAsync result
                    }
                )
                POST >=> route "/api/users/new" >=> (fun next ctx ->
                    task {
                        let! newUser = Controller.getModel<User> ctx
                        let result = Reader.run ( UserReader.insert newUser) connection

                        return! ctx.WriteJsonAsync result
                    }
                )
            ]