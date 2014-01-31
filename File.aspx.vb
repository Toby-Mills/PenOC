Imports System.Net
Imports System.IO
Imports System.Text

Partial Class File
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

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim intFile As Integer
		Dim strURL As String
		Dim byteOut() As Byte
		Dim dtFile As DataTable
		Dim drFile As DataRow
		Dim strTitle As String

        LogPageAccess(c_conDB, Page)

		intFile = Request.Item("idFile")

		dtFile = GetTable_FileBLOB(c_conDB, WhereFileBLOB_FileID(intFile))
		If dtFile.Rows.Count > 0 Then
			drFile = dtFile.Rows(0)
			LoadDBValue(drFile.Item("strFileName"), strTitle)

			Response.Clear()
			Response.ContentType = "application/octet-stream"
			Response.AppendHeader("Content-Disposition", "attachment;filename=" & strTitle)

			'Read BLOB into the byte array
			byteOut = drFile.Item("imgFile")

			'Write the byte array to the http response
			Response.OutputStream.Write(byteOut, 0, byteOut.Length)

			'Finish the http response
			Response.End()
		Else
			WebMsgBox(Page, "File Error", "The requested file was not found")
		End If
        ShowHourGlass(Page)

	End Sub

End Class
