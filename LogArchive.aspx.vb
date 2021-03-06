Partial Class LogArchive
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
	Protected WithEvents LogListArchive As LogList

	Private c_conDB As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            Me.LogListArchive.DisplayColumn(LogList.LogListColumn.Year) = False
            Me.LogListArchive.DisplayColumn(LogList.LogListColumn.Name) = False
            Me.LogListArchive.DisplayColumn(LogList.LogListColumn.Current) = False
            RefreshLogGrid()
        End If
        ShowHourGlass(Page)

	End Sub

	Public Sub RefreshLogGrid()
		Dim dtLog As DataTable

		dtLog = GetTable_Log(c_conDB, "")

		Me.LogListArchive.LogTable = dtLog
	End Sub

	Private Sub LogListArchive_Selected() Handles LogListArchive.Selected
		Response.Redirect("LogResults.aspx?idLog=" & Me.LogListArchive.SelectedLog)
	End Sub
End Class
