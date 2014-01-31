Partial Class UploadFile
	Inherits System.Web.UI.UserControl

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

	Public Event Uploaded()
	Public Event Cancelled()

	Public Property FileID() As Integer
		Get
			FileID = CInt(Me.txtID.Text)
		End Get
		Set(ByVal Value As Integer)
			Me.txtID.Text = Value
		End Set
	End Property

	Public Property Description() As String
		Get
			Description = Me.txtDescription.Text
		End Get
		Set(ByVal Value As String)
			Me.txtDescription.Text = Value
		End Set

	End Property
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not Page.IsPostBack Then
			Me.txtID.Text = Request.Item("idFile")
		End If
	End Sub

	Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
		Dim strPath As String
		Dim fileTemp As System.IO.FileInfo
		Dim intFile As Integer
		Dim blnSuccess As Boolean

		blnSuccess = True

		If Not file1.PostedFile Is Nothing Then
			If file1.PostedFile.FileName > "" Then
				If PathExists(Server.MapPath("Temp"), True) Then
					strPath = Server.MapPath("Temp") & "\" & FileName(file1.PostedFile.FileName)
					file1.PostedFile.SaveAs(strPath)
					If Me.txtID.Text > "" Then
						PenOCDB.UpdateFile(c_conDB, Me.txtID.Text, strPath, txtDescription.Text)
					Else
						intFile = PenOCDB.UploadFile(c_conDB, strPath, txtDescription.Text)
					End If
					fileTemp = New System.IO.FileInfo(strPath)
					fileTemp.Delete()
				End If
			Else
				blnSuccess = False
			End If
		Else
			blnSuccess = False
		End If

		Me.lblUploadValidation.Visible = Not (blnSuccess)

		If blnSuccess Then
			RaiseEvent Uploaded()
		End If

	End Sub


	Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
		RaiseEvent Cancelled()
	End Sub
End Class
