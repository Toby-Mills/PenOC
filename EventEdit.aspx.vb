Partial Class EventEdit
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

    Protected WithEvents CourseList As CourseList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intEvent As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            'Me.txtID.Width = New System.Web.UI.WebControls.Unit(0)
            Me.txtPlannerID.Width = New System.Web.UI.WebControls.Unit(0)
            Me.txtControllerID.Width = New System.Web.UI.WebControls.Unit(0)

            PopulateControls()
            CalendarPopup.ShowCalendar(Me.cmdCalEventDate, Me.txtDate, g_strDateFormat, False)
            CompetitorSearchPopup.ShowCompetitorSearch(Me.cmdPlannerSearch, Me.txtPlannerID, Me.txtPlannerName)
            CompetitorSearchPopup.ShowCompetitorSearch(Me.cmdControllerSearch, Me.txtControllerID, Me.txtControllerName)

            With Me.CourseList
                .Selectable = True
                .DisplayColumn(CourseList.enumCourseListColumn.Delete) = True
                .DisplayColumn(CourseList.enumCourseListColumn.Competitors) = False
                .DisplayColumn(CourseList.enumCourseListColumn.EventDate) = False
                .DisplayColumn(CourseList.enumCourseListColumn.EventName) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Venue) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Winner) = False
                .DisplayColumn(CourseList.enumCourseListColumn.WinningTime) = False
            End With
            intEvent = Request.Item("idevent")
            If intEvent > 0 Then
                LoadEvent(intEvent)
            End If
            EnableEditing(True)
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub PopulateControls()
        Dim dtCompetitor As DataTable
        Dim drCompetitor As DataRow
        Dim strWHERE As String

        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbVenue, LookupManager.enumLookupTable.Venue, False)
        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbClub, LookupManager.enumLookupTable.ClubShortName, False)
        g_lookupManager.PopulateWebCheckBoxList(c_conDB, Me.cblEventType, LookupManager.enumLookupTable.EventType)

		strWHERE = PenOCDB.WhereCompetitor_Individual
		strWHERE = strWHERE & " OR " & PenOCDB.WhereCompetitor_Organiser

        dtCompetitor = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)
        drCompetitor = dtCompetitor.NewRow()
        drCompetitor.Item("idCompetitor") = 0
        dtCompetitor.Rows.InsertAt(drCompetitor, 0)

        cmdAutoNotice.Attributes("language") = "vbscript"
        cmdAutoNotice.Attributes("onclick") = "txtNotice.value=""auto""" & vbCrLf & "window.event.returnValue = false"

        cmdAutoResults.Attributes("language") = "vbscript"
        cmdAutoResults.Attributes("onclick") = "txtResults.value=""auto""" & vbCrLf & "window.event.returnValue = false"

        cmdAutoPhotos.Attributes("language") = "vbscript"
        cmdAutoPhotos.Attributes("onclick") = "txtPhotos.value=""auto""" & vbCrLf & "window.event.returnValue = false"

    End Sub

    Private Sub LoadEvent(ByVal intEvent As Integer)
        Dim dtEvent As DataTable
        Dim drEvent As DataRow
        Dim dtCourse As DataTable
        Dim strWHERE As String
        Dim dteDate As Date
        Dim intVenue As Integer
        Dim intClub As Integer
        Dim intPlanner As Integer
        Dim intController As Integer
        Dim intMaxLogPoints As Integer
        Dim dtEventType As DataTable
        Dim drEventType As DataRow
        Dim intIndex As Integer
        Dim strReport As String

        If intEvent > 0 Then
			strWHERE = PenOCDB.WhereEvent_idEvent(intEvent)
            dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, "")
            If dtEvent.Rows.Count > 0 Then
                drEvent = dtEvent.Rows(0)
                With drEvent
                    'Event ID
                    Me.txtID.Text = .Item("idEvent")
                    'Event Name
                    Me.txtEventName.Text = .Item("EventName").ToString
                    'Date
                    LoadDBValue(.Item("dteDate"), dteDate)
                    If Not dteDate = Date.MinValue Then
                        Me.txtDate.Text = dteDate.ToString(g_strDateFormat)
                    Else
                        Me.txtDate.Text = ""
                    End If
                    'Venue
                    If Not TypeOf .Item("idVenue") Is DBNull Then
                        Me.cmbVenue.SelectedValue = .Item("idVenue")
                    Else
                        Me.cmbVenue.ClearSelection()
                    End If
                    'Club
                    If Not TypeOf .Item("idClub") Is DBNull Then
                        Me.cmbClub.SelectedValue = .Item("idClub")
                    Else
                        Me.cmbClub.ClearSelection()
                    End If
                    'Planner
                    If Not TypeOf .Item("idPlanner") Is DBNull Then
						Me.txtPlannerID.Text = .Item("idPlanner")
						Me.txtPlannerName.Text = PenOCDB.LookupCompetitor_FullName(c_conDB, .Item("idPlanner"))
                    Else
						Me.txtPlannerID.Text = ""
						Me.txtPlannerName.Text = ""
                    End If
                    'Controller
                    If Not TypeOf .Item("idController") Is DBNull Then
						Me.txtControllerID.Text = .Item("idController")
						Me.txtControllerName.Text = PenOCDB.LookupCompetitor_FullName(c_conDB, .Item("idController"))
					Else
						Me.txtControllerID.Text = ""
						Me.txtControllerName.Text = ""
					End If
					'Max Log Points
					If Not TypeOf .Item("MaxPoints") Is DBNull Then
						Me.txtMaxLogPoints.Text = .Item("MaxPoints")
					Else
						Me.txtMaxLogPoints.Text = 0
					End If
					'Directions
					Me.txtDirections.Text = drEvent.Item("Directions").ToString
					'Notice URL
					Me.txtNotice.Text = drEvent.Item("Notice").ToString
					'Results URL
					Me.txtResults.Text = drEvent.Item("Results").ToString
					'Photos URL
					Me.txtPhotos.Text = drEvent.Item("Photos").ToString
					'Registration
					Me.txtRegistration.Text = drEvent.Item("Registration").ToString
					'Starts
					Me.txtStarts.Text = drEvent.Item("Starts").ToString
					'Courses Close
					Me.txtClose.Text = drEvent.Item("Close").ToString
					'Courses
					Me.txtCourses.Text = drEvent.Item("Courses").ToString
					'Cost
					Me.txtCost.Text = drEvent.Item("Cost").ToString
					'Special Note
					Me.txtSpecialNote.Text = drEvent.Item("SpecialNote").ToString
					'Report
                    Me.txtReport.Text = LookupEvent_Report(c_conDB, intEvent)
                    'Permalinks
                    Me.lblNoticePermaLink.Text = "http://www.penoc.org.za/Hook.aspx?" & Hook.URL_HOOK_TYPE & "=" & Hook.URL_HOOK_TYPE_EVENT_NOTICE & "&" & Hook.URL_HOOK_ID & "=" & .Item("idEvent")
                    Me.lblResultsPermaLink.Text = "http://www.penoc.org.za/Hook.aspx?" & Hook.URL_HOOK_TYPE & "=" & Hook.URL_HOOK_TYPE_EVENT_RESULTS & "&" & Hook.URL_HOOK_ID & "=" & .Item("idEvent")
				End With
            End If

			dtEventType = PenOCDB.GetTable_EventType(c_conDB, intEvent)
            For Each drEventType In dtEventType.Rows
                intIndex = GetIndex(Me.cblEventType, drEventType.Item("idEventType"))
                Me.cblEventType.Items(intIndex).Selected = True
            Next

			strWHERE = PenOCDB.WhereCourse_idEvent(intEvent)
            dtCourse = PenOCDB.GetTable_Course(c_conDB, strWHERE)
        Me.CourseList.CourseTable = dtCourse
        Else 'Clear the form
            'Event ID
            Me.txtID.Text = ""
            'Event Name
            Me.txtEventName.Text = ""
            'Date
            Me.txtDate.Text = ""
            'Venue
            Me.cmbVenue.ClearSelection()
            'Club
            Me.cmbClub.ClearSelection()
            'Planner
			Me.txtPlannerID.Text = ""
			Me.txtPlannerName.Text = ""
            'Controller
			Me.txtControllerID.Text = ""
			Me.txtControllerName.Text = ""
            'Max Log Points
            Me.txtMaxLogPoints.Text = 0
            'Directions
            Me.txtDirections.Text = ""
            'Notice URL
            Me.txtNotice.Text = ""
            'Results URL
            Me.txtResults.Text = ""
            'Photos URL
            Me.txtPhotos.Text = ""
            'Registration
            Me.txtRegistration.Text = ""
            'Starts
            Me.txtStarts.Text = ""
            'Courses Close
            Me.txtClose.Text = ""
            'Courses
            Me.txtCourses.Text = ""
            'Cost
            Me.txtCost.Text = ""
            'Special Note
            Me.txtSpecialNote.Text = ""
            'Report
            Me.txtReport.Text = ""
            'Event Type
            Me.cblEventType.ClearSelection()
            'Course List
            Me.CourseList.CourseTable = Nothing
        End If

    End Sub

    Private Function EnableEditing(ByVal blnEnable As Boolean)

        Me.txtEventName.Enabled = blnEnable
        Me.txtDate.Enabled = blnEnable
        Me.txtCourses.Enabled = blnEnable
        Me.txtCost.Enabled = blnEnable
        Me.cblEventType.Enabled = blnEnable
        Me.txtSpecialNote.Enabled = blnEnable
        Me.txtRegistration.Enabled = blnEnable
        Me.txtStarts.Enabled = blnEnable
        Me.txtClose.Enabled = blnEnable
        Me.cmdCalEventDate.Enabled = blnEnable
        Me.cmbVenue.Enabled = blnEnable
		Me.cmdPlannerSearch.Enabled = blnEnable
		Me.cmdControllerSearch.Enabled = blnEnable
        Me.cmbClub.Enabled = blnEnable
        Me.txtMaxLogPoints.Enabled = blnEnable
        Me.txtNotice.Enabled = blnEnable
        Me.txtResults.Enabled = blnEnable
        Me.txtPhotos.Enabled = blnEnable
        Me.txtDirections.Enabled = blnEnable
        Me.cmdAutoNotice.Enabled = blnEnable
        Me.cmdAutoResults.Enabled = blnEnable
        Me.cmdAutoPhotos.Enabled = blnEnable

        Me.cmdNewCourse.Enabled = Not blnEnable

        Me.cmdEdit.Enabled = Not blnEnable
        Me.cmdSave.Enabled = blnEnable
        Me.cmdCancel.Enabled = blnEnable

    End Function

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        EnableEditing(True)
    End Sub

    Private Sub CourseListEvent_CourseSelected() Handles CourseList.CourseSelected

		Server.Transfer("CourseEdit.aspx?idcourse=" & CourseList.SelectedCourse)

    End Sub

    Private Sub CourseListEvent_CourseDeleted() Handles CourseList.CourseDeleted

		ConfirmDelete.PopupConfirmDelete(Page, ConfirmDelete.enumDeleteType.Course, CourseList.SelectedCourse)

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intEvent As Integer
        Dim blnSave As Boolean

        blnSave = ValidateEvent()
        If blnSave Then
            If Me.txtID.Text = "" Then
                intEvent = PenOCDB.NewEvent(c_conDB, Me.txtEventName.Text)
            Else
                intEvent = Me.txtID.Text
            End If

            If SaveEvent(intEvent) Then
                LoadEvent(intEvent)
                EnableEditing(False)
            Else
                WebMsgBox(Page, "Error", "Failed to Save")
            End If

        End If

    End Sub

    Private Function ValidateEvent() As Boolean
        Dim strMessage As String
        Dim blnReturn As Boolean

        blnReturn = True

        Select Case True
            Case Me.txtEventName.Text = ""
                strMessage = "Please enter an Event Name"
        End Select

        Try
            Date.ParseExact(Me.txtDate.Text, g_strDateFormat, Nothing)
        Catch ex As Exception
            strMessage = "Date format should be: " & g_strDateFormat
        End Try

        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
            blnReturn = False
        End If

        Return blnReturn

    End Function

    Private Function SaveEvent(ByVal intEvent As Integer) As Boolean

        Dim strSQL As String
        Dim strName As String
        Dim dteDate As Date
        Dim intVenue As Integer
        Dim intPlanner As Integer
        Dim intController As Integer
        Dim intClub As Integer
        Dim intMaxPoints As Integer
        Dim strNotice As String
        Dim strResults As String
        Dim strPhotos As String
        Dim strRegistration As String
        Dim strStarts As String
        Dim strClose As String
        Dim strCourses As String
        Dim strCost As String
        Dim strSpecialNote As String
        Dim strReport As String
        Dim strDirections As String
        Dim itm As ListItem
        Dim blnReturn As Boolean

        blnReturn = True

		'Event Name
		strName = Me.txtEventName.Text

		'Date
        If IsDate(Me.txtDate.Text) Then
            dteDate = Date.ParseExact(Me.txtDate.Text, g_strDateFormat, Nothing)
        Else
            dteDate = Date.MinValue
        End If

		'Venue
		intVenue = Me.cmbVenue.SelectedValue

		'Planner
		If IsNumeric(txtPlannerID.Text) Then
			intPlanner = txtPlannerID.Text
		Else
			intPlanner = NULL_NUMBER
		End If

		'Controller
		If IsNumeric(txtControllerID.Text) Then
			intController = txtControllerID.Text
		Else
			intController = NULL_NUMBER
		End If

        'Club
        intClub = Me.cmbClub.SelectedValue

		'Log Points
		If IsNumeric(Me.txtMaxLogPoints.Text) Then
			intMaxPoints = CInt(Me.txtMaxLogPoints.Text)
		Else
			intMaxPoints = 0
		End If

		strNotice = Me.txtNotice.Text
		strResults = Me.txtResults.Text
		strPhotos = Me.txtPhotos.Text
		strRegistration = Me.txtRegistration.Text
		strStarts = Me.txtStarts.Text
		strClose = Me.txtClose.Text
		strCourses = Me.txtCourses.Text
		strCost = Me.txtCost.Text
		strSpecialNote = Me.txtSpecialNote.Text
		strReport = Me.txtReport.Text
		strDirections = Me.txtDirections.Text

        strSQL = "UPDATE tblEvent SET " & _
            " strName = " & SQLFormat(strName) & _
            ", dteDate = " & SQLFormat(dteDate) & _
            ", intVenue = " & SQLFormat(intVenue) & _
            ", intPlanner = " & SQLFormat(intPlanner) & _
            ", intController = " & SQLFormat(intController) & _
            ", intOrganisingClub = " & SQLFormat(intClub) & _
            ", intMaxPoints = " & SQLFormat(intMaxPoints) & _
            ", strNotice = " & SQLFormat(strNotice) & _
            ", strResults = " & SQLFormat(strResults) & _
            ", strPhotos = " & SQLFormat(strPhotos) & _
            ", strRegTime = " & SQLFormat(strRegistration) & _
            ", strStarts = " & SQLFormat(strStarts) & _
            ", strClose = " & SQLFormat(strClose) & _
            ", strCourses = " & SQLFormat(strCourses) & _
            ", strCost = " & SQLFormat(strCost) & _
            ", strSpecialNote = " & SQLFormat(strSpecialNote) & _
            ", strPlannerReport = " & SQLFormat(strReport) & _
            ", strDirections = " & SQLFormat(strDirections) & _
            " WHERE idEvent = " & intEvent
		Try
			ExecuteSQL(c_conDB, strSQL)
		Catch ex As Exception
			blnReturn = False
		End Try

		If blnReturn = True Then
			DeleteDataRows(c_conDB, "tblEvent_EventType", "intEvent", intEvent)
			For Each itm In Me.cblEventType.Items
				If itm.Selected Then
					strSQL = "INSERT INTO tblEvent_EventType (intEvent, intEventType)" & _
					 " VALUES (" & intEvent & ", " & itm.Value & ")"
					Try
						ExecuteSQL(c_conDB, strSQL)
					Catch ex As Exception
						blnReturn = False
					End Try
				End If
			Next
		End If

		Return blnReturn

    End Function

    Private Sub cmdNewCourse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewCourse.Click
        Dim intCourse As Integer

		intCourse = SaveCourse(c_conDB, -1, Me.txtID.Text, "New Course", 0, 0, 0, 0, 0)

		Server.Transfer("CourseEdit.aspx?idcourse=" & intCourse)

    End Sub

	Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Dim intEvent As Integer

		If Me.txtID.Text = "" Then
			LoadEvent(0)
		Else
			intEvent = Me.txtID.Text
			LoadEvent(intEvent)
			EnableEditing(False)
		End If

	End Sub

	Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click
		Server.Transfer("ImportResults.aspx?idevent=" & Me.txtID.Text)
	End Sub

	Private Sub cmdCalculateLogPoints_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculateLogPoints.Click
		AutoCalculateEventLogPoints(c_conDB, Me.txtID.Text)
		WebMsgBox(Page, "Log", "Log Points Calculated")
	End Sub

    Private Sub cmdClearPlanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearPlanner.Click
        Me.txtPlannerID.Text = ""
        Me.txtPlannerName.Text = ""
    End Sub

    Private Sub cmdClearController_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearController.Click
        Me.txtControllerID.Text = ""
        Me.txtControllerName.Text = ""
    End Sub

End Class
