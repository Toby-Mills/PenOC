Partial Class Contact
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdSubscribeAddress As System.Web.UI.WebControls.Button

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
            PopulateControls()
            With Me.CompetitorListCommittee
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.BirthDate) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Category) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Gender) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FirstName) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Surname) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Competitor) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FullName) = True
                .AllowPaging = False
            End With
            PopulateCommittee()
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub PopulateControls()
        Me.cmdEmailEnquiry.Text = g_strEmailInfo.Replace("@", " @ ")
        Me.cmdEmailWebsite.Text = g_strEmailWebsite.Replace("@", " @ ")
    End Sub

    Private Sub cmdEmailEnquiry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEmailEnquiry.Click
		OpenPopUp(Page, "mailto:" & g_strEmailInfo & "?subject=PenOC Website Enquiry", "Email")
    End Sub

    Private Sub PopulateCommittee()
        Dim dtCommittee As DataTable
        Dim strWHERE As String

		strWHERE = PenOCDB.WhereCompetitor_Committee
        dtCommittee = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)
        Me.CompetitorListCommittee.CompetitorsTable = dtCommittee

    End Sub

    Private Sub cmdWebsiteEnquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenPopUp(Page, "mailto:" & g_strEmailWebsite & "?subject=PenOC Website Enquiry", "Email")
    End Sub

	Private Sub cmdEmailWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEmailWebsite.Click
		OpenPopUp(Page, "mailto:" & g_strEmailWebsite & "?subject=PenOC Website Enquiry", "Email")
	End Sub

	Private Sub CompetitorListCommittee_CompetitorEmailed() Handles CompetitorListCommittee.CompetitorEmailed
		EmailCompetitor(Page, c_conDB, CompetitorListCommittee.SelectedCompetitor, "PenOC Website Enquiry")
	End Sub
End Class
