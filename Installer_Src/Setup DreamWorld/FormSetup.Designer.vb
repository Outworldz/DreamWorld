﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            If disposing AndAlso PropMyUPnpMap IsNot Nothing Then
                PropMyUPnpMap.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Form.set_Text(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolStripItem.set_Text(System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSetup))
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JustQuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsoleToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHideAllways = New System.Windows.Forms.ToolStripMenuItem()
        Me.LanguageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeepOnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FloatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHyperGridAddressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpStartingUpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PDFManualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoopBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnIARSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnOARsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TroubleshootingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TechnicalInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsoleCOmmandsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.CommonConsoleCommandsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowUserDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendAlertToAllUsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllUsersAllSimsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JustOneRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Debug = New System.Windows.Forms.ToolStripMenuItem()
        Me.Info = New System.Windows.Forms.ToolStripMenuItem()
        Me.Warn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Fatal1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Off1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartOneRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartTheInstanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartAllRegionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsStopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsStartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsSuspendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsResumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ThreadpoolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XengineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JobEngineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimulatorStatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewWebUI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewRegionMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewIcecastWebPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DiagnosticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeePortsInUseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.CHeckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.DebugToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevisionHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MnuContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.IslandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClothingInventoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadLocalOARSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadRegionOARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveRegionOARToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForOarsAtOutworldzToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadLocalOARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadIARsToolMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadInventoryIARToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveInventoryIARToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadLocalIARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindIARsOnOutworldzToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForObjectsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchForGridsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CheckAndRepairDatbaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupRestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreDatabaseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupCriticalFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.RestartMysqlIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartMysqlItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteServiceToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectToConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartRobustIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartRobustItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartApacheIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteServiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectToWebPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartIcecastIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartIceCastItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AvatarLabel = New System.Windows.Forms.Label()
        Me.PercentCPU = New System.Windows.Forms.Label()
        Me.PercentRAM = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.BusyButton = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.DiskSize = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox1 = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JustQuitToolStripMenuItem, Me.mnuExit})
        Me.FileToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.exit_icon
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(57, 24)
        Me.FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.File_word
        '
        'JustQuitToolStripMenuItem
        '
        Me.JustQuitToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.JustQuitToolStripMenuItem.Name = "JustQuitToolStripMenuItem"
        Me.JustQuitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.JustQuitToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.JustQuitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Quit_Now_Word
        '
        'mnuExit
        '
        Me.mnuExit.Image = Global.Outworldz.My.Resources.Resources.exit_icon
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuExit.Size = New System.Drawing.Size(168, 22)
        Me.mnuExit.Text = Global.Outworldz.My.Resources.Resources.Exit__word
        '
        'mnuSettings
        '
        Me.mnuSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RegionsToolStripMenuItem, Me.ConsoleToolStripMenuItem1, Me.LanguageToolStripMenuItem1, Me.KeepOnTopToolStripMenuItem, Me.AdvancedSettingsToolStripMenuItem})
        Me.mnuSettings.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.mnuSettings.Name = "mnuSettings"
        Me.mnuSettings.Size = New System.Drawing.Size(69, 24)
        Me.mnuSettings.Text = Global.Outworldz.My.Resources.Resources.Setup_word
        '
        'RegionsToolStripMenuItem
        '
        Me.RegionsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
        Me.RegionsToolStripMenuItem.Name = "RegionsToolStripMenuItem"
        Me.RegionsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RegionsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.RegionsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Regions_word
        '
        'ConsoleToolStripMenuItem1
        '
        Me.ConsoleToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHide, Me.mnuShow, Me.mnuHideAllways})
        Me.ConsoleToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.window_add
        Me.ConsoleToolStripMenuItem1.Name = "ConsoleToolStripMenuItem1"
        Me.ConsoleToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
        Me.ConsoleToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Consoles_word
        Me.ConsoleToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Consoletext
        '
        'mnuHide
        '
        Me.mnuHide.Checked = True
        Me.mnuHide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuHide.Image = Global.Outworldz.My.Resources.Resources.navigate_down
        Me.mnuHide.Name = "mnuHide"
        Me.mnuHide.Size = New System.Drawing.Size(225, 22)
        Me.mnuHide.Text = Global.Outworldz.My.Resources.Resources.Hide
        '
        'mnuShow
        '
        Me.mnuShow.Image = Global.Outworldz.My.Resources.Resources.navigate_up
        Me.mnuShow.Name = "mnuShow"
        Me.mnuShow.Size = New System.Drawing.Size(225, 22)
        Me.mnuShow.Text = Global.Outworldz.My.Resources.Resources.Show_word
        '
        'mnuHideAllways
        '
        Me.mnuHideAllways.Image = Global.Outworldz.My.Resources.Resources.navigate_down2
        Me.mnuHideAllways.Name = "mnuHideAllways"
        Me.mnuHideAllways.Size = New System.Drawing.Size(225, 22)
        Me.mnuHideAllways.Text = Global.Outworldz.My.Resources.Resources.Hide_Allways_word
        '
        'LanguageToolStripMenuItem1
        '
        Me.LanguageToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.users1
        Me.LanguageToolStripMenuItem1.Name = "LanguageToolStripMenuItem1"
        Me.LanguageToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
        Me.LanguageToolStripMenuItem1.Text = "Language"
        '
        'KeepOnTopToolStripMenuItem
        '
        Me.KeepOnTopToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnTopToolStripMenuItem, Me.FloatToolStripMenuItem})
        Me.KeepOnTopToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.tables
        Me.KeepOnTopToolStripMenuItem.Name = "KeepOnTopToolStripMenuItem"
        Me.KeepOnTopToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.KeepOnTopToolStripMenuItem.Text = "Window"
        '
        'OnTopToolStripMenuItem
        '
        Me.OnTopToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.tables
        Me.OnTopToolStripMenuItem.Name = "OnTopToolStripMenuItem"
        Me.OnTopToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.OnTopToolStripMenuItem.Text = " On Top"
        '
        'FloatToolStripMenuItem
        '
        Me.FloatToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.table
        Me.FloatToolStripMenuItem.Name = "FloatToolStripMenuItem"
        Me.FloatToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.FloatToolStripMenuItem.Text = "Float"
        '
        'AdvancedSettingsToolStripMenuItem
        '
        Me.AdvancedSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.earth_network
        Me.AdvancedSettingsToolStripMenuItem.Name = "AdvancedSettingsToolStripMenuItem"
        Me.AdvancedSettingsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.AdvancedSettingsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.AdvancedSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Settings_word
        Me.AdvancedSettingsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.All_Global_Settings_word
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHyperGridAddressToolStripMenuItem, Me.ToolStripSeparator1, Me.HelpStartingUpToolStripMenuItem1, Me.HelpOnSettingsToolStripMenuItem, Me.LoopBackToolStripMenuItem, Me.ToolStripMenuItem1, Me.HelpOnIARSToolStripMenuItem, Me.HelpOnOARsToolStripMenuItem, Me.TroubleshootingToolStripMenuItem, Me.TechnicalInfoToolStripMenuItem, Me.ConsoleCOmmandsToolStripMenuItem1, Me.ToolStripSeparator7, Me.CommonConsoleCommandsToolStripMenuItem, Me.ToolStripSeparator2, Me.ViewLogsToolStripMenuItem, Me.SimulatorStatsToolStripMenuItem, Me.ViewWebUI, Me.ViewRegionMapToolStripMenuItem, Me.ViewIcecastWebPageToolStripMenuItem, Me.ToolStripSeparator4, Me.DiagnosticsToolStripMenuItem, Me.SeePortsInUseToolStripMenuItem, Me.ToolStripSeparator8, Me.CHeckForUpdatesToolStripMenuItem, Me.ToolStripSeparator5, Me.DebugToolStripMenuItem1, Me.RevisionHistoryToolStripMenuItem, Me.mnuAbout})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ShowHyperGridAddressToolStripMenuItem
        '
        Me.ShowHyperGridAddressToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ShowHyperGridAddressToolStripMenuItem.Name = "ShowHyperGridAddressToolStripMenuItem"
        Me.ShowHyperGridAddressToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ShowHyperGridAddressToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ShowHyperGridAddressToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_Grid_Address
        Me.ShowHyperGridAddressToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Grid_Address_text
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(281, 6)
        '
        'HelpStartingUpToolStripMenuItem1
        '
        Me.HelpStartingUpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.HelpStartingUpToolStripMenuItem1.Name = "HelpStartingUpToolStripMenuItem1"
        Me.HelpStartingUpToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D1), System.Windows.Forms.Keys)
        Me.HelpStartingUpToolStripMenuItem1.Size = New System.Drawing.Size(284, 22)
        Me.HelpStartingUpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Startup
        '
        'HelpOnSettingsToolStripMenuItem
        '
        Me.HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PDFManualToolStripMenuItem})
        Me.HelpOnSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.HelpOnSettingsToolStripMenuItem.Name = "HelpOnSettingsToolStripMenuItem"
        Me.HelpOnSettingsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.HelpOnSettingsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.HelpOnSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Manuals_word
        '
        'PDFManualToolStripMenuItem
        '
        Me.PDFManualToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.pdf
        Me.PDFManualToolStripMenuItem.Name = "PDFManualToolStripMenuItem"
        Me.PDFManualToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.PDFManualToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.PDF_Manual_word
        '
        'LoopBackToolStripMenuItem
        '
        Me.LoopBackToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.refresh
        Me.LoopBackToolStripMenuItem.Name = "LoopBackToolStripMenuItem"
        Me.LoopBackToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.LoopBackToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.LoopBackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_On_LoopBack_word
        Me.LoopBackToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Loopback_Text
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.document_connection
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(284, 22)
        Me.ToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Forward
        Me.ToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Forward_text
        '
        'HelpOnIARSToolStripMenuItem
        '
        Me.HelpOnIARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.HelpOnIARSToolStripMenuItem.Name = "HelpOnIARSToolStripMenuItem"
        Me.HelpOnIARSToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.HelpOnIARSToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.HelpOnIARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_On_IARS_word
        Me.HelpOnIARSToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_IARS_text
        '
        'HelpOnOARsToolStripMenuItem
        '
        Me.HelpOnOARsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.HelpOnOARsToolStripMenuItem.Name = "HelpOnOARsToolStripMenuItem"
        Me.HelpOnOARsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.HelpOnOARsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.HelpOnOARsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_OARS
        Me.HelpOnOARsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_OARS_text
        '
        'TroubleshootingToolStripMenuItem
        '
        Me.TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.TroubleshootingToolStripMenuItem.Name = "TroubleshootingToolStripMenuItem"
        Me.TroubleshootingToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TroubleshootingToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.TroubleshootingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Troubleshooting_word
        '
        'TechnicalInfoToolStripMenuItem
        '
        Me.TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.TechnicalInfoToolStripMenuItem.Name = "TechnicalInfoToolStripMenuItem"
        Me.TechnicalInfoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.TechnicalInfoToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.TechnicalInfoToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Technical
        Me.TechnicalInfoToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Technical_text
        '
        'ConsoleCOmmandsToolStripMenuItem1
        '
        Me.ConsoleCOmmandsToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.text_marked
        Me.ConsoleCOmmandsToolStripMenuItem1.Name = "ConsoleCOmmandsToolStripMenuItem1"
        Me.ConsoleCOmmandsToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.ConsoleCOmmandsToolStripMenuItem1.Size = New System.Drawing.Size(284, 22)
        Me.ConsoleCOmmandsToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Console
        Me.ConsoleCOmmandsToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Console_text
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(281, 6)
        '
        'CommonConsoleCommandsToolStripMenuItem
        '
        Me.CommonConsoleCommandsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UsersToolStripMenuItem, Me.SendAlertToAllUsersToolStripMenuItem, Me.DebugToolStripMenuItem, Me.RestartRegionToolStripMenuItem, Me.ScriptsToolStripMenuItem, Me.ShowStatusToolStripMenuItem})
        Me.CommonConsoleCommandsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.text_marked
        Me.CommonConsoleCommandsToolStripMenuItem.Name = "CommonConsoleCommandsToolStripMenuItem"
        Me.CommonConsoleCommandsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.CommonConsoleCommandsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.CommonConsoleCommandsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Issue_Commands
        '
        'UsersToolStripMenuItem
        '
        Me.UsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddUserToolStripMenuItem, Me.ChangePasswordToolStripMenuItem, Me.ShowUserDetailsToolStripMenuItem})
        Me.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem"
        Me.UsersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Users_word
        '
        'AddUserToolStripMenuItem
        '
        Me.AddUserToolStripMenuItem.Name = "AddUserToolStripMenuItem"
        Me.AddUserToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.AddUserToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Add_User_word
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ChangePasswordToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Change_Password_word
        '
        'ShowUserDetailsToolStripMenuItem
        '
        Me.ShowUserDetailsToolStripMenuItem.Name = "ShowUserDetailsToolStripMenuItem"
        Me.ShowUserDetailsToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ShowUserDetailsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_User_Details_word
        '
        'SendAlertToAllUsersToolStripMenuItem
        '
        Me.SendAlertToAllUsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllUsersAllSimsToolStripMenuItem, Me.JustOneRegionToolStripMenuItem})
        Me.SendAlertToAllUsersToolStripMenuItem.Name = "SendAlertToAllUsersToolStripMenuItem"
        Me.SendAlertToAllUsersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SendAlertToAllUsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Send_Alert_Message_word
        '
        'AllUsersAllSimsToolStripMenuItem
        '
        Me.AllUsersAllSimsToolStripMenuItem.Name = "AllUsersAllSimsToolStripMenuItem"
        Me.AllUsersAllSimsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.AllUsersAllSimsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.All_Users_All_Sims_word
        '
        'JustOneRegionToolStripMenuItem
        '
        Me.JustOneRegionToolStripMenuItem.Name = "JustOneRegionToolStripMenuItem"
        Me.JustOneRegionToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.JustOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Just_one_region_word
        '
        'DebugToolStripMenuItem
        '
        Me.DebugToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Debug, Me.Info, Me.Warn, Me.ErrorToolStripMenuItem, Me.Fatal1, Me.Off1})
        Me.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem"
        Me.DebugToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DebugToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Log_Level
        '
        'Debug
        '
        Me.Debug.Name = "Debug"
        Me.Debug.Size = New System.Drawing.Size(109, 22)
        Me.Debug.Text = Global.Outworldz.My.Resources.Resources.Debug_word
        '
        'Info
        '
        Me.Info.Name = "Info"
        Me.Info.Size = New System.Drawing.Size(109, 22)
        Me.Info.Text = Global.Outworldz.My.Resources.Resources.Info_word
        '
        'Warn
        '
        Me.Warn.Name = "Warn"
        Me.Warn.Size = New System.Drawing.Size(109, 22)
        Me.Warn.Text = Global.Outworldz.My.Resources.Resources.Warn_word
        '
        'ErrorToolStripMenuItem
        '
        Me.ErrorToolStripMenuItem.Name = "ErrorToolStripMenuItem"
        Me.ErrorToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.ErrorToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Error_word
        '
        'Fatal1
        '
        Me.Fatal1.Name = "Fatal1"
        Me.Fatal1.Size = New System.Drawing.Size(109, 22)
        Me.Fatal1.Text = Global.Outworldz.My.Resources.Resources.Fatal_word
        '
        'Off1
        '
        Me.Off1.Name = "Off1"
        Me.Off1.Size = New System.Drawing.Size(109, 22)
        Me.Off1.Text = Global.Outworldz.My.Resources.Resources.Off
        '
        'RestartRegionToolStripMenuItem
        '
        Me.RestartRegionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartOneRegionToolStripMenuItem, Me.RestartTheInstanceToolStripMenuItem, Me.RestartAllRegionsToolStripMenuItem})
        Me.RestartRegionToolStripMenuItem.Name = "RestartRegionToolStripMenuItem"
        Me.RestartRegionToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RestartRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_Region_word
        '
        'RestartOneRegionToolStripMenuItem
        '
        Me.RestartOneRegionToolStripMenuItem.Name = "RestartOneRegionToolStripMenuItem"
        Me.RestartOneRegionToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestartOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_one_region_word
        '
        'RestartTheInstanceToolStripMenuItem
        '
        Me.RestartTheInstanceToolStripMenuItem.Name = "RestartTheInstanceToolStripMenuItem"
        Me.RestartTheInstanceToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestartTheInstanceToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_one_instance_word
        '
        'RestartAllRegionsToolStripMenuItem
        '
        Me.RestartAllRegionsToolStripMenuItem.Name = "RestartAllRegionsToolStripMenuItem"
        Me.RestartAllRegionsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestartAllRegionsToolStripMenuItem.Text = "Restart All Regions"
        '
        'ScriptsToolStripMenuItem
        '
        Me.ScriptsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScriptsStopToolStripMenuItem, Me.ScriptsStartToolStripMenuItem, Me.ScriptsSuspendToolStripMenuItem, Me.ScriptsResumeToolStripMenuItem})
        Me.ScriptsToolStripMenuItem.Name = "ScriptsToolStripMenuItem"
        Me.ScriptsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ScriptsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_word
        '
        'ScriptsStopToolStripMenuItem
        '
        Me.ScriptsStopToolStripMenuItem.Name = "ScriptsStopToolStripMenuItem"
        Me.ScriptsStopToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsStopToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Stop_word
        '
        'ScriptsStartToolStripMenuItem
        '
        Me.ScriptsStartToolStripMenuItem.Name = "ScriptsStartToolStripMenuItem"
        Me.ScriptsStartToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsStartToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Start_word
        '
        'ScriptsSuspendToolStripMenuItem
        '
        Me.ScriptsSuspendToolStripMenuItem.Name = "ScriptsSuspendToolStripMenuItem"
        Me.ScriptsSuspendToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsSuspendToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Suspend_word
        '
        'ScriptsResumeToolStripMenuItem
        '
        Me.ScriptsResumeToolStripMenuItem.Name = "ScriptsResumeToolStripMenuItem"
        Me.ScriptsResumeToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsResumeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Resume_word
        '
        'ShowStatusToolStripMenuItem
        '
        Me.ShowStatusToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ThreadpoolsToolStripMenuItem, Me.XengineToolStripMenuItem, Me.JobEngineToolStripMenuItem})
        Me.ShowStatusToolStripMenuItem.Name = "ShowStatusToolStripMenuItem"
        Me.ShowStatusToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ShowStatusToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_Status_word
        '
        'ThreadpoolsToolStripMenuItem
        '
        Me.ThreadpoolsToolStripMenuItem.Name = "ThreadpoolsToolStripMenuItem"
        Me.ThreadpoolsToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.ThreadpoolsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Thread_pools_word
        '
        'XengineToolStripMenuItem
        '
        Me.XengineToolStripMenuItem.Name = "XengineToolStripMenuItem"
        Me.XengineToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.XengineToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
        '
        'JobEngineToolStripMenuItem
        '
        Me.JobEngineToolStripMenuItem.Name = "JobEngineToolStripMenuItem"
        Me.JobEngineToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.JobEngineToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.JobEngine_word
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(281, 6)
        '
        'ViewLogsToolStripMenuItem
        '
        Me.ViewLogsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ViewLogsToolStripMenuItem.Name = "ViewLogsToolStripMenuItem"
        Me.ViewLogsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.ViewLogsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewLogsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Logs
        '
        'SimulatorStatsToolStripMenuItem
        '
        Me.SimulatorStatsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.SimulatorStatsToolStripMenuItem.Name = "SimulatorStatsToolStripMenuItem"
        Me.SimulatorStatsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SimulatorStatsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.SimulatorStatsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Simulator_Stats
        Me.SimulatorStatsToolStripMenuItem.Visible = False
        '
        'ViewWebUI
        '
        Me.ViewWebUI.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ViewWebUI.Name = "ViewWebUI"
        Me.ViewWebUI.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.ViewWebUI.Size = New System.Drawing.Size(284, 22)
        Me.ViewWebUI.Text = Global.Outworldz.My.Resources.Resources.View_Web_Interface
        Me.ViewWebUI.ToolTipText = Global.Outworldz.My.Resources.Resources.View_Web_Interface_text
        '
        'ViewRegionMapToolStripMenuItem
        '
        Me.ViewRegionMapToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Good
        Me.ViewRegionMapToolStripMenuItem.Name = "ViewRegionMapToolStripMenuItem"
        Me.ViewRegionMapToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.ViewRegionMapToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewRegionMapToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Maps
        '
        'ViewIcecastWebPageToolStripMenuItem
        '
        Me.ViewIcecastWebPageToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.cube_green
        Me.ViewIcecastWebPageToolStripMenuItem.Name = "ViewIcecastWebPageToolStripMenuItem"
        Me.ViewIcecastWebPageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.ViewIcecastWebPageToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewIcecastWebPageToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Icecast
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(281, 6)
        '
        'DiagnosticsToolStripMenuItem
        '
        Me.DiagnosticsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
        Me.DiagnosticsToolStripMenuItem.Name = "DiagnosticsToolStripMenuItem"
        Me.DiagnosticsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.DiagnosticsToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.DiagnosticsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Network_Diagnostics
        Me.DiagnosticsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Network_Diagnostics_text
        '
        'SeePortsInUseToolStripMenuItem
        '
        Me.SeePortsInUseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.server_connection
        Me.SeePortsInUseToolStripMenuItem.Name = "SeePortsInUseToolStripMenuItem"
        Me.SeePortsInUseToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.SeePortsInUseToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.SeePortsInUseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.See_Ports_In_Use_word
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(281, 6)
        '
        'CHeckForUpdatesToolStripMenuItem
        '
        Me.CHeckForUpdatesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.cube_blue_add
        Me.CHeckForUpdatesToolStripMenuItem.Name = "CHeckForUpdatesToolStripMenuItem"
        Me.CHeckForUpdatesToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.U), System.Windows.Forms.Keys)
        Me.CHeckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.CHeckForUpdatesToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Check_for_Updates_word
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(281, 6)
        '
        'DebugToolStripMenuItem1
        '
        Me.DebugToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.DebugToolStripMenuItem1.Name = "DebugToolStripMenuItem1"
        Me.DebugToolStripMenuItem1.Size = New System.Drawing.Size(284, 22)
        Me.DebugToolStripMenuItem1.Text = "Debug"
        '
        'RevisionHistoryToolStripMenuItem
        '
        Me.RevisionHistoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.RevisionHistoryToolStripMenuItem.Name = "RevisionHistoryToolStripMenuItem"
        Me.RevisionHistoryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RevisionHistoryToolStripMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.RevisionHistoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Revision_History_word
        '
        'mnuAbout
        '
        Me.mnuAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.mnuAbout.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.mnuAbout.Size = New System.Drawing.Size(284, 22)
        Me.mnuAbout.Text = Global.Outworldz.My.Resources.Resources.About_word
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.mnuSettings, Me.MnuContent, Me.HelpToolStripMenuItem, Me.RestartMysqlIcon, Me.RestartRobustIcon, Me.RestartApacheIcon, Me.RestartIcecastIcon})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(597, 26)
        Me.MenuStrip1.TabIndex = 0
        '
        'MnuContent
        '
        Me.MnuContent.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IslandToolStripMenuItem, Me.ClothingInventoryToolStripMenuItem, Me.LoadLocalOARSToolStripMenuItem, Me.LoadIARsToolMenuItem, Me.MoreFreeIslandsandPartsContentToolStripMenuItem, Me.SearchForObjectsMenuItem, Me.SearchForGridsMenuItem, Me.ToolStripSeparator3, Me.CheckAndRepairDatbaseToolStripMenuItem, Me.BackupRestoreToolStripMenuItem, Me.BackupCriticalFilesToolStripMenuItem, Me.ToolStripSeparator6})
        Me.MnuContent.Image = Global.Outworldz.My.Resources.Resources.earth_network
        Me.MnuContent.Name = "MnuContent"
        Me.MnuContent.Size = New System.Drawing.Size(82, 24)
        Me.MnuContent.Text = Global.Outworldz.My.Resources.Resources.Content_word
        '
        'IslandToolStripMenuItem
        '
        Me.IslandToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.IslandToolStripMenuItem.Name = "IslandToolStripMenuItem"
        Me.IslandToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.IslandToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Free_DreamGrid_OARs_word
        '
        'ClothingInventoryToolStripMenuItem
        '
        Me.ClothingInventoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.ClothingInventoryToolStripMenuItem.Name = "ClothingInventoryToolStripMenuItem"
        Me.ClothingInventoryToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.ClothingInventoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Free_Avatar_Inventory_word
        Me.ClothingInventoryToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Load_Free_Avatar_Inventory_text
        '
        'LoadLocalOARSToolStripMenuItem
        '
        Me.LoadLocalOARSToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadRegionOARToolStripMenuItem, Me.SaveRegionOARToolStripMenuItem1, Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem, Me.SearchForOarsAtOutworldzToolStripMenuItem, Me.LoadLocalOARToolStripMenuItem})
        Me.LoadLocalOARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.LoadLocalOARSToolStripMenuItem.Name = "LoadLocalOARSToolStripMenuItem"
        Me.LoadLocalOARSToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.LoadLocalOARSToolStripMenuItem.Text = "Region OAR Load, Save and Backup"
        '
        'LoadRegionOARToolStripMenuItem
        '
        Me.LoadRegionOARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.data
        Me.LoadRegionOARToolStripMenuItem.Name = "LoadRegionOARToolStripMenuItem"
        Me.LoadRegionOARToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.LoadRegionOARToolStripMenuItem.Text = "Load Region OAR"
        '
        'SaveRegionOARToolStripMenuItem1
        '
        Me.SaveRegionOARToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.SaveRegionOARToolStripMenuItem1.Name = "SaveRegionOARToolStripMenuItem1"
        Me.SaveRegionOARToolStripMenuItem1.Size = New System.Drawing.Size(252, 22)
        Me.SaveRegionOARToolStripMenuItem1.Text = "Save Region OAR"
        '
        'SaveAllRunningRegiondsAsOARSToolStripMenuItem
        '
        Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem.Name = "SaveAllRunningRegiondsAsOARSToolStripMenuItem"
        Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.SaveAllRunningRegiondsAsOARSToolStripMenuItem.Text = "Save all Running Regions to OARs"
        '
        'SearchForOarsAtOutworldzToolStripMenuItem
        '
        Me.SearchForOarsAtOutworldzToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.earth_network
        Me.SearchForOarsAtOutworldzToolStripMenuItem.Name = "SearchForOarsAtOutworldzToolStripMenuItem"
        Me.SearchForOarsAtOutworldzToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.SearchForOarsAtOutworldzToolStripMenuItem.Text = "Search for Oars at Outworldz"
        '
        'LoadLocalOARToolStripMenuItem
        '
        Me.LoadLocalOARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.LoadLocalOARToolStripMenuItem.Name = "LoadLocalOARToolStripMenuItem"
        Me.LoadLocalOARToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.LoadLocalOARToolStripMenuItem.Text = "Load Local OAR"
        '
        'LoadIARsToolMenuItem
        '
        Me.LoadIARsToolMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadInventoryIARToolStripMenuItem1, Me.SaveInventoryIARToolStripMenuItem1, Me.LoadLocalIARToolStripMenuItem, Me.FindIARsOnOutworldzToolStripMenuItem})
        Me.LoadIARsToolMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.LoadIARsToolMenuItem.Name = "LoadIARsToolMenuItem"
        Me.LoadIARsToolMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.LoadIARsToolMenuItem.Text = "Inventory IAR Load and Save"
        '
        'LoadInventoryIARToolStripMenuItem1
        '
        Me.LoadInventoryIARToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.data
        Me.LoadInventoryIARToolStripMenuItem1.Name = "LoadInventoryIARToolStripMenuItem1"
        Me.LoadInventoryIARToolStripMenuItem1.Size = New System.Drawing.Size(198, 22)
        Me.LoadInventoryIARToolStripMenuItem1.Text = "Load Inventory IAR"
        '
        'SaveInventoryIARToolStripMenuItem1
        '
        Me.SaveInventoryIARToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.SaveInventoryIARToolStripMenuItem1.Name = "SaveInventoryIARToolStripMenuItem1"
        Me.SaveInventoryIARToolStripMenuItem1.Size = New System.Drawing.Size(198, 22)
        Me.SaveInventoryIARToolStripMenuItem1.Text = "Save Inventory IAR"
        '
        'LoadLocalIARToolStripMenuItem
        '
        Me.LoadLocalIARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.LoadLocalIARToolStripMenuItem.Name = "LoadLocalIARToolStripMenuItem"
        Me.LoadLocalIARToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.LoadLocalIARToolStripMenuItem.Text = "Load Local IAR"
        '
        'FindIARsOnOutworldzToolStripMenuItem
        '
        Me.FindIARsOnOutworldzToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.FindIARsOnOutworldzToolStripMenuItem.Name = "FindIARsOnOutworldzToolStripMenuItem"
        Me.FindIARsOnOutworldzToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.FindIARsOnOutworldzToolStripMenuItem.Text = "Find IARs on Outworldz"
        '
        'MoreFreeIslandsandPartsContentToolStripMenuItem
        '
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem.Name = "MoreFreeIslandsandPartsContentToolStripMenuItem"
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.More_Free_Islands_and_Parts_word
        Me.MoreFreeIslandsandPartsContentToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Free_DLC_word
        '
        'SearchForObjectsMenuItem
        '
        Me.SearchForObjectsMenuItem.Image = Global.Outworldz.My.Resources.Resources.text_find
        Me.SearchForObjectsMenuItem.Name = "SearchForObjectsMenuItem"
        Me.SearchForObjectsMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.SearchForObjectsMenuItem.Text = Global.Outworldz.My.Resources.Resources.Search_Events
        '
        'SearchForGridsMenuItem
        '
        Me.SearchForGridsMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_colors
        Me.SearchForGridsMenuItem.Name = "SearchForGridsMenuItem"
        Me.SearchForGridsMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.SearchForGridsMenuItem.Text = "Search for Grid (www)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(265, 6)
        '
        'CheckAndRepairDatbaseToolStripMenuItem
        '
        Me.CheckAndRepairDatbaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
        Me.CheckAndRepairDatbaseToolStripMenuItem.Name = "CheckAndRepairDatbaseToolStripMenuItem"
        Me.CheckAndRepairDatbaseToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.CheckAndRepairDatbaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Check_and_Repair_Database_word
        '
        'BackupRestoreToolStripMenuItem
        '
        Me.BackupRestoreToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackupDatabaseToolStripMenuItem, Me.RestoreDatabaseToolStripMenuItem1})
        Me.BackupRestoreToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupRestoreToolStripMenuItem.Name = "BackupRestoreToolStripMenuItem"
        Me.BackupRestoreToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.BackupRestoreToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.SQL_Database_Backup_Restore
        '
        'BackupDatabaseToolStripMenuItem
        '
        Me.BackupDatabaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupDatabaseToolStripMenuItem.Name = "BackupDatabaseToolStripMenuItem"
        Me.BackupDatabaseToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BackupDatabaseToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.BackupDatabaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Backup_Databases
        '
        'RestoreDatabaseToolStripMenuItem1
        '
        Me.RestoreDatabaseToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.cube_green
        Me.RestoreDatabaseToolStripMenuItem1.Name = "RestoreDatabaseToolStripMenuItem1"
        Me.RestoreDatabaseToolStripMenuItem1.Size = New System.Drawing.Size(210, 22)
        Me.RestoreDatabaseToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Restore_Database_word
        '
        'BackupCriticalFilesToolStripMenuItem
        '
        Me.BackupCriticalFilesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupCriticalFilesToolStripMenuItem.Name = "BackupCriticalFilesToolStripMenuItem"
        Me.BackupCriticalFilesToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.BackupCriticalFilesToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.System_Backup_word
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(265, 6)
        '
        'RestartMysqlIcon
        '
        Me.RestartMysqlIcon.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.StopToolStripMenuItem1, Me.RestartMysqlItem, Me.StartToolStripMenuItem1, Me.DeleteServiceToolStripMenuItem1, Me.ConnectToConsoleToolStripMenuItem})
        Me.RestartMysqlIcon.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.RestartMysqlIcon.Name = "RestartMysqlIcon"
        Me.RestartMysqlIcon.Size = New System.Drawing.Size(71, 24)
        Me.RestartMysqlIcon.Text = Global.Outworldz.My.Resources.Resources.Mysql_Word
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(184, 26)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'StopToolStripMenuItem1
        '
        Me.StopToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.media_stop
        Me.StopToolStripMenuItem1.Name = "StopToolStripMenuItem1"
        Me.StopToolStripMenuItem1.Size = New System.Drawing.Size(184, 26)
        Me.StopToolStripMenuItem1.Text = "Stop"
        '
        'RestartMysqlItem
        '
        Me.RestartMysqlItem.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.RestartMysqlItem.Name = "RestartMysqlItem"
        Me.RestartMysqlItem.Size = New System.Drawing.Size(184, 26)
        Me.RestartMysqlItem.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        '
        'StartToolStripMenuItem1
        '
        Me.StartToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.media_play
        Me.StartToolStripMenuItem1.Name = "StartToolStripMenuItem1"
        Me.StartToolStripMenuItem1.Size = New System.Drawing.Size(184, 26)
        Me.StartToolStripMenuItem1.Text = "Start"
        '
        'DeleteServiceToolStripMenuItem1
        '
        Me.DeleteServiceToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.media_stop_red
        Me.DeleteServiceToolStripMenuItem1.Name = "DeleteServiceToolStripMenuItem1"
        Me.DeleteServiceToolStripMenuItem1.Size = New System.Drawing.Size(184, 26)
        Me.DeleteServiceToolStripMenuItem1.Text = "Delete Service"
        '
        'ConnectToConsoleToolStripMenuItem
        '
        Me.ConnectToConsoleToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.data_copy1
        Me.ConnectToConsoleToolStripMenuItem.Name = "ConnectToConsoleToolStripMenuItem"
        Me.ConnectToConsoleToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.ConnectToConsoleToolStripMenuItem.Text = "Connect to Console"
        '
        'RestartRobustIcon
        '
        Me.RestartRobustIcon.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem2, Me.RestartRobustItem})
        Me.RestartRobustIcon.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.RestartRobustIcon.Name = "RestartRobustIcon"
        Me.RestartRobustIcon.Size = New System.Drawing.Size(76, 24)
        Me.RestartRobustIcon.Text = Global.Outworldz.My.Resources.Resources.Robust_word
        '
        'HelpToolStripMenuItem2
        '
        Me.HelpToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem2.Name = "HelpToolStripMenuItem2"
        Me.HelpToolStripMenuItem2.Size = New System.Drawing.Size(110, 22)
        Me.HelpToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'RestartRobustItem
        '
        Me.RestartRobustItem.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.RestartRobustItem.Name = "RestartRobustItem"
        Me.RestartRobustItem.Size = New System.Drawing.Size(110, 22)
        Me.RestartRobustItem.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        '
        'RestartApacheIcon
        '
        Me.RestartApacheIcon.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem3, Me.RestartToolStripMenuItem2, Me.StopToolStripMenuItem, Me.StartToolStripMenuItem, Me.DeleteServiceToolStripMenuItem, Me.ConnectToWebPageToolStripMenuItem})
        Me.RestartApacheIcon.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.RestartApacheIcon.Name = "RestartApacheIcon"
        Me.RestartApacheIcon.Size = New System.Drawing.Size(79, 24)
        Me.RestartApacheIcon.Text = Global.Outworldz.My.Resources.Resources.Apache_word
        '
        'HelpToolStripMenuItem3
        '
        Me.HelpToolStripMenuItem3.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem3.Name = "HelpToolStripMenuItem3"
        Me.HelpToolStripMenuItem3.Size = New System.Drawing.Size(189, 22)
        Me.HelpToolStripMenuItem3.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'RestartToolStripMenuItem2
        '
        Me.RestartToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.RestartToolStripMenuItem2.Name = "RestartToolStripMenuItem2"
        Me.RestartToolStripMenuItem2.Size = New System.Drawing.Size(189, 22)
        Me.RestartToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.media_stop
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.StopToolStripMenuItem.Text = "Stop "
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.media_play
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'DeleteServiceToolStripMenuItem
        '
        Me.DeleteServiceToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.media_stop_red
        Me.DeleteServiceToolStripMenuItem.Name = "DeleteServiceToolStripMenuItem"
        Me.DeleteServiceToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.DeleteServiceToolStripMenuItem.Text = "Delete Service"
        '
        'ConnectToWebPageToolStripMenuItem
        '
        Me.ConnectToWebPageToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ConnectToWebPageToolStripMenuItem.Name = "ConnectToWebPageToolStripMenuItem"
        Me.ConnectToWebPageToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ConnectToWebPageToolStripMenuItem.Text = "Connect to Web page"
        '
        'RestartIcecastIcon
        '
        Me.RestartIcecastIcon.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem4, Me.RestartIceCastItem2})
        Me.RestartIcecastIcon.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.RestartIcecastIcon.Name = "RestartIcecastIcon"
        Me.RestartIcecastIcon.Size = New System.Drawing.Size(75, 24)
        Me.RestartIcecastIcon.Text = Global.Outworldz.My.Resources.Resources.Icecast_word
        '
        'HelpToolStripMenuItem4
        '
        Me.HelpToolStripMenuItem4.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem4.Name = "HelpToolStripMenuItem4"
        Me.HelpToolStripMenuItem4.Size = New System.Drawing.Size(110, 22)
        Me.HelpToolStripMenuItem4.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'RestartIceCastItem2
        '
        Me.RestartIceCastItem2.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.RestartIceCastItem2.Name = "RestartIceCastItem2"
        Me.RestartIceCastItem2.Size = New System.Drawing.Size(110, 22)
        Me.RestartIceCastItem2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        '
        'Timer1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'AvatarLabel
        '
        Me.AvatarLabel.AutoSize = True
        Me.AvatarLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AvatarLabel.Location = New System.Drawing.Point(3, 0)
        Me.AvatarLabel.Name = "AvatarLabel"
        Me.AvatarLabel.Size = New System.Drawing.Size(55, 15)
        Me.AvatarLabel.TabIndex = 0
        Me.AvatarLabel.Text = "0 Avatars"
        '
        'PercentCPU
        '
        Me.PercentCPU.AutoSize = True
        Me.PercentCPU.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PercentCPU.Location = New System.Drawing.Point(125, 0)
        Me.PercentCPU.Name = "PercentCPU"
        Me.PercentCPU.Size = New System.Drawing.Size(49, 15)
        Me.PercentCPU.TabIndex = 2
        Me.PercentCPU.Text = "0% CPU"
        '
        'PercentRAM
        '
        Me.PercentRAM.AutoSize = True
        Me.PercentRAM.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PercentRAM.Location = New System.Drawing.Point(64, 0)
        Me.PercentRAM.Name = "PercentRAM"
        Me.PercentRAM.Size = New System.Drawing.Size(55, 15)
        Me.PercentRAM.TabIndex = 1
        Me.PercentRAM.Text = "0 % RAM"
        '
        'BusyButton
        '
        Me.BusyButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BusyButton.Location = New System.Drawing.Point(12, 31)
        Me.BusyButton.Name = "BusyButton"
        Me.BusyButton.Size = New System.Drawing.Size(84, 23)
        Me.BusyButton.TabIndex = 1
        Me.BusyButton.Text = Global.Outworldz.My.Resources.Resources.Busy_word
        Me.BusyButton.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.Location = New System.Drawing.Point(12, 31)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(84, 23)
        Me.StopButton.TabIndex = 50
        Me.StopButton.Text = Global.Outworldz.My.Resources.Resources.Stop_word
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(12, 31)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(84, 23)
        Me.StartButton.TabIndex = 49
        Me.StartButton.Text = Global.Outworldz.My.Resources.Resources.Start_word
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'DiskSize
        '
        Me.DiskSize.AutoSize = True
        Me.DiskSize.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DiskSize.Location = New System.Drawing.Point(180, 0)
        Me.DiskSize.Name = "DiskSize"
        Me.DiskSize.Size = New System.Drawing.Size(0, 15)
        Me.DiskSize.TabIndex = 3
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.DiskSize, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AvatarLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PercentCPU, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PercentRAM, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(142, 31)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(347, 23)
        Me.TableLayoutPanel1.TabIndex = 18611
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.AutoWordSelection = True
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 58)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(573, 96)
        Me.TextBox1.TabIndex = 18612
        Me.TextBox1.Text = ""
        '
        'FormSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(597, 161)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.BusyButton)
        Me.Controls.Add(Me.StopButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(351, 200)
        Me.Name = "FormSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DreamGrid"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub New()

        InitializeComponent()

    End Sub
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuExit As ToolStripMenuItem
    Friend WithEvents mnuSettings As ToolStripMenuItem
    Friend WithEvents ConsoleToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents mnuHide As ToolStripMenuItem
    Friend WithEvents mnuShow As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuAbout As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ViewWebUI As ToolStripMenuItem
    Friend WithEvents MnuContent As ToolStripMenuItem
    Friend WithEvents MensClothingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FemaleClothingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoopBackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoreFreeIslandsandPartsContentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdvancedSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CHeckForUpdatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DiagnosticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents ShowHyperGridAddressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConsoleCOmmandsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RegionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupRestoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestoreDatabaseToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ViewIcecastWebPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SimulatorStatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents HelpOnOARsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpOnIARSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupCriticalFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents TechnicalInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TroubleshootingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckAndRepairDatbaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpStartingUpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HelpOnSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewLogsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents RevisionHistoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AvatarLabel As Label
    Friend WithEvents PDFManualToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PercentCPU As Label
    Friend WithEvents PercentRAM As Label
    Friend WithEvents JustQuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BusyButton As Button
    Friend WithEvents StopButton As Button
    Friend WithEvents StartButton As Button
    Friend WithEvents SeePortsInUseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewRegionMapToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuHideAllways As ToolStripMenuItem
    Friend WithEvents RestartMysqlIcon As ToolStripMenuItem
    Friend WithEvents RestartApacheIcon As ToolStripMenuItem
    Friend WithEvents RestartRobustIcon As ToolStripMenuItem
    Friend WithEvents RestartIcecastIcon As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RestartMysqlItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents RestartRobustItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents RestartIceCastItem2 As ToolStripMenuItem
    Friend WithEvents CommonConsoleCommandsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddUserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowUserDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SendAlertToAllUsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllUsersAllSimsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JustOneRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Debug As ToolStripMenuItem
    Friend WithEvents Info As ToolStripMenuItem
    Friend WithEvents Warn As ToolStripMenuItem
    Friend WithEvents ErrorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Fatal1 As ToolStripMenuItem
    Friend WithEvents Off1 As ToolStripMenuItem
    Friend WithEvents RestartRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartOneRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartTheInstanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsStopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsStartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsSuspendToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsResumeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ThreadpoolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents XengineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobEngineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IslandToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClothingInventoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadLocalOARSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents SearchForObjectsMenuItem As ToolStripMenuItem
    Friend WithEvents SearchForGridsMenuItem As ToolStripMenuItem
    Friend WithEvents LoadRegionOARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveRegionOARToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveAllRunningRegiondsAsOARSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadIARsToolMenuItem As ToolStripMenuItem
    Friend WithEvents LoadInventoryIARToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveInventoryIARToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LoadLocalIARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindIARsOnOutworldzToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchForOarsAtOutworldzToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadLocalOARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartAllRegionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KeepOnTopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OnTopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FloatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LanguageToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DebugToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteServiceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DiskSize As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ConnectToConsoleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents StopToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DeleteServiceToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ConnectToWebPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox1 As RichTextBox
End Class
