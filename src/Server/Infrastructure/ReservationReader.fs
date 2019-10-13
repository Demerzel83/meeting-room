namespace Reservation.Infrastructure

open System

open ReservationDb
open MeetingRoom.Shared
open FSharpPlus.Data
open System.Data

module ReservationReader =

    let getAllReservations (): Reader<IDbConnection, Reservation list> =
        Reader getAllReservations

    // let getMeetingRoom id : Reader<IDbConnection, MeetingRoom option> =
    //     Reader
    //      (getMeetingRoom id)

    // let insertMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int> =
    //     Reader (insertMeetingRoom meetingRoom)

    // let updateMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int>  =
    //     Reader (updateMeetingRoom meetingRoom)

    // let deleteMeetingRoom  (id:int): Reader<IDbConnection, int>  =
    //     Reader (deleteMeetingRoom id)
