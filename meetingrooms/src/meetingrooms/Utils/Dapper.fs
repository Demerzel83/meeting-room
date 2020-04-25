namespace MeetingRooms.Utils

open Dapper
open System
open System.Data

module Dapper =
    type OptionHandler<'T>() =
        inherit SqlMapper.TypeHandler<option<'T>>()

        override __.SetValue(param, value) =
            let valueOrNull =
                match value with
                | Some x -> box x
                | None -> null

            param.Value <- valueOrNull

        override __.Parse value =
            if isNull value || value = box DBNull.Value
            then None
            else Some (value :?> 'T)

    let dapperQuery<'Result>  (connection:IDbConnection) (query:string) =
        Dapper.SqlMapper.AddTypeHandler (OptionHandler<string>())
        connection.Query<'Result>(query)

    let dapperParametrizedQuery<'Result>  (connection:IDbConnection) (query:string) (param:obj) : 'Result seq =
        Dapper.SqlMapper.AddTypeHandler (OptionHandler<string>())
        connection.Query<'Result>(query, param)

    let dapperParametrizedQueryFirstOrDefault<'Result>  (connection:IDbConnection) (query:string) (param:obj) : 'Result =
        connection.QueryFirstOrDefault<'Result>(query, param)