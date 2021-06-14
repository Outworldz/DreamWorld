#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Threading

Module FileStuff

    Public Sub Cleanup() ' old files

        ' cleanup old code and files
        Dim ToDrop = New List(Of String) From {
            "LiveSwitch.TextControl.exe.config",
            "LiveSwitch.TextControl.pdb",
            "LiveSwitch.TextControl.exe",
            "fw.bat",
            "Downloader.exe",
            "DreamGridSetup.exe",
            "Downloader.exe.config",
            "DreamGridSetup.exe.config",
            "OutworldzFiles\IAR\Outworldz Teleport System V3.9.iar",
            "OutworldzFiles\Opensim\bin\OpenSim.Additional.AutoRestart.dll",
            "OutworldzFiles\Opensim\bin\OpenSim.Additional.AutoRestart.pdb",
            "OutworldzFiles\Opensim\bin\config-include\Birds.ini",
            "SET_externalIP-Log.txt",
            "OutworldzFiles\Opensim\bin\Opensim.proto",
            "OutworldzFiles\Opensim\bin\OpenSimRegion.proto",
            "OutworldzFiles\Opensim\bin\OpensimMetro.proto",
            "OutworldzFiles\Opensim\bin\OpensimOsGrid.proto",
            "OutworldzFiles\IAR\Outworldz Smart Start Alpha Teleport System V4.iar",
            "OutworldzFiles\IAR\Outworldz Teleport System V2.5.iar",
            "OutworldzFiles\IAR\Partner Control Panel.iar",
            "How_to_Start_and_Login.txt",
            "How_to_Compile.txt",
            "PRIVACYNOTICE.txt",
            "Revisions.txt",
            "ReadMe"
        }

        For Each N As String In ToDrop
            DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, N))
        Next

        Dim files As New List(Of String) From {
        "\Shoutcast", ' deprecated
        "\Icecast",   ' moved to OutworldzFiles
        "\OutworldzFiles\Opensim\bin\addins",' moved to OutworldzFiles
        "\OutworldzFiles\Opensim\bin\addin-db-002", ' must be cleared or opensim updates can break.
        "\OutworldzFiles\Opensim\bin\addin-db-001", ' must be cleared or opensim updates can break.
        "\OutworldzFiles\Opensim\bin\addin-db",' must be cleared or opensim updates can break.
        "\OutworldzFiles\Opensim\bin\Library.proto" ' old Diva library for standalone only
        }

        If FormSetup.PropKillSource Then
            files.Add("OutworldzFiles\Opensim\.nant")
            files.Add("OutworldzFiles\Opensim\doc")
            files.Add("OutworldzFiles\Opensim\Opensim")
            files.Add("OutworldzFiles\Opensim\Prebuild")
            files.Add("OutworldzFiles\Opensim\share")
            files.Add("OutworldzFiles\Opensim\Thirdparty")

        End If

        For Each N In files
            DeleteFolder(N)   ' wipe these folders out
        Next

        ' crap load of old DLLS have to be eliminated
        CleanDLLs() ' drop old opensim Dll's

        DeleteDirectoryTmp()

    End Sub

    ''' <summary>Deletes old log files</summary>
    Public Sub ClearOldLogFiles()

        ' old crap
        Dim Logfiles = New List(Of String) From {
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Diagnostics.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Outworldz.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Restart.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Teleport.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Diagnostics.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\UPnp.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\http.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\PHPLog.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "http.log")     ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            DeleteFile(thing)
        Next

    End Sub

    Public Sub CopyFileFast(From As String, Dest As String)

        If Not File.Exists(From) Then Return
        If From.Contains("Thumbs.db") Then Return

        'Create the file stream for the source file
        Try
            Using streamRead As New System.IO.FileStream(From, System.IO.FileMode.Open)
                'Create the file stream for the destination file
                Using streamWrite As New System.IO.FileStream(Dest, System.IO.FileMode.Create)
                    'Determine the size in bytes of the source file (-1 as our position starts at 0)
                    Dim lngLen As Long = streamRead.Length - 1
                    Dim byteBuffer(1048576) As Byte   'our stream buffer
                    Dim intBytesRead As Integer    'number of bytes read

                    While streamRead.Position < lngLen    'keep streaming until EOF
                        'Read from the Source
                        intBytesRead = (streamRead.Read(byteBuffer, 0, 1048576))
                        'Write to the Target
                        streamWrite.Write(byteBuffer, 0, intBytesRead)
                    End While

                    streamWrite.Flush()
                End Using
            End Using
        Catch ex As Exception
            Logger("Warn", $"Cannot copy file {From}", "Error")
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
            MsgBox("Cannot locate folder " & sourcePath, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
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
                    TextPrint(CStr(ctr) & " copied")
                End If

                Try
                    CopyFileFast(fileSystemInfo.FullName, destinationFileName)
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

    Public Sub CopyWifi()

        Dim Path As String = ""
        If Settings.Theme = "Black" Then

            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages"))
            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages"))

            Path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/NewBlack.png")
        ElseIf Settings.Theme = "White" Then

            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages"))
            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages"))

            Path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/NewWhite.png")
        ElseIf Settings.Theme = "Custom" Then

            Dim L As Integer = Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-" & Settings.Theme), "*", SearchOption.TopDirectoryOnly).Length

            If L <= 1 Then
                CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages-White"), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages-Custom"))
                CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-White"), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-Custom"))
            End If

            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\WifiPages"))
            CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages"))

            Path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/NewCustom.png")

        End If

        If Not System.IO.File.Exists(Path) Then
            Path = Path.Replace("New", "")
        End If

        CopyFileFast(Path, IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\WifiPages\images\Photo.png"))

    End Sub

    Sub DeleteDirectory(folder As String, param As FileIO.DeleteDirectoryOption)

        Try
            My.Computer.FileSystem.DeleteDirectory(folder, param)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DeleteDirectoryTmp()

        Try
            Dim WebThread = New Thread(AddressOf Deltmp)
            WebThread.SetApartmentState(ApartmentState.STA)
            WebThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
            WebThread.Start()
        Catch
        End Try

    End Sub

    Sub DeleteFile(file As String)

        Try
            If My.Computer.FileSystem.FileExists(file) Then
                My.Computer.FileSystem.DeleteFile(file)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub DeleteFolder(n As String)

        If System.IO.Directory.Exists(n) Then
            Try
                System.IO.Directory.Delete(n, True)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

        End If

    End Sub

    Sub DeleteOldFiles()

        Try
            Dim folder As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Help")
            Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(folder)

            Dim fileSystemInfo As System.IO.FileSystemInfo
            For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
                If fileSystemInfo.FullName.EndsWith(".rtf", StringComparison.InvariantCulture) Then
                    DeleteFile(fileSystemInfo.FullName)
                End If
            Next
        Catch
        End Try

    End Sub

    Public Sub ExpireLogsByAge()

        ' Hourly

        If Not Settings.DeleteByDate Then Return
        Deletefilesin(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Apache"))
        Deletefilesin(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs"))
        Deletefilesin(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Icecast\log"))
        Deletefilesin(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Autobackup"))

        DeleteThisOldFile(IO.Path.Combine(Settings.OpensimBinPath, "Robust.log"))

        For Each UUID As String In PropRegionClass.RegionUuids
            Dim GroupName = PropRegionClass.GroupName(UUID)
            DeleteThisOldFile($"{Settings.OpensimBinPath()}\Regions\{GroupName}\Opensim.log")
            DeleteThisOldFile($"{Settings.OpensimBinPath()}\regions\{GroupName}\OpensimConsole.log")
            DeleteThisOldFile($"{Settings.OpensimBinPath()}\regions\{GroupName}\OpenSimStats.log")
        Next

    End Sub

    Sub FixUpdater()

        CopyFileFast(IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.New"), IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.exe"))

    End Sub

    Public Sub MoveFile(Src As String, Dest As String)

        Try
            File.Move(Src, Dest)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#Region "Grep"

    ''' <summary>Replaces .config file XML with log level and path info</summary>
    ''' <param name="INI">Path to file</param>
    ''' <param name="LP">OSIM_LOGPATH path to log file in regions folder</param>
    ''' <param name="LL">OSIM_LOGLEVEL DEBUG, INFO, ALL, etc</param>
    ''
    Public Sub Grep(INI As String, LL As String)

        If INI Is Nothing Then Return
        Dim Retry = 100 ' 10 sec

        While Retry > 0
            Try
                Using file As New System.IO.StreamWriter(INI & ".bak")
                    Using Reader As New StreamReader(INI & ".proto", System.Text.Encoding.UTF8)
                        While Not Reader.EndOfStream
                            Dim line As String = Reader.ReadLine
                            line = line.Replace("${OSIM_LOGLEVEL}", LL)
                            file.WriteLine(line)
                        End While
                    End Using
                End Using
                Retry = 0
            Catch ex As Exception
                Retry -= 1
                Sleep(100)
            End Try
        End While

        Dim f = System.IO.Path.GetFileName(INI)
        Retry = 100 ' 10 sec
        While Retry > 0
            DeleteFile(INI)
            Try
                My.Computer.FileSystem.RenameFile(INI & ".bak", f)
                Retry = 0
            Catch
                Retry -= 1
                Sleep(100)
            End Try
        End While

    End Sub

#End Region

    Private Sub CleanDLLs()

        If Not Debugger.IsAttached Then
            Dim dlls As List(Of String) = GetDlls(IO.Path.Combine(Settings.CurrentDirectory, "dlls.txt"))
            Dim localdlls As List(Of String) = GetFilesRecursive(Settings.OpensimBinPath)
            For Each localdllname In localdlls
                Application.DoEvents()
                Dim x = localdllname.IndexOf("OutworldzFiles", StringComparison.InvariantCulture)
                Dim newlocaldllname = Mid(localdllname, x)
                If Not CompareDLLignoreCase(newlocaldllname, dlls) Then
                    DeleteFile(localdllname)
                End If
            Next
        End If

    End Sub

    Private Function CompareDLLignoreCase(tofind As String, dll As List(Of String)) As Boolean
        If dll Is Nothing Then Return False
        If tofind Is Nothing Then Return False
        For Each filename In dll
            If tofind.ToUpper(Globalization.CultureInfo.InvariantCulture) = filename.ToUpper(Globalization.CultureInfo.InvariantCulture) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Deletefilesin(LogPath As String)

        Try
            FileIO.FileSystem.CreateDirectory(LogPath)
            Dim directory As New System.IO.DirectoryInfo(LogPath)
            ' get each file's last modified date
            For Each File As System.IO.FileInfo In directory.GetFiles()
                ' get  file's last modified date
                Dim strLastModified As Date = System.IO.File.GetLastWriteTime(File.FullName)
                Dim Datedifference = DateDiff("h", strLastModified, Date.Now)
                If Datedifference > Settings.KeepForDays * 24 Then
                    DeleteFile(File.FullName)
                End If
            Next
        Catch
        End Try

    End Sub

    Private Sub DeleteThisOldFile(File As String)

        If Not IO.File.Exists(File) Then Return

        ' get  file's last modified date
        Dim strLastModified As Date = System.IO.File.GetLastWriteTime(File)
        Dim Datedifference = DateDiff("h", strLastModified, Date.Now)
        If Datedifference > Settings.KeepForDays * 24 Then DeleteFile(File)

    End Sub

    Private Sub Deltmp() ' thread

        DeleteDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp"), FileIO.DeleteDirectoryOption.DeleteAllContents)
        DeleteDirectory(IO.Path.Combine(Settings.CurrentDirectory, "tmp"), FileIO.DeleteDirectoryOption.DeleteAllContents)
        FileIO.FileSystem.CreateDirectory(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp"))
        Application.ExitThread()
    End Sub

    Private Function GetDlls(fname As String) As List(Of String)

        Dim DllList As New List(Of String)

        If System.IO.File.Exists(fname) Then
            Dim line As String
            Using reader As StreamReader = System.IO.File.OpenText(fname)
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()
                    DllList.Add(line)
                End While
            End Using
        End If
        Return DllList

    End Function

    Private Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ''' <summary>This method starts at the specified directory. It traverses all subdirectories. It returns a List of those directories.</summary>
        ''' ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop

            ' Add all immediate file paths
            Try
                result.AddRange(Directory.GetFiles(dir, "*.dll"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            ' Loop through all subdirectories and add them to the stack.
            Dim directoryName As String

            Try
                'Save, but skip script engines
                For Each directoryName In Directory.GetDirectories(dir)
                    If Not directoryName.Contains("ScriptEngines") And
                    Not directoryName.Contains("fsassets") And
                    Not directoryName.Contains("assetcache") And
                    Not directoryName.Contains("j2kDecodeCache") Then
                        stack.Push(directoryName)
                    End If

                    Application.DoEvents()
                Next
            Catch
            End Try

            Application.DoEvents()
        Loop

        ' Return the list
        Return result
    End Function

End Module
