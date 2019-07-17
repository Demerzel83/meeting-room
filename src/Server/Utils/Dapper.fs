namespace Utils

open Dapper
open System.Data.SqlClient

module Dapper =
    let dapperQuery<'Result>  (connection:SqlConnection) (query:string) =
        connection.Query<'Result>(query)
