namespace UI

open Shared

module Messages =
    // The Msg type defines what events/actions can occur while the application is running
    // the state of the application changes *only* in reaction to these events
    type Msg =
      | InitialListLoaded of MeetingRoom list
      | FetchFailure of string*exn
      | FetchSuccess of MeetingRoom option