#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Public Class FormSettings

#Region "Declarations"

#Disable Warning CA2213

    Private Backups As New FormAutoBackups
    Dim Banlist As New FormBanList
    Private Bird As New FormBird
    Dim FormApache As New FormApache
    Dim FormCache As New FormCaches
    Dim FormDatabase As New FormDatabase
    Dim FormDiva As New FormDiva
    Dim FormDNSName As New FormDNSName
    Dim FormJoomla As New FormJoomla
    Dim FormPermissions As New FormPermissions
    Dim FormPhysics As New FormPhysics
    Dim FormPorts As New FormPorts
    Dim FormPublicity As New FormPublicity
    Dim FormRegions As New FormRegions
    Dim FormRestart As New FormRestart
    Dim FormServerType As New FormServerType
    Dim Gloebits As New FormGloebits
    Dim Icecast As New FormIcecast
    Dim Logging As New FormLogging
    Dim Maps As New FormMaps
    Dim Scripts As New FormScripts
    Dim Tide As New FormTide
    Dim Tos As New TosForm
    Dim Voice As New FormVoice
#Enable Warning CA2213

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Private Methods"

    Private Sub Advanced_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ApacheButton.Text = Global.Outworldz.My.Resources.Resources.Apache_Webserver
        BackupButton1.Text = Global.Outworldz.My.Resources.Resources.Backup_Settings_word
        BanListButton.Text = Global.Outworldz.My.Resources.Resources.Ban_List_word
        Birds.Text = Global.Outworldz.My.Resources.Resources.Bird_Settings_word
        Button1.Text = Global.Outworldz.My.Resources.Resources.Server_Type_word
        Button2.Text = Global.Outworldz.My.Resources.Resources.Permissions_word
        Button3.Text = Global.Outworldz.My.Resources.Resources.Restart_Settings_word
        Button4.Text = Global.Outworldz.My.Resources.Resources.Publicity_Word
        CacheButton1.Text = Global.Outworldz.My.Resources.Resources.Caches_word
        DNSButton.Text = Global.Outworldz.My.Resources.Resources.Hypergrid
        DatabaseButton2.Text = Global.Outworldz.My.Resources.Resources.Database_Setup_word
        DivaButton1.Text = Global.Outworldz.My.Resources.Resources.Web
        GloebitsButton.Text = Global.Outworldz.My.Resources.Resources.Currency_word
        LoggingButton.Text = Global.Outworldz.My.Resources.Resources.Logging_word
        MapsButton.Text = Global.Outworldz.My.Resources.Resources.Maps_word
        PhysicsButton1.Text = Global.Outworldz.My.Resources.Resources.Physics_word
        PortsButton1.Text = Global.Outworldz.My.Resources.Resources.Network_Ports
        RegionsButton1.Text = Global.Outworldz.My.Resources.Resources.Regions_word
        ScriptButton.Text = Global.Outworldz.My.Resources.Resources.Scripts_word
        Shoutcast.Text = Global.Outworldz.My.Resources.Resources.Icecast_word
        TOSButton.Text = Global.Outworldz.My.Resources.Resources.Terms_of_Service
        TideButton.Text = Global.Outworldz.My.Resources.Resources.Tides_word
        ToolTip1.SetToolTip(ApacheButton, Global.Outworldz.My.Resources.Resources.ApacheWebServer)
        ToolTip1.SetToolTip(BackupButton1, Global.Outworldz.My.Resources.Resources.Backup_Schedule)
        ToolTip1.SetToolTip(BanListButton, Global.Outworldz.My.Resources.Resources.BanList_string)
        ToolTip1.SetToolTip(Birds, Global.Outworldz.My.Resources.Resources.Click_Birds)
        ToolTip1.SetToolTip(Button1, Global.Outworldz.My.Resources.Resources.Click_Server)
        ToolTip1.SetToolTip(Button2, Global.Outworldz.My.Resources.Resources.Click_for_God_Mode)
        ToolTip1.SetToolTip(Button3, Global.Outworldz.My.Resources.Resources.Click_Restart)
        ToolTip1.SetToolTip(Button4, Global.Outworldz.My.Resources.Resources.Click_Publicity)
        ToolTip1.SetToolTip(Button5, Global.Outworldz.My.Resources.Resources.Click_Setup)
        ToolTip1.SetToolTip(CacheButton1, Global.Outworldz.My.Resources.Resources.Click_Caches)
        ToolTip1.SetToolTip(DNSButton, Global.Outworldz.My.Resources.Resources.Click_HG)
        ToolTip1.SetToolTip(DatabaseButton2, Global.Outworldz.My.Resources.Resources.Click_Database)
        ToolTip1.SetToolTip(DivaButton1, Global.Outworldz.My.Resources.Resources.Click_Web)
        ToolTip1.SetToolTip(GloebitsButton, Global.Outworldz.My.Resources.Resources.Click_Currency)
        ToolTip1.SetToolTip(LoggingButton, Global.Outworldz.My.Resources.Resources.Log_Level)
        ToolTip1.SetToolTip(MapsButton, Global.Outworldz.My.Resources.Resources.Click_Maps)
        ToolTip1.SetToolTip(PhysicsButton1, Global.Outworldz.My.Resources.Resources.Click_Physics)
        ToolTip1.SetToolTip(PortsButton1, Global.Outworldz.My.Resources.Resources.Click_Ports)
        ToolTip1.SetToolTip(RegionsButton1, Global.Outworldz.My.Resources.Resources.Click_Regions)
        ToolTip1.SetToolTip(ScriptButton, Global.Outworldz.My.Resources.Resources.Click_to_View_this_word)
        ToolTip1.SetToolTip(Shoutcast, Global.Outworldz.My.Resources.Resources.Click_Icecast)
        ToolTip1.SetToolTip(TOSButton, Global.Outworldz.My.Resources.Resources.Setup_TOS)
        ToolTip1.SetToolTip(TideButton, Global.Outworldz.My.Resources.Resources.Click_Tides)
        ToolTip1.SetToolTip(VoiceButton1, Global.Outworldz.My.Resources.Resources.Click_Voice)
        VoiceButton1.Text = Global.Outworldz.My.Resources.Resources.Vivox_Voice_word

        SetScreen()

        Me.Visible = True
        Me.ToolTip1.SetToolTip(Me.TOSButton, Global.Outworldz.My.Resources.Setup_TOS)

    End Sub

