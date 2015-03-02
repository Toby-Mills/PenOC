Partial Class ResultList
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
    Private c_dtResults As DataTable

    Public Enum enumResultListColumn
        EventID = 0
        CourseID = 1
        CompetitorID = 2
        Edit = 3
        Delete = 4
        EventDate = 5
        EventName = 6
        EventVenue = 7
        CourseName = 8
        CourseLength = 9
        CourseClimb = 10
        CourseControls = 11
        CourseTechnical = 12
        CourseLog = 13
        ResultPosition = 14
        ResultCompetitor = 15
        ResultCategory = 16
        ResultClub = 17
        ResultRaceNumber = 18
        ResultTime = 19
        ResultPoints = 20
        ResultDisqualified = 21
        ResultComment = 22
    End Enum

    Private c_intSelectedEvent As Integer
    Private c_intSelectedCourse As Integer
    Private c_intSelectedCompetitor As Integer
    Private c_blnEditInPlace As Boolean

    Public Event EventSelected()
    Public Event CourseSelected()
    Public Event ResultEdited()
    Public Event ResultDeleted()

    Public Property EditInPlace() As Boolean
        Get
            EditInPlace = c_blnEditInPlace
        End Get
        Set(ByVal Value As Boolean)
            c_blnEditInPlace = Value
        End Set
    End Property

    Public ReadOnly Property SelectedEvent() As Integer
        Get
            SelectedEvent = c_intSelectedEvent
        End Get
    End Property

    Public ReadOnly Property SelectedCourse() As Integer
        Get
            SelectedCourse = c_intSelectedCourse
        End Get

    End Property

    Public ReadOnly Property SelectedCompetitor() As Integer
        Get
            SelectedCompetitor = c_intSelectedCompetitor
        End Get

    End Property

    Public WriteOnly Property ResultTable() As DataTable
        Set(ByVal Value As DataTable)
            c_dtResults = Value
            RefreshGrid()
        End Set
    End Property

    Public Property AllowPaging() As Boolean
        Get
            AllowPaging = Me.grdResultList.AllowPaging
        End Get
        Set(ByVal Value As Boolean)
            Me.grdResultList.AllowPaging = Value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            If Me.grdResultList.AllowPaging Then
                PageSize = Me.grdResultList.PageSize
            Else
                PageSize = -1
            End If
        End Get
        Set(ByVal Value As Integer)
            If Value < 0 Then
                grdResultList.AllowPaging = False
            Else
                grdResultList.PageSize = Value
            End If
        End Set
    End Property

    Public Property DisplayColumn(ByVal intColumn As enumResultListColumn) As Boolean
        Get
            DisplayColumn = Me.grdResultList.Columns(intColumn).Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.grdResultList.Columns(intColumn).Visible = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
        Else
            Me.c_blnEditInPlace = Session.Item(Me.ID & "editinplace")
            Me.c_dtResults = Session.Item(Me.ID & "resulttable")
            RefreshGrid()
        End If
    End Sub

    Public Function RefreshGrid()
        Me.grdResultList.DataSource = c_dtResults
        Me.grdResultList.DataBind()
    End Function

    Private Sub grdResultList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdResultList.PageIndexChanged
        Me.grdResultList.CurrentPageIndex = e.NewPageIndex
        RefreshGrid()
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item(Me.ID & "resulttable") = Me.c_dtResults
        Session.Item(Me.ID & "editinplace") = Me.c_blnEditInPlace

    End Sub

    Private Sub grdResultList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdResultList.ItemDataBound
        Dim chkDSQ As CheckBox
        Dim txtTextBox As TextBox
        Dim cmbCombobox As DropDownList

        If Not e.Item.ItemIndex = Me.grdResultList.EditItemIndex Then
            If e.Item.Cells(Me.enumResultListColumn.ResultDisqualified).Controls.Count > 0 Then
                Try
                    chkDSQ = e.Item.Cells(enumResultListColumn.ResultDisqualified).Controls(1)
                    If chkDSQ.Checked Then
                        e.Item.BackColor = Color.PeachPuff
                        e.Item.Cells(enumResultListColumn.ResultPosition).Text = "DSQ"
                    End If
                Catch ex As Exception

                End Try
            End If
        Else
            If Not e.Item.ItemIndex = -1 Then
                txtTextBox = e.Item.Cells(Me.enumResultListColumn.ResultPosition).Controls(0)
                txtTextBox.Width = New Web.UI.WebControls.Unit("50px")
                cmbCombobox = e.Item.Cells(Me.enumResultListColumn.ResultCompetitor).Controls(1)
                cmbCombobox.Width = New Web.UI.WebControls.Unit("300px")
                txtTextBox = e.Item.Cells(Me.enumResultListColumn.ResultTime).Controls(0)
                txtTextBox.Width = New Web.UI.WebControls.Unit("100px")
                txtTextBox = e.Item.Cells(Me.enumResultListColumn.ResultPoints).Controls(0)
                txtTextBox.Width = New Web.UI.WebControls.Unit("100px")
                txtTextBox = e.Item.Cells(Me.enumResultListColumn.ResultComment).Controls(0)
                txtTextBox.Width = New Web.UI.WebControls.Unit("200px")
            End If
        End If

    End Sub

    Private Sub grdResultList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdResultList.ItemCommand

        Select Case e.CommandName
            Case "event"
                c_intSelectedEvent = e.Item.Cells(enumResultListColumn.EventID).Text
                RaiseEvent EventSelected()
            Case "course"
                c_intSelectedCourse = e.Item.Cells(enumResultListColumn.CourseID).Text
                RaiseEvent CourseSelected()
        End Select
    End Sub

    Protected Sub cmdEdit_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem

        itm = sender.Parent.Parent

        If Me.c_blnEditInPlace Then
            grdResultList.EditItemIndex = itm.ItemIndex
            RefreshGrid()
        Else
            c_intSelectedCourse = CInt(itm.Cells(enumResultListColumn.CourseID).Text)
            c_intSelectedCompetitor = CInt(itm.Cells(enumResultListColumn.CompetitorID).Text)
            RaiseEvent ResultEdited()
        End If

    End Sub

    Protected Sub cmdUpdate_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim strSQL As String

        Dim intCompetitor As Integer
        Dim intNewCompetitor As Integer
        Dim intClub As Integer
        Dim intCategory As Integer
        Dim dteTime As Date
        Dim intPosition As Integer
        Dim intPoints As Integer
        Dim blnDisqualified As Boolean
        Dim strComment As String
        Dim intCourse As Integer

        Dim txtTextBox As TextBox
        Dim cmbComboBox As DropDownList
        Dim chkCheckBox As CheckBox
        Dim strMessage As String

        itm = sender.parent.parent
        intCompetitor = itm.Cells(Me.enumResultListColumn.CompetitorID).Text
        intCourse = itm.Cells(Me.enumResultListColumn.CourseID).Text

        cmbComboBox = itm.Cells(Me.enumResultListColumn.ResultCompetitor).Controls(1)
        intNewCompetitor = cmbComboBox.SelectedValue
        cmbComboBox = itm.Cells(Me.enumResultListColumn.ResultCategory).Controls(1)
        If IsNumeric(cmbComboBox.SelectedValue) Then
            intCategory = cmbComboBox.SelectedValue
        Else
            intCategory = NULL_NUMBER
        End If
        cmbComboBox = itm.Cells(Me.enumResultListColumn.ResultClub).Controls(1)
        If IsNumeric(cmbComboBox.SelectedValue) Then
            intClub = cmbComboBox.SelectedValue
        Else
            intClub = NULL_NUMBER
        End If
        txtTextBox = itm.Cells(Me.enumResultListColumn.ResultTime).Controls(0)
        dteTime = ParseTime(txtTextBox.Text)
        txtTextBox = itm.Cells(Me.enumResultListColumn.ResultPosition).Controls(0)
        If IsNumeric(txtTextBox.Text) Then
            intPosition = txtTextBox.Text
        Else
            strMessage = "Please provide a numeric value for Position."
        End If
        txtTextBox = itm.Cells(Me.enumResultListColumn.ResultPoints).Controls(0)
        If IsNumeric(txtTextBox.Text) Then
            intPoints = txtTextBox.Text
        Else
            intPoints = NULL_NUMBER
        End If
        chkCheckBox = itm.Cells(Me.enumResultListColumn.ResultDisqualified).Controls(1)
        blnDisqualified = chkCheckBox.Checked
        txtTextBox = itm.Cells(Me.enumResultListColumn.ResultComment).Controls(0)
        strComment = txtTextBox.Text

        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
        Else
            strSQL = "UPDATE tblResult SET " & _
                " intCompetitor = " & SQLFormat(intNewCompetitor) & _
                ", intCategory = " & SQLFormat(intCategory) & _
                ", intClub = " & SQLFormat(intClub) & _
                ", dteTime = " & SQLFormat(dteTime) & _
                ", intPosition = " & SQLFormat(intPosition) & _
                ", intPoints = " & SQLFormat(intPoints) & _
                ", blnDisqualified = " & SQLFormat(blnDisqualified) & _
                ", strComment = " & SQLFormat(strComment) & _
                " WHERE (intCompetitor = " & intCompetitor & ") AND (intCourse = " & intCourse & ")"

            ExecuteSQL(c_conDB, strSQL)
            Me.grdResultList.EditItemIndex = -1

            RaiseEvent ResultEdited()
        End If

    End Sub

    Protected Sub cmdCancel_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.grdResultList.EditItemIndex = -1
        RefreshGrid()
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem

        itm = sender.Parent.Parent
        c_intSelectedCourse = CInt(itm.Cells(enumResultListColumn.CourseID).Text)
        c_intSelectedCompetitor = CInt(itm.Cells(enumResultListColumn.CompetitorID).Text)
        RaiseEvent ResultDeleted()

    End Sub

    Public Function CompetitorTable() As DataTable
        Dim dtReturn As DataTable

        dtReturn = PenOCDB.GetTable_Competitor(c_conDB, "")

        Return dtReturn

    End Function

    Protected Function Categories() As DataTable
        Dim dtReturn As DataTable
        Dim drRow As DataRow

        dtReturn = g_lookupManager.GetLookupTable(c_conDB, LookupManager.enumLookupTable.Category)
        drRow = dtReturn.NewRow
        drRow.Item("value") = ""
        drRow.Item("text") = ""
        dtReturn.Rows.InsertAt(drRow, 0)

        Return dtReturn

    End Function

    Protected Function Clubs() As DataTable
        Dim dtReturn As DataTable
        Dim drRow As DataRow

        dtReturn = g_lookupManager.GetLookupTable(c_conDB, LookupManager.enumLookupTable.ClubShortName)
        drRow = dtReturn.NewRow
        drRow.Item("value") = ""
        drRow.Item("text") = ""
        dtReturn.Rows.InsertAt(drRow, 0)
        Return dtReturn

    End Function
End Class
