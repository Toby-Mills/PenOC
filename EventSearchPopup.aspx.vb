Partial Class EventSearchPopup
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
    Protected WithEvents EventSearch As EventSearch
    Private c_condb As OleDb.OleDbConnection

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'First time loading....
        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With Me.EventSearch
                .Selectable = True
            End With
            If Request.Item("autosearch") > "" Then
                Me.EventSearch.AutoSearch = CBool(Request.Item("autosearch"))
            End If
            lblMessage.Text = Request.Item("message")
            txtIDControlID.Text = Request.Item("ctlID")
            txtNameControlID.Text = Request.Item("ctlName")
            txtAutoPostback.Text = Request.Item("autopostback")
            txtIDControlID.Width = New Web.UI.WebControls.Unit(0)
            txtNameControlID.Width = New Web.UI.WebControls.Unit(0)
            txtAutoPostback.Width = New Web.UI.WebControls.Unit(0)
        End If
        ShowHourGlass(Page)
    End Sub

    Public Shared Sub ShowEventSearch(ByVal ctlOpener As System.Web.UI.WebControls.WebControl, ByVal ctlID As System.Web.UI.WebControls.WebControl, ByVal ctlName As System.Web.UI.WebControls.WebControl, ByVal blnAutoSearch As Boolean, ByVal strMessage As String, ByVal blnAutoPostback As Boolean)
        Dim clientScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=600px," & _
         "height=650px," & _
         "left='+((screen.width -600) / 2)+'," & _
         "top='+ (screen.height - 650) / 2+',"

        'Building the client script- window.open
        clientScript = "newwin = window.open ('EventSearchPopup.aspx?ctlID=" & ctlID.ClientID
        If Not ctlName Is Nothing Then
            clientScript &= "&ctlName=" & ctlName.ClientID
        End If
        clientScript &= "&autosearch=" & CStr(blnAutoSearch) & "&autopostback=" & CStr(blnAutoPostback) & "&message=" & strMessage & "','Calendar','" & windowAttribs & "');newwin.focus();return false;"
        'regiter the script to the clientside click event of the 'opener' control
        ctlOpener.Attributes.Add("onClick", clientScript)
        ctlOpener.Attributes.Add("language", "javascript")
    End Sub

    Private Sub EventSearch_CompetitorSelected() Handles EventSearch.EventSelected
        'when the user selects a date, run a javascript to update the window that opened the calendar
        Dim script(5) As String
        Dim intEvent As Integer
        Dim strEvent As String

        intEvent = EventSearch.SelectedEvent
        strEvent = PenOCDB.LookupEvent_Name(c_condb, intEvent)

        'build the script
        script(1) = "<script>window.opener.document.forms[0]." + txtIDControlID.Text + ".value= '" & intEvent & "';"
        If Me.txtNameControlID.Text > "" Then
            script(2) = "window.opener.document.forms[0]." + txtNameControlID.Text + ".value= '" & strEvent & "';"
        Else
            script(2) = ""
        End If
        If CBool(Me.txtAutoPostback.Text) Then
            script(3) = "window.opener.document.forms[0].submit();"
        Else
            script(3) = ""
        End If
        script(4) = "self.close()"
        script(5) = "</" + "script>"

        'execute the script
        RegisterClientScriptBlock("test", Join(script, ""))

    End Sub
End Class
