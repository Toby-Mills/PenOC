Partial Class EventList
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

    Private c_dtEvents As DataTable
    Private c_conDB As OleDb.OleDbConnection
    Private c_intSelectedEvent As Integer

    Public Enum enumEventListColumn
        EventID = 0
        Delete = 1
        Icons = 2
        EventDate = 3
        EventName = 4
        EventNameLink = 5
        Venue = 6
        Registration = 7
        Starts = 8
        Close = 9
        Photos = 10
    End Enum

    Public Event EventSelected()
    Public Event EventDeleted()
    Public Event EventPhotos()

    Public Property DisplayColumn(ByVal intColumn As enumEventListColumn) As Boolean
        Get
            DisplayColumn = Me.grdEvent.Columns(intColumn).Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.grdEvent.Columns(intColumn).Visible = Value
        End Set
    End Property

    Public Property Selectable() As Boolean
        Get
            Selectable = grdEvent.Columns(enumEventListColumn.EventNameLink).Visible
        End Get
        Set(ByVal Value As Boolean)
            grdEvent.Columns(enumEventListColumn.EventNameLink).Visible = Value
            grdEvent.Columns(enumEventListColumn.EventName).Visible = Not (Value)
        End Set
    End Property

    Public Property DisplayEventCount() As Boolean
        Get
            DisplayEventCount = Me.lblEventCount.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.lblEventCount.Visible = Value
            Me.lblEventCountLabel.Visible = Value
        End Set
    End Property

    Public ReadOnly Property SelectedEvent() As Integer
        Get
            SelectedEvent = c_intSelectedEvent
        End Get
    End Property

    Public WriteOnly Property EventTable() As DataTable
        Set(ByVal Value As DataTable)
            c_dtEvents = Value
            Me.grdEvent.CurrentPageIndex = 0
            RefreshGrid()
        End Set
    End Property

    Public Property AllowPaging() As Boolean
        Get
            AllowPaging = Me.grdEvent.AllowPaging
        End Get
        Set(ByVal Value As Boolean)
            Me.grdEvent.AllowPaging = Value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            PageSize = Me.grdEvent.PageSize
        End Get
        Set(ByVal Value As Integer)
            Me.grdEvent.PageSize = Value
        End Set
    End Property

    Public Function RefreshGrid()
        Me.grdEvent.DataSource = c_dtEvents
        Me.grdEvent.DataBind()
        If c_dtEvents Is Nothing Then
            Me.lblEventCount.Text = "0"
        Else
            Me.lblEventCount.Text = c_dtEvents.Rows.Count
        End If
    End Function

    Private Sub grdEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEvent.PageIndexChanged
        Me.grdEvent.CurrentPageIndex = e.NewPageIndex
        RefreshGrid()
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Item(Me.ID & "eventtable") = Me.c_dtEvents
    End Sub

    Protected Sub cmdPhotos_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intEvent As Integer

        itm = sender.Parent.Parent
        intEvent = CInt(itm.Cells(Me.enumEventListColumn.EventID).Text)
        c_intSelectedEvent = intEvent
        RaiseEvent EventPhotos()

    End Sub

    Private Sub grdEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdEvent.ItemDataBound
        Dim drvEvent As DataRowView
        Dim intEvent As Integer
        Dim strDetails As String
        Dim strResults As String
        Dim strPhotos As String
        Dim dtEventType As DataTable
        Dim drEventType As DataRow
        Dim imgEventType As Web.UI.WebControls.Image

        If TypeOf (e.Item.DataItem) Is DataRowView Then
            drvEvent = e.Item.DataItem
            LoadDBValue(drvEvent.Item("idEvent"), intEvent)
            LoadDBValue(drvEvent.Item("notice"), strDetails)
            LoadDBValue(drvEvent.Item("results"), strResults)
            LoadDBValue(drvEvent.Item("photos"), strPhotos)
            e.Item.Cells(Me.enumEventListColumn.Photos).Controls(1).Visible = (strPhotos > "")
            If Me.grdEvent.Columns(Me.enumEventListColumn.Icons).Visible Then
                dtEventType = PenOCDB.GetTable_EventType(c_conDB, intEvent)
                For Each drEventType In dtEventType.Rows
                    imgEventType = New Web.UI.WebControls.Image
                    imgEventType.ImageUrl = "images\" & drEventType.Item("EventTypeIcon")
                    imgEventType.Style.Add("margin", "0px")
                    imgEventType.ToolTip = drEventType.Item("EventType")
                    e.Item.Cells(Me.enumEventListColumn.Icons).Controls.Add(imgEventType)
                Next
            End If
        End If
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intEvent As Integer

        itm = sender.Parent.Parent
        intEvent = CInt(itm.Cells(Me.enumEventListColumn.EventID).Text)

        Me.c_intSelectedEvent = intEvent
        RaiseEvent EventDeleted()

    End Sub

    Private Sub grdEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdEvent.ItemCommand
        Select Case e.CommandName
            Case "Select"
                Me.c_intSelectedEvent = e.Item.Cells(Me.enumEventListColumn.EventID).Text
                RaiseEvent EventSelected()
        End Select
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
        Else
            Me.c_dtEvents = Session.Item(Me.ID & "eventtable")
        End If
    End Sub
End Class
