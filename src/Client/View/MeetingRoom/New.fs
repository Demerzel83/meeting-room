namespace UI.MeetingRoom

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:Msg -> unit) =
        form [ ]
             [ // Name field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Name" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value model.NewMeetingRoom.Name;
                            Input.OnChange (fun event -> dispatch (NewNameUpdated event.Value) ) ] ] ]
               // Username field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Code" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  model.NewMeetingRoom.Code);
                            Input.OnChange (fun event -> dispatch (NewCodeUpdated event.Value)) ] ] ]
               Field.div [ Field.IsGrouped ]
                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveNewMeetingRoom) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadMeetingRooms) ]
                        [ str "Cancel" ] ] ]
        ]