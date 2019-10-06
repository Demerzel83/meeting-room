namespace UI.Reservation

open Fable.React
open Fable.React.Props
open Fulma

open  MeetingRoom.Shared
open UI.Messages.Type

module List =

    let showRows (reservations:Reservation list) (dispatch : Msg -> unit) =
        List.map (fun (mr:Reservation) -> tr [ ]
                                             [ td [ ] [ a [ Href ("#/reservation/" + mr.Id.ToString()) ] [ str "Open"]  ]
                                               td [ ] [ str (mr.From.ToString()) ]
                                               td [ ] [ str (mr.To.ToString()) ]
                                               td [ ] [ str (mr.UserId.ToString()) ]
                                               td [ ] [ str (mr.MeetingRoomId.ToString())]
                                               td [ ] [ Button.button [ Button.Color IsDanger; Button.OnClick (fun _ -> dispatch (DeleteReservation mr.Id)) ]
                                                            [ str "Delete" ] ] ]) reservations

    let showList reservations (dispatch : Msg -> unit) =
      [ Table.table [ Table.IsHoverable ]
                            [ thead [ ]
                                [ tr [ ]
                                    [ th [ ] [ str "Id" ]
                                      th [ ] [ str "From" ]
                                      th [ ] [ str "To" ]
                                      th [ ] [ str "UserId"]
                                      th [ ] [ str "MeetingRoomId"] ] ]
                              tbody [ ]
                                 (showRows reservations dispatch)
                                   ] ]