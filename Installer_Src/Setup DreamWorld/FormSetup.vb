#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC. AGPL3.0 https://opensource.org/licenses/AGPL

#End Region

#Region "To do"

#End Region

'Option Strict On

Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Threading
Imports IWshRuntimeLibrary

Public Class FormSetup

    Private searcher As ManagementObjectSearcher

#Region "Private Declarations"

    Private WithEvents UpdateProcess As New Process()
    Private ReadOnly _exitList As New Dictionary(Of String, String)
    Private ReadOnly _regionHandles As New Dictionary(Of Integer, String)
    ReadOnly BackupThread As New Backups
    Private ReadOnly BootedList As New List(Of String)
    Private ReadOnly D As New Dictionary(Of String, String)
    Private ReadOnly ExitInterval As Integer = 2
    Private ReadOnly HandlerSetup As New EventHandler(AddressOf Resize_page)
    Private ReadOnly MyCPUCollection As New List(Of Double)
    Private ReadOnly MyRAMCollection As New List(Of Double)
    Private _Adv As FormSettings
    Private _ContentIAR As FormOAR
    Private _ContentOAR As FormOAR
    Private _CurSlashDir As String
    Private _DNSSTimer As Integer
    Private _ExitHandlerIsBusy As Boolean
    Private _IcecastCrashCounter As Integer
    Private _IceCastExited As Boolean

    Private _IPv4Address As String

    Private _KillSource As Boolean

    Private _OpensimBinPath As String
    Private _regionForm As FormRegionlist
    Private _RestartApache As Boolean
    Private _RestartMysql As Boolean
    Private _speed As Double = 50
    Private _StopMysql As Boolean = True
    Private _timerBusy1 As Integer
    Private _viewedSettings As Boolean
#Disable Warning CA2213 ' Disposable fields should be disposed
    Private cpu As New PerformanceCounter
#Enable Warning CA2213 ' Disposable fields should be disposed
    Private ScreenPosition As ScreenPos

#End Region

#Region "Globals"

    Private speed As Double
    Private speed1 As Double
    Private speed2 As Double
    Private speed3 As Double
    Private Update_version As String
    Private ws As NetServer

#End Region

#Region "Events"

    Public Event ApacheExited As EventHandler

    Public Event Exited As EventHandler

    Public Event RobustExited As EventHandler

#End Region

#Region "Public Properties"

    Public Property Adv1 As FormSettings
        Get
            Return _Adv
        End Get
        Set(value As FormSettings)
            _Adv = value
        End Set
    End Property

    Public ReadOnly Property BootedList1 As List(Of String)
        Get
            Return BootedList
        End Get
    End Property

    Public Property ContentIAR As FormOAR
        Get
            Return _ContentIAR
        End Get
        Set(value As FormOAR)
            _ContentIAR = value
        End Set
    End Property

    Public Property ContentOAR As FormOAR
        Get
            Return _ContentOAR
        End Get
        Set(value As FormOAR)
            _ContentOAR = value
        End Set
    End Property

    Public Property Cpu1 As PerformanceCounter
        Get
            Return cpu
        End Get
        Set(value As PerformanceCounter)
            cpu = value
        End Set
    End Property

    Public Property CPUAverageSpeed As Double
        Get
            Return _speed
        End Get
        Set(value As Double)
            _speed = value
        End Set
    End Property

    Public Property IcecastCrashCounter As Integer
        Get
            Return _IcecastCrashCounter
        End Get
        Set(value As Integer)
            _IcecastCrashCounter = value
        End Set
    End Property

    Public Property OpensimBinPath As String
        Get
            Return _OpensimBinPath
        End Get
        Set(value As String)
            _OpensimBinPath = value
        End Set
    End Property

    ' TODO:  Implement PropChangedRegionSettings as a dictionary in a module we can prompt for restart with
    Public Property PropChangedRegionSettings As Boolean
        Get
            Return ViewedSettings
        End Get
        Set(value As Boolean)
            Diagnostics.Debug.Print("ViewedSettings =" & value)
            ViewedSettings = value
        End Set
    End Property

    Public Property PropCurSlashDir As String
        Get
            Return _CurSlashDir
        End Get
        Set(value As String)
            _CurSlashDir = value
        End Set
    End Property

    Public Property PropExitHandlerIsBusy() As Boolean
        Get
            Return _ExitHandlerIsBusy
        End Get
        Set(ByVal Value As Boolean)
            _ExitHandlerIsBusy = Value
        End Set
    End Property

    Public ReadOnly Property PropExitList As Dictionary(Of String, String)
        Get
            Return _exitList
        End Get
    End Property

    Public Property PropIceCastExited() As Boolean
        Get
            Return _IceCastExited
        End Get
        Set(ByVal Value As Boolean)
            _IceCastExited = Value
        End Set
    End Property

    Public ReadOnly Property PropInstanceHandles As Dictionary(Of Integer, String)
        Get
            Return _regionHandles
        End Get
    End Property

    Public Property PropIPv4Address() As String
        Get
            Return _IPv4Address
        End Get
        Set(ByVal Value As String)
            _IPv4Address = Value
        End Set
    End Property

    Public Property PropKillSource As Boolean
        Get
            Return _KillSource
        End Get
        Set(value As Boolean)
            _KillSource = value
        End Set
    End Property

    Public Property PropRegionForm As FormRegionlist
        Get
            Return _regionForm
        End Get
        Set(value As FormRegionlist)
            _regionForm = value
        End Set
    End Property

    Public Property PropRestartApache() As Boolean
        Get
            Return _RestartApache
        End Get
        Set(ByVal Value As Boolean)
            _RestartApache = Value
        End Set
    End Property

    Public Property PropRestartMySql() As Boolean
        Get
            Return _RestartMysql
        End Get
        Set(ByVal Value As Boolean)
            _RestartMysql = Value
        End Set
    End Property

    Public Property PropStopMysql As Boolean
        Get
            Return _StopMysql
        End Get
        Set(value As Boolean)
            _StopMysql = value
        End Set
    End Property

    Public Property PropUseIcons As Boolean

    Public Property PropWebServer As NetServer
        Get
            Return ws
        End Get
        Set(value As NetServer)
            ws = value
        End Set
    End Property

    Public Property ScreenPosition1 As ScreenPos
        Get
            Return ScreenPosition
        End Get
        Set(value As ScreenPos)
            ScreenPosition = value
        End Set
    End Property

    Public Property SecondsTicker() As Integer
        Get
            Return _DNSSTimer
        End Get
        Set(ByVal Value As Integer)
            _DNSSTimer = Value
        End Set
    End Property

    Public Property TimerBusy As Integer
        Get
            Return _timerBusy1
        End Get
        Set(value As Integer)
            _timerBusy1 = value
        End Set
    End Property

    Public Property ViewedSettings As Boolean
        Get
            Return _viewedSettings
        End Get
        Set(value As Boolean)
            _viewedSettings = value
        End Set
    End Property

#End Region

#Region "Public Function"

    Public Shared Function AvatarsIsInGroup(groupname As String) As Boolean

        Dim present As Integer = 0
        For Each RegionUUID As String In PropRegionClass.RegionUuidListByName(groupname)
            If IsAgentInRegion(RegionUUID) Then
                present += 1
            End If
        Next
        Return CType(present, Boolean)

    End Function

    Public Sub Buttons(b As Button)

        If b Is Nothing Then Return
        ' Turns off all 3 stacked buttons, then enables one of them
        BusyButton.Visible = False
        StopButton.Visible = False
        StartButton.Visible = False

        b.Visible = True

    End Sub

#End Region

