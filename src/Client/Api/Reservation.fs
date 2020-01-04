namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared

module Reservation =

    let getAll ()
        = Fetch.fetchAs<Reservation list> "/api/reservations"

    let get (id:string)
        = Fetch.fetchAs<Reservation> ("api/reservations/" + id)

    let update (reservation:Reservation) =
        Fetch.put ("api/reservations/", (Enconding.encodeReservation reservation), Decode.int)

    let create (reservation:Reservation) =
        Fetch.post ("api/reservations/new", (Enconding.encodeReservation reservation), Decode.int)

    let delete (id : int) =
        let url = sprintf "api/reservations/%d" id
        Fetch.delete(url, null, Decode.int)




