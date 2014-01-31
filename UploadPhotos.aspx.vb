Imports PenOC.modFileUtilities

Partial Class UploadPhotos
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

        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
        End If
        Me.txtEventID.Width = New Web.UI.WebControls.Unit(0)

        EventSearchPopup.ShowEventSearch(Me.cmdEventSearch, Me.txtEventID, Me.txtEvent, True, "Select event to upload photos for", False)
        ShowHourGlass(Page)
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim strError As String

        Select Case True
            Case Me.txtEventID.Text = ""
                WebMsgBox(Page, "Select Event", "Please select an event for which to upload photos")
            Case Me.filePhotos.PostedFile.FileName = ""
                WebMsgBox(Page, "Select Zip file", "Please select the zip file of photos to upload")
            Case System.IO.Path.GetExtension(Me.filePhotos.PostedFile.FileName) <> ".zip"
                WebMsgBox(Page, "Select Zip file", "Please select the zip file of photos to upload")
            Case Else
                SaveTempFile()
                strError = UnzipToPhotoDirectory()
                If strError > "" Then
                    WebMsgBox(Page, "Upload Error", strError)
                Else
                    If Me.chkAutoURL.Checked Then
                        PenOCDB.SetEvent_PhotosURL(c_conDB, txtEventID.Text, "auto")
                    End If
                    WebMsgBox(Page, "Upload complete", "Upload complete")
                End If
                DeleteTempFile()
        End Select

    End Sub

    Private Sub SaveTempFile()

        Dim filePhotos As System.Web.HttpPostedFile
        Dim strPath As String

        If PathExists(Server.MapPath("temp"), True) Then
            filePhotos = Me.filePhotos.PostedFile()
            strPath = Server.MapPath("temp") & "\" & System.IO.Path.GetFileName(filePhotos.FileName)
            filePhotos.SaveAs(strPath)
        End If

    End Sub

    Private Sub DeleteTempFile()
        Dim fileTemp As System.IO.FileInfo
        Dim filePhotos As System.Web.HttpPostedFile
        Dim strPath As String

        Try
            filePhotos = Me.filePhotos.PostedFile()
            strPath = Server.MapPath("temp") & "\" & System.IO.Path.GetFileName(filePhotos.FileName)
            fileTemp = New System.IO.FileInfo(strPath)
            If fileTemp.Exists Then
                fileTemp.Delete()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function UnzipToPhotoDirectory() As String

        Dim strPath As String
        Dim dirPhotos As System.IO.DirectoryInfo
        Dim dirEvents As System.IO.DirectoryInfo
        Dim dirEvent As System.IO.DirectoryInfo
        Dim strError As String

        dirPhotos = New System.IO.DirectoryInfo(Server.MapPath("photos\"))
        If Not dirPhotos.Exists Then
            If Not CreateFolder(Server.MapPath(""), "photos") Then
                strError = Server.MapPath("photos\") & " folder does not exist and could not be created."
                GoTo exitfunction
            End If
        End If

        dirEvents = New System.IO.DirectoryInfo(Server.MapPath("photos\events\"))
        If Not dirEvents.Exists Then
            If Not CreateFolder(Server.MapPath("photos\"), "events") Then
                strError = Server.MapPath("photos\events\") & " folder does not exist and could not be created."
                GoTo exitfunction
            End If
        End If

        dirEvent = New System.IO.DirectoryInfo(Server.MapPath("photos\events\" & Me.txtEventID.Text))
        If Not dirEvent.Exists Then
            If Not CreateFolder(Server.MapPath("photos\events\"), Me.txtEventID.Text) Then
                strError = Server.MapPath("photos\events\" & Me.txtEventID.Text) & " folder does not exist and could not be created."
                GoTo exitfunction
            End If
        End If

        strPath = Server.MapPath("photos\events\" & Me.txtEventID.Text)

        strError = UnzipFile(Server.MapPath("temp") & "\" & System.IO.Path.GetFileName(Me.filePhotos.PostedFile.FileName), strPath)

ExitFunction:
        Return strError
        Exit Function

    End Function
End Class
