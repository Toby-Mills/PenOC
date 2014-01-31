Public Class LookupManager
    'This class is used to store and provide text values from lookup tables
    'It should be used as a global variable, to ensure that it is persisted (and shared)
    'throughout the application, to decrease unnecessary DB communication
    'use the RefreshLookupTable function if the contents of a lookup have been edited

#Region "Private Variables"
    'Private hashtables to store lookup tables
    Private c_hashClubShortName As Hashtable
	Private c_hashClubFullName As Hashtable
	Private c_hashClub_Code As Hashtable
    Private c_hashEventType As Hashtable
    Private c_hashEventTypeIcon
    Private c_hashGender As Hashtable
	Private c_hashTechnical As Hashtable
	Private c_hashTechnical_Code As Hashtable
	Private c_hashCategory As Hashtable
	Private c_hashCategory_Code As Hashtable
    Private c_hashVenue As Hashtable
    Private c_hashUserName As Hashtable

#End Region

#Region "Public Variables"
    'Public enum of lookup tables available
    Public Enum enumLookupTable
		Category
		Category_Code
        ClubFullName
		ClubShortName
		Club_Code
        EventType
        EventTypeIcon
        Gender
		Technical
		Technical_Code
        UserName
        Venue
    End Enum
#End Region

#Region "Hard Coded Lookup Tables Enums"
    'Public enums of 'HardCoded' lookup tables, whose values must not be edited
    ' (The text value in these lookup tables may still be edited)
    Public Enum enumGender As Integer
        Male = 1
        Female = 2
        Group = 3
    End Enum

    Public Enum enumCategory As Integer
        M12 = 1
        M16 = 2
        M20 = 3
        M21 = 4
        M40 = 5
        M50 = 6
        M60 = 7
        W12 = 8
        W16 = 9
        W20 = 10
        W21 = 11
        W35 = 12
        W45 = 13
        W55 = 14
        Group = 15
        M70 = 16
        W65 = 17
    End Enum

#End Region

#Region "Private Functions"

    Private Function GetHashTable(ByVal intLookupTable As enumLookupTable) As Hashtable
        'Return the local hashtable for the specified lookup table
        Dim hashReturn As Hashtable

        Select Case intLookupTable

            Case enumLookupTable.Category
				hashReturn = c_hashCategory
			Case enumLookupTable.Category_Code
				hashReturn = c_hashCategory_Code
			Case enumLookupTable.ClubShortName
				hashReturn = c_hashClubShortName
			Case enumLookupTable.ClubFullName
				hashReturn = c_hashClubFullName
			Case enumLookupTable.Club_Code
				hashReturn = c_hashClub_Code
			Case enumLookupTable.EventType
				hashReturn = c_hashEventType
			Case enumLookupTable.EventTypeIcon
				hashReturn = c_hashEventTypeIcon
			Case enumLookupTable.Gender
				hashReturn = c_hashGender
			Case enumLookupTable.Technical
				hashReturn = c_hashTechnical
			Case enumLookupTable.Technical_Code
				hashReturn = c_hashTechnical_Code
			Case enumLookupTable.UserName
				hashReturn = c_hashUserName
			Case enumLookupTable.Venue
				hashReturn = c_hashVenue

		End Select

        Return hashReturn

    End Function

    Private Function SetHashTable(ByVal intLookupTable As enumLookupTable, ByVal hashLookup As Hashtable)
        Select Case intLookupTable

            Case enumLookupTable.Category
				c_hashCategory = hashLookup
			Case enumLookupTable.Category_Code
				c_hashCategory_Code = hashLookup
			Case enumLookupTable.ClubFullName
				c_hashClubFullName = hashLookup
			Case enumLookupTable.ClubShortName
				c_hashClubShortName = hashLookup
			Case enumLookupTable.Club_Code
				c_hashClub_Code = hashLookup
			Case enumLookupTable.EventType
				c_hashEventType = hashLookup
			Case enumLookupTable.EventTypeIcon
				c_hashEventTypeIcon = hashLookup
			Case enumLookupTable.Gender
				c_hashGender = hashLookup
			Case enumLookupTable.Technical
				c_hashTechnical = hashLookup
			Case enumLookupTable.Technical_Code
				c_hashTechnical_Code = hashLookup
			Case enumLookupTable.UserName
				c_hashUserName = hashLookup
			Case enumLookupTable.Venue
				c_hashVenue = hashLookup
		End Select
    End Function

    Private Function GetLookupTableQuery(ByVal intLookupTable As enumLookupTable) As String
        'Get the correct SQL query for the requested LookupTable

        Dim strValueFieldName As String
        Dim strTextFieldName As String
        Dim strTableName As String
        Dim strWhere As String
        Dim strOrder As String
        Dim strReturn As String

        strValueFieldName = GetValueFieldName(intLookupTable)
        strTextFieldName = GetTextFieldName(intLookupTable)
        strTableName = GetTableName(intLookupTable)
        strWhere = GetLookupWhereClause(intLookupTable)
        strOrder = getlookuporderclause(intLookupTable)

        strReturn = "SELECT [" & strValueFieldName & "], [" & strTextFieldName & "] FROM [" & strTableName & "]"
        If strWhere > "" Then
            strReturn = strReturn & " WHERE " & strWhere
        End If
        If strOrder = "" Then
            strReturn = strReturn & " ORDER BY [" & strTextFieldName & "]"
        Else
            strReturn = strReturn & strOrder
        End If
        Return strReturn

    End Function

