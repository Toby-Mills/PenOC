Partial Class CourseEdit
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
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
    Protected WithEvents CompetitorSelectResult As CompetitorSelect
    Protected WithEvents EventBrief As EventBrief
    Protected WithEvents ResultListCourse As ResultList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intCourse As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With EventBrief
                .DisplayEditButton = True
            End With
            With ResultListCourse
                .DisplayColumn(ResultList.enumResultListColumn.CourseClimb) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseControls) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLength) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLog) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseName) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseTechnical) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventDate) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventName) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventVenue) = False
                .AllowPaging = False
                .EditInPlace = True
            End With

            With CompetitorSelectResult
                .AutoPostBack = True
                .DisplayNew = True
                .DisplayEdit = True
            End With

            PopulateControls()
            Me.txtID.Width = New System.Web.UI.WebControls.Unit(0)
            Me.txtEventID.Width = New System.Web.UI.WebControls.Unit(0)
            intCourse = Request.Item("idcourse")
            If intCourse > 0 Then
                LoadCourse(intCourse)
            End If
            EnableEditing(True)
            EnableResultsEditing(False)
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub PopulateControls()
        Dim dtLog As DataTable
		Dim drLog As DataRow

        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbTechnical, LookupManager.enumLookupTable.Technical, False)
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbCategory, LookupManager.enumLookupTable.Category, False)
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbClub, LookupManager.enumLookupTable.ClubShortName, False)

		dtLog = GetTable_Log(c_conDB, "")
		drLog = dtLog.NewRow
		drLog.Item("idLog") = 0
		drLog.Item("Description") = ""
		dtLog.Rows.InsertAt(drLog, 0)
        With Me.cmbLog
            .DataSource = dtLog
            .DataValueField = "idLog"
            .DataTextField = "Description"
            .DataBind()
        End With
    End Sub

    Private Sub LoadCourse(ByVal intCourse As Integer)
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim strWHERE As String
        Dim intTechnical As Integer
        Dim intLog As Integer
        Dim intEvent As Integer
        Dim dtResults As DataTable

		strWHERE = PenOCDB.WhereCourse_idCourse(intCourse)
        dtCourse = PenOCDB.GetTable_Course(c_conDB, strWHERE)
        If dtCourse.Rows.Count > 0 Then
            drCourse = dtCourse.Rows(0)
            With drCourse
                Me.txtID.Text = intCourse
                intEvent = CInt(.Item("idEvent"))
                Me.txtEventID.Text = intEvent
                Me.txtCourseName.Text = .Item("CourseName").ToString
                Me.txtLength.Text = .Item("Length").ToString
                Me.txtClimb.Text = .Item("Climb").ToString
                Me.txtControls.Text = .Item("Controls").ToString
				LoadDBValue(.Item("idTechnical"), intTechnical)
				If Not intTechnical = NULL_NUMBER Then
					Me.cmbTechnical.SelectedValue = intTechnical
				Else
					Me.cmbTechnical.ClearSelection()
				End If
				LoadDBValue(.Item("idLog"), intLog)
				If Not intLog = NULL_NUMBER Then
					Me.cmbLog.SelectedValue = intLog
				Else
					Me.cmbLog.ClearSelection()
                End If

                Me.txtSplitsURL.Text = .Item("SplitsURL").ToString
			End With
		End If

        Me.EventBrief.LoadEvent(c_conDB, intEvent)

		strWHERE = PenOCDB.WhereResult_idCourse(intCourse)
        dtResults = PenOCDB.GetTable_Result(c_conDB, strWHERE)
        Me.ResultListCourse.ResultTable = dtResults

    End Sub

    Private Sub EnableEditing(ByVal blnEnable As Boolean)
        Me.txtCourseName.Enabled = blnEnable
        Me.txtLength.Enabled = blnEnable
        Me.txtClimb.Enabled = blnEnable
        Me.txtControls.Enabled = blnEnable
        Me.cmbTechnical.Enabled = blnEnable
        Me.cmbLog.Enabled = blnEnable
        Me.txtSplitsURL.Enabled = blnEnable

        Me.cmdEdit.Enabled = Not blnEnable
        Me.cmdCancel.Enabled = blnEnable
        Me.cmdSave.Enabled = blnEnable

        If blnEnable Then
            Me.pnlCourseDetails.Visible = True
        End If

    End Sub

    Private Sub EnableResultsEditing(ByVal blnEnable As Boolean)

        Me.pnlAddResult.Visible = blnEnable
        Me.ResultListCourse.DisplayColumn(ResultList.enumResultListColumn.Edit) = blnEnable
        Me.ResultListCourse.DisplayColumn(ResultList.enumResultListColumn.Delete) = blnEnable

        Me.cmdStartEditResults.Enabled = Not blnEnable
        Me.cmdCalculatePoints.Enabled = blnEnable
        Me.cmdEndEditingResults.Enabled = blnEnable

        Me.pnlCourseDetails.Visible = Not blnEnable

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.LoadCourse(Me.txtID.Text)
        EnableEditing(False)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim blnSave As Boolean
        Dim intCourse As Integer

        blnSave = ValidateCourse()

        If blnSave Then
            If Me.txtID.Text > "" Then
                intCourse = Me.txtID.Text
            Else
				intCourse = PenOCDB.SaveCourse(c_conDB, -1, Me.txtEventID.Text, Me.txtCourseName.Text, 0, 0, 0, 0, 0)
            End If
            SaveCourse(intCourse)
            LoadCourse(intCourse)
            EnableEditing(False)
        End If
    End Sub

    Private Sub SaveCourse(ByVal intCourse As Integer)
        Dim strSQL As String
        Dim intLength As Integer
        Dim intClimb As Integer
        Dim intControls As Integer
        Dim intTechnical As Integer
        Dim intLog As Integer

        If IsNumeric(Me.txtLength.Text) Then
            intLength = CInt(Me.txtLength.Text)
        Else
            intLength = NULL_NUMBER
        End If
        If IsNumeric(Me.txtClimb.Text) Then
            intClimb = CInt(Me.txtClimb.Text)
        Else
            intClimb = NULL_NUMBER
        End If
        If IsNumeric(Me.txtControls.Text) Then
            intControls = CInt(Me.txtControls.Text)
        Else
            intControls = NULL_NUMBER
        End If

        intTechnical = Me.cmbTechnical.SelectedValue
        intLog = Me.cmbLog.SelectedValue

        If intLog = 0 Then
            intLog = NULL_NUMBER
        End If

        strSQL = "UPDATE tblCourse SET " & _
            "strName = " & SQLFormat(Me.txtCourseName.Text) & _
            ", intLength = " & SQLFormat(intLength) & _
            ", intClimb = " & SQLFormat(intClimb) & _
            ", intControls = " & SQLFormat(intControls) & _
            ", intTechnical = " & SQLFormat(intTechnical) & _
            ", intLog = " & SQLFormat(intLog) & _
            ", strSplitsURL = " & SQLFormat(Me.txtSplitsURL.Text) & _
            " WHERE idCourse = " & intCourse

        ExecuteSQL(c_conDB, strSQL)

    End Sub

    Private Function ValidateCourse() As Boolean
        Dim strMessage As String
        Dim blnReturn As Boolean

        strMessage = ""
        Select Case True
            Case Me.txtCourseName.Text = ""
                strMessage = "Please enter a Course Name."
        End Select

        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
            blnReturn = False
        Else
            blnReturn = True
        End If

        Return blnReturn

    End Function

    Private Function ValidateAddResult() As Boolean
        Dim strMessage As String
        Dim blnReturn As Boolean

        Select Case True
            Case Me.CompetitorSelectResult.Selected <= 0
                strMessage = "Please select a competitor to add."
            Case Not IsNumeric(Me.txtPosition.Text)
                strMessage = "Please provide a numeric value for Position."
        End Select

        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
            blnReturn = False
        Else
            blnReturn = True
        End If

        Return blnReturn

    End Function
    Private Sub cmdAddResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddResult.Click
        Dim strSQL As String

        Dim intCompetitor As Integer
        Dim intClub As Integer
        Dim intCategory As Integer
        Dim dteTime As Date
        Dim intPosition As Integer
        Dim intPoints As Integer
        Dim blnDisqualified As Boolean

        Dim strComment As String
        Dim intCourse As Integer

        Dim blnSave As Boolean

        blnSave = Me.ValidateAddResult
        If blnSave Then
            intCompetitor = Me.CompetitorSelectResult.Selected
            intCourse = Me.txtID.Text

            intCategory = Me.cmbCategory.SelectedValue
            intClub = Me.cmbClub.SelectedValue
            dteTime = ParseTime(Me.txtTime.Text)
            intPosition = Me.txtPosition.Text
            If IsNumeric(Me.txtPoints.Text) Then
                intPoints = CInt(Me.txtPoints.Text)
            Else
                intPoints = NULL_NUMBER
            End If
            blnDisqualified = Me.chkDisqualified.Checked
            strComment = Me.txtComment.Text

            Try
				PenOCDB.NewResult(c_conDB, intCourse, intPosition, intCompetitor, intCategory, intClub, dteTime, intPoints, blnDisqualified, strComment)
            Catch ex As Exception
                WebMsgBox(Page, "Error", "An error occurred adding this result: " & Replace(ex.Message, "'", ""))
            End Try

            Me.LoadCourse(intCourse)

            txtPosition.Text = intPosition + 1

            CompetitorSelectResult.ClearSelection()
            txtTime.Text = ""
            txtPoints.Text = ""
            cmbCategory.ClearSelection()
            cmbClub.ClearSelection()
            txtComment.Text = ""
        End If


    End Sub

    Private Sub ResultListCourse_ResultDeleted() Handles ResultListCourse.ResultDeleted
        PenOCDB.DeleteResult(c_conDB, Me.ResultListCourse.SelectedCourse, Me.ResultListCourse.SelectedCompetitor)
        Me.LoadCourse(Me.txtID.Text)
    End Sub

    Private Sub cmdStartEditResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartEditResults.Click
        LoadCourse(Me.txtID.Text)
        EnableResultsEditing(True)
        EnableEditing(False)
    End Sub

    Private Sub cmdEndEditingResults_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEndEditingResults.Click
        EnableResultsEditing(False)
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        LoadCourse(Me.txtID.Text)
        EnableEditing(True)
        EnableResultsEditing(False)
    End Sub

    Private Sub ResultListCourse_ResultEdited() Handles ResultListCourse.ResultEdited
        LoadCourse(Me.txtID.Text)
    End Sub

    Private Sub CompetitorSelectResult_NewCompetitor() Handles CompetitorSelectResult.NewCompetitor

        Session.Item(CompetitorEdit.RETURN_URL) = CompetitorEdit.AUTOCLOSE
        OpenPopUp(Page, "CompetitorEdit.aspx", "NewCompetitor")

    End Sub

    Private Sub CompetitorSelectResult_EditCompetitor() Handles CompetitorSelectResult.EditCompetitor

        Session.Item(CompetitorEdit.RETURN_URL) = CompetitorEdit.AUTOCLOSE
        OpenPopUp(Page, "CompetitorEdit.aspx?idcompetitor=" & CompetitorSelectResult.Selected, "EditCompetitor")

    End Sub

    Private Sub CompetitorSelectResult_SelectionChanged() Handles CompetitorSelectResult.SelectionChanged
        Dim intCompetitor As Integer
        Dim intCategory As Integer
        Dim intClub As Integer

        intCompetitor = CompetitorSelectResult.Selected
        intCategory = CompetitorCurrentCategory(c_conDB, intCompetitor)
        intClub = CompetitorCurrentClub(c_conDB, intCompetitor)

        Me.cmbCategory.SelectedValue = intCategory
        Me.cmbClub.SelectedValue = intClub

    End Sub

    Private Sub cmdCalculatePoints_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalculatePoints.Click
        Dim intCourse As Integer

        intCourse = Me.txtID.Text
        AutoCalculateLogPoints(c_conDB, intCourse)
        LoadCourse(intCourse)

    End Sub

    Private Sub EventBrief_Edit() Handles EventBrief.Edit
		Server.Transfer("EventEdit.aspx?idevent=" & Me.txtEventID.Text)
    End Sub
End Class
