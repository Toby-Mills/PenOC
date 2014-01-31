Partial Class EventsFuture
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
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
	Protected WithEvents EventListFuture As EventList

    Dim c_conDB As OleDb.OleDbConnection
    Dim c_dtEvent As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strWHERE As String

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            strWHERE = PenOCDB.WhereEvent_Future
            c_dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, "")
            c_dtEvent.DefaultView.Sort = "dteDate ASC"
            With EventListFuture
                .AllowPaging = False
                .EventTable = c_dtEvent
                .DisplayColumn(EventList.enumEventListColumn.Photos) = False
                .Selectable = True
            End With
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub EventListFuture_EventSelected() Handles EventListFuture.EventSelected

		Server.Transfer("EventDetails.aspx?idevent=" & EventListFuture.SelectedEvent)

    End Sub
End Class
