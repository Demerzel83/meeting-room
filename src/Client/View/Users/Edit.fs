namespace UI.User

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module Edit =
    let editForm (model:Model)  (dispatch:Msg -> unit) =
        let user = model.User
        form [ ]
             [
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Email" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value user.Email;
                            Input.OnChange (fun event -> dispatch (EmailUpdated event.Value) ) ] ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Name" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  user.Name);
                            Input.OnChange (fun event -> dispatch (UserNameUpdated event.Value)) ] ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Surname" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  user.Surname);
                            Input.OnChange (fun event -> dispatch (SurnameUpdated event.Value)) ] ] ]
               Field.div [ Field.IsGrouped ]
                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveUser) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadUsers) ]
                        [ str "Cancel" ] ] ]
        ]