Partial Class EventsRecent
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
	Protected WithEvents EventListRecent As EventList

    Dim c_conDB As OleDb.OleDbConnection
    Dim c_dtEvent As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strWHERE As String

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            strWHERE = PenOCDB.WhereEvent_PastMonths(6)
            c_dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, "")
            c_dtEvent.DefaultView.Sort = "dteDate DESC"
            With EventListRecent
                .AllowPaging = False
                .DisplayColumn(EventList.enumEventListColumn.Starts) = False
                .DisplayColumn(EventList.enumEventListColumn.Registration) = False
                .DisplayColumn(EventList.enumEventListColumn.Close) = False
                .Selectable = True
                .EventTable = c_dtEvent
            End With
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub EventListRecent_EventSelected() Handles EventListRecent.EventSelected
        Dim intEvent As Integer
        Dim strResultsURL As String

        intEvent = EventListRecent.SelectedEvent
        strResultsURL = LookupEvent_ResultsURL(c_conDB, intEvent)

            Select Case strResultsURL
                Case ""
                Case "auto"
				Server.Transfer("EventResults.aspx?idevent=" & intEvent)
                Case Else
                    Response.Redirect(strResultsURL)
            End Select

    End Sub

    Private Sub EventListRecent_EventPhotos() Handles EventListRecent.EventPhotos

        Dim intEvent As Integer
        Dim strPhotosURL As String

        intEvent = EventListRecent.SelectedEvent
        strPhotosURL = LookupEvent_PhotosURL(c_conDB, intEvent)

        Select Case strPhotosURL
            Case ""
            Case "auto"
                OpenPopUp(Page, "Hook_EventPhotos.aspx?" & Hook_EventPhotos.ARG_EVENT_ID & "=" & intEvent, "Event Photos")
            Case Else
                Response.Redirect(strPhotosURL)
        End Select

    End Sub
End Class
