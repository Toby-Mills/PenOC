Partial Class CompetitorSearchPopup
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
		'First time loading....
        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With Me.CompetitorSearch
                .Selectable = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.BirthDate) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Delete) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Email) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Position) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone1) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone2) = False
            End With
            txtIDControlID.Text = Request.Item("ctlID")
            txtNameControlID.Text = Request.Item("ctlName")
            txtIDControlID.Width = New Web.UI.WebControls.Unit(0)
            txtNameControlID.Width = New Web.UI.WebControls.Unit(0)
        End If
        ShowHourGlass(Page)
	End Sub

	Public Shared Sub ShowCompetitorSearch(ByVal ctlOpener As System.Web.UI.WebControls.WebControl, ByVal ctlID As System.Web.UI.WebControls.WebControl, ByVal ctlName As System.Web.UI.WebControls.WebControl)
		Dim clientScript As String
		Dim windowAttribs As String

		'Building Client side window attributes with width and height.
		'Also the the window will be positioned to the middle of the screen
		windowAttribs = "width=600px," & _
		 "height=500px," & _
		 "left='+((screen.width -600) / 2)+'," & _
		 "top='+ (screen.height - 500) / 2+',"

		'Building the client script- window.open
		clientScript = "newwin = window.open ('CompetitorSearchPopup.aspx?ctlID=" & ctlID.ClientID & "&ctlName=" & ctlName.ClientID & "','Calendar','" & windowAttribs & "');newwin.focus();return false;"
		'regiter the script to the clientside click event of the 'opener' control
		ctlOpener.Attributes.Add("onClick", clientScript)
		ctlOpener.Attributes.Add("language", "javascript")
	End Sub

	Private Sub CompetitorSearch_CompetitorSelected() Handles CompetitorSearch.CompetitorSelected
		'when the user selects a date, run a javascript to update the window that opened the calendar
		Dim script(4) As String
		Dim intCompetitor As Integer
		Dim strCompetitor As String

		intCompetitor = CompetitorSearch.SelectedCompetitor
		strCompetitor = PenOCDB.LookupCompetitor_FullName(c_condb, intCompetitor)

		'build the script
		script(1) = "<script>window.opener.document.forms[0]." + txtIDControlID.Text + ".value= '" & intCompetitor
		script(2) = "';window.opener.document.forms[0]." + txtNameControlID.Text + ".value= '" & strCompetitor
		script(3) = "';self.close()"
		script(4) = "</" + "script>"

		'execute the script
		RegisterClientScriptBlock("test", Join(script, ""))

	End Sub
End Class
