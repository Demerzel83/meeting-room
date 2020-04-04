namespace UI.Reservation

open Fable.React
open Fable.React.Props
open Fulma

open  MeetingRoom.Shared
open UI.Messages.Type

module List =

    let showRows (reservations:Reservation list) (dispatch : ReservationsMessages -> unit) =
        List.map (fun (mr:Reservation) -> tr [ ]
                                             [ td [ ] [ a [ Href ("#/reservation/" + mr.Id.ToString()) ] [ str "Open"]  ]
                                               td [ ] [ str (mr.From.ToString("dd-MM-yyyy HH:mm:ss")) ]
                                               td [ ] [ str (mr.To.ToString("dd-MM-yyyy HH:mm:ss")) ]
                                               td [ ] [ str (mr.User.Email) ]
                                               td [ ] [ str (mr.MeetingRoom.Name)]
                                               td [ ] [ Button.button [ Button.Color IsDanger; Button.OnClick (fun _ -> dispatch (DeleteReservation mr.Id)) ]
                                                            [ str "Delete" ] ] ]) reservations

    let showList reservations (dispatch : ReservationsMessages -> unit) =
       Table.table [ Table.IsHoverable ]
                            [ thead [ ]
                                [ tr [ ]
                                    [ th [ ] [ str "Id" ]
                                      th [ ] [ str "From" ]
                                      th [ ] [ str "To" ]
                                      th [ ] [ str "UserId"]
                                      th [ ] [ str "MeetingRoomId"] ] ]
                              tbody [ ]
                                 (showRows reservations dispatch)
                                   ]