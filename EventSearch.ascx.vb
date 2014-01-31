Partial Class EventSearch
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
    Protected WithEvents EventListSearch As EventList

    Private c_conDB As OleDb.OleDbConnection
    Private c_blnAutoSearch As Boolean

    Public Event EventSelected()
    Public Event EventDeleted()
    Public Event EventPhotos()

    Public ReadOnly Property SelectedEvent() As Integer
        Get
            SelectedEvent = Me.EventListSearch.selectedevent
        End Get
    End Property
    Public Property AllowPaging() As Boolean
        Get
            AllowPaging = Me.EventListSearch.AllowPaging
        End Get
        Set(ByVal Value As Boolean)
            Me.EventListSearch.AllowPaging = Value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            PageSize = Me.EventListSearch.PageSize
        End Get
        Set(ByVal Value As Integer)
            Me.EventListSearch.PageSize = Value
        End Set
    End Property

    Public Property DisplayColumn(ByVal intColumn As EventList.enumEventListColumn) As Boolean
        Get
            DisplayColumn = Me.EventListSearch.DisplayColumn(intColumn)
        End Get
        Set(ByVal Value As Boolean)
            Me.EventListSearch.DisplayColumn(intColumn) = Value
        End Set
    End Property

    Public Property Selectable() As Boolean
        Get
            Selectable = EventListSearch.Selectable
        End Get
        Set(ByVal Value As Boolean)
            EventListSearch.Selectable = Value
        End Set
    End Property

    Public Property AutoSearch() As Boolean
        Get
            AutoSearch = c_blnAutoSearch
        End Get
        Set(ByVal Value As Boolean)
            c_blnAutoSearch = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            PopulateControls()
            CalendarPopup.ShowCalendar(Me.cmdCalDateFrom, Me.txtDateFrom, g_strDateFormat, False)
            CalendarPopup.ShowCalendar(Me.cmdCalDateTo, Me.txtDateTo, g_strDateFormat, False)
            If c_blnAutoSearch Then
                RunSearch()
            End If
        End If
    End Sub

    Private Sub PopulateControls()
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbClub, LookupManager.enumLookupTable.ClubShortName, False)
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbVenue, LookupManager.enumLookupTable.Venue, False)
        PopulateOrganiser()
    End Sub

    Private Sub PopulateOrganiser()
        Dim dtOrganiser As DataTable
        Dim drOrganiser As DataRow

		dtOrganiser = PenOCDB.GetTable_Competitor(c_conDB, PenOCDB.WhereCompetitor_Organiser)
        drOrganiser = dtOrganiser.NewRow
        drOrganiser.Item("idCompetitor") = 0
        dtOrganiser.Rows.InsertAt(drOrganiser, 0)
        Me.cmbOrganiser.DataSource = dtOrganiser
        Me.cmbOrganiser.DataValueField = "idCompetitor"
        Me.cmbOrganiser.DataTextField = "Competitor"
        Me.cmbOrganiser.DataBind()

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        RunSearch()

    End Sub

    Private Sub EventListSearch_EventSelected() Handles EventListSearch.EventSelected
        RaiseEvent EventSelected()
    End Sub

    Private Sub EventListSearch_EventDeleted() Handles EventListSearch.EventDeleted
        RaiseEvent EventDeleted()
    End Sub

    Public Sub RunSearch()
        Dim dtEvent As DataTable
        Dim strWHERE As String
        Dim dteFrom As Date
        Dim dteTo As Date

        If Me.cmbClub.SelectedValue > 0 Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereEvent_idClub(Me.cmbClub.SelectedValue)
        End If

        If Me.cmbVenue.SelectedValue > 0 Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereEvent_idVenue(Me.cmbVenue.SelectedValue)
        End If

        If Me.cmbOrganiser.SelectedValue > 0 Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereEvent_idOrganiser(Me.cmbOrganiser.SelectedValue)
        End If

        If Me.txtDateFrom.Text > "" Then
            dteFrom = Date.ParseExact(Me.txtDateFrom.Text, g_strDateFormat, Nothing)
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereEvent_DateFrom(dteFrom)
        End If

        If Me.txtDateTo.Text > "" Then
            dteTo = Date.ParseExact(Me.txtDateTo.Text, g_strDateFormat, Nothing)
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereEvent_DateTo(dteTo)
        End If

        dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, OrderEvent_Date(False))
        Me.EventListSearch.EventTable = dtEvent

    End Sub

    Private Sub EventListSearch_EventPhotos() Handles EventListSearch.EventPhotos
        RaiseEvent eventphotos()
    End Sub
End Class
