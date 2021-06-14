#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Threading
Imports IniParser.Model

Module GlobalSettings

#Region "Const"

    Public Const _Domain As String = "http://outworldz.com"
    Public Const _MyVersion As String = "4.41"
    Public Const _SimVersion As String = "#1610c3f741 mantis 8862"
    Public Const jOpensimRev As String = "Joomla_3.9.23-Stable-Full_Package"
    Public Const jRev As String = "3.9.23"
    Public Const MySqlRev = "5.6.5"

#End Region

#Region "Global Strings"

    Public Const FreeDiskSpaceWarn As Long = 100000000 ' 100 M
    Public Const Hyperica As String = "Hyperica"
    Public Const JOpensim As String = "JOpensim"
    Public Const MetroServer As String = "Metro"
    Public Const OsgridServer As String = "OsGrid"
    Public Const RegionServerName As String = "Region"
    Public Const RobustServerName As String = "Robust"

#End Region

#Region "Globals"

    Public Bench As New Benchmark()
    Public gEstateName As String = ""
    Public gEstateOwner As String = ""
    Public MapX As Integer = 100
    Public MapY As Integer = 100

    Dim _Data As IniParser.Model.IniData
    Private _IsRunning As Boolean
    Private _mySetting As MySettings
    Private _PropAborting As Boolean
    Private _regionClass As RegionMaker
    Private _SelectedBox As String = ""
    Private _SkipSetup As Boolean = True
    Private _UpdateView As Boolean = True
    Private _XYINI As String ' global XY INI

#End Region

#Region "Properties"

    Public ReadOnly Property GitVersion As String
        ' output of  git rev-parse --short HEAD   from Perl
        Get
            Dim line As String = "None"
            Dim fname = IO.Path.Combine(Settings.CurrentDirectory, "GitVersion")
            If System.IO.File.Exists(fname) Then
                Using reader As StreamReader = System.IO.File.OpenText(fname)
                    'now loop through each line
                    While reader.Peek <> -1
                        line = reader.ReadLine()
                    End While
                End Using
            End If
            Return line
        End Get

    End Property

    Public Property PropAborting() As Boolean
        Get
            Return _PropAborting
        End Get
        Set(ByVal Value As Boolean)
            _PropAborting = Value
        End Set
    End Property

    Public ReadOnly Property PropDomain As String
        Get
            Return _Domain
        End Get

    End Property

    Public ReadOnly Property PropMyVersion As String
        Get
            Return _MyVersion
        End Get
    End Property

    Public Property PropOpensimIsRunning() As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            _IsRunning = Value
        End Set
    End Property

    Public Property PropRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property PropSelectedBox As String
        Get
            Return _SelectedBox
        End Get
        Set(value As String)
            _SelectedBox = value
        End Set
    End Property

    Public ReadOnly Property PropSimVersion As String
        Get
            Return _SimVersion
        End Get
    End Property

    Public Property PropUpdateView() As Boolean
        Get
            Return _UpdateView
        End Get
        Set(ByVal Value As Boolean)
            'If Debugger.IsAttached Then Diagnostics.Debug.Print("View refresh")
            _UpdateView = Value
        End Set
    End Property

    Public Property Settings As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Property SkipSetup() As Boolean
        Get
            Return _SkipSetup
        End Get
        Set(ByVal Value As Boolean)
            _SkipSetup = Value
        End Set
    End Property

    Public Property XYData As IniData
        Get
            Return _Data
        End Get
        Set(value As IniData)
            _Data = value
        End Set
    End Property

    Public Property XYINI As String
        Get
            Return _XYINI
        End Get
        Set(value As String)
            _XYINI = value
        End Set
    End Property

#End Region

#Region "Subs"

    Public Sub Sleep(value As Integer)
        ''' <summary>Sleep(ms)</summary>
        ''' <param name="value">millseconds</param>
        ' value is in milliseconds, but we do it in 25 passes so we can doevents() to free up console
        Dim slices = 25
        Dim sleeptime As Double = value / slices

        While sleeptime > 0
            Application.DoEvents()
            Thread.Sleep(CInt(1000 / slices))
            sleeptime -= 1
        End While
    End Sub

    Public Sub TextPrint(Value As String)

        Log(My.Resources.Info_word, Value)
        FormSetup.TextBox1.Text += Value & vbCrLf
        If FormSetup.TextBox1.Text.Length > FormSetup.TextBox1.MaxLength - 1000 Then
            FormSetup.TextBox1.Text = Mid(FormSetup.TextBox1.Text, 1000)
        End If

    End Sub

#End Region

#Region "Functions"

    Public Function GetStateString(state As Integer) As String

        Dim statestring As String
        Select Case state
            Case 0
                statestring = "Stopped"
            Case 1
                statestring = "Booting"
            Case 2
                statestring = "Booted"
            Case 3
                statestring = "RecyclingUp"
            Case 4
                statestring = "RecyclingDown"
            Case 5
                statestring = "ShuttingDown"
            Case 6
                statestring = "RestartPending"
            Case 7
                statestring = "RetartingNow"
            Case 8
                statestring = "Resume"
            Case 9
                statestring = "Suspended"
            Case 10
                statestring = "Error"
            Case 11
                statestring = "RestartStage2"
            Case 12
                statestring = "ShuttingDownForGood"
            Case 13
                statestring = "NoLogin"
            Case 14
                statestring = "No Error on Exit"
            Case Else
                statestring = "**** Unknown state ****"
        End Select

        Return statestring

    End Function

    Public Sub PokeRegionTimer(UUID As String)

        PropRegionClass.Timer(UUID) = Date.Now ' wait another interval

    End Sub

    Public Function RobustName() As String

        Return "Robust " & Settings.PublicIP

    End Function

    Public Function SafeFolderName() As String

        Dim destinationpath As String = IO.Path.Combine(Settings.CurrentDirectory(), "tmp/" & CStr(DateTime.Now.Ticks))
        If Not System.IO.Directory.Exists(destinationpath) Then
            System.IO.Directory.CreateDirectory(destinationpath)
        End If

        Return destinationpath

    End Function

    Public Function VarChooser(RegionName As String, Optional modal As Boolean = True, Optional Map As Boolean = True) As String

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim size = PropRegionClass.SizeX(RegionUUID)

#Disable Warning CA2000
        Dim VarForm As New FormDisplacement ' form for choosing a region in  a var
#Enable Warning CA2000
        Dim span As Integer = CInt(Math.Ceiling(size / 256))
        ' Show Dialog as a modal dialog
        VarForm.Init(span, RegionUUID, Map)

        If modal Then
            VarForm.ShowDialog()
            VarForm.Dispose()
        Else
            VarForm.Show()
        End If

        Return PropSelectedBox

    End Function

#End Region

End Module
