namespace Users.Infrastructure

open UserDb
open Users.Core.Types
open FSharpPlus.Data
open System.Data

module UserReader =

    let getAll (): Reader<IDbConnection, User list> =
        Reader getAll

    let get id : Reader<IDbConnection, User option> =
        Reader
         (get id)

    let insert (user:User): Reader<IDbConnection, int> =
        Reader (insert user)

    let update (user:User): Reader<IDbConnection, int>  =
        Reader (update user)

    let delete  (id:int): Reader<IDbConnection, int>  =
        Reader (delete id)
