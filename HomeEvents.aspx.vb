Partial Class HomeEvents
    'Inherits System.Web.UI.Page
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
	Protected WithEvents EventListUpcoming As EventList

	Private c_conDB As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim strWHERE As String
		Dim dtEvent As DataTable

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            strWHERE = PenOCDB.WhereEvent_PastMonths(2) & " AND " & PenOCDB.WhereEvent_HasResults(True)
            dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, OrderEvent_Date(False))
            With EventListRecent
                .DisplayColumn(EventList.enumEventListColumn.Close) = False
                .DisplayColumn(EventList.enumEventListColumn.Registration) = False
                .DisplayColumn(EventList.enumEventListColumn.Starts) = False
                .DisplayColumn(EventList.enumEventListColumn.Photos) = True
                .Selectable = True
                .AllowPaging = False
                .DisplayEventCount = False
                .EventTable = dtEvent
            End With

            strWHERE = PenOCDB.WhereEvent_FutureMonths(2)
            dtEvent = PenOCDB.GetTable_Event(c_conDB, strWHERE, "")
            With Me.EventListUpcoming
                .DisplayColumn(EventList.enumEventListColumn.Close) = False
                .DisplayColumn(EventList.enumEventListColumn.Photos) = False
                .DisplayColumn(EventList.enumEventListColumn.Registration) = False
                .DisplayColumn(EventList.enumEventListColumn.Starts) = True
                .Selectable = True
                .DisplayEventCount = False
                .AllowPaging = False
                .EventTable = dtEvent
            End With

        End If
        ShowHourGlass(Page)

	End Sub

	Private Sub EventListRecent_EventSelected() Handles EventListRecent.EventSelected
		Dim strURL As String

		strURL = LookupEvent_ResultsURL(c_conDB, EventListRecent.SelectedEvent)
		If strURL > "" Then
			If strURL = "auto" Then
				Session.Item("frame") = "parent"
				Session.Item("redirect") = "EventResults.aspx?idevent=" & EventListRecent.SelectedEvent
				Server.Transfer("RedirectFrame.aspx")
			Else
				OpenPopUp(Page, strURL, "results")
			End If
		Else
			WebMsgBox(Page, "results", "Results are not yet available for this event")
		End If

	End Sub

	Private Sub EventListUpcoming_EventSelected() Handles EventListUpcoming.EventSelected

		Session.Item("frame") = "parent"
		Session.Item("redirect") = "EventDetails.aspx?idevent=" & EventListUpcoming.SelectedEvent
		Server.Transfer("RedirectFrame.aspx")

	End Sub

    Private Sub EventListRecent_EventPhotos() Handles EventListRecent.EventPhotos
        OpenPopUp(Page, "Hook_EventPhotos.aspx?" & Hook_EventPhotos.ARG_EVENT_ID & "=" & EventListRecent.SelectedEvent, "Event Photos")

    End Sub
End Class