#End Region

#Region "Public Functions"

    Public Function GetLookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal intLookuptable As enumLookupTable, ByVal intValue As Integer) As Object
        Dim objReturn As Object

        objReturn = GetLookupValueFromObject(conDB, intLookuptable, intValue)
        Return objReturn
    End Function

    Public Function GetLookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal intLookuptable As enumLookupTable, ByVal guidValue As Guid) As Object
        Dim objReturn As Object

        objReturn = GetLookupValueFromObject(conDB, intLookuptable, guidValue)
        Return objReturn
    End Function

    Public Function GetLookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal intLookuptable As enumLookupTable, ByVal strValue As String) As Object
        Dim objReturn As Object

        objReturn = GetLookupValueFromObject(conDB, intLookuptable, strValue)
        Return objReturn
    End Function

    Public Function GetLookupValueFromObject(ByRef conDB As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable, ByVal objValue As Object) As Object
        'Returns the value from the specified lookup table for the code passed
        'objValue is typed as Object, to allow for integers & guids
        'return value is an Object, to allow for strings, integers & guids

        Dim objReturn As Object
        Dim hashLookup As Hashtable

        'Get the correct hash table
        hashLookup = GetHashTable(intLookupTable)
        If hashLookup Is Nothing Then
            RefreshLookupTable(conDB, intLookupTable)
            hashLookup = GetHashTable(intLookupTable)
        End If

        'Return the text value from the hash table
        If Not hashLookup Is Nothing Then
			objReturn = hashLookup.Item(objValue)
		Else
			objReturn = Nothing
		End If

		Return objReturn

    End Function

    Public Sub RefreshLookupTable(ByRef conDB As OleDb.OleDbConnection, ByVal intLookuptable As enumLookupTable)
        'Load (or reload) a hash table with values from a lookup table in the DB

        Dim strSQL As String
        Dim hashLookup As Hashtable

        hashLookup = GetHashTable(intLookuptable)
        strSQL = GetLookupTableQuery(intLookuptable)

        'Reset the existing hash table
        hashLookup = Nothing
        'Load the hash table from the DB
        PopulateHashTable(conDB, hashLookup, strSQL)
        SetHashTable(intLookuptable, hashLookup)

    End Sub

    Public Function GetLookupTable(ByRef conDB As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable) As DataTable
        'Return a datatable with the same data as the relevant hashtable
        Dim hashLookup As Hashtable
        Dim intItem As Integer
        Dim dtReturn As DataTable
        Dim drLookupRow As DataRow
        Dim objKey As Object

        'Get the correct hash table
        hashLookup = GetHashTable(intLookupTable)
        If hashLookup Is Nothing Then
            RefreshLookupTable(conDB, intLookupTable)
            hashLookup = GetHashTable(intLookupTable)
        End If

        'build a new table to be returned
        dtReturn = New DataTable
        dtReturn.Columns.Add(New DataColumn("value"))
        dtReturn.Columns.Add(New DataColumn("text"))

        'populate the return table from the hash table
        For Each objKey In hashLookup.Keys
            drLookupRow = dtReturn.NewRow
            drLookupRow.Item("value") = objKey
            drLookupRow.Item("text") = hashLookup.Item(objKey)
            dtReturn.Rows.Add(drLookupRow)
        Next

        'sort by the 'text' field
        dtReturn.DefaultView.Sort = "text"

        Return dtReturn

    End Function

    Public Sub PopulateWebComboBox(ByRef conDB As OleDb.OleDbConnection, ByVal cmbControl As Web.UI.WebControls.DropDownList, ByVal intLookupTable As enumLookupTable, ByVal blnRequired As Boolean)
        Dim dtLookupTable As DataTable
        Dim drRow As DataRow

        dtLookupTable = GetLookupTable(conDB, intLookupTable)
        If Not blnRequired Then
            drRow = dtLookupTable.NewRow
            drRow.Item("value") = NULL_NUMBER
            drRow.Item("text") = ""
            dtLookupTable.Rows.InsertAt(drRow, 0)
        End If

        cmbControl.Items.Clear()

        cmbControl.DataValueField = "value"
        cmbControl.DataTextField = "text"
        cmbControl.DataSource = dtLookupTable
        cmbControl.DataBind()

    End Sub

    Public Sub PopulateWinComboBox(ByRef conDB As OleDb.OleDbConnection, ByVal cmbControl As System.Windows.Forms.ComboBox, ByVal intLookupTable As enumLookupTable, ByVal blnRequired As Boolean)
        Dim dtLookupTable As DataTable
        Dim drRow As DataRow

        dtLookupTable = GetLookupTable(conDB, intLookupTable)
        If Not blnRequired Then
            drRow = dtLookupTable.NewRow
            drRow.Item("value") = 0
            drRow.Item("text") = ""
            dtLookupTable.Rows.InsertAt(drRow, 0)
        End If

        cmbControl.ValueMember = "value"
        cmbControl.DisplayMember = "text"
        cmbControl.DataSource = dtLookupTable

    End Sub

    Public Sub PopulateWebCheckBoxList(ByRef conDB As OleDb.OleDbConnection, ByVal cblControl As Web.UI.WebControls.CheckBoxList, ByVal intLookupTable As enumLookupTable)
        Dim dtLookupTable As DataTable
        Dim drRow As DataRow

        dtLookupTable = GetLookupTable(conDB, intLookupTable)
        cblControl.DataSource = dtLookupTable
        cblControl.DataValueField = "value"
        cblControl.DataTextField = "text"
        cblControl.DataBind()

    End Sub

    Public Sub PopulateWebRadioButtonList(ByRef conDB As OleDb.OleDbConnection, ByVal rblControl As Web.UI.WebControls.RadioButtonList, ByVal intLookupTable As enumLookupTable, ByVal blnRequired As Boolean)
        Dim dtLookupTable As DataTable
        Dim drRow As DataRow

        dtLookupTable = GetLookupTable(conDB, intLookupTable)
        If Not blnRequired Then
            drRow = dtLookupTable.NewRow
            drRow.Item("value") = NULL_NUMBER
            drRow.Item("text") = "Unspecified"
            dtLookupTable.Rows.InsertAt(drRow, 0)
        End If

        rblControl.DataSource = dtLookupTable
        rblControl.DataValueField = "value"
        rblControl.DataTextField = "text"
        rblControl.DataBind()
    End Sub
    'ToDo: Private Function LookupTableIsEditable
    'ToDo: AddItemToLookupTable
    'ToDo: RemoveItemFromLookupTable
    'ToDo: UpdateItemInLookupTable

    Public Function GetValueFieldName(ByVal intLookupTable As enumLookupTable) As String

        Dim strValueFieldName As String

        Select Case intLookupTable

            Case enumLookupTable.Category
				strValueFieldName = "idCategory"

			Case enumLookupTable.Category_Code
				strValueFieldName = "strCategory"

			Case enumLookupTable.ClubFullName
				strValueFieldName = "idClub"

			Case enumLookupTable.ClubShortName
				strValueFieldName = "idClub"

			Case enumLookupTable.Club_Code
				strValueFieldName = "strShortName"

			Case enumLookupTable.EventType
				strValueFieldName = "idEventType"

			Case enumLookupTable.EventTypeIcon
				strValueFieldName = "idEventType"

			Case enumLookupTable.Gender
				strValueFieldName = "idGender"

			Case enumLookupTable.Technical
				strValueFieldName = "idTechnical"

			Case enumLookupTable.Technical_Code
				strValueFieldName = "strTechnical"

			Case enumLookupTable.UserName
				strValueFieldName = "idUser"

			Case enumLookupTable.Venue
				strValueFieldName = "idVenue"

		End Select

        Return strValueFieldName

    End Function

    Public Function GetTextFieldName(ByVal intLookupTable As enumLookupTable) As String

        Dim strTextFieldName As String

        Select Case intLookupTable

            Case enumLookupTable.Category
                strTextFieldName = "strCategory"

			Case enumLookupTable.Category_Code
				strTextFieldName = "idCategory"

			Case enumLookupTable.ClubFullName
				strTextFieldName = "strFullName"

			Case enumLookupTable.ClubShortName
				strTextFieldName = "strShortName"

			Case enumLookupTable.Club_Code
				strTextFieldName = "idClub"

			Case enumLookupTable.EventType
				strTextFieldName = "strEventType"

			Case enumLookupTable.EventTypeIcon
				strTextFieldName = "strEventTypeIcon"

			Case enumLookupTable.Gender
				strTextFieldName = "strGender"

			Case enumLookupTable.Technical
				strTextFieldName = "strTechnical"

			Case enumLookupTable.Technical_Code
				strTextFieldName = "idTechnical"

			Case enumLookupTable.UserName
				strTextFieldName = "strUserName"

			Case enumLookupTable.Venue
				strTextFieldName = "strName"

		End Select

        Return strTextFieldName

    End Function

    Public Function GetTableName(ByVal intLookupTable As enumLookupTable) As String

        Dim strTableName As String

        Select Case intLookupTable

            Case enumLookupTable.Category
                strTableName = "lutCategory"

			Case enumLookupTable.Category_Code
				strTableName = "lutCategory"

			Case enumLookupTable.ClubFullName
				strTableName = "lutClub"

			Case enumLookupTable.ClubShortName
				strTableName = "lutClub"

			Case enumLookupTable.Club_Code
				strTableName = "lutClub"

			Case enumLookupTable.EventType
				strTableName = "lutEventType"

			Case enumLookupTable.EventTypeIcon
				strTableName = "lutEventType"

			Case enumLookupTable.Gender
				strTableName = "lutGender"

			Case enumLookupTable.Technical
				strTableName = "lutTechnical"

			Case enumLookupTable.Technical_Code
				strTableName = "lutTechnical"

			Case enumLookupTable.UserName
				strTableName = "tblUser"

			Case enumLookupTable.Venue
				strTableName = "tblVenue"

		End Select

        Return strTableName
    End Function

    Public Function GetLookupWhereClause(ByVal intLookupTable As enumLookupTable) As String
        Dim strWhere As String

        Select Case intLookupTable


        End Select

        Return strWhere
    End Function

    Public Function GetLookupOrderClause(ByVal intLookupTable As enumLookupTable) As String
        Dim strOrder As String

        Select Case intLookupTable
            Case enumLookupTable.Gender
                strOrder = " ORDER BY idGender "
        End Select

        Return strOrder

    End Function

    Public Function AddLookupValue(ByRef condb As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable, ByVal strTextField As String) As Guid
        Dim strSQL As String
        Dim strTablename As String
        Dim strValueFiledName As String
        Dim strTextFieldName As String
        Dim guidNew As Guid

        strTablename = GetTableName(intLookupTable)
        strValueFiledName = GetValueFieldName(intLookupTable)
        strTextFieldName = GetTextFieldName(intLookupTable)
        guidNew = Guid.NewGuid

        strSQL = "INSERT INTO " & strTablename & " " & _
        "(" & strValueFiledName & " , " & strTextFieldName & ") " & _
        "VALUES (" & SQLFormat(guidNew) & " , '" & strTextField & "')"

        Try
            ExecuteSQL(condb, strSQL)
        Catch ex As Exception

        End Try

        Return guidNew
    End Function

    Public Sub EditLookupValue(ByRef condb As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable, ByVal guidValueField As Guid, ByVal strTextField As String)
        Dim strSQL As String
        Dim strTablename As String
        Dim strValueFieldName As String
        Dim strTextFieldName As String

        strValueFieldName = GetValueFieldName(intLookupTable)
        strTablename = GetTableName(intLookupTable)
        strTextFieldName = GetTextFieldName(intLookupTable)

        strSQL = "UPDATE " & strTablename & " " & _
        " SET " & strTextFieldName & " = '" & strTextField & "' WHERE " & strValueFieldName & " = " & SQLFormat(guidValueField)

        Try
            ExecuteSQL(condb, strSQL)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub EditLookupValue(ByRef condb As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable, ByVal intValueField As Integer, ByVal strTextField As String)
        Dim strSQL As String
        Dim strTablename As String
        Dim strValueFieldName As String
        Dim strTextFieldName As String

        strValueFieldName = GetValueFieldName(intLookupTable)
        strTablename = GetTableName(intLookupTable)
        strTextFieldName = GetTextFieldName(intLookupTable)

        strSQL = "UPDATE " & strTablename & " " & _
        " SET " & strTextFieldName & " = '" & strTextField & "' WHERE " & strValueFieldName & " = " & SQLFormat(intValueField)

        Try
            ExecuteSQL(condb, strSQL)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DeleteLookupValue(ByRef condb As OleDb.OleDbConnection, ByVal intLookupTable As enumLookupTable, ByVal guidValueField As Guid)
        Dim strSQL As String
        Dim strTablename As String
        Dim strValueFieldName As String

        strTablename = GetTableName(intLookupTable)
        strValueFieldName = GetValueFieldName(intLookupTable)

        strSQL = "DELETE FROM " & strTablename & " WHERE (" & strValueFieldName & " = " & SQLFormat(guidValueField) & ")"

        Try
            ExecuteSQL(condb, strSQL)
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
