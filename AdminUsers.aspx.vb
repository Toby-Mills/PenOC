Partial Class AdminUsers
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
    Protected WithEvents UserList As UserList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            Me.txtUserID.Width = New Web.UI.WebControls.Unit(0)
            CompetitorSearchPopup.ShowCompetitorSearch(Me.cmdUserSearch, Me.txtUserID, Me.txtUser)
            Me.UserList.Selectable = True
            Me.UserList.DisplayColumn(UserList.UserListColumn.Delete) = True
            Me.pnlUserList.Visible = True
            Me.pnlEdit.Visible = False
            RefreshUserList()
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub RefreshUserList()
        Dim dtUser As DataTable

        dtUser = GetTable_User(c_conDB, "")
        Me.UserList.UserTable = dtUser
    End Sub

    Private Sub UserList_UserSelected() Handles UserList.UserSelected
        Me.pnlUserList.Visible = False
        Me.pnlEdit.Visible = True
        LoadUser(Me.UserList.SelectedUser)

    End Sub

    Private Sub LoadUser(ByVal intUser As Integer)
        Dim dtUser As DataTable
        Dim drUser As DataRow
        Dim strUser As String
        Dim strUserName As String
        Dim strPassword As String
        Dim blnEnabled As Boolean
        Dim blnAdministrator As Boolean

        If intUser = -1 Then
            txtUserID.Text = intUser
            txtUser.Text = ""
            txtUserName.Text = ""
            chkEnabled.Checked = True
            chkAdministrator.Checked = False
            Me.cmdUserSearch.Visible = True
        Else
            dtUser = GetTable_User(c_conDB, WhereUser_UserID(intUser))
            If dtUser.Rows.Count > 0 Then
                drUser = dtUser.Rows(0)
                LoadDBValue(drUser.Item("FullName"), strUser)
                LoadDBValue(drUser.Item("UserName"), strUserName)
                LoadDBValue(drUser.Item("Enabled"), blnEnabled)
                LoadDBValue(drUser.Item("Administrator"), blnAdministrator)
                txtUserID.Text = intUser
                txtUser.Text = strUser
                txtUserName.Text = strUserName
                chkEnabled.Checked = blnEnabled
                chkAdministrator.Checked = blnAdministrator
                Me.cmdUserSearch.Visible = False
            End If
        End If

        Me.chkResetPassword.Checked = False
        Me.lblMessage.Text = ""

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.pnlUserList.Visible = True
        Me.pnlEdit.Visible = False
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.lblmessage.text = ValidateSave()
        If Me.lblMessage.Text = "" Then
            PenOCDB.SaveUser(c_conDB, Me.txtUserID.Text, Me.txtUserName.Text, Me.chkEnabled.Checked, Me.chkAdministrator.Checked)
            If Me.chkResetPassword.Checked Then
                PenOCDB.SetUserPassword(c_conDB, txtUserID.Text, Me.txtNewPassword.Text)
            End If
            RefreshUserList()
            Me.pnlUserList.Visible = True
            Me.pnlEdit.Visible = False
        End If

    End Sub

    Private Function ValidateSave() As String
        Dim strReturn As String

        Select Case True
            Case Me.txtUserID.Text = ""
                strReturn = "Please select a user"
            Case Me.txtUserID.Text = "-1"
                strReturn = "Please select a user"
            Case Me.txtUserName.Text = ""
                strReturn = "Please provide a user name"
            Case Me.chkResetPassword.Checked And txtNewPassword.Text <> txtConfirmNewPassword.Text
                strReturn = "New password does not match confirmation password"
        End Select

        Return strReturn

    End Function
    Private Sub cmdNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewUser.Click
        LoadUser(-1)
        Me.pnlEdit.Visible = True
        Me.pnlUserList.Visible = False
    End Sub

    Private Sub UserList_UserDeleted() Handles UserList.UserDeleted

        ConfirmDelete.PopupConfirmDelete(Page, ConfirmDelete.enumDeleteType.User, UserList.SelectedUser)

    End Sub
End Class
