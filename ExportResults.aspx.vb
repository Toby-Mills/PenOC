Imports SpatialDimensionLibrary.WebForms

Partial Class ExportResults
    Inherits System.Web.UI.Page

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

    Private c_condb As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("style") > "" Then
            Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
        Else
            Me.lnkStylesheet.Attributes.Add("href", "styles.css")
        End If
        EventSearchPopup.ShowEventSearch(Me.cmdEventSearch, Me.txtEventID, Me.txtEvent, True, "Select event to export results for", False)
        Me.txtEventID.Width = New Web.UI.WebControls.Unit(0)

    End Sub

    Private Sub cmdExportResults_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportResults.Click
        Dim dtEvent As DataTable
        Dim dtCourse As DataTable
        Dim drCourse As DataRow
        Dim dtResult As DataTable
        Dim drResult As DataRow
        Dim strWHERE As String
        Dim strFileName As String
        Dim swExportFile As IO.StreamWriter

        Dim intLongestName As Integer
        Dim strCategory As String
        Dim strClub As String
        Dim strTime As String
        Dim strComment As String

        strWHERE = WhereCourse_idEvent(Me.txtEventID.Text)
        dtEvent = GetTable_Event(c_condb, strWHERE, "")

        strFileName = "Results " & dtEvent.Rows(0).Item("EventName") & " " & dtEvent.Rows(0).Item("Date") & ".txt"

        swExportFile = New IO.StreamWriter(Server.MapPath("temp") & "/" & strFileName)

        strWHERE = WhereResult_idEvent(Me.txtEventID.Text)
        dtResult = GetTable_Result(c_condb, strWHERE)
        For Each drResult In dtResult.Rows
            If Len(drResult.Item("Competitor")) > intLongestName Then
                intLongestName = Len(drResult.Item("Competitor"))
                If intLongestName > 30 Then
                    intLongestName = 30
                    Exit For
                End If
            End If
        Next

        strWHERE = WhereCourse_idEvent(Me.txtEventID.Text)
        dtCourse = GetTable_Course(c_condb, strWHERE)
        With swExportFile
            For Each drCourse In dtCourse.Rows
                .Write(drCourse.Item("CourseName") & " (")
                .Write(drCourse.Item("Length") & "m, ")
                .Write(drCourse.Item("Climb") & "m climb, ")
                .Write(drCourse.Item("Controls") & " controls, ")
                .Write(drCourse.Item("Technical") & ")")
                .WriteLine()
                .WriteLine()
                .Write("Pos")
                .Write(" ")
                .Write(PadRight("Competitor", " ", intLongestName))
                .Write(" ")
                .Write(PadRight("Cat", " ", 5))
                .Write(" ")
                .Write(PadRight("Club", " ", 10))
                .Write(" ")
                .Write(PadRight("Time", " ", 8))
                .Write(" ")
                .Write(PadRight("Points", " ", 6))
                .Write(" ")
                .Write("Comment")
                .WriteLine()

                strWHERE = WhereResult_idCourse(drCourse.Item("idCourse"))
                dtResult = GetTable_Result(c_condb, strWHERE)
                For Each drResult In dtResult.Rows
                    If Not drResult.Item("Disqualified") Then
                        .Write(PadLeft(drResult.Item("Position"), " ", 3))
                    Else
                        .Write("DSQ")
                    End If
                    .Write(" ")
                    .Write(PadRight(drResult.Item("Competitor"), " ", intLongestName))
                    .Write(" ")
                    LoadDBValue(drResult.Item("Category"), strCategory)
                    .Write(PadRight(strCategory, " ", 5))
                    .Write(" ")
                    LoadDBValue(drResult.Item("Club"), strClub)
                    .Write(PadRight(strClub, " ", 10))
                    .Write(" ")
                    LoadDBValue(drResult.Item("Time"), strTime)
                    .Write(PadLeft(strTime, " ", 8))
                    .Write(" ")
                    .Write(PadLeft(drResult.Item("Points"), " ", 6))
                    .Write(" ")
                    LoadDBValue(drResult.Item("Comment"), strComment)
                    .Write(strComment)

                    .WriteLine()
                Next
                .WriteLine()
            Next
            .Close()
        End With

        StreamFileThroughResponse(Me.Response, Server.MapPath("temp") & "\" & strFileName, strFileName, "text")

    End Sub

    Private Function PadLeft(ByVal strInput As String, ByVal chrPad As Char, ByVal intLength As Integer) As String
        Dim strReturn As String

        strReturn = strInput
        Do Until Len(strReturn) >= intLength
            strReturn = chrPad & strReturn
        Loop

        Return strReturn

    End Function

    Private Function PadRight(ByVal strInput As String, ByVal chrPad As Char, ByVal intLength As Integer) As String
        Dim strReturn As String

        strReturn = strInput
        Do Until Len(strReturn) >= intLength
            strReturn &= chrPad
        Loop

        Return strReturn

    End Function
End Class
