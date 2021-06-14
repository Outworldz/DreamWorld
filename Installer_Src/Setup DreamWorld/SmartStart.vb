#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC. AGPL3.0 https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System
Imports System.Collections.Generic
Imports System.Linq

Module SmartStart

    Private ReadOnly Sleeping As New List(Of String)
    Private ReadOnly slop As Integer = 10     ' amount of extra time to add in for booting

#Region "SmartBegin"

    Public Function SmartStartParse(post As String) As String

        ' Smart Start AutoStart Region mode
        Debug.Print("Smart Start:" + post)

        Bench.Start()
        Bench.Print("Tp Request")
        'Smart Start:http://192.168.2.140:8999/?alt=Deliverance_of_JarJar_Binks__Fred_Beckhusen_1X1&agent=Ferd%20Frederix&AgentID=6f285c43-e656-42d9-b0e9-a78684fee15d&password=XYZZY

        Dim pattern As Regex = New Regex("alt=(.*?)&agent=(.*?)&agentid=(.*?)&password=(.*)", RegexOptions.IgnoreCase)
        Dim match As Match = pattern.Match(post)
        If match.Success Then
            Dim Name As String = Uri.UnescapeDataString(match.Groups(1).Value)
            'Debug.Print($"Name={Name}")
            Dim AgentName As String = Uri.UnescapeDataString(match.Groups(2).Value)
            'Debug.Print($"AgentName={AgentName}")
            Dim AgentID As String = Uri.UnescapeDataString(match.Groups(3).Value)
            'Debug.Print($"AgentID={AgentID}")
            Dim Password As String = Uri.UnescapeDataString(match.Groups(4).Value)
            'Debug.Print($"Password={Password}")
            If Password <> Settings.MachineID Then
                Logger("ERROR", $"Bad Password {Password} for Teleport system. Should be the Dyn DNS password.", "Outworldz")
                Return ""
            End If

            Dim time As String

            ' Region may be a name or a Region UUID
            Dim RegionUUID = PropRegionClass.FindRegionUUIDByName(Name)
            If RegionUUID.Length = 0 Then
                RegionUUID = Name ' Its a UUID
            Else
                Name = PropRegionClass.RegionName(RegionUUID)
            End If
            'Debug.Print("Teleport to " & Name)

            ' Smart Start below here

            If PropRegionClass.SmartStart(RegionUUID) = "True" Then

                ' smart, and up
                If PropRegionClass.RegionEnabled(RegionUUID) And PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then

                    If AgentName.ToUpperInvariant = "UUID" Then
                        'Logger("UUID Teleport", Name & ":" & AgentID, "Teleport")
                        Return RegionUUID
                    ElseIf AgentName.ToUpperInvariant = "REGIONNAME" Then
                        'Logger("Named Teleport", Name & ":" & AgentID, "Teleport")
                        Return Name
                    Else ' Its a sign!
                        ' Logger("Teleport Sign Booted", Name & ":" & AgentID, "Teleport")
                        Return Name & "|0"
                    End If
                Else  ' requires booting

                    If AgentName.ToUpperInvariant = "UUID" Then
                        'Logger("UUID Teleport", Name & ":" & AgentID, "Teleport")
                        AddEm(RegionUUID, AgentID)
                        RPC_admin_dialog(AgentID, $"Booting your region {PropRegionClass.RegionName(RegionUUID)}.{vbCrLf}Region will be ready in {CStr(PropRegionClass.BootTime(RegionUUID) + slop)} seconds. Please wait in this region.")
                        Dim u = PropRegionClass.FindRegionUUIDByName(Settings.WelcomeRegion)
                        Return u
                    ElseIf AgentName.ToUpperInvariant = "REGIONNAME" Then
                        Logger("Named Teleport", Name & ":" & AgentID, "Teleport")
                        AddEm(RegionUUID, AgentID)
                        RPC_admin_dialog(AgentID, $"Booting your region {PropRegionClass.RegionName(RegionUUID)}.{vbCrLf}Region will be ready in {CStr(PropRegionClass.BootTime(RegionUUID) + slop)} seconds. Please wait in this region.")
                        Dim u = PropRegionClass.FindRegionUUIDByName(Settings.WelcomeRegion)
                        Return u
                    Else ' Its a v4 sign

                        If Settings.MapType = "None" AndAlso PropRegionClass.MapType(RegionUUID).Length = 0 Then
                            time = "|" & CStr(PropRegionClass.BootTime(RegionUUID) + slop) ' 5 seconds of slop time
                        Else
                            time = "|" & CStr(PropRegionClass.MapTime(RegionUUID) + slop) ' 5 seconds of slop time
                        End If
                        RPC_admin_dialog(AgentID, $"Booting your region {PropRegionClass.RegionName(RegionUUID)}.{vbCrLf}Region will be ready in {CStr(PropRegionClass.BootTime(RegionUUID) + slop)} seconds. {vbCrLf}Please wait in this region.")
                        Logger("Agent ", Name & ":" & AgentID, "Teleport")
                        AddEm(RegionUUID, AgentID)
                        Return Settings.WelcomeRegion
                    End If

                End If
            Else ' Non Smart Start

                If AgentName.ToUpperInvariant = "UUID" Then
                    Logger("Teleport Non Smart", Name & ":" & AgentID, "Teleport")
                    Return RegionUUID
                ElseIf AgentName.ToUpperInvariant = "REGIONNAME" Then
                    'Logger("Teleport Non Smart", Name & ":" & AgentID, "Teleport")
                    Return Name
                Else     ' Its a sign!
                    'Logger("Teleport Sign ", Name & ":" & AgentID, "Teleport")
                    AddEm(RegionUUID, AgentID)
                    Return Name
                End If

            End If

        End If

        Return PropRegionClass.FindRegionByName(Settings.WelcomeRegion)

    End Function

    Private Function AddEm(RegionUUID As String, AgentID As String) As Boolean

        If RegionUUID = "00000000-0000-0000-0000-000000000000" Then
            BreakPoint.Show("UUID Zero")
            Logger("Addem", "Bad UUID", "Teleport")
            Return True
        End If

        Dim result As New Guid
        If Not Guid.TryParse(RegionUUID, result) Then
            Logger("Addem", "Bad UUID", "Teleport")
            Return False
        End If

        'TextPrint(My.Resources.Smart_Start_word & " " & PropRegionClass.RegionName(RegionUUID))
        'Logger("Teleport Request", PropRegionClass.RegionName(RegionUUID) & ":" & AgentID, "Teleport")

        If TeleportAvatarDict.ContainsKey(AgentID) Then
            TeleportAvatarDict.Remove(AgentID)
        End If
        TeleportAvatarDict.Add(AgentID, RegionUUID)
        Bench.Print("Teleport Added")

        ReBoot(RegionUUID) ' Wait for it to start booting

        Bench.Print("Reboot Signaled")
        Return False

    End Function

