Partial Class LogResults
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
    Private c_conDb As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intLog As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            intLog = Request.Item("idlog")
            If Not intLog > 0 Then
                intLog = CurrentLog(c_conDb)
            End If
            LoadLog(intLog)
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub LoadLog(ByVal intLog As Integer)
        Dim dtLog As DataTable
        Dim drLog As DataRow
        Dim dtLogResults As DataTable

        dtLog = GetTable_Log(c_conDb, WhereLog_LogID(intLog))
        If dtLog.Rows.Count > 0 Then
            drLog = dtLog.Rows(0)
            Me.lblLogDescription.Text = drLog.Item("Description")
            Me.lblTotalEvents.Text = drLog.Item("Events")
            Me.lblDisregardWorst.Text = drLog.Item("intDisregardWorst")

            dtLogResults = GetTable_LogResult(c_conDb, intLog)
            dtLogResults.DefaultView.Sort = "Total DESC"
            dtLogResults.PrimaryKey = Nothing
            dtLogResults.Columns.Remove("idCompetitor")
            Me.grdLog.DataSource = dtLogResults
            Me.grdLog.DataBind()
            Me.pnlLogResult.Visible = True
            Me.pnlError.Visible = False

            Me.lblLogPermalink.Text = "http://www.penoc.org.za/Hook.aspx?" & Hook.URL_HOOK_TYPE & "=" & Hook.URL_HOOK_TYPE_LOG & "&" & Hook.URL_HOOK_ID & "=" & intLog
        Else
            Me.lblErrorMessage.Text = "Selected Log was not found"
            Me.pnlLogResult.Visible = False
            Me.pnlError.Visible = True
        End If

    End Sub

	Private Sub grdLog_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdLog.ItemDataBound
		Dim intCol As Integer

		e.Item.Cells(1).Font.Bold = True
		e.Item.Cells(3).Font.Bold = True
		For intCol = 4 To e.Item.Cells.Count - 1
			If e.Item.Cells(intCol).Text = "0" Then
				e.Item.Cells(intCol).Text = ""
			End If
		Next
	End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

    End Sub
End Class
