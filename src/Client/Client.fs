module Client

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fetch.Types
open Thoth.Fetch
open Fulma
open Thoth.Json

open Shared

type Model = { MeetingRooms: MeetingRoom list }

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
// | Increment
// | Decrement
| InitialListLoaded of MeetingRoom list

let initialList () = Fetch.fetchAs<MeetingRoom list> "/api/meetingrooms"

let init () : Model * Cmd<Msg> =
    let initialModel = { MeetingRooms = [] }
    let loadCountCmd =
        Cmd.OfPromise.perform initialList () InitialListLoaded
    initialModel, loadCountCmd

let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.MeetingRooms, msg with
    // | Some counter, Increment ->
    //     let nextModel = { currentModel with Counter = Some { Value = counter.Value + 1 } }
    //     nextModel, Cmd.none
    // | Some counter, Decrement ->
    //     let nextModel = { currentModel with Counter = Some { Value = counter.Value - 1 } }
    //     nextModel, Cmd.none
    | _, InitialListLoaded meetingRooms->
        let nextModel = { MeetingRooms = meetingRooms }
        nextModel, Cmd.none
    | _ -> currentModel, Cmd.none


let safeComponents =
    let components =
        span [ ]
           [ a [ ]
               [ str "SAFE  "
                 str Version.template ]
             str ", "
             a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
             str ", "
             a [ Href "http://fable.io" ] [ str "Fable" ]
             str ", "
             a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
             str ", "
             a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]

           ]

    span [ ]
        [ str "Version "
          strong [ ] [ str Version.app ]
          str " powered by: "
          components ]

// let show = function
// | { MeetingRooms = meetingRooms } -> string counter.Value
// | { MeetingRooms = []   } -> "Loading..."

let button txt onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color IsPrimary
          Button.OnClick onClick ]
        [ str txt ]


let showCode code =
    match code with
    | Some c -> str c
    | None -> str "No Code"

let showRows meetingRooms =
    List.map (fun mr -> tr [ ]
                                 [ td [ ] [ mr.Id.ToString() |> str ]
                                   td [ ] [ str mr.Name ]
                                   td [ ] [ (showCode mr.Code) ] ]) meetingRooms

let showList meetingRooms =
  [ Table.table [ Table.IsHoverable ]
                        [ thead [ ]
                            [ tr [ ]
                                [ th [ ] [ str "Id" ]
                                  th [ ] [ str "Name" ]
                                  th [ ] [ str "Code" ] ] ]
                          tbody [ ]
                             (showRows meetingRooms)
                               ] ]


let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "Meeting Room List" ] ] ]

          Container.container []
              [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                  (showList model.MeetingRooms)
              ]
          Footer.footer [ ]
                [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ safeComponents ] ] ]


#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
