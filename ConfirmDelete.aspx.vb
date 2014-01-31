Partial Class ConfirmDelete
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

    Private c_intObjectID As Integer
    Private c_intDeleteType As enumDeleteType
    Private c_conDB As OleDb.OleDbConnection

    Public Const DELETE_TYPE = "deletetype"
    Public Const OBJECT_ID = "objectid"

    Public Enum enumDeleteType
        Course
        Competitor
		Venue
		User
		Log
        File
        Club
    End Enum

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

		If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
			If Session.Item("style") > "" Then
				Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
			Else
				Me.lnkStylesheet.Attributes.Add("href", "styles.css")
			End If

			c_intObjectID = Request.Item(OBJECT_ID)
			Me.Session.Item(Me.ID & OBJECT_ID) = c_intObjectID
			c_intDeleteType = Request.Item(DELETE_TYPE)
			Me.Session.Item(Me.ID & DELETE_TYPE) = c_intDeleteType

			Select Case c_intDeleteType
				Case enumDeleteType.Competitor
					Me.lblMessage2.Text = "competitor"
				Case enumDeleteType.Course
					Me.lblMessage2.Text = "course"
				Case enumDeleteType.Venue
					Me.lblMessage2.Text = "venue"
				Case enumDeleteType.User
					Me.lblMessage2.Text = "user"
				Case enumDeleteType.Log
					Me.lblMessage2.Text = "log"
				Case enumDeleteType.File
                    Me.lblMessage2.Text = "file"
                Case enumDeleteType.Club
                    Me.lblMessage2.Text = "club"
            End Select
        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
		Dim blnDeleted As Boolean
		Dim strObjectType As String
		Dim strError As String

		c_intObjectID = Me.Session.Item(Me.ID & OBJECT_ID)
        c_intDeleteType = Me.Session.Item(Me.ID & DELETE_TYPE)
		Try
			Select Case c_intDeleteType
				Case enumDeleteType.Competitor
					strObjectType = "Competitor"
					blnDeleted = DeleteCompetitor(c_conDB, c_intObjectID)
                    If Not blnDeleted Then
                        strError = "This Competitor may have associated results, or organised events."
                    End If
				Case enumDeleteType.Course
					strObjectType = "Course"
					blnDeleted = DeleteCourse(c_conDB, c_intObjectID, False)
					If Not blnDeleted Then
						strError = "This Course may have results."
					End If
				Case enumDeleteType.Venue
					strObjectType = "Venue"
                    blnDeleted = DeleteVenue(c_conDB, c_intObjectID)
                    If blnDeleted Then
                        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.Venue)
                    Else
                        strError = "This Venue may have been used for one or more events"
                    End If
				Case enumDeleteType.User
					strObjectType = "User"
                    blnDeleted = DeleteUser(c_conDB, c_intObjectID)
                    If blnDeleted Then
                        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.UserName)
                    End If
                Case enumDeleteType.Log
                    strObjectType = "Log"
                    blnDeleted = DeleteLog(c_conDB, c_intObjectID)
                    If Not blnDeleted Then
                        strError = "This Log may have one or more courses."
                    End If
                Case enumDeleteType.File
                    strObjectType = "File"
                    blnDeleted = DeleteFile(c_conDB, c_intObjectID)
                    If Not blnDeleted Then
                        strError = "The File could not be deleted."
                    End If
                Case enumDeleteType.Club
                    strObjectType = "Club"
                    blnDeleted = DeleteClub(c_conDB, c_intObjectID)
                    If Not blnDeleted Then
                        strError = "The Club may have one or more results or events."
                    Else
                        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.Club_Code)
                        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.ClubFullName)
                        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.ClubShortName)
                    End If
            End Select
		Catch ex As Exception
			blnDeleted = False
			strError = ex.Message
		End Try

		If blnDeleted Then
			RefreshPopupOpener(Page)
			CloseWebWindow(Page)
		Else
			Me.lblError.Text = "Unable to delete this " & strObjectType & "."
			If strError > "" Then
				Me.lblError.Text &= " " & strError
			End If
		End If


	End Sub

	Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

		CloseWebWindow(Page)

	End Sub

	Public Shared Sub PopupConfirmDelete(ByVal page As Web.UI.Page, ByVal intDeleteType As enumDeleteType, ByVal strObjectID As String)

		OpenPopUp(page, "ConfirmDelete.aspx?" & DELETE_TYPE & "=" & intDeleteType & "&" & OBJECT_ID & "=" & strObjectID, "Confirm Delete", 300, 200)

	End Sub
End Class
