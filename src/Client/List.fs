namespace UI

open Fable.React
open Fable.React.Props
open Fulma

open Shared
open UI.Model
open UI.Messages

module List =
    let safeComponents =
        let components =
            span [ ]
               [ a [ ]
                   [ str "SAFE  "
                     str Version.template ]
                 str ", "
                 a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
                 str ", "
                 a [ Href "http://fable.io" ] [ str "Fable" ]
                 str ", "
                 a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
                 str ", "
                 a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]

               ]

        span [ ]
            [ str "Version "
              strong [ ] [ str Version.app ]
              str " powered by: "
              components ]

    let showCode code =
        match code with
        | Some c -> str c
        | None -> str "No Code"

    let showRows meetingRooms =
        List.map (fun mr -> tr [ ]
                                     [ td [ ] [ a [ Href ("api/meetingrooms/" + mr.Id.ToString()) ] [ str "Open"]  ]
                                       td [ ] [ str mr.Name ]
                                       td [ ] [ (showCode mr.Code) ] ]) meetingRooms

    let showList meetingRooms =
      [ Table.table [ Table.IsHoverable ]
                            [ thead [ ]
                                [ tr [ ]
                                    [ th [ ] [ str "Id" ]
                                      th [ ] [ str "Name" ]
                                      th [ ] [ str "Code" ] ] ]
                              tbody [ ]
                                 (showRows meetingRooms)
                                   ] ]

    let view (model : Model) (dispatch : Msg -> unit) =
        div []
            [ Navbar.navbar [ Navbar.Color IsPrimary ]
                [ Navbar.Item.div [ ]
                    [ Heading.h2 [ ]
                        [ str "Meeting Room List" ] ] ]

              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      (showList model.MeetingRooms)
                  ]
              Footer.footer [ ]
                    [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ safeComponents ] ] ]