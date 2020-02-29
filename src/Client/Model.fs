namespace UI

open System
open Fulma.Elmish

open  MeetingRoom.Shared
open UI.Parser.Type

module Model =
    type Model =
        {   Page : Page

            Users : User list
            UserId : string option
            User:  User
            ShowListUsers : bool

            Reservations : Reservation list
            ReservationId : string option
            Reservation :  Reservation
            ShowListMeetingRooms : bool

            Loading : bool
            LoadingData : bool

            MeetingRooms : MeetingRoom list
            MeetingRoomId : string option
            MeetingRoom :  MeetingRoom

            DatePickerFromState : DatePicker.Types.State
            DatePickerToState : DatePicker.Types.State }

    let getDefaultStatus ():Model =
        {   Page = Page.MeetingRoomList ;
            MeetingRooms = [] ;
            Users = [] ;
            Reservations = [] ;
            Loading = true ;
            LoadingData = false;
            MeetingRoomId = None ;
            ShowListMeetingRooms = false;
            ShowListUsers = false;
            MeetingRoom = {
                Id = 0 ;
                Name = "" ;
                Code = None };
            UserId = None;
            ReservationId = None;
            User = {
                Id = 0;
                Name = None;
                Surname = None;
                Email = "" };
            Reservation = {
                Id = 0;
                MeetingRoom = {
                    Id = 0;
                    Name = "";
                    Code = None;
                    };
                User = {
                    Id = 0;
                    Email = "";
                    Name = None;
                    Surname = None
                };
                From = DateTime.Now;
                To = DateTime.Now }
            DatePickerFromState = DatePicker.Types.defaultState;
            DatePickerToState = DatePicker.Types.defaultState
        }