Partial Class AdminCompetitors
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
	Protected WithEvents CompetitorSearch As CompetitorSearch

	Private c_condb As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		DefaultButton(Page, "CompetitorSearch:cmdSearch")
		WebFocusControl(Page, "CompetitorSearch:txtName")

		If Session.Item("style") > "" Then
			Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
		Else
			Me.lnkStylesheet.Attributes.Add("href", "styles.css")
		End If

        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            With Me.CompetitorSearch
                .Selectable = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Competitor) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Delete) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Position) = False
                .AutoSearch = True
            End With
        End If
        ShowHourGlass(Page)
	End Sub

	Private Sub CompetitorSearch_CompetitorSelected() Handles CompetitorSearch.CompetitorSelected
		Dim intCompetitor As Integer

		intCompetitor = Me.CompetitorSearch.SelectedCompetitor
		Session.Item(CompetitorEdit.RETURN_URL) = "AdminCompetitors.aspx"
		Server.Transfer("CompetitorEdit.aspx?idcompetitor=" & intCompetitor)

	End Sub

	Private Sub cmdNewCompetitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewCompetitor.Click
		Server.Transfer("CompetitorEdit.aspx")
	End Sub

	Private Sub CompetitorSearch_CompetitorDeleted() Handles CompetitorSearch.CompetitorDeleted
		Dim strPath As String

		strPath = "ConfirmDelete.aspx?" & ConfirmDelete.OBJECT_ID & "=" & Me.CompetitorSearch.SelectedCompetitor & "&" & ConfirmDelete.DELETE_TYPE & "=" & ConfirmDelete.enumDeleteType.Competitor
		OpenPopUp(Page, strPath, "Delete Competitor", 250, 150)
	End Sub
End Class
