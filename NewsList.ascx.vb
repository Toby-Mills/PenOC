Partial Class NewsList
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNews As System.Web.UI.WebControls.Label
    Protected WithEvents cmdEdit As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cmdDelete As System.Web.UI.WebControls.ImageButton
    Protected WithEvents cmdTitle As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private c_dtNews As DataTable
    Private c_intSelectedNews As Integer
    Private c_blnDisplayEdit As Boolean
    Private c_blnDisplayDelete As Boolean
    Private c_blnDisplayImages As Boolean
    Private c_blnDisplayTitleOnly As Boolean

    Public Event NewsSelected()
    Public Event NewsEdited()
    Public Event NewsDeleted()

    Public WriteOnly Property NewsTable() As DataTable
        Set(ByVal Value As DataTable)
            c_dtNews = Value
            RefreshNews()
        End Set
    End Property

    Public Property DisplayEdit() As Boolean
        Get
            DisplayEdit = c_blnDisplayEdit
        End Get
        Set(ByVal Value As Boolean)
            c_blnDisplayEdit = Value
        End Set
    End Property

    Public Property DisplayDelete() As Boolean
        Get
            DisplayDelete = c_blnDisplayDelete
        End Get
        Set(ByVal Value As Boolean)
            c_blnDisplayDelete = Value
        End Set
    End Property

    Public Property DisplayTitleOnly() As Boolean
        Get
            DisplayTitleOnly = c_blnDisplayTitleOnly
        End Get
        Set(ByVal Value As Boolean)
            c_blnDisplayTitleOnly = Value
        End Set
    End Property

    Public ReadOnly Property SelectedNewsItem() As Integer
        Get
            SelectedNewsItem = c_intSelectedNews
        End Get
    End Property

    Public Property DisplayImages() As Boolean
        Get
            DisplayImages = c_blnDisplayImages
        End Get
        Set(ByVal Value As Boolean)
            c_blnDisplayImages = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            RefreshNews()
        Else
            c_blnDisplayEdit = Session.Item(Me.ID & "displayedit")
            c_blnDisplayDelete = Session.Item(Me.ID & "displaydelete")
            c_blnDisplayImages = Session.Item(Me.ID & "displayimages")
        End If
    End Sub

    Private Sub RefreshNews()

        Me.RepeaterNews.DataSource = c_dtNews
        Me.RepeaterNews.DataBind()

    End Sub

    Private Sub RepeaterNews_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles RepeaterNews.ItemCommand
        Dim intNews As Integer

        Select Case e.CommandName
            Case "News"
                c_intSelectedNews = e.CommandArgument
                RaiseEvent NewsSelected()
            Case "Edit"
                c_intSelectedNews = e.CommandArgument
                RaiseEvent NewsEdited()
            Case "Delete"
                c_intSelectedNews = e.CommandArgument
                RaiseEvent NewsDeleted()

        End Select
    End Sub

    Private Sub RepeaterNews_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterNews.ItemDataBound
        Dim strNews As String

        cmdEdit = e.Item.FindControl("cmdEdit")
        cmdEdit.Visible = c_blnDisplayEdit
        cmdDelete = e.Item.FindControl("cmdDelete")
        cmdDelete.Visible = c_blnDisplayDelete

        If c_blnDisplayTitleOnly Then
            strNews = "..."
            lblNews = e.Item.FindControl("lblNews")
            lblNews.Text = strNews
        Else
            If Not c_blnDisplayImages Then
                strNews = e.Item.DataItem.item("News")
                strNews = ReplaceHTMLTags(strNews, "img", "[IMAGE]")
                lblNews = e.Item.FindControl("lblNews")
                lblNews.Text = strNews
            End If
        End If

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item(Me.ID & "displayedit") = c_blnDisplayEdit
        Session.Item(Me.ID & "displaydelete") = c_blnDisplayDelete
        Session.Item(Me.ID & "displayimages") = c_blnDisplayImages

    End Sub
End Class
