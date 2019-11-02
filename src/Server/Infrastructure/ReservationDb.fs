namespace Reservation.Infrastructure

open Dapper

open MeetingRoom.Utils.Dapper
open MeetingRoom.Shared
open System.Data

module ReservationDb =

    let getAll (connection:IDbConnection) =
            dapperQuery<Reservation> connection "SELECT Id, MeetingRoomId, UserId, [From], [to] FROM dbo.Reservations"
            |> List.ofSeq


    let get (id:int) (connection:IDbConnection)  =
            let dp = DynamicParameters()
            dp.Add("Id", id)

            let mr =
                dapperParametrizedQuery<Reservation> connection "SELECT Id, MeetingRoomId, UserId, [From], [to] FROM dbo.Reservations WHERE Id = @Id" dp
                |> List.ofSeq

            match mr with
            | [mro] -> Some mro
            | _ -> None


    let insert  (reservation:Reservation) (connection:IDbConnection) =
        let dp = DynamicParameters()
        dp.Add("MeetingRoomId", reservation.MeetingRoomId)
        dp.Add("UserId", reservation.UserId)
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
        dp.Add("MeetingRoomId", reservation.MeetingRoomId)
        dp.Add("UserId", reservation.UserId)
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
