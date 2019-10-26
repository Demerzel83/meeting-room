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
                                Input.Value (model.NewReservation.MeetingRoomId.ToString())
                                Input.OnChange (fun event -> dispatch (NameUpdated event.Value) ) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "User" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.NewReservation.UserId.ToString());
                                Input.OnChange (fun event -> dispatch (CodeUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "From" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.NewReservation.From.ToString());
                                Input.OnChange (fun event -> dispatch (CodeUpdated event.Value)) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "To" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.NewReservation.To.ToString());
                                Input.OnChange (fun event -> dispatch (CodeUpdated event.Value)) ] ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveMeetingRoom) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadMeetingRooms) ]
                            [ str "Cancel" ] ] ]
            ]