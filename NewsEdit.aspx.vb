Partial Class NewsEdit
	Inherits System.Web.UI.Page
	'Inherits PageViewStateZip
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
    Protected HTMLEditorNews As HTMLEditor

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intNewsItem As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            CalendarPopup.ShowCalendar(Me.cmdCalDate, Me.txtDate, g_strDateFormat, False)
            OpenPopUp(Me.cmdUploadImage, "UploadImage.aspx?autoclose=true&directory=" & UploadImage.DIRECTORY_NEWSIMAGES, "Upload", 600, 200)
            Me.txtID.Width = New System.Web.UI.WebControls.Unit(0)
            Me.HTMLEditorNews.ImageDirectory = UploadImage.DIRECTORY_NEWSIMAGES
            intNewsItem = Request.Item("idnewsitem")
            If intNewsItem > 0 Then
                LoadNewsItem(intNewsItem)
            Else
                Me.txtDate.Text = Today.ToString(g_strDateFormat)
            End If
            EnableEditing(True)
        End If
        ShowHourGlass(Page)

    End Sub

    Private Sub LoadNewsItem(ByVal intNewsItem As Integer)
        Dim dtNewsItem As DataTable
        Dim drNewsItem As DataRow
        Dim strWHERE As String

        strWHERE = PenOCDB.NewsWhere_idNews(intNewsItem)
        dtNewsItem = PenOCDB.GetTable_News(c_conDB, strWHERE)
        If dtNewsItem.Rows.Count > 0 Then
            drNewsItem = dtNewsItem.Rows(0)
            Me.txtID.Text = intNewsItem
            Me.txtDate.Text = drNewsItem.Item("Date")
            Me.txtTitle.Text = drNewsItem.Item("Title")
            Me.HTMLEditorNews.Text = drNewsItem.Item("News")
        End If

        Session.Remove("newsimage1")
        Session.Remove("newsimage2")
        Session.Remove("newsimage3")

    End Sub

    Private Sub SaveNewsItem(ByVal intNewsItem As Integer)
        Dim strSQL As String
        Dim dteDate As Date
        Dim strTitle As String
        Dim strNews As String
        Dim fileImage As System.Web.HttpPostedFile

        If IsDate(Me.txtDate.Text) Then
            Try
                dteDate = Date.ParseExact(Me.txtDate.Text, g_strDateFormat, Nothing)
            Catch ex As Exception
                dteDate = CDate(Me.txtDate.Text)
            End Try
            strTitle = Me.txtTitle.Text
            strNews = Me.HTMLEditorNews.Text
        End If

        strSQL = " UPDATE tblNews SET " & _
        " dteDate = " & SQLFormat(dteDate) & _
        ", strTitle = " & SQLFormat(strTitle) & _
        ", strNews = " & SQLFormat(strNews) & _
        " WHERE idNews = " & intNewsItem

        ExecuteSQL(c_conDB, strSQL)

    End Sub

    Private Sub EnableEditing(ByVal blnEnable As Boolean)
        Me.txtDate.Enabled = blnEnable
        Me.cmdCalDate.Enabled = blnEnable
        Me.txtTitle.Enabled = blnEnable
        Me.HTMLEditorNews.Enabled = blnEnable

        Me.cmdEdit.Enabled = Not blnEnable
        Me.cmdSave.Enabled = blnEnable
        Me.cmdCancel.Enabled = blnEnable

    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intNews As Integer
        Dim dteDate As Date
        Dim strNews As String

        Me.lblValidationDate.Visible = False

        Try
            Date.ParseExact(Me.txtDate.Text, g_strDateFormat, Nothing)
        Catch ex As Exception
            Me.lblValidationDate.Visible = True
            Me.lblValidationDate.Text = g_strDateFormat
            Exit Sub
        End Try

        If Me.txtID.Text > "" Then
            intNews = txtID.Text
        Else
            dteDate = Date.ParseExact(Me.txtDate.Text, g_strDateFormat, Nothing)
            strNews = Me.HTMLEditorNews.Text
            intNews = PenOCDB.NewNewsItem(c_conDB, dteDate, strNews)
        End If
        SaveNewsItem(intNews)
        LoadNewsItem(intNews)
        EnableEditing(False)
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Dim intNews As Integer

        If Me.txtID.Text > "" Then
            intNews = txtID.Text
            LoadNewsItem(intNews)
            EnableEditing(False)
        End If

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        EnableEditing(True)
    End Sub
End Class
