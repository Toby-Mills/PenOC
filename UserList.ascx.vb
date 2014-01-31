Partial Class UserList
    Inherits System.Web.UI.UserControl

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

	Private c_dtUser As DataTable
	Private c_intSelectedUser As Integer

	Public Event UserDeleted()
	Public Event UserSelected()

	Public Enum UserListColumn As Integer
		UserID = 0
		Delete = 1
		Enabled = 2
		Name = 3
		NameLink = 4
		UserName = 5
		Admin = 6
	End Enum

	Public Property DisplayColumn(ByVal intColumn As UserListColumn) As Boolean
		Get
			DisplayColumn = Me.grdUser.Columns(intColumn).Visible
		End Get
		Set(ByVal Value As Boolean)
			grdUser.Columns(intColumn).Visible = Value
		End Set
	End Property

	Public Property Selectable() As Boolean
		Get
			Selectable = Me.DisplayColumn(UserListColumn.NameLink)
		End Get
		Set(ByVal Value As Boolean)
			Me.DisplayColumn(UserListColumn.NameLink) = Value
			Me.DisplayColumn(UserListColumn.Name) = Not Value
		End Set
	End Property

	Public Property UserTable() As DataTable
		Get
			UserTable = c_dtUser
		End Get
		Set(ByVal Value As DataTable)
			c_dtUser = Value
			BindGrid()
		End Set
	End Property

	Public ReadOnly Property SelectedUser() As Integer
		Get
			SelectedUser = c_intSelectedUser
		End Get
	End Property

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
	End Sub

	Private Function BindGrid()
		Me.grdUser.DataSource = c_dtUser
		Me.grdUser.DataBind()
	End Function

	Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
		Dim itm As DataGridItem
		Dim intUser As Integer

		itm = sender.Parent.Parent
		intUser = CInt(itm.Cells(UserListColumn.UserID).Text)

		Me.c_intSelectedUser = intUser
		RaiseEvent UserDeleted()

	End Sub

	Private Sub grdUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUser.ItemCommand
		Dim intUser As Integer

		Select Case e.CommandName
			Case "Select"
				c_intSelectedUser = e.Item.Cells(Me.UserListColumn.UserID).Text
				RaiseEvent UserSelected()
		End Select
	End Sub
End Class
