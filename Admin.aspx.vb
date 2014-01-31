Partial Class Admin
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            If Not Request.Item("sm") = "admin_home" Then
                modFunctions_WebForms.RedirectFrame(Page, "contents", "menu.aspx")
            End If
        End If
    End Sub

    Private Sub cmdAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddEvent.Click
        Server.Transfer("EventEdit.aspx")
    End Sub

    Private Sub cmdEditEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditEvent.Click
        Server.Transfer("AdminEvents.aspx")
    End Sub

    Private Sub cmdImportResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportResults.Click
        Server.Transfer("ImportResults.aspx")
    End Sub

    Private Sub cmdEditLookupTables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditLookupTables.Click
        Server.Transfer("LookupTables.aspx")
    End Sub

    Private Sub cmdUploadPhotos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUploadPhotos.Click
        Server.Transfer("UploadPhotos.aspx")
    End Sub

    Private Sub cmdWorkItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWorkItems.Click
        Server.Transfer("WorkItems.aspx")
    End Sub

    Private Sub cmdLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogs.Click
        Server.Transfer("AdminLogs.aspx")
    End Sub

    Private Sub lnkMembership_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Server.Transfer("AdminMembership.aspx")
    End Sub

    Private Sub cmdAddCompetitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCompetitor.Click
        Server.Transfer("CompetitorEdit.aspx")
    End Sub

    Private Sub cmdEditCompetitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditCompetitor.Click
        Server.Transfer("AdminCompetitors.aspx")
    End Sub

    Private Sub cmdMembership_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMembership.Click
        Server.Transfer("AdminMembership.aspx")
    End Sub

    Private Sub cmdLogOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogOut.Click
        Server.Transfer("LogOut.aspx")
    End Sub

    Private Sub cmdAddNews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNews.Click
        Server.Transfer("NewsEdit.aspx")
    End Sub

    Private Sub cmdEditNews_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditNews.Click
        Server.Transfer("AdminNews.aspx")
    End Sub

    Private Sub cmdCommittee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommittee.Click
        Server.Transfer("AdminCommittee.aspx")
    End Sub

    Private Sub cmdFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiles.Click
        Server.Transfer("AdminFiles.aspx")
    End Sub

    Private Sub cmdUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUsers.Click
        Server.Transfer("AdminUsers.aspx")
    End Sub

    Private Sub cmdExportResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportResults.Click
        Server.Transfer("ExportResults.aspx")
    End Sub
End Class
