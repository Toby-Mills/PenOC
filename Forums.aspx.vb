Partial Class Forums
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
    Protected WithEvents ForumList As ForumList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            Me.ForumList.ForumTable = GetTable_Forum(c_conDB, "")
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub ForumList_Selected() Handles ForumList.Selected
		Server.Transfer("Forum.aspx?idforum=" & Me.ForumList.SelectedForum)
    End Sub
End Class
