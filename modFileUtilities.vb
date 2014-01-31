Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO

Module modFileUtilities

    Public Function CreateFolder(ByVal strParentDirectory As String, ByVal strFolderName As String) As Boolean
        'Creates a new folder under the specified parent directory
        Dim dirParentDirectory As DirectoryInfo
        Dim dirSubDirectory As DirectoryInfo
        Dim blnReturn As Boolean

        blnReturn = True

        dirParentDirectory = New DirectoryInfo(strParentDirectory)
        If Not dirParentDirectory.Exists Then
            blnReturn = False
        Else
            dirSubDirectory = dirParentDirectory.CreateSubdirectory(strFolderName)
            If Not dirSubDirectory.Exists Then
                blnReturn = False
            End If
        End If

        Return blnReturn

    End Function

    Public Function UnzipFile(ByVal strFilePath As String, ByVal strDestinationFolder As String) As String
        'Unzips the file specified by strFilePath to the required destination folder
        'Includes folder names specified in the zip file
        'returns any error message as string, blank if no error

        Dim strMessage As String
        Dim strFileName As String
        Dim strExtension As String
        Dim objZipInputStream As ZipInputStream
        Dim objZipEntry As ZipEntry
        Dim strFolderPath As String
        Dim dirDestination As DirectoryInfo
        Dim intSize As Integer
        Dim arrData(2048) As Byte

        If Not Left(strDestinationFolder, 1) = "/" Then
            strDestinationFolder &= "/"
        End If

        Try
            objZipInputStream = New ZipInputStream(System.IO.File.OpenRead(strFilePath))
        Catch ex As Exception
            Return ex.Message
        End Try

        objZipEntry = objZipInputStream.GetNextEntry
        While Not objZipEntry Is Nothing
            If objZipEntry.IsDirectory Then
                Try
                    CreateFolder(strDestinationFolder, objZipEntry.Name)
                Catch ex As Exception
                    objZipInputStream.Close()
                    Return ex.Message
                End Try
            End If
            objZipEntry = objZipInputStream.GetNextEntry
        End While

        objZipInputStream = New ZipInputStream(System.IO.File.OpenRead(strFilePath))
        objZipEntry = objZipInputStream.GetNextEntry
        While Not objZipEntry Is Nothing
            If Not objZipEntry.IsDirectory Then
                strFileName = Path.GetFileName(objZipEntry.Name)

                If strFileName <> "" Then
                    strExtension = Path.GetExtension(strFileName).Replace(".", "")
                    Try
                        strFolderPath = System.IO.Path.GetDirectoryName(strDestinationFolder & Replace(objZipEntry.Name, "/", "\"))
                        dirDestination = New DirectoryInfo(strFolderPath)
                        If Not dirDestination.Exists Then
                            CreateFolder(strDestinationFolder, objZipEntry.Name.Substring(0, Replace(objZipEntry.Name, "/", "\").LastIndexOf("\")))
                        End If
                        Dim objFileStream As FileStream = Nothing
                        Try
                            objFileStream = System.IO.File.Create(strDestinationFolder & Replace(objZipEntry.Name, "/", "\"))
                        Catch ex As Exception
                            If Not objFileStream Is Nothing Then
                                objFileStream.Close()
                            End If
                            Return ex.Message
                        End Try

                        intSize = objZipInputStream.Read(arrData, 0, arrData.Length)
                        While intSize > 0
                            objFileStream.Write(arrData, 0, intSize)
                            intSize = objZipInputStream.Read(arrData, 0, arrData.Length)
                        End While

                        objFileStream.Close()

                    Catch ex As Exception
                        If Not objZipInputStream Is Nothing Then
                            objZipInputStream.Close()
                        End If
                        Return ex.Message
                    End Try
                End If
            End If

            objZipEntry = objZipInputStream.GetNextEntry
        End While

        objZipInputStream.Close()

        Return strMessage

    End Function

End Module
