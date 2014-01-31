Partial Class Forum
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
    Protected ThreadListForum As ThreadList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intForum As Integer
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            txtForumID.Width = New Web.UI.WebControls.Unit("0")
            intForum = Request.Item("idforum")
            If intForum > 0 Then
                LoadForum(intForum)
            End If
        End If
        ShowHourGlass(Page)
    End Sub

    Private Function LoadForum(ByVal intForum As Integer)
        Me.txtForumID.Text = intForum
        Me.lblForumName.Text = LookupForum_Name(c_conDB, intForum)

		Me.ThreadListForum.ThreadsTable = GetTable_Thread(c_conDB, WhereThread_ForumID(intForum))

    End Function
End Class
