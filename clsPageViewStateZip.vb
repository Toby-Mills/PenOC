Imports System.IO
Imports System.Web.UI
Imports System
Imports Zip = ICSharpCode.SharpZipLib.Zip.Compression
Public Class PageViewStateZip : Inherits System.Web.UI.Page


    Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Dim vState As String = Me.Request.Form("__VSTATE")
        Dim bytes As Byte() = System.Convert.FromBase64String(vState)
        bytes = vioZip.Decompress(bytes)
        Dim format As New System.Web.UI.LosFormatter
        Return format.Deserialize(System.Convert.ToBase64String(bytes))
    End Function
    Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        Dim format As New System.Web.UI.LosFormatter
        Dim writer As New StringWriter
        format.Serialize(writer, viewState)
        Dim viewStateStr As String = writer.ToString()
        Dim bytes As Byte() = System.Convert.FromBase64String(viewStateStr)
        bytes = vioZip.Compress(bytes)
        Dim vStateStr As String = System.Convert.ToBase64String(bytes)
        RegisterHiddenField("__VSTATE", vStateStr)





    End Sub
End Class
