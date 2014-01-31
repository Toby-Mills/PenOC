Partial Class AdminCommittee
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
	Protected WithEvents CompetitorListCommittee As CompetitorList

	Private c_conDB As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If

            Me.txtPersonID.Width = New Web.UI.WebControls.Unit(0)
            CompetitorSearchPopup.ShowCompetitorSearch(Me.cmdCompetitorSelect, Me.txtPersonID, Me.txtPersonName)

            With CompetitorListCommittee
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Delete) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.BirthDate) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Category) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Gender) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FirstName) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Surname) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Competitor) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FullName) = True
                LoadCommittee()
            End With
        End If
        ShowHourGlass(Page)
	End Sub

	Private Sub LoadCommittee()
		Dim strWHERE As String
		Dim dtCommittee As DataTable

		strWHERE = WhereCompetitor_Committee()
		dtCommittee = GetTable_Competitor(c_conDB, strWHERE)
		Me.CompetitorListCommittee.CompetitorsTable = dtCommittee

	End Sub

	Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
		Dim intCompetitor As Integer
		Dim strPosition As String
		Dim strMessage As String

		intCompetitor = Me.txtPersonID.Text		' CompetitorSelectCommittee.Selected
		strPosition = Me.txtPosition.Text

		If intCompetitor = 0 Then
			WebMsgBox(Page, "Error", "Please select a person to add")
		Else
			AddCommitteeMemeber(c_conDB, intCompetitor, strPosition)
			LoadCommittee()
		End If


	End Sub

	Private Sub CompetitorListCommittee_CompetitorDeleted() Handles CompetitorListCommittee.CompetitorDeleted
		Dim intCompetitor As Integer

		intCompetitor = CompetitorListCommittee.SelectedCompetitor

		RemoveCommitteeMember(c_conDB, intCompetitor)
		LoadCommittee()

	End Sub

	'Private Sub CompetitorSelectCommittee_EditCompetitor()

	'	OpenPopUp(Page, "CompetitorEdit.aspx?idCompetitor=" & CompetitorSelectCommittee.Selected & "&autoclose=true", "Edit")

	'End Sub
End Class
