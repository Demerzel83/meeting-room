namespace UI.User

open Fable.React
open Fable.React.Props
open Fulma

open  MeetingRoom.Shared
open UI.Messages.Type

module List =

    let showRows (reservations:User list) (dispatch : Msg -> unit) =
        List.map (fun (mr:User) -> tr [ ]
                                             [ td [ ] [ a [ Href ("#/user/" + mr.Id.ToString()) ] [ str "Open"]  ]
                                               td [ ] [ str (mr.Name.ToString()) ]
                                               td [ ] [ str (mr.Surname.ToString()) ]
                                               td [ ] [ str (mr.Email.ToString()) ]
                                               td [ ] [ Button.button [ Button.Color IsDanger; Button.OnClick (fun _ -> dispatch (DeleteUser mr.Id)) ]
                                                            [ str "Delete" ] ] ]) reservations

    let showList users (dispatch : Msg -> unit) =
       Table.table [ Table.IsHoverable ]
                            [ thead [ ]
                                [ tr [ ]
                                    [ th [ ] [ str "Id" ]
                                      th [ ] [ str "Name" ]
                                      th [ ] [ str "SurName" ]
                                      th [ ] [ str "Email"] ] ]
                              tbody [ ]
                                 (showRows users dispatch)
                                   ]