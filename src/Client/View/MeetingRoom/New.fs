namespace UI.MeetingRoom

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:Msg<MeetingRoomMessages> -> unit) =
        form [ ]
             [ // Name field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Name" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value model.MeetingRoom.Name;
                            Input.OnChange (fun event -> dispatch (Msg (MeetingRoomNameUpdated event.Value)) ) ] ] ]
               // Username field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Code" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  model.MeetingRoom.Code);
                            Input.OnChange (fun event -> dispatch (Msg (MeetingRoomCodeUpdated event.Value)))] ] ]
               Field.div [ Field.IsGrouped ]
                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch (Msg SaveNewMeetingRoom)) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch (Msg LoadMeetingRooms)) ]
                        [ str "Cancel" ] ] ]
        ]