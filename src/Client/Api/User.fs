namespace UI.Api

open Thoth.Fetch
open Thoth.Json

open  MeetingRoom.Shared


module User =

    let getAll ()
        = Fetch.fetchAs<User list> "/api/users"

    let get (id:string)
        = Fetch.fetchAs<User> ("api/users/" + id)

    let encode (user:User)
        = Encode.object
            [
                "Id", Encode.int user.Id
                "Name", Encode.option Encode.string user.Name
                "Surname", Encode.option Encode.string user.Surname
                "Email", Encode.string user.Email
            ]

    let update (user:User)
        = Fetch.put ("api/users", (encode user), Decode.int)

    let create (user:User)
        = Fetch.post ("api/users/new", (encode user), Decode.int)

    let delete (id:int) =
        let url = sprintf "api/users/%d" id
        Fetch.delete(url, null, Decode.int)


