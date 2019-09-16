namespace MeetingRoom.Utils

open Reader
open System.Data

module Sql =
    type SqlAction<'a> = Reader<IDbConnection, 'a>

    /// Create an SqlConnection and run the action on it
    /// SqlAction<'a> -> 'a
    let execute (connection:IDbConnection) (action:SqlAction<'a>) =
        connection.Open()
        let result = run connection action
        connection.Close()
        result