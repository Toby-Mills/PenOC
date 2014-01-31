Partial Class AdminEvents
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
    Protected WithEvents EventSearch As EventSearch

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With EventSearch
                .DisplayColumn(EventList.enumEventListColumn.Delete) = True
                .DisplayColumn(EventList.enumEventListColumn.Photos) = False
                .Selectable = True
                .AutoSearch = True
            End With
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub EventSearch_EventSelected() Handles EventSearch.EventSelected
        Dim intEvent As Integer

        intEvent = Me.EventSearch.SelectedEvent
		Server.Transfer("EventEdit.aspx?idevent=" & intEvent)

    End Sub

    Private Sub cmdNewEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewEvent.Click
		Server.Transfer("EventEdit.aspx")
    End Sub

    Private Sub EventSearch_EventDeleted() Handles EventSearch.EventDeleted
        If PenOCDB.DeleteEvent(c_conDB, Me.EventSearch.SelectedEvent) Then
            Me.EventSearch.RunSearch()
        Else
            WebMsgBox(Page, "Error", "Unable to delete Event." & vbCrLf & "The event may have associated courses and results.")
        End If
    End Sub

End Class
