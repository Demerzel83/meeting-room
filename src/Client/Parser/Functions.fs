namespace UI.Parser

open Elmish
open Elmish.UrlParser

open UI.Model
open UI.Messages.Type
open UI.Parser.Type
open UI.Api

module Functions =
    /// The URL is turned into a Page option.
    let pageParser : Parser<Page -> Page,_>  =
        oneOf
            [
              map Page.New (s "new")
              map Page.List (s "list")
              map Page.MeetingRoom (s "meetingroom" </> str)]

    let urlParser location =
        parseHash pageParser location

    let urlUpdate (result:Page option) model =
      match result with
      | Some (Page.MeetingRoom id) ->
          { model with Page = (Page.MeetingRoom id) }, Cmd.OfPromise.either getMeetingRoom (id.ToString()) FetchSuccess (fun ex -> FetchFailure (id,ex))
      | Some page -> { model with Page = page }, []
      | None -> { model with Page = Page.List }, []