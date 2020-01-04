namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared


module User =

    let getAll ()
        = Fetch.fetchAs<User list> "/api/users"

    let get (id:string)
        = Fetch.fetchAs<User> ("api/users/" + id)

    let update (user:User)
        = Fetch.put ("api/users", (Enconding.encodeUser user), Decode.int)

    let create (user:User)
        = Fetch.post ("api/users/new", (Enconding.encodeUser user), Decode.int)

    let delete (id:int) =
        let url = sprintf "api/users/%d" id
        Fetch.delete(url, null, Decode.int)


