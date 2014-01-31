Partial Class CompetitorMatch
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
    Protected WithEvents CompetitorListMatch As CompetitorList

    'URL varibales
    Public Const FIRSTNAME = "FirstName"
    Public Const SURNAME = "Surname"
    Public Const RETURNID_CONTROL = "ReturnIDControl"

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strFirstName As String
        Dim strSurname As String
        Dim strWHERE As String
        Dim strORDER As String
        Dim dtCompetitor As DataTable

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)

            strFirstName = Request.Item(FIRSTNAME)
            If strFirstName = "&nbsp;" Then
                strFirstName = ""
            End If

            strSurname = Request.Item(SURNAME)
            If strSurname = "&nbsp;" Then
                strSurname = ""
            End If

            Me.lblCompetitorName.Text = Trim(strFirstName & " " & strSurname)

            strWHERE = WhereCompetitor_NameSoundsLike(strSurname, strFirstName)
            strORDER = OrderCompetitor_NameSoundsLike(strSurname, strFirstName, False)
            dtCompetitor = GetTable_Competitor(c_conDB, strWHERE, strORDER)
            With CompetitorListMatch
                .PageSize = -1
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.BirthDate) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Category) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Competitor) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Email) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Position) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FirstName) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FullName) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Surname) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FullNameLink) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone1) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone2) = False

                .CompetitorsTable = dtCompetitor

            End With

            Me.CompetitorListMatch.Visible = dtCompetitor.Rows.Count > 0
            Me.lblNoMatch.Visible = dtCompetitor.Rows.Count = 0

            txtReturnIDControl.Text = Request.Item(RETURNID_CONTROL)

        End If

        Me.txtReturnIDControl.Width = New Web.UI.WebControls.Unit(0)
        ShowHourGlass(Page)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        CloseWebWindow(Page)
    End Sub

    Private Sub CompetitorListMatch_CompetitorSelected() Handles CompetitorListMatch.CompetitorSelected
        Dim script(5) As String

        script(1) = "<script>window.opener.document.forms[0]." + txtReturnIDControl.Text + ".value= '"
        script(2) = CompetitorListMatch.SelectedCompetitor & "'"
        script(3) = ";window.opener.document.forms[0].submit()"
        script(4) = ";self.close()"
        script(5) = "</" + "script>"

        'execute the script
        RegisterClientScriptBlock("test", Join(script, ""))
    End Sub

End Class
