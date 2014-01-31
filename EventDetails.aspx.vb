Partial Class EventDetails
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
            intEvent = Request.Item("idevent")
            LoadEvent(intEvent)
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub LoadEvent(ByVal intEvent As Integer)
        Dim dtEvent As DataTable
        Dim drEvent As DataRow
        Dim strWHERE As String
        Dim dtEventType As DataTable
        Dim drEventType As DataRow
        Dim imgEventType As Web.UI.WebControls.Image

        strWHERE = PenOCDB.WhereEvent_idEvent(intEvent)
        dtEvent = GetTable_Event(c_conDB, strWHERE, "")
        If dtEvent.Rows.Count > 0 Then
            drEvent = dtEvent.Rows(0)

            If drEvent.Item("notice") > "" AndAlso drEvent.Item("notice") <> "auto" Then
                OpenPopUp(Me.Page, drEvent.Item("notice"), "Event Notice")
            End If

            With drEvent
                Me.lblSpecialNote.Text = .Item("SpecialNote").ToString
                Me.lblStarts.Text = .Item("Starts").ToString
                Me.lblRegistration.Text = .Item("Registration").ToString
                Me.lblClose.Text = .Item("Close").ToString
                Me.lblCourses.Text = .Item("Courses").ToString
                Me.lblStarts.Text = .Item("Starts").ToString
                Me.lblCost.Text = .Item("Cost").ToString
                Me.lblDirections.Text = .Item("Directions").ToString
            End With

            Me.EventBrief.LoadEvent(c_conDB, intEvent)

            dtEventType = PenOCDB.GetTable_EventType(c_conDB, intEvent)
            For Each drEventType In dtEventType.Rows
                imgEventType = New Web.UI.WebControls.Image
                imgEventType.ImageUrl = "images\" & drEventType.Item("EventTypeIcon")
                imgEventType.ToolTip = drEventType.Item("EventType")
                imgEventType.Style.Add("margin", "0px")
                Me.phEventType.Controls.Add(imgEventType)
            Next

        End If
    End Sub
End Class
