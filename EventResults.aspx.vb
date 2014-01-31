Partial Class EventResults
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
	Protected WithEvents EventBrief As EventBrief
	Protected WithEvents CourseList As CourseList
	Protected WithEvents CourseResults As CourseResults

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
            With CourseList
                .Selectable = True
                .HighlightSelected = True
                .DisplayColumn(CourseList.enumCourseListColumn.EventDate) = False
                .DisplayColumn(CourseList.enumCourseListColumn.EventName) = False
                .DisplayColumn(CourseList.enumCourseListColumn.Venue) = False
            End With
            intEvent = Request.Item("idEvent")
            LoadEvent(intEvent)
        End If
        ShowHourGlass(Page)

    End Sub
    Private Function LoadEvent(ByVal intEvent As Integer)
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim intCourse As Integer
        Dim strReport As String
		Dim strURL As String

		Me.EventBrief.LoadEvent(c_conDB, intEvent)

		dtCourse = PenOCDB.GetTable_Course(c_conDB, PenOCDB.WhereCourse_idEvent(intEvent))
		Me.CourseList.CourseTable = dtCourse

		strReport = LookupEvent_Report(c_conDB, intEvent)
        Me.litReport.Text = strReport
        If dtCourse.Rows.Count > 0 Then
            CourseResults.LoadCourse(c_conDB, CourseList.CourseTable.Rows(0).Item("idCourse"))
            CourseResults.Visible = True
        Else
            CourseResults.Visible = False
        End If

    End Function

	Private Sub CourseList_Selected() Handles CourseList.CourseSelected

		CourseResults.LoadCourse(c_conDB, CourseList.SelectedCourse)

	End Sub

End Class
