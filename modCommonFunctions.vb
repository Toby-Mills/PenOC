
Imports System.Globalization


Public Module modCommonFunctions

    Public Const NULL_NUMBER As Integer = -9999

    Public Function GetIndexByText(ByVal ctlControl As Object, ByVal strText As String) As Integer
        'Takes a combobox and a value. Searches the list of items for the first one with the specified text (rather than value)
        'returns the index of that item
        Dim intcount As Integer

        GetIndexByText = -1
        For intcount = 0 To ctlControl.Items.Count - 1
            If ctlControl.Items(intcount).text = strText Then
                GetIndexByText = intcount
                Exit Function
            End If
        Next
    End Function

    Public Function RightOf(ByVal theString As String, ByVal theKey As String) As String
        'returns the sub string to the right of the first occurance of theKey
        'check starts from the right side - if not found returns the same string
        'example: rightof("xxx\yyyy\zzzz","\") returns "zzzz"

        Dim l As Integer, lk As Integer, a As Integer

        RightOf = theString 'default return
        Try
            l = Len(theString)
            lk = Len(theKey)
            a = InStrRev(theString, theKey, -1, vbTextCompare) 'InStr(1, str, tabstr, 1)
            If (a > 0) Then
                RightOf = Right(theString, l - (a + lk - 1))
            Else
                RightOf = theString
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function LeftOf(ByVal theString As String, ByVal theKey As String) As String
        'returns the sub string left of theKey
        'search for theKey starts from the left side of the string
        'example: leftof("xxx\yyyy\zzzz","\") returns "xxx"

        Dim l As Long, lk As Long, a As Long

        Try
            LeftOf = theString 'default return
            l = Len(theString)
            lk = Len(theKey)
            a = InStr(1, theString, theKey, 1) 'InStr(1, str, tabstr, 1)
            If (a > 0) Then
                LeftOf = Left(theString, a - 1)
            Else
                LeftOf = theString
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Sub Info(ByVal Message As String)
        'displays an information message
        MsgBox(Message, MsgBoxStyle.Information, "Information")
    End Sub

    Public Sub Warning(ByVal Message As String)
        'displays an warning message
        MsgBox(Message, MsgBoxStyle.Exclamation, "Warning")
    End Sub

    Public Sub ErrorMsg(ByVal Message As String, Optional ByVal RoutineName As String = "")
        'displays a error message information message
        If Len(Message) > 200 Then Message = Left(Message, 200) & "..."
        If RoutineName = "" Then
            MsgBox(Message, MsgBoxStyle.Critical, "Error ")
        Else
            MsgBox(Message, MsgBoxStyle.Critical, "Error (" & RoutineName & ")")
        End If
    End Sub

    Public Function NewGUID() As String

        NewGUID = Guid.NewGuid().ToString()

    End Function

    Public Function StringToGUID(ByVal InString As String) As Guid
        Dim objConverter As New System.ComponentModel.GuidConverter
        Dim guidOut As Guid

        Try
            guidOut = objConverter.ConvertFromString(InString)
        Catch ex As Exception
            Return Nothing
        End Try
        Return guidOut

    End Function

    Public Function GetValues(ByRef conDb As OleDb.OleDbConnection, ByVal cmbTarget As System.Web.UI.WebControls.DropDownList, ByVal strField As String, ByVal strTable As String, ByVal strValue As String, _
        Optional ByVal blnUpper As Boolean = True, Optional ByVal blnSort As Boolean = True, _
        Optional ByVal strSQLstring As String = Nothing)
        'Function used to Populate a combobox with data from the database witha query using a LIKE clause
        'blSort if true will sort the list box in descending order
        'Alfie 15/02/2005

        'retrieve data from database and populate combo box
        Dim objDataReader As OleDb.OleDbDataReader
        Dim strSql As String

        If blnUpper Then
            strValue = strValue.ToUpper()
        End If

        'if the from clause is a full SELECT statement, use that
        If UCase(Left(strSQLstring, 6)) = "SELECT" Then
            strSql = strSql & strSQLstring
        Else
            'build a SELECT statement, using the table name, field name and value name
            strSql = strSql & "SELECT " & strField & " FROM " & strTable
            strSql = strSql & " WHERE (" & strField & " LIKE '" & strValue & "%')"
        End If

        'Retrieve the data and bind to the dropdownlist
        With cmbTarget
            Try
                objDataReader = GetDataReader(conDb, strSql)
                .DataSource = objDataReader
                .DataValueField = strField
                .DataTextField = strField
                .DataBind()
            Catch ex As Exception
            Finally
                If Not objDataReader Is Nothing Then
                    objDataReader.Close()
                End If
            End Try

        End With

        'sort the listbox in descending order
        If blnSort Then
            SortComboBox(cmbTarget, False)
        End If

    End Function

