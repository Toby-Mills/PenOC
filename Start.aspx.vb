Partial Class Start
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'This page is used as a starting point to set the session.
        'This ensures that all subsequent pages are part of the correct session 
        ' and therefore function as expected

        Dim intHookType As enum_HookType
        Dim intHookID As Integer

        intHookType = Session.Item(S_HOOK_TYPE)
        Select Case intHookType
            Case enum_HookType.EventNotice
                intHookID = Session.Item(S_HOOK_ID)
                modFunctions_WebForms.RedirectFrame(Page, "main", "EventDetails.aspx?idEvent=" & intHookID)
            Case enum_HookType.EventResults
                intHookID = Session.Item(S_HOOK_ID)
                modFunctions_WebForms.RedirectFrame(Page, "main", "EventResults.aspx?idEvent=" & intHookID)
            Case enum_HookType.Log
                intHookID = Session.Item(S_HOOK_ID)
                modFunctions_WebForms.RedirectFrame(Page, "main", "LogResults.aspx?idLog=" & intHookID)
            Case enum_HookType.EventPhotos
                intHookID = Session.Item(S_HOOK_ID)
                Session.Item(SESSION_PHOTO_EVENT_ID) = intHookID
                modFunctions_WebForms.RedirectFrame(Page, "main", "PhotoViewer.htm")
            Case Else
                modFunctions_WebForms.RedirectFrame(Page, "contents", "menu.aspx", "RedirectMenu")
                modFunctions_WebForms.RedirectFrame(Page, "main", "home.htm", "RedirectMain")
        End Select

    End Sub

End Class
