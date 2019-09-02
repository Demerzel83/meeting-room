namespace Shared

#if FABLE_COMPILER
open Thoth.Json
#else
open Thoth.Json.Net
#endif

open System

type MeetingRoom =
      { Id : Guid
        Name: string
        Code: string option }
        // static member Decoder : Decode.Decoder<MeetingRoom> =
        //     Decode.object
        //         (fun get ->
        //             { Id = get.Required.Field "id" Decode.guid
        //                 Name = get.Required.Field "name" Decode.string
        //                 Code = get.Optional.Field "code" Decode.string }
        //         )