namespace MeetingRoom.Infrastructure

open System

open MeetingRoom.Utils.Sql
open MeetingRoomDb
open MeetingRoom.Shared

module MeetingRoomReader =

    let getAllMeetingRooms () =
        SqlAction.Reader getAllMeetingRooms

    let getMeetingRoom id =
        SqlAction.Reader (getMeetingRoom id)

    let insertMeetingRoom (meetingRoom:MeetingRoom) =
        SqlAction.Reader (insertMeetingRoom meetingRoom)

    let updateMeetingRoom (meetingRoom:MeetingRoom) =
        SqlAction.Reader (updateMeetingRoom meetingRoom)

    let deleteMeetingRoom  (id:Guid) =
        SqlAction.Reader (deleteMeetingRoom id)
