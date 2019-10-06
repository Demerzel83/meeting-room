namespace UI.Api

open Thoth.Fetch

open  MeetingRoom.Shared

module User =

    let getAllUsers ()
        = Fetch.fetchAs<User list> "/api/users"




