Public Class EventsRecent_Small
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Protected WithEvents EventListRecent As EventList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strWHERE As String
        Dim dtEvent As DataTable

        If Not Page.IsPostBack Then
            strWHERE = PenOCDB.EventsWhere_PastMonths(2)
            dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE)
            With EventListRecent
                .DisplayColumn(EventList.enumEventListColumn.Close) = False
                .DisplayColumn(EventList.enumEventListColumn.Registration) = False
                .DisplayColumn(EventList.enumEventListColumn.Starts) = False
                .DisplayColumn(EventList.enumEventListColumn.Photos) = False
                .Selectable = True
                .AllowPaging = False
                .DisplayEventCount = False
                .EventTable = dtEvent
            End With
        End If
    End Sub

    Private Sub EventListRecent_EventSelected() Handles EventListRecent.EventSelected

        modFunctions_WebForms.RedirectFrameParent(Page, "EventResults.aspx?idevent=" & EventListRecent.SelectedEvent)

    End Sub
End Class