#Region "AccessFormat"

    Public Function AccessFormat(ByVal dteDate As Date) As String
        If dteDate = Date.MinValue Then
            AccessFormat = "NULL"
        Else
            AccessFormat = "#" & dteDate.ToString("MM/dd/yyyy HH:mm:ss") & "#"
            AccessFormat = "#" & dteDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat) & "#"
        End If
    End Function

    Public Function AccessFormat(ByVal intInteger As Integer) As String
        If intInteger = NULL_NUMBER Then
            AccessFormat = "NULL"
        Else
            AccessFormat = intInteger.ToString
        End If
    End Function

    Public Function AccessFormat(ByVal decDecimal As Decimal) As String
        If decDecimal = NULL_NUMBER Then
            AccessFormat = "NULL"
        Else
            AccessFormat = decDecimal.ToString(CultureInfo.InvariantCulture.NumberFormat)
        End If
    End Function

    Public Function AccessFormat(ByVal guidGuid As Guid) As String
        AccessFormat = "{guid {" & guidGuid.ToString & "}}"
    End Function

    Public Function AccessFormat(ByVal strString As String) As String
        If strString Is Nothing Then
            AccessFormat = "NULL"
        Else
            strString = strString.Replace("'", "''")
            AccessFormat = "'" & strString & "'"
        End If
    End Function

    Public Function AccessFormat(ByVal blnBoolean As Boolean) As String
        AccessFormat = blnBoolean.ToString
    End Function

#End Region


#Region "SQLFormat"

    Public Function SQLFormatTrim(ByVal strSQL As String) As String
        'Function accepts a string that is to be trimmed, formats
        'the "trim" statement to be accepted by SQL, using the LTRIM 
        'and RTRIM functions

        If strSQL = "" Then
            SQLFormatTrim = ""
        Else
            SQLFormatTrim = "LTRIM(RTRIM(" & strSQL & "))"
        End If

    End Function

    Public Function SQLFormat(ByVal dteDate As Date) As String
        If dteDate = Date.MinValue Then
            SQLFormat = "NULL"
        Else
            If dteDate.Year = 1 And dteDate.Month = 1 And dteDate.Day = 1 Then
                SQLFormat = "CONVERT(DATETIME, '" & dteDate.ToString("HH:mm:ss") & "', 108)"
            Else
                SQLFormat = "CONVERT(DATETIME, '" & dteDate.ToString("yyyy-MM-dd HH:mm:ss") & "', 102)"
            End If
            'AccessFormat = "#" & dteDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat) & "#"
        End If
    End Function

    Public Function SQLFormat(ByVal intInteger As Integer) As String
        If intInteger = NULL_NUMBER Then
            SQLFormat = "NULL"
        Else
            SQLFormat = intInteger.ToString
        End If
    End Function

    Public Function SQLFormat(ByVal decDecimal As Decimal) As String
        If decDecimal = NULL_NUMBER Then
            SQLFormat = "NULL"
        Else
            SQLFormat = decDecimal.ToString(CultureInfo.InvariantCulture.NumberFormat)
        End If
    End Function

    Public Function SQLFormat(ByVal guidGuid As Guid) As String
        If guidGuid.Equals(Guid.Empty) Then
            SQLFormat = "NULL"
        Else
            SQLFormat = "'{" & guidGuid.ToString & "}'"
        End If

    End Function

    Public Function SQLFormat(ByVal strString As String) As String
        If strString Is Nothing Then
            SQLFormat = "NULL"
        Else
            strString = strString.Replace("'", "''")
            SQLFormat = "'" & strString & "'"
        End If
    End Function

    Public Function SQLFormat(ByVal blnBoolean As Boolean) As String
        If blnBoolean Then
            SQLFormat = "1"
        Else
            SQLFormat = "0"
        End If
    End Function

#End Region

