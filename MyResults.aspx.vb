Partial Class MyResults
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
	Protected WithEvents ResultListMyResults As ResultList
    Protected WithEvents CompetitorSelect As CompetitorSelect

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtCompetitor As DataTable

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With ResultListMyResults
                .AllowPaging = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseClimb) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultComment) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultCompetitor) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseControls) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultDisqualified) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLength) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseTechnical) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLog) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultCategory) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultPoints) = False
            End With
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub LoadResults()
        Dim intCompetitor As Integer
        Dim dtResults As DataTable

        intCompetitor = Me.CompetitorSelect.Selected
		dtResults = PenOCDB.GetTable_Result(c_conDB, PenOCDB.WhereResult_idCompetitor(intCompetitor))
        dtResults.DefaultView.Sort = "dteDate DESC"
        ResultListMyResults.ResultTable = dtResults
    End Sub

    Private Sub CompetitorSelect_SelectionChanged() Handles CompetitorSelect.SelectionChanged
        LoadResults()
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If Not Page.IsPostBack Then
            LoadResults()
        End If
    End Sub

    Private Sub ResultListMyResults_EventSelected() Handles ResultListMyResults.EventSelected
		Server.Transfer("EventResults.aspx?idevent=" & Me.ResultListMyResults.SelectedEvent)
    End Sub
End Class
