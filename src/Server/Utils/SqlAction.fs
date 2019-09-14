namespace MeetingRoom.Utils

open System.Data.SqlClient
open Reader

module Sql =
    type SqlAction<'a> = Reader<SqlConnection, 'a>

    /// Create an SqlConnection and run the action on it
    /// SqlAction<'a> -> 'a
    let execute (connection:SqlConnection) (action:SqlAction<'a>) =
        connection.Open()
        let result = run connection action
        connection.Close()
        result