#End Region

#Region "HTML"

    Public Function RegionListHTML(Settings As MySettings, PropRegionClass As RegionMaker, Data As String) As String

        ' http://localhost:8001/teleports.htm
        ' http://YourURL:8001/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String = ""

        Dim ToSort As New Dictionary(Of String, String)

        Dim WelcomeUUID = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)
        ToSort.Add(Settings.WelcomeRegion, "0")

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            Dim Sort = ""
            Dim Name As String
            If Data.ToUpperInvariant.Contains("NAME") Then
                Name = PropRegionClass.RegionName(RegionUUID)
            ElseIf Data.ToUpperInvariant.Contains("GROUP") Then
                Name = PropRegionClass.GroupName(RegionUUID)
            ElseIf Data.ToUpperInvariant.Contains("ESTATE") Then
                Name = EstateName(RegionUUID)
            Else
                Name = PropRegionClass.RegionName(RegionUUID)
            End If

            Debug.Print($"Sort by {Name}")

            Dim status = PropRegionClass.Status(RegionUUID)
            If (PropRegionClass.Teleport(RegionUUID) = "True" AndAlso
                status = RegionMaker.SIMSTATUSENUM.Booted) Or
               (PropRegionClass.Teleport(RegionUUID) = "True" AndAlso
                PropRegionClass.SmartStart(RegionUUID) = "True" AndAlso
                Settings.SmartStart) Then

                If Settings.WelcomeRegion = PropRegionClass.RegionName(RegionUUID) Then Continue For
                ToSort.Add(PropRegionClass.RegionName(RegionUUID), Name)
            End If
        Next

        Dim myList As List(Of KeyValuePair(Of String, String)) = ToSort.ToList()
        Dim sorted = ToSort.OrderBy(Function(kvp) kvp.Value, StringComparer.CurrentCultureIgnoreCase).ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)

        For Each S As KeyValuePair(Of String, String) In sorted
            Dim V = S.Key
            HTML += $"*|{V}||{Settings.PublicIP}:{Settings.HttpPort}:{V}||{V}|{vbCrLf}"
        Next

        Dim HTMLFILE = Settings.OpensimBinPath & "data\teleports.htm"
        DeleteFile(HTMLFILE)

        Try
            Using outputFile As New StreamWriter(HTMLFILE, True)
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception
            ' BreakPoint.Show(ex.Message)
        End Try

        Return HTML

    End Function

