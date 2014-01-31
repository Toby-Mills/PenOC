Partial Class Login
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

	Private c_condb As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            DefaultButton(Page, cmdLogin.ClientID)
            WebFocusControl(Page, txtUserName)
            Me.pnlLogin.Visible = True
            Me.pnlError.Visible = False
            Me.pnlSuccess.Visible = False
        End If
        ShowHourGlass(Page)

	End Sub

	Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
		Dim intUser As Integer
		Dim dtUser As DataTable
		Dim drUser As DataRow
		Dim strFullName As String
		Dim strRedirect As String

		intUser = LoginUser(c_condb, Me.txtUserName.Text, Me.txtPassword.Text)
		If intUser = -1 Then
			Me.pnlError.Visible = True
		Else
			dtUser = Gettable_User(c_condb, WhereUser_UserID(intUser))
			If dtUser.Rows.Count > 0 Then
				drUser = dtUser.Rows(0)
				strFullName = drUser.Item("FullName")
			End If
		End If

		Session.Item("user") = intUser
		Session.Item("username") = strFullName

		If intUser > -1 Then
			strRedirect = Session.Item("loginredirect")

			If strRedirect > "" Then
				Server.Transfer(strRedirect)
            Else
                If UserIsAdministrator(c_condb, intUser) Then
                    Server.Transfer("Admin.aspx")
                Else
                    Server.Transfer("Start.aspx")
                End If
            End If
        End If

		modFunctions_WebForms.RedirectFrame(Page, "contents", "menu.aspx")

	End Sub

	Private Sub cmdRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRequest.Click
		OpenPopUp(Page, "mailto:" & g_strEmailWebsite & "?subject=PenOC Website Login Request", "Email")
	End Sub
End Class
