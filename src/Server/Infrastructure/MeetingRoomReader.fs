namespace MeetingRoom.Infrastructure

open System

open MeetingRoomDb
open MeetingRoom.Shared
open FSharpPlus.Data
open System.Data

module MeetingRoomReader =

    let getAllMeetingRooms (): Reader<IDbConnection, MeetingRoom list> =
        Reader getAllMeetingRooms

    let getMeetingRoom id : Reader<IDbConnection, MeetingRoom option> =
        Reader
         (getMeetingRoom id)

    let insertMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int> =
        Reader (insertMeetingRoom meetingRoom)

    let updateMeetingRoom (meetingRoom:MeetingRoom): Reader<IDbConnection, int>  =
        Reader (updateMeetingRoom meetingRoom)

    let deleteMeetingRoom  (id:int): Reader<IDbConnection, int>  =
        Reader (deleteMeetingRoom id)
