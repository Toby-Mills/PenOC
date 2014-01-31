Partial Class WebForm1
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
	Protected WithEvents pro1 As OboutInc.SlideMenu.SlideMenu
	Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Image4 As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

	Private c_conDb As OleDb.OleDbConnection


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not Page.IsPostBack Then
			If Session.Item("style") > "" Then
				Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
			Else
				Me.lnkStylesheet.Attributes.Add("href", "styles.css")
			End If

            If Me.Session.Item("user") > 0 Then
                Me.lblUserName.Text = UserFullName(c_conDb, Session.Item("user"))
            End If
            Me.lblUser.Visible = (Me.Session.Item("user") > 0)
            Me.lblUserName.Visible = (Me.Session.Item("user") > 0)

            With menuPenOC

                .AddParent("home", "Home")
                .AddChild("home_home", "Home", "home.htm")
                .AddChild("home_links", "Useful Links", "links.aspx")
                .AddChild("home_contact", "Contact Us", "Contact.aspx")
                If Session.Item("user") > 0 Then
                    .AddChild("home_logout", "Log out", "LogOut.aspx")
                Else
                    .AddChild("home_login", "Log in", "Login.aspx")
                End If

                .AddParent("events", "Events")
                .AddChild("events_upcoming", "Upcoming", "EventsFuture.aspx")
                .AddChild("events_recent", "Recent", "EventsRecent.aspx")
                .AddChild("events_archive", "Archive", "EventsArchive.aspx")
                .AddChild("events_myresults", "My Results", "MyResults.aspx")

                .AddParent("news", "News")
                .AddChild("news_current", "Current", "NewsItem.aspx")
                .AddChild("news_archive", "Archive", "NewsArchive.aspx")

                .AddParent("logs", "Logs")
                .AddChild("logs_current", "Current", "LogResults.aspx")
                .AddChild("logs_archive", "Archive", "LogArchive.aspx")
                .AddChild("logs_faq", "FAQ", "LogExplanation.aspx")

                .AddParent("downloads", "Downloads")
                .AddChild("downloads_downloads", "Downloads", "Downloads.aspx")

                '.AddParent("chat", "Chat")
                '.AddChild("chat_forums", "Forums", "Forums.aspx")

                '.AddParent("gallery", "Gallery")

                '.AddParent("links", "Links")

                '.AddParent("beginners", "Beginners")

                If UserIsAdministrator(c_conDb, Session.Item("user")) Then
                    .AddParent("admin", "Admin")
                    .AddChild("admin_home", "Admin Home", "Admin.aspx")
                    .AddChild("admin_competitors", "Competitors", "AdminCompetitors.aspx")
                    .AddChild("admin_events", "Events", "AdminEvents.aspx")
                    .AddChild("admin_logs", "Logs", "AdminLogs.aspx")
                    .AddChild("admin_news", "News", "AdminNews.aspx")
                    .AddChild("admin_committee", "Committee", "AdminCommittee.aspx")
                    .AddChild("admin_users", "Users", "AdminUsers.aspx")
                    .AddChild("admin_files", "Files", "AdminFiles.aspx")
                    .AddChild("admin_lookups", "Lookups", "LookupTables.aspx")
                End If
                .AddParent("footer", "", "")
                .SelectedId = "home_home"
            End With
        End If
        ShowHourGlass(Page)

    End Sub

End Class
