namespace User.Infrastructure

open System

open UserDb
open MeetingRoom.Shared
open FSharpPlus.Data
open System.Data

module UserReader =

    let getAllUsers (): Reader<IDbConnection, User list> =
        Reader getAllUsers

    // let getMeetingRoom id : Reader<IDbConnection, MeetingRoom option> =
    //     Reader
    //      (getMeetingRoom id)

    // let insertMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int> =
    //     Reader (insertMeetingRoom meetingRoom)

    // let updateMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int>  =
    //     Reader (updateMeetingRoom meetingRoom)

    // let deleteMeetingRoom  (id:int): Reader<IDbConnection, int>  =
    //     Reader (deleteMeetingRoom id)
