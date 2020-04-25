namespace Reservations.Api

open FSharp.Control.Tasks.V2
open Giraffe

open Reservations.Core.Types
open System.Data
open FSharpPlus.Data
open Reservations.Infrastructure


module Reservation =
    let getHandlers (connection:IDbConnection) =
            choose [
                GET >=> route "/api/reservations" >=> (fun next ctx ->
                    task {
                        let reservations = ReservationReader.getAll ()
                        let result = Reader.run reservations connection
                        return! json result next ctx
                    }
                )
                GET >=> routef "/api/reservations/%i" (fun id next ctx ->
                    task {
                        let reservation =
                            id
                            |> ReservationReader.get

                        let result = Reader.run reservation connection

                        return! json result next ctx
                    })
                DELETE >=> routef "/api/reservations/%i" (fun id next ctx ->
                    task {
                        let deleted =
                            id
                            |> ReservationReader.delete

                        let result = Reader.run deleted connection

                        return! json result next ctx
                    })
                PUT >=> route "/api/reservations/" >=> (fun next ctx ->
                    task {
                        let! reservation = ctx.BindJsonAsync<Reservation>()
                        let result = Reader.run (ReservationReader.update reservation) connection

                        return! ctx.WriteJsonAsync result
                    }
                )
                POST >=> route "/api/reservations/new" >=> (fun next ctx ->
                    task {
                        let! newReservation = ctx.BindJsonAsync<Reservation> ()
                        let result = Reader.run ( ReservationReader.insert newReservation) connection

                        return! ctx.WriteJsonAsync result
                    }
                )
            ]