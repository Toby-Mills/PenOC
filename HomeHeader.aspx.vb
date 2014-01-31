Partial Class HomeHeader
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
	Protected WithEvents Image1 As System.Web.UI.WebControls.Image

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
		Dim intPhoto(5)
		Dim intCount1 As Integer
		Dim intCount2 As Integer
		Dim rndPhoto As Random
		Dim intTotalPhotos As Integer
		Dim intTest As Integer
		Dim blnUsed As Boolean

        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
        End If

        intTotalPhotos = PhotoCount()

        rndPhoto = New Random

        For intCount1 = 1 To 5
            blnUsed = False
            Do

                intTest = rndPhoto.Next(0, intTotalPhotos)
                blnUsed = False
                For intCount2 = 1 To 5
                    If intPhoto(intCount2) = intTest Then
                        blnUsed = True
                    End If
                Next

            Loop While blnUsed
            intPhoto(intCount1) = intTest

        Next

        Me.img1.ImageUrl = GetPhoto(intPhoto(1))
        Me.img2.ImageUrl = GetPhoto(intPhoto(2))
        Me.img3.ImageUrl = GetPhoto(intPhoto(3))
        Me.img4.ImageUrl = GetPhoto(intPhoto(4))
        Me.img5.ImageUrl = GetPhoto(intPhoto(5))

	End Sub

	Private Function PhotoCount() As Integer
		Dim folder As System.IO.Directory

		Return UBound(folder.GetFiles(Server.MapPath("images/header")))

	End Function

	Private Function GetPhoto(ByVal intPhoto As Integer)
		Dim strFileName() As String
		Dim folder As System.IO.Directory
		Dim strReturn As String

		strFileName = folder.GetFiles(Server.MapPath("images/header"))

		If intPhoto <= UBound(strFileName) Then
			strReturn = "images/header/" & FileName(strFileName(intPhoto))
		End If

		Return (strReturn)

	End Function
End Class
