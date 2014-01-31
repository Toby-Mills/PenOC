Public Class Log
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents grdLog As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmbLog As System.Web.UI.WebControls.DropDownList
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

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            PopulateControls()
            LoadLogResultsTable()

        End If
    End Sub

    Private Sub PopulateControls()
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbLog, LookupManager.enumLookupTable.Log, True)
    End Sub

    Private Sub LoadLogResultsTable()
        Dim dtLog As DataTable

        dtLog = GetLogResultsTable(c_conDB, cmbLog.SelectedValue)
        dtLog.DefaultView.Sort = "Total DESC"
        Me.grdLog.DataSource = dtLog
        Me.grdLog.DataBind()
    End Sub

    Private Sub cmbLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLog.SelectedIndexChanged
        LoadLogResultsTable()
    End Sub
End Class
