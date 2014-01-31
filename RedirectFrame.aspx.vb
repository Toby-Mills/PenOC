Partial Class RedirectFrame
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

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim strFrame As String
		Dim strPath As String

		strFrame = Session.Item("frame")
		strPath = Session.Item("redirect")

		Session.Remove("frame")
		Session.Remove("redirect")

		If strFrame = "parent" Then
			modFunctions_WebForms.RedirectFrameParent(Page, strPath)
		Else
			modFunctions_WebForms.RedirectFrame(Page, strFrame, strPath)
		End If
        ShowHourGlass(Page)

	End Sub

End Class
