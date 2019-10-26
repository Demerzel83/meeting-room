namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn

open MeetingRoom.Shared
open System.Data
open FSharpPlus.Data
open Reservation.Infrastructure
open User.Infrastructure
open MeetingRoom.Infrastructure

module Route =
    let Definition (connection:IDbConnection) = router {
        get "/api/reservations" (fun next ctx ->
            task {
                let reservations = ReservationReader.getAll ()
                let result = Reader.run reservations connection
                return! json result next ctx
            }
        )
        getf "/api/reservations/%i" (fun id next ctx ->
            task {
                let reservation =
                    id
                    |> ReservationReader.get

                let result = Reader.run reservation connection

                return! json result next ctx
            })
        deletef "/api/reservations/%i" (fun id next ctx ->
            task {
                let deleted =
                    id
                    |> ReservationReader.delete

                let result = Reader.run deleted connection

                return! json result next ctx
            })
        put "/api/reservations/" (fun next ctx ->
            task {
                let! reservation = Controller.getModel<Reservation> ctx
                let result = Reader.run (ReservationReader.update reservation) connection

                return! ctx.WriteJsonAsync result
            }
        )
        post "/api/reservations/new" (fun next ctx ->
            task {
                let! newReservation = Controller.getModel<Reservation> ctx
                let result = Reader.run ( ReservationReader.insert newReservation) connection

                return! ctx.WriteJsonAsync result
            }
        )
        getf "/api/users/%i" (fun id next ctx ->
            task {
                let user =
                    id
                    |> UserReader.get

                let result = Reader.run user connection

                return! json result next ctx
            })
        get "/api/users" (fun next ctx ->
            task {
                let users = UserReader.getAll()
                let result = Reader.run users connection
                return! json result next ctx
            }
        )
        deletef "/api/users/%i" (fun id next ctx ->
            task {
                let deleted =
                    id
                    |> UserReader.delete

                let result = Reader.run deleted connection

                return! json result next ctx
            })
        put "/api/users/" (fun next ctx ->
            task {
                let! user = Controller.getModel<User> ctx
                let result = Reader.run (UserReader.update user) connection

                return! ctx.WriteJsonAsync result
            }
        )
        post "/api/users/new" (fun next ctx ->
            task {
                let! newUser = Controller.getModel<User> ctx
                let result = Reader.run ( UserReader.insert newUser) connection

                return! ctx.WriteJsonAsync result
            }
        )
        get "/api/meetingrooms" (fun next ctx ->
            task {
                let meetingRooms = MeetingRoomReader.getAll()
                let result = Reader.run meetingRooms connection

                return! json result next ctx
            })
        getf "/api/meetingrooms/%i" (fun id next ctx ->
            task {
                let meetingRoom =
                    id
                    |> MeetingRoomReader.get

                let result = Reader.run meetingRoom connection

                return! json result next ctx
            })
        deletef "/api/meetingrooms/%i" (fun id next ctx ->
            task {
                let changes =
                    id
                    |> MeetingRoomReader.delete
                let result = Reader.run changes connection

                return! ctx.WriteJsonAsync result
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run (MeetingRoomReader.update meetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run ( MeetingRoomReader.insert newMeetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
    }