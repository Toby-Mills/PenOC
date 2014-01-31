Partial Class NewsItem
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
	Private c_condb As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intNews As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            intNews = Request.Item("idnewsitem")
            LoadNewsItem(intNews)
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub LoadNewsItem(ByVal intNews As Integer)
        Dim strWHERE As String
        Dim dtNews As DataTable
        Dim drNews As DataRow

        If intNews > 0 Then
            strWHERE = PenOCDB.NewsWhere_idNews(intNews)
        Else
            strWHERE = PenOCDB.NewsWhere_MostRecent(c_condb)
        End If

        dtNews = PenOCDB.GetTable_News(c_condb, strWHERE)
        If Not dtNews.Rows.Count = 0 Then
            drNews = dtNews.Rows(0)
            Me.lblDate.Text = drNews.Item("Date").ToString
            Me.lblTitle.Text = drNews.Item("Title").ToString
            Me.lblNews.Text = drNews.Item("News").ToString
        End If
        dtNews = Nothing

    End Sub
End Class
