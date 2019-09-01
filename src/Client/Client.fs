module Client

open Elmish
open Elmish.Navigation

open Elmish.React
open Elmish.Debug

open UI.Parser.Functions
open UI.View
open UI.Messages.Update

Program.mkProgram init update view
|> Program.toNavigable urlParser urlUpdate
|> Program.withConsoleTrace
|> Program.withReactBatched "elmish-app"
|> Program.withDebugger
|> Program.run
