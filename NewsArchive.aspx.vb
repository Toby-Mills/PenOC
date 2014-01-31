Partial Class NewsArchive
	'Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RepeaterNews As System.Web.UI.WebControls.Repeater
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblNews As System.Web.UI.WebControls.Label
    Protected WithEvents cmdNews As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblID As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Protected WithEvents NewsListArchive As NewsList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtNews As DataTable

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            dtNews = PenOCDB.GetTable_News(c_conDB, "")
            Me.NewsListArchive.DisplayImages = True
            Me.NewsListArchive.NewsTable = dtNews
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub NewsListArchive_NewsSelected() Handles NewsListArchive.NewsSelected
		Server.Transfer("NewsItem.aspx?idnewsitem=" & NewsListArchive.SelectedNewsItem)
    End Sub
End Class