#End Region

#Region "Help"

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

        Dim webAddress As String = FormSetup.PropDomain + "/Outworldz_installer/technical.htm#Regions"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)

        End Try

    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>Dispose!</summary>
    ''' <remarks></remarks>

#End Region

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Visible = False

        Backups.Dispose()
        Bird.Dispose()
        FormApache.Dispose()
        FormCache.Dispose()
        FormDatabase.Dispose()
        FormDiva.Dispose()
        FormDNSName.Dispose()
        FormPermissions.Dispose()
        FormPhysics.Dispose()
        FormPorts.Dispose()
        FormPublicity.Dispose()
        FormRegions.Dispose()
        FormRestart.Dispose()
        FormServerType.Dispose()
        Gloebits.Dispose()
        Icecast.Dispose()
        Logging.Dispose()
        Maps.Dispose()
        Scripts.Dispose()
        Tide.Dispose()
        Tos.Dispose()
        Voice.Dispose()
        Banlist.Dispose()
        FormJoomla.Dispose()

    End Sub

#Region "Clicks"

    Private Sub ApacheButton_Click(sender As Object, e As EventArgs) Handles ApacheButton.Click

        FormApache.Close()
        FormApache.Dispose()
        FormApache = New FormApache
        FormApache.Activate()
        FormApache.Visible = True
        FormApache.Select()
        FormApache.BringToFront()

    End Sub

    Private Sub BackupButton1_Click(sender As Object, e As EventArgs) Handles BackupButton1.Click

        Backups.Close()
        Backups.Dispose()
        Backups = New FormAutoBackups
        Backups.Activate()
        Backups.Visible = True
        Backups.Select()
        Backups.BringToFront()

    End Sub

    Private Sub BanListButton_Click(sender As Object, e As EventArgs) Handles BanListButton.Click

        If Settings.ServerType = "Robust" Then
            Banlist.Close()
            Banlist.Dispose()
            Banlist = New FormBanList

            Banlist.Activate()
            Banlist.Visible = True
            Banlist.Select()
            Banlist.BringToFront()
        Else
            Banlist.Close()
            Banlist.Dispose()
        End If

    End Sub

    Private Sub Birds_Click(sender As Object, e As EventArgs) Handles Birds.Click

        Bird.Close()
        Bird.Dispose()
        Bird = New FormBird
        Bird.Activate()
        Bird.Visible = True
        Bird.Select()
        Bird.BringToFront()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles GloebitsButton.Click

        Gloebits.Close()
        Gloebits.Dispose()
        Gloebits = New FormGloebits
        Gloebits.Activate()
        Gloebits.Visible = True
        Gloebits.Select()
        Gloebits.BringToFront()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles PortsButton1.Click

        FormPorts.Close()
        FormPorts.Dispose()
        FormPorts = New FormPorts
        FormPorts.Activate()
        FormPorts.Visible = True
        FormPorts.Select()
        FormPorts.BringToFront()

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click

        FormServerType.Close()
        FormServerType.Dispose()
        FormServerType = New FormServerType
        FormCaches.Activate()
        FormServerType.Visible = True
        FormServerType.Select()
        FormServerType.BringToFront()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        FormPermissions.Close()
        FormPermissions.Dispose()
        FormPermissions = New FormPermissions
        FormPermissions.Activate()
        FormPermissions.Visible = True
        FormPermissions.Select()
        FormPermissions.BringToFront()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        FormRestart.Close()
        FormRestart.Dispose()
        FormRestart = New FormRestart
        FormRestart.Activate()
        FormRestart.Visible = True
        FormRestart.Select()
        FormRestart.BringToFront()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        FormPublicity.Close()
        FormPublicity.Dispose()
        FormPublicity = New FormPublicity
        FormPublicity.Activate()
        FormPublicity.Visible = True
        FormPublicity.Select()
        FormPublicity.BringToFront()

    End Sub

    Private Sub CacheButton1_Click(sender As Object, e As EventArgs) Handles CacheButton1.Click

        FormCache.Close()
        FormCache.Dispose()
        FormCache = New FormCaches
        FormCache.Activate()
        FormCache.Visible = True
        FormCache.Select()
        FormCache.BringToFront()

    End Sub

    Private Sub DatabaseButton2_Click(sender As Object, e As EventArgs) Handles DatabaseButton2.Click

        FormDatabase.Close()
        FormDatabase.Dispose()
        FormDatabase = New FormDatabase
        FormDatabase.Activate()
        FormDatabase.Visible = True
        FormDatabase.Select()
        FormDatabase.BringToFront()

    End Sub

    Private Sub DivaButton1_Click(sender As Object, e As EventArgs) Handles DivaButton1.Click

        FormDiva.Close()
        FormDiva.Dispose()
        FormDiva = New FormDiva
        FormDiva.Activate()
        FormDiva.Visible = True
        FormDiva.Select()
        FormDiva.BringToFront()

    End Sub

    Private Sub DNSButton_Click(sender As Object, e As EventArgs) Handles DNSButton.Click

        FormDNSName.Close()
        FormDNSName.Dispose()
        FormDNSName = New FormDNSName
        FormDNSName.Activate()
        FormDNSName.Visible = True
        FormDNSName.Select()
        FormDNSName.BringToFront()

    End Sub

    Private Sub LoggingButton_Click(sender As Object, e As EventArgs) Handles LoggingButton.Click

        Logging.Close()
        Logging.Dispose()
        Logging = New FormLogging With {
            .Visible = True
        }
        Logging.Activate()
        Logging.Select()
        Logging.BringToFront()
    End Sub

    Private Sub MapsButton_Click(sender As Object, e As EventArgs) Handles MapsButton.Click

        Maps.Close()
        Maps.Dispose()
        Maps = New FormMaps
        Maps.Activate()
        Maps.Select()
        Maps.Visible = True
        Maps.BringToFront()

    End Sub

    Private Sub PhysicsButton1_Click(sender As Object, e As EventArgs) Handles PhysicsButton1.Click

        FormPhysics.Close()
        FormPhysics.Dispose()
        FormPhysics = New FormPhysics
        FormPhysics.Activate()
        FormPhysics.Visible = True
        FormPhysics.Select()
        FormPhysics.BringToFront()

    End Sub

    Private Sub RegionsButton1_Click(sender As Object, e As EventArgs) Handles RegionsButton1.Click

        FormRegions.Close()
        FormRegions.Dispose()
        FormRegions = New FormRegions
        FormRegions.Activate()
        FormRegions.Visible = True
        FormRegions.Select()
        FormRegions.BringToFront()

    End Sub

    Private Sub ScriptButton_Click(sender As Object, e As EventArgs) Handles ScriptButton.Click

        Scripts.Close()
        Scripts.Dispose()
        Scripts = New FormScripts
        Scripts.Activate()
        Scripts.Visible = True
        Scripts.Select()
        Scripts.BringToFront()

    End Sub

    Private Sub Shoutcast_Click(sender As Object, e As EventArgs) Handles Shoutcast.Click

        Icecast.Close()
        Icecast.Dispose()
        Icecast = New FormIcecast With {
            .Visible = True
        }
        Icecast.Activate()
        Icecast.Select()
        Icecast.BringToFront()

    End Sub

    Private Sub TideButton_Click(sender As Object, e As EventArgs) Handles TideButton.Click

        Tide.Close()
        Tide.Dispose()
        Tide = New FormTide
        Tide.Activate()
        Tide.Visible = True
        Tide.Select()
        Tide.BringToFront()

    End Sub

    Private Sub TOSButton_Click(sender As Object, e As EventArgs) Handles TOSButton.Click

        Tos.Close()
        Tos.Dispose()
        Tos = New TosForm
        Tos.Activate()
        Tos.Visible = True
        Tos.Select()
        Tos.BringToFront()

    End Sub

    Private Sub VoiceButton1_Click(sender As Object, e As EventArgs) Handles VoiceButton1.Click

        Voice.Close()
        Voice.Dispose()
        Voice = New FormVoice
        Voice.Activate()
        Voice.Visible = True
        Voice.Select()
        Voice.BringToFront()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        FormJoomla.Close()
        FormJoomla.Dispose()
        FormJoomla = New FormJoomla
        FormJoomla.Activate()
        FormJoomla.Visible = True
        FormJoomla.Select()
        FormJoomla.BringToFront()

    End Sub

#End Region

End Class
