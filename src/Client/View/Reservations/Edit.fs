namespace UI.Reservation

open Fable.React
open Fulma
open Fulma.Elmish

open MeetingRoom.Shared
open UI.Model
open UI.Messages.Type
open Fable.FontAwesome

module Edit =
    let editForm (model:Model)  (dispatch:Msg -> unit) =
        let pickerFromConfig : DatePicker.Types.Config<Msg> =
          DatePicker.Types.defaultConfig FromUpdated

        let pickerToConfig : DatePicker.Types.Config<Msg> =
          DatePicker.Types.defaultConfig ToUpdated

        let reservation = model.Reservation
        form [ ]
             [
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Meeting Room" ]
                      Control.div [ ]
                        [ Dropdown.dropdown [ Dropdown.IsActive (model.Reservations.Length > 1) ]
                            [ div [ ]
                                [ Button.button [ Button.OnClick (fun _ -> dispatch FetchMeetingRooms) ]
                                    [ span [ ]
                                        [ str (model.Reservation.MeetingRoom.Id.ToString()) ]
                                      Icon.icon [ Icon.Size IsSmall ]
                                        [ Fa.i [ Fa.Solid.AngleDown ]
                                            [ ] ] ] ]
                              Dropdown.menu [ ]
                                [ Dropdown.content [  ]
                                    (List.map (fun (mr:MeetingRoom) ->  Dropdown.Item.a [ Dropdown.Item.IsActive (mr.Id = model.Reservation.MeetingRoom.Id);  ] [ Button.button [ Button.OnClick (fun _ -> dispatch (MeetingRoomUpdated ( mr.Id.ToString()) ))] [ str mr.Name] ]) model.MeetingRooms)
                                ]
                            ]
                        ]

                    ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "User" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value ( reservation.User.Id.ToString());
                            Input.OnChange (fun event -> dispatch (UserUpdated event.Value)) ] ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "From" ]
                      Control.div [ ]
                        [ DatePicker.View.root pickerFromConfig model.DatePickerFromState (Some reservation.From) dispatch
                        ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "To" ]
                      Control.div [ ]
                        [ DatePicker.View.root pickerToConfig model.DatePickerToState (Some reservation.To) dispatch
                        ] ]
               Field.div [ Field.IsGrouped ]
                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveReservation) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadReservations) ]
                        [ str "Cancel" ] ] ]
        ]