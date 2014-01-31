Partial Class MembershipList
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

    Private c_dtMembership As DataTable
    Private c_intSelected As Integer

    Public Event Selected()
    Public Event Deleted()

    Public Enum MembershipListColumn
        MembershipID = 0
        Delete = 1
        Club = 2
        MembershipNumber = 3
        MembershipType = 4
        PrincipalMember = 5
        MemberCount = 6
        LastYear = 7
    End Enum

    Public Property MembershipTable() As DataTable
        Get
            MembershipTable = c_dtMembership
        End Get
        Set(ByVal Value As DataTable)
            c_dtMembership = Value
            BindGrid()
        End Set
    End Property

    Public ReadOnly Property SelectedMembership() As Integer
        Get
            SelectedMembership = c_intSelected
        End Get
    End Property

    Public Property DisplayColumn(ByVal intColumn As MembershipListColumn) As Boolean
        Get
            DisplayColumn = Me.grdMembership.Columns(intColumn).Visible
        End Get
        Set(ByVal Value As Boolean)
            grdMembership.Columns(intColumn).Visible = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Function BindGrid()
        Me.grdMembership.DataSource = c_dtMembership
        Me.grdMembership.DataBind()
    End Function

    Private Sub grdLog_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdMembership.ItemCommand
        Dim intLog As Integer

        intLog = e.Item.Cells(MembershipListColumn.MembershipID).Text
        Select Case e.CommandName

        End Select
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem

        itm = sender.Parent.Parent
        Me.c_intSelected = CInt(itm.Cells(MembershipListColumn.MembershipID).Text)

        RaiseEvent Deleted()

    End Sub

End Class
