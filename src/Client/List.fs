namespace UI

open Fable.React
open Fable.React.Props
open Fulma

open Shared
open UI.Messages.Type

module List =
    let showCode code =
        match code with
        | Some c -> str c
        | None -> str "No Code"

    let showRows meetingRooms (dispatch : Msg -> unit) =
        List.map (fun mr -> tr [ ]
                                 [ td [ ] [ a [ Href ("#/meetingroom/" + mr.Id.ToString()) ] [ str "Open"]  ]
                                   td [ ] [ str mr.Name ]
                                   td [ ] [ (showCode mr.Code) ]
                                   td [ ] [ Button.button [ Button.Color IsDanger; Button.OnClick (fun _ -> dispatch (DeleteMeetingRoom mr.Id)) ]
                                                [ str "Delete" ] ] ]) meetingRooms

    let showList meetingRooms (dispatch : Msg -> unit) =
      [ Table.table [ Table.IsHoverable ]
                            [ thead [ ]
                                [ tr [ ]
                                    [ th [ ] [ str "Id" ]
                                      th [ ] [ str "Name" ]
                                      th [ ] [ str "Code" ] ] ]
                              tbody [ ]
                                 (showRows meetingRooms dispatch)
                                   ] ]