#Region "LoadDBValue"

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef dteDate As Date)
        If TypeOf objObject Is DBNull Then
            dteDate = Date.MinValue
        Else
            dteDate = objObject
        End If
    End Sub

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef intInteger As Integer)
        If TypeOf objObject Is DBNull Then
            intInteger = NULL_NUMBER
        Else
            intInteger = objObject
        End If
    End Sub

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef decDecimal As Decimal)
        If TypeOf objObject Is DBNull Then
            decDecimal = NULL_NUMBER
        Else
            decDecimal = objObject
        End If
    End Sub

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef strString As String)
        If TypeOf objObject Is DBNull Then
            strString = ""
        Else
            strString = objObject
        End If
    End Sub

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef guidGUID As Guid)
        If TypeOf objObject Is DBNull Then
            guidGUID = Guid.Empty
        Else
            guidGUID = objObject
        End If
    End Sub

    Public Sub LoadDBValue(ByVal objObject As Object, ByRef objVariable As Object)
        If TypeOf objObject Is DBNull Then
            objVariable = Nothing
        Else
            objVariable = objObject
        End If
    End Sub

#End Region

#Region "DisplayDBValue"
    Public Function DisplayDBValue(ByVal intInteger As Integer) As String
        If intInteger = NULL_NUMBER Then
            DisplayDBValue = ""
        Else
            DisplayDBValue = intInteger.ToString
        End If
    End Function

    Public Function DisplayDBValue(ByVal decDecimal As Decimal) As String
        If decDecimal = NULL_NUMBER Then
            DisplayDBValue = ""
        Else
            DisplayDBValue = decDecimal.ToString(CultureInfo.InvariantCulture.NumberFormat)
        End If
    End Function

    Public Function DisplayDBValue(ByVal dteDate As Date, Optional ByVal strFormat As String = "")
        If dteDate = Date.MinValue Then
            DisplayDBValue = ""
        Else
            If strFormat > "" Then
                DisplayDBValue = Format(dteDate, strFormat)
            Else
                DisplayDBValue = Format(dteDate, "dd MMM yyyy")
            End If
        End If
    End Function
#End Region

    Public Function FileName(ByVal strPath As String) As String
        Dim strToken() As String

        strToken = strPath.Split(CChar("/"), CChar("\"))
        FileName = strToken(UBound(strToken))
    End Function

    Public Function FilePath(ByVal strPath As String) As String
        Dim strFileName As String

        strFileName = FileName(strPath)
        If Len(strPath) > Len(strFileName) + 1 Then
            FilePath = Left(strPath, Len(strPath) - (Len(strFileName) + 1))
        Else
            FilePath = ""
        End If

    End Function

    Public Function PathExists(ByVal strPath As String, ByVal blnCreateIfMissing As Boolean) As Boolean
        Dim dir As System.IO.DirectoryInfo

        PathExists = False

        dir = New System.IO.DirectoryInfo(strPath)
        If dir.Exists Then
            PathExists = True
        Else
            If blnCreateIfMissing = True Then
                Try
                    dir.Create()
                    dir = New System.IO.DirectoryInfo(strPath)
                    If dir.Exists Then
                        PathExists = True
                    End If
                Catch ex As Exception
                    PathExists = False
                End Try
            End If
        End If

    End Function

    Public Function FilePathToURL(ByVal strPath As String) As String
        Dim strURL As String

        strURL = strPath.Replace("\", "/")
        strURL = "File:///" & strURL

        Return strURL

    End Function

    Public Function StringArrayToArrayList(ByVal strArray() As String) As ArrayList
        Dim i As Integer
        Dim retArrayList As New ArrayList
        For i = 0 To strArray.Length

        Next
    End Function

    Public Function IsStringAnDouble(ByVal strInput As String) As Boolean
        Dim dblParse As Double
        Dim blnIsInteger As Boolean
        'try to convert value to int by choosing the middle value
        Try
            dblParse.Parse(strInput)
            blnIsInteger = True
        Catch ex As Exception
            blnIsInteger = False
        End Try

        Return blnIsInteger
    End Function

    Public Function Extension(ByVal strFileName As String) As String

        Dim strName() As String

        strName = Split(strFileName, ".")
        Extension = strName(UBound(strName))

	End Function

	Public Function GetFileAsByteArray(ByVal strFilePath As String) As Byte()
		Dim filestream As IO.FileStream
		Dim binaryreader As IO.BinaryReader
		Dim byteFile() As Byte

		filestream = New IO.FileStream(strFilePath, IO.FileMode.Open, IO.FileAccess.Read)
		binaryreader = New IO.BinaryReader(filestream)
		byteFile = binaryreader.ReadBytes(filestream.Length)

		binaryreader.Close()
		filestream.Close()

		Return byteFile
	End Function

End Module