#End Region

#Region "Disk"

    Public Function CalcDiskFree() As Long

        Dim d = DriveInfo.GetDrives()
        Dim c = CurDir()
        Dim Free As Long
        Try
            For Each drive As DriveInfo In d
                Dim x = Mid(c, 1, 1)
                If x = Mid(drive.Name, 1, 1) Then
                    Dim Percent = drive.AvailableFreeSpace / drive.TotalSize
                    Dim FreeDisk = Percent * 100
                    Free = drive.TotalSize - drive.AvailableFreeSpace

                    If Sleeping.Count = 0 Then
                        If Free < FreeDiskSpaceWarn Then
                            Dim SThread = New Thread(AddressOf FreezeAll)
                            SThread.SetApartmentState(ApartmentState.STA)
                            SThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
                            SThread.Start()
                            Busy = True
                            MsgBox(My.Resources.Diskspacelow & $" {Free:n0} Bytes", vbInformation Or MsgBoxStyle.MsgBoxSetForeground)
                        End If
                    End If

                    Dim tt = My.Resources.Available
                    Dim Text = $"{Percent:P1} {tt}"

                    FormSetup.DiskSize.Text = $"Disk {x}: {Text} "
                    Exit For
                End If

            Next
        Catch
        End Try

        Return Free

    End Function

    Public Sub DoSuspend(RegionName As String)

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim PID = PropRegionClass.ProcessID(RegionUUID)

        Logger("State Changed to ShuttingDown", RegionName, "Teleport")
        Dim GroupName = PropRegionClass.GroupName(RegionUUID)
        For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
            PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
            PokeRegionTimer(UUID)
        Next
        ShutDown(RegionUUID)
        Application.DoEvents()
        PropUpdateView = True ' make form refresh

    End Sub

    Public Sub FreezeAll()

        Dim running As Boolean
        For Each RegionUUID In PropRegionClass.RegionUuids()
            Dim status = PropRegionClass.Status(RegionUUID)
            If Not Sleeping.Contains(RegionUUID) Then
                Sleeping.Add(RegionUUID)
                Select Case status
                    Case RegionMaker.SIMSTATUSENUM.Booted
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.Booting
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.Error
                    Case RegionMaker.SIMSTATUSENUM.NoLogin
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.RecyclingDown
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.RecyclingUp
                    Case RegionMaker.SIMSTATUSENUM.RestartPending
                    Case RegionMaker.SIMSTATUSENUM.RestartStage2
                    Case RegionMaker.SIMSTATUSENUM.RetartingNow
                    Case RegionMaker.SIMSTATUSENUM.Stopped
                    Case RegionMaker.SIMSTATUSENUM.Suspended
                End Select
            End If
        Next

        PropUpdateView = True ' make form refresh
        If Not running Then
            Busy = False
            Sleeping.Clear()
            Return
        End If

        While CalcDiskFree() < FreeDiskSpaceWarn AndAlso PropOpensimIsRunning
            Sleep(1000)
        End While

        For Each RegionUUID In Sleeping
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)
            FreezeThaw(RegionUUID, "-rpid " & PropRegionClass.ProcessID(RegionUUID))
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted
        Next

        Sleeping.Clear()
        Busy = False

        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub FreezeThaw(RegionUUID As String, Arg As String)

        Using SuspendProcess As New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = Arg,
                .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "NtSuspendProcess64.exe") & """"
            }

            pi.CreateNoWindow = True
            pi.WindowStyle = ProcessWindowStyle.Minimized

            SuspendProcess.StartInfo = pi

            Try
                SuspendProcess.Start()
                SuspendProcess.WaitForExit()
                PokeRegionTimer(RegionUUID)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)

            End Try
        End Using

    End Sub

    Private Sub Pause(RegionUUID As String)

        Dim RegionName = PropRegionClass.RegionName(RegionUUID)
        FreezeThaw(RegionUUID, "-pid " & PropRegionClass.ProcessID(RegionUUID))
        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended
        Application.DoEvents()

    End Sub

#End Region

#Region "BootUp"

    Public Function Boot(BootName As String) As Boolean
        ''' <summary>Starts Opensim for a given name</summary>
        ''' <param name="BootName">Name of region to start</param>
        ''' <returns>success = true</returns>

        Bench.Print($"Boot {BootName}")

        If FormSetup.Timer1.Enabled = False Then
            FormSetup.Timer1.Interval = 1000
            FormSetup.Timer1.Start() 'Timer starts functioning
        End If

        PropOpensimIsRunning() = True
        If PropAborting Then Return True

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(BootName)
        Dim GroupName = PropRegionClass.GroupName(RegionUUID)

        If String.IsNullOrEmpty(RegionUUID) Then
            ErrorLog("Cannot find " & BootName & " to boot!")
            Return False
        End If

        PropRegionClass.CrashCounter(RegionUUID) = 0

        If CBool(GetHwnd(PropRegionClass.GroupName(RegionUUID))) Then

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                Logger("Suspended, Resuming it", BootName, "Teleport")

                Dim PID As Integer = GetPIDofWindow(GroupName)
                If Not PropInstanceHandles.ContainsKey(PID) Then
                    PropInstanceHandles.Add(PID, GroupName)
                End If
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Resume
                    PropRegionClass.ProcessID(UUID) = PID
                    PokeRegionTimer(UUID)
                Next
                ShowDOSWindow(GetHwnd(PropRegionClass.GroupName(RegionUUID)), MaybeShowWindow())
                Logger("Info", "Region " & BootName & " skipped as it is Suspended, Resuming it instead", "Teleport")
                PropUpdateView = True ' make form refresh
                Return True
            Else    ' needs to be captured into the event handler
                ' TextPrint(BootName & " " & My.Resources.Running_word)
                Dim PID As Integer = GetPIDofWindow(GroupName)
                If Not PropInstanceHandles.ContainsKey(PID) Then
                    PropInstanceHandles.Add(PID, GroupName)
                End If
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booted
                    PokeRegionTimer(UUID)
                    SendToOpensimWorld(RegionUUID, 0)
                    PropRegionClass.ProcessID(UUID) = PID
                Next
                ShowDOSWindow(GetHwnd(PropRegionClass.GroupName(RegionUUID)), MaybeHideWindow())

                PropUpdateView = True ' make form refresh
                Return True
            End If
        End If

        TextPrint(BootName & " " & Global.Outworldz.My.Resources.Starting_word)

        DoGloebits()

        If PropRegionClass.CopyOpensimProto(RegionUUID) Then Return False

        Dim ini = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\OpenSim.exe.config")
        Grep(ini, Settings.LogLevel)

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim BootProcess = New Process With {
            .EnableRaisingEvents = True
        }
#Enable Warning CA2000 ' Dispose objects before losing scope

        BootProcess.StartInfo.UseShellExecute = True
        BootProcess.StartInfo.WorkingDirectory = Settings.OpensimBinPath()

        BootProcess.StartInfo.FileName = """" & Settings.OpensimBinPath() & "OpenSim.exe" & """"
        BootProcess.StartInfo.CreateNoWindow = False

        Select Case Settings.ConsoleShow
            Case "True"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "False"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "None"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End Select

        BootProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & GroupName & """"

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", Settings.OpensimBinPath() & "Regions\" & GroupName)

        FormSetup.SequentialPause()   ' wait for previous region to give us some CPU
        Logger("Booting", GroupName, "Teleport")

        Dim ok As Boolean = False
        Try
            ok = BootProcess.Start
            Application.DoEvents()
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try

        If ok Then
            Dim PID = WaitForPID(BootProcess)           ' check if it gave us a PID, if not, it failed.

            If PID > 0 Then
                If Not SetWindowTextCall(BootProcess, GroupName) Then
                    ' Try again
                    If Not SetWindowTextCall(BootProcess, GroupName) Then
                        ErrorLog($"Timeout setting the title of {GroupName }")
                    End If
                End If
                If Not PropInstanceHandles.ContainsKey(PID) Then
                    PropInstanceHandles.Add(PID, GroupName)
                End If
                ' Mark them before we boot as a crash will immediately trigger the event that it exited
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booting
                    PokeRegionTimer(RegionUUID)
                Next
            Else
                BreakPoint.Show("No PID for " & GroupName)
            End If

            AddCPU(PID, GroupName) ' get a list of running opensim processes
            For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                PropRegionClass.ProcessID(UUID) = PID
            Next
            PropUpdateView = True ' make form refresh
            FormSetup.Buttons(FormSetup.StopButton)
            Return True
        End If
        PropUpdateView = True ' make form refresh
        Logger("Failed to boot ", BootName, "Teleport")
        TextPrint("Failed to boot region " & BootName)
        Return False

    End Function

    Public Sub ReBoot(RegionUUID As String)

        Bench.Print($"Reboot")
        If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Or
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped Then

            FormSetup.SequentialPause()   ' wait for previous region to give us some CPU
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
            Logger("State Changed to Resume", PropRegionClass.RegionName(RegionUUID), "Teleport")
            PropUpdateView = True ' make form refresh
        Else
            Bench.Print($"Reboot Not needed, already running")
        End If

    End Sub

