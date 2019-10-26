namespace Reservation.Infrastructure

open ReservationDb
open MeetingRoom.Shared
open FSharpPlus.Data
open System.Data

module ReservationReader =

    let getAll (): Reader<IDbConnection, Reservation list> =
        Reader getAll

    let get id : Reader<IDbConnection, Reservation option> =
        Reader
         (get id)

    let insert (reservation:Reservation): Reader<IDbConnection, int> =
        Reader (insert reservation)

    let update (reservation:Reservation): Reader<IDbConnection, int>  =
        Reader (update reservation)

    let delete  (id:int): Reader<IDbConnection, int>  =
        Reader (delete id)
