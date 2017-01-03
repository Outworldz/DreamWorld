﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ConsoleShow() As Boolean
            Get
                Return CType(Me("ConsoleShow"),Boolean)
            End Get
            Set
                Me("ConsoleShow") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AvatarShow() As Boolean
            Get
                Return CType(Me("AvatarShow"),Boolean)
            End Get
            Set
                Me("AvatarShow") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ViewerEase() As Boolean
            Get
                Return CType(Me("ViewerEase"),Boolean)
            End Get
            Set
                Me("ViewerEase") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Opensim")>  _
        Public Property GridFolder() As String
            Get
                Return CType(Me("GridFolder"),String)
            End Get
            Set
                Me("GridFolder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoBackup() As Boolean
            Get
                Return CType(Me("AutoBackup"),Boolean)
            End Get
            Set
                Me("AutoBackup") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8002")>  _
        Public Property PublicPort() As String
            Get
                Return CType(Me("PublicPort"),String)
            End Get
            Set
                Me("PublicPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("127.0.0.1")>  _
        Public Property PublicIP() As String
            Get
                Return CType(Me("PublicIP"),String)
            End Get
            Set
                Me("PublicIP") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Onlook() As Boolean
            Get
                Return CType(Me("Onlook"),Boolean)
            End Get
            Set
                Me("Onlook") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("DreamWorld")>  _
        Public Property SimName() As String
            Get
                Return CType(Me("SimName"),String)
            End Get
            Set
                Me("SimName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2000")>  _
        Public Property CoordX() As String
            Get
                Return CType(Me("CoordX"),String)
            End Get
            Set
                Me("CoordX") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2000")>  _
        Public Property CoordY() As String
            Get
                Return CType(Me("CoordY"),String)
            End Get
            Set
                Me("CoordY") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8003")>  _
        Public Property PrivatePort() As String
            Get
                Return CType(Me("PrivatePort"),String)
            End Get
            Set
                Me("PrivatePort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("256")>  _
        Public Property SizeX() As String
            Get
                Return CType(Me("SizeX"),String)
            End Get
            Set
                Me("SizeX") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("256")>  _
        Public Property SizeY() As String
            Get
                Return CType(Me("SizeY"),String)
            End Get
            Set
                Me("SizeY") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8001")>  _
        Public Property LoopBack() As String
            Get
                Return CType(Me("LoopBack"),String)
            End Get
            Set
                Me("LoopBack") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3308")>  _
        Public Property MySqlPort() As String
            Get
                Return CType(Me("MySqlPort"),String)
            End Get
            Set
                Me("MySqlPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("720")>  _
        Public Property AutobackupInterval() As String
            Get
                Return CType(Me("AutobackupInterval"),String)
            End Get
            Set
                Me("AutobackupInterval") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("30")>  _
        Public Property KeepForDays() As Integer
            Get
                Return CType(Me("KeepForDays"),Integer)
            End Get
            Set
                Me("KeepForDays") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("secret")>  _
        Public Property Password() As String
            Get
                Return CType(Me("Password"),String)
            End Get
            Set
                Me("Password") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Wifi")>  _
        Public Property AdminFirst() As String
            Get
                Return CType(Me("AdminFirst"),String)
            End Get
            Set
                Me("AdminFirst") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Admin")>  _
        Public Property AdminLast() As String
            Get
                Return CType(Me("AdminLast"),String)
            End Get
            Set
                Me("AdminLast") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("email@somewhere.com")>  _
        Public Property AdminEmail() As String
            Get
                Return CType(Me("AdminEmail"),String)
            End Get
            Set
                Me("AdminEmail") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Console")>  _
        Public Property ConsoleUser() As String
            Get
                Return CType(Me("ConsoleUser"),String)
            End Get
            Set
                Me("ConsoleUser") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("secret")>  _
        Public Property ConsolePass() As String
            Get
                Return CType(Me("ConsolePass"),String)
            End Get
            Set
                Me("ConsolePass") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8004")>  _
        Public Property RegionPort() As String
            Get
                Return CType(Me("RegionPort"),String)
            End Get
            Set
                Me("RegionPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1000")>  _
        Public Property ChatTime() As Integer
            Get
                Return CType(Me("ChatTime"),Integer)
            End Get
            Set
                Me("ChatTime") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ViewerInstalled() As Boolean
            Get
                Return CType(Me("ViewerInstalled"),Boolean)
            End Get
            Set
                Me("ViewerInstalled") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property ImageNum() As Integer
            Get
                Return CType(Me("ImageNum"),Integer)
            End Get
            Set
                Me("ImageNum") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property allow_grid_gods() As Boolean
            Get
                Return CType(Me("allow_grid_gods"),Boolean)
            End Get
            Set
                Me("allow_grid_gods") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property region_owner_is_god() As Boolean
            Get
                Return CType(Me("region_owner_is_god"),Boolean)
            End Get
            Set
                Me("region_owner_is_god") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property region_manager_is_god() As Boolean
            Get
                Return CType(Me("region_manager_is_god"),Boolean)
            End Get
            Set
                Me("region_manager_is_god") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property parcel_owner_is_god() As Boolean
            Get
                Return CType(Me("parcel_owner_is_god"),Boolean)
            End Get
            Set
                Me("parcel_owner_is_god") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("60")>  _
        Public Property TimerInterval() As Integer
            Get
                Return CType(Me("TimerInterval"),Integer)
            End Get
            Set
                Me("TimerInterval") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AccountConfirmationRequired() As Boolean
            Get
                Return CType(Me("AccountConfirmationRequired"),Boolean)
            End Get
            Set
                Me("AccountConfirmationRequired") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Gmail for notification")>  _
        Public Property SmtpUsername() As String
            Get
                Return CType(Me("SmtpUsername"),String)
            End Get
            Set
                Me("SmtpUsername") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Gmail password")>  _
        Public Property SmtpPassword() As String
            Get
                Return CType(Me("SmtpPassword"),String)
            End Get
            Set
                Me("SmtpPassword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property RanAllDiags() As Boolean
            Get
                Return CType(Me("RanAllDiags"),Boolean)
            End Get
            Set
                Me("RanAllDiags") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SkipUpdateCheck() As Boolean
            Get
                Return CType(Me("SkipUpdateCheck"),Boolean)
            End Get
            Set
                Me("SkipUpdateCheck") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DiagFailed() As Boolean
            Get
                Return CType(Me("DiagFailed"),Boolean)
            End Get
            Set
                Me("DiagFailed") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DnsName() As String
            Get
                Return CType(Me("DnsName"),String)
            End Get
            Set
                Me("DnsName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property WebStats() As Boolean
            Get
                Return CType(Me("WebStats"),Boolean)
            End Get
            Set
                Me("WebStats") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8002")>  _
        Public Property HttpPort() As String
            Get
                Return CType(Me("HttpPort"),String)
            End Get
            Set
                Me("HttpPort") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property MachineID() As String
            Get
                Return CType(Me("MachineID"),String)
            End Get
            Set
                Me("MachineID") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property LoopBackDiag() As Boolean
            Get
                Return CType(Me("LoopBackDiag"),Boolean)
            End Get
            Set
                Me("LoopBackDiag") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UPnPDiag() As Boolean
            Get
                Return CType(Me("UPnPDiag"),Boolean)
            End Get
            Set
                Me("UPnPDiag") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("opensim")>  _
        Public Property DBName() As String
            Get
                Return CType(Me("DBName"),String)
            End Get
            Set
                Me("DBName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("opensimuser")>  _
        Public Property DBUserID() As String
            Get
                Return CType(Me("DBUserID"),String)
            End Get
            Set
                Me("DBUserID") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("opensimpassword")>  _
        Public Property DBPassword() As String
            Get
                Return CType(Me("DBPassword"),String)
            End Get
            Set
                Me("DBPassword") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("localhost")>  _
        Public Property DBSource() As String
            Get
                Return CType(Me("DBSource"),String)
            End Get
            Set
                Me("DBSource") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property SplashPage() As String
            Get
                Return CType(Me("SplashPage"),String)
            End Get
            Set
                Me("SplashPage") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3")>  _
        Public Property Physics() As Integer
            Get
                Return CType(Me("Physics"),Integer)
            End Get
            Set
                Me("Physics") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property MyX() As Integer
            Get
                Return CType(Me("MyX"),Integer)
            End Get
            Set
                Me("MyX") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property MyY() As Integer
            Get
                Return CType(Me("MyY"),Integer)
            End Get
            Set
                Me("MyY") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property MyIP() As String
            Get
                Return CType(Me("MyIP"),String)
            End Get
            Set
                Me("MyIP") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Outworldz.My.MySettings
            Get
                Return Global.Outworldz.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
