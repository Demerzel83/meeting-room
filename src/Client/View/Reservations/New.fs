namespace UI.Reservation

open Fable.React
open Fulma
open Fulma.Elmish

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:Msg -> unit) =
        let pickerFromConfig : DatePicker.Types.Config<Msg> =
          DatePicker.Types.defaultConfig FromUpdated

        let pickerToConfig : DatePicker.Types.Config<Msg> =
          DatePicker.Types.defaultConfig ToUpdated

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
                            [ DatePicker.View.root pickerFromConfig model.DatePickerFromState (Some model.Reservation.From) dispatch
                            ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "To" ]
                          Control.div [ ]
                            [ DatePicker.View.root pickerToConfig model.DatePickerToState (Some model.Reservation.To) dispatch
                            ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveNewReservation) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadReservations) ]
                            [ str "Cancel" ] ] ]
            ]