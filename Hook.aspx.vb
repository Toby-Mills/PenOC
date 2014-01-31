Public Partial Class Hook
    Inherits System.Web.UI.Page

    Public Const URL_HOOK_TYPE As String = "HookType"
    Public Const URL_HOOK_ID As String = "HookID"

    Public Const URL_HOOK_TYPE_EVENT_NOTICE As String = "EventNotice"
    Public Const URL_HOOK_TYPE_EVENT_RESULTS As String = "EventResults"
    Public Const URL_HOOK_TYPE_EVENT_PHOTOS As String = "EventPhotos"
    Public Const URL_HOOK_TYPE_LOG As String = "Log"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strHookType As String
        Dim strHookID As String

        strHookType = Request.Item(URL_HOOK_TYPE)
        strHookID = Request.Item(URL_HOOK_ID)

        Select Case strHookType
            Case URL_HOOK_TYPE_LOG
                Session.Item(S_HOOK_TYPE) = enum_HookType.Log
                Session.Item(S_HOOK_ID) = CInt(strHookID)
                Response.Redirect("penoc.htm")
            Case URL_HOOK_TYPE_EVENT_NOTICE
                Session.Item(S_HOOK_TYPE) = enum_HookType.eventnotice
                Session.Item(S_HOOK_ID) = CInt(strHookID)
                Response.Redirect("penoc.htm")
            Case URL_HOOK_TYPE_EVENT_RESULTS
                Session.Item(S_HOOK_TYPE) = enum_HookType.EventResults
                Session.Item(S_HOOK_ID) = CInt(strHookID)
                Response.Redirect("penoc.htm")
            Case URL_HOOK_TYPE_EVENT_PHOTOS
                Session.Item(S_HOOK_TYPE) = enum_HookType.EventPhotos
                Session.Item(S_HOOK_ID) = CInt(strHookID)
                Response.Redirect("penoc.htm")
        End Select

    End Sub

End Class