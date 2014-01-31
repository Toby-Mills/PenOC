Partial Class CompetitorList
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
    Private c_dtCompetitors As DataTable
    Private c_conDB As OleDb.OleDbConnection
    Private c_intSelectedCompetitor As Integer
    Private c_blnHighlightSelected As Boolean

    Public Enum enumCompetiorListColumn
        CompetitorID = 0
        Delete = 1
        Email = 2
        Position = 3
        FullName = 4
        FullNameLink = 5
        Surname = 6
        FirstName = 7
        Competitor = 8
        BirthDate = 9
        Gender = 10
        Category = 11
        EmitNumber = 12
        Telephone1 = 13
        Telephone2 = 14
    End Enum

    Public Event CompetitorSelected()
    Public Event CompetitorDeleted()
	Public Event CompetitorEmailed()

    Public Property DisplayColumn(ByVal intColumn As enumCompetiorListColumn) As Boolean
        Get
            DisplayColumn = Me.grdCompetitor.Columns(intColumn).Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.grdCompetitor.Columns(intColumn).Visible = Value
        End Set
    End Property

    Public Property Selectable() As Boolean
        Get
            Selectable = grdCompetitor.Columns(enumCompetiorListColumn.FullNameLink).Visible
        End Get
        Set(ByVal Value As Boolean)
            grdCompetitor.Columns(enumCompetiorListColumn.FullNameLink).Visible = Value
            grdCompetitor.Columns(enumCompetiorListColumn.FullName).Visible = Not (Value)
        End Set
    End Property

    Public Property HighlightSelected() As Boolean
        Get
            HighlightSelected = c_blnHighlightSelected
        End Get
        Set(ByVal Value As Boolean)
            c_blnHighlightSelected = Value
        End Set
    End Property

    Public ReadOnly Property SelectedCompetitor() As Integer
        Get
            SelectedCompetitor = c_intSelectedCompetitor
        End Get
    End Property

    Public WriteOnly Property CompetitorsTable() As DataTable
        Set(ByVal Value As DataTable)
            Me.grdCompetitor.CurrentPageIndex = 0
            c_dtCompetitors = Value
            RefreshGrid()
        End Set
    End Property

    Public Property AllowPaging() As Boolean
        Get
            AllowPaging = Me.grdCompetitor.AllowPaging
        End Get
        Set(ByVal Value As Boolean)
            Me.grdCompetitor.AllowPaging = Value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            If Me.grdCompetitor.AllowPaging Then
                PageSize = Me.grdCompetitor.PageSize
            Else
                PageSize = -1
            End If
        End Get
        Set(ByVal Value As Integer)
            Me.grdCompetitor.AllowPaging = (Value > 0)
            If Value > 0 Then
                Me.grdCompetitor.PageSize = Value
            End If
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
		Else
            c_dtCompetitors = Session.Item(Me.ID & "competitorstable")
            c_blnHighlightSelected = Session.Item(Me.ID & "highlightselected")
            'RefreshGrid()
        End If
    End Sub

    Public Function RefreshGrid()
        Me.grdCompetitor.DataSource = c_dtCompetitors
        Me.grdCompetitor.DataBind()
    End Function

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Item(Me.ID & "competitorstable") = c_dtCompetitors
        Session.Item(Me.ID & "highlightselected") = c_blnHighlightSelected
    End Sub

    Private Sub grdCompetitor_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdCompetitor.PageIndexChanged
        Me.grdCompetitor.CurrentPageIndex = e.NewPageIndex
        RefreshGrid()
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intCompetitor As Integer

        itm = sender.Parent.Parent
        intCompetitor = CInt(itm.Cells(enumCompetiorListColumn.CompetitorID).Text)

        Me.c_intSelectedCompetitor = intCompetitor
        RaiseEvent CompetitorDeleted()

    End Sub

    Protected Sub cmdEmail_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intCompetitor As Integer

        itm = sender.Parent.Parent
        intCompetitor = CInt(itm.Cells(enumCompetiorListColumn.CompetitorID).Text)
		c_intSelectedCompetitor = intCompetitor
		RaiseEvent CompetitorEmailed()

    End Sub

    Private Sub grdCompetitor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCompetitor.ItemCommand
        Select Case e.CommandName
            Case "Select"
                c_intSelectedCompetitor = e.Item.Cells(enumCompetiorListColumn.CompetitorID).Text
                RaiseEvent CompetitorSelected()
        End Select
    End Sub
End Class
