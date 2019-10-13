namespace UI.Messages

open System
open MeetingRoom.Shared

module Type =
    // The Msg type defines what events/actions can occur while the application is running
    // the state of the application changes *only* in reaction to these events
    type Msg =
      | InitialListLoaded of MeetingRoom list
      | FetchFailure of string*exn
      | FetchMeetingRoomSuccess of MeetingRoom option
      | NameUpdated of string
      | CodeUpdated of string
      | NewNameUpdated of string
      | NewCodeUpdated of string
      | SaveMeetingRoom
      | SaveNewMeetingRoom
      | DeleteMeetingRoom of int
      | NewMeetingRoom
      | LoadMeetingRooms
      | LoadReservations
      | ReservationsLoaded of Reservation list
      | DeleteReservation of int
      | LoadUsers
      | UsersLoaded of User list
      | DeleteUser of int