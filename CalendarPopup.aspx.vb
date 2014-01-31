'Namespace DM.FCHC.Web.UI.Pages
Partial Class CalendarPopup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Private components As System.ComponentModel.IContainer

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private strDateFormat As String = "dd/MM/yyyy"
    Private c_blnShowTime As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strStartDate As String
        Dim dtStart As DateTime

        'First time loading....
        If Not Page.IsPostBack Then

            control.Value = Request.QueryString("textbox").ToString()
            c_blnShowTime = (Request.QueryString("showtime"))
            If Request.QueryString("dateformat") > "" Then
                strDateFormat = Request.QueryString("dateformat")
            End If
            strStartDate = Request.QueryString("date")
            'If a valid date was passed in the url, select it
            Try
                dtStart = DateTime.ParseExact(strStartDate, strDateFormat, Nothing)
                'thedate.SelectedDate = dtStart
                thedate.VisibleDate = dtStart
                txtHour.Text = dtStart.Hour.ToString("00")
                txtMinute.Text = dtStart.Minute.ToString("00")
                txtSecond.Text = dtStart.Second.ToString("00")
            Catch ex As Exception
                thedate.VisibleDate = Now
            End Try

            ShowTime(c_blnShowTime)

            SetYearLinks()

            'Post backs...
        Else
            'maintain the required date format variable through postbacks
            strDateFormat = Session.Item(Me.ID & "dateformat")
        End If

    End Sub

    Private Function ShowTime(ByVal blnShowTime As Boolean)
        lblTime.Visible = blnShowTime
        txtHour.Visible = blnShowTime
        lblHourMinSeparator.Visible = blnShowTime
        txtMinute.Visible = blnShowTime
        lblMinSecSeparator.Visible = blnShowTime
        txtSecond.Visible = blnShowTime
    End Function

    Private Sub thedate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles thedate.SelectionChanged
        'when the user selects a date, run a javascript to update the window that opened the calendar
        Dim script(4) As String
        Dim dtReturn As DateTime

        dtReturn = thedate.SelectedDate

        Try
            dtReturn = dtReturn.AddHours(CDbl(txtHour.Text))
            dtReturn = dtReturn.AddMinutes(CDbl(txtMinute.Text))
            dtReturn = dtReturn.AddSeconds(CDbl(txtSecond.Text))
        Catch ex As Exception
        End Try

        'build the script
        script(1) = "<script>window.opener.document.forms[0]." + control.Value + ".value= '"
        script(2) = dtReturn.ToString(strDateFormat)
        script(3) = "';self.close()"
        script(4) = "</" + "script>"

        'execute the script
        RegisterClientScriptBlock("test", Join(script, ""))

    End Sub

    Private Sub lnkPreviousYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkPreviousYear.Click
        'change the view to the same date on the previous year
        thedate.VisibleDate = DateAdd(DateInterval.Year, -1, thedate.VisibleDate)
        'update the year links
        SetYearLinks()
    End Sub

    Private Sub lnkNextYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkNextYear.Click
        'change the view to the same date on the next year
        thedate.VisibleDate = DateAdd(DateInterval.Year, 1, thedate.VisibleDate)
        'update the year links
        SetYearLinks()
    End Sub

    Private Sub SetYearLinks()
        'set the text of the year links, to be -1 and +1 from the currently visible year
        lnkPreviousYear.Text = thedate.VisibleDate.Year - 1
        lnkNextYear.Text = thedate.VisibleDate.Year + 1
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        'save session variables to persist settings
        Session.Item(Me.ID & "dateformat") = strDateFormat
    End Sub

    Public Shared Sub ShowCalendar(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal dateControl As System.Web.UI.WebControls.WebControl, ByVal strDateFormat As String, ByVal blnShowTime As Boolean)
        Dim clientScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=300px," & _
                        "height=255px," & _
                        "left='+((screen.width -300) / 2)+'," & _
                        "top='+ (screen.height - 255) / 2+'"
        'Building the client script- window.open
        clientScript = "newwin = window.open ('CalendarPopup.aspx?textbox=" & dateControl.ClientID & "&date=' + " & dateControl.ClientID & ".value + '&dateformat=" & strDateFormat & "&showtime=" & (blnShowTime = True) & "','Calendar','" & windowAttribs & "');newwin.focus();return false;"
        'regiter the script to the clientside click event of the 'opener' control
        opener.Attributes.Add("onClick", clientScript)
        opener.Attributes.Add("language", "javascript")
    End Sub

End Class
'End Namespace
