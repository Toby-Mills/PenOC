Partial Class DownloadEdit_Popup
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

    Private c_conDB As OleDb.OleDbConnection
    Private c_strRefresh As String
    Private c_intDownload As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            c_strRefresh = Request.Item("ctlRefresh")
            If Request.Item("intDownload") > "" Then
                c_intDownload = Request.Item("intDownload")
                PopulateFromDownload(c_intDownload)
            Else
                c_intDownload = NULL_NUMBER
            End If
        Else
            c_intDownload = Session.Item(Me.ClientID & "intDownload")
            c_strRefresh = Session.Item(Me.ClientID & "strRefresh")
        End If
    End Sub

    Private Sub PopulateFromDownload(ByVal intDownload As Integer)
        Dim dtDownload As DataTable
        Dim drDownload As DataRow

        dtDownload = GetTable_Download(c_conDB, WhereDownload_idDownload(intDownload))
        drDownload = dtDownload.Rows(0)
        Me.txtDownloadID.Text = drDownload.Item("idDownload")
        Me.txtFileID.Text = drDownload.Item("intFile")
        Me.txtTitle.Text = drDownload.Item("Title")
        Me.txtDescription.Text = drDownload.Item("Description")

    End Sub

    Public Shared Function ShowDownloadEdit(ByRef pageOpener As Page, ByVal intDownload As Integer, ByVal btnRefresh As Web.UI.WebControls.WebControl)
        Dim strScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=900px," & _
         "height=500px," & _
         "left='+((screen.width -900) / 2)+'," & _
         "top='+ (screen.height - 500) / 2+',"

        'Building the client script- window.open
        strScript = "newwin = window.open ('DownloadEdit_Popup.aspx?intDownload=" & intDownload & "&ctlRefresh=" & btnRefresh.ClientID & "','Download','" & windowAttribs & "');newwin.focus();"

        pageOpener.RegisterClientScriptBlock("DownloadEdit", "<script>" & strScript & "</script>")

    End Function

    Public Shared Function ShowDownloadNew(ByRef ctlOpener As Web.UI.WebControls.WebControl, ByVal btnRefresh As Web.UI.WebControls.WebControl)
        Dim strScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=900px," & _
         "height=500px," & _
         "left='+((screen.width -900) / 2)+'," & _
         "top='+ (screen.height - 500) / 2+',"

        'Building the client script- window.open
        strScript = "newwin = window.open ('DownloadEdit_Popup.aspx?ctlRefresh=" & btnRefresh.ClientID & "','Download','" & windowAttribs & "');newwin.focus();return false;"
        ctlOpener.Attributes.Add("onclick", strScript)
        'pageOpener.RegisterClientScriptBlock("DownloadEdit", "<script>" & strScript & "</script>")

    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click

        If Me.txtDownloadID.Text > "" Then
            UpdateDownload(c_conDB, Me.txtDownloadID.Text, Me.txtTitle.Text, Me.txtFileID.Text, Me.txtDescription.Text)
        Else
            AddDownload(c_conDB, Me.txtTitle.Text, Me.txtFileID.Text, Me.txtDescription.Text)
        End If

        PostToOpener()

    End Sub

    Private Sub PostToOpener()
        Dim script As String

        'build the script
        script = "<script>"
        If c_strRefresh > "" Then
            script &= "window.opener.document.forms[0]." & c_strRefresh & ".click(); "
        End If
        script &= "self.close();"
        script &= "</script>"

        'execute the script
        Me.RegisterClientScriptBlock("DownloadUpdate", script)
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item(Me.ClientID & "intDownload") = c_intDownload
        Session.Item(Me.ClientID & "strRefresh") = c_strRefresh

    End Sub
End Class
