Partial Class EventsArchive
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
    Protected WithEvents EventSearchArchive As EventSearch

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            SetArchiveCount()
            With EventSearchArchive
                .DisplayColumn(EventList.enumEventListColumn.Close) = False
                .DisplayColumn(EventList.enumEventListColumn.Registration) = False
                .DisplayColumn(EventList.enumEventListColumn.Starts) = False
                .Selectable = True
                .AutoSearch = True
            End With
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub SetArchiveCount()
        Dim dtEvent As DataTable

        dtEvent = PenOCDB.GetTable_Event(c_conDB, "", "")
        If Not dtEvent Is Nothing Then
            Me.lblEventCount.Text = dtEvent.Rows.Count
            dtEvent = Nothing
        End If
    End Sub

    Private Sub EventSearchArchive_EventSelected() Handles EventSearchArchive.EventSelected

		Server.Transfer("EventResults.aspx?idevent=" & EventSearchArchive.SelectedEvent)

    End Sub

    Private Sub EventSearchArchive_EventPhotos() Handles EventSearchArchive.EventPhotos
        OpenPopUp(Page, "Hook_EventPhotos.aspx?" & Hook_EventPhotos.ARG_EVENT_ID & "=" & EventSearchArchive.SelectedEvent, "Event Photos")
    End Sub
End Class
