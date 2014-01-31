Imports System.IO
Imports Zip = ICSharpCode.SharpZipLib.Zip.Compression
Public Class vioZip
    Shared Function Compress(ByVal bytes() As Byte) As Byte()
        Dim memory As New MemoryStream
        Dim stream = New Zip.Streams.DeflaterOutputStream(memory, _
                    New Zip.Deflater(Zip.Deflater.BEST_COMPRESSION), 131072)
        stream.Write(bytes, 0, bytes.Length)
        stream.Close()
        Return memory.ToArray()
    End Function
    Shared Function Decompress(ByVal bytes() As Byte) As Byte()
        Dim stream = New Zip.Streams.InflaterInputStream(New MemoryStream(bytes))
        Dim memory As New MemoryStream
        Dim writeData(4096) As Byte
        Dim size As Integer
        While True
            size = stream.Read(writeData, 0, writeData.Length)
            If size > 0 Then memory.Write(writeData, 0, size) Else Exit While
        End While
        stream.Close()
        Return memory.ToArray()
    End Function
End Class