#Region "Updater"

    ''' <summary>Checks the Outworldz Web site to see if a new version exist,.</summary>
    Public Sub CheckForUpdates()

        Using client As New WebClient ' download client for web pages
            TextPrint(My.Resources.Checking_for_Updates_word)
            Try
                Update_version = client.DownloadString(PropDomain & "/Outworldz_Installer/UpdateGrid.plx" & GetPostData())
            Catch ex As Exception
                ErrorLog(My.Resources.Wrong & " " & ex.Message)
                Return
            End Try
        End Using

        ' Update Error check could be nothing
        If Update_version.Length = 0 Then Update_version = PropMyVersion

        Try
            Dim uv As Single = 0
            uv = Convert.ToSingle(Update_version, Globalization.CultureInfo.InvariantCulture)
            Dim ToVersion = Convert.ToSingle(Settings.SkipUpdateCheck, Globalization.CultureInfo.InvariantCulture)
            ' could be the same or later version already
            If uv <= ToVersion Then
                Return
            End If
            If uv <= Convert.ToSingle(PropMyVersion, Globalization.CultureInfo.InvariantCulture) Then
                Return
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Return
        End Try

        TextPrint(My.Resources.Update_is_available & ":" & Update_version)

        Dim doUpdate = MsgBox(My.Resources.Update_is_available, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Information, My.Resources.Update_is_available)
        If doUpdate = vbOK Then

            If DoStopActions() = False Then Return

            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .WindowStyle = ProcessWindowStyle.Normal,
                .FileName = IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.exe")
            }

            UpdateProcess.StartInfo = pi

            Try
                UpdateProcess.Start()
                Settings.SkipUpdateCheck() = Update_version
                Settings.SaveSettings()
                End
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.ErrUpdate)
            End Try
        End If

    End Sub

#End Region

#Region "Misc"

    ''' <summary>Brings up a region chooser with no buttons, of all regions</summary>
    Public Shared Sub ShowRegionMap()

        Dim region = ChooseRegion(False)
        If region.Length = 0 Then Return

        VarChooser(region, False, False)

    End Sub

    Public Function DoStopActions() As Boolean

        TextPrint(My.Resources.Stopping_word)
        Buttons(BusyButton)
        If Not KillAll() Then Return False
        Buttons(StartButton)
        TextPrint(My.Resources.Stopped_word)
        ToolBar(False)
        Return True

    End Function

    ''' <summary>Event handler for Icecast</summary>
    Public Sub IceCastExited(ByVal sender As Object, ByVal e As EventArgs)

        'RestartIcecastIcon.Image = Global.Outworldz.My.Resources.nav_plain_red

        If PropAborting Then Return

        If Settings.RestartOnCrash And IcecastCrashCounter < 10 Then
            IcecastCrashCounter += 1
            PropIceCastExited = True
            Return
        End If
        IcecastCrashCounter = 0

        Dim yesno = MsgBox(My.Resources.Icecast_Exited, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Critical, Global.Outworldz.My.Resources.Error_word)

        If (yesno = vbYes) Then
            Dim IceCastLog As String = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Icecast\log\error.log")
            Try
                System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), $"""{IceCastLog}""")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Public Function KillAll() As Boolean

        If ScanAgents() > 0 Then
            Dim response = MsgBox(My.Resources.Avatars_in_World, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Information, My.Resources.Agents_word)
            If response = vbNo Then Return False
        End If

        AvatarLabel.Text = ""
        PropAborting = True
        ToolBar(False)
        ' close everything as gracefully as possible.

        StopIcecast()

        Dim n As Integer = PropRegionClass.RegionCount()

        Dim TotalRunningRegions As Integer

        For Each RegionUUID As String In PropRegionClass.RegionUuids
            If PropRegionClass.IsBooted(RegionUUID) Then
                TotalRunningRegions += 1
            End If
        Next
        Log(My.Resources.Info_word, "Total Enabled Regions=" & CStr(TotalRunningRegions))

        For Each RegionUUID As String In PropRegionClass.RegionUuids
            If PropRegionClass.RegionEnabled(RegionUUID) And
                (PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Or
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting) Then
                SequentialPause()
                Dim s As Boolean = ShutDown(RegionUUID)
                TextPrint(PropRegionClass.GroupName(RegionUUID) & " " & Global.Outworldz.My.Resources.Stopping_word)
                Dim GroupName = PropRegionClass.GroupName(RegionUUID)
                If s Then
                    For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
                        PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                    Next
                    PropUpdateView = True ' make form refresh
                    Application.DoEvents()
                End If
            End If
        Next

        Dim LastCount As Integer = 0
        Dim counter As Integer = 6000 ' 10 minutes to quit all regions

        ' only wait if the port 8001 is working
        If PropUseIcons Then
            If PropOpensimIsRunning Then TextPrint(My.Resources.Waiting_text)

            While (counter > 0 And PropOpensimIsRunning())

                Application.DoEvents()
                counter -= 1
                Dim CountisRunning As Integer = 0

                For Each RegionUUID As String In PropRegionClass.RegionUuids
                    If (Not PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped) _
                        And PropRegionClass.RegionEnabled(RegionUUID) Then

                        Dim GroupName = PropRegionClass.GroupName(RegionUUID)
                        For Each p In Process.GetProcessesByName("Opensim")
                            Try
                                Application.DoEvents()
                                If p.MainWindowTitle = GroupName Then
                                    CountisRunning += 1
                                    Exit For
                                End If
                            Catch
                            End Try
                        Next
                    End If
                    Application.DoEvents()
                Next

                ExitHandlerPoll()

                If CountisRunning <> LastCount Then
                    If CountisRunning = 1 Then
                        TextPrint(My.Resources.One_region)
                    Else
                        TextPrint(CStr(CountisRunning) & " " & Global.Outworldz.My.Resources.Regions_Are_Running)
                    End If
                End If

                LastCount = CountisRunning

                If CountisRunning = 0 Then
                    counter = 0
                End If

                PropUpdateView = True ' make form refresh

            End While
            PropUpdateView = True ' make form refresh
        End If

        ClearAllRegions()

        StopRobust()

        Timer1.Stop()
        TimerBusy = 0

        PropOpensimIsRunning() = False

        ToolBar(False)
        Return True

    End Function

    Public Sub SequentialPause()

        If Settings.Sequential Then
            Dim ctr = 5 * 60  '  minute max to start a region
            While True
                Dim wait As Boolean = False
                For Each RegionUUID As String In PropRegionClass.RegionUuids

                    Dim status = PropRegionClass.Status(RegionUUID)
                    If PropRegionClass.RegionEnabled(RegionUUID) _
                            And Not PropAborting _
                            And (status = RegionMaker.SIMSTATUSENUM.Booting Or
                            status = RegionMaker.SIMSTATUSENUM.ShuttingDown Or
                            status = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood Or
                            status = RegionMaker.SIMSTATUSENUM.RecyclingDown
                            ) Then
                        Diagnostics.Debug.Print(PropRegionClass.RegionName(RegionUUID))
                        wait = True
                        Application.DoEvents()
                    End If
                Next

                If wait Then
                    ctr -= 1
                Else
                    Exit While
                End If
                If ctr <= 0 Then Exit While
                Sleep(1000)
            End While
        Else

            Dim ctr = 5 * 60 ' 5 minute max to start a region at 100% CPU
            While True
                If CPUAverageSpeed < Settings.CPUMAX And Settings.Ramused < 90 Then
                    Exit While
                End If
                Sleep(1000)
                Application.DoEvents()
                ctr -= 1
                If ctr <= 0 Then Exit While
            End While

        End If

    End Sub

#End Region

#Region "StartOpensim"

    Public Shared Sub StopGroup(Groupname As String)

        For Each RegionUUID As String In PropRegionClass.RegionUuidListByName(Groupname)
            Logger(My.Resources.Info_word, PropRegionClass.RegionName(RegionUUID) & " is Stopped", "Teleport")
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
        Next
        Logger("Info", Groupname & " Group is now stopped", "Teleport")

    End Sub

    Public Function StartOpensimulator() As Boolean

        PropExitHandlerIsBusy = False
        PropAborting = False

        Buttons(BusyButton)

        If Not StartRobust() Then
            Buttons(StopButton)
            Return False
        End If

        DoEstates() ' has to be done after Mysql starts up.

        ' Reload
        If PropChangedRegionSettings Then
            PropRegionClass.GetAllRegions()
            PropRegionClass.UpdateAllRegionPorts()
        End If
        Application.DoEvents()
        Dim ini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\OpenSim.exe.config")
        Settings.Grep(ini, Settings.LogLevel)

        PropRegionClass.CheckOverLap()

        Dim l = PropRegionClass.RegionUuids()

        If Settings.ServerType = RobustServerName Then
            Dim RegionName = Settings.WelcomeRegion
            Dim UUID As String = PropRegionClass.FindRegionByName(RegionName)
            Dim out As New Guid
            If Guid.TryParse(UUID, out) Then
                PropRegionClass.CrashCounter(UUID) = 0
                If Not Boot(PropRegionClass.RegionName(UUID)) Then Return False
            End If
            l.Remove(UUID)
        End If
        Application.DoEvents()

        ' Boot them up
        For Each RegionUUID As String In l
            If PropRegionClass.RegionEnabled(RegionUUID) Then
                Dim BootNeeded As Boolean = False
                Select Case Settings.SmartStart
                    Case True
                        ' Really Smart Start, not in Region table
                        If PropRegionClass.SmartStart(RegionUUID) = "True" And Not RegionIsRegistered(RegionUUID) Then
                            BootNeeded = True
                        End If

                        ' if set to default, which is true
                        If PropRegionClass.SmartStart(RegionUUID).Length = 0 Or
                            PropRegionClass.SmartStart(RegionUUID) = "False" Then
                            BootNeeded = True
                        End If
                    Case False
                        BootNeeded = True
                End Select

                If BootNeeded Then
                    If Not Boot(PropRegionClass.RegionName(RegionUUID)) Then
                        Exit For
                    End If

                End If
            End If

            Application.DoEvents()
        Next

        Return True

    End Function

    ''' <summary>Startup() Starts opensimulator system Called by Start Button or by AutoStart</summary>
    Public Sub Startup()

        Buttons(BusyButton)

        Dim DefaultName As String = ""

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(Settings.WelcomeRegion)
        If RegionUUID.Length = 0 And Settings.ServerType = RobustServerName Then
            MsgBox(My.Resources.Default_Welcome, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Question, My.Resources.Information_word)
            TextPrint(My.Resources.Stopped_word)
#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim FormRegions = New FormRegions
#Enable Warning CA2000 ' Dispose objects before losing scope
            FormRegions.Activate()
            FormRegions.Select()
            FormRegions.Visible = True
            FormRegions.BringToFront()
            Buttons(StartButton)
            Return
        End If

        TextPrint(My.Resources.Starting_word)

        'PropRegionClass.RegionEnabled(RegionUUID) = True

        PropExitHandlerIsBusy = False
        PropAborting = False  ' suppress exit warning messages

        ToolBar(False)

        If Settings.Language.Length = 0 Then
            Settings.Language = "en-US"
        End If

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)

        If Settings.AutoBackup Then
            ' add 30 minutes to allow time to auto backup and then restart
            Dim BTime As Integer = CInt("0" & Settings.AutobackupInterval)
            If Settings.AutoRestartInterval > 0 And Settings.AutoRestartInterval < BTime Then
                Settings.AutoRestartInterval = BTime + 30
                TextPrint(My.Resources.AutorestartTime & " " & CStr(BTime) & " + 30 min.")
            End If
        End If

        If SetIniData() Then
            MsgBox("Failed to setup", MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Critical, My.Resources.Error_word)
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        ' create tables in case we need them
        SetupWordPress()
        SetupMutelist()

        StartApache()
        StartIcecast()

        UploadPhoto()

        SetBirdsOnOrOff()

        If Not StartRobust() Then
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        If Not Settings.RunOnce And Settings.ServerType = RobustServerName Then

            Using InitialSetup As New FormInitialSetup ' form for use and password
                Dim ret = InitialSetup.ShowDialog()
                If ret = DialogResult.Cancel Then
                    Buttons(StartButton)
                    TextPrint(My.Resources.Stopped_word)
                    Return
                End If

                ' Read the chosen sim name

                ConsoleCommand(RobustName, "create user " & InitialSetup.FirstName & " " & InitialSetup.LastName & " " & InitialSetup.Password & " " & InitialSetup.Email)
                ConsoleCommand(RobustName, "{ENTER}")

                Settings.RunOnce = True
                Settings.SaveSettings()
            End Using
        End If

        ToolBar(True)

        ' Launch the rockets
        TextPrint(My.Resources.Start_Regions_word)

        If Not StartOpensimulator() Then
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        Buttons(StopButton)
        TextPrint(My.Resources.Grid_address_word & vbCrLf & "http://" & Settings.BaseHostName & ":" & Settings.HttpPort)

        ' done with boot up

    End Sub

#End Region

