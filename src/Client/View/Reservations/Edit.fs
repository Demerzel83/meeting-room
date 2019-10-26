namespace UI.Reservation

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module Edit =
    let editForm (model:Model)  (dispatch:Msg -> unit) =
        match model.Reservation with
        | None -> div [] [str "Missing Reservation"]
        | Some reservation ->
            form [ ]
                 [
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "Meeting Room" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value (reservation.MeetingRoomId.ToString())
                                Input.OnChange (fun event -> dispatch (MeetingRoomUpdated event.Value) ) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "User" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( reservation.UserId.ToString());
                                Input.OnChange (fun event -> dispatch (UserUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "From" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( reservation.From.ToString());
                                Input.OnChange (fun event -> dispatch (FromUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "To" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( reservation.To.ToString());
                                Input.OnChange (fun event -> dispatch (ToUpdated event.Value)) ] ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveReservation) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadReservations) ]
                            [ str "Cancel" ] ] ]
            ]