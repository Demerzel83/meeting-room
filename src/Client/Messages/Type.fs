namespace UI.Messages

open System
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
      | SaveMeetingRoom
      | SaveNewMeetingRoom
      | DeleteMeetingRoom of int
      | NewMeetingRoom
      | LoadMeetingRooms
      | LoadReservations
      | ReservationsLoaded of Reservation list
      | DeleteReservation of int
      | FetchReservationSuccess of Reservation
      | LoadUsers
      | UsersLoaded of User list
      | DeleteUser of int
      | FetchUserSuccess of User
      | MeetingRoomUpdated of string
      | UserUpdated of string
      | FromUpdated of string
      | ToUpdated of string
      | SaveReservation
      | SaveNewReservation
      | UserNameUpdated of string
      | EmailUpdated of string
      | SurnameUpdated of string
      | SaveUser
      | SaveNewUser
