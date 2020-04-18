namespace UI.Parser

open Elmish
open Elmish.UrlParser
open Elmish.Navigation

open UI.Model
open UI.Messages.Type
open UI.Parser.Type
open UI.Api.MeetingRoom
open UI.Api

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

    // let urlUpdate (result:Page option) model =
    //   match result with
    //   | Some (Page.UserList) ->
    //     {model with Page = Page.UserList}, Cmd.OfPromise.either User.getAll () UsersLoaded (fun ex -> FetchFailure ("error", ex))
    //   | Some (Page.ReservationList) ->
    //     { model with Page = Page.ReservationList; LoadingPage = true}, Cmd.OfPromise.either Reservation.getAll () ReservationsLoaded (fun ex -> FetchFailure ("error", ex))
    //   | Some (Page.MeetingRoom id) ->
    //     { model with Page = (Page.MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom (id.ToString()) FetchMeetingRoomSuccess (fun ex -> FetchFailure (id,ex))
    //   | Some (Page.User id) ->
    //     { model with Page = (Page.User id) }, Cmd.OfPromise.either User.get (id.ToString()) FetchUserSuccess (fun ex -> FetchFailure (id,ex))
    //   | Some (Page.Reservation id) ->
    //     { model with Page = (Page.Reservation id) }, Cmd.OfPromise.either Reservation.get (id.ToString()) FetchReservationSuccess (fun ex -> FetchFailure (id,ex))
    //   | Some page -> { model with Page = page }, Cmd.none
    //   | None -> { model with Page = Page.MeetingRoomList }, Cmd.none

    let urlUpdate (result:Page option) model =
      match result with
      | Some (Page.UserList) ->
        {model with Page = Page.UserList}, Cmd.OfPromise.perform User.getAll () UsersLoaded
      | Some (Page.ReservationList) ->
        { model with Page = Page.ReservationList; LoadingPage = true}, Cmd.OfPromise.perform Reservation.getAll () ReservationsLoaded
      | Some (Page.MeetingRoom id) ->
        { model with Page = (Page.MeetingRoom id) }, Cmd.OfPromise.perform getMeetingRoom (id.ToString()) FetchMeetingRoomSuccess
      | Some (Page.User id) ->
        { model with Page = (Page.User id) }, Cmd.OfPromise.perform User.get (id.ToString()) FetchUserSuccess
      | Some (Page.Reservation id) ->
        { model with Page = (Page.Reservation id) }, Cmd.OfPromise.perform Reservation.get (id.ToString()) FetchReservationSuccess
      | Some page -> { model with Page = page }, Cmd.none
      | None -> { model with Page = Page.MeetingRoomList }, Cmd.none