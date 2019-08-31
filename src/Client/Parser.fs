namespace UI.Parser

open Elmish.UrlParser

module Url =
    type Page =
      | List
      | MeetingRoom of string

    /// The URL is turned into a Page option.
    let pageParser : Parser<Page -> Page,_>  =
        oneOf
            [ map List (s "list")
              map MeetingRoom (s "meeting" </> str)]