#Region "Misc"

    Public Sub ToolBar(visible As Boolean)

        AvatarLabel.Visible = visible
        PercentCPU.Visible = visible
        PercentRAM.Visible = visible
        DiskSize.Visible = visible

    End Sub

    Private Shared Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshShortcut
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(CurDir() & "\Start.exe")
        MyShortcut.WorkingDirectory = CurDir()
        MyShortcut.Save()

    End Sub

    Private Shared Sub SetBirdsOnOrOff()

        If Settings.BirdsModuleStartup Then
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.dll") Then
                    CopyFileFast(Settings.OpensimBinPath & "OpenSimBirds.Module.bak", Settings.OpensimBinPath & "OpenSimBirds.Module.dll")
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        Else
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.bak") Then
                    CopyFileFast(Settings.OpensimBinPath & "OpenSimBirds.Module.dll", Settings.OpensimBinPath & "OpenSimBirds.Module.bak")
                End If
            Catch
            End Try

            DeleteFile(Settings.OpensimBinPath & "\OpenSimBirds.Module.dll")
        End If

    End Sub

    Private Shared Sub SetQuickEditOff()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "Set-ItemProperty -path HKCU:\Console -name QuickEdit -value 0",
            .FileName = "powershell.exe",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .Verb = "runas"
        }
        Using PowerShell As Process = New Process With {
             .StartInfo = pi
            }

            Try
                PowerShell.Start()
            Catch ex As Exception
                ErrorLog("Cannot set Quickedit off")
            End Try
        End Using

    End Sub

    Private Shared Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

    Private Sub ApachePictureBox_Click(sender As Object, e As EventArgs)

        If Not CheckApache() Then
            StartApache()
        Else
            StopApache(True) 'Do Stop, even If a service
        End If

    End Sub

    Private Sub Backupper()

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            ReBoot(RegionUUID)

            SequentialPause()   ' wait for previous region to give us some CPU
            ConsoleCommand(RegionUUID, "change region " & """" & PropRegionClass.RegionName(RegionUUID) & """")
            ConsoleCommand(RegionUUID, "save oar  " & """" & BackupPath() & "/" & PropRegionClass.RegionName(RegionUUID) & "_" &
                               DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar" & """")

        Next

    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        PropAborting = True
        ClearAllRegions()
        Timer1.Stop()
        TimerBusy = 0

        PropUpdateView = True ' make form refresh

        PropOpensimIsRunning() = False
        ToolBar(False)
        TextPrint(My.Resources.Stopped_word)
        Buttons(StartButton)

    End Sub

    Private Sub Chart()
        ' Graph https://github.com/sinairv/MSChartWrapper
        Try
            ' running average
            speed3 = speed2
            speed2 = speed1
            speed1 = speed
            Try
                speed = Me.Cpu1.NextValue()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                If Not Settings.CPUPatched Then
                    Dim pUpdate As Process = New Process()
                    Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                        .Arguments = "/ R",
                        .FileName = "loadctr"
                    }
                    pUpdate.StartInfo = pi
                    pUpdate.Start()
                    pUpdate.WaitForExit()
                    pUpdate.Dispose()
                    Settings.CPUPatched = True
                End If

            End Try

            CPUAverageSpeed = (speed + speed1 + speed2 + speed3) / 4

            MyCPUCollection.Add(CPUAverageSpeed)

            If MyCPUCollection.Count > 180 Then MyCPUCollection.RemoveAt(0)

            PercentCPU.Text = $"CPU: {CPUAverageSpeed / 100:P1}"

            ChartWrapper1.ClearChart()
            Dim CPU1() As Double = MyCPUCollection.ToArray()
            ChartWrapper1.AddLinePlot("CPU", CPU1)
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try
        'RAM

        Try
            Dim results As ManagementObjectCollection = searcher.Get()
            For Each result In results
                Dim value As Double = (CDbl(result("TotalVisibleMemorySize").ToString) - CDbl(result("FreePhysicalMemory").ToString)) / CDbl(result("TotalVisibleMemorySize").ToString) * 100
                MyRAMCollection.Add(value)
                If MyRAMCollection.Count > 180 Then MyRAMCollection.RemoveAt(0)
                value = Math.Round(value)
                Settings.Ramused = value
                PercentRAM.Text = $"RAM: {value / 100:p1}"
            Next
            ChartWrapper2.ClearChart()
            Dim RAM() As Double = MyRAMCollection.ToArray()
            ChartWrapper2.AddLinePlot("RAM", RAM)
            results.Dispose()
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try

    End Sub

    Private Sub CheckDiagPort()

        PropUseIcons = True
        TextPrint(My.Resources.Check_Diag)
        Dim wsstarted = CheckPort("127.0.0.1", CType(Settings.DiagnosticPort, Integer))
        If wsstarted = False Then
            MsgBox($"{My.Resources.Diag_Port_word} {Settings.DiagnosticPort} : {Global.Outworldz.My.Resources.Diag_Broken}", MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxSetForeground, My.Resources.Error_word)
            PropUseIcons = False
        End If

    End Sub

    Private Sub ClearAllRegions()

        For Each RegionUUID As String In PropRegionClass.RegionUuids
            Logger("State is Stopped", PropRegionClass.RegionName(RegionUUID), "Teleport")
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
            PropRegionClass.ProcessID(RegionUUID) = 0
        Next
        Try
            PropExitList.Clear()
            PropRegionClass.ClearStack()
            PropInstanceHandles.Clear()
            PropRegionClass.WebserverList.Clear()
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub ExitHandlerPoll()

        Dim GroupName As String = ""
        Dim TimerValue As Integer

        ' booted regions from web server
        While BootedList1.Count > 0
            Dim Ruuid As String = BootedList1(0)
            BootedList1.RemoveAt(0)
            Dim RegionName = PropRegionClass.RegionName(Ruuid)
            Dim seconds = DateAndTime.DateDiff(DateInterval.Second, PropRegionClass.Timer(Ruuid), DateTime.Now)

            TextPrint($"{RegionName} {My.Resources.Running_word}: {CStr(seconds)} {My.Resources.Seconds_word}")
            If Not PropRegionClass.Status(Ruuid) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood Then
                PropRegionClass.Status(Ruuid) = RegionMaker.SIMSTATUSENUM.Booted
            End If

            If Settings.MapType = "None" AndAlso PropRegionClass.MapType(Ruuid).Length = 0 Then
                PropRegionClass.BootTime(Ruuid) = CInt(seconds)
            Else
                PropRegionClass.MapTime(Ruuid) = CInt(seconds)
            End If

            PropRegionClass.Timer(Ruuid) = Date.Now ' wait another interval

            ShowDOSWindow(GetHwnd(PropRegionClass.GroupName(Ruuid)), MaybeHideWindow())
            PropUpdateView = True
        End While

        ' check to see if a handle to all regions exists

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            Dim status = PropRegionClass.Status(RegionUUID)
            If CBool((status = RegionMaker.SIMSTATUSENUM.Booted) _
                    Or (status = RegionMaker.SIMSTATUSENUM.Booting) _
                    Or (status = RegionMaker.SIMSTATUSENUM.RecyclingDown) _
                    Or (status = RegionMaker.SIMSTATUSENUM.NoError) _
                    Or (status = RegionMaker.SIMSTATUSENUM.ShuttingDown) _
                    Or (status = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood) _
                    Or (status = RegionMaker.SIMSTATUSENUM.Suspended)) Then

                Dim G = PropRegionClass.GroupName(RegionUUID)

                If GetHwnd(G) = IntPtr.Zero Then
                    Try
                        If Not PropExitList.ContainsKey(G) Then
                            PropExitList.Add(G, "Exit")
                        End If
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End If
            End If

            If Not PropOpensimIsRunning() Then Exit For
            If Not PropRegionClass.RegionEnabled(RegionUUID) Then Continue For

            GroupName = PropRegionClass.GroupName(RegionUUID)

            Dim time2restart = PropRegionClass.Timer(RegionUUID).AddMinutes(CDbl(Settings.AutoRestartInterval))
            Dim Expired As Integer = DateTime.Compare(Date.Now, time2restart)

            Dim timesmartstart = PropRegionClass.Timer(RegionUUID).AddSeconds(Settings.SmartStartTimeout)
            Dim SSExpired = DateTime.Compare(Date.Now, timesmartstart)

            If (Expired > 0) Then
                If (Settings.AutoRestartInterval > 0) Then
                    If Settings.AutoRestartEnabled Then
                        PropRegionClass.Timer(RegionUUID) = Date.Now ' wait another interval
                    End If
                End If
            End If

            ' if it is past time and no one is in the sim... Smart shutdown
            If PropRegionClass.SmartStart(RegionUUID) = "True" _
                    And Settings.SmartStart _
                    And SSExpired > 0 _
                    And PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then

                ' time consuming, so a separate if
                If Not AvatarsIsInGroup(GroupName) Then
                    Logger("State Changed to ShuttingDown", GroupName, "Teleport")
                    ShutDown(RegionUUID)
                    For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
                        PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                    Next

                    PropUpdateView = True ' make form refresh
                    Continue For
                Else
                    For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
                        PropRegionClass.Timer(RegionUUID) = Date.Now ' wait another interval
                    Next
                End If
            End If

            ' auto restart timer
            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted _
                And Expired > 0 _
                And Settings.AutoRestartInterval() > 0 _
                And Settings.AutoRestartEnabled Then

                If Not AvatarsIsInGroup(GroupName) Then

                    ' shut down the group when AutoRestartInterval has gone by.
                    Logger("State is Time Exceeded, shutdown", GroupName, "Teleport")

                    ShowDOSWindow(GetHwnd(GroupName), MaybeShowWindow())
                    SequentialPause()
                    ' shut down all regions in the DOS box
                    ShutDown(RegionUUID)
                    Dim GroupList As List(Of String) = PropRegionClass.RegionUuidListByName(GroupName)
                    For Each UUID As String In GroupList
                        PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                    Next
                    Logger("State changed to RecyclingDown", GroupName, "Teleport")

                    TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Automatic_restart_word)
                    PropUpdateView = True
                    Continue For
                End If
            End If

            ' if a RestartPending is signaled, boot it up
            If status = RegionMaker.SIMSTATUSENUM.RestartPending Then
                Logger("State is RestartPending", GroupName, "Teleport")
                'RestartPending = 6
                Boot(RegionName)
                Logger("State is now Booted", PropRegionClass.RegionName(RegionUUID), "Teleport")
                PropUpdateView = True
                Continue For
            End If

            If status = RegionMaker.SIMSTATUSENUM.Resume Then
                '[Resume] = 8
                Logger("State is Resuming", GroupName, "Teleport")
                Dim GroupList As List(Of String) = PropRegionClass.RegionUuidListByName(GroupName)
                For Each R As String In GroupList
                    Boot(RegionName)
                Next
                PropUpdateView = True
                Continue For
            End If

            If status = RegionMaker.SIMSTATUSENUM.RestartStage2 Then
                'RestartStage2 = 11

                TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Restart_Pending_word)
                Dim GroupList As List(Of String) = PropRegionClass.RegionUuidListByName(GroupName)
                For Each R In GroupList
                    PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartPending
                    Logger("State changed to RestartPending", PropRegionClass.RegionName(R), "Teleport")
                Next
                PropUpdateView = True ' make form refresh
                Continue For
            End If
        Next

        ' now look at the exit stack
        While PropExitList.Count > 0
            GroupName = PropExitList.Keys.First
            Dim Reason = PropExitList.Item(GroupName) ' NoLogin or Exit
            PropExitList.Remove(GroupName)

            'If GroupName = "Welcome" Then
            'BreakPoint.Show("Break")
            'End If

            TextPrint(GroupName & " " & Reason)

            ' Need a region number and a Name. Name is either a region or a Group. For groups we need to get a region name from the group
            Dim GroupList As List(Of String) = PropRegionClass.RegionUuidListByName(GroupName)

            Dim PID As Integer
            Dim RegionUUID As String = ""
            If GroupList.Count > 0 Then
                RegionUUID = GroupList(0)
                ' Already done, just being safe here
                PID = PropRegionClass.ProcessID(RegionUUID)
                If PropInstanceHandles.ContainsKey(PID) Then
                    PropInstanceHandles.Remove(PID)
                End If
            Else
                Logger("No UUID", GroupName, "Teleport")
                Continue While
            End If

            If Reason = "NoLogin" Then
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.NoLogin
                PropUpdateView = True
                Logger("State changed to NoLogin", PropRegionClass.RegionName(RegionUUID), "Teleport")
                Continue While
            End If

            Dim Status = PropRegionClass.Status(RegionUUID)
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            Diagnostics.Debug.Print($"{RegionName} {GetStateString(Status)}")

            If Not PropRegionClass.RegionEnabled(RegionUUID) Then
                Continue While
            End If

            If Status = RegionMaker.SIMSTATUSENUM.NoError Then
                For Each R In GroupList
                    PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.Stopped
                Next
                PropUpdateView = True
                Continue While

            ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood Then
                For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped
                Next
                PropUpdateView = True ' make form refresh
                Continue While

            ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped
                StopGroup(GroupName)
                PropUpdateView = True
                Logger("State changed to Stopped", PropRegionClass.RegionName(RegionUUID), "Teleport")
                Continue While

            ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingDown And Not PropAborting Then
                'RecyclingDown = 4
                Logger("State is RecyclingDown", GroupName, "Teleport")
                TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Restart_Queued_word)
                For Each R In GroupList
                    PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartStage2
                Next
                Logger("State changed to RestartStage2", PropRegionClass.RegionName(RegionUUID), "Teleport")
                PropUpdateView = True
                Continue While

            ElseIf (Status = RegionMaker.SIMSTATUSENUM.RecyclingUp Or
                Status = RegionMaker.SIMSTATUSENUM.Booting Or
                Status = RegionMaker.SIMSTATUSENUM.Booted) And
                TimerValue >= 0 And
                Not PropAborting Then

                ' Maybe we crashed during warm up or running. Skip prompt if auto restart on crash and restart the beast
                Status = RegionMaker.SIMSTATUSENUM.Error
                PropUpdateView = True
                Application.DoEvents()
                Logger("Crash", GroupName & " Crashed", "Teleport")
                If Settings.RestartOnCrash Then

                    If PropRegionClass.CrashCounter(RegionUUID) > 4 Then
                        Logger("Crash", GroupName & " Crashed 4 times", "Teleport")
                        TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly)
                        StopGroup(GroupName)
                        PropRegionClass.CrashCounter(RegionUUID) = 0
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Error
                        PropUpdateView = True
                        Continue While
                    End If

                    PropRegionClass.CrashCounter(RegionUUID) += 1

                    ' shut down all regions in the DOS box
                    TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly)
                    StopGroup(GroupName)
                    TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Restart_Queued_word)
                    For Each R In GroupList
                        PropRegionClass.Status(R) = RegionMaker.SIMSTATUSENUM.RestartStage2
                    Next
                    Logger("Stopped", GroupName & "  was stopped", "Teleport")
                    PropUpdateView = True
                    Continue While
                Else
                    If PropAborting Then
                        PropExitHandlerIsBusy = False
                        Return ' not if we are aborting
                    End If
                    TextPrint(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly)
                    StopGroup(GroupName)
                    Dim yesno = MsgBox(GroupName & " " & Global.Outworldz.My.Resources.Quit_unexpectedly & " " & Global.Outworldz.My.Resources.See_Log, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Critical, Global.Outworldz.My.Resources.Error_word)
                    If (yesno = vbYes) Then
                        Try
                            System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), $"""{PropRegionClass.OpensimIniPath(RegionUUID)}\Opensim.log""")
                        Catch ex As Exception
                            BreakPoint.Show(ex.Message)
                        End Try
                    End If

                End If
            End If
            PropUpdateView = True
        End While

        PropExitHandlerIsBusy = False

    End Sub

