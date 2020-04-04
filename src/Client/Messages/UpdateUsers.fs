namespace UI.Messages

open Elmish
open Elmish.Navigation

open MeetingRoom.Shared
open UI.Model
open UI.Api
open UI.Parser.Type

module UpdateUsers =
    let always arg = fun _ -> arg

    let loadUsers =
        Cmd.OfPromise.perform User.getAll () UsersLoaded

    let updateUser user =
        Cmd.OfPromise.perform User.update user (always LoadUsers)

    let createUser user =
        Cmd.OfPromise.perform User.create user (always LoadUsers)

    let deleteUser id =
        // Cmd.OfPromise.either User.delete id (always LoadUsers) (fun error -> FetchFailure ("Error", error))
        Cmd.OfPromise.perform User.delete id (always LoadUsers)

    let loadAllUsers model =
        let modelLoading = { model  with LoadingPage = true }
        modelLoading, loadUsers

    let update (msg : UserMessages) (currentModel : Model) : Model * Cmd<UserMessages> =
        match msg with
        | LoadUsers ->
            loadAllUsers currentModel
        | UsersLoaded users ->
            let modelWithUsers = { currentModel with Users = users; LoadingPage = false }
            modelWithUsers, Cmd.none
        | SaveUser ->
            let newModel = { currentModel with Page = Page.UserList; LoadingPage = true}
            newModel,  seq { updateUser currentModel.User; Navigation.newUrl "#/userList" } |> Cmd.batch
        | SaveNewUser ->
            currentModel, seq { createUser currentModel.User; Navigation.newUrl "#/userList" } |> Cmd.batch
        | DeleteUser id ->
            currentModel, (deleteUser id)
        | UserNameUpdated userName ->
            let updatedUser = { currentModel.User  with Name = Some userName }
            { currentModel with User = updatedUser }, Cmd.none
        | EmailUpdated email ->
            let updatedUser = { currentModel.User  with Email = email }
            { currentModel with User = updatedUser }, Cmd.none
        | SurnameUpdated surname ->
            let updatedUser = { currentModel.User  with Surname = Some surname }
            { currentModel with User = updatedUser }, Cmd.none
        | FetchUserSuccess user ->
            { currentModel with User = user }, Cmd.none
        | UsersClicked ->
            if currentModel.Users.Length = 0 then
                { currentModel with ShowListUsers = false }, loadUsers
            else
                { currentModel with ShowListUsers = true }, Cmd.none
