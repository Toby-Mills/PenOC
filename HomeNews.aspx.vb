Partial Class HomeNews
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
    Protected WithEvents NewsListMostRecent As NewsList
    Protected WithEvents NewsListRecent As NewsList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtNews As DataTable

        If Not Page.IsPostBack Then

            Me.NewsListMostRecent.DisplayEdit = False
            Me.NewsListMostRecent.DisplayDelete = False
            Me.NewsListMostRecent.DisplayImages = True
            Me.NewsListMostRecent.DisplayTitleOnly = False

            Me.NewsListRecent.DisplayEdit = False
            Me.NewsListRecent.DisplayDelete = False
            Me.NewsListRecent.DisplayImages = False
            Me.NewsListRecent.DisplayTitleOnly = True

            dtNews = GetTable_News(c_conDB, NewsWhere_Top(c_conDB, 3))
            Me.NewsListMostRecent.NewsTable = dtNews

            dtNews = GetTable_News(c_conDB, NewsWhere_Top(c_conDB, 20))
            dtNews.Rows.RemoveAt(0)
            dtNews.Rows.RemoveAt(0)
            dtNews.Rows.RemoveAt(0)
            dtNews.AcceptChanges()
            Me.NewsListRecent.NewsTable = dtNews

            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
        End If

    End Sub

    Private Sub cmdMoreItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoreItems.Click
        modFunctions_WebForms.RedirectFrame(Me, "main", "NewsArchive.aspx")
    End Sub

    Private Sub NewsListMostRecent_NewsSelected() Handles NewsListMostRecent.NewsSelected
        modFunctions_WebForms.RedirectFrame(Me, "main", "NewsItem.aspx?idnewsitem=" & NewsListMostRecent.SelectedNewsItem)
    End Sub

    Private Sub NewsListRecent_NewsSelected() Handles NewsListRecent.NewsSelected
        modFunctions_WebForms.RedirectFrame(Me, "main", "NewsItem.aspx?idnewsitem=" & NewsListRecent.SelectedNewsItem)
    End Sub
End Class