#End Region

#Region "StartStop"

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        ReallyQuit()

    End Sub

    ''' <summary>Form Load is main() for all DreamGrid</summary>
    ''' <param name="sender">Unused</param>
    ''' <param name="e">Unused</param>
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Application.EnableVisualStyles()

        Dim _myFolder As String = My.Application.Info.DirectoryPath

        ' setup a debug path
        If Debugger.IsAttached Then
            ' for debugging when compiling
            _myFolder = _myFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Debug", "")
            _myFolder = _myFolder.Replace("\Installer_Src\Setup DreamWorld\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
        End If

        PropCurSlashDir = _myFolder.Replace("\", "/")    ' because MySQL uses Unix like slashes, that's why

        If Not System.IO.File.Exists(_myFolder & "\OutworldzFiles\Settings.ini") Then
            Create_ShortCut(_myFolder & "\Start.exe")
        End If

        'Load Settings, if any
        Settings.Init(_myFolder)

        Settings.CurrentDirectory = _myFolder
        Settings.OpensimBinPath() = _myFolder & "\OutworldzFiles\Opensim\bin\"

        If Settings.KeepOnTopMain Then
            Me.TopMost = True
            KeepOnTopToolStripMenuItem.Image = My.Resources.tables
        Else
            Me.TopMost = False
            KeepOnTopToolStripMenuItem.Image = My.Resources.table
        End If

        Log("Startup:", DisplayObjectInfo(Me))
        SetScreen()     ' move Form to fit screen from SetXY.ini

        Dim cinfo() = System.Globalization.CultureInfo.GetCultures(CultureTypes.AllCultures)
        Try
            My.Application.ChangeUICulture(Settings.Language)
            My.Application.ChangeCulture(Settings.Language)
        Catch
            My.Application.ChangeUICulture("en")
            My.Application.ChangeCulture("en")
        End Try

        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture

        TextPrint("Language is " & CultureInfo.CurrentCulture.Name)

        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        Application.DoEvents()
        FrmHome_Load(sender, e) 'Load everything in your form load event again so it will be translated

    End Sub

    Private Sub FrmHome_Load(ByVal sender As Object, ByVal e As EventArgs)

        'Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name)

        AddUserToolStripMenuItem.Text = Global.Outworldz.My.Resources.Add_User_word
        AdvancedSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.earth_network
        AdvancedSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Settings_word
        AdvancedSettingsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.All_Global_Settings_word
        AllUsersAllSimsToolStripMenuItem.Text = Global.Outworldz.My.Resources.All_Users_All_Sims_word
        BackupCriticalFilesToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_blue
        BackupCriticalFilesToolStripMenuItem.Text = Global.Outworldz.My.Resources.System_Backup_word
        BackupDatabaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_blue
        BackupDatabaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Backup_Databases
        BackupRestoreToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_blue
        BackupRestoreToolStripMenuItem.Text = Global.Outworldz.My.Resources.SQL_Database_Backup_Restore
        BusyButton.Text = Global.Outworldz.My.Resources.Busy_word
        CHeckForUpdatesToolStripMenuItem.Image = Global.Outworldz.My.Resources.download
        CHeckForUpdatesToolStripMenuItem.Text = Global.Outworldz.My.Resources.Check_for_Updates_word
        ChangePasswordToolStripMenuItem.Text = Global.Outworldz.My.Resources.Change_Password_word
        ChartWrapper1.AxisXTitle = Global.Outworldz.My.Resources.Minutes_word
        ChartWrapper2.AxisXTitle = Global.Outworldz.My.Resources.Minutes_word
        CheckAndRepairDatbaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Server_Client
        CheckAndRepairDatbaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Check_and_Repair_Database_word
        ClothingInventoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.user1_into
        ClothingInventoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Free_Avatar_Inventory_word
        ClothingInventoryToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Load_Free_Avatar_Inventory_text
        CommonConsoleCommandsToolStripMenuItem.Image = Global.Outworldz.My.Resources.text_marked
        CommonConsoleCommandsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Issue_Commands
        ConsoleCOmmandsToolStripMenuItem1.Image = Global.Outworldz.My.Resources.text_marked
        ConsoleCOmmandsToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_Console
        ConsoleCOmmandsToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Help_Console_text
        ConsoleToolStripMenuItem1.Image = Global.Outworldz.My.Resources.window_add
        ConsoleToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Consoles_word
        ConsoleToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Consoletext
        Debug.Text = Global.Outworldz.My.Resources.Debug_word
        DebugToolStripMenuItem.Text = Global.Outworldz.My.Resources.Set_Debug_Level_word
        DiagnosticsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Server_Client
        DiagnosticsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Network_Diagnostics
        DiagnosticsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Network_Diagnostics_text
        ErrorToolStripMenuItem.Text = Global.Outworldz.My.Resources.Error_word
        Fatal1.Text = Global.Outworldz.My.Resources.Float
        FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.File_word
        HelpOnIARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.disks
        HelpOnIARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_On_IARS_word
        HelpOnIARSToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Help_IARS_text
        HelpOnOARsToolStripMenuItem.Image = Global.Outworldz.My.Resources.disks
        HelpOnOARsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_OARS
        HelpOnOARsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Help_OARS_text
        HelpOnSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.gear
        HelpOnSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_Manuals_word
        HelpStartingUpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.box_tall
        HelpStartingUpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_Startup
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem2.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem3.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem3.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem4.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem4.Text = Global.Outworldz.My.Resources.Help_word
        Info.Text = Global.Outworldz.My.Resources.Info_word
        IslandToolStripMenuItem.Image = Global.Outworldz.My.Resources.box_tall
        IslandToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Free_DreamGrid_OARs_word
        JobEngineToolStripMenuItem.Text = Global.Outworldz.My.Resources.JobEngine_word
        JustOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Just_one_region_word
        JustQuitToolStripMenuItem.Image = Global.Outworldz.My.Resources.exit_icon
        JustQuitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Quit_Now_Word
        KeepOnTopToolStripMenuItem.Text = Global.Outworldz.My.Resources.Window_Word
        LanguageToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Language
        LoadIARsToolMenuItem.Image = Global.Outworldz.My.Resources.user1_into
        LoadIARsToolMenuItem.Text = Global.Outworldz.My.Resources.Inventory_IAR_Load_and_Save_words
        LoadLocalOARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.box_tall
        LoadLocalOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Local_OARs_word
        LoopBackToolStripMenuItem.Image = Global.Outworldz.My.Resources.refresh
        LoopBackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_On_LoopBack_word
        LoopBackToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Help_Loopback_Text
        MnuContent.Text = Global.Outworldz.My.Resources.Content_word
        MoreFreeIslandsandPartsContentToolStripMenuItem.Image = Global.Outworldz.My.Resources.download
        MoreFreeIslandsandPartsContentToolStripMenuItem.Text = Global.Outworldz.My.Resources.More_Free_Islands_and_Parts_word
        MoreFreeIslandsandPartsContentToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Free_DLC_word
        RestartMysqlIcon.Image = Global.Outworldz.My.Resources.gear
        RestartMysqlIcon.Text = Global.Outworldz.My.Resources.Mysql_Word
        Off1.Text = Global.Outworldz.My.Resources.Off
        OnTopToolStripMenuItem.Text = Global.Outworldz.My.Resources.On_Top
        PDFManualToolStripMenuItem.Image = Global.Outworldz.My.Resources.pdf
        PDFManualToolStripMenuItem.Text = Global.Outworldz.My.Resources.PDF_Manual_word
        RegionsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Server_Client
        RegionsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Regions_word
        RestartApacheIcon.Image = Global.Outworldz.My.Resources.gear
        RestartApacheIcon.Text = Global.Outworldz.My.Resources.Apache_word
        RestartIceCastItem2.Image = Global.Outworldz.My.Resources.recycle
        RestartIceCastItem2.Text = Global.Outworldz.My.Resources.Restart_word
        RestartIcecastIcon.Image = Global.Outworldz.My.Resources.gear
        RestartIcecastIcon.Text = Global.Outworldz.My.Resources.Icecast_word
        RestartMysqlItem.Image = Global.Outworldz.My.Resources.recycle
        RestartMysqlItem.Text = Global.Outworldz.My.Resources.Restart_word
        RestartOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Restart_one_region_word
        RestartRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Restart_Region_word
        RestartRobustItem.Image = Global.Outworldz.My.Resources.recycle
        RestartRobustItem.Text = Global.Outworldz.My.Resources.Restart_word
        RestartTheInstanceToolStripMenuItem.Text = Global.Outworldz.My.Resources.Restart_one_instance_word
        RestartToolStripMenuItem2.Image = Global.Outworldz.My.Resources.recycle
        RestartToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Restart_word
        RestoreDatabaseToolStripMenuItem1.Image = Global.Outworldz.My.Resources.cube_blue
        RestoreDatabaseToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Restore_Database_word
        RevisionHistoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_dirty
        RevisionHistoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Revision_History_word
        RestartRobustIcon.Image = Global.Outworldz.My.Resources.gear
        RestartRobustIcon.Text = Global.Outworldz.My.Resources.Robust_word
        ScriptsResumeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Scripts_Resume_word
        ScriptsStartToolStripMenuItem.Text = Global.Outworldz.My.Resources.Scripts_Start_word
        ScriptsStopToolStripMenuItem.Text = Global.Outworldz.My.Resources.Scripts_Stop_word
        ScriptsSuspendToolStripMenuItem.Text = Global.Outworldz.My.Resources.Scripts_Suspend_word
        ScriptsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Scripts_word
        SeePortsInUseToolStripMenuItem.Image = Global.Outworldz.My.Resources.server_connection
        SeePortsInUseToolStripMenuItem.Text = Global.Outworldz.My.Resources.See_Ports_In_Use_word
        SendAlertToAllUsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Send_Alert_Message_word
        ShowHyperGridAddressToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
        ShowHyperGridAddressToolStripMenuItem.Text = Global.Outworldz.My.Resources.Show_Grid_Address
        ShowHyperGridAddressToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Grid_Address_text
        ShowStatusToolStripMenuItem.Text = Global.Outworldz.My.Resources.Show_Status_word
        ShowUserDetailsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Show_User_Details_word
        SimulatorStatsToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
        SimulatorStatsToolStripMenuItem.Text = Global.Outworldz.My.Resources.View_Simulator_Stats
        StartButton.Text = Global.Outworldz.My.Resources.Start_word
        StopButton.Text = Global.Outworldz.My.Resources.Stop_word
        TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_dirty
        TechnicalInfoToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_Technical
        TechnicalInfoToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Help_Technical_text
        ThreadpoolsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Thread_pools_word
        ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.document_connection
        ToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_Forward
        ToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Help_Forward_text
        TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_view
        TroubleshootingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_Troubleshooting_word
        UsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Users_word
        ViewIcecastWebPageToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
        ViewIcecastWebPageToolStripMenuItem.Text = Global.Outworldz.My.Resources.View_Icecast
        ViewLogsToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_view
        ViewLogsToolStripMenuItem.Text = Global.Outworldz.My.Resources.View_Logs
        ViewRegionMapToolStripMenuItem.Image = Global.Outworldz.My.Resources.Good
        ViewRegionMapToolStripMenuItem.Text = Global.Outworldz.My.Resources.View_Maps
        ViewWebUI.Image = Global.Outworldz.My.Resources.document_view
        ViewWebUI.Text = Global.Outworldz.My.Resources.View_Web_Interface
        ViewWebUI.ToolTipText = Global.Outworldz.My.Resources.View_Web_Interface_text
        Warn.Text = Global.Outworldz.My.Resources.Warn_word
        XengineToolStripMenuItem.Text = Global.Outworldz.My.Resources.XEngine_word
        mnuAbout.Image = Global.Outworldz.My.Resources.question_and_answer
        mnuAbout.Text = Global.Outworldz.My.Resources.About_word
        mnuExit.Image = Global.Outworldz.My.Resources.exit_icon
        mnuExit.Text = Global.Outworldz.My.Resources.Exit__word
        mnuHide.Image = Global.Outworldz.My.Resources.navigate_down
        mnuHide.Text = Global.Outworldz.My.Resources.Hide
        mnuHideAllways.Image = Global.Outworldz.My.Resources.navigate_down2
        mnuHideAllways.Text = Global.Outworldz.My.Resources.Hide_Allways_word
        mnuSettings.Text = Global.Outworldz.My.Resources.Setup_word
        mnuShow.Image = Global.Outworldz.My.Resources.navigate_up
        mnuShow.Text = Global.Outworldz.My.Resources.Show_word

        ' OAR AND IAR MENU
        SearchForObjectsMenuItem.Text = Global.Outworldz.My.Resources.Search_Events
        SearchForGridsMenuItem.Text = Global.Outworldz.My.Resources.Search_grids
        LoadInventoryIARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Load_Inventory_IAR
        SaveAllRunningRegiondsAsOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Save_All_Regions
        LoadRegionOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Region_OAR
        LoadLocalOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.OAR_load_save_backupp_word
        SaveInventoryIARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Save_Inventory_IAR_word
        SaveRegionOARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Save_Region_OAR_word

        ' show box styled nicely.
        Application.EnableVisualStyles()
        Buttons(BusyButton)

        TextBox1.BackColor = Me.BackColor
        ' initialize the scrolling text box

        ToolBar(False)

        Adv1 = New FormSettings

        Me.Show()
        TextPrint("GitVersion: #" & GitVersion())
        TextPrint(My.Resources.Version_word & " " & PropMyVersion)
        TextPrint(My.Resources.Version_word & " " & PropSimVersion)
        TextPrint(My.Resources.Getting_regions_word)
        PropRegionClass = RegionMaker.Instance()

        PropRegionClass.Init()

        UpgradeDotNet()

        Dim wql As ObjectQuery = New ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem")
        searcher = New ManagementObjectSearcher(wql)

        CopyWifi()
        Cleanup() ' old files thread
        PropMyUPnpMap = New UPnp()

        DeleteOldFiles()

        TextPrint(My.Resources.Starting_WebServer_word)
        'must start after region Class Is instantiated
        PropWebServer = NetServer.GetWebServer
        PropWebServer.StartServer(Settings.CurrentDirectory, Settings)
        Application.DoEvents()
        CheckDiagPort()

        ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable, but it needs to be unique
        Randomize()
        If Settings.MachineID().Length = 0 Then Settings.MachineID() = RandomNumber.Random  ' a random machine ID may be generated.  Happens only once

        ' WebUI
        ViewWebUI.Visible = Settings.WifiEnabled

        Me.Text += " V" & PropMyVersion

        PropOpensimIsRunning() = False ' true when opensim is running

        Application.DoEvents()

        ClearOldLogFiles() ' clear log files

        If Not IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "BareTail.udm")) Then
            IO.File.Copy(IO.Path.Combine(Settings.CurrentDirectory, "BareTail.udm.bak"), IO.Path.Combine(Settings.CurrentDirectory, "BareTail.udm"))
        End If

        CheckDefaultPorts()

        TextPrint(My.Resources.Setup_Network)
        Application.DoEvents()
        SetPublicIP()

        SetServerType()
        Application.DoEvents()
        TextPrint(My.Resources.Setup_Ports_word)
        OpenPorts()

        Application.DoEvents()
        SetQuickEditOff()
        Application.DoEvents()
        SetLoopback()
        Application.DoEvents()
        'mnuShow shows the DOS box for Opensimulator
        Select Case Settings.ConsoleShow
            Case "True"
                mnuShow.Checked = True
                mnuHide.Checked = False
                mnuHideAllways.Checked = False
            Case "False"
                mnuShow.Checked = False
                mnuHide.Checked = True
                mnuHideAllways.Checked = False
            Case "None"
                mnuShow.Checked = False
                mnuHide.Checked = False
                mnuHideAllways.Checked = True
        End Select

        If SetIniData() Then
            MsgBox("Failed to setup", MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxSetForeground, My.Resources.Error_word)
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        With Cpu1
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        CheckForUpdates()
        Application.DoEvents()

        mnuSettings.Visible = True

        LoadHelp()      ' Help loads once

        FixUpdater()    ' replace DreamGridUpdater.exe with DreamGridUpdater.new

        If Settings.Password = "secret" Or Settings.Password.Length = 0 Then
            Dim Password = New PassGen
            Settings.Password = Password.GeneratePass()
        End If

        TextPrint(My.Resources.Setup_Graphs_word)
        ' Graph fill

        Dim msChart = ChartWrapper1.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = True
        msChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        ChartWrapper1.AddMarkers = True
        ChartWrapper1.MarkerFreq = 60

        msChart = ChartWrapper2.TheChart
        msChart.ChartAreas(0).AxisX.Maximum = 180
        msChart.ChartAreas(0).AxisX.Minimum = 0
        msChart.ChartAreas(0).AxisY.Maximum = 100
        msChart.ChartAreas(0).AxisY.Minimum = 0
        msChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        msChart.ChartAreas(0).AxisY.LabelStyle.Enabled = True
        ChartWrapper2.AddMarkers = True
        ChartWrapper2.MarkerFreq = 60

        If Settings.RegionListVisible Then
            ShowRegionform()
        End If

        TextPrint(My.Resources.Checking_MySql_word)
        Application.DoEvents()
        If MysqlInterface.IsMySqlRunning() Then PropStopMysql() = False

        TextPrint(My.Resources.RefreshingOAR)
        ContentOAR = New FormOAR
        ContentOAR.Init("OAR")

        ContentIAR = New FormOAR
        ContentIAR.Init("IAR")

        Application.DoEvents()
        LoadLocalIAROAR() ' load IAR and OAR local content

        TextPrint(My.Resources.Setup_Ports_word)
        Application.DoEvents()
        PropRegionClass.UpdateAllRegionPorts() ' must be after SetIniData

        If Settings.Autostart Then
            TextPrint(My.Resources.Auto_Startup_word)
            Application.DoEvents()
            Startup()
        Else
            Settings.SaveSettings()
            TextPrint(My.Resources.Ready_to_Launch & vbCrLf & "------------------" & vbCrLf & Global.Outworldz.My.Resources.Click_Start_2_Begin & vbCrLf)
            Application.DoEvents()
            Buttons(StartButton)
        End If

        ' Get the names of all the lands
        InitLand()
        InitTrees()

        HelpOnce("License") ' license on bottom
        HelpOnce("Startup")

        Joomla.CheckForjOpensimUpdate()

    End Sub

#End Region

#Region "Language"

    Public Sub Language(sender As Object, e As EventArgs)
        Settings.SaveSettings()

        'For Each ci As CultureInfo In CultureInfo.GetCultures(CultureTypes.NeutralCultures)
        'Diagnostics.Debug.Print("")
        'Diagnostics.Debug.Print(ci.Name)
        'Diagnostics.Debug.Print(ci.TwoLetterISOLanguageName)
        'Diagnostics.Debug.Print(ci.ThreeLetterISOLanguageName)
        'Diagnostics.Debug.Print(ci.ThreeLetterWindowsLanguageName)
        'Diagnostics.Debug.Print(ci.DisplayName)
        'Diagnostics.Debug.Print(ci.EnglishName)
        'Next

        My.Application.ChangeUICulture(Settings.Language)
        My.Application.ChangeCulture(Settings.Language)
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        FrmHome_Load(sender, e) 'Load everything in your form load event again
    End Sub

#End Region

#Region "Help"

    Private Sub HelpClick(sender As Object, e As EventArgs)

        If sender.Text.toupper(Globalization.CultureInfo.InvariantCulture) <> "DreamGrid Manual.pdf".ToUpper(Globalization.CultureInfo.InvariantCulture) Then HelpManual(CStr(sender.Text))

    End Sub

    Private Sub HelpOnIARSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnIARSToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Inventory_Archives"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub HelpOnOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpOnOARsToolStripMenuItem.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Load_Oar_0.9.0%2B"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub HelpStartingUpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpStartingUpToolStripMenuItem1.Click

        HelpManual("Startup")

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click

        HelpManual("Database")

    End Sub

    Private Sub HelpToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem2.Click

        HelpManual("ServerType")

    End Sub

    Private Sub HelpToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem3.Click

        HelpManual("Apache")

    End Sub

    Private Sub HelpToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem4.Click

        HelpManual("Icecast")

    End Sub

    Private Sub TechnicalInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TechnicalInfoToolStripMenuItem.Click
        Dim webAddress As String = PropDomain & "/Outworldz_installer/technical.htm"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "Loopback"

#End Region

    Private Shared Sub SetLoopback()

        Dim Adapters = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In Adapters
            Diagnostics.Debug.Print(adapter.Name)

            If adapter.Name = "Loopback" Then
                TextPrint(My.Resources.Setting_Loopback)
                Using LoopbackProcess As New Process
                    LoopbackProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                    LoopbackProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "NAT_Loopback_Tool.bat")
                    LoopbackProcess.StartInfo.CreateNoWindow = True
                    LoopbackProcess.StartInfo.Arguments = "Loopback"
                    LoopbackProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    Try
                        LoopbackProcess.Start()
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End Using
            End If
        Next

    End Sub

#Region "Help"

    Private Sub LoadHelp()

        ' read help files for menu

        Dim folders As Array = Nothing
        Try
            folders = Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Help"))
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        For Each aline As String In folders
            If aline.EndsWith(".htm", StringComparison.InvariantCultureIgnoreCase) Then
                aline = System.IO.Path.GetFileNameWithoutExtension(aline)
                Dim HelpMenu As New ToolStripMenuItem With {
                    .Text = aline,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text,
                    .Image = Global.Outworldz.My.Resources.question_and_answer
                }
                AddHandler HelpMenu.Click, New EventHandler(AddressOf HelpClick)
                HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HelpMenu})
            End If
        Next

        AddLog("All Logs")
        AddLog("Robust")
        AddLog("Error")
        AddLog("Outworldz")
        AddLog("Icecast")
        AddLog("MySQL")
        AddLog("All Settings")
        AddLog("--- Regions ---")
        For Each RegionUUID As String In PropRegionClass.RegionUuids
            Dim Name = PropRegionClass.RegionName(RegionUUID)
            AddLog("Region " & Name)
        Next

    End Sub

#End Region

#Region "Clicks"

    Public Shared Sub BuildLand(Avatars As Dictionary(Of String, String))

        If Not Settings.AutoFill Then Return
        If Avatars.Count = 0 Then Return

        For Each Agent In Avatars
            If Agent.Value.Length > 0 Then

                Dim RegionName = Agent.Value
                Dim RegionUUID = PropRegionClass.FindRegionByName(RegionName)
                Dim X = PropRegionClass.CoordX(RegionUUID)
                Dim Y = PropRegionClass.CoordY(RegionUUID)
                If X = 0 Or Y = 0 Then Continue For

#Disable Warning BC42016 ' Implicit conversion
                Dim start As ParameterizedThreadStart = AddressOf SurroundingLandMaker
#Enable Warning BC42016 ' Implicit conversion

                Dim _WebThread1 = New Thread(start)
                _WebThread1.SetApartmentState(ApartmentState.STA)
                _WebThread1.Priority = ThreadPriority.BelowNormal
                _WebThread1.Start(RegionUUID)

            End If

        Next

    End Sub

    Private Sub LogViewClick(sender As Object, e As EventArgs)

        Viewlog(CStr(sender.Text))

    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click

        HelpManual("Loopback Fixes")

    End Sub

    Private Sub MnuAbout_Click(sender As System.Object, e As EventArgs) Handles mnuAbout.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim HelpAbout = New FormCopyright
#Enable Warning CA2000 ' Dispose objects before losing scope
        HelpAbout.Show()
        HelpAbout.Activate()
        HelpAbout.Select()
        HelpAbout.BringToFront()

    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As EventArgs) Handles mnuExit.Click
        ReallyQuit()
    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As EventArgs) Handles mnuHide.Click

        TextPrint(My.Resources.Not_Shown)
        mnuShow.Checked = False
        mnuHide.Checked = True
        mnuHideAllways.Checked = False

        Settings.ConsoleShow = "False"
        Settings.SaveSettings()

    End Sub

    Private Sub MnuHideAllways_Click(sender As Object, e As EventArgs) Handles mnuHideAllways.Click
        TextPrint(My.Resources.Not_Shown)
        mnuShow.Checked = False
        mnuHide.Checked = False
        mnuHideAllways.Checked = True

        Settings.ConsoleShow = "None"
        Settings.SaveSettings()

    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreFreeIslandsandPartsContentToolStripMenuItem.Click

        Dim webAddress As String = PropDomain & "/cgi/freesculpts.plx"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub MysqlPictureBox_Click(sender As Object, e As EventArgs)

        If MysqlInterface.IsMySqlRunning() Then
            PropStopMysql = True
            StopMysql()
        Else
            StartMySQL()
        End If

    End Sub

    Private Sub PDFManualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFManualToolStripMenuItem.Click
        Dim webAddress As String = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Help\Dreamgrid Manual.pdf")
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReallyQuit()

        If Not KillAll() Then Return

        cpu.Dispose()
        searcher.Dispose()

        If PropWebServer IsNot Nothing Then
            PropWebServer.StopWebServer()
        End If

        PropAborting = True
        StopMysql()

        TextPrint("Zzzz...")
        Thread.Sleep(2000)
        End

    End Sub

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        ShowRegionform()

    End Sub

    Private Sub Resize_page(ByVal sender As Object, ByVal e As EventArgs)
        ScreenPosition1.SaveXY(Me.Left, Me.Top)
        ScreenPosition1.SaveHW(Me.Height, Me.Width)
    End Sub

    Private Sub RestartDOSboxes()

        If PropRestartRobust And PropRobustExited = True Then
            PropRobustExited = False
            RobustIcon(False)
            If Not StartRobust() Then Return
        End If

        If PropMysqlExited Then
            MySQLIcon(False)
            StartMySQL()
        End If

        If PropApacheExited Then
            ApacheIcon(False)
            StartApache()
        End If

        If PropIceCastExited Then
            IceCastIcon(False)
            StartIcecast()
        End If

    End Sub

    Private Sub RestartOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartOneRegionToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)

        If RegionUUID.Length > 0 Then
            ShutDown(RegionUUID)
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
            Logger("RecyclingDown", PropRegionClass.RegionName(RegionUUID), "Teleport")
            PropUpdateView = True ' make form refresh
        End If

        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub RestartTheInstanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartTheInstanceToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If
        Dim name = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(name)
        If RegionUUID.Length > 0 Then
            ShutDown(RegionUUID)
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
            Logger("RecyclingDown", PropRegionClass.RegionName(RegionUUID), "Teleport")
            PropUpdateView = True ' make form refresh
        End If

    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartMysqlItem.Click

        PropAborting = True
        PropStopMysql = True
        StopMysql()
        StartMySQL()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestartRobustItem.Click

        PropAborting = True
        StopRobust()
        StartRobust()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem2.Click

        If Not Settings.ApacheEnable Then
            ApacheIcon(False)
            TextPrint(My.Resources.Apache_Disabled)
        End If
        StopApache(True)
        StartApache()
        PropAborting = False

    End Sub

    Private Sub RestartToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles RestartIceCastItem2.Click

        If Not Settings.SCEnable Then
            Settings.SCEnable = True
        End If

        PropAborting = True
        StopIcecast()
        StartIcecast()
        PropAborting = False

    End Sub

    Private Sub RestoreDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestoreDatabaseToolStripMenuItem1.Click

        If PropOpensimIsRunning() Then
            TextPrint(My.Resources.Aborted_word)
            Return
        End If

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        ' Create an instance of the open file dialog box. Set filter options and filter index.
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog With {
            .InitialDirectory = BackupPath(),
            .Filter = Global.Outworldz.My.Resources.Backup_Folder & "(*.sql)|*.sql|All Files (*.*)|*.*",
            .FilterIndex = 1,
            .Multiselect = False
        }

        ' Call the ShowDialog method to show the dialogbox.
        Dim UserClickedOK As DialogResult = openFileDialog1.ShowDialog
        openFileDialog1.Dispose()
        ' Process input if the user clicked OK.
        If UserClickedOK = DialogResult.OK Then
            Dim thing = openFileDialog1.FileName
            If thing.Length > 0 Then

                Dim db As String
                If thing.ToUpper(Globalization.CultureInfo.InvariantCulture).Contains("ROBUST") Then
                    db = Settings.RobustDataBaseName
                Else
                    db = Settings.RegionDBName
                End If

                Dim yesno = MsgBox(My.Resources.Are_You_Sure, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Exclamation, Global.Outworldz.My.Resources.Restore_word)
                If yesno = vbYes Then

                    DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\RestoreMysql.bat"))

                    Try
                        Dim filename As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\RestoreMysql.bat")
                        Using outputFile As New StreamWriter(filename, True)
                            outputFile.WriteLine("@REM A program to restore Mysql from a backup" & vbCrLf _
                                & "mysql -u root " & db & " < " & """" & thing & """" _
                                & vbCrLf & "@pause" & vbCrLf)
                        End Using
                    Catch ex As Exception

                        ErrorLog("Failed to create restore file:" & ex.Message)
                        Return
                    End Try

                    Using pMySqlRestore As Process = New Process()
                        ' pi.Arguments = thing
                        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                            .WindowStyle = ProcessWindowStyle.Normal,
                            .WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\"),
                            .FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\RestoreMysql.bat")
                        }
                        pMySqlRestore.StartInfo = pi
                        TextPrint(My.Resources.Do_Not_Interrupt_word)
                        Try
                            pMySqlRestore.Start()
                        Catch ex As Exception
                            BreakPoint.Show(ex.Message)
                        End Try
                    End Using
                End If
            Else
                TextPrint(My.Resources.Cancelled_word)
            End If
        End If
    End Sub

    Private Sub RevisionHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevisionHistoryToolStripMenuItem.Click

        HelpManual("Revisions")

    End Sub

    Private Sub RobustPictureBox_Click(sender As Object, e As EventArgs)

        If Not IsRobustRunning() Then
            StartRobust()
        Else
            StopRobust()
        End If

    End Sub

    Private Function ScanAgents() As Integer

        If Not MysqlInterface.IsMySqlRunning() Then Return 0
        Dim sbttl As Integer = 0

        Try
            ' Scan all the regions
            Dim A = GetAgentList()
            Dim B = GetHGAgentList()

            Dim C As Dictionary(Of String, String) = Nothing

            C = A.Union(B).ToDictionary(Function(p) p.Key, Function(p) p.Value)

            If C IsNot Nothing Then BuildLand(C)

            '; start with zero avatars
            For Each RegionUUID As String In PropRegionClass.RegionUuids
                PropRegionClass.AvatarCount(RegionUUID) = 0
            Next

            AvatarLabel.Text = ""

            For Each NameValue In C
                Dim Avatar = NameValue.Key
                Dim RegionName = NameValue.Value

                If Not D.ContainsKey(Avatar) And RegionName.Length > 0 Then
                    TextPrint(Avatar & " is in " & RegionName)
                    D.Add(Avatar, RegionName)
                End If
            Next

            Dim Str As String = ""
            For Each NameValue In C
                Dim Avatar = NameValue.Key
                Dim RegionName = NameValue.Value

                Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
                If RegionUUID.Length > 0 And RegionName.Length > 0 Then
                    PropRegionClass.AvatarCount(RegionUUID) += 1
                    Str += Avatar & " in " & RegionName & ", "
                End If
            Next

            Dim E As New List(Of String)
            For Each NameValue In D
                Dim Avatar = NameValue.Key
                Dim RegionName = NameValue.Value

                If Not C.ContainsKey(Avatar) Then
                    TextPrint(Avatar & " left " & RegionName)
                    E.Add(Avatar)
                End If
            Next
            For Each F In E
                D.Remove(F)
            Next

            Dim total As Integer = C.Count
            AvatarLabel.Text = CStr(total) & " " & My.Resources.Avatars_word
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return sbttl

    End Function

    Private Sub ScriptsResumeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsResumeToolStripMenuItem.Click

        SendScriptCmd("scripts resume")
    End Sub

    Private Sub ScriptsStartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStartToolStripMenuItem.Click
        SendScriptCmd("scripts start")
    End Sub

    Private Sub ScriptsStopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsStopToolStripMenuItem.Click
        SendScriptCmd("scripts stop")
    End Sub

    Private Sub ScriptsSuspendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsSuspendToolStripMenuItem.Click
        SendScriptCmd("scripts suspend")
    End Sub

    Private Sub SeePortsInUseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeePortsInUseToolStripMenuItem.Click

        Using CPortsProcess As New Process
            CPortsProcess.StartInfo.UseShellExecute = True
            CPortsProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "Cports.exe")
            CPortsProcess.StartInfo.CreateNoWindow = False
            CPortsProcess.StartInfo.Arguments = ""
            CPortsProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Try
                CPortsProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End Using

    End Sub

    ''' <summary>Sets H,W and pos of screen on load</summary>
    Private Sub SetScreen()
        '365, 238 default
        ScreenPosition1 = New ScreenPos("Form1")
        AddHandler ResizeEnd, HandlerSetup
        Dim xy As List(Of Integer) = ScreenPosition1.GetXY()
        Left = xy.Item(0)
        Top = xy.Item(1)

        Dim hw As List(Of Integer) = ScreenPosition1.GetHW()

        If hw.Item(0) = 0 Then
            Me.Height = 238
        Else
            Try
                Me.Height = hw.Item(0)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

        If hw.Item(1) = 0 Then
            Me.Width = 365
        Else
            Me.Width = hw.Item(1)

        End If

        ScreenPosition1.SaveHW(Me.Height, Me.Width)

    End Sub

