namespace MeetingRoom.Api

open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open System

open MeetingRoom.Shared
open MeetingRoom.Infrastructure.MeetingRoomReader
open User.Infrastructure.UserReader
open System.Data
open FSharpPlus.Data
open Reservation.Infrastructure.ReservationReader

module Route =
    let Definition (connection:IDbConnection) = router {
        get "/api/reservations" (fun next ctx ->
            task {
                let reservations = getAllReservations ()
                let result = Reader.run reservations connection
                return! json result next ctx
            }
        )
        get "/api/users" (fun next ctx ->
            task {
                let users = getAllUsers()
                let result = Reader.run users connection
                return! json result next ctx
            }
        )
        get "/api/meetingrooms" (fun next ctx ->
            task {
                let meetingRooms = getAllMeetingRooms()
                let result = Reader.run meetingRooms connection

                return! json result next ctx
            })
        getf "/api/meetingrooms/%i" (fun id next ctx ->
            task {
                let meetingRoom =
                    id
                    |> getMeetingRoom

                let result = Reader.run meetingRoom connection

                return! json result next ctx
            })
        deletef "/api/meetingrooms/%i" (fun id next ctx ->
            task {
                let changes =
                    id
                    |> deleteMeetingRoom
                let result = Reader.run changes connection

                return! ctx.WriteJsonAsync result
            }
        )
        put "/api/meetingrooms/" (fun next ctx ->
            task {
                let! meetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run (updateMeetingRoom meetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
        post "/api/meetingrooms/new" (fun next ctx ->
            task {
                let! newMeetingRoom = Controller.getModel<MeetingRoom> ctx
                let result = Reader.run ( insertMeetingRoom newMeetingRoom) connection

                return! ctx.WriteJsonAsync result
            }
        )
    }