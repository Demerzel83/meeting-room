namespace UI.Messages

open System
open Fulma.Elmish

open MeetingRoom.Shared

module Type =
    // The Msg type defines what events/actions can occur while the application is running
    // the state of the application changes *only* in reaction to these events
    type Msg =
      | InitialListLoaded of MeetingRoom list
      | FetchFailure of string*exn
      | FetchMeetingRoomSuccess of MeetingRoom
      | MeetingRoomNameUpdated of string
      | MeetingRoomCodeUpdated of string
      | MeetingRoomClicked
      | SaveMeetingRoom
      | SaveNewMeetingRoom
      | DeleteMeetingRoom of int
      | NewMeetingRoom
      | LoadMeetingRooms
      | FetchMeetingRooms
      | MeetingRoomsFetched of MeetingRoom list
      | LoadReservations
      | ReservationsLoaded of Reservation list
      | DeleteReservation of int
      | FetchReservationSuccess of Reservation
      | LoadUsers
      | UsersLoaded of User list
      | DeleteUser of int
      | FetchUserSuccess of User
      | MeetingRoomUpdated of MeetingRoom
      | UserUpdated of User
      | ShowListUsers
      | UsersClicked
      | SaveReservation
      | SaveNewReservation
      | UserNameUpdated of string
      | EmailUpdated of string
      | SurnameUpdated of string
      | SaveUser
      | SaveNewUser
      | FromUpdated of DatePicker.Types.State * (DateTime option)
      | ToUpdated of DatePicker.Types.State * (DateTime option)