#End Region

#Region "Things"

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        TextPrint(My.Resources.Grid_Address_is_word & vbCrLf & "http://" & Settings.PublicIP & ":" & Settings.HttpPort)

    End Sub

    Private Sub ShowRegionform()

        PropRegionForm = New FormRegionlist
        PropRegionForm.Show()
        PropRegionForm.Activate()
        PropRegionForm.Select()
        PropRegionForm.BringToFront()

    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles mnuShow.Click

        TextPrint(My.Resources.Is_Shown)
        mnuShow.Checked = True
        mnuHide.Checked = False
        mnuHideAllways.Checked = False

        Settings.ConsoleShow = "True"
        Settings.SaveSettings()

    End Sub

    Private Sub ShowUserDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowUserDetailsToolStripMenuItem.Click
        Dim person = InputBox(My.Resources.Enter_1_2)
        If person.Length > 0 Then
            ConsoleCommand(RobustName, "show account " & person)
        End If
    End Sub

    Private Sub StartButton_Click(sender As System.Object, e As EventArgs) Handles StartButton.Click
        Startup()
    End Sub

    Private Sub Statmenu(sender As Object, e As EventArgs)
        If PropOpensimIsRunning() Then
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(CStr(sender.Text))
            Dim port As String = CStr(PropRegionClass.RegionPort(RegionUUID))
            Dim webAddress As String = "http://localhost:" & Settings.HttpPort & "/bin/data/sim.html?port=" & port
            Try
                Process.Start(webAddress)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        Else
            TextPrint(My.Resources.Not_Running)
        End If
    End Sub

