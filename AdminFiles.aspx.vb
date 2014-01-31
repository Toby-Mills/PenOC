Partial Class AdminFiles
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
	Protected WithEvents Filelist As FileList
	Protected WithEvents UploadFile As UploadFile

	Private c_condb As OleDb.OleDbConnection

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_condb, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            With Filelist
                .DisplayColumn(Filelist.enumFileListColumn.FileID) = True
                .DisplayColumn(Filelist.enumFileListColumn.Delete) = True
                .DisplayColumn(Filelist.enumFileListColumn.Update) = True
                .FileTable = PenOCDB.GetTable_File(c_condb, "")
            End With
        End If
        ShowHourGlass(Page)
	End Sub

	Private Sub FileList_FileSelected() Handles Filelist.FileSelected

		Response.Redirect("File.aspx?idFile=" & Filelist.SelectedFile)

	End Sub

	Private Sub FileList_FileDeleted() Handles Filelist.FileDeleted

		ConfirmDelete.PopupConfirmDelete(Page, ConfirmDelete.enumDeleteType.File, Filelist.SelectedFile)

	End Sub

	Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

		Me.UploadFile.Visible = True

	End Sub

	Private Sub UploadFile_Cancelled() Handles UploadFile.Cancelled

		Me.UploadFile.Visible = False

	End Sub

	Private Sub UploadFile_Uploaded() Handles UploadFile.Uploaded

		Filelist.FileTable = PenOCDB.GetTable_File(c_condb, "")
		UploadFile.Visible = False

	End Sub

	Private Sub FileList_FileUpdated() Handles Filelist.FileUpdated

		Me.UploadFile.FileID = Me.Filelist.SelectedFile
		Me.UploadFile.Visible = True

	End Sub
End Class
