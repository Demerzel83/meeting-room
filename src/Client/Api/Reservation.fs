namespace UI.Api

open Thoth.Fetch

open  MeetingRoom.Shared

module Reservation =

    let getAllReservations ()
        = Fetch.fetchAs<Reservation list> "/api/reservations"

    let getReservation (id:string)
        = Fetch.fetchAs<Reservation option> ("api/reservations/" + id)