#End Region

#Region "Stopping"

    Public Sub StopMysql()

        If Not MysqlInterface.IsMySqlRunning() Then
            Application.DoEvents()
            MysqlInterface.IsRunning = False    ' mark all as not running
            MySQLIcon(False)
            Return
        End If

        If Not PropStopMysql Then
            MysqlInterface.IsRunning = True    ' mark all as  running
            MySQLIcon(True)
            TextPrint(My.Resources.MySQL_Was_Running)
            Return
        End If

        TextPrint("MySQL " & Global.Outworldz.My.Resources.Stopping_word)

        Using p As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "--port " & CStr(Settings.MySqlRobustDBPort) & " -u root shutdown",
            .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin\mysqladmin.exe") & """",
            .UseShellExecute = True, ' so we can redirect streams and minimize
            .WindowStyle = ProcessWindowStyle.Hidden
        }
            p.StartInfo = pi

            Try
                p.Start()
                MysqlInterface.IsRunning = False
                Application.DoEvents()
                p.WaitForExit()
            Catch
            End Try

        End Using

        MySQLIcon(False)
        If MysqlInterface.IsMySqlRunning() Then
            MysqlInterface.IsRunning = True    ' mark all as running
            MySQLIcon(True)
        End If

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As EventArgs) Handles StopButton.Click

        DoStopActions()

    End Sub

#End Region

#Region "Scrolling text box"

    Private Sub TextBox1_TextChanged(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

#End Region

#Region "Updater"

    Private Sub UpdaterGo(Filename As String)

        KillAll()
        StopApache(True) 'really stop it, even if a service
        StopMysql()
        Application.DoEvents()
        Dim pUpdate As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = Filename,
            .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "DreamGridSetup.exe") & """"
        }
        pUpdate.StartInfo = pi
        TextPrint(My.Resources.SeeYouSoon)
        Try
            pUpdate.Start()
        Catch ex As Exception

            ErrorLog(My.Resources.ErrInstall)
        End Try
        End ' program

    End Sub

#End Region

#Region "Timer"

    ''' <summary>
    ''' Timer runs every second registers DNS,looks for web server stuff that arrives, restarts any sims , updates lists of agents builds teleports.html for older teleport checks for crashed regions
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As EventArgs) Handles Timer1.Tick

        If Not PropOpensimIsRunning() Then
            Timer1.Stop()
            TimerBusy = 0
            Return
        End If

        Chart() ' do charts collection each second

        If TimerBusy > 0 And TimerBusy < 10 Then
            Diagnostics.Debug.Print("Ticker busy")
            TimerBusy += 1
            Timer1.Interval += 100
            Diagnostics.Debug.Print("Timer Is Now at " & CStr(Timer1.Interval) & " ms")
            Return
        End If

        Timer1.Interval = 1000
        TimerBusy = 1

        If SecondsTicker Mod ExitInterval = 0 And SecondsTicker > 0 Then
            PropRegionClass.CheckPost() ' get the stack filled ASAP
            ExitHandlerPoll() ' see if any regions have exited and set it up for Region Restart
            TeleportAgents()
            RestartDOSboxes()
            CalcDiskFree()
        End If

        Dim thisDate As Date = Now
        Dim dt As String = thisDate.ToString(Globalization.CultureInfo.CurrentCulture)

        ' print how many backups are running
        Dim t = BackupsRunning(dt)
        If t.Length > 0 Then
            TextPrint(t)
        End If

        ' 10 seconds, not at boot
        If SecondsTicker Mod 5 = 0 And SecondsTicker > 0 Then
            ScanAgents() ' update agent count seconds
        End If

        ' every minute
        If SecondsTicker Mod 60 = 0 Then
            CalcCPU() ' get a list of running opensim processes
            BackupThread.RunAllBackups(False) ' run background based on time of day = false
            RegionListHTML(Settings, PropRegionClass, "Name") ' create HTML for teleport boards
        End If

        ' print hourly marks on console, after boot
        If SecondsTicker Mod 3600 = 0 Then
            TextPrint($"{dt} {Global.Outworldz.My.Resources.Running_word} {CInt((SecondsTicker / 3600)).ToString(Globalization.CultureInfo.InvariantCulture)} {Global.Outworldz.My.Resources.Hours_word}")
            SetPublicIP()
            ExpireLogsByAge()
            DeleteDirectoryTmp()
        End If

        SecondsTicker += 1
        TimerBusy = 0

    End Sub

#End Region

#Region "Toolbars"

    Private Sub AddLog(name As String)
        Dim LogMenu As New ToolStripMenuItem With {
                .Text = name,
                .ToolTipText = Global.Outworldz.My.Resources.Click_to_View_this_word,
                .Size = New Size(269, 26),
                .Image = Global.Outworldz.My.Resources.document_view,
                .DisplayStyle = ToolStripItemDisplayStyle.Text
            }
        AddHandler LogMenu.Click, New EventHandler(AddressOf LogViewClick)
        ViewLogsToolStripMenuItem.Visible = True
        ViewLogsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {LogMenu})

    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click

        ConsoleCommand(RobustName, "create user")

    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click

        If PropOpensimIsRunning() Then
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            Else
                Dim webAddress As String = "http://127.0.0.1:" & Settings.HttpPort
                Try
                    Process.Start(webAddress)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
                TextPrint(My.Resources.User_Name_word & ":" & Settings.AdminFirst & " " & Settings.AdminLast)
                TextPrint(My.Resources.Password_word & ":" & Settings.Password)
            End If
        Else
            If Settings.ApacheEnable Then
                Dim webAddress As String = "http://127.0.0.1:" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture)
                Try
                    Process.Start(webAddress)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            Else
                TextPrint(My.Resources.Not_Running)
            End If
        End If

    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        SkipSetup = False
        Adv1.Activate()
        Adv1.Visible = True
        Adv1.Select()
        Adv1.Init()
        Adv1.BringToFront()

    End Sub

    Private Sub AllUsersAllSimsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustOneRegionToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If
        Dim RegionName = ChooseRegion(True)
        If RegionName.Length > 0 Then
            Dim Message = InputBox(My.Resources.What_to_say_2_region)
            If Message.Length = 0 Then Return

            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                SendMessage(RegionUUID, Message)
                SendAdminMessage(RegionUUID, Message)
            End If

        End If

    End Sub

    Private Sub BackupCriticalFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupCriticalFilesToolStripMenuItem.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim CriticalForm As New FormBackupCheckboxes
