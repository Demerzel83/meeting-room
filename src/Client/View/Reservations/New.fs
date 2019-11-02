namespace UI.Reservation

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:Msg -> unit) =
        form [ ]
                 [
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "Meeting Room" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value (model.Reservation.MeetingRoomId.ToString())
                                Input.OnChange (fun event -> dispatch (MeetingRoomUpdated event.Value) ) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "User" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.Reservation.UserId.ToString());
                                Input.OnChange (fun event -> dispatch (UserUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "From" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.Reservation.From.ToString());
                                Input.OnChange (fun event -> dispatch (FromUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "To" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.Reservation.To.ToString());
                                Input.OnChange (fun event -> dispatch (ToUpdated event.Value)) ] ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveNewReservation) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadReservations) ]
                            [ str "Cancel" ] ] ]
            ]