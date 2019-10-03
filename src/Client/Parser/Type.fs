namespace UI.Parser

module Type =
    [<RequireQualifiedAccess>]
    type Page =
      | New
      | List
      | MeetingRoom of int