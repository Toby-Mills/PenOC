Partial Class FileList
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
	Private c_dtFile As DataTable
	Private c_intSelectedFile As Integer

	Public Event FileSelected()
	Public Event FileDeleted()
	Public Event FileUpdated()

	Public Enum enumFileListColumn
		FileID = 0
		Delete = 1
		Update = 2
		FileName = 3
		FileNameLink = 4
		Description = 5
	End Enum

	Public Property FileTable() As DataTable
		Get
			FileTable = c_dtFile
		End Get
		Set(ByVal Value As DataTable)
			c_dtFile = Value
			Me.grdFile.CurrentPageIndex = 0
			RefreshGrid()
		End Set
	End Property

	Public ReadOnly Property SelectedFile() As Integer
		Get
			SelectedFile = c_intSelectedFile
		End Get
	End Property

	Public Property DisplayColumn(ByVal intColumn As enumFileListColumn) As Boolean
		Get
			DisplayColumn = Me.grdFile.Columns(intColumn).Visible
		End Get
		Set(ByVal Value As Boolean)
			Me.grdFile.Columns(intColumn).Visible = Value
		End Set
	End Property
	Public Function RefreshGrid()
		Me.grdFile.DataSource = c_dtFile
		Me.grdFile.DataBind()
	End Function

	Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
		Dim itm As DataGridItem
		Dim intEvent As Integer

		itm = sender.Parent.Parent
		intEvent = CInt(itm.Cells(Me.enumFileListColumn.FileID).Text)

		Me.c_intSelectedFile = intEvent
		RaiseEvent FileDeleted()

	End Sub

	Protected Sub cmdUpdate_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
		Dim itm As DataGridItem
		Dim intEvent As Integer

		itm = sender.Parent.Parent
		intEvent = CInt(itm.Cells(Me.enumFileListColumn.FileID).Text)

		Me.c_intSelectedFile = intEvent
		RaiseEvent FileUpdated()


	End Sub
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
        Else
            c_dtFile = Session.Item("c_dtfile")
        End If
	End Sub

	Private Sub grdFile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFile.ItemCommand
		Select Case e.CommandName
			Case "Select"
				Me.c_intSelectedFile = e.Item.Cells(Me.enumFileListColumn.FileID).Text
				RaiseEvent FileSelected()
		End Select
	End Sub

    Private Sub grdFile_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdFile.PageIndexChanged
        grdFile.CurrentPageIndex = e.NewPageIndex
        RefreshGrid()

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Item("c_dtfile") = c_dtFile

    End Sub
End Class
