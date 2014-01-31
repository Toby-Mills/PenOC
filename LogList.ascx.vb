Partial Class LogList
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

	Private c_dtLog As DataTable
	Private c_intSelected As Integer

	Public Event Selected()
    Public Event LogDeleted()
    Public Event SetCurrent()

	Public Enum LogListColumn
		LogID = 0
		Delete = 1
		Log = 2
		Name = 3
		Year = 4
		Events = 5
        Disregard = 6
        Current = 7
	End Enum

	Public Property LogTable() As DataTable
		Get
			LogTable = c_dtLog
		End Get
		Set(ByVal Value As DataTable)
			c_dtLog = Value
			BindGrid()
		End Set
	End Property

	Public ReadOnly Property SelectedLog() As Integer
		Get
			SelectedLog = c_intSelected
		End Get
	End Property

	Public Property DisplayColumn(ByVal intColumn As LogListColumn) As Boolean
		Get
			DisplayColumn = grdLog.Columns(intColumn).Visible
		End Get
		Set(ByVal Value As Boolean)
			grdLog.Columns(intColumn).Visible = Value
		End Set
	End Property

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
	End Sub

	Private Function BindGrid()
		Me.grdLog.DataSource = c_dtLog
		Me.grdLog.DataBind()
	End Function

	Private Sub grdLog_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdLog.ItemCommand
		Dim intLog As Integer

		intLog = e.Item.Cells(Me.LogListColumn.LogID).Text
		Select Case e.CommandName
			Case "log"
				c_intSelected = intLog
				RaiseEvent Selected()
		End Select
	End Sub

	Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
		Dim itm As DataGridItem
		Dim intLog As Integer

		itm = sender.Parent.Parent
		intLog = CInt(itm.Cells(LogListColumn.LogID).Text)

		Me.c_intSelected = intLog
		RaiseEvent LogDeleted()

	End Sub
    Public Sub chkCurrent_OnCheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dgiLog As DataGridItem
        Dim chkCheckBox As CheckBox
        Dim intLog As Integer
        Dim blnCurrent As Boolean

        'get the datagird item
        dgiLog = sender.parent.parent

        If Not dgiLog Is Nothing Then
            'get the id of the selected row
            c_intSelected = dgiLog.Cells(LogListColumn.LogID).Text
            chkCheckBox = dgiLog.Cells(LogListColumn.Current).Controls(1)
            blnCurrent = chkCheckBox.Checked
        End If

        RaiseEvent SetCurrent()
    End Sub
End Class
