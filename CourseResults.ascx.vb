Partial Class CourseResults
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
    Protected WithEvents ResultList As ResultList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            With ResultList
                .DisplayColumn(ResultList.enumResultListColumn.CourseClimb) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseControls) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseName) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventDate) = False
                .DisplayColumn(ResultList.enumResultListColumn.ResultDisqualified) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLength) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseLog) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventName) = False
                .DisplayColumn(ResultList.enumResultListColumn.CourseTechnical) = False
                .DisplayColumn(ResultList.enumResultListColumn.EventVenue) = False
            End With
        End If
    End Sub

    Public Function LoadCourse(ByRef conDB As OleDb.OleDbConnection, ByVal intCourse As Integer)
        Dim dtCourse As DataTable
        Dim dtResult As DataTable
        Dim drCourse As DataRow
        Dim strLength As String
        Dim strClimb As String

		dtCourse = PenOCDB.GetTable_Course(c_conDB, PenOCDB.WhereCourse_idCourse(intCourse))
        If dtCourse.Rows.Count > 0 Then
            drCourse = dtCourse.Rows(0)
            With drCourse
                LoadDBValue(.Item("Length"), strLength)
                LoadDBValue(.Item("Climb"), strClimb)
                lblCourseName.Text = .Item("CourseName")
                lblLength.Text = strLength & "m"
                lblClimb.Text = strClimb & "m"
                lblTechnical.Text = "(" & .Item("Technical") & ")"
                LoadDBValue(.Item("SplitsURL"), Me.lnkSplits.NavigateUrl)
                lblCourseSplits.Visible = (Len(lnkSplits.NavigateUrl) > 0)
                lnkSplits.Visible = (Len(lnkSplits.NavigateUrl) > 0)
            End With
        End If

		dtResult = PenOCDB.GetTable_Result(c_conDB, PenOCDB.WhereResult_idCourse(intCourse))
        Me.ResultList.AllowPaging = False
        Me.ResultList.ResultTable = dtResult

    End Function

End Class
