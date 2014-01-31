Partial Class CompetitorSearch
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
    Protected WithEvents CompetitorListSearch As CompetitorList

    Private c_conDB As OleDb.OleDbConnection
    Private c_blnAutoSearch As Boolean

    Public Event CompetitorSelected()
    Public Event CompetitorDeleted()

    Public ReadOnly Property SelectedCompetitor() As Integer
        Get
            SelectedCompetitor = Me.CompetitorListSearch.SelectedCompetitor
        End Get
    End Property

    Public Property DisplayColumn(ByVal intColumn As CompetitorList.enumCompetiorListColumn) As Boolean
        Get
            DisplayColumn(intColumn) = Me.CompetitorListSearch.DisplayColumn(intColumn)
        End Get
        Set(ByVal Value As Boolean)
            Me.CompetitorListSearch.DisplayColumn(intColumn) = Value
        End Set
    End Property

    Public Property Selectable() As Boolean
        Get
            Selectable = Me.CompetitorListSearch.Selectable
        End Get
        Set(ByVal Value As Boolean)
            CompetitorListSearch.Selectable = Value
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
            If c_blnAutoSearch Then
                RunSearch()
            End If
        End If
    End Sub

    Private Sub PopulateControls()
        g_lookupManager.PopulateWebRadioButtonList(c_conDB, Me.rblGender, LookupManager.enumLookupTable.Gender, False)
        Me.rblGender.SelectedValue = NULL_NUMBER
    End Sub
    Public Sub RunSearch()
        Dim dtCompetitor As DataTable
        Dim strWHERE As String
        Dim intCount As Integer

        Dim strDelimiter As String
        Dim chrDelimiter As Char()

        strDelimiter = " "
        chrDelimiter = strDelimiter.ToCharArray()

        If Me.txtName.Text > "" Then
            For intCount = 0 To UBound(txtName.Text.Split(chrDelimiter))
                If strWHERE > "" Then
                    strWHERE = strWHERE & " AND "
                End If
				strWHERE = strWHERE & PenOCDB.WhereCompetitor_NameLike("%" & txtName.Text.Split(" ")(intCount) & "%")
            Next
        End If

        If Me.rblGender.SelectedValue > 0 Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereCompetitor_Gender(Me.rblGender.SelectedValue)
        End If

        If Me.chkOrganisers.Checked Then
            If strWHERE > "" Then
                strWHERE = strWHERE & " AND "
            End If
			strWHERE = strWHERE & PenOCDB.WhereCompetitor_Organiser
        End If

        dtCompetitor = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)

        Me.CompetitorListSearch.CompetitorsTable = dtCompetitor

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        RunSearch()
    End Sub

    Private Sub CompetitorListSearch_CompetitorSelected() Handles CompetitorListSearch.CompetitorSelected
        RaiseEvent CompetitorSelected()
    End Sub

    Private Sub CompetitorListSearch_CompetitorDeleted() Handles CompetitorListSearch.CompetitorDeleted
        RaiseEvent CompetitorDeleted()
    End Sub
End Class
