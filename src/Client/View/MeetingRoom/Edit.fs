namespace UI.MeetingRoom

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module Edit =
    let editForm (model:Model)  (dispatch:Msg -> unit) =
        match model.MeetingRoom with
        | None -> div [] [str "Missing meeting room"]
        | Some meetingRoom ->
            form [ ]
                 [
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "Name" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value meetingRoom.Name;
                                Input.OnChange (fun event -> dispatch (NameUpdated event.Value) ) ] ] ]
                   Field.div [ ]
                        [ Label.label [ ]
                            [ str "Code" ]
                          Control.div [ ]
                            [ Input.text [
                                Input.Value (Option.defaultValue ""  meetingRoom.Code);
                                Input.OnChange (fun event -> dispatch (CodeUpdated event.Value)) ] ] ]
                   Field.div [ Field.IsGrouped ]
                    [ Control.div [ ]
                        [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveMeetingRoom) ]
                            [ str "Save" ] ]
                      Control.div [ ]
                        [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadMeetingRooms) ]
                            [ str "Cancel" ] ] ]
            ]