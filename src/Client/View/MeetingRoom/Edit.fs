namespace UI.MeetingRoom

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module Edit =
    let editForm (model:Model)  (dispatch:Msg -> unit) =
        let meetingRoom = model.MeetingRoom
        form [ ]
             [
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Name" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value meetingRoom.Name;
                            Input.OnChange (fun event -> dispatch (MeetingRoomNameUpdated event.Value) ) ] ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Code" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  meetingRoom.Code);
                            Input.OnChange (fun event -> dispatch (MeetingRoomCodeUpdated event.Value)) ] ] ]
               Field.div [ Field.IsGrouped ]
                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveMeetingRoom) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadMeetingRooms) ]
                        [ str "Cancel" ] ] ]
        ]