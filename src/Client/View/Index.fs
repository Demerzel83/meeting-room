namespace UI

open Fable.React
open Fable.React.Props
open Fulma

open UI.Model
open UI.Messages.Type
open UI
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
        if model.LoadingPage then
            div [] [str "Loading...."]
        else
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

    let showError model dispatch =
      match model.Error with
          | Some (_, ex) ->
            Notification.notification [ Notification.Color IsDanger ]
                [ Button.button [ Button.OnClick (fun _ -> dispatch ClearError) ] [ ]; str (ex.ToString ()) ]
          | None -> span [] []

    let view (model : Model) (dispatch : Msg -> unit) =
        let isPage = (=) model.Page

        div []
            [ Navbar.navbar [ Navbar.Color IsPrimary ]
                [ Navbar.Item.div [ ]
                    [ Heading.h2 [ ]
                        [ str "Meeting Room List" ] ] ]
              Menu.menu [ ]
                [ Menu.label [ ] [ str "General" ]
                  Menu.list [ ]
                    [ menuLink "#/meetingroomList" "Meeting Rooms" (isPage Page.MeetingRoomList)
                      menuLink "#/userList" "Users" (isPage Page.UserList)
                      menuLink "#/reservationList" "Reservations" (isPage Page.ReservationList) ] ]
              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      ( match model.Page with
                        | Page.MeetingRoomList -> [ a [ Href ("#/meetingroomNew") ] [ str "New"]  ]
                        | Page.ReservationList -> [ a [ Href ("#/reservationNew") ] [ str "New"]  ]
                        | Page.UserList -> [ a [ Href ("#/userNew") ] [ str "New"]  ]
                        | _ -> [ str "Other"] )

                  ]
              showError model dispatch
              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                      [showContent model dispatch]
                  ]

              Footer.footer [ ]
                    [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ safeComponents ] ] ]