#End Region

    ''' <summary>
    ''' Waits for a restarted region to be fully up
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>
    ''' <returns>True of region is booted</returns>
    Public Function WaitForBooted(RegionUUID As String) As Boolean

        Dim c As Integer = 600 ' 5 minutes
        While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booted

            c -= 1  ' skip on timeout error
            If c = 0 Then
                BreakPoint.Show("Timeout")
                ShutDown(RegionUUID)
                ConsoleCommand(RegionUUID, "q{ENTER}")
                Return False
            End If

            Debug.Print($"{GetStateString(PropRegionClass.Status(RegionUUID))} {PropRegionClass.RegionName(RegionUUID)}")
            Sleep(1000)

        End While
        Return True

    End Function

    ''' deprecated
    '''
    ''' <summary>
    ''' Waits for a restarted region to be booting
    ''' </summary>
    ''' <param name="RegionUUID">Region UUID</param>
    ''' <returns>True of region is booting</returns>
    Public Function WaitForBooting(RegionUUID As String) As Boolean

        Dim c As Integer = 60
        While c > 0

            c -= 1
            If c = 0 Then
                BreakPoint.Show("Timeout")
                Return False
            End If

            If PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Resume Then
                Exit While
            End If

            Debug.Print($"{GetStateString(PropRegionClass.Status(RegionUUID))} {PropRegionClass.RegionName(RegionUUID)}")
            Sleep(1000)

        End While
        Return True

    End Function

End Module
