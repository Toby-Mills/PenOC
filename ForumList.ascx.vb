Partial Class ForumList
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

    Public Event Selected()

    Private c_intSelectedForum As Integer

    Public Enum enumForumListColumn
        ForumID = 0
        ForumName = 1
    End Enum
    Public Property ForumTable() As DataTable
        Get
            ForumTable = Me.grdForum.DataSource
        End Get
        Set(ByVal Value As DataTable)
            grdForum.DataSource = Value
            BindGrid()
        End Set
    End Property

    Public ReadOnly Property SelectedForum() As Integer
        Get
            SelectedForum = c_intSelectedForum
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Function BindGrid()

        Me.grdForum.DataBind()

    End Function

    Private Sub grdForum_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdForum.ItemCommand
        Select Case e.CommandName
            Case "select"
                c_intSelectedForum = e.Item.Cells(Me.enumForumListColumn.ForumID).Text
                RaiseEvent Selected()
        End Select
    End Sub
End Class
