'This module is dependent on modDBFunctions. Please include modDBFunctions in this project.

Public Module modDB_WebForms

    Public Sub PopulateCombobox(ByRef conDB As OleDb.OleDbConnection, ByVal cmbTarget As System.Web.UI.WebControls.DropDownList, ByVal strValueField As String, ByVal strTextField As String, ByVal strFROM As String, ByVal blnRequired As Boolean)

        'retrieve data from the database and populate the combobox

        Dim objDataReader As OleDb.OleDbDataReader
        Dim strSQL As String

        'if the from clause is a full SELECT statement, use that
        If UCase(Left(strFROM, 6)) = "SELECT" Then
            strSQL = strSQL & strFROM
        Else
            'otherwise build a SELECT statement, using the 2 field names & table name provided
            strSQL = strSQL & "SELECT " & strValueField & ", " & strTextField & " FROM " & strFROM
            strSQL = strSQL & " ORDER BY " & strTextField
        End If

        'retrieve the data & bind to the combobox
        With cmbTarget
            Try
                objDataReader = GetDataReader(conDB, strSQL)
                .DataSource = objDataReader
                .DataValueField = strValueField
                .DataTextField = strTextField
                .DataBind()
            Catch ex As Exception
            Finally
                If Not objDataReader Is Nothing Then
                    objDataReader.Close()
                End If
            End Try
        End With

        'insert a blank item at the top of the list
        If Not blnRequired Then
            cmbTarget.Items.Insert(0, New System.Web.UI.WebControls.ListItem("", 0))
        End If

    End Sub


    Public Sub PopulateCheckboxlist(ByRef conDB As OleDb.OleDbConnection, ByVal cblTarget As System.Web.UI.WebControls.CheckBoxList, ByVal strValueField As String, ByVal strTextField As String, ByVal strFROM As String)

        'retrieve data from the database and populate the combobox

        Dim objDataReader As OleDb.OleDbDataReader
        Dim strSQL As String

        'if the from clause is a full SELECT statement, use that
        If UCase(Left(strFROM, 6)) = "SELECT" Then
            strSQL = strSQL & strFROM
        Else
            'otherwise build a SELECT statement, using the 2 field names & table name provided
            strSQL = strSQL & "SELECT " & strValueField & ", " & strTextField & " FROM " & strFROM
            strSQL = strSQL & " ORDER BY " & strTextField
        End If

        'retrieve the data & bind to the combobox
        With cblTarget
            Try
                objDataReader = GetDataReader(conDB, strSQL)
                .DataSource = objDataReader
                .DataValueField = strValueField
                .DataTextField = strTextField
                .DataBind()
            Catch ex As Exception
            Finally
                If Not objDataReader Is Nothing Then
                    objDataReader.Close()
                End If
            End Try


        End With

    End Sub

    Public Function BindDataGrid(ByRef conDB As OleDb.OleDbConnection, ByVal strSQL As String, ByVal dgTarget As System.Web.UI.WebControls.DataGrid) As Integer
        'use the SQL statement to retrieve a datatable from the database
        'bind the datagrid to that datatable

        Dim objAdapter As New OleDb.OleDbDataAdapter
        Dim objDataset As DataSet

        OpenDBConnection(conDB)

        Dim objCommand As New OleDb.OleDbCommand(strSQL, conDB)
        objCommand.CommandType = CommandType.Text
        objAdapter.SelectCommand = objCommand
        objDataset = New DataSet(0)
        objAdapter.Fill(objDataset)

        dgTarget.DataSource = objDataset.Tables(0).DataSet
        dgTarget.DataBind()

        BindDataGrid = objDataset.Tables(0).Rows.Count()

    End Function

End Module
