Partial Class UploadImage
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

	Public Const DIRECTORY_IMAGES = "images"
	Public Const DIRECTORY_NEWSIMAGES = "images/news"

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim strDirectory As String

        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            strDirectory = Request.Item("directory")
            Me.txtPath.Text = strDirectory
        End If
        ShowHourGlass(Page)

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
