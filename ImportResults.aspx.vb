Partial Class ImportResults
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
	Protected WithEvents CourseListPreview As CourseList
	Protected WithEvents ResultListPreview As ResultList
    Protected WithEvents cmdSearchEvent As System.Web.UI.WebControls.LinkButton

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

	Private c_dtImport() As DataTable	  'Array of tables of results read from spreadsheet
	'First table is a list of courses, the remaining tables are course results
	Private c_dtNewCompetitor As DataTable	'Temp table of competitors not yet in the database
	Private c_dtCourseList As DataTable	'Table of courses to be saved
	Private c_dtResult() As DataTable	'Array of tables of results to be saved

	Private Enum enumImportCourseColumn
		Order = 0
		CourseName = 1
		Length = 2
		Climb = 3
		Controls = 4
		Technical = 5
	End Enum

	Private Enum enumImportResultsColumn
		Position = 0
		Surname = 1
		FirstName = 2
		Category = 3
		Club = 4
		Time = 5
		Points = 6
		DSQ = 7
        Comments = 8
        RaceNumber = 9
	End Enum

	Private Enum NewCompetitorGridColumn
		idNewCompetitor = 0
		idCompetitor = 1
		Add = 2
		FirstName = 3
		Surname = 4
        idGender = 5
        strGender = 6
        BestMatch = 7
        idBestMatch = 8
        MoreSuggestions = 9
	End Enum

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If

            Me.txtEventID.Text = Request.Item("idEvent")
            If txtEventID.Text > "" Then
                Me.txtEvent.Text = PenOCDB.LookupEvent_Name(c_conDB, txtEventID.Text)
            End If

            Me.CourseListPreview.Selectable = True
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.EventName) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.EventDate) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.Venue) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.Log) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.Winner) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.WinningTime) = False
            Me.CourseListPreview.DisplayColumn(CourseList.enumCourseListColumn.Competitors) = False

            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseClimb) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseControls) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseLength) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseLog) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseName) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.CourseTechnical) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.EventDate) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.EventVenue) = False
            Me.ResultListPreview.DisplayColumn(ResultList.enumResultListColumn.EventName) = False
            Me.ResultListPreview.PageSize = -1
            ShowPanel("upload")
        Else
            c_dtImport = Session.Item("dtImport")
            c_dtNewCompetitor = Session.Item("dtNewCompetitor")
            c_dtCourseList = Session.Item("dtCourseList")
            c_dtResult = Session.Item("dtResult")
        End If

        Me.txtCompetitorID.Width = New Web.UI.WebControls.Unit(0)
        Me.txtNewCompetitorID.Width = New Web.UI.WebControls.Unit(0)
        Me.txtEventID.Width = New Web.UI.WebControls.Unit(0)

        EventSearchPopup.ShowEventSearch(Me.cmdEventSearch, Me.txtEventID, Me.txtEvent, True, "Select event to upload results for", False)
        ShowHourGlass(Page)

	End Sub

	Private Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click

		Try
			Saveworkbook()
			ReadWorkbook()
			ValidateCourses()
			ValidateResults()
			MatchCompetitors()
			If Not c_dtNewCompetitor Is Nothing Then
				If c_dtNewCompetitor.Rows.Count > 0 Then
					ShowPanel("newcompetitor")
				Else
					ShowPanel("preview")
				End If
			Else
				ShowPanel("preview")
			End If

		Catch ex As Exception
			WebMsgBox(Page, "Import Error", "An error occurred during the import: " & ex.Message)
		End Try

	End Sub

	Private Sub Saveworkbook()
		Dim fileResults As System.Web.HttpPostedFile
		Dim strPath As String

		If PathExists(Server.MapPath("temp"), True) Then
			strPath = Server.MapPath("temp") & "\results.xls"

			fileResults = Me.fileUpload.PostedFile()
			fileResults.SaveAs(strPath)
		End If

	End Sub

	Private Sub ReadWorkbook()
		Dim conWorkBook As System.Data.OleDb.OleDbConnection
		Dim strSQL As String
		Dim strPath As String
		Dim drCourse As DataRow
		Dim intCourseNumber As Integer

		strPath = Server.MapPath("temp") & "\results.xls"

		conWorkBook = New System.Data.OleDb.OleDbConnection
        conWorkBook.ConnectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" & strPath & ";Extended Properties=Excel 8.0;"

		strSQL = "SELECT * FROM [Courses$]"
		ReDim c_dtImport(0)
		c_dtImport(0) = GetDataTable(conWorkBook, strSQL)

		intCourseNumber = 1
		Try
			For Each drCourse In c_dtImport(0).Rows
				ReDim Preserve c_dtImport(intCourseNumber)
				strSQL = "SELECT * FROM [Course" & intCourseNumber & "$]"
				c_dtImport(intCourseNumber) = GetDataTable(conWorkBook, strSQL)
				intCourseNumber += 1
			Next
		Catch ex As Exception
            WebMsgBox(Page, "Import Error", "Error reading results for course: " & drCourse.Item("Course") & ". These should be in a Worksheet named 'Course" & intCourseNumber & "'")
		End Try

		conWorkBook.Close()

	End Sub

	Private Function ValidateCourses()
		Dim drCourse As DataRow
		Dim strReturn As String
		Dim intOrder As Integer
		Dim strName As String
		Dim intLength As Integer
		Dim intClimb As Integer
		Dim intControls As Integer
		Dim strTechnical As String
		Dim intTechnical As Integer
		Dim blnReturn As Boolean

		If Not c_dtImport(0).Columns.Contains("idTechnical") Then
			c_dtImport(0).Columns.Add("idTechnical", GetType(Integer))
		End If

		If c_dtImport(0).Rows.Count > 0 Then
			For Each drCourse In c_dtImport(0).Rows
				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.Order), intOrder)
					If intOrder < 0 Then
						Throw New Exception
					End If
				Catch ex As Exception
					Throw New Exception("Invalid course ORDER NUMBER", ex)
				End Try

				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.CourseName), strName)
					If strName = "" Then
						Throw New Exception
					End If
				Catch ex As Exception
					Throw New Exception("Invalid course NAME for course " & intOrder, ex)
				End Try

				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.Length), intLength)
                    If intLength < 0 And intLength <> NULL_NUMBER Then
                        Throw New Exception
                    End If
                Catch ex As Exception
					Throw New Exception("Invalid course LENGTH for course " & intOrder, ex)
				End Try

				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.Climb), intClimb)
                    If intClimb < 0 And intLength <> NULL_NUMBER Then
                        Throw New Exception
                    End If
                Catch ex As Exception
					Throw New Exception("Invalid course CLIMB for course " & intOrder, ex)
				End Try

				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.Controls), intControls)
                    If intControls < 0 And intControls <> NULL_NUMBER Then
                        Throw New Exception
                    End If
                Catch ex As Exception
					Throw New Exception("Invalid course CONTROLS for course " & intOrder, ex)
				End Try

				Try
					LoadDBValue(drCourse.Item(enumImportCourseColumn.Technical), strTechnical)
					intTechnical = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Technical_Code, drCourse.Item(Me.enumImportCourseColumn.Technical))
					drCourse.Item("idTechnical") = intTechnical
					If intTechnical = 0 Then
						Throw New Exception
					End If
				Catch ex As Exception
					Throw New Exception("Invalid course DIFFICULTY for course " & intOrder, ex)
				End Try
			Next
		Else
			strReturn = "No courses were listed in the 'Courses' sheet."
		End If

		Return strReturn

	End Function

	Private Function ValidateResults()
		Dim intCourse As Integer
		Dim dtResult As DataTable
		Dim drResult As DataRow
		Dim intPosition As Integer
		Dim strSurname As String
		Dim strFirstName As String
		Dim intCompetitor As Integer
		Dim strCategory As String
		Dim intCategory As Integer
		Dim strClub As String
		Dim intClub As Integer
		Dim dteTime As DateTime
		Dim intPoints As Integer
		Dim strDSQ As String
		Dim blnDSQ As Boolean
		Dim strComment As String

		For intCourse = 1 To UBound(c_dtImport)
			dtResult = c_dtImport(intCourse)

			If Not dtResult.Columns.Contains("idCategory") Then
				dtResult.Columns.Add("idCategory", GetType(Integer))
			End If

			If Not dtResult.Columns.Contains("idClub") Then
				dtResult.Columns.Add("idClub", GetType(Integer))
			End If

            For Each drResult In dtResult.Rows
                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Position), intPosition)
                    If intPosition < 0 Then
                        Throw New Exception
                    End If
                Catch ex As Exception
                    'Throw New Exception("Invalid result POSITION NUMBER in Course " & intCourse, ex)
                    Exit For
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Category), strCategory)
                    If strCategory > "" Then
                        intCategory = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Category_Code, strCategory)
                        If intCategory = 0 Then
                            Throw New Exception
                        End If
                        drResult.Item("idCategory") = intCategory
                    Else
                        intCategory = NULL_NUMBER
                        drResult.Item("idCategory") = intCategory
                    End If
                Catch ex As Exception
                    Throw New Exception("Invalid result CATEGORY '" & strCategory & "' for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Surname), strSurname)
                Catch ex As Exception
                    Throw New Exception("Invalid result SURNAME/GROUP NAME for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                If strSurname = "" Then
                    Throw New Exception("SURNAME/GROUP NAME for position " & intPosition & " in Course " & intCourse & " may not be blank.")
                End If

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.FirstName), strFirstName)

                Catch ex As Exception
                    Throw New Exception("Invalid result FIRST NAME for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                If strFirstName > "" And intCategory = LookupManager.enumCategory.Group Then
                    Throw New Exception("A GROUP may not have a FIRST NAME -  position " & intPosition & " in Course " & intCourse)
                End If

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Club), strClub)
                    If strClub > "" Then
                        intClub = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Club_Code, strClub)
                        If intClub = 0 Then
                            Throw New Exception
                        End If
                        drResult.Item("idClub") = intClub
                    End If
                Catch ex As Exception
                    Throw New Exception("Invalid result CLUB '" & strClub & "' for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Time), dteTime)
                Catch ex As Exception
                    Throw New Exception("Invalid result TIME for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Points), intPoints)
                Catch ex As Exception
                    Throw New Exception("Invalid result POINTS for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.DSQ), blnDSQ)
                Catch ex As Exception
                    Throw New Exception("Invalid result DSQ for position " & intPosition & " in Course " & intCourse, ex)
                End Try

                Try
                    LoadDBValue(drResult.Item(Me.enumImportResultsColumn.Comments), strComment)
                Catch ex As Exception
                    Throw New Exception("Invalid result COMMENT for position " & intPosition & " in Course " & intCourse, ex)
                End Try

            Next
        Next

	End Function

	Private Function MatchCompetitors() As Integer
		Dim intCourse As Integer
		Dim dtResult As DataTable
		Dim drResult As DataRow
		Dim dtCompetitor As DataTable
		Dim drCompetitor As DataRow
		Dim intCategory As Integer
		Dim strSurname As String
		Dim strFirstName As String
        Dim strWHERE As String
        Dim strORDER As String
		Dim intNewCompetitor As Integer
        Dim drNewCompetitor As DataRow
        Dim drMatchCompetitor As DataRow
        Dim intPosition As Integer

		For intCourse = 1 To UBound(c_dtImport)
			dtResult = c_dtImport(intCourse)

			If Not dtResult.Columns.Contains("idCompetitor") Then
                dtResult.Columns.Add("idCompetitor", GetType(Integer))
            End If

            For Each drResult In dtResult.Rows

                Try
                    LoadDBValue(drResult.Item(enumImportResultsColumn.Position), intPosition)
                    If intPosition = NULL_NUMBER Then
                        Exit For
                    End If
                Catch ex As Exception
                    Exit For
                End Try

                LoadDBValue(drResult.Item(enumImportResultsColumn.FirstName), strFirstName)
                LoadDBValue(drResult.Item(enumImportResultsColumn.Surname), strSurname)
                LoadDBValue(drResult.Item("idCategory"), intCategory)
                strWHERE = WhereCompetitor_Surname(strSurname)
                strWHERE &= " AND " & WhereCompetitor_FirstName(strFirstName)
                dtCompetitor = GetTable_Competitor(c_conDB, strWHERE)
                If dtCompetitor.Rows.Count = 1 Then
                    drCompetitor = dtCompetitor.Rows(0)
                    drResult.Item("idCompetitor") = drCompetitor.Item("idCompetitor")
                Else
                    intNewCompetitor -= 1
                    drResult.Item("idCompetitor") = intNewCompetitor
                    If c_dtNewCompetitor Is Nothing Then
                        c_dtNewCompetitor = New DataTable
                        With c_dtNewCompetitor.Columns
                            .Add("idNewCompetitor", GetType(Integer))
                            .Add("idCompetitor", GetType(Integer))
                            .Add("strFirstName", GetType(String))
                            .Add("strSurname", GetType(String))
                            .Add("idGender", GetType(Integer))
                            .Add("strGender", GetType(String))
                            .Add("intBestMatch", GetType(Integer))
                            .Add("strBestMatch", GetType(String))
                            .Add("blnSuggest", GetType(String))
                        End With
                    End If
                    drNewCompetitor = c_dtNewCompetitor.NewRow
                    With drNewCompetitor
                        .Item("idNewCompetitor") = intNewCompetitor
                        .Item("strFirstName") = strFirstName
                        .Item("strSurname") = strSurname
                        .Item("idGender") = CategoryGender(intCategory)
                        .Item("strGender") = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Gender, CategoryGender(intCategory))
                        strWHERE = WhereCompetitor_NameSoundsLike(strSurname, strFirstName)
                        strORDER = OrderCompetitor_NameSoundsLike(strSurname, strFirstName, False)
                        dtCompetitor = GetTable_Competitor(c_conDB, strWHERE, strORDER)
                        If dtCompetitor.Rows.Count > 0 Then
                            drMatchCompetitor = dtCompetitor.Rows(0)
                            .Item("intBestMatch") = drMatchCompetitor.Item("idCompetitor")
                            .Item("strBestMatch") = drMatchCompetitor.Item("FullName")
                            If dtCompetitor.Rows.Count > 1 Then
                                .Item("blnSuggest") = "more suggestions"
                            Else
                                .Item("blnSuggest") = ""
                            End If
                        Else
                            .Item("blnSuggest") = ""
                            .Item("strBestMatch") = ""
                        End If
                    End With
                    c_dtNewCompetitor.Rows.Add(drNewCompetitor)
                End If
            Next
        Next

	End Function

	Private Sub grdNewCompetitor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdNewCompetitor.ItemCommand
		Select Case e.CommandName
			Case "add"
                AddCompetitor(e)
            Case "match"
                AcceptMatch(e)
            Case "suggest"
                SuggestCompetitor(e)
        End Select
	End Sub

	Private Sub AddCompetitor(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
		Dim intNewCompetitorID As Integer
		Dim strFirstName As String
		Dim strSurname As String
		Dim intGender As Integer
		Dim strURL As String

		intNewCompetitorID = e.Item.Cells(NewCompetitorGridColumn.idNewCompetitor).Text
        strFirstName = Replace(e.Item.Cells(NewCompetitorGridColumn.FirstName).Text, "&nbsp;", "")
        strSurname = Trim(Replace(e.Item.Cells(NewCompetitorGridColumn.Surname).Text, "&nbsp;", ""))
		intGender = e.Item.Cells(NewCompetitorGridColumn.idGender).Text

		Me.txtNewCompetitorID.Text = intNewCompetitorID

        strURL = "CompetitorEdit.aspx" & _
        "?" & CompetitorEdit.FIRSTNAME & "=" & URLEscapeCharacters(strFirstName) & _
        "&" & CompetitorEdit.SURNAME & "=" & URLEscapeCharacters(strSurname) & _
        "&" & CompetitorEdit.GENDER_ID & "=" & intGender & _
        "&" & CompetitorEdit.RETURNID_CONTROL & "=" & Me.txtCompetitorID.ClientID

		OpenPopUp(Page, strURL, "Add Competitor")

	End Sub

    Private Sub SuggestCompetitor(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim intNewCompetitorID As Integer
        Dim strFirstName As String
        Dim strSurname As String
        Dim strURL As String

        intNewCompetitorID = e.Item.Cells(NewCompetitorGridColumn.idNewCompetitor).Text
        strFirstName = e.Item.Cells(NewCompetitorGridColumn.FirstName).Text
        strSurname = e.Item.Cells(NewCompetitorGridColumn.Surname).Text

        Me.txtNewCompetitorID.Text = intNewCompetitorID

        strURL = "CompetitorMatch.aspx" & _
        "?" & CompetitorMatch.FIRSTNAME & "=" & URLEscapeCharacters(strFirstName) & _
        "&" & CompetitorMatch.SURNAME & "=" & URLEscapeCharacters(strSurname) & _
        "&" & CompetitorMatch.RETURNID_CONTROL & "=" & Me.txtCompetitorID.ClientID

        OpenPopUp(Page, strURL, "Match Competitor")

    End Sub

    Private Sub txtCompetitorID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompetitorID.TextChanged
        Dim intNewCompetitorID As Integer
        Dim intCompetitorID As Integer
        Dim intCourse As Integer
        Dim dtResult As DataTable
        Dim drResult As DataRow
        Dim drCompetitor As DataRow

        If IsNumeric(Me.txtCompetitorID.Text) Then

        intNewCompetitorID = Me.txtNewCompetitorID.Text
        intCompetitorID = Me.txtCompetitorID.Text

        For intCourse = 1 To UBound(c_dtImport)
            dtResult = c_dtImport(intCourse)
            For Each drResult In dtResult.Rows
                If TypeOf (drResult.Item("idCompetitor")) Is DBNull Then
                    Exit For
                End If
                If drResult.Item("idCompetitor") = intNewCompetitorID Then
                    drResult.Item("idCompetitor") = intCompetitorID
                End If
            Next
        Next

        For Each drCompetitor In c_dtNewCompetitor.Rows
            If drCompetitor.Item("idNewCompetitor") = intNewCompetitorID Then
                drCompetitor.Delete()
                c_dtNewCompetitor.AcceptChanges()
                Exit For
            End If
        Next

        If c_dtNewCompetitor.Rows.Count > 0 Then
            ShowPanel("newcompetitor")
        Else
            ShowPanel("preview")
        End If

        End If

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item("dtImport") = c_dtImport
        Session.Item("dtNewCompetitor") = c_dtNewCompetitor
        Session.Item("dtCourseList") = c_dtCourseList
        Session.Item("dtResult") = c_dtResult

    End Sub

    Private Sub ShowPanel(ByVal strPanel As String)

        'display the correct panel
        Me.pnlUpload.Visible = (strPanel = "upload")
        Me.pnlNewCompetitor.Visible = (strPanel = "newcompetitor")
        Me.pnlPreview.Visible = (strPanel = "preview")

        'Bind appropriate grids to data sources
        Select Case strPanel
            Case "newcompetitor"
                Me.grdNewCompetitor.DataSource = c_dtNewCompetitor
                Me.grdNewCompetitor.DataBind()
            Case "preview"
                LoadImport()
                Me.CourseListPreview.CourseTable = c_dtCourseList
                Me.ResultListPreview.ResultTable = c_dtResult(0)
        End Select
    End Sub

    Private Sub LoadImport()
        Dim intCourse As Integer
        Dim dtResultImport As DataTable
        Dim dtResult As DataTable
        Dim drResultImport As DataRow
        Dim drResult As DataRow
        Dim drCourseImport As DataRow
        Dim drCourse As DataRow
        Dim strWHERE As String

        strWHERE = WhereCourse_NONE()
        c_dtCourseList = GetTable_Course(c_conDB, strWHERE)

        'first import the course list
        For Each drCourseImport In c_dtImport(0).Rows
            drCourse = c_dtCourseList.NewRow
            With drCourse
                .Item("idCourse") = drCourseImport.Item("Order")
                .Item("CourseName") = drCourseImport.Item("Course")
                .Item("Length") = drCourseImport.Item("Length")
                .Item("Climb") = drCourseImport.Item("Climb")
                .Item("Controls") = drCourseImport.Item("Controls")
                .Item("idTechnical") = drCourseImport.Item("idTechnical")
                If Not TypeOf (.Item("idTechnical")) Is DBNull Then
                    .Item("Technical") = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Technical, drCourseImport.Item("idTechnical"))
                End If
            End With
            c_dtCourseList.Rows.Add(drCourse)
        Next

        'now import each of the results tables
        ReDim c_dtResult(0)
        strWHERE = WhereResult_NONE()

        For intCourse = 1 To UBound(c_dtImport)
            dtResultImport = c_dtImport(intCourse)
            dtResult = GetTable_Result(c_conDB, strWHERE)
            For Each drResultImport In dtResultImport.Rows
                drResult = dtResult.NewRow
                With drResult
                    .Item("idCourse") = intCourse
                    .Item("idCompetitor") = drResultImport.Item("idCompetitor")
                    .Item("Competitor") = LTrim(RTrim(drResultImport.Item("FirstName") & " " & drResultImport.Item("Surname")))
                    .Item("idCategory") = drResultImport.Item("idCategory")
                    If Not TypeOf (.Item("idCategory")) Is DBNull Then
                        .Item("Category") = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.Category, drResultImport.Item("idCategory"))
                    End If
                    .Item("idClub") = drResultImport.Item("idClub")
                    If Not TypeOf (.Item("idClub")) Is DBNull Then
                        .Item("Club") = g_lookupManager.GetLookupValue(c_conDB, LookupManager.enumLookupTable.ClubShortName, drResultImport.Item("idClub"))
                    End If
                    .Item("Time") = drResultImport.Item("Time")
                    .Item("Points") = drResultImport.Item("Points")
                    .Item("Disqualified") = drResultImport.Item("DSQ")
                    If TypeOf (.Item("Disqualified")) Is DBNull Then
                        .Item("Disqualified") = False
                    End If
                    .Item("Position") = drResultImport.Item("Pos")
                    .Item("Comment") = drResultImport.Item("Comment")
                    If drResultImport.Table.Columns.Contains("RaceNumber") Then
                        .Item("strRaceNumber") = drResultImport.Item("RaceNumber")
                    End If
                End With
                dtResult.Rows.Add(drResult)
                dtResult.AcceptChanges()
            Next
            ReDim Preserve c_dtResult(UBound(c_dtResult) + 1)
            c_dtResult(UBound(c_dtResult)) = dtResult
        Next

    End Sub

    Private Sub CourseListPreview_CourseSelected() Handles CourseListPreview.CourseSelected
        Dim intCourse As Integer

        intCourse = CourseListPreview.SelectedCourse
        Me.ResultListPreview.ResultTable = c_dtResult(intCourse)

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intCourse As Integer
        Dim intNewCourseID As Integer
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim dtResult As DataTable
        Dim drResult As DataRow
        Dim blnSave As Boolean
        Dim intPosition As Integer
        Dim intCategory As Integer
        Dim intClub As Integer
        Dim intPoints As Integer
        Dim strComment As String
        Dim strRaceNumber As String
        Dim dteTime As Date
        Dim strError As String

        blnSave = True
        If Me.chkOverwrite.Checked Then
            DeleteEventCourses(c_conDB, txtEventID.Text, True)
        End If

        'loop through every course, adding it to the event, 
        'and update the results with the new course ID
        For intCourse = 0 To c_dtCourseList.Rows.Count - 1
            drCourse = c_dtCourseList.Rows(intCourse)
            Try
                intNewCourseID = PenOCDB.SaveCourse(c_conDB, -1, txtEventID.Text, drCourse.Item("CourseName"), drCourse.Item("Length"), drCourse.Item("Climb"), drCourse.Item("Controls"), drCourse.Item("idTechnical"), NULL_NUMBER)
            Catch ex As Exception
                strError = ex.Message
                blnSave = False
                'WebMsgBox(Page, "Import Error", "One or more courses failed to save. Import has been terminated.")
            End Try

            If blnSave Then
                For Each drResult In c_dtResult(intCourse + 1).Rows
                    drResult.Item("idCourse") = intNewCourseID
                Next
                c_dtResult(intCourse + 1).AcceptChanges()
            End If

        Next

        'now loop through each result table, saving the results to the database
        If blnSave Then
            For intCourse = 1 To UBound(c_dtResult)
                dtResult = c_dtResult(intCourse)
                For Each drResult In dtResult.Rows
                    LoadDBValue(drResult.Item("Position"), intPosition)
                    If intPosition > 0 Then
                        LoadDBValue(drResult.Item("idCategory"), intCategory)
                        LoadDBValue(drResult.Item("idClub"), intClub)
                        LoadDBValue(drResult.Item("Time"), dteTime)
                        LoadDBValue(drResult.Item("Comment"), strComment)
                        LoadDBValue(drResult.Item("Points"), intPoints)
                        LoadDBValue(drResult.Item("strRaceNumber"), strRaceNumber)
                        If intPoints = NULL_NUMBER Then
                            intPoints = 0
                        End If
                        Try
                            PenOCDB.NewResult(c_conDB, drResult.Item("idCourse"), drResult.Item("Position"), drResult.Item("idCompetitor"), intCategory, intClub, dteTime, intPoints, drResult.Item("Disqualified"), strComment, strRaceNumber)
                        Catch ex As Exception
                            strError = ex.Message
                            blnSave = False
                        End Try
                    End If
                Next
            Next
        End If

        If Me.chkAutoResults.Checked Then
            PenOCDB.SetEvent_ResultsURL(c_conDB, txtEventID.Text, "auto")
        End If

        If Me.chkAutoCalculateLogPoints.Checked Then
            AutoCalculateEventLogPoints(c_conDB, txtEventID.Text)
        End If

        If blnSave Then
            WebMsgBox(Page, "Import Complete", "Import Complete")
        Else
            WebMsgBox(Page, "Import Error", "An error occurred during import. Details: " & strError)
        End If

    End Sub

    Private Sub AcceptMatch(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim intNewCompetitorID As Integer
        Dim intCompetitorID As Integer
        Dim intCourse As Integer
        Dim dtResult As DataTable
        Dim drResult As DataRow
        Dim drCompetitor As DataRow

        intNewCompetitorID = e.Item.Cells(NewCompetitorGridColumn.idNewCompetitor).Text
        intCompetitorID = e.Item.Cells(NewCompetitorGridColumn.idBestMatch).Text

        For intCourse = 1 To UBound(c_dtImport)
            dtResult = c_dtImport(intCourse)
            For Each drResult In dtResult.Rows
                If TypeOf (drResult.Item("idCompetitor")) Is DBNull Then
                    Exit For
                End If
                If drResult.Item("idCompetitor") = intNewCompetitorID Then
                    drResult.Item("idCompetitor") = intCompetitorID
                End If
            Next
        Next

        For Each drCompetitor In c_dtNewCompetitor.Rows
            If drCompetitor.Item("idNewCompetitor") = intNewCompetitorID Then
                drCompetitor.Delete()
                c_dtNewCompetitor.AcceptChanges()
                Exit For
            End If
        Next

        If c_dtNewCompetitor.Rows.Count > 0 Then
            ShowPanel("newcompetitor")
        Else
            ShowPanel("preview")
        End If

    End Sub
End Class
