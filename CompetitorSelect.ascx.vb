Partial Class CompetitorSelect
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

    Private c_conDB As OleDb.OleDbConnection
    Private c_dtCompetitor As DataTable
    Private c_blnRequired As Boolean

    Public Event NewCompetitor()
    Public Event EditCompetitor()
    Public Event SelectionChanged()

    Public Property DisplayNew() As Boolean
        Get
            DisplayNew = Me.cmdAddNew.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.cmdAddNew.Visible = Value
        End Set
    End Property

    Public Property DisplayEdit() As Boolean
        Get
            DisplayEdit = Me.cmdEditCompetitor.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.cmdEditCompetitor.Visible = Value
        End Set
    End Property

    Public Property AutoPostBack() As Boolean
        Get
            AutoPostBack = Me.cmbCompetitor.AutoPostBack
        End Get
        Set(ByVal Value As Boolean)
            Me.cmbCompetitor.AutoPostBack = Value
        End Set
    End Property

    Public ReadOnly Property Selected() As Integer
        Get
            Selected = Me.cmbCompetitor.SelectedValue
        End Get
    End Property

    Public Property Required() As Boolean
        Get
            Required = c_blnRequired
        End Get
        Set(ByVal Value As Boolean)
            c_blnRequired = Value
        End Set
    End Property
    Public Sub ClearSelection()

        Me.cmbCompetitor.ClearSelection()

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            PopulateControls()
            FilterDataTable()
            'c_dtCompetitor = PenOCDB.GetCompetitorTable(c_conDB, "")
            'RefreshCombobox()
        Else
            c_dtCompetitor = Session.Item(Me.ID & "competitortable")
            c_blnRequired = Session.Item(Me.ID & "required")
        End If

    End Sub

    Private Function PopulateControls()
        g_lookupManager.PopulateWebRadioButtonList(c_conDB, Me.rblGender, LookupManager.enumLookupTable.Gender, False)
        Me.rblGender.SelectedValue = NULL_NUMBER
    End Function

    Private Sub RefreshCombobox()
        With cmbCompetitor
            .DataSource = c_dtCompetitor
            .DataValueField = "idCompetitor"
            .DataTextField = "Competitor"
            .DataBind()
        End With

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item(Me.ID & "competitortable") = c_dtCompetitor
        Session.Item(Me.ID & "required") = c_blnRequired

    End Sub

    Private Sub cmbCompetitor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCompetitor.SelectedIndexChanged

        RaiseEvent SelectionChanged()

    End Sub

    Private Sub FilterDataTable()
        Dim strWHERE As String
        Dim strSurnameStart As String
        Dim strSurnameEnd As String
        Dim drCompetitor As DataRow

        If Me.rblGender.SelectedValue > 0 Then
			strWHERE = PenOCDB.WhereCompetitor_Gender(Me.rblGender.SelectedValue)
        End If

        Select Case rblSurname.SelectedValue
            Case 0
            Case 1
                strSurnameStart = "A"
                strSurnameEnd = "G"
            Case 2
                strSurnameStart = "G"
                strSurnameEnd = "N"
            Case 3
                strSurnameStart = "N"
                strSurnameEnd = "S"
            Case 4
                strSurnameStart = "S"
                strSurnameEnd = "Z"
        End Select
        If strSurnameStart > "" Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereCompetitor_SurnameStart(strSurnameStart)
		End If
		If strSurnameStart > "" Then
			If strWHERE > "" Then
				strWHERE = strWHERE & " AND "
			End If
			strWHERE = strWHERE & PenOCDB.WhereCompetitor_SurnameEnd(strSurnameEnd)
		End If

		c_dtCompetitor = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)
		If (c_dtCompetitor.Rows.Count = 0) Or (c_blnRequired = False) Then
			drCompetitor = c_dtCompetitor.NewRow
			drCompetitor.Item("idCompetitor") = NULL_NUMBER
			c_dtCompetitor.Rows.InsertAt(drCompetitor, 0)
		End If
		RefreshCombobox()

    End Sub

    Private Sub rblGender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblGender.SelectedIndexChanged
        FilterDataTable()
        RaiseEvent SelectionChanged()
    End Sub

    Private Sub rblSurname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSurname.SelectedIndexChanged
        FilterDataTable()
        RaiseEvent SelectionChanged()
    End Sub

    Private Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        RaiseEvent NewCompetitor()
    End Sub

    Private Sub cmdEditCompetitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEditCompetitor.Click
        RaiseEvent EditCompetitor()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        FilterDataTable()
        RaiseEvent SelectionChanged()
    End Sub
End Class
