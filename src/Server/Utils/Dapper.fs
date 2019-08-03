namespace Utils

open Dapper
open System.Data.SqlClient
open System
open System.Dynamic
open System.Collections.Generic

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

    let dapperQuery<'Result>  (connection:SqlConnection) (query:string) =
        connection.Query<'Result>(query)

    let dapperParametrizedQuery<'Result>  (connection:SqlConnection) (query:string) (param:obj) : 'Result seq =
        connection.Query<'Result>(query, param)

    let dapperParametrizedQueryFirstOrDefault<'Result>  (connection:SqlConnection) (query:string) (param:obj) : 'Result =
        connection.QueryFirstOrDefault<'Result>(query, param)
    // let dapperMapParametrizedQuery<'Result> (query:string) (connection:SqlConnection) (param : Map<string,_>)  : 'Result seq =
    //     let expando = ExpandoObject()
    //     let expandoDictionary = expando :> IDictionary<string,obj>
    //     for paramValue in param do
    //         expandoDictionary.Add(paramValue.Key, paramValue.Value :> obj)

    //     connection |> dapperParametrizedQuery query expando