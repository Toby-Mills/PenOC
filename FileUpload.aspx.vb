Public Class FileUpload
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents file1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents file2 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents file3 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents cmdDirectoryImages As System.Web.UI.WebControls.Button
    Protected WithEvents cmdDirectoryNewsImages As System.Web.UI.WebControls.Button
    Protected WithEvents cmdUpload As System.Web.UI.WebControls.Button
    Protected WithEvents cmdDirectoryMinutes As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

	Public Const DIRECTORY_IMAGES = "images"
	Public Const DIRECTORY_NEWSIMAGES = "images/news"
	Public Const DIRECTORY_MINUTES = "downloads/minutes"

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim strDirectory As String

		If Not Page.IsPostBack Then
			strDirectory = Request.Item("directory")
			Me.txtPath.Text = strDirectory
		End If

		cmdDirectoryImages.Attributes("language") = "vbscript"
		cmdDirectoryImages.Attributes("onclick") = "txtPath.value=""" & DIRECTORY_IMAGES & """" & vbCrLf & "window.event.returnValue = false"

		cmdDirectoryNewsImages.Attributes("language") = "vbscript"
		cmdDirectoryNewsImages.Attributes("onclick") = "txtPath.value=""" & DIRECTORY_NEWSIMAGES & """" & vbCrLf & "window.event.returnValue = false"

		cmdDirectoryMinutes.Attributes("language") = "vbscript"
		cmdDirectoryMinutes.Attributes("onclick") = "txtPath.value=""" & DIRECTORY_MINUTES & """" & vbCrLf & "window.event.returnValue = false"

	End Sub

	Private Sub cmdupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
		Dim file As System.Web.HttpPostedFile

		file = Me.file1.PostedFile
		If Not file.FileName = "" Then
			file.SaveAs(Server.MapPath(Me.txtPath.Text) & "/" & FileName(file.FileName))
		End If

		file = Me.file2.PostedFile
		If Not file.FileName = "" Then
			file.SaveAs(Server.MapPath(Me.txtPath.Text) & "/" & FileName(file.FileName))
		End If

		file = Me.file3.PostedFile
		If Not file.FileName = "" Then
			file.SaveAs(Server.MapPath(Me.txtPath.Text) & "/" & FileName(file.FileName))
		End If

		If Request.Item("autoclose") = "true" Then
			CloseWebWindow(Page)
		Else
			WebMsgBox(Page, "Upload Complete", "File upload complete")
		End If

	End Sub

	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		CloseWebWindow(Page)
	End Sub
End Class
