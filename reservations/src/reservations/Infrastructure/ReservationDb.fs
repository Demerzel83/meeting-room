namespace Reservations.Infrastructure

open Dapper

open Reservations.Utils.Dapper
open Reservations.Core.Types
open System.Data
open System
open Newtonsoft.Json
open Reservations.Utils.Json

module ReservationDb =

    type ReservationDto =
      { Id : int
        From : DateTime
        To : DateTime
        MeetingRoom : string
        User : string }

    type MapFn = ReservationDto -> Reservation
    let mapReservation: MapFn =
        fun r ->
            {
                Id = r.Id
                From = r.From
                To = r.To
                MeetingRoom = JsonConvert.DeserializeObject<MeetingRoom>(r.MeetingRoom, new OptionConverter() )
                User = JsonConvert.DeserializeObject<User>(r.User, new OptionConverter())
            }

    let ReservationSql = "
                SELECT
                    Id,
                    [From],
                    [to],
                    (Select Id, Name, Code From dbo.MeetingRooms where id = MeetingRoomId for Json auto, Without_Array_Wrapper) as 'MeetingRoom',
                    (Select Id, Email, Name, Surname From dbo.Users where id = UserId for Json auto, Without_Array_Wrapper) as 'User'
                FROM dbo.Reservations"


    let getAll (connection:IDbConnection) =
            dapperQuery<ReservationDto> connection ReservationSql
            |> List.ofSeq
            |> List.map mapReservation

    let get (id:int) (connection:IDbConnection)  =
            let dp = DynamicParameters()
            dp.Add("Id", id)

            let mr =
                dapperParametrizedQuery<ReservationDto> connection (ReservationSql + " WHERE Id = @Id") dp

                |> List.ofSeq
                |> List.map mapReservation

            match mr with
            | [mro] -> Some mro
            | _ -> None


    let insert  (reservation:Reservation) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("MeetingRoomId", reservation.MeetingRoom.Id)
        dp.Add("UserId", reservation.User.Id)
        dp.Add("From", reservation.From)
        dp.Add("To", reservation.To)

        connection.Execute("
            INSERT INTO [dbo].[Reservations]
               ([MeetingRoomId]
               ,[UserId]
               ,[From]
               ,[To])
         VALUES (@MeetingRoomId,@UserId,@From,@To)", dp)


    let update (reservation:Reservation) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("MeetingRoomId", reservation.MeetingRoom.Id)
        dp.Add("UserId", reservation.User.Id)
        dp.Add("From", reservation.From)
        dp.Add("To", reservation.To)
        dp.Add("Id", reservation.Id)

        connection.Execute("
            UPDATE [dbo].[Reservations]
             SET [MeetingRoomId] = @MeetingRoomId
               ,[UserId] = @UserId
               ,[From] = @From
               ,[To] = @To
             WHERE [Id] = @Id", dp)


    let delete (id:int)  (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("Id", id)

        connection.Execute("
            DELETE FROM [dbo].[Reservations]
             WHERE [Id] = @Id", dp)
