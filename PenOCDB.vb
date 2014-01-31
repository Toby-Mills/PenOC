Module PenOCDB

    Public Enum enumGender
        Male = 1
        Female = 2
        Group = 3
    End Enum

    Public Enum enumTechnical
        Easy = 1
        Moderate = 2
        Difficult = 3
    End Enum

#Region "Events"

    Public Function GetTable_Event(ByRef conDB As OleDb.OleDbConnection, ByVal strWhere As String, ByVal strORDER As String) As DataTable
        Dim strSQL As String
        Dim dsEvent As DataSet
        Dim dtReturn As DataTable

        strSQL = "SELECT DISTINCT tblEvent.idEvent, tblEvent.strName AS EventName, " & _
         " CONVERT(varchar, tblEvent.dteDate, 106) AS [Date], tblEvent.dteDate, " & _
         " tblEvent.strRegTime AS Registration, tblEvent.strStarts AS Starts, " & _
         " tblEvent.strClose AS [Close], lutClub.strShortName AS Club, " & _
         " tblVenue.strName AS Venue, tblCompetitor_2.strFirstName + ' ' + " & _
         " tblCompetitor_2.strSurname AS Planner, tblCompetitor_1.strFirstName + ' ' " & _
         " + tblCompetitor_1.strSurname AS Controller, tblEvent.strPhotos AS photos, " & _
         " tblEvent.strNotice AS notice, tblEvent.strResults AS results, " & _
         " tblEvent.strSpecialNote AS SpecialNote, tblEvent.strDirections AS " & _
         " Directions, tblEvent.strCourses AS Courses, lutClub.idClub, " & _
         " tblCompetitor_1.idCompetitor AS idController, " & _
         " tblCompetitor_2.idCompetitor AS idPlanner, tblVenue.idVenue, " & _
         " tblEvent.intMaxPoints AS MaxPoints, tblEvent.strCost AS Cost, " & _
         " tblEvent.strPlannerReport FROM tblCompetitor tblCompetitor_1 RIGHT OUTER " & _
         " JOIN lutClub RIGHT OUTER JOIN tblEvent LEFT OUTER JOIN tblCourse ON " & _
         " tblEvent.idEvent = tblCourse.intEvent ON lutClub.idClub = " & _
         " tblEvent.intOrganisingClub LEFT OUTER JOIN tblVenue ON tblEvent.intVenue " & _
         " = tblVenue.idVenue ON tblCompetitor_1.idCompetitor = " & _
         " tblEvent.intController LEFT OUTER JOIN tblCompetitor tblCompetitor_2 ON " & _
         " tblEvent.intPlanner = tblCompetitor_2.idCompetitor"

        If strWhere > "" Then
            strSQL = strSQL & " WHERE " & strWhere
        End If

        If strORDER > "" Then
            strSQL &= " ORDER BY " & strORDER
        Else
            strSQL = strSQL & " ORDER BY tblEvent.dteDate"
        End If

        dsEvent = GetDataSet(conDB, strSQL, "events")
        dtReturn = dsEvent.Tables("events")

        Return dtReturn

    End Function

    Public Function WhereEvent_Future() As String
        Dim strReturn As String

        strReturn = " (dteDate >= GETDATE()) "

        Return strReturn

    End Function

    Public Function WhereEvent_FutureMonths(ByVal intMonths As Integer) As String
        Dim strReturn As String

        strReturn = " (dteDate >= DATEADD(DD,-1,GETDATE()) AND dteDate <= DATEADD(MM, " & intMonths & ", GETDATE()))"

        Return strReturn

    End Function

    Public Function WhereEvent_PastMonths(ByVal intMonths As Integer) As String
        Dim strReturn As String

        strReturn = " (dteDate < GETDATE() AND dteDate >= DATEADD(MM, -" & intMonths & ", GETDATE()))"

        Return strReturn

    End Function

    Public Function WhereEvent_HasResults(ByVal blnHasResults As Boolean) As String

        Dim strReturn As String

        If blnHasResults Then
            strReturn = "(tblEvent.strResults > '')"
        Else
            strReturn = "(tblEvent.strResults = '' OR tblEvent.strResults IS NULL)"
        End If

        Return strReturn

    End Function

    Public Function WhereEvent_idEvent(ByVal intEvent As Integer) As String

        Dim strReturn As String

        strReturn = " (tblEvent.idEvent = " & SQLFormat(intEvent) & ") "

        Return strReturn

    End Function

    Public Function WhereEvent_idOrganiser(ByVal intOrganiser As Integer) As String
        Dim strReturn As String

        strReturn = " (tblEvent.intPlanner = " & intOrganiser & " OR tblEvent.intController = " & intOrganiser & ") "

        Return strReturn

    End Function

    Public Function WhereEvent_idClub(ByVal intClub As Integer) As String
        Dim strReturn As String

        strReturn = " (tblEvent.intOrganisingClub = " & intClub & ") "

        Return strReturn

    End Function

    Public Function WhereEvent_idVenue(ByVal intVenue As Integer) As String
        Dim strReturn As String

        strReturn = " (tblEvent.intVenue = " & intVenue & ") "

        Return strReturn

    End Function

    Public Function WhereEvent_DateFrom(ByVal dteFrom As Date) As String
        Dim strReturn As String

        strReturn = " (dteDate > CONVERT(datetime,'" & dteFrom.ToString("dd/MM/yyyy") & "', 103))"

        Return strReturn

    End Function

    Public Function WhereEvent_DateTo(ByVal dteTo As Date) As String
        Dim strReturn As String

        strReturn = " (dteDate < CONVERT(datetime,'" & dteTo.ToString("dd/MM/yyyy") & "', 103))"

        Return strReturn

    End Function

    Public Function WhereEvent_idLog(ByVal intLog As Integer) As String
        Dim strReturn As String

        strReturn = "(tblCourse.intLog = " & SQLFormat(intLog) & ")"

        Return strReturn

    End Function

    Public Function OrderEvent_Date(ByVal blnAscending As Boolean) As String
        Dim strReturn As String

        strReturn = " tblEvent.dteDate "

        If Not blnAscending Then
            strReturn &= "DESC "
        End If

        Return strReturn

    End Function

    Public Function NewEvent(ByRef conDB As OleDb.OleDbConnection, ByVal strName As String) As Integer
        Dim intReturn As Integer
        Dim strSQL As String

        strSQL = "INSERT INTO tblEvent (strName) VALUES (" & SQLFormat(strName) & ")"
        ExecuteSQL(conDB, strSQL)
        intReturn = modDBFunctions.GetLastRecord(conDB, "tblEvent", "idEvent")

        Return intReturn

    End Function

    Public Function DeleteEvent(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As Boolean
        Dim blnReturn As Boolean

        blnReturn = True

        Try
            DeleteDataRows(conDB, "tblEvent", "idEvent", intEvent)
        Catch ex As Exception
            blnReturn = False
        End Try
        Return blnReturn

    End Function

    Public Function GetTable_EventType(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As DataTable
        Dim dtReturn As DataTable
        Dim dsEventType As DataSet
        Dim strSQL As String

        strSQL = "SELECT tblEvent.idEvent AS idEvent, lutEventType.idEventType AS " & _
         " idEventType, lutEventType.strEventType AS EventType, " & _
         " lutEventType.strEventTypeIcon AS EventTypeIcon FROM lutEventType INNER " & _
         " JOIN tblEvent_EventType ON lutEventType.idEventType = " & _
         " tblEvent_EventType.intEventType INNER JOIN tblEvent ON " & _
         " tblEvent_EventType.intEvent = tblEvent.idEvent"

        strSQL = strSQL & " WHERE tblEvent.idEvent = " & intEvent

        dsEventType = GetDataSet(conDB, strSQL, "eventtype")
        dtReturn = dsEventType.Tables("eventtype")

        Return dtReturn

    End Function

    Private Function LookupEvent_Value(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer, ByVal strColumnName As String) As Object
        Dim dtEvent As DataTable
        Dim drEvent As DataRow
        Dim objReturn As Object

        dtEvent = GetTable_Event(conDB, WhereEvent_idEvent(intEvent), "")
        If dtEvent.Rows.Count > 0 Then
            drEvent = dtEvent.Rows(0)
            objReturn = drEvent.Item(strColumnName)
        End If

        Return objReturn

    End Function

    Public Function LookupEvent_Report(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupEvent_Value(conDB, intEvent, "strPlannerReport"), strReturn)

        Return strReturn

    End Function

    Public Function LookupEvent_ResultsURL(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupEvent_Value(conDB, intEvent, "results"), strReturn)

        Return strReturn

    End Function

    Public Function LookupEvent_NoticeURL(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupEvent_Value(conDB, intEvent, "notice"), strReturn)

        Return strReturn

    End Function

    Public Function LookupEvent_PhotosURL(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupEvent_Value(conDB, intEvent, "photos"), strReturn)

        Return strReturn

    End Function

    Public Function LookupEvent_Name(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupEvent_Value(conDB, intEvent, "EventName"), strReturn)

        Return strReturn

    End Function

    Public Function SetEvent_ResultsURL(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer, ByVal strResultsURL As String) As Boolean
        Dim strSQL As String

        strSQL = "UPDATE tblEvent SET strResults = " & SQLFormat(strResultsURL) & _
            " WHERE idEvent = " & SQLFormat(intEvent)

        Return ExecuteSQL(conDB, strSQL)

    End Function

    Public Function SetEvent_PhotosURL(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer, ByVal strPhotosURL As String) As Boolean
        Dim strSQL As String

        strSQL = "UPDATE tblEvent SET strPhotos = " & SQLFormat(strPhotosURL) & _
            " WHERE idEvent = " & SQLFormat(intEvent)

        Return ExecuteSQL(conDB, strSQL)

    End Function
#End Region

#Region "Courses"

    Public Function GetTable_Course(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dsCourse As DataSet
        Dim dtReturn As DataTable

        strSQL = "SELECT tblEvent.idEvent, tblCourse.idCourse, tblVenue.strName AS Venue, " & _
         " tblEvent.strName AS EventName, CONVERT(varchar, tblEvent.dteDate, 106) AS Date, " & _
         " tblCourse.strName AS CourseName, tblCourse.intLength AS Length, " & _
         " tblCourse.intClimb AS Climb, tblCourse.intControls AS Controls, " & _
         " tblCourse.intListOrder AS ListOrder, lutTechnical.idTechnical, lutTechnical.strTechnical AS " & _
         " Technical, tblCourse.strSplitsURL AS SplitsURL, " & _
         " CONVERT(varchar,tblLog.intYear) + ' ' + tblLog.strLog AS Log, tblLog.idLog, COUNT(tblResult.intCompetitor) AS " & _
         " Competitors, qryCourseWinner.Winner, qryCourseWinner.WinningTime AS [Winning Time] FROM " & _
         " tblResult LEFT OUTER JOIN qryCourseWinner ON tblResult.intCourse = " & _
         " qryCourseWinner.intCourse RIGHT OUTER JOIN tblCourse INNER JOIN tblEvent " & _
         " ON tblCourse.intEvent = tblEvent.idEvent INNER JOIN tblVenue ON " & _
         " tblEvent.intVenue = tblVenue.idVenue ON tblResult.intCourse = " & _
         " tblCourse.idCourse LEFT OUTER JOIN lutTechnical ON tblCourse.intTechnical " & _
         " = lutTechnical.idTechnical LEFT OUTER JOIN tblLog " & _
         "  ON tblCourse.intLog = " & _
         " tblLog.idLog GROUP BY tblEvent.idEvent, tblCourse.idCourse, " & _
         " tblVenue.strName, tblEvent.strName, tblEvent.dteDate, tblCourse.strName, " & _
         " tblCourse.intLength, tblCourse.intClimb, tblCourse.intControls, tblCourse.strSplitsURL, " & _
         " tblCourse.intListOrder,lutTechnical.idTechnical, lutTechnical.strTechnical, " & _
         " CONVERT(varchar,tblLog.intYear) + ' ' + tblLog.strLog, tblLog.idLog, qryCourseWinner.Winner, " & _
         " qryCourseWinner.WinningTime"

        If strWHERE > "" Then
            strSQL = strSQL & " HAVING " & strWHERE
        End If

        dsCourse = GetDataSet(conDB, strSQL, "courses")
        dtReturn = dsCourse.Tables("courses")

        Return dtReturn
    End Function

	Public Function WhereCourse_idCourse(ByVal intCourse As Integer) As String
		Dim strReturn As String

		strReturn = " (tblCourse.idCourse = " & SQLFormat(intCourse) & ") "

		Return strReturn

	End Function

	Public Function WhereCourse_idEvent(ByVal intEvent As Integer) As String

		Dim strReturn As String

		strReturn = " (tblEvent.idEvent = " & SQLFormat(intEvent) & ") "

		Return strReturn

	End Function

	Public Function WhereCourse_idTechnical(ByVal intTechnical As enumTechnical) As String
		Dim strReturn As String

		strReturn = "(lutTechnical.idTechnical = " & SQLFormat(intTechnical) & ")"

		Return strReturn
	End Function

	Public Function WhereCourse_idLog(ByVal intLog As Integer) As String
		Dim strReturn As String

		strReturn = "(tblLog.idLog = " & SQLFormat(intLog) & ")"

		Return strReturn

	End Function

	Public Function WhereCourse_NONE() As String
		Dim strReturn As String

		strReturn = "(tblCourse.idCourse <> tblCourse.idCourse)"

		Return strReturn

	End Function

	Public Function SaveCourse(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer, ByVal intEvent As Integer, ByVal strCourseName As String, ByVal intLength As Integer, ByVal intClimb As Integer, ByVal intControls As Integer, ByVal intTechnical As Integer, ByVal intLog As Integer) As Integer
		Dim intReturn As Integer
		Dim strSQL As String

		If intCourse = -1 Then
			strSQL = "INSERT INTO tblCourse (intEvent, strName) VALUES (" & SQLFormat(intEvent) & ", " & SQLFormat(strCourseName) & ")"
			ExecuteSQL(conDB, strSQL)
			intCourse = modDBFunctions.GetLastRecord(conDB, "tblCourse", "idCourse")
		End If

		strSQL = "UPDATE tblCourse SET " & _
		  "strName = " & SQLFormat(strCourseName) & _
		  ", intLength = " & SQLFormat(intLength) & _
		  ", intClimb = " & SQLFormat(intClimb) & _
		  ", intControls = " & SQLFormat(intControls) & _
		  ", intTechnical = " & SQLFormat(intTechnical) & _
		  ", intLog = " & SQLFormat(intLog) & _
		  " WHERE idCourse = " & intCourse

		ExecuteSQL(conDB, strSQL)

		Return intCourse

	End Function

	Public Function DeleteEventCourses(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer, ByVal blnDeleteResults As Boolean)
		Dim dtCourse As DataTable
		Dim drCourse As DataRow
		Dim intCourse As Integer
		Dim strWHERE As String

		strWHERE = PenOCDB.WhereCourse_idEvent(intEvent)
		dtCourse = PenOCDB.GetTable_Course(conDB, strWHERE)
		For Each drCourse In dtCourse.Rows
			intCourse = drCourse.Item("idCourse")
			DeleteCourse(conDB, intCourse, blnDeleteResults)
		Next
	End Function

	Public Function DeleteCourse(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer, ByVal blnDeleteResults As Boolean) As Boolean
		Dim strSQL As String
		Dim ex As Exception
		Dim blnReturn As Boolean

		If blnDeleteResults Then
			strSQL = "DELETE FROM tblResult WHERE intCourse = " & SQLFormat(intCourse)
			ExecuteSQL(conDB, strSQL)
		Else
			If ExistsInTable(conDB, "tblResult", "intCourse", intCourse) Then
				ex = New Exception("Course cannot be deleted as it has associated results.")
				Throw ex
			End If
		End If

		strSQL = "DELETE FROM tblCourse WHERE idCourse = " & SQLFormat(intCourse)

		blnReturn = ExecuteSQL(conDB, strSQL)

		Return blnReturn

	End Function

    Public Function AddCourseToLog(ByRef conDB As OleDb.OleDbConnection, ByVal intLog As Integer, ByVal intCourse As Integer) As Boolean
        Dim strSQL As String

        strSQL = "UPDATE tblCourse SET intLog = " & SQLFormat(intLog) & " WHERE idCourse = " & SQLFormat(intCourse)
        Return ExecuteSQL(conDB, strSQL)

    End Function

    Public Function RemoveCourseFromLog(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer) As Boolean
        Dim strSQL As String

        strSQL = "UPDATE tblCourse SET intLog = " & SQLFormat(NULL_NUMBER) & " WHERE idCourse = " & SQLFormat(intCourse)
        Return ExecuteSQL(conDB, strSQL)

    End Function

#End Region

#Region "Results"

    Public Function GetTable_Result(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dsResult As DataSet
        Dim dtReturn As DataTable

        strSQL = "SELECT tblEvent.idEvent, tblCourse.idCourse, tblCompetitor.idCompetitor, " & _
            " CONVERT(varchar, tblEvent.dteDate, 106) AS Date, tblEvent.dteDate, tblEvent.strName AS " & _
            " Name, tblVenue.strName AS Venue, tblCourse.strName AS Course, " & _
            " tblCourse.intLength AS Length, tblCourse.intClimb AS Climb, " & _
            " tblCourse.intControls AS Controls, tblLog.strLog AS Log, lutTechnical.strTechnical AS " & _
            " Technical, strReadOnlyFullName AS Competitor, " & _
            " lutCategory.idCategory, lutCategory.strCategory AS Category, lutClub.idClub, lutClub.strShortName AS Club, " & _
            " CONVERT(varchar, tblResult.dteTime, 108) AS Time, " & _
            " tblResult.blnDisqualified AS Disqualified, tblResult.strComment AS " & _
            " Comment, tblResult.intPosition AS Position, tblResult.intPoints AS Points " & _
            " FROM  tblLog  RIGHT OUTER JOIN tblResult INNER JOIN tblCourse ON " & _
            " tblResult.intCourse = tblCourse.idCourse INNER JOIN tblEvent ON " & _
            " tblCourse.intEvent = tblEvent.idEvent INNER JOIN tblCompetitor ON " & _
            " tblResult.intCompetitor = tblCompetitor.idCompetitor LEFT OUTER JOIN " & _
            " lutTechnical ON tblCourse.intTechnical = lutTechnical.idTechnical ON " & _
            " tblLog.idLog = tblCourse.intLog LEFT OUTER JOIN lutCategory ON " & _
            " tblResult.intCategory = lutCategory.idCategory LEFT OUTER JOIN tblVenue " & _
            " ON tblEvent.intVenue = tblVenue.idVenue LEFT OUTER JOIN lutClub ON " & _
            " tblResult.intClub = lutClub.idClub "

        If strWHERE > "" Then
            strSQL = strSQL & " WHERE " & strWHERE
        End If

        strSQL = strSQL & " ORDER BY idEvent DESC, intListOrder, intPosition "

        dsResult = GetDataSet(conDB, strSQL, "results")
        dtReturn = dsResult.Tables("results")

        Return dtReturn

    End Function

	Public Function WhereResult_idCompetitor(ByVal intCompetitor As Integer) As String
		Dim strReturn As String

		strReturn = " (tblCompetitor.idCompetitor = " & SQLFormat(intCompetitor) & ") "

		Return strReturn

	End Function

    Public Function WhereResult_idEvent(ByVal intEvent As Integer) As String
        Dim strReturn As String

        strReturn = " (tblEvent.idEvent = " & SQLFormat(intEvent) & ") "

        Return strReturn

    End Function

    Public Function WhereResult_idCourse(ByVal intCourse As Integer) As String
        Dim strReturn As String

        strReturn = " (tblCourse.idCourse = " & SQLFormat(intCourse) & ") "

        Return strReturn

    End Function

    Public Function WhereResult_NONE() As String
        Dim strReturn As String

        strReturn = "(tblResult.intCompetitor <> tblResult.intCompetitor)"

        Return strReturn

    End Function

    Public Function NewResult(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer, ByVal intPosition As Integer, ByVal intCompetitor As Integer, ByVal intCategory As Integer, ByVal intClub As Integer, ByVal dteTime As DateTime, ByVal intPoints As Integer, ByVal blnDSQ As Boolean, ByVal strComment As String)
        Dim strSQL As String

        strSQL = "INSERT INTO tblResult (" & _
         " intCourse, intCompetitor, intCategory, intClub, dteTime, intPosition, intPoints, blnDisqualified, strComment) " & _
         " VALUES (" & SQLFormat(intCourse) & _
         ", " & SQLFormat(intCompetitor) & _
         ", " & SQLFormat(intCategory) & _
         ", " & SQLFormat(intClub) & _
         ", " & SQLFormat(dteTime) & _
         ", " & SQLFormat(intPosition) & _
         ", " & SQLFormat(intPoints) & _
         ", " & SQLFormat(blnDSQ) & _
         ", " & SQLFormat(strComment) & ")"

        ExecuteSQL(conDB, strSQL)

    End Function

    Public Function DeleteResult(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer, ByVal intCompetitor As Integer)
        Dim strSQL As String

        strSQL = "DELETE FROM tblResult WHERE intCourse = " & intCourse & " AND intCompetitor = " & intCompetitor

        ExecuteSQL(conDB, strSQL)

    End Function

    Public Function ParseTime(ByVal strTime As String) As DateTime

        Dim strTimeParts As String()
        Dim strTimePart As String
        Dim strDelimiter As String
        Dim chrDelimiter As Char()
        Dim intHour As Integer
        Dim intMinute As Integer
        Dim intSecond As Integer
        Dim dteReturn As DateTime

        If strTime = "" Then
            dteReturn = Date.MinValue
        Else
            strDelimiter = ":;,._ "
            chrDelimiter = strDelimiter.ToCharArray()

            strTimeParts = strTime.Split(chrDelimiter)

            Select Case UBound(strTimeParts)
                Case 0
                    intMinute = CInt(strTimeParts(0))
                Case 1
                    intMinute = CInt(strTimeParts(0))
                    intSecond = CInt(strTimeParts(1))
                Case 2
                    intHour = CInt(strTimeParts(0))
                    intMinute = CInt(strTimeParts(1))
                    intSecond = CInt(strTimeParts(2))
            End Select

            dteReturn = Date.ParseExact(Format(intHour, "00") & ":" & Format(intMinute, "00") & ":" & Format(intSecond, "00"), "HH:mm:ss", Nothing)

        End If

        Return dteReturn

    End Function

#End Region

#Region "Competitors"

    Public Function GetTable_Competitor(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String, Optional ByVal strORDER As String = "") As DataTable
        Dim strSQL As String
        Dim dsCompetitor As DataSet
        Dim dtReturn As DataTable

        strSQL = "SELECT tblCompetitor.idCompetitor, tblCompetitor.strFirstName AS " & _
         " FirstName, tblCompetitor.strSurname AS Surname, CASE WHEN tblCompetitor.strSurname IS NULL THEN '' " & _
         " ELSE tblCompetitor.strSurname END + CASE WHEN tblCompetitor.strFirstName IS NULL THEN '' ELSE " & _
         " ', ' + tblCompetitor.strFirstName END AS Competitor, CASE WHEN " & _
         " tblCompetitor.strFirstName IS NULL THEN '' ELSE " & _
         " tblCompetitor.strFirstName END + ' ' + CASE WHEN tblCompetitor.strSurname " & _
         " IS NULL THEN '' ELSE tblCompetitor.strSurname END AS FullName, " & _
         " lutCategory.idCategory AS idCategory, lutCategory.strCategory AS " & _
         " Category, tblCompetitor.dteBirthDate AS BirthDate, " & _
         " tblCompetitor.intEmitNumber AS EmitNumber, " & _
         " tblCompetitor.strTelephone1 AS Telephone1, tblCompetitor.strTelephone2 AS " & _
         " Telephone2, tblCompetitor.strEmail AS Email, lutGender.idGender, " & _
         " lutGender.strGender AS Gender, tblCommittee.strPosition AS Position FROM " & _
         " tblCompetitor LEFT OUTER JOIN tblCommittee ON tblCompetitor.idCompetitor " & _
         " = tblCommittee.intCompetitor LEFT OUTER JOIN lutGender ON " & _
         " tblCompetitor.intGender = lutGender.idGender LEFT OUTER JOIN lutCategory " & _
         " ON tblCompetitor.intCategory = lutCategory.idCategory"
        If strWHERE > "" Then
            strSQL = strSQL & " WHERE " & strWHERE
        End If

        If strORDER > "" Then
            strSQL &= " ORDER BY " & strORDER
        Else
            strSQL = strSQL & " ORDER BY CASE WHEN tblCompetitor.strSurname " & _
                " IS NULL THEN '' ELSE tblCompetitor.strSurname END + ', ' + CASE WHEN " & _
                " tblCompetitor.strFirstName IS NULL THEN '' ELSE " & _
                " tblCompetitor.strFirstName END"
        End If

        dsCompetitor = GetDataSet(conDB, strSQL, "competitors")
        dtReturn = dsCompetitor.Tables("competitors")

        Return dtReturn

    End Function

    Public Function NewCompetitor(ByRef conDB As OleDb.OleDbConnection, ByVal strSurname As String, ByVal strFirstName As String) As Integer
        Dim strSQL As String
        Dim intReturn As Integer

        strSQL = "INSERT INTO tblCompetitor (strSurname, strFirstName) " & _
            " VALUES (" & SQLFormat(strSurname) & ", " & SQLFormat(strFirstName) & ")"

        ExecuteSQL(conDB, strSQL)

        intReturn = modDBFunctions.GetLastRecord(conDB, "tblCompetitor", "idCompetitor")

        Return intReturn

    End Function

    Public Function DeleteCompetitor(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer) As Boolean
        Dim strSQL As String
        Dim blnReturn As Boolean

        blnReturn = True

        Try
            DeleteDataRows(conDB, "tblCompetitor", "idCompetitor", intCompetitor)
        Catch ex As Exception
            blnReturn = False
        End Try

        Return blnReturn

    End Function

    Public Function WhereCompetitor_idCompetitor(ByVal intCompetitor As Integer) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.idCompetitor = " & intCompetitor & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_Gender(ByVal intGender As enumGender) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.intGender = " & intGender & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_Individual() As String
        Dim strReturn As String

        strReturn = " (lutCategory.strCategory <> 'Group') "

        Return strReturn

    End Function

    Public Function WhereCompetitor_SurnameStart(ByVal strSurnameStart As String) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.strSurname >= " & SQLFormat(strSurnameStart) & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_SurnameEnd(ByVal strSurnameEnd As String) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.strSurname < " & SQLFormat(strSurnameEnd) & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_NameLike(ByVal strString) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.strSurname Like " & SQLFormat(strString) & " OR tblCompetitor.strFirstName Like " & SQLFormat(strString) & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_Organiser() As String
        Dim strReturn As String

        strReturn = " (idCompetitor IN ((SELECT DISTINCT intPlanner FROM tblEvent) UNION (SELECT DISTINCT intController FROM tblEvent))) "

        Return strReturn

    End Function

    Public Function WhereCompetitor_NameSoundsLike(ByVal strSurname As String, ByVal strFirstname As String) As String
        Dim strReturn As String

        'Select Case True
        '    Case strSurname > "" And strFirstname > ""
        '        strReturn = " (DIFFERENCE(tblCompetitor.strFirstname," & SQLFormat(strFirstname) & ") + DIFFERENCE(tblCompetitor.strSurname," & SQLFormat(strSurname) & ") >=7)"
        '    Case strSurname > ""
        '        strReturn = " (DIFFERENCE(tblCompetitor.strSurname," & SQLFormat(strSurname) & ") >=3)"
        '    Case strFirstname > ""
        '        strReturn = " (DIFFERENCE(tblCompetitor.strFirstname," & SQLFormat(strFirstname) & ") >=3)"
        'End Select

        If strFirstname > "" Then
            strReturn &= "DIFFERENCE(tblCompetitor.strReadOnlyFirstNameMatch, dbo.fn_CleanMatchString(" & SQLFormat(strFirstname) & ")) >=3 AND "
        End If
        If strSurname > "" Then
            strReturn &= "DIFFERENCE(tblCompetitor.strReadOnlySurnameMatch, dbo.fn_CleanMatchString(" & SQLFormat(strSurname) & ")) >=3 AND "
        End If

        strReturn = Left(strReturn, Len(strReturn) - 5)

        strReturn = "(" & strReturn & ")"

        Return strReturn

    End Function

    Public Function OrderCompetitor_NameSoundsLike(ByVal strSurname As String, ByVal strFirstName As String, ByVal blnAscending As Boolean) As String
        Dim strReturn As String

        If strFirstName > "" Then
            strReturn = "CASE WHEN strReadOnlyFirstNameMatch = dbo.fn_CleanMatchString(" & SQLFormat(strFirstName) & ") THEN 2 ELSE 0 END + DIFFERENCE(tblCompetitor.strReadOnlyFirstNameMatch, dbo.fn_CleanMatchString(" & SQLFormat(strFirstName) & ")) + CASE WHEN strReadOnlySurnameMatch = dbo.fn_CleanMatchString(" & SQLFormat(strSurname) & ") THEN 2 ELSE 0 END + DIFFERENCE(tblCompetitor.strReadOnlySurnameMatch, dbo.fn_CleanMatchString(" & SQLFormat(strSurname) & "))"
            If Not blnAscending Then
                strReturn &= " DESC "
            End If
        Else
            strReturn = "CASE WHEN strReadOnlyFirstNameMatch IS NULL THEN 2 ELSE 0 END + CASE WHEN strReadOnlySurnameMatch = dbo.fn_CleanMatchString(" & SQLFormat(strSurname) & ") THEN 2 ELSE 0 END + DIFFERENCE(tblCompetitor.strReadOnlySurnameMatch, dbo.fn_CleanMatchString(" & SQLFormat(strSurname) & "))"
            If Not blnAscending Then
                strReturn &= " DESC "
            End If
        End If

        Return strReturn

    End Function
    Public Function WhereCompetitor_Surname(ByVal strSurname As String) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.strSurname = " & SQLFormat(strSurname) & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_FirstName(ByVal strFirstName As String) As String
        Dim strReturn As String

        strReturn = " (tblCompetitor.strFirstName = " & SQLFormat(strFirstName) & ") "

        Return strReturn

    End Function

    Public Function WhereCompetitor_Committee() As String
        Dim strReturn As String

        strReturn = " (NOT(tblCommittee.intCompetitor IS NULL)) "

        Return strReturn

    End Function

    Private Function LookupCompetitor_Value(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer, ByVal strColumnName As String) As Object
        Dim dtCompetitor As DataTable
        Dim drCompetitor As DataRow
        Dim objReturn As Object

        dtCompetitor = GetTable_Competitor(conDB, WhereCompetitor_idCompetitor(intCompetitor))
        If dtCompetitor.Rows.Count > 0 Then
            drCompetitor = dtCompetitor.Rows(0)
            objReturn = drCompetitor.Item(strColumnName)
        End If

        Return objReturn

    End Function

    Public Function LookupCompetitor_Email(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupCompetitor_Value(conDB, intCompetitor, "Email"), strReturn)

        Return strReturn

    End Function

    Public Function LookupCompetitor_FullName(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupCompetitor_Value(conDB, intCompetitor, "FullName"), strReturn)

        Return strReturn

    End Function

    Public Function CompetitorCurrentCategory(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer) As Integer
        Dim intGender As enumGender
        Dim dteBirthDate As Date
        Dim dtCompetitor As DataTable
        Dim drCompetitor As DataRow
        Dim strWHERE As String
        Dim intReturn As Integer

        intReturn = NULL_NUMBER

        strWHERE = WhereCompetitor_idCompetitor(intCompetitor)
        dtCompetitor = GetTable_Competitor(conDB, strWHERE)
        If dtCompetitor.Rows.Count > 0 Then
            drCompetitor = dtCompetitor.Rows(0)
            LoadDBValue(drCompetitor.Item("idGender"), intGender)
            LoadDBValue(drCompetitor.Item("BirthDate"), dteBirthDate)
            Select Case True
                Case intGender = NULL_NUMBER
                Case dteBirthDate = Date.MinValue
                Case Else
                    intReturn = CompetitorCategory(intGender, dteBirthDate, Now)
            End Select
        End If

        Return intReturn

    End Function

    Public Function CompetitorCategory(ByVal intGender As enumGender, ByVal dteBirthDate As Date, ByVal dteCompetition As Date) As LookupManager.enumCategory
        Dim intReturn As LookupManager.enumCategory
        Dim intAge As Integer

        intReturn = NULL_NUMBER

        intAge = DateDiff(DateInterval.Year, dteBirthDate, dteCompetition)

        Select Case intGender
            Case enumGender.Male
                Select Case intAge
                    Case Is <= 12
                        intReturn = LookupManager.enumCategory.M12
                    Case Is <= 16
                        intReturn = LookupManager.enumCategory.M16
                    Case Is <= 20
                        intReturn = LookupManager.enumCategory.M20
                    Case Is >= 70
                        intReturn = LookupManager.enumCategory.M70
                    Case Is >= 60
                        intReturn = LookupManager.enumCategory.M60
                    Case Is >= 50
                        intReturn = LookupManager.enumCategory.M50
                    Case Is >= 40
                        intReturn = LookupManager.enumCategory.M40
                    Case Else
                        intReturn = LookupManager.enumCategory.M21
                End Select
            Case enumGender.Female
                Select Case intAge
                    Case Is <= 12
                        intReturn = LookupManager.enumCategory.W12
                    Case Is <= 16
                        intReturn = LookupManager.enumCategory.W16
                    Case Is <= 20
                        intReturn = LookupManager.enumCategory.W20
                    Case Is >= 65
                        intReturn = LookupManager.enumCategory.W65
                    Case Is >= 55
                        intReturn = LookupManager.enumCategory.W55
                    Case Is >= 45
                        intReturn = LookupManager.enumCategory.W45
                    Case Is >= 35
                        intReturn = LookupManager.enumCategory.W35
                    Case Else
                        intReturn = LookupManager.enumCategory.W21
                End Select
            Case enumGender.Group
                intReturn = LookupManager.enumCategory.Group
        End Select

        Return intReturn

    End Function

    Public Function CompetitorCurrentClub(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer) As Integer
        Dim strSQL As String
        Dim strWHERE As String
        Dim intYear As Integer
        Dim dtMembership As DataTable
        Dim drMembership As DataRow
        Dim intReturn As Integer

        intReturn = NULL_NUMBER

        strSQL = "SELECT lutClub.idClub FROM tblMembership INNER JOIN " & _
         " tblCompetitorMembership ON tblMembership.idMembership = " & _
         " tblCompetitorMembership.intMembership INNER JOIN lutClub ON " & _
         " tblMembership.intClub = lutClub.idClub"

        strWHERE = " WHERE intCompetitor = " & SQLFormat(intCompetitor) & _
         " AND tblMembership.intYear = " & SQLFormat(Now.Year)

        strSQL = strSQL & strWHERE

        dtMembership = GetDataTable(conDB, strSQL)
        If dtMembership.Rows.Count > 0 Then
            drMembership = dtMembership.Rows(0)
            LoadDBValue(drMembership.Item("idClub"), intReturn)
        End If

        Return intReturn

    End Function

    Public Function AddCommitteeMemeber(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer, ByVal strPosition As String)
        Dim strSQL As String

        strSQL = "INSERT INTO tblCommittee (intCompetitor, strPosition) VALUES (" & _
         SQLFormat(intCompetitor) & ", " & SQLFormat(strPosition) & ")"

        ExecuteSQL(conDB, strSQL)

    End Function

    Public Function RemoveCommitteeMember(ByRef conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer)
        Dim strSQL As String

        strSQL = "DELETE FROM tblCommittee WHERE intCompetitor = " & SQLFormat(intCompetitor)

        ExecuteSQL(conDB, strSQL)

    End Function

#End Region

#Region "Clubs"

    Public Function GetTable_Club(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idClub, strFullName, strShortName FROM lutClub"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        strSQL &= " ORDER BY strShortName"

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function NewClub(ByRef conDB As OleDb.OleDbConnection, ByVal strFullName As String, ByVal strShortName As String) As Integer
        Dim strSQL As String
        Dim intReturn As Integer

        strSQL = "INSERT INTO lutClub (strFullName, strShortName) " & _
            " VALUES (" & SQLFormat(strFullName) & ", " & SQLFormat(strShortName) & ")"

        ExecuteSQL(conDB, strSQL)

        intReturn = modDBFunctions.GetLastRecord(conDB, "lutClub", "idClub")

        Return intReturn

    End Function

    Public Function DeleteClub(ByRef conDB As OleDb.OleDbConnection, ByVal intClub As Integer) As Boolean
        Dim blnReturn As Boolean

        blnReturn = True

        Try
            DeleteDataRows(conDB, "lutClub", "idClub", intClub)
        Catch ex As Exception
            blnReturn = False
        End Try
        Return blnReturn

    End Function

#End Region

#Region "Venues"

    Public Function GetTable_Venue(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idVenue, strName FROM tblVenue"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        strSQL &= " ORDER BY strName"

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn
    End Function

    Public Function NewVenue(ByRef conDB As OleDb.OleDbConnection, ByVal strName As String) As Integer
        Dim strSQL As String
        Dim intReturn As Integer

        strSQL = "INSERT INTO tblVenue (strName) " & _
            " VALUES (" & SQLFormat(strName) & ")"

        ExecuteSQL(conDB, strSQL)

        intReturn = modDBFunctions.GetLastRecord(conDB, "tblVenue", "idVenue")

        Return intReturn

    End Function

    Public Function DeleteVenue(ByRef conDB As OleDb.OleDbConnection, ByVal intVenue As Integer) As Boolean
        Dim blnReturn As Boolean

        blnReturn = True

        Try
            DeleteDataRows(conDB, "tblVenue", "idVenue", intVenue)
        Catch ex As Exception
            blnReturn = False
        End Try
        Return blnReturn

    End Function

#End Region

#Region "Users"

    Public Function GetTable_User(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT tblCompetitor.idCompetitor, tblCompetitor.strFirstName AS " & _
         " FirstName, tblCompetitor.strSurname AS Surname, lutGender.strGender AS " & _
         " Gender, tblCompetitor.dteBirthDate AS BirthDate, " & _
         " tblCompetitor.strTelephone1 AS Telephone1, tblCompetitor.strTelephone2 AS " & _
         " Telephone2, tblCompetitor.strEmail AS Email, " & _
         " tblCompetitor.strReadOnlyFullName AS FullName, tblUser.strUserName AS " & _
         " UserName, tblUser.strPassword AS Password, tblUser.blnEnabled AS Enabled, " & _
         " tblUser.blnAdministrator AS Administrator FROM tblUser INNER JOIN " & _
         " tblCompetitor ON tblUser.intCompetitor = tblCompetitor.idCompetitor LEFT " & _
         " OUTER JOIN lutGender ON tblCompetitor.intGender = lutGender.idGender"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereUser_UserID(ByVal intUser As Integer) As String
        Dim strReturn As String

        strReturn = "(tblUser.intCompetitor = " & SQLFormat(intUser) & ")"

        Return strReturn

    End Function

    Public Function WhereUser_UserName(ByVal strUserName As String) As String
        Dim strReturn As String

        strReturn = "(tblUser.strUserName = " & SQLFormat(strUserName) & ")"

        Return strReturn

    End Function

    Public Function WhereUser_Enabled(ByVal blnEnabled As Boolean) As String
        Dim strReturn As String

        strReturn = "(tblUser.blnEnabled = " & SQLFormat(blnEnabled) & ")"

        Return strReturn

    End Function

    Public Sub SaveUser(ByRef conDB As OleDb.OleDbConnection, ByVal intUser As Integer, ByVal strUserName As String, ByVal blnEnabled As Boolean, ByVal blnAdministrator As Boolean, Optional ByVal strPassword As String = "")
        Dim strSQL As String

        If Not ExistsInTable(conDB, "tblUser", "intCompetitor", intUser) Then
            strSQL = "INSERT INTO tblUser (intCompetitor, strUserName, blnEnabled,strPassword) VALUES (" & _
            SQLFormat(intUser) & _
            ", " & SQLFormat(strUserName) & _
            ", " & SQLFormat(blnEnabled) & _
            ", " & SQLFormat(strPassword) & ")"
            ExecuteSQL(conDB, strSQL)
        End If

        strSQL = "UPDATE tblUser SET " & _
            " strUserName = " & SQLFormat(strUserName) & _
            ", blnEnabled = " & SQLFormat(blnEnabled) & _
            ", blnAdministrator = " & SQLFormat(blnAdministrator)

        If strPassword > "" Then
            strSQL &= ", strPassword = " & SQLFormat(strPassword)
        End If

        strSQL &= " WHERE intCompetitor = " & SQLFormat(intUser)

        ExecuteSQL(conDB, strSQL)

    End Sub

    Public Sub SetUserPassword(ByRef conDB As OleDb.OleDbConnection, ByVal intUser As Integer, ByVal strPassword As String)
        Dim strSQL As String

        strSQL = "UPDATE tblUser SET strPassword = " & SQLFormat(strPassword) & " WHERE intCompetitor = " & SQLFormat(intUser)

        ExecuteSQL(conDB, strSQL)

    End Sub

    Public Function DeleteUser(ByRef conDB As OleDb.OleDbConnection, ByVal intUser As Integer) As Boolean
        Dim strSQL As String
        Dim blnReturn As Boolean

        strSQL = "DELETE FROM tblUser WHERE intCompetitor = " & SQLFormat(intUser)

        blnReturn = ExecuteSQL(conDB, strSQL)

        Return blnReturn

    End Function

#End Region

#Region "News"

    Public Function GetTable_News(ByRef c_conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dsNews As DataSet
        Dim dtReturn As DataTable

        strSQL = "SELECT idNews, strNews AS News, CONVERT (varchar,dteDate,106) as Date, strTitle as Title " & _
         " FROM tblNews"
        If strWHERE > "" Then
            strSQL = strSQL & " WHERE " & strWHERE
        End If

        strSQL &= " ORDER BY dteDate DESC "

        Try
            dsNews = GetDataSet(c_conDB, strSQL, "news")
            dtReturn = dsNews.Tables("news")
        Catch ex As Exception
        End Try

        Return dtReturn

    End Function

    Public Function NewsWhere_idNews(ByVal intNews As Integer) As String
        Dim strReturn As String

        strReturn = " (tblNews.idNews = " & intNews & ") "

        Return strReturn

    End Function

    Public Function NewsWhere_MostRecent(ByRef conDB As OleDb.OleDbConnection) As String
        Dim strReturn

        strReturn = NewsWhere_Top(conDB, 1)

        Return strReturn

    End Function

    Public Function NewsWhere_Top(ByRef conDB As OleDb.OleDbConnection, ByVal intItems As Integer) As String
        Dim strSQL As String
        Dim intNews As Integer
        Dim drNews As DataRow
        Dim dtNews As DataTable
        Dim strReturn

        strSQL = "SELECT TOP " & intItems & " tblNews.idNews FROM tblNews ORDER BY dteDate DESC"
        Try
            dtNews = GetDataTable(conDB, strSQL)

            For Each drNews In dtNews.Rows
                strReturn &= "tblNews.idNews = " & drNews.Item("idNews") & " OR "
            Next
            strReturn = Left(strReturn, Len(strReturn) - 4)
            strReturn = "(" & strReturn & ")"

        Catch ex As Exception

        End Try

        Return strReturn

    End Function
    Public Function NewNewsItem(ByRef conDB As OleDb.OleDbConnection, ByVal dteDate As String, ByVal strNews As String) As Integer
        Dim intReturn As Integer
        Dim strSQL As String

        strSQL = "INSERT INTO tblNews (dteDate, strNews) VALUES (" & _
         SQLFormat(dteDate) & ", " & _
         SQLFormat(strNews) & ")"
        ExecuteSQL(conDB, strSQL)
        intReturn = modDBFunctions.GetLastRecord(conDB, "tblNews", "idNews")

        Return intReturn

    End Function

    Public Sub DeleteNewsItem(ByRef conDB As OleDb.OleDbConnection, ByVal intNews As Integer)
        Dim strSQL As String

        strSQL = "DELETE FROM tblNews WHERE idNews = " & SQLFormat(intNews)

        ExecuteSQL(conDB, strSQL)

    End Sub
#End Region

#Region "Files"

    Public Function GetTable_File(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idFile, strFileName, strDescription FROM tblFile"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        strSQL &= " ORDER BY idFile DESC"

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function


    Public Function GetTable_FileBLOB(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT strFileName,imgFile FROM tblFile"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereFileBLOB_FileID(ByVal intFile As Integer) As String
        Dim strReturn As String

        strReturn = " tblFile.idFile = " & SQLFormat(intFile)

        Return strReturn

    End Function

    Public Function UploadFile(ByRef conDB As OleDb.OleDbConnection, ByVal strFilePath As String, ByVal strDescription As String) As Integer
        Dim cmdAddFile As OleDb.OleDbCommand
        Dim file() As Byte
        Dim intReturn As Integer

        file = GetFileAsByteArray(strFilePath)

        OpenDBConnection(conDB)

        cmdAddFile = New OleDb.OleDbCommand("INSERT INTO tblFile (strFileName, imgFile, strDescription) Values(?,?,?)", conDB)
        With cmdAddFile.Parameters
            .Add("@strFileName", OleDb.OleDbType.VarWChar).Value = FileName(strFilePath)
            .Add("@imgFile", OleDb.OleDbType.Binary, file.Length).Value = file
            .Add("@strDescription", OleDb.OleDbType.VarWChar).Value = strDescription
        End With
        cmdAddFile.ExecuteNonQuery()

        intReturn = modDBFunctions.GetLastRecord(conDB, "tblFile", "idFile")

        Return intReturn

    End Function

    Public Function UpdateFile(ByRef conDB As OleDb.OleDbConnection, ByVal intFile As Integer, ByVal strFilePath As String, ByVal strDescription As String)
        Dim cmdAddFile As OleDb.OleDbCommand
        Dim file() As Byte

        file = GetFileAsByteArray(strFilePath)

        OpenDBConnection(conDB)

        cmdAddFile = New OleDb.OleDbCommand("UPDATE tblFile SET strFileName = ?, imgFile = ?, strDescription = ? WHERE idFile = " & SQLFormat(intFile), conDB)
        With cmdAddFile.Parameters
            .Add("@strFileName", OleDb.OleDbType.VarWChar).Value = FileName(strFilePath)
            .Add("@imgFile", OleDb.OleDbType.Binary, file.Length).Value = file
            .Add("@strDescription", OleDb.OleDbType.VarWChar).Value = strDescription
        End With
        cmdAddFile.ExecuteNonQuery()

    End Function

    Public Function DeleteFile(ByRef conDB As OleDb.OleDbConnection, ByVal intFile As Integer) As Boolean
        Dim strSQL As String
        Dim blnReturn As Boolean

        strSQL = "DELETE FROM tblFile WHERE idFile = " & SQLFormat(intFile)

        blnReturn = ExecuteSQL(conDB, strSQL)

        Return blnReturn

    End Function
#End Region

#Region "Logs"

    Public Function GetTable_Log(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT tblLog.idLog, tblLog.intYear, tblLog.strLog, " & _
         " tblLog.intDisregardWorst, CONVERT(varchar, tblLog.intYear) + ' ' + " & _
         " tblLog.strLog AS Description, COUNT(DISTINCT tblEvent.idEvent) AS Events, " & _
         " tblLog.blnCurrent " & _
         " FROM tblEvent INNER JOIN tblCourse ON tblEvent.idEvent = " & _
         " tblCourse.intEvent RIGHT OUTER JOIN tblLog ON tblCourse.intLog = " & _
         " tblLog.idLog"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        strSQL &= " GROUP BY " & _
         " tblLog.idLog, tblLog.intYear, tblLog.strLog, tblLog.intDisregardWorst, tblLog.blnCurrent," & _
         " CONVERT(varchar, tblLog.intYear) + ' ' + tblLog.strLog"

        strSQL &= " ORDER BY intYear DESC, strLog"

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereLog_LogID(ByVal intLog As Integer) As String
        Dim strReturn As String

        strReturn = "(tblLog.idLog = " & SQLFormat(intLog) & ")"

        Return strReturn
    End Function

    Public Function WhereLog_Year(ByVal intYear As Integer) As String
        Dim strReturn As String

        strReturn = "(tblLog.intYear = " & SQLFormat(intYear) & ")"

        Return strReturn

    End Function

    Public Function WhereLog_Current(ByVal blnCurrent As Boolean) As String

        Dim strReturn As String

        strReturn = " (tblLog.blnCurrent = " & SQLFormat(blnCurrent) & ") "

        Return strReturn

    End Function

    Public Function SaveLog(ByRef conDb As OleDb.OleDbConnection, ByVal intLog As Integer, ByVal intYear As Integer, ByVal strLog As String, ByVal intDisregard As Integer)
        Dim intReturn As Integer
        Dim strSQL As String

        If intLog = -1 Then
            strSQL = "INSERT INTO tblLog (intYear, strLog) VALUES (" & SQLFormat(intYear) & ", " & SQLFormat(strLog) & ")"
            ExecuteSQL(conDb, strSQL)
            intLog = modDBFunctions.GetLastRecord(conDb, "tblLog", "idLog")
        End If

        strSQL = "UPDATE tblLog SET " & _
         " intYear = " & SQLFormat(intYear) & _
         ", strLog = " & SQLFormat(strLog) & _
         ", intDisregardWorst = " & SQLFormat(intDisregard) & _
         " WHERE idLog = " & SQLFormat(intLog)

        ExecuteSQL(conDb, strSQL)

        Return intLog

    End Function

    Public Function DeleteLog(ByRef conDB As OleDb.OleDbConnection, ByVal intLog As Integer) As Boolean
        Dim strSQL As String
        Dim ex As Exception
        Dim blnReturn As Boolean

        If ExistsInTable(conDB, "tblCourse", "intLog", intLog) Then
            ex = New Exception("Log cannot be deleted as it has associated courses.")
            Throw ex
        End If

        strSQL = "DELETE FROM tblLog WHERE idLog = " & SQLFormat(intLog)

        blnReturn = ExecuteSQL(conDB, strSQL)

        Return blnReturn

    End Function

    Public Function TechnicalScale(ByVal intTechnical As enumTechnical) As Decimal
        Dim decReturn As Decimal

        Select Case intTechnical
            Case enumTechnical.Easy
                decReturn = 0.25
            Case enumTechnical.Moderate
                decReturn = 0.75
            Case enumTechnical.Difficult
                decReturn = 1
        End Select

        Return decReturn

    End Function

    Public Function CourseWeight(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer)
        Dim strWHERE As String
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim intLength As Integer
        Dim intClimb As Integer
        Dim intTechnical As Integer
        Dim intReturn As Integer

        strWHERE = WhereCourse_idCourse(intCourse)
        dtCourse = GetTable_Course(conDB, strWHERE)
        If dtCourse.Rows.Count > 0 Then
            drCourse = dtCourse.Rows(0)
            intLength = drCourse.Item("Length")
            intClimb = drCourse.Item("Climb")
            intTechnical = drCourse.Item("idTechnical")
            intReturn = (intLength + (intClimb * 10)) * (TechnicalScale(intTechnical))
        Else
            intReturn = 0
        End If

        Return intReturn

    End Function

    Public Function AutoCalculateEventLogPoints(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer)
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim intCourse As Integer

        dtCourse = GetTable_Course(conDB, WhereCourse_idEvent(intEvent))
        For Each drCourse In dtCourse.Rows
            intCourse = drCourse.Item("idCourse")
            AutoCalculateLogPoints(conDB, intCourse)
        Next

    End Function

    Public Function AutoCalculateLogPoints(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer)
        Dim strSQL As String
        Dim strWHERE As String
        Dim dtEvent As DataTable
        Dim dtCourse As DataTable
        Dim intEvent As Integer
        Dim intCourseWeight As Integer
        Dim intBenchMarkCourse As Integer
        Dim intBenchmarkCourseWeight As Integer
        Dim dteBenchmarkWinningTime As DateTime
        Dim decBenchmarkWinningMinutes As Decimal
        Dim decCourseWeightFactor As Decimal
        Dim intMaxpoints As Integer

        intCourseWeight = CourseWeight(conDB, intCourse)
        strWHERE = WhereCourse_idCourse(intCourse)
        dtCourse = GetTable_Course(conDB, strWHERE)
        If dtCourse.Rows.Count > 0 Then
            intEvent = dtCourse.Rows(0).Item("idEvent")
            intBenchMarkCourse = BenchmarkCourse(conDB, intEvent)
            intBenchmarkCourseWeight = CourseWeight(conDB, intBenchMarkCourse)
            strWHERE = WhereCourse_idCourse(intBenchMarkCourse)
            dtCourse = GetTable_Course(conDB, strWHERE)
            dteBenchmarkWinningTime = dtCourse.Rows(0).Item("Winning Time")
            decBenchmarkWinningMinutes = Hour(dteBenchmarkWinningTime) * 60 + Minute(dteBenchmarkWinningTime) + Second(dteBenchmarkWinningTime) / 60
            decCourseWeightFactor = intCourseWeight / intBenchmarkCourseWeight
            strWHERE = WhereEvent_idEvent(intEvent)
            dtEvent = GetTable_Event(conDB, strWHERE, "")
            intMaxpoints = dtEvent.Rows(0).Item("MaxPoints")
            strSQL = "UPDATE tblResult SET  intPoints = CASE WHEN blndisqualified=1 THEN 0 WHEN dteTime IS NULL THEN 0 WHEN dteTime = 0 THEN 0 " & _
             " ELSE " & intMaxpoints & " *(" & SQLFormat(decBenchmarkWinningMinutes) & _
             " / (DATEPART(HOUR, dteTime) * 60 + DATEPART(MINUTE, dteTime) + CONVERT(Decimal, DATEPART(SECOND, dteTime)) / 60)) " & _
             " * (" & decCourseWeightFactor & ") END " & _
             " WHERE intCourse = " & SQLFormat(intCourse)
            ExecuteSQL(conDB, strSQL)
        End If
    End Function

    Private Function BenchmarkCourse(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer) As Integer
        Dim strWHERE As String
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim intReturn As Integer
        Dim intMaxRating As Integer
        Dim intCourse As Integer
        Dim intRating As Integer

        strWHERE = PenOCDB.WhereCourse_idEvent(intEvent) & " AND " & PenOCDB.WhereCourse_idTechnical(enumTechnical.Difficult)
        dtCourse = GetTable_Course(conDB, strWHERE)
        If dtCourse.Rows.Count = 0 Then
            strWHERE = PenOCDB.WhereCourse_idEvent(intEvent)
            dtCourse = GetTable_Course(conDB, strWHERE)
        End If

        For Each drCourse In dtCourse.Rows
            LoadDBValue(drCourse.Item("idCourse"), intCourse)
            intRating = CourseWeight(conDB, intCourse)
            If intRating > intMaxRating Then
                intReturn = intCourse
                intMaxRating = intRating
            End If
        Next

        Return intReturn

    End Function

    Public Function GetTable_LogResult(ByRef conDB As OleDb.OleDbConnection, ByVal intLog As Integer) As DataTable
        Dim dtLog As DataTable
        Dim drLog As DataRow
        Dim dtEvent As DataTable
        Dim drEvent As DataRow
        Dim intEvent As Integer
        Dim dtReturn As DataTable
        Dim strWHERE As String
        Dim strSQL As String
        Dim strSQLOuter1 As String
        Dim strSQLOuter2 As String
        Dim intDisregardWorst As Integer
        Dim intPlanner As Integer
        Dim intController As Integer
        Dim intMaxPOints As Integer
        Dim drCompetitor As DataRow
        Dim dcPrimaryKey(0) As DataColumn
        Dim drEventPlanner As DataRow
        Dim intEventPlanner As Integer
        Dim intResult1 As Integer
        Dim intResult2 As Integer
        Dim intResult3 As Integer
        Dim intResultAverage As Integer

        dtLog = GetTable_Log(conDB, WhereLog_LogID(intLog))
        If dtLog.Rows.Count > 0 Then
            drLog = dtLog.Rows(0)
            intDisregardWorst = drLog.Item("intDisregardWorst")

            strSQL = "SELECT 1 as [Pos], idCompetitor, strReadOnlyFullName AS Competitor, COUNT(intEvent) AS Events"
            strWHERE = WhereEvent_idLog(intLog)
            dtEvent = GetTable_Event(conDB, strWHERE, "")
            For Each drEvent In dtEvent.Rows
                LoadDBValue(drEvent.Item("idEvent"), intEvent)

                strSQL = strSQL & ", SUM(CASE WHEN intEvent = " & SQLFormat(intEvent) & " THEN intPoints ELSE 0 END) AS event" & intEvent
                strSQLOuter1 = strSQLOuter1 & "Log.event" & intEvent & " + "
                strSQLOuter2 = strSQLOuter2 & ", " & "Log.event" & intEvent
            Next

            strSQL = strSQL & " FROM tblResult INNER JOIN" & _
               " tblCompetitor ON tblResult.intCompetitor = tblCompetitor.idCompetitor INNER JOIN" & _
               " tblCourse ON tblResult.intCourse = tblCourse.idCourse"
            strSQL = strSQL & " WHERE tblCourse.intLog = " & SQLFormat(intLog) & " AND NOT(tblCompetitor.intGender = " & SQLFormat(enumGender.Group) & ")"
            strSQL = strSQL & " GROUP BY idCompetitor, strReadOnlyFullName"

            If strSQLOuter1 > "" Then
                strSQLOuter1 = Left(strSQLOuter1, Len(strSQLOuter1) - 3)
            Else
                strSQLOuter1 = "0"
            End If

            strSQLOuter1 = "SELECT Log.Pos, Log.idCompetitor, Log.Competitor, Log.Events, (" & strSQLOuter1 & ") AS Total " & strSQLOuter2 & " FROM ("

            strSQL = strSQLOuter1 & strSQL & ") AS Log"

            dtReturn = GetDataTable(conDB, strSQL)
            dcPrimaryKey(0) = dtReturn.Columns("idCompetitor")
            dtReturn.PrimaryKey = dcPrimaryKey

            'Go through each event and assign points to both the Planner and Controller
            'Points are calculated as the average of their top 3 results. If they have no results, they get no points.
            For Each drEvent In dtEvent.Rows
                LoadDBValue(drEvent.Item("idEvent"), intEvent)
                LoadDBValue(drEvent.Item("idPlanner"), intPlanner)
                LoadDBValue(drEvent.Item("idController"), intController)
                LoadDBValue(drEvent.Item("MaxPoints"), intMaxPOints)

                'Planner
                If Not intPlanner = NULL_NUMBER Then
                    drCompetitor = dtReturn.Rows.Find(intPlanner)
                    If Not drCompetitor Is Nothing Then

                        intResult1 = 0
                        intResult2 = 0
                        intResult3 = 0
                        intResultAverage = 0

                        For Each drEventPlanner In dtEvent.Rows
                            LoadDBValue(drEventPlanner.Item("idEvent"), intEventPlanner)
                            Select Case True
                                Case drCompetitor.Item("event" & intEventPlanner) >= intResult1
                                    intResult3 = intResult2
                                    intResult2 = intResult1
                                    intResult1 = drCompetitor.Item("event" & intEventPlanner)
                                Case drCompetitor.Item("event" & intEventPlanner) >= intResult2
                                    intResult3 = intResult2
                                    intResult2 = drCompetitor.Item("event" & intEventPlanner)
                                Case drCompetitor.Item("event" & intEventPlanner) >= intResult3
                                    intResult3 = drCompetitor.Item("event" & intEventPlanner)
                            End Select
                        Next

                        Select Case True
                            Case intResult3 > 0
                                intResultAverage = (intResult3 + intResult2 + intResult1) / 3
                            Case intResult2 > 0
                                intResultAverage = (intResult2 + intResult1) / 2
                            Case intResult1 > 0
                                intResultAverage = intResult1
                        End Select

                        drCompetitor.Item("event" & intEvent) = intResultAverage
                        drCompetitor.Item("Events") = drCompetitor.Item("Events") + 1
                    End If
                    dtReturn.AcceptChanges()
                End If

                'Controller
                If Not intController = NULL_NUMBER Then
                    If Not intController = intPlanner Then

                        drCompetitor = dtReturn.Rows.Find(intController)
                        If Not drCompetitor Is Nothing Then

                            intResult1 = 0
                            intResult2 = 0
                            intResult3 = 0
                            intResultAverage = 0

                            For Each drEventPlanner In dtEvent.Rows
                                LoadDBValue(drEventPlanner.Item("idEvent"), intEventPlanner)
                                Select Case True
                                    Case drCompetitor.Item("event" & intEventPlanner) >= intResult1
                                        intResult3 = intResult2
                                        intResult2 = intResult1
                                        intResult1 = drCompetitor.Item("event" & intEventPlanner)
                                    Case drCompetitor.Item("event" & intEventPlanner) >= intResult2
                                        intResult3 = intResult2
                                        intResult2 = drCompetitor.Item("event" & intEventPlanner)
                                    Case drCompetitor.Item("event" & intEventPlanner) >= intResult3
                                        intResult3 = drCompetitor.Item("event" & intEventPlanner)
                                End Select
                            Next

                            Select Case True
                                Case intResult3 > 0
                                    intResultAverage = (intResult3 + intResult2 + intResult1) / 3
                                Case intResult2 > 0
                                    intResultAverage = (intResult2 + intResult1) / 2
                                Case intResult1 > 0
                                    intResultAverage = intResult1
                            End Select

                            drCompetitor.Item("event" & intEvent) = intResultAverage
                            drCompetitor.Item("Events") = drCompetitor.Item("Events") + 1
                        End If
                        dtReturn.AcceptChanges()
                    End If
                End If
            Next


            'Calculate totals (distregarding worst events if necessary)
            CalculateLogTotals(dtReturn, intDisregardWorst)

            'Put the event details as the column headings
            RenameLogTableFields(conDB, dtReturn)

        End If

        Return dtReturn

    End Function

    Private Function CalculateLogTotals(ByRef dtLogResults As DataTable, ByVal intDisregardWorst As Integer)
        Dim drCompetitor As DataRow
        Dim dcEvent As DataColumn
        Dim intScore As Integer
        Dim intTotal As Integer
        Dim alstScore As ArrayList
        Dim intCount As Integer
        Dim dvPos As DataView
        Dim intPoints As Integer
        Dim intPos As Integer

        For Each drCompetitor In dtLogResults.Rows
            alstScore = New ArrayList
            intTotal = 0
            For Each dcEvent In dtLogResults.Columns
                If Left(dcEvent.ColumnName, 5) = "event" Then
                    intScore = drCompetitor.Item(dcEvent.ColumnName)
                    alstScore.Add(intScore)
                    intTotal += intScore
                End If
            Next
            alstScore.Sort()
            For intCount = 0 To intDisregardWorst - 1
                intScore = alstScore.Item(intCount)
                intTotal -= intScore
            Next
            drCompetitor.Item("Total") = intTotal
        Next

        dvPos = dtLogResults.DefaultView
        dvPos.Sort = "Total DESC"

        If dvPos.Count > 1 Then
            intPoints = dvPos.Item(0).Item("Total")
            intPos = 1
            For intCount = 0 To dvPos.Count - 1
                If dvPos.Item(intCount).Item("Total") < intPoints Then
                    intPos += 1
                    intPoints = dvPos.Item(intCount).Item("Total")
                End If
                dvPos.Item(intCount).Item("Pos") = intPos
            Next
        End If

        dtLogResults.AcceptChanges()

    End Function

    Private Sub RenameLogTableFields(ByRef conDB As OleDb.OleDbConnection, ByRef dtLogResults As DataTable)
        Dim dtEvents As DataTable
        Dim drEvent As DataRow
        Dim intColumn As Integer
        Dim strFieldName As String
        Dim intEvent As Integer
        Dim strEventVenue As String
        Dim strEventDescription As String
        Dim dteDate As Date
        Dim Key(0) As DataColumn

        dtEvents = GetTable_Event(conDB, "", "")
        Key(0) = dtEvents.Columns("idEvent")
        dtEvents.PrimaryKey = Key

        For intColumn = 0 To dtLogResults.Columns.Count - 1
            strFieldName = dtLogResults.Columns(intColumn).ColumnName
            If Left(strFieldName, 5) = "event" Then
                intEvent = CInt(Right(strFieldName, Len(strFieldName) - 5))
                drEvent = dtEvents.Rows.Find(intEvent)
                LoadDBValue(drEvent.Item("Venue"), strEventVenue)
                LoadDBValue(drEvent.Item("Date"), dteDate)
                strEventDescription = strEventVenue & " " & Format(dteDate, "dd/MM/yy")
                dtLogResults.Columns(intColumn).ColumnName = strEventDescription
            End If
        Next
    End Sub

    Public Function CurrentLog(ByRef conDB As OleDb.OleDbConnection) As Integer
        Dim dtLog As DataTable
        Dim strWHERE As String
        Dim intReturn As Integer

        strWHERE = WhereLog_Current(True)
        dtLog = GetTable_Log(conDB, strWHERE)

        If dtLog.Rows.Count > 0 Then
            intReturn = dtLog.Rows(0).Item("idLog")
        Else
            intReturn = -1
        End If

        Return intReturn

    End Function

    Public Function SetCurrentLog(ByRef conDb As OleDb.OleDbConnection, ByVal intLog As Integer) As Boolean
        Dim strSQL As String

        strSQL = "UPDATE tblLog SET blnCurrent = " & SQLFormat(False)
        ExecuteSQL(conDb, strSQL)

        strSQL = "UPDATE tblLog SET blnCurrent = " & SQLFormat(True) & " WHERE idLog = " & SQLFormat(intLog)
        Return ExecuteSQL(conDb, strSQL)

    End Function

#End Region

#Region "Membership"

    Public Function GetTable_Membership(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT tblMembership.idMembership, lutClub.strShortName AS ClubShortName, " & _
            " tblMembership.strMembershipNumber, tblCompetitor.strReadOnlyFullName AS " & _
            " PrincipalMember, lutMembershipType.strMembershipType, " & _
            " MAX(LastYear.[Last Year]) AS LastYear, " & _
            " COUNT(tblCompetitorMembership_1.intCompetitor) AS MemberCount FROM " & _
            " tblCompetitorMembership tblCompetitorMembership_1 RIGHT OUTER JOIN " & _
            " lutClub INNER JOIN tblMembership ON lutClub.idClub = " & _
            " tblMembership.intClub INNER JOIN lutMembershipType ON " & _
            " tblMembership.intMembershipType = lutMembershipType.idMembershipType ON " & _
            " tblCompetitorMembership_1.intMembership = tblMembership.idMembership LEFT " & _
            " OUTER JOIN (SELECT intMembership, MAX(intYear) AS [Last Year] FROM " & _
            " tblMembershipYear GROUP BY intMembership) LastYear ON " & _
            " tblMembership.idMembership = LastYear.intMembership LEFT OUTER JOIN " & _
            " tblCompetitorMembership INNER JOIN tblCompetitorMembershipPrincipal INNER " & _
            " JOIN tblCompetitor ON tblCompetitorMembershipPrincipal.intCompetitor = " & _
            " tblCompetitor.idCompetitor ON tblCompetitorMembership.intCompetitor = " & _
            " tblCompetitorMembershipPrincipal.intCompetitor AND " & _
            " tblCompetitorMembership.intMembership = " & _
            " tblCompetitorMembershipPrincipal.intMembership ON " & _
            " tblMembership.idMembership = tblCompetitorMembership.intMembership"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        strSQL &= "  GROUP BY tblMembership.idMembership, lutClub.strShortName, " & _
            " tblCompetitor.strReadOnlyFullName, tblMembership.strMembershipNumber, " & _
            " lutMembershipType.strMembershipType"

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

#End Region
#Region "Forums"

    Public Function GetTable_Forum(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idForum, strForumName FROM tblForum"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereForum_ForumID(ByVal intForum As Integer) As String
        Dim strReturn As String

        strReturn = "(tblForum.idForum = " & SQLFormat(intForum) & ")"

        Return strReturn

    End Function

    Private Function LookupForum_Value(ByRef conDB As OleDb.OleDbConnection, ByVal intForum As Integer, ByVal strColumnName As String) As Object
        Dim dtForum As DataTable
        Dim drForum As DataRow
        Dim objReturn As Object

        dtForum = GetTable_Forum(conDB, WhereForum_ForumID(intForum))
        If dtForum.Rows.Count > 0 Then
            drForum = dtForum.Rows(0)
            objReturn = drForum.Item(strColumnName)
        End If

        Return objReturn

    End Function

    Public Function LookupForum_Name(ByRef conDB As OleDb.OleDbConnection, ByVal intForum As Integer) As String
        Dim strReturn As String

        LoadDBValue(LookupForum_Value(conDB, intForum, "strForumName"), strReturn)

        Return strReturn

    End Function

#End Region

#Region "Threads"

    Public Function GetTable_Thread(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT tblThread.idThread, tblThread.strThread, tblThread.intForum, tblForum.strForumName " & _
         " FROM tblThread INNER JOIN tblForum ON tblThread.intForum = tblForum.idForum"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereThread_ForumID(ByVal intForum As Integer) As String
        Dim strReturn As String

        strReturn = "(tblThread.intForum = " & SQLFormat(intForum) & ")"

        Return strReturn

    End Function

#End Region

#Region "Web Log"
    Public Function AddWebLog(ByRef conDB As OleDb.OleDbConnection, ByVal strSession As String, ByVal dteDate As DateTime, ByVal strUser As String, ByVal strPage As String, ByVal strParameters As String)
        Dim strSQL As String

        strSQL = "INSERT INTO tblWebLog (strUserName, dteDate, strSession, strPageName, strParameters)" & _
            " VALUES (" & SQLFormat(strUser) & ", " & SQLFormat(dteDate) & ", " & SQLFormat(strSession) & ", " & SQLFormat(strPage) & ", " & SQLFormat(strParameters) & ")"

        ExecuteSQL(conDB, strSQL)

    End Function
#End Region

#Region "Work Items"
    Public Function GetTable_WorkItem(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idWorkItem, strWorkItem, blnResolved FROM tblWorkItem"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function AddWorkItem(ByRef conDB As OleDb.OleDbConnection, ByVal strWorkItem As String)
        Dim strSQL As String

        strSQL = "INSERT INTO tblWorkItem (strWorkItem) VALUES (" & SQLFormat(strWorkItem) & ")"
        ExecuteSQL(conDB, strSQL)

    End Function

    Public Function DeleteWorkItem(ByRef conDB As OleDb.OleDbConnection, ByVal intWorkItem As Integer)

        DeleteDataRows(conDB, "tblWorkItem", "idWorkItem", intWorkItem)

    End Function

    Public Function SetWorkItemResolved(ByRef conDB As OleDb.OleDbConnection, ByVal intWorkItem As Integer, ByVal blnresolved As Boolean)
        Dim strSQL As String

        strSQL = "UPDATE tblWorkItem SET blnResolved = " & SQLFormat(blnresolved) & " WHERE idWorkItem = " & SQLFormat(intWorkItem)
        ExecuteSQL(conDB, strSQL)

    End Function
#End Region

#Region "Downloads"

    Public Function GetTable_Download(ByRef conDB As OleDb.OleDbConnection, ByVal strWHERE As String) As DataTable
        Dim strSQL As String
        Dim dtReturn As DataTable

        strSQL = "SELECT idDownload, strTitle AS Title, strDescription AS Description, intFile FROM tblDownload"

        If strWHERE > "" Then
            strSQL &= " WHERE " & strWHERE
        End If

        dtReturn = GetDataTable(conDB, strSQL)

        Return dtReturn

    End Function

    Public Function WhereDownload_idDownload(ByVal intDownload As Integer) As String
        Dim strReturn As String

        strReturn = "(idDownload = " & SQLFormat(intDownload) & ")"

        Return strReturn

    End Function

    Public Sub DeleteDownload(ByRef conDB As OleDb.OleDbConnection, ByVal intDownload As Integer)
        Dim strSQL As String

        strSQL = "DELETE FROM tblDownload WHERE idDownload = " & SQLFormat(intDownload)

        ExecuteSQL(conDB, strSQL)

    End Sub

    Public Sub UpdateDownload(ByRef conDB As OleDb.OleDbConnection, ByVal intDownload As Integer, ByVal strTitle As String, ByVal intFile As Integer, ByVal strDescription As String)

        Dim strSQL As String

        strSQL = "UPDATE tblDownload SET intFile = " & SQLFormat(intFile) & ", strTitle = " & SQLFormat(strTitle) & ", strDescription = " & SQLFormat(strDescription) & " WHERE idDownload = " & SQLFormat(intDownload)

        ExecuteSQL(conDB, strSQL)

    End Sub

    Public Sub AddDownload(ByRef conDB As OleDb.OleDbConnection, ByVal strTitle As String, ByVal intFile As Integer, ByVal strDescription As String)

        Dim strSQL As String

        strSQL = "INSERT INTO tblDownload (intFile, strTitle, strDescription) VALUES (" & _
            SQLFormat(intFile) & ", " & SQLFormat(strTitle) & ", " & SQLFormat(strDescription) & ")"

        ExecuteSQL(conDB, strSQL)

    End Sub
#End Region

End Module
