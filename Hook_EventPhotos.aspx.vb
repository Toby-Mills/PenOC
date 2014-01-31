Partial Class Hook_EventPhotos
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

    Public Const ARG_EVENT_ID = "eventID"
    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intEvent As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDb, Page)
            If Page.Request.Item(ARG_EVENT_ID) > "" Then
                intEvent = CInt(Page.Request.Item(ARG_EVENT_ID))
                Page.Session.Item(SESSION_PHOTO_EVENT_ID) = intEvent
                Server.Transfer("PhotoViewer.htm")
            End If
        End If
    End Sub

End Class
