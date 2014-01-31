Partial Class Downloads
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
    Protected WithEvents DownloadList As DownloadList

    Private c_conDB As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim blnAdmin As Boolean

        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            LogPageAccess(c_conDB, Page)
            blnAdmin = UserIsAdministrator(c_conDB, Session.Item("user"))
            Me.btnNewDownload.Visible = blnAdmin
            Me.DownloadList.Editable = blnAdmin
            RefreshDownloadList()
        End If

        DownloadEdit_Popup.ShowDownloadNew(Me.btnNewDownload, Me.btnRefresh)

        ShowHourGlass(Page)
    End Sub

    Private Function RefreshDownloadList()
        Dim dtDownload As DataTable

        dtDownload = PenOCDB.GetTable_Download(c_conDB, "")
        Me.DownloadList.DownloadTable = dtDownload

    End Function
    Private Sub DownloadList_Edit() Handles DownloadList.Edit
        DownloadEdit_Popup.ShowDownloadEdit(Me.Page, Me.DownloadList.Selected, Me.btnRefresh)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshDownloadList()
    End Sub

    Private Sub DownloadList_Delete() Handles DownloadList.Delete
        DeleteDownload(c_conDB, DownloadList.Selected)
        RefreshDownloadList()
    End Sub

End Class
