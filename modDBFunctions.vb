Imports System.Data.OleDb

Public Module modDBFunctions
    'This module contains functions for interacting with the database
    'The functions all take a connection object as an argument, but all share a common connectin string
    'This allows for connection pooling

    Public strConnectionString As String

    Public Function OpenDBConnection(ByRef conDB As OleDb.OleDbConnection) As Boolean
        'if the passed connection is not yet open,
        'use the common connection string to open it

        'assume success
        OpenDBConnection = True

        'create new object if needed
        If conDB Is Nothing Then
            conDB = New System.Data.OleDb.OleDbConnection
        End If

        If conDB.ConnectionString = "" Then
            conDB.ConnectionString = strConnectionString
        End If

        Select Case conDB.State
            Case Is = ConnectionState.Closed
                Try
                    'occasionally db is stuck 'connecting' - need to close before reattempting
                    conDB.Close()
                    conDB.Open()
                Catch ex As InvalidOperationException
                    'this occurs when the connection state is 'Connecting'
                    OpenDBConnection = False
                Catch ex As Exception
                    'any other exception
                    OpenDBConnection = False
                End Try
            Case Is = ConnectionState.Broken
                Try
                    conDB.Open()
                Catch ex As InvalidOperationException
                    'this occurs when the connection state is 'Connecting'
                    OpenDBConnection = False
                Catch ex As Exception
                    'any other exception
                    OpenDBConnection = False
                End Try
        End Select

    End Function

    Public Function CloseDBConnection(ByRef conDB As OleDb.OleDbConnection)
        'if the passed connection is open, close it
        If Not conDB Is Nothing Then
            If conDB.State <> ConnectionState.Closed Then
                conDB.Close()
            End If
        End If

    End Function

    Public Function ExecuteSQL(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String) As Integer
        'execute the passed SQL command - return the rows affected
        Dim com As New OleDb.OleDbCommand

            OpenDBConnection(conDB)
            com.Connection = conDB
            com.CommandText = strSQL
            ExecuteSQL = com.ExecuteNonQuery()


    End Function

    Public Function GetDataReader(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String) As System.Data.OleDb.OleDbDataReader
        'return a datareader based on the SELECT statement passed
        Dim comDataset As System.Data.OleDb.OleDbCommand

        If OpenDBConnection(conDB) Then
            comDataset = conDB.CreateCommand()
            comDataset.CommandText = strSQL
            GetDataReader = comDataset.ExecuteReader()
            comDataset = Nothing
        Else
            GetDataReader = Nothing
        End If

    End Function

    Public Function GetDataSet(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String, ByVal strTableName As String) As System.Data.DataSet
        'return a dataset, with a datatable filled with rows based on the SELECT statement passed
        Dim adapter As System.Data.OleDb.OleDbDataAdapter
        Dim dsReturn As DataSet

        OpenDBConnection(conDB)
        adapter = New OleDb.OleDbDataAdapter(strSQL, conDB)
        dsReturn = New System.Data.DataSet
        adapter.Fill(dsReturn, strTableName)

        Return dsReturn

    End Function

    Public Function GetDataTable(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String) As DataTable
        'return a datatable filled with rows based on the SQL query passed
        Dim dsDataset As DataSet
        Dim dtReturn As DataTable

        dsDataset = GetDataSet(conDB, strSQL, "return")
        dtReturn = dsDataset.Tables("return")

        Return dtReturn

    End Function

    Public Function AddDataSet(ByRef conDB As OleDb.OleDbConnection, ByRef objDataSet As Data.DataSet, ByVal strSQL As String, ByVal strTableName As String)
        'return a dataset, with a datatable filled with rows based on the SELECT statement passed
        Dim adapter As System.Data.OleDb.OleDbDataAdapter

        OpenDBConnection(conDB)
        adapter = New OleDb.OleDbDataAdapter(strSQL, conDB)
        If objDataSet Is Nothing Then
            objDataSet = New System.Data.DataSet
        End If

        adapter.Fill(objDataSet, strTableName)

    End Function



    ''' <summary>
    '''     
    ''' </summary>
    ''' <param name="strSearchValue" type="String">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <param name="strSearchField" type="String">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <param name="objDataTable" type="Data.DataTable">
    '''     <para>
    '''         
    '''     </para>
    ''' </param>
    ''' <returns>
    '''     A System.Data.DataRow value...
    ''' </returns>
    Public Function FindDataRow(ByRef conDB As OleDb.OleDbConnection, ByVal strSearchValue As String, ByVal strSearchField As String, ByVal objDataTable As Data.DataTable, Optional ByVal blnTrim As Boolean = False) As DataRow
        'search through the passed datatable for a row with the specifed value
        'return that row
        Dim objDataRow As Data.DataRow
        Dim strValue As String

        FindDataRow = Nothing
        If blnTrim Then
            strSearchValue = Trim(strSearchValue)
        End If


        For Each objDataRow In objDataTable.Rows
            strValue = objDataRow(strSearchField)
            If blnTrim Then
                strValue = Trim(strValue)
            End If
            If strValue = strSearchValue Then
                FindDataRow = objDataRow
                Exit For
            End If
        Next


    End Function


    Public Function GetLastRecord(ByRef conDB As OleDb.OleDbConnection, ByVal strTable As String, ByVal strIDField As String) As Integer
        'get the highest ID in a table
        'generally used to get the ID of the most recently added record, in a table with an autonumber

        Dim strSQL As String
        Dim drLastRecord As Data.OleDb.OleDbDataReader

        strSQL = "SELECT * FROM " & strTable & " ORDER BY " & strIDField & " DESC"
        drLastRecord = GetDataReader(conDB, strSQL)
        GetLastRecord = -1
        If drLastRecord.Read Then
            GetLastRecord = drLastRecord.Item(strIDField)
        End If
        drLastRecord.Close()
        drLastRecord = Nothing

    End Function

    ''' <summary>
    '''     Deletes rows from the database table. Constructs the SQL from parameters. Id field can be string or integer.
    '''     Will delete all rows with specified criteria so needs to be used with caution.
    ''' </summary>
    ''' <param name="strTableName" type="String">
    '''     <para>
    '''         A valid table name for the data source
    '''     </para>
    ''' </param>
    ''' <param name="strIdField" type="String">
    '''     <para>
    '''         A fieldname for specifying the criteria
    '''     </para>
    ''' </param>
    ''' <param name="strIdValue" type="String">
    '''     <para>
    '''         the value of the specified field
    '''     </para>
    ''' </param>
    ''' <param name="blAsInteger" type="Boolean">
    '''     <para>
    '''         indicates if the specified field is numeric or integer for building the sql string
    '''     </para>
    ''' </param>
    ''' <returns>
    '''     An integer value specifying the number of records deleted
    ''' </returns>
    Public Function DeleteDataRows(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strIdField As String, ByVal strIdValue As String, ByVal blAsInteger As Boolean) As Integer
        'Delete all records in a table meeting the criteria
        Dim strSQL As String
        Dim intCount As Integer

        If blAsInteger = True Then
            strSQL = "DELETE FROM " & strTableName & " WHERE " & strIdField & "=" & strIdValue
        ElseIf blAsInteger = False Then
            strSQL = "DELETE FROM " & strTableName & " WHERE " & strIdField & "='" & strIdValue & "'"
        End If

        DeleteDataRows = ExecuteSQL(conDB, strSQL)

    End Function
    Public Function DeleteDataRows(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strIdField As String, ByVal intIDValue As Integer) As Integer
        'Delete all records in a table meeting the criteria
        Dim strSQL As String
        Dim intCount As Integer

        strSQL = "DELETE FROM " & strTableName & " WHERE " & strIdField & "=" & intIDValue

        DeleteDataRows = ExecuteSQL(conDB, strSQL)

    End Function
    Public Function DeleteDataRows(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strIdField As String, ByVal guidValue As Guid) As Integer
        'Delete all records in a table meeting the criteria
        Dim strSQL As String
        Dim intCount As Integer

        strSQL = "DELETE FROM " & strTableName & " WHERE " & strIdField & "= " & SQLFormat(guidValue)

        DeleteDataRows = ExecuteSQL(conDB, strSQL)

    End Function
    Public Function DeleteDataRows(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String) As Integer

        ExecuteSQL(conDB, "DELETE FROM [" & strTableName & "]")

    End Function

    Public Function LookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal strValueField As String, ByVal strTextField As String, ByVal strTableName As String, ByVal strSearchText As String, ByVal blAsInteger As Boolean) As String
        'look up a value from a lookup table in the database
        Dim drLookup As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        LookupValue = Nothing

        If blAsInteger = True Then
            strSQL = "SELECT " & strTextField & " FROM " & strTableName & " WHERE " & strValueField & "=" & strSearchText
        ElseIf blAsInteger = False Then
            strSQL = "SELECT " & strTextField & " FROM " & strTableName & " WHERE " & strValueField & "='" & strSearchText & "'"
        End If

        LookupValue = LookupValue(conDB, strSQL)

    End Function
    Public Function LookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String) As Object
        'look up a value from the database using the provided SQL String
        Dim drLookup As Data.OleDb.OleDbDataReader

        LookupValue = Nothing

        Try
            drLookup = GetDataReader(conDB, strSQL)
            If drLookup.Read() Then
                LookupValue = drLookup.Item(0)
            End If
        Catch ex As Exception
            LookupValue = Nothing
        Finally
            If Not drLookup Is Nothing Then
                drLookup.Close()
            End If
        End Try

    End Function
    Public Function LookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal strValueField As String, ByVal strTextField As String, ByVal strTableName As String, ByVal intSearchValue As Integer) As Object
        'look up a value from a lookup table in the database
        Dim drLookup As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        strSQL = "SELECT " & strTextField & " FROM " & strTableName & " WHERE " & strValueField & "=" & intSearchValue

        LookupValue = LookupValue(conDB, strSQL)

    End Function
    Public Function LookupValue(ByRef conDB As OleDb.OleDbConnection, ByVal strValueField As String, ByVal strTextField As String, ByVal strTableName As String, ByVal guidSearchValue As Guid) As Object
        'look up a value from a lookup table in the database
        Dim drLookup As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        strSQL = "SELECT " & strTextField & " FROM " & strTableName & " WHERE " & strValueField & "=" & SQLFormat(guidSearchValue)

        LookupValue = LookupValue(conDB, strSQL)

    End Function

    Public Function ExistsInTable(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strFieldName As String, ByVal strFieldValue As String) As Boolean

        Dim drTable As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        strSQL = "SELECT " & strFieldName & " FROM " & strTableName & " WHERE " & strFieldName & "='" & strFieldValue & "'"
        drTable = GetDataReader(conDB, strSQL)
        ExistsInTable = drTable.Read()
        drTable.Close()

    End Function
    Public Function ExistsInTable(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strFieldName As String, ByVal intFieldValue As Integer) As Boolean

        Dim drTable As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        strSQL = "SELECT " & strFieldName & " FROM " & strTableName & " WHERE " & strFieldName & "=" & CStr(intFieldValue)
        drTable = GetDataReader(conDB, strSQL)
        ExistsInTable = drTable.Read()
        drTable.Close()

    End Function

    Public Function ExistsInTable(ByRef conDB As OleDb.OleDbConnection, ByVal strTableName As String, ByVal strFieldName As String, ByVal strGuidFieldValue As Guid) As Boolean

        Dim drTable As Data.OleDb.OleDbDataReader
        Dim strSQL As String

        strSQL = "SELECT " & strFieldName & " FROM " & strTableName & " WHERE " & strFieldName & "= " & SQLFormat(strGuidFieldValue)
        Try
            drTable = GetDataReader(conDB, strSQL)
            ExistsInTable = drTable.Read()
        Catch ex As Exception
        Finally
            If Not drTable Is Nothing Then
                drTable.Close()
            End If
        End Try

    End Function

    Public Sub PopulateHashTable(ByRef conDB As OleDb.OleDbConnection, ByRef hashTable As Hashtable, ByVal strSQL As String)
        Dim drResult As OleDb.OleDbDataReader

        If hashTable Is Nothing Then
            hashTable = New Hashtable
        End If

        Try
            drResult = GetDataReader(conDB, strSQL)
            Do While drResult.Read
                hashTable.Add(drResult.Item(0), drResult.Item(1))
            Loop
        Catch ex As Exception
        Finally
            If Not drResult Is Nothing Then
                drResult.Close()
                drResult = Nothing
            End If
        End Try


    End Sub

    Public Sub PopulateHashTable(ByRef conDB As OleDb.OleDbConnection, ByRef hashTable As Hashtable, ByVal strTableName As String, ByVal strKeyField As String, ByVal strValueField As String)
        Dim strSQL As String

        strSQL = "SELECT " & strKeyField & ", " & strValueField & " FROM " & strTableName
        PopulateHashTable(conDB, hashTable, strSQL)

    End Sub

End Module
