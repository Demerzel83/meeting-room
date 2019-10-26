namespace UI

open Fable.React
open Fable.React.Props
open Fulma

open UI.Model
open UI.Messages.Type
open UI
open Fable.Core.JS
open UI.Parser.Type

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
            div [] [str "Loading...."]
        else
              console.log ("Page", model.Page)
              match model.Page with
              | Page.MeetingRoomList -> MeetingRoom.List.showList model.MeetingRooms dispatch
              | Page.ReservationList -> Reservation.List.showList model.Reservations dispatch
              | Page.UserList -> User.List.showList model.Users dispatch
              | Page.MeetingRoomNew -> MeetingRoom.New.newForm model dispatch
              | Page.MeetingRoom _ -> MeetingRoom.Edit.editForm model dispatch
              | Page.User _ -> User.Edit.editForm model dispatch
              | Page.Reservation _ -> Reservation.Edit.editForm model dispatch
              | Page.ReservationNew -> Reservation.New.newForm model dispatch
              | Page.UserNew -> User.New.newForm model dispatch


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
                      ( match model.Page with
                        | Page.MeetingRoomList -> [ a [ Href ("#/meetingroomNew") ] [ str "New"]  ]
                        | Page.ReservationList -> [ a [ Href ("#/reservationNew") ] [ str "New"]  ]
                        | Page.UserList -> [ a [ Href ("#/userNew") ] [ str "New"]  ]
                        | _ -> [ str "Other"] )

                  ]
              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      [showContent model dispatch]
                  ]
              Footer.footer [ ]
                    [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ safeComponents ] ] ]