namespace UI.Reservation

open Fable.React
open Fulma
open Fulma.Elmish

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:Msg<ReservationsMessages> -> unit) =
        let pickerFromConfig : DatePicker.Types.Config<ReservationsMessages> =
          DatePicker.Types.defaultConfig FromUpdated

        let pickerToConfig : DatePicker.Types.Config<ReservationsMessages> =
          DatePicker.Types.defaultConfig ToUpdated

        form [ ]
                 [
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "Meeting Room" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value (model.Reservation.MeetingRoom.Name.ToString())
                                Input.OnChange (fun event -> dispatch (Msg (MeetingRoomUpdated model.Reservation.MeetingRoom) )) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "User" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value ( model.Reservation.User.Email.ToString());
                                Input.OnChange (fun event -> dispatch (Msg (UserUpdated ({ model.User with Name = Some event.Value})))) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "From" ]
                          Control.div [ ]
                            [ DatePicker.View.root pickerFromConfig model.DatePickerFromState (Some model.Reservation.From)  (fun msg -> dispatch (Msg msg))
                            ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "To" ]
                          Control.div [ ]
                            [ DatePicker.View.root pickerToConfig model.DatePickerToState (Some model.Reservation.To) (fun msg -> dispatch (Msg msg))
                            ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch (Msg SaveNewReservation)) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch (Msg LoadReservations)) ]
                            [ str "Cancel" ] ] ]
            ]