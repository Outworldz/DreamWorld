﻿Imports System.IO

Module FileStuff

    Sub CopyFile(Source As String, Dest As String, overwrite As Boolean)

        If Source.EndsWith("Opensim.ini", StringComparison.InvariantCulture) Then Return
        If Source.EndsWith("OpenSim.log", StringComparison.InvariantCulture) Then Return
        If Source.EndsWith("OpenSimStats.log", StringComparison.InvariantCulture) Then Return
        If Source.EndsWith("PID.pid", StringComparison.InvariantCulture) Then Return
        If Source.EndsWith("DataSnapshot", StringComparison.InvariantCulture) Then Return

        Try
            My.Computer.FileSystem.CopyFile(Source, Dest, overwrite)

        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Sub CopyFolder(ByVal sourcePath As String, ByVal destinationPath As String)

        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)

        ' If the destination folder don't exist then create it
        If Not System.IO.Directory.Exists(destinationPath) Then
            Try
                System.IO.Directory.CreateDirectory(destinationPath)

            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try
        End If

        If Not System.IO.Directory.Exists(sourcePath) Then
            MsgBox("Cannot locate folder " & sourcePath, vbInformation)
            Return
        End If
        Dim ctr = 0
        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String =
                System.IO.Path.Combine(destinationPath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            If File.Exists(fileSystemInfo.FullName) Then
                ctr += 1
                If ctr Mod 100 = 0 Then
                    Form1.Print(CStr(ctr) & " copied")
                End If

                Try
                    CopyFile(fileSystemInfo.FullName, destinationFileName, True)

                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            Else
                ' Recursively call the method to copy all the nested folders
                If Not System.IO.Directory.Exists(fileSystemInfo.FullName) Then
                    Try
                        System.IO.Directory.CreateDirectory(fileSystemInfo.FullName)

                    Catch ex As Exception

                        BreakPoint.Show(ex.Message)
                    End Try
                End If
                CopyFolder(fileSystemInfo.FullName, destinationFileName)
                Application.DoEvents()
            End If

        Next
    End Sub

    Sub DeleteDirectory(folder As String, param As FileIO.DeleteDirectoryOption)
        Try
            My.Computer.FileSystem.DeleteDirectory(folder, param)

        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Sub DeleteFile(file As String)

        If My.Computer.FileSystem.FileExists(file) Then
            My.Computer.FileSystem.DeleteFile(file)
        End If

    End Sub

End Module
