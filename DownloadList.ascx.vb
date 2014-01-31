Partial Class DownloadList
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents imgDownload As System.Web.UI.WebControls.Image
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnDeleteDownload As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnEditDownload As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtDownloadID As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private c_dtDownload As DataTable
    Private c_intSelected As Integer
    Private c_blnEditable As Boolean

    Public Event Edit()
    Public Event Delete()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

        Else
            c_dtDownload = Session.Item(Me.ClientID & "dtDownload")
            c_intSelected = Session.Item(Me.ClientID & "intSelected")
            c_blnEditable = Session.Item(Me.ClientID & "blnEditable")
        End If

    End Sub

    Public Property Editable() As Boolean
        Get
            Return c_blnEditable
        End Get
        Set(ByVal Value As Boolean)
            c_blnEditable = Value
        End Set
    End Property

    Public ReadOnly Property Selected() As Integer
        Get
            Return c_intSelected
        End Get
    End Property

    Public WriteOnly Property DownloadTable() As DataTable

        Set(ByVal Value As DataTable)
            c_dtDownload = Value
            RefreshDownloadList()
        End Set
    End Property

    Private Sub RefreshDownloadList()
        Me.rptDownload.DataSource = c_dtDownload
        Me.rptDownload.DataBind()
    End Sub

    Private Sub rptDownload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptDownload.ItemCommand
        Dim intDownload As Integer
        Select Case e.CommandName
            Case "Edit"
                c_intSelected = e.CommandArgument
                RaiseEvent Edit()
            Case "Delete"
                c_intSelected = e.CommandArgument
                RaiseEvent Delete()
        End Select
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Item(Me.ClientID & "dtDownload") = c_dtDownload
        Session.Item(Me.ClientID & "intSelected") = c_intSelected
        Session.Item(Me.ClientID & "blnEditable") = c_blnEditable
    End Sub

    Private Sub rptDownload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDownload.ItemDataBound

        Dim btnEdit As Web.UI.WebControls.WebControl
        Dim btnDelete As Web.UI.WebControls.WebControl

        If TypeOf (e.Item.DataItem) Is DataRowView Then
            btnEdit = e.Item.FindControl("btnEditDownload")
            btnEdit.Visible = c_blnEditable
            btnDelete = e.Item.FindControl("btnDeleteDownload")
            btnDelete.Visible = c_blnEditable
        End If

    End Sub
End Class
