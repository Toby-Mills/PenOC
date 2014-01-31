Partial Class AdminLogs
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
	Protected WithEvents CourseListLog As CourseList
	Protected WithEvents LogList As LogList

	Private c_conDB As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            Me.txtID.Width = New Web.UI.WebControls.Unit(0)

            With Me.CourseListLog
                .DisplayColumn(CourseList.enumCourseListColumn.Delete) = True
                .DisplayColumn(CourseList.enumCourseListColumn.Climb) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Competitors) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Controls) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Length) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Log) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Technical) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Winner) = False
                .DisplayColumn(CourseList.enumCourseListColumn.WinningTime) = False
            End With

            RefreshLogList()

            Me.pnlLogList.Visible = True
            Me.pnlEdit.Visible = False
            Me.txtAddEventID.Width = New Web.UI.WebControls.Unit(0)
            EventSearchPopup.ShowEventSearch(Me.cmdAddEvent, Me.txtAddEventID, Nothing, True, "Select the event to add to the log", True)
        End If
        ShowHourGlass(Page)
	End Sub

	Private Sub LogList_Selected() Handles LogList.Selected
		Dim intLog As Integer

		Me.pnlLogList.Visible = False
		Me.pnlEdit.Visible = True
		intLog = LogList.SelectedLog
		LoadLog(intLog)

	End Sub

	Private Sub LoadLog(ByVal intLog As Integer)
		Dim dtLog As DataTable
		Dim drLog As DataRow
		Dim intYear As Integer
		Dim strName As String
		Dim intDisregardWorst As Integer
		Dim dtCourse As DataTable

		If intLog = -1 Then
			txtID.Text = intLog
			txtYear.Text = ""
			txtName.Text = ""
			txtDisregard.Text = ""
		Else
			dtLog = GetTable_Log(c_conDB, WhereLog_LogID(intLog))
			If dtLog.Rows.Count > 0 Then
				drLog = dtLog.Rows(0)
				LoadDBValue(drLog.Item("intYear"), intYear)
				LoadDBValue(drLog.Item("strLog"), strName)
				LoadDBValue(drLog.Item("intDisregardWorst"), intDisregardWorst)
				txtID.Text = intLog
				txtYear.Text = DisplayDBValue(intYear)
				txtName.Text = strName
				txtDisregard.Text = DisplayDBValue(intDisregardWorst)

				dtCourse = GetTable_Course(c_conDB, WhereCourse_idLog(intLog))
				Me.CourseListLog.CourseTable = dtCourse
			End If
		End If


	End Sub

	Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		Me.pnlLogList.Visible = True
		Me.pnlEdit.Visible = False
	End Sub

	Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
		PenOCDB.SaveLog(c_conDB, Me.txtID.Text, Me.txtYear.Text, Me.txtName.Text, Me.txtDisregard.Text)
		RefreshLogList()
		Me.pnlLogList.Visible = True
		Me.pnlEdit.Visible = False

	End Sub

	Private Sub RefreshLogList()
		Dim dtLog As DataTable

		dtLog = GetTable_Log(c_conDB, "")
        Me.LogList.LogTable = dtLog

	End Sub

	Private Sub cmdNewLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewLog.Click
		LoadLog(-1)
		Me.pnlLogList.Visible = False
		Me.pnlEdit.Visible = True
	End Sub

	Private Sub LogList_LogDeleted() Handles LogList.LogDeleted

		ConfirmDelete.PopupConfirmDelete(Page, ConfirmDelete.enumDeleteType.Log, LogList.SelectedLog)

	End Sub

    Private Sub txtAddEventID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddEventID.TextChanged
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim strWHERE As String

        strWHERE = WhereCourse_idEvent(Me.txtAddEventID.Text)
        dtCourse = GetTable_Course(c_conDB, strWHERE)
        For Each drCourse In dtCourse.Rows
            AddCourseToLog(c_conDB, Me.txtID.Text, drCourse.Item("idCourse"))
        Next

        LoadLog(Me.txtID.Text)

    End Sub

    Private Sub CourseListLog_CourseDeleted() Handles CourseListLog.CourseDeleted

        RemoveCourseFromLog(c_conDB, CourseListLog.SelectedCourse)
        LoadLog(txtID.Text)

    End Sub

    Private Sub LogList_SetCurrent() Handles LogList.SetCurrent

        SetCurrentLog(c_conDB, LogList.SelectedLog)
        RefreshLogList()

    End Sub
End Class
