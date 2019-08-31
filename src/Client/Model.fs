namespace UI

open Shared
open UI.Parser.Url

module Model =
    type Model =
        {   Page: Page
            MeetingRooms: MeetingRoom list
            Loading: bool
            MeetingRoomId: string option
            MeetingRoom:  MeetingRoom option
            NewMeetingRoom: MeetingRoom }