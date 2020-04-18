namespace UI.Messages

open System
open Fulma.Elmish

open MeetingRoom.Shared

module Type =
    type Msg<'T> = Msg of 'T

    type SystemMessages =
          | FetchFailure of string*exn
          | ClearError

    type UserMessages =
          | LoadUsers
          | UsersLoaded of User list
          | DeleteUser of int
          | FetchUserSuccess of User
          | ShowListUsers
          | UserNameUpdated of string
          | EmailUpdated of string
          | SurnameUpdated of string
          | SaveUser
          | SaveNewUser

    type  ReservationsMessages =
          | LoadReservations
          | ReservationsLoaded of Reservation list
          | DeleteReservation of int
          | FetchReservationSuccess of Reservation
          | SaveReservation
          | SaveNewReservation
          | FromUpdated of DatePicker.Types.State * (DateTime option)
          | ToUpdated of DatePicker.Types.State * (DateTime option)
          | UserUpdated of User
          | MeetingRoomUpdated of MeetingRoom
          | UsersClicked
          | MeetingRoomClicked

    type MeetingRoomMessages =
          | InitialListLoaded of MeetingRoom list
          | FetchMeetingRoomSuccess of MeetingRoom
          | MeetingRoomNameUpdated of string
          | MeetingRoomCodeUpdated of string
          | SaveMeetingRoom
          | SaveNewMeetingRoom
          | DeleteMeetingRoom of int
          | NewMeetingRoom
          | LoadMeetingRooms
          | FetchMeetingRooms
          | MeetingRoomsFetched of MeetingRoom list