#Enable Warning CA2000 ' Dispose objects before losing scope

        CriticalForm.Activate()
        CriticalForm.Visible = True
        CriticalForm.Select()
        CriticalForm.BringToFront()

    End Sub

    Private Sub BackupDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupDatabaseToolStripMenuItem.Click

        Dim A As New Backups
        A.BackupSQLDB(Settings.RegionDBName)
        If Settings.RegionDBName <> Settings.RobustDataBaseName Then
            Sleep(5000)
            Dim B As New Backups
            B.BackupSQLDB(Settings.RobustDataBaseName)
        End If

    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click

        ConsoleCommand(RobustName, "reset user password")

    End Sub

    Private Sub CheckAndRepairDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckAndRepairDatbaseToolStripMenuItem.Click

        If Not StartMySQL() Then
            ToolBar(False)
            Buttons(StartButton)
            TextPrint(My.Resources.Stopped_word)
            Return
        End If

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\mysql\bin"))
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = CStr(Settings.MySqlRobustDBPort)

        pi.FileName = "CheckAndRepair.bat"
        Using pMySqlDiag1 As Process = New Process With {
                .StartInfo = pi
            }
            Try
                pMySqlDiag1.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            pMySqlDiag1.WaitForExit()
        End Using

        ChDir(Settings.CurrentDirectory)

    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click

        CheckForUpdates()

    End Sub

    Private Sub ClothingInventoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClothingInventoryToolStripMenuItem.Click

        ContentIAR.Activate()
        ContentIAR.ShowForm()
        ContentIAR.Select()
        ContentIAR.BringToFront()

    End Sub

    Private Sub ConsoleCOmmandsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConsoleCOmmandsToolStripMenuItem1.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Server_Commands"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub DiagnosticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiagnosticsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Click_Start)
            Return
        End If

        DoDiag()
        If Settings.DiagFailed = "True" Then
            TextPrint(My.Resources.HG_Failed)
        Else
            TextPrint(My.Resources.HG_Works)
        End If

    End Sub

    Private Sub JobEngineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JobEngineToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUuidListByName("*")
            If Not RPC_Region_Command(RegionUUID, "debug jobengine status") Then Return
        Next
    End Sub

    Private Sub JustOneRegionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUsersAllSimsToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If

        Dim HowManyAreOnline As Integer = 0
        Dim Message = InputBox(My.Resources.What_2_say_To_all)
        If Message.Length > 0 Then
            For Each RegionUUID As String In PropRegionClass.RegionUuids
                If PropRegionClass.AvatarCount(RegionUUID) > 0 Then
                    HowManyAreOnline += 1
                    SendMessage(RegionUUID, Message)
                End If
            Next
            If HowManyAreOnline = 0 Then
                TextPrint(My.Resources.Nobody_Online)
            Else
                TextPrint(My.Resources.Message_sent_word & ":" & CStr(HowManyAreOnline) & " regions")
            End If
        End If

    End Sub

    ''' <summary>The main startup - done this way so languages can reload the entire form</summary>
    Private Sub JustQuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JustQuitToolStripMenuItem.Click

        TextPrint("Zzzz...")
        Thread.Sleep(100)
        End

    End Sub

    Private Sub ThreadpoolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThreadpoolsToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUuidListByName("*")
            If Not RPC_Region_Command(RegionUUID, "show threads") Then Return
        Next
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = PropDomain & "/Outworldz_Installer/PortForwarding.htm"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub TroubleshootingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TroubleshootingToolStripMenuItem.Click

        HelpManual("TroubleShooting")

    End Sub

    Private Sub ViewIcecastWebPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewIcecastWebPageToolStripMenuItem.Click
        If PropOpensimIsRunning() And Settings.SCEnable Then
            Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CStr(Settings.SCPortBase)
            TextPrint(My.Resources.Icecast_Desc & webAddress & "/stream")
            Try
                Process.Start(webAddress)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        ElseIf Settings.SCEnable = False Then
            TextPrint(My.Resources.Shoutcast_Disabled)
        Else
            TextPrint(My.Resources.Not_Running)
        End If
    End Sub

    Private Sub ViewRegionMapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewRegionMapToolStripMenuItem.Click

        ShowRegionMap()

    End Sub

    Private Sub XengineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XengineToolStripMenuItem.Click
        For Each RegionUUID As String In PropRegionClass.RegionUuidListByName("*")
            If Not RPC_Region_Command(RegionUUID, "xengine status") Then Return
        Next
    End Sub

#End Region

#Region "Logging"

    Private Sub Debug_Click(sender As Object, e As EventArgs) Handles Debug.Click

        Settings.LogLevel = "DEBUG"
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub ErrorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ErrorToolStripMenuItem.Click

        Settings.LogLevel = "ERROR"
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub Fatal1_Click(sender As Object, e As EventArgs) Handles Fatal1.Click

        Settings.LogLevel = "FATAL"
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub Info_Click(sender As Object, e As EventArgs) Handles Info.Click

        Settings.LogLevel = "INFO"
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub Off1_Click(sender As Object, e As EventArgs) Handles Off1.Click

        Settings.LogLevel = "OFF"
        SendMsg(Settings.LogLevel)

    End Sub

    Private Sub Warn_Click(sender As Object, e As EventArgs) Handles Warn.Click

        Settings.LogLevel = "WARN"
        SendMsg(Settings.LogLevel)

    End Sub

#End Region

#Region "IAR OAR"

    Public Sub RestartAllRegions()

        PropOpensimIsRunning() = True
        If Not StartMySQL() Then Return
        If Not StartRobust() Then Return
        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning
        TimerBusy = 0

        For Each RegionUUID As String In PropRegionClass.RegionUuids

            If Not PropRegionClass.RegionEnabled(RegionUUID) Then
                Continue For
            End If

            Dim GroupName = PropRegionClass.GroupName(RegionUUID)
            Dim Status = PropRegionClass.Status(RegionUUID)

            If PropRegionClass.RegionEnabled(RegionUUID) And AvatarsIsInGroup(GroupName) Then
                TextPrint("People are in " & GroupName)
                Continue For
            End If

            If Not PropAborting And (Status = RegionMaker.SIMSTATUSENUM.Booting Or Status = RegionMaker.SIMSTATUSENUM.Booted) Then
                Dim hwnd = GetHwnd(GroupName)
                ShowDOSWindow(hwnd, MaybeShowWindow())
                ShutDown(RegionUUID)
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown
                PropUpdateView = True ' make form refresh
            Else
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
            End If

        Next

    End Sub

    Private Sub FindIARsOnOutworldzToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindIARsOnOutworldzToolStripMenuItem.Click

        Dim webAddress As String = PropDomain & "/outworldz_installer/IAR"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub LoadFreeDreamGridOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IslandToolStripMenuItem.Click

        Try
            ContentOAR.Activate()
            ContentOAR.ShowForm()
            ContentOAR.Select()
            ContentOAR.BringToFront()
        Catch
        End Try

    End Sub

    Private Sub LoadIarClick(sender As Object, e As EventArgs) ' event handler

        Dim File As String = IO.Path.Combine(BackupPath, CStr(sender.Text)) 'make a real URL
        If LoadIARContent(File) Then
            TextPrint(My.Resources.Opensimulator_is_loading & " " & CStr(sender.Text) & ".  " & Global.Outworldz.My.Resources.Take_time)
        End If

    End Sub

    Private Sub LoadInventoryIARToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem1.Click

        LoadIAR()

    End Sub

    Private Sub LoadLocalIAROAR()

        ''' <summary>Loads OAR and IAR to the menu</summary>
        ''' <remarks>Handles both the IAR/OAR and Autobackup folders</remarks>

        LoadLocalOARToolStripMenuItem.DropDownItems.Clear()
        Dim MaxFileNum As Integer = 25
        Dim counter = MaxFileNum
        Dim Filename = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\OAR\")
        Dim OARs As Array = Nothing
        Try
            OARs = Directory.GetFiles(Filename, "*.OAR", SearchOption.TopDirectoryOnly)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Try
            For Each OAR As String In OARs
                counter -= 1

                If counter > 0 Then
                    Dim Name = Path.GetFileName(OAR)
                    Dim OarMenu As New ToolStripMenuItem With {
                    .Text = Name,
                    .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                    .DisplayStyle = ToolStripItemDisplayStyle.Text,
                    .Image = My.Resources.box_new
                }
                    AddHandler OarMenu.Click, New EventHandler(AddressOf LocalOarClick)
                    LoadLocalOARToolStripMenuItem.Visible = True
                    LoadLocalOARToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                End If

            Next
        Catch
        End Try

        Dim AutoOARs As Array = Nothing
        Try
            AutoOARs = Directory.GetFiles(BackupPath(), "*.OAR", SearchOption.TopDirectoryOnly)

            counter = MaxFileNum

            If AutoOARs IsNot Nothing Then
                For Each OAR As String In AutoOARs
                    counter -= 1
                    If counter > 0 Then
                        Dim Name = Path.GetFileName(OAR)
                        Dim OarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                        .DisplayStyle = ToolStripItemDisplayStyle.Text,
                        .Image = My.Resources.box_new
                    }
                        AddHandler OarMenu.Click, New EventHandler(AddressOf LoadOarClick)
                        LoadLocalOARToolStripMenuItem.Visible = True
                        LoadLocalOARToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                    End If
                Next
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Dim IARs As Array = Nothing
        ' now for the IARs
        Try
            Filename = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\IAR\")
            IARs = Directory.GetFiles(Filename, "*.IAR", SearchOption.TopDirectoryOnly)

            LoadLocalIARToolStripMenuItem.DropDownItems.Clear()
            counter = MaxFileNum
            For Each IAR As String In IARs
                counter -= 1
                If counter > 0 Then
                    Dim Name = Path.GetFileName(IAR)
                    Dim IarMenu As New ToolStripMenuItem With {
                        .Text = Name,
                        .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                        .DisplayStyle = ToolStripItemDisplayStyle.Text,
                        .Image = My.Resources.box_new
                    }
                    AddHandler IarMenu.Click, New EventHandler(AddressOf LocalIarClick)
                    LoadLocalIARToolStripMenuItem.Visible = True
                    LoadLocalIARToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})

                End If
            Next
        Catch
        End Try

        Dim AutoIARs As Array = Nothing
        Try
            AutoIARs = Directory.GetFiles(BackupPath, "*.IAR", SearchOption.TopDirectoryOnly)

            If AutoIARs IsNot Nothing Then
                counter = MaxFileNum
                For Each IAR As String In AutoIARs
                    counter -= 1
                    If counter > 0 Then
                        Dim Name = Path.GetFileName(IAR)
                        Dim IarMenu As New ToolStripMenuItem With {
                            .Text = Name,
                            .ToolTipText = Global.Outworldz.My.Resources.Click_to_load,
                            .DisplayStyle = ToolStripItemDisplayStyle.Text
                        }
                        AddHandler IarMenu.Click, New EventHandler(AddressOf LoadIarClick)
                        LoadLocalIARToolStripMenuItem.Visible = True
                        LoadLocalIARToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                    End If

                Next
            End If
        Catch
        End Try

    End Sub

    Private Sub LoadOarClick(sender As Object, e As EventArgs) ' event handler

        Dim File As String = IO.Path.Combine(BackupPath, CStr(sender.Text)) 'make a real URL
        If LoadOARContent(File) Then
            TextPrint(My.Resources.Opensimulator_is_loading & " " & CStr(sender.Text) & ".  " & Global.Outworldz.My.Resources.Take_time)
        End If

    End Sub

    Private Sub LoadOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOARToolStripMenuItem.Click

        LoadOar("")

    End Sub

    Private Sub LocalIarClick(sender As Object, e As EventArgs) ''''

        Dim thing As String = sender.text.ToString

        Dim File As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/IAR/" & CStr(thing)) 'make a real URL
        If LoadIARContent(File) Then
            TextPrint(My.Resources.Opensimulator_is_loading & CStr(thing))
        End If

    End Sub

    Private Sub LocalOarClick(sender As Object, e As EventArgs)

        Dim thing As String = sender.text.ToString
        Dim File As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/OAR/" & thing) 'make a real URL
        If LoadOARContent(File) Then
            TextPrint(My.Resources.Opensimulator_is_loading & CStr(sender.Text))
        End If

    End Sub

    Private Sub RestartAllRegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartAllRegionsToolStripMenuItem.Click

        RestartAllRegions()

    End Sub

    Private Sub SaveAllRunningRegiondsAsOARSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAllRunningRegiondsAsOARSToolStripMenuItem.Click

        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If

        Dim WebThread = New Thread(AddressOf Backupper)
        WebThread.SetApartmentState(ApartmentState.STA)
        WebThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
        WebThread.Start()

    End Sub

    Private Sub SaveInventoryIARToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveInventoryIARToolStripMenuItem1.Click

        SaveIARTask()

    End Sub

    Private Sub SaveRegionOARToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveRegionOARToolStripMenuItem1.Click

        SaveOar("")

    End Sub

    Private Sub SearchForOarsAtOutworldzToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchForOarsAtOutworldzToolStripMenuItem.Click

        Dim webAddress As String = PropDomain & "/outworldz_installer/OAR"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region "Toolstrip"

    Private Sub FloatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FloatToolStripMenuItem.Click

        Me.TopMost = False
        Settings.KeepOnTopMain = False
        KeepOnTopToolStripMenuItem.Image = My.Resources.table
        OnTopToolStripMenuItem.Checked = False
        FloatToolStripMenuItem.Checked = True

    End Sub

    Private Sub LanguageToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LanguageToolStripMenuItem1.Click

#Disable Warning CA2000
        Dim Lang As New Language
#Enable Warning CA2000
        Lang.Activate()
        Lang.Visible = True
        Lang.Select()
        Lang.BringToFront()

    End Sub

    Private Sub OnTopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnTopToolStripMenuItem.Click

        Me.TopMost = True
        Settings.KeepOnTopMain = True
        KeepOnTopToolStripMenuItem.Image = My.Resources.tables
        OnTopToolStripMenuItem.Checked = True
        FloatToolStripMenuItem.Checked = False

    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchForObjectsMenuItem.Click

        Dim webAddress As String = "https://hyperica.com/Search/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region "Testing"

    Private Sub DebugToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem1.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim FormInput As New FormDebug
#Enable Warning CA2000 ' Dispose objects before losing scope
        FormInput.Activate()
        FormInput.Visible = True
        FormInput.Select()
        FormInput.BringToFront()

    End Sub

    Private Sub DeleteServiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteServiceToolStripMenuItem.Click

        StopApache(True)
        Dim win = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "sc.exe")
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .WindowStyle = ProcessWindowStyle.Hidden,
            .FileName = win,
            .Arguments = "delete ApacheHTTPServer"
        }
        Using p As New Process
            p.StartInfo = pi
            Try
                p.Start()
                p.WaitForExit()
                ApacheIcon(False)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End Using

    End Sub

    Private Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click
        StartApache()
    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        StopApache(True)
    End Sub

#End Region

End Class
