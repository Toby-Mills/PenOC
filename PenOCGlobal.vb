Module PenOCGlobal

    Public g_strDateFormat As String
    Public g_strEmailInfo As String
    Public g_strEmailWebsite As String
    Public g_intCurrentLog As Integer
    Public g_lookupManager As LookupManager

    'Public constants of Session Variable Names
    Public Const SESSION_PHOTO_EVENT_ID = "PhotoEventID"
    Public Const S_HOOK_TYPE As String = "HookType"
    Public Const S_HOOK_ID As String = "HookID"

    Public Enum enum_HookType As Integer
        EventNotice = 1
        EventResults = 2
        EventPhotos = 3
        Log = 4
    End Enum

	Public Function EmailCompetitor(ByVal page As Web.UI.Page, ByRef c_conDB As OleDb.OleDbConnection, ByVal intCompetitor As Integer, ByVal strSubject As String)
		Dim strScript As String
        Dim strEmailAddress As String

        strEmailAddress = LookupCompetitor_Email(c_conDB, intCompetitor)

        If strEmailAddress > "" Then
            strScript = "<script language='javascript'>newwin = window.open('mailto:" & strEmailAddress & "?subject=" & strSubject & "','Email');newwin.focus();</script>"
            page.RegisterClientScriptBlock("popup", strScript)
        Else
            WebMsgBox(page, "No email address", "No registered email address for this person")
        End If

	End Function

	Public Function CategoryGender(ByVal intCategory As LookupManager.enumCategory) As LookupManager.enumGender
		Dim intReturn As LookupManager.enumGender

		Select Case intCategory
			Case LookupManager.enumCategory.Group
				intReturn = LookupManager.enumGender.Group
			Case LookupManager.enumCategory.M12, LookupManager.enumCategory.M16, LookupManager.enumCategory.M20, LookupManager.enumCategory.M21, LookupManager.enumCategory.M40, LookupManager.enumCategory.M50, LookupManager.enumCategory.M60, LookupManager.enumCategory.M70
				intReturn = LookupManager.enumGender.Male
			Case LookupManager.enumCategory.W12, LookupManager.enumCategory.W16, LookupManager.enumCategory.W20, LookupManager.enumCategory.W21, LookupManager.enumCategory.W35, LookupManager.enumCategory.W45, LookupManager.enumCategory.W55, LookupManager.enumCategory.W65
				intReturn = LookupManager.enumGender.Female
		End Select

		Return intReturn

	End Function

	Public Function UserIsAdministrator(ByRef conDB As OleDb.OleDbConnection, ByVal intUser As Integer) As Boolean
		Dim dtUser As DataTable
		Dim drUser As DataRow
		Dim blnReturn As Boolean

		blnReturn = False

		If intUser > -1 Then
			dtUser = GetTable_User(conDB, WhereUser_UserID(intUser))
			If dtUser.Rows.Count > 0 Then
				drUser = dtUser.Rows(0)
				LoadDBValue(drUser.Item("Administrator"), blnReturn)
			End If
		End If

		Return blnReturn

	End Function

    Public Function UserFullName(ByRef conDB As OleDb.OleDbConnection, ByVal intUser As Integer) As String
        Dim dtUser As DataTable
        Dim strWHERE As String
        Dim strReturn As String

        strWHERE = WhereUser_UserID(intUser)
        dtUser = GetTable_User(conDB, strWHERE)

        If dtUser.Rows.Count > 0 Then
            strReturn = dtUser.Rows(0).Item("FullName")
        End If

        Return strReturn

    End Function

    Public Function LoginUser(ByRef conDB As OleDb.OleDbConnection, ByVal strUserName As String, ByVal strPassword As String) As Integer
        Dim dtUser As DataTable
        Dim drUser As DataRow
        Dim strWHERE As String
        Dim intReturn As Integer

        intReturn = -1

        strWHERE = WhereUser_UserName(strUserName)
        strWHERE &= " AND "
        strWHERE &= WhereUser_Enabled(True)

        dtUser = GetTable_User(conDB, strWHERE)
        If dtUser.Rows.Count > 0 Then
            drUser = dtUser.Rows(0)
            If drUser.Item("Password") = strPassword Then
                intReturn = drUser.Item("idCompetitor")
            End If
        End If

        Return intReturn

    End Function

    Public Function LogOut(ByVal page As Web.UI.Page)
        page.Session.Remove("user")
        page.Session.Remove("username")
    End Function

    Public Function LogPageAccess(ByRef conDB As OleDb.OleDbConnection, ByVal page As Web.ui.Page)
        Dim strURL As String
        Dim strPage As String
        Dim strParameters As String

        strURL = page.Request.Url.ToString
        strPage = page.Request.Url.GetLeftPart(UriPartial.Path).ToString()
        strParameters = Right(strURL, Len(strURL) - Len(strPage))

        AddWebLog(conDB, page.Session.SessionID, Now, page.Session.Item("username"), strPage, strParameters)

    End Function


End Module
