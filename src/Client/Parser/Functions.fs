namespace UI.Parser

open Elmish
open Elmish.UrlParser

open UI.Model
open UI.Messages.Type
open UI.Parser.Type
open UI.Api.MeetingRoom
open UI.Api.User
open UI.Api.Reservation

module Functions =
    /// The URL is turned into a Page option.
    let pageParser : Parser<Page -> Page,_>  =
        oneOf
            [
              map Page.ReservationList ( s "reservationList")
              map Page.UserList ( s "userList")
              map Page.MeetingRoomNew (s "meetingroomNew")
              map Page.ReservationNew (s "reservationNew")
              map Page.UserNew (s "userNew")
              map Page.MeetingRoomList (s "meetingroomList")
              map Page.MeetingRoom (s "meetingroom" </> str)
              map Page.User (s "user" </> str)
              map Page.Reservation (s "reservation" </> str)
            ]

    let urlParser location =
        parseHash pageParser location

    let urlUpdate (result:Page option) model =
      match result with
      | Some (Page.UserList) ->
        model, Cmd.OfPromise.either getAllUsers () UsersLoaded (fun ex -> FetchFailure ("error", ex))
      | Some (Page.ReservationList) ->
        { model with Loading = true}, Cmd.OfPromise.either getAllReservations () ReservationsLoaded (fun ex -> FetchFailure ("error", ex))
      | Some (Page.MeetingRoom id) ->
          { model with Page = (Page.MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom (id.ToString()) FetchMeetingRoomSuccess (fun ex -> FetchFailure (id,ex))
      | Some (Page.User id) ->
          { model with Page = (Page.User id) }, Cmd.OfPromise.either getUser (id.ToString()) FetchUserSuccess (fun ex -> FetchFailure (id,ex))
      | Some (Page.Reservation id) ->
          { model with Page = (Page.Reservation id) }, Cmd.OfPromise.either getReservation (id.ToString()) FetchReservationSuccess (fun ex -> FetchFailure (id,ex))

      | Some page -> { model with Page = page }, Cmd.none
      | None -> { model with Page = Page.MeetingRoomList }, Cmd.none