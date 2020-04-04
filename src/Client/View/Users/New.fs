namespace UI.User

open Fable.React
open Fulma

open UI.Model
open UI.Messages.Type

module New =
    let newForm (model:Model)  (dispatch:UserMessages -> unit) =
        form [ ]
             [ // Name field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "email" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value model.User.Email;
                            Input.OnChange (fun event -> dispatch (EmailUpdated event.Value) ) ] ] ]
               // Username field
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Name" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  model.User.Name);
                            Input.OnChange (fun event -> dispatch (UserNameUpdated event.Value)) ] ] ]
               Field.div [ ]
                    [ Label.label [ ]
                        [ str "Surname" ]
                      Control.div [ ]
                        [ Input.text [
                            Input.Value (Option.defaultValue ""  model.User.Surname);
                            Input.OnChange (fun event -> dispatch (SurnameUpdated event.Value)) ] ] ]

               Field.div [ Field.IsGrouped ]

                [ Control.div [ ]
                    [ Button.button [ Button.Color IsPrimary; Button.OnClick (fun _ -> dispatch SaveNewUser) ]
                        [ str "Save" ] ]
                  Control.div [ ]
                    [ Button.button [ Button.IsLink; Button.OnClick (fun _ -> dispatch LoadUsers) ]
                        [ str "Cancel" ] ] ]
        ]