namespace UI

open Fable.React
open Fable.React.Props
open Fulma

open UI.Model
open UI.Messages.Type
open UI
open Fable.Core.JS

module View =
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


    let showContent (model:Model) (dispatch : Msg -> unit) =
        if model.Loading then
            [div [] [str "Loading...."]]
        else
              console.log ("Log", model.Page.ToString())
              match model.Page with
              | Parser.Type.Page.UserList -> User.List.showList model.Users dispatch
              | Parser.Type.Page.MeetingRoomNew -> [MeetingRoom.New.newForm model dispatch]
              | Parser.Type.Page.MeetingRoom _ -> [MeetingRoom.Edit.editForm model dispatch]
              | Parser.Type.Page.MeetingRoomList -> MeetingRoom.List.showList model.MeetingRooms dispatch
              | Parser.Type.Page.ReservationList -> Reservation.List.showList model.Reservations dispatch


    let menuItem label isActive =
        Menu.Item.li [ Menu.Item.IsActive isActive ]
            [ str label ]

    let menuLink url label isActive =
        Menu.Item.li [ Menu.Item.IsActive isActive ]
            [ a [ Href url ] [ str label ]  ]

    let view (model : Model) (dispatch : Msg -> unit) =
        div []
            [ Navbar.navbar [ Navbar.Color IsPrimary ]
                [ Navbar.Item.div [ ]
                    [ Heading.h2 [ ]
                        [ str "Meeting Room List" ] ] ]
              Menu.menu [ ]
                [ Menu.label [ ] [ str "General" ]
                  Menu.list [ ]
                    [ menuLink "#/meetingroomList" "Meeting Rooms" true
                      menuLink "#/userList" "Users" false
                      menuLink "#/reservationList" "Reservations" false ] ]
              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      [ a [ Href ("#/meetingroomNew") ] [ str "New"]  ]
                  ]
              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      (showContent model dispatch)
                  ]
              Footer.footer [ ]
                    [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ safeComponents ] ] ]