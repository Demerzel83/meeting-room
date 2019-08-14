namespace UI
open Fable.React
open Messages
open Model
open Fulma

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
                    [ Button.button [ Button.IsLink ]
                        [ str "Cancel" ] ] ]
        ]