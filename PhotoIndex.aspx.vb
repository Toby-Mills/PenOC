Partial Class PhotoIndex
    Inherits System.Web.UI.Page

    Private c_conDb As OleDb.OleDbConnection
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
        Dim intEvent As Integer

        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            If Page.Session.Item(SESSION_PHOTO_EVENT_ID) > 0 Then
                intEvent = CInt(Page.Session.Item(SESSION_PHOTO_EVENT_ID))
                LoadThumbnails(intEvent)
            End If
        End If
    End Sub

    Private Sub LoadThumbnails(ByVal intEvent As Integer)
        Dim intThumbnails As Integer
        Dim intCount As Integer
        Dim strLiteral As String
        Dim blnFirstPhotoFound As Boolean

        intCount = 0
        intThumbnails = CountThumbnails(intEvent)
        If intThumbnails = 0 Then
            strLiteral = "No photos available"
        Else
            Do While intCount <= intThumbnails
                If GetThumbnailURL(intEvent, intCount) > "" Then
                    strLiteral &= "<A Target='photo' HREF='" & GetImageURL(intEvent, intCount) & "'><IMG id='photo_'" & intCount & " SRC='" & GetThumbnailURL(intEvent, intCount) & "' /></A><BR />"
                    If blnFirstPhotoFound = False Then
                        blnFirstPhotoFound = True
                        modFunctions_WebForms.RedirectFrame(Page, "photo", GetImageURL(intEvent, intCount))
                    End If
                End If
                intCount += 1
            Loop
        End If

        Me.litThumbnails.Text = strLiteral

    End Sub

    Private Function CountThumbnails(ByVal intEvent As Integer) As Integer
        Dim folder As System.IO.Directory

        If folder.Exists(Server.MapPath("photos/events/" & intEvent & "/thumbnails")) Then
            Return folder.GetFiles(Server.MapPath("photos/events/" & intEvent & "/thumbnails")).Length
        Else
            Return 0
        End If

    End Function

    Private Function GetThumbnailURL(ByVal intEvent As Integer, ByVal intPhoto As Integer)
        Dim strFileName() As String
        Dim folder As System.IO.Directory
        Dim strReturn As String

        strFileName = folder.GetFiles(Server.MapPath("photos/events/" & intEvent & "/thumbnails"))

        If intPhoto <= UBound(strFileName) Then
            If Not FileName(strFileName(intPhoto)) = "Thumbs.db" Then
                strReturn = "photos/events/" & intEvent & "/thumbnails/" & FileName(strFileName(intPhoto))
            End If
        End If

        Return (strReturn)

    End Function
    Private Function GetImageURL(ByVal intEvent As Integer, ByVal intPhoto As Integer)
        Dim strFileName() As String
        Dim folder As System.IO.Directory
        Dim strReturn As String

        strFileName = folder.GetFiles(Server.MapPath("photos/events/" & intEvent & "/thumbnails"))

        If intPhoto <= UBound(strFileName) Then
            strReturn = "photos/events/" & intEvent & "/images/" & FileName(strFileName(intPhoto))
        End If

        Return (strReturn)

    End Function
End Class
