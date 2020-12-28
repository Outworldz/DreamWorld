﻿#Region "Copyright"

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

Imports System.IO
Imports IniParser
Imports IniParser.Model

Public Class MySettings

#Region "Private Fields"

    Private Const DreamGrid As String = "DreamGrid"
    Private Const JOpensim As String = "JOpensim"

    Dim Apachein As New List(Of String)
    Dim Apacheout As New List(Of String)
    Dim Data As IniParser.Model.IniData
    Dim gFolder As String
    Dim INI As String = ""
    Dim MyData As IniParser.Model.IniData
    Dim myINI As String = ""
    Dim Myparser As IniParser.FileIniDataParser
    Dim parser As IniParser.FileIniDataParser

#End Region

#Region "New"

    Public Sub New()

        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""

    End Sub

    Public Sub Init(Folder As String)
        gFolder = Folder

        myINI = Folder + "\OutworldzFiles\Settings.ini"
        If File.Exists(myINI) Then
            LoadSettingsIni()
            Settings.CurrentDirectory = Folder
        Else
            myINI = Folder + "\OutworldzFiles\Settings.ini"
            Dim contents = "[Data]" + vbCrLf
            Try
                Using outputFile As New StreamWriter(myINI, False)
                    outputFile.WriteLine(contents)
                End Using
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            LoadSettingsIni()

            Settings.CurrentDirectory = Folder

            Dim SCPasswordAdmin = New PassGen
            SCPassword() = SCPasswordAdmin.GeneratePass()
            SCAdminPassword() = SCPasswordAdmin.GeneratePass()

            'email
            SmtpHost() = "smtp.gmail.com"
            SmtpPort() = 587

        End If

        If Theme().Length = 0 Then
            Theme() = "White"
            FormSetup.CopyWifi("White")
        ElseIf Theme() = "Black" Then
            FormSetup.CopyWifi("Black")
        ElseIf Theme() = "White" Then
            FormSetup.CopyWifi("White")
        ElseIf Theme() = "Custom" Then
            FormSetup.CopyWifi("Custom")
        End If

    End Sub

#End Region

#Region "Functions And Subs"

    Public Function LoadIni(arg As String, comment As String) As Boolean

        'FormSetup.Log(My.Resources.Info_word, "Loading INI " & arg)
        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = comment ' Opensim uses semicolons
        Try
            Data = parser.ReadFile(arg, System.Text.Encoding.UTF8)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            MsgBox(ex.Message)
            FormSetup.Logger("Warn", ex.Message, "Error")
            Return True
        End Try
        INI = arg
        Return False
    End Function

    Public Sub LoadSettingsIni()

        Myparser = New FileIniDataParser()

        Myparser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        Myparser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons

        Try
            MyData = Myparser.ReadFile(gFolder + "\OutworldzFiles\Settings.ini", System.Text.Encoding.UTF8)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    ''' <summary>writes to the ini the name value pair.</summary>
    ''' <param name="section"></param>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Sub SetIni(section As String, key As String, value As String)

        ' sets values into any INI file Form1.Log(My.Resources.Info, "Writing section [" + section +
        ' "] " + key + "=" + value)
        Try
            Data(section)(key) = value
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            FormSetup.ErrorLog(ex.Message)
        End Try

    End Sub

    Public Sub SetMyIni(section As String, key As String, value As String)

        If value = Nothing Then value = ""
        ' sets values into any INI file
        Try
            MyData(section)(key) = value
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            FormSetup.ErrorLog(ex.Message)
        End Try

    End Sub

#End Region

#Region "GetSet"

    Shared Function Stripqq(input As String) As String

        ' remove double quotes and any comments ";"
        Return Replace(input, """", "")

    End Function

    Public Function GetIni(section As String, key As String, Value As String, Optional V As String = Nothing) As Object

        Dim Variable = Stripqq(Data(section)(key))
        If Variable = Nothing Then Variable = Value
        If Variable Is Nothing Then Return Value

        Dim bool As Boolean
        If V = "Boolean" Then
            If Not Boolean.TryParse(Variable, bool) Then
                Return Variable
            End If
            Return bool
        ElseIf V = "String" Then
            Return Variable
        ElseIf V = "Double" Then
            Dim DBL As Double
            If Not Double.TryParse(Variable, DBL) Then
                Return Variable
            End If
            Return DBL
        ElseIf V = "Single" Then
            Dim SNG As Single
            If Not Single.TryParse(Variable, SNG) Then
                Return Variable
            End If
            Return SNG
        ElseIf V = "Integer" Then
            Dim I As Integer
            If Not Integer.TryParse(Variable, I) Then
                Return Variable
            End If
            Return I
        End If

        Return Variable

    End Function

    Public Function GetMySetting(key As String, Optional D As String = "") As String

        Try
            Dim value = Stripqq(MyData("Data")(key))
            If value = Nothing Then value = D
#Disable Warning CA1062 ' Validate arguments of public methods
            Return value.ToString(Globalization.CultureInfo.InvariantCulture)
#Enable Warning CA1062 ' Validate arguments of public methods
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Return D
        End Try

    End Function

    Public Sub SaveINI(encoding As System.Text.Encoding)

        'FormSetup.Log(My.Resources.Info_word, "Save INI " & INI)
        Try
            parser.WriteFile(INI, Data, encoding)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            FormSetup.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SaveSettings()

        FormSetup.Log(My.Resources.Info_word, "Save Settings " & myINI)
        Try
            Myparser.WriteFile(myINI, MyData, System.Text.Encoding.UTF8)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            MsgBox(My.Resources.Unable_2_Save + myINI)
            FormSetup.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SetMySetting(key As String, value As String)
        Try
#Disable Warning CA1062
            SetMyIni("Data", key, value.ToString(Globalization.CultureInfo.InvariantCulture))
#Enable Warning CA1062
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            FormSetup.Logger("Error", ex.Message, "Error")
        End Try

    End Sub

#End Region

#Region "Properties"

    Public Property KeepOnTop() As Boolean
        Get
            Return CType(GetMySetting("BacKeepOnTopkupOARs", "False"), Boolean)
        End Get
        Set
            SetMySetting("KeepOnTop", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupOARs() As Boolean
        Get
            Return CType(GetMySetting("BackupOARs", "True"), Boolean)
        End Get
        Set
            SetMySetting("BackupOARs", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupWifi() As Boolean
        Get
            Return CType(GetMySetting("BackupWifi", "True"), Boolean)
        End Get
        Set
            SetMySetting("BackupWifi", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupFSAssets() As Boolean
        Get
            Return CType(GetMySetting("BackupFSAssets", "True"), Boolean)
        End Get
        Set
            SetMySetting("BackupFSAssets", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupMysql() As Boolean
        Get
            Return CType(GetMySetting("BackupMysql", "True"), Boolean)
        End Get
        Set
            SetMySetting("BackupMysql", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupRegion() As Boolean
        Get
            Return CType(GetMySetting("BackupRegion", "True"), Boolean)
        End Get
        Set
            SetMySetting("BackupRegion", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AccountConfirmationRequired() As Boolean
        Get
            Return CType(GetMySetting("AccountConfirmationRequired", "False"), Boolean)
        End Get
        Set
            SetMySetting("AccountConfirmationRequired", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AdminEmail() As String
        Get
            Dim mail As String = GetMySetting("AdminEmail", "not@set.yet")
            Return mail
        End Get
        Set
            SetMySetting("AdminEmail", Value)
        End Set
    End Property

    Public Property MysqlRev() As String
        Get
            Dim mail As String = GetMySetting("MysqlRev", "")
            Return mail
        End Get
        Set
            SetMySetting("MysqlRev", Value)
        End Set
    End Property

    Public Property AdminFirst() As String
        Get
            Return GetMySetting("AdminFirst", "Wifi")
        End Get
        Set
            SetMySetting("AdminFirst", Value)
        End Set
    End Property

    Public Property AdminLast() As String
        Get
            Return GetMySetting("AdminLast", "Admin")
        End Get
        Set
            SetMySetting("AdminLast", Value)
        End Set
    End Property

    Public Property JOpensimSearch() As String
        Get
            Return GetMySetting("JOpensimSearch", "")
        End Get
        Set
            SetMySetting("JOpensimSearch", Value)
        End Set
    End Property

    Public Property AllowGridGods() As Boolean
        Get
            Return CType(GetMySetting("Allow_grid_gods", "False"), Boolean)
        End Get
        Set
            SetMySetting("Allow_grid_gods", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AltDnsName() As String
        Get
            Dim AltDns As String = GetMySetting("AltDnsName", "")
            Return AltDns
        End Get
        Set
            SetMySetting("AltDnsName", Value)
        End Set
    End Property

    Public Property ApacheEnable() As Boolean
        Get
            Return CType(GetMySetting("ApacheEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("ApacheEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SiteMap() As Boolean
        Get
            Return CType(GetMySetting("SiteMap", "True"), Boolean)
        End Get
        Set
            SetMySetting("SiteMap", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ApachePort() As Integer
        Get
            Return CInt("0" & GetMySetting("ApachePort", "80"))
        End Get
        Set
            SetMySetting("ApachePort", CType(Value, String))
        End Set
    End Property

    Public Property ApacheService() As Boolean
        Get
            Return CType(GetMySetting("ApacheService", "False"), Boolean)
        End Get
        Set
            SetMySetting("ApacheService", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AutoBackup() As Boolean
        Get
            Return CType(GetMySetting("AutoBackup", "True"), Boolean)
        End Get
        Set
            SetMySetting("AutoBackup", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AutobackupInterval() As String
        Get
            Return GetMySetting("AutobackupInterval", "720")
        End Get
        Set
            SetMySetting("AutobackupInterval", Value)
        End Set
    End Property

    Public Property AutoRestartEnabled() As Boolean
        Get
            Return CType(GetMySetting("AutoRestartEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("AutoRestartEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property AutoRestartInterval() As Integer
        Get
            Return CInt("0" & GetMySetting("AutoRestartInterval", "0".ToUpperInvariant))
        End Get
        Set
            SetMySetting("AutoRestartInterval", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property Autostart() As Boolean
        Get
            Return CType(GetMySetting("Autostart", "False"), Boolean)
        End Get
        Set
            SetMySetting("Autostart", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BackupFolder() As String
        Get
            Return GetMySetting("BackupFolder", "AutoBackup")
        End Get
        Set
            SetMySetting("BackupFolder", Value)
        End Set
    End Property

    Public Property BanList() As String
        Get
            Return GetMySetting("BanList", "")
        End Get
        Set
            SetMySetting("BanList", Value)
        End Set
    End Property

    'BaseDirectory
    Public Property BaseDirectory() As String
        Get
            Return GetMySetting("BaseDirectory", "./fsassets")
        End Get
        Set
            SetMySetting("BaseDirectory", Value)
        End Set
    End Property

    Public Property BaseHostName() As String
        Get
            Return GetMySetting("BaseHostName", DNSName)
        End Get
        Set
            SetMySetting("BaseHostName", Value)
        End Set
    End Property

    ''' <summary>how close to the edge of a region can we get?</summary>
    ''' <returns></returns>
    Public Property BirdsBorderSize() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsBorderSize", "25"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 25
        End Get
        Set
            SetMySetting("BirdsBorderSize", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>which channel do we listen on for in world commands</summary>
    ''' <returns></returns>
    Public Property BirdsChatChannel() As Integer
        Get
            Return CInt("0" & GetMySetting("BirdsChatChannel", "118"))
        End Get
        Set
            SetMySetting("BirdsChatChannel", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>how far away from other birds we would like to stay</summary>
    ''' <returns></returns>
    Public Property BirdsDesiredSeparation() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsDesiredSeparation", "5"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 5
        End Get
        Set
            SetMySetting("BirdsDesiredSeparation", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BirdsEnabled() As Boolean
        Get
            Return CType(GetMySetting("BirdsEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("BirdsEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BirdsFlockSize() As Integer
        Get
            Return CInt("0" & GetMySetting("BirdsFlockSize", "25"))
        End Get
        Set
            SetMySetting("BirdsFlockSize", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BirdsMaxForce() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsMaxForce", "0.2"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 0.2
        End Get
        Set
            SetMySetting("BirdsMaxForce", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>how high are we allowed to flock</summary>
    ''' <returns></returns>
    Public Property BirdsMaxHeight() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsMaxHeight", "25"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 25
        End Get
        Set
            SetMySetting("BirdsMaxHeight", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>'how far each bird can travel per update</summary>
    ''' <returns></returns>
    Public Property BirdsMaxSpeed() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsMaxSpeed", "1.0"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 1.0
        End Get
        Set
            SetMySetting("BirdsMaxSpeed", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BirdsModuleStartup() As Boolean
        Get
            Return CType(GetMySetting("BirdsModuleStartup", "False"), Boolean)
        End Get
        Set
            SetMySetting("BirdsModuleStartup", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>max distance for other birds to be considered in the same flock as us</summary>
    ''' <returns></returns>
    Public Property BirdsNeighbourDistance() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsNeighbourDistance", "25"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 25
        End Get
        Set
            SetMySetting("BirdsNeighbourDistance", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>
    ''' By default the module will create a flock of plain wooden spheres, however this can be
    ''' overridden to the name of an existing prim that needs to already exist in the scene - i.e.
    ''' be rezzed in the region.
    ''' </summary>
    ''' <returns></returns>
    Public Property BirdsPrim() As String
        Get
            Return GetMySetting("BirdsPrim", "SeaGull1")
        End Get
        Set
            SetMySetting("BirdsPrim", Value)
        End Set
    End Property

    ''' <summary>how close to the edges of things can we get without being worried</summary>
    ''' <returns></returns>
    Public Property BirdsTolerance() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("BirdsTolerance", "25"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 25
        End Get
        Set
            SetMySetting("BirdsTolerance", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property BroadcastTideInfo() As Boolean
        Get
            Return CType(GetMySetting("BroadcastTideInfo", "True"), Boolean)
        End Get
        Set
            SetMySetting("BroadcastTideInfo", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property CacheEnabled() As Boolean
        Get
            Return CType(GetMySetting("CacheEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("CacheEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property CacheFolder() As String
        Get
            Return GetMySetting("CacheFolder", ".\assetcache")
        End Get
        Set
            SetMySetting("CacheFolder", Value)
        End Set
    End Property

    Public Property CacheLogLevel() As String
        Get
            Return GetMySetting("CacheLogLevel", "0".ToUpperInvariant)
        End Get
        Set
            SetMySetting("CacheLogLevel", Value)
        End Set
    End Property

    Public Property CacheTimeout() As String
        Get
            Return GetMySetting("CacheTimeout", "4")
        End Get
        Set
            SetMySetting("CacheTimeout", Value)
        End Set
    End Property

    Public Property Categories() As String
        Get
            Return GetMySetting("Categories")
        End Get
        Set
            SetMySetting("Categories", Value)
        End Set
    End Property

    Public Property Clouds() As Boolean
        Get
            Return CType(GetMySetting("Clouds", "False"), Boolean)
        End Get
        Set
            SetMySetting("Clouds", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property CMS() As String
        Get
            Dim var = GetMySetting("CMS", DreamGrid)
            If var = "Joomla" Then var = JOpensim
            Dim installed As Boolean = Joomla.IsjOpensimInstalled()
            If (Not installed) & Settings.JOpensimSearch = JOpensim Then
                Return DreamGrid
            End If
            Return var
        End Get
        Set
            SetMySetting("CMS", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ConsolePass() As String
        Get
            Return GetMySetting("ConsolePass")
        End Get
        Set
            SetMySetting("ConsolePass", Value)
        End Set
    End Property

    Public Property ConsoleShow() As String
        Get
            Return GetMySetting("ConsoleShow", "False")
        End Get
        Set
            SetMySetting("ConsoleShow", Value)
        End Set
    End Property

    Public Property ConsoleUser() As String
        Get
            Return GetMySetting("ConsoleUser")
        End Get
        Set
            SetMySetting("ConsoleUser", Value)
        End Set
    End Property

    Public Property CoordX() As Integer
        Get
            Return CInt("0" & GetMySetting("CoordX", CStr(RandomNumber.Between(1010, 990))))
        End Get
        Set
            SetMySetting("CoordX", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property CoordY() As Integer
        Get
            Return CInt("0" & GetMySetting("CoordY", CStr(RandomNumber.Between(1010, 990))))
        End Get
        Set
            SetMySetting("CoordY", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property CPUMAX As Single
        Get
            Return CType(GetMySetting("CPUMax", "90"), Single)
        End Get
        Set(value As Single)
            SetMySetting("CPUMax", CStr(value))
        End Set
    End Property

    Public Property OldInstallFolder() As String
        Get
            Return GetMySetting("OldInstallFolder", "")
        End Get
        Set
            SetMySetting("OldInstallFolder", Value)
        End Set
    End Property

    Public Property LastDirectory() As String
        Get
            Return GetMySetting("LastDirectory") ' no default
        End Get
        Set
            SetMySetting("LastDirectory", Value)
        End Set
    End Property

    Public Property CurrentDirectory() As String
        Get
            Return GetMySetting("Myfolder") ' no default
        End Get
        Set
            SetMySetting("Myfolder", Value)
        End Set
    End Property

    Public Property CycleTime() As Integer
        Get
            Return CInt("0" & GetMySetting("CycleTime", "900"))
        End Get
        Set
            SetMySetting("CycleTime", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property DeleteScriptsOnStartupLevel() As String
        Get
            Return GetMySetting("DeleteScriptsOnStartupLevel", "")
        End Get
        Set
            SetMySetting("DeleteScriptsOnStartupLevel", Value)
        End Set
    End Property

    Public Property Density() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("Density", "0.5"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 0.5
        End Get
        Set
            SetMySetting("Density", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property Description() As String
        Get
            Return GetMySetting("Description")
        End Get
        Set
            SetMySetting("Description", Value)
        End Set
    End Property

    Public Property DiagFailed() As String
        Get
            Return GetMySetting("DiagFailed", "False")
        End Get
        Set
            SetMySetting("DiagFailed", Value)
        End Set
    End Property

    Public Property DiagnosticPort() As Integer
        Get
            Return CInt("0" & GetMySetting("DiagnosticPort", "8001"))
        End Get
        Set
            SetMySetting("DiagnosticPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property DNSName() As String
        Get
            Return GetMySetting("DnsName")
        End Get
        Set
            SetMySetting("DnsName", Value)
        End Set
    End Property

    Public Property EnableHypergrid() As Boolean
        Get
            Return CType(GetMySetting("EnableHypergrid", "True"), Boolean)
        End Get
        Set
            SetMySetting("EnableHypergrid", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ExternalHostName() As String
        Get
            Return GetMySetting("ExternalHostName", DNSName)
        End Get
        Set
            SetMySetting("ExternalHostName", Value)
        End Set
    End Property

    Public Property FirstRegionPort() As Integer
        Get
            Return CInt("0" & GetMySetting("FirstRegionPort", "8004"))
        End Get
        Set
            SetMySetting("FirstRegionPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property FirstXMLRegionPort() As Integer
        Get
            Return CInt("0" & GetMySetting("XMLRegionStartPort", "0"))
        End Get
        Set
            SetMySetting("XMLRegionStartPort", CStr(Value))
        End Set
    End Property

    ' fsassets
    Public Property FsAssetsEnabled() As Boolean
        Get
            Return CType(GetMySetting("FsAssetsEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("FsAssetsEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property GDPR() As Boolean
        Get
            Return CType(GetMySetting("GDPR", "False"), Boolean)
        End Get
        Set
            SetMySetting("GDPR", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property GLBOwnerEmail() As String
        Get
            Return GetMySetting("GLBOwnerEmail")
        End Get
        Set
            SetMySetting("GLBOwnerEmail", Value)
        End Set
    End Property

    Public Property GLBOwnerName() As String
        Get
            Return GetMySetting("GLBOwnerName")
        End Get
        Set
            SetMySetting("GLBOwnerName", Value)
        End Set
    End Property

    ''' <summary>Show  authorization Instant Message to user at session start?</summary>
    ''' <returns>False</returns>
    Public Property GLBShowNewSessionAuthIM() As Boolean
        Get
            Return CType(GetMySetting("GLBShowNewSessionAuthIM", "False"), Boolean)
        End Get
        Set
            SetMySetting("GLBShowNewSessionAuthIM", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>Show purchase Gloebit Instant Message to user at session start?</summary>
    ''' <returns>False</returns>
    Public Property GLBShowNewSessionPurchaseIM() As Boolean
        Get
            Return CType(GetMySetting("GLBShowNewSessionPurchaseIM", "False"), Boolean)
        End Get
        Set
            SetMySetting("GLBShowNewSessionPurchaseIM", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ''' <summary>Show welcome message when entering a region?</summary>
    ''' <returns>True</returns>
    Public Property GLBShowWelcomeMessage() As Boolean
        Get
            Return CType(GetMySetting("GLBShowWelcomeMessage", "True"), Boolean)
        End Get
        Set
            SetMySetting("GLBShowWelcomeMessage", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property GloebitsEnable() As Boolean
        Get
            Return CType(GetMySetting("GloebitsEnable", "False"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsEnable", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property GloebitsMode() As Boolean
        Get
            Return CType(GetMySetting("GloebitsMode", "False"), Boolean)
        End Get
        Set
            SetMySetting("GloebitsMode", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property GLProdKey() As String
        Get
            Return GetMySetting("GLProdKey")
        End Get
        Set
            SetMySetting("GLProdKey", Value)
        End Set
    End Property

    Public Property GLProdSecret() As String
        Get
            Return GetMySetting("GLProdSecret")
        End Get
        Set
            SetMySetting("GLProdSecret", Value)
        End Set
    End Property

    Public Property GLSandKey() As String
        Get
            Return GetMySetting("GLSandKey")
        End Get
        Set
            SetMySetting("GLSandKey", Value)
        End Set
    End Property

    Public Property GLSandSecret() As String
        Get
            Return GetMySetting("GLSandSecret")
        End Get
        Set
            SetMySetting("GLSandSecret", Value)
        End Set
    End Property

    Public Property HomeVectorX() As String
        Get
            Return GetMySetting("HomeVectorX", "128")
        End Get
        Set
            SetMySetting("HomeVectorX", Value)
        End Set
    End Property

    Public Property HomeVectorY() As String
        Get
            Return GetMySetting("HomeVectorY", "128")
        End Get
        Set
            SetMySetting("HomeVectorY", Value)
        End Set
    End Property

    Public Property HomeVectorZ() As String
        Get
            Return GetMySetting("HomeVectorZ", "24")
        End Get
        Set
            SetMySetting("HomeVectorZ", Value)
        End Set
    End Property

    Public Property HttpPort() As Integer
        Get
            Return CInt("0" & GetMySetting("HttpPort", "8002"))
        End Get
        Set
            SetMySetting("HttpPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property KeepForDays() As Integer
        Get
            Return CInt("0" & GetMySetting("KeepForDays", "7"))
        End Get
        Set
            SetMySetting("KeepForDays", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property Language() As String
        Get
            Return GetMySetting("Language")
        End Get
        Set
            SetMySetting("Language", Value)
        End Set
    End Property

    'LocalServiceModule
    Public Property LocalServiceModule() As String
        Get
            Return GetMySetting("LocalServiceModule", "OpenSim.Services.AssetService.dll:AssetService")
        End Get
        Set
            SetMySetting("LocalServiceModule", Value)
        End Set
    End Property

    Public Property LogLevel() As String
        Get
            Return GetMySetting("LogLevel", "INFO")
        End Get
        Set
            SetMySetting("LogLevel", Value)
        End Set
    End Property

    Public Property LoopBackDiag() As Boolean
        Get
            Return CType(GetMySetting("LoopBackDiag", "True"), Boolean)
        End Get
        Set
            SetMySetting("LoopBackDiag", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property LSLHTTP() As Boolean
        Get
            Return CType(GetMySetting("LSL_HTTP", "False"), Boolean)
        End Get
        Set
            SetMySetting("LSL_HTTP", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MachineID() As String
        Get
            Return GetMySetting("MachineID")
        End Get
        Set
            SetMySetting("MachineID", Value)
        End Set
    End Property

    Public Property MapCenterX() As Integer
        Get
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(WelcomeRegion)
            Dim Center As String = CStr(PropRegionClass.CoordX(RegionUUID))
            Return CInt("0" & GetMySetting("MapCenterX", Center))
        End Get
        Set
            SetMySetting("MapCenterX", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MapCenterY() As Integer
        Get
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(WelcomeRegion)
            Dim Center As String = CStr(PropRegionClass.CoordY(RegionUUID))
            Return CInt("0" & GetMySetting("MapCenterY", Center))
        End Get
        Set
            SetMySetting("MapCenterY", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MapType() As String
        Get
            Return GetMySetting("MapType")
        End Get
        Set
            SetMySetting("MapType", Value)
        End Set
    End Property

    Public Property MinTimerInterval() As Double
        Get
            Try
                Dim value = Convert.ToDouble(GetMySetting("MinTimerInterval", "0.2"), Globalization.CultureInfo.InvariantCulture)
                If value < 0.05 Or value > 1 Then value = 0.2
                Return value
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 0.2
        End Get
        Set
            SetMySetting("MinTimerInterval", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MySqlRegionDBPort() As Integer
        Get
            Return CInt("0" & GetMySetting("MySqlRegionDBPort", "3306"))
        End Get
        Set
            SetMySetting("MySqlRegionDBPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MySqlRobustDBPort() As Integer
        Get
            Return CInt("0" & GetMySetting("MySqlRobustDBPort", "3306"))
        End Get
        Set
            SetMySetting("MySqlRobustDBPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MyX() As Integer
        Get
            Return CInt("0" & GetMySetting("MyX", "0".ToUpperInvariant))
        End Get
        Set
            SetMySetting("MyX", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property MyY() As Integer
        Get
            Return CInt("0" & GetMySetting("MyY", "0".ToUpperInvariant))
        End Get
        Set
            SetMySetting("MyY", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property OpensimBinPath() As String
        Get
            Return GetMySetting("OpensimBinPath")
        End Get
        Set
            SetMySetting("OpensimBinPath", Value)
        End Set
    End Property

    Public Property OutBoundPermissions() As Boolean
        Get
            Return CType(GetMySetting("OutBoundPermissions", "True"), Boolean)
        End Get
        Set
            SetMySetting("OutBoundPermissions", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property OverrideName() As String
        Get
            Return GetMySetting("OverrideName", "")
        End Get
        Set
            SetMySetting("OverrideName", Value)
        End Set
    End Property

    Public Property Password() As String
        Get
            Return GetMySetting("Password")
        End Get
        Set
            SetMySetting("Password", Value)
        End Set
    End Property

    Public Property Physics() As Integer
        Get
            Return CInt("0" & GetMySetting("Physics", "3"))
        End Get
        Set
            SetMySetting("Physics", CType(Value, String))
        End Set
    End Property

    Public Property Primlimits() As Boolean
        Get
            Return CType(GetMySetting("Primlimits", "False"), Boolean)
        End Get
        Set
            SetMySetting("Primlimits", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property PrivatePort() As Integer
        Get
            Return CInt("0" & GetMySetting("PrivatePort", "8003"))
        End Get
        Set
            SetMySetting("PrivatePort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

#Disable Warning CA1056 ' Uri properties should not be strings

    Public Property PrivateURL() As String
#Enable Warning CA1056 ' Uri properties should not be strings
        Get
            Return GetMySetting("PrivateURL")   ' no default
        End Get
        Set
            SetMySetting("PrivateURL", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property PublicIP() As String
        Get
            Return GetMySetting("PublicIP")
        End Get
        Set
            SetMySetting("PublicIP", Value)
        End Set
    End Property

    Public Property RanAllDiags() As Boolean
        Get
            Return CType(GetMySetting("RanAllDiags", "False"), Boolean)
        End Get
        Set
            SetMySetting("RanAllDiags", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RegionDBName() As String
        Get
            Return GetMySetting("RegionDBName", "opensim")
        End Get
        Set
            SetMySetting("RegionDBName", Value)
        End Set
    End Property

    Public Property RegionDbPassword() As String
        Get
            Return GetMySetting("RegionDbPassword", "opensimpassword")
        End Get
        Set
            SetMySetting("RegionDbPassword", Value)
        End Set
    End Property

    Public Property RegionDBUsername() As String
        Get
            Return GetMySetting("RegionDBUsername", "opensimuser")
        End Get
        Set
            SetMySetting("RegionDBUsername", Value)
        End Set
    End Property

    Public Property RegionListView() As Integer
        Get
            Return CInt("0" & GetMySetting("RegionListView", "2"))
        End Get
        Set
            SetMySetting("RegionListView", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RegionListVisible() As Boolean
        Get
            Return CType(GetMySetting("RegionListVisible", "False"), Boolean)
        End Get
        Set
            SetMySetting("RegionListVisible", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RegionManagerIsGod() As Boolean
        Get
            Return CType(GetMySetting("Region_manager_is_god", "False"), Boolean)
        End Get
        Set
            SetMySetting("Region_manager_is_god", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RegionOwnerIsGod() As Boolean
        Get
            Return CType(GetMySetting("Region_owner_is_god", "False"), Boolean)
        End Get
        Set
            SetMySetting("Region_owner_is_god", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RegionServer() As String
        Get
            Return GetMySetting("RegionServer", "127.0.0.1")
        End Get
        Set
            SetMySetting("RegionServer", Value)
        End Set
    End Property

    Public Property RenderMaxHeight() As Double
        Get
            Try
                Return Convert.ToDouble(GetMySetting("RenderMaxHeight", "4096"), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Return 4096

        End Get
        Set
            SetMySetting("RenderMaxHeight", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RenderMinHeight() As Integer
        Get
            Return CInt(GetMySetting("RenderMinHeight", "-100"))
        End Get
        Set
            SetMySetting("RenderMinHeight", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RestartOnCrash() As Boolean
        Get
            Return CType(GetMySetting("RestartOnCrash", "False"), Boolean)
        End Get
        Set
            SetMySetting("RestartOnCrash", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RestartonPhysics() As Boolean
        Get
            Return CType(GetMySetting("RestartonPhysics", "False"), Boolean)
        End Get
        Set
            SetMySetting("RestartonPhysics", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RobustDataBaseName() As String
        Get
            Return GetMySetting("RobustMySqlName", "Robust")
        End Get
        Set
            SetMySetting("RobustMySqlName", Value)
        End Set
    End Property

    Public Property RobustPassword() As String
        Get
            Return GetMySetting("RobustMySqlPassword", "robustpassword")
        End Get
        Set
            SetMySetting("RobustMySqlPassword", Value)
        End Set
    End Property

    Public Property RobustServer() As String
        Get
            Return GetMySetting("RobustServer", "127.0.0.1")
        End Get
        Set
            SetMySetting("RobustServer", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property RobustUsername() As String
        Get
            Return GetMySetting("RobustMySqlUsername", "robustuser")
        End Get
        Set
            SetMySetting("RobustMySqlUsername", Value)
        End Set
    End Property

    Public Property RunOnce() As Boolean
        Get
            Return CType(GetMySetting("RunOnce", "False"), Boolean)
        End Get
        Set
            SetMySetting("RunOnce", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SCAdminPassword() As String
        Get
            Return GetMySetting("SC_AdminPassword")
        End Get
        Set
            SetMySetting("SC_AdminPassword", Value)
        End Set
    End Property

    Public Property SCEnable() As Boolean
        Get
            Return CType(GetMySetting("SC_Enable", "False"), Boolean)
        End Get
        Set
            SetMySetting("SC_Enable", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SCPassword() As String
        Get
            Return GetMySetting("SC_Password")
        End Get
        Set
            SetMySetting("SC_Password", Value)
        End Set
    End Property

    Public Property SCPortBase() As Integer
        Get
            Return CType(GetMySetting("SC_PortBase", "8100"), Integer)
        End Get
        Set
            SetMySetting("SC_PortBase", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SCPortBase1() As Integer
        Get
            Return CType(GetMySetting("SC_PortBase1", "8101"), Integer)
        End Get
        Set
            SetMySetting("SC_PortBase1", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ScriptEngine() As String
        Get
            Return GetMySetting("ScriptEngine", "YEngine")
        End Get
        Set
            SetMySetting("ScriptEngine", Value)
        End Set
    End Property

    Public Property SearchEnabled() As Boolean
        Get
            Return CType(GetMySetting("SearchEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("SearchEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SearchMigration() As Integer
        Get
            Return CType(GetMySetting("SearchMigration", "0".ToUpperInvariant), Integer)
        End Get
        Set
            SetMySetting("SearchMigration", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property Sequential() As Boolean
        Get
            Return CType(GetMySetting("Sequential", "False"), Boolean)
        End Get
        Set
            SetMySetting("Sequential", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ServerType() As String
        Get
            Return GetMySetting("ServerType", "Robust")
        End Get
        Set
            SetMySetting("ServerType", Value)
        End Set
    End Property

    'ShowConsoleStats
    Public Property ShowConsoleStats() As String
        Get
            Return GetMySetting("ShowConsoleStats", "True")
        End Get
        Set
            SetMySetting("ShowConsoleStats", Value)
        End Set
    End Property

    Public Property ShowToForeignUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToForeignUsers", "False"), Boolean)
        End Get
        Set
            SetMySetting("ShowToForeignUsers", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property ShowToLocalUsers() As Boolean
        Get
            Return CType(GetMySetting("ShowToLocalUsers", "False"), Boolean)
        End Get
        Set
            SetMySetting("ShowToLocalUsers", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SimName() As String
        Get
            Return GetMySetting("SimName", DreamGrid)
        End Get
        Set
            SetMySetting("SimName", Value)
        End Set
    End Property

    Public Property SizeX() As String
        Get
            Return GetMySetting("SizeX", "256")
        End Get
        Set
            SetMySetting("SizeX", Value)
        End Set
    End Property

    Public Property SizeY() As String
        Get
            Return GetMySetting("SizeY", "256")
        End Get
        Set
            SetMySetting("SizeY", Value)
        End Set
    End Property

    Public Property SkipUpdateCheck() As String
        Get
            Return GetMySetting("SkipUpdateCheck", "0")
        End Get
        Set
            SetMySetting("SkipUpdateCheck", CStr(Value))
        End Set
    End Property

    Public Property SmartStart() As Boolean
        Get
            Return CType(GetMySetting("SmartStart", "False"), Boolean)
        End Get
        Set
            SetMySetting("SmartStart", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set

    End Property

    Public Property SmtpHost() As String
        Get
            Return GetMySetting("SmtpHost", "smtp.gmail.com")
        End Get
        Set
            SetMySetting("SmtpHost", Value)
        End Set
    End Property

    Public Property SmtpPassword() As String
        Get
            Return GetMySetting("SmtpPassword", "Some Password")
        End Get
        Set
            SetMySetting("SmtpPassword", Value)
        End Set
    End Property

    Public Property SmtpPort() As Integer
        Get
            Return CInt("0" & GetMySetting("SmtpPort", "587"))
        End Get
        Set
            SetMySetting("SmtpPort", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SmtPropUserName() As String
        Get
            Return GetMySetting("SmtPropUserName", "LoginName@gmail.com")
        End Get
        Set
            SetMySetting("SmtPropUserName", Value)
        End Set
    End Property

    Public Property SplashPage() As String
        Get
            Return GetMySetting("SplashPage", FormSetup.PropDomain & "/Outworldz_installer/Welcome.htm")
        End Get
        Set
            SetMySetting("SplashPage", Value)
        End Set
    End Property

    Public Property Suitcase() As Boolean
        Get
            Return CType(GetMySetting("Suitcase", "True"), Boolean)
        End Get
        Set
            SetMySetting("Suitcase", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property SupportViewerObjectsCache() As Boolean
        Get
            Return CType(GetMySetting("SupportViewerObjectsCache", "True"), Boolean)
        End Get
        Set
            SetMySetting("SupportViewerObjectsCache", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property Theme() As String
        Get
            Return GetMySetting("Theme", "") ' no default, so we copy it.
        End Get
        Set
            SetMySetting("Theme", Value)
        End Set
    End Property

    Public Property TideEnabled() As Boolean
        Get
            Return CType(GetMySetting("TideEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("TideEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property TideHighLevel() As Single
        Get
            Return CSng(GetMySetting("TideHighLevel", "20"))
        End Get
        Set
            SetMySetting("TideHighLevel", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property TideInfoChannel() As Integer
        Get
            Return CInt("0" & GetMySetting("TideInfoChannel", "5555"))
        End Get
        Set
            SetMySetting("TideInfoChannel", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    ' Tides
    Public Property TideInfoDebug() As Boolean
        Get
            Return CType(GetMySetting("TideInfoDebug", "False"), Boolean)
        End Get
        Set
            SetMySetting("TideInfoDebug", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property TideLevelChannel() As Integer
        Get
            Return CInt("0" & GetMySetting("TideLevelChannel", "5556"))
        End Get
        Set
            SetMySetting("TideLevelChannel", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property TideLowLevel() As Single
        Get
            Return CInt("0" & GetMySetting("TideLowLevel", "17"))
        End Get
        Set
            SetMySetting("TideLowLevel", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property TOSEnabled() As Boolean
        Get
            Return CType(GetMySetting("TOSEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("TOSEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property UPnpDiag() As Boolean
        Get
            Return CType(GetMySetting("UPnpDiag", "False"), Boolean)
        End Get
        Set
            SetMySetting("UPnpDiag", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property UPnPEnabled() As Boolean
        Get
            Return CType(GetMySetting("UPnPEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("UPnPEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property VivoxEnabled() As Boolean
        Get
            Return CType(GetMySetting("VivoxEnabled", "False"), Boolean)
        End Get
        Set
            SetMySetting("VivoxEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

    Public Property VivoxPassword() As String
        Get
            Return GetMySetting("Vivox_password", "Passord")
        End Get
        Set
            SetMySetting("Vivox_password", Value)
        End Set
    End Property

    Public Property VivoxUserName() As String
        Get
            Return GetMySetting("Vivox_username", "User Name")
        End Get
        Set
            SetMySetting("Vivox_username", Value)
        End Set
    End Property

    Public Property WelcomeMessage() As String
        Get
            Return GetMySetting("WelcomeMessage", "Welcome to " & SimName() & ", <USERNAME>")
        End Get
        Set
            SetMySetting("WelcomeMessage", Value)
        End Set

    End Property

    Public Property WelcomeRegion() As String
        Get
            Return GetMySetting("WelcomeRegion", "Welcome")
        End Get
        Set
            SetMySetting("WelcomeRegion", Value)
        End Set
    End Property

    Public Property WifiEnabled() As Boolean
        Get
            Return CType(GetMySetting("WifiEnabled", "True"), Boolean)
        End Get
        Set
            SetMySetting("WifiEnabled", Convert.ToString(Value, Globalization.CultureInfo.InvariantCulture))
        End Set
    End Property

#End Region

#Region "Grep"

    ''' <summary>
    ''' Replaces .config file XML with log level and path info
    ''' </summary>
    ''' <param name="INI">Path to file</param>
    ''' <param name="bar">OSIM_LOGPATH path to log file in regions folder</param>
    ''' <param name="baz">OSIM_LOGLEVEL DEBUG, INFO, ALL, etc</param>
#Disable Warning CA1822 ' Mark members as static

    Public Sub Grep(INI As String, bar As String, baz As String)
#Enable Warning CA1822 ' Mark members as static

        If INI Is Nothing Then Return
        FileStuff.DeleteFile(INI)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(INI, False)
        Using Reader As New StreamReader(INI & ".proto", System.Text.Encoding.UTF8)
            While Not Reader.EndOfStream
                Dim line As String = Reader.ReadLine
                line = line.Replace("OSIM_LOGPATH", bar)
                line = line.Replace("OSIM_LOGLEVEL", baz)
                file.WriteLine(line)
            End While
        End Using
        file.Close()

    End Sub

#End Region

#Region "Apache"

    ' reader ApacheStrings
    Public Sub LoadLiteralIni(ini As String)

        Apachein.Clear()
        Using Reader As New StreamReader(ini, System.Text.Encoding.UTF8)
            While Reader.EndOfStream = False
                Apachein.Add(Reader.ReadLine())
            End While
        End Using

    End Sub

    'writer of ApacheStrings
    Public Sub SaveLiteralIni(ini As String, name As String)

        ' make a backup

        FileStuff.DeleteFile(ini & ".bak")

        Try
            My.Computer.FileSystem.RenameFile(ini, name & ".bak")
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        FileStuff.DeleteFile(ini)

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(ini, True)
        For Each Item As String In Apachein
            file.WriteLine(Item)
        Next
        file.Close()

    End Sub

    Public Sub SetLiteralIni(Name As String, value As String)

        Apacheout.Clear()
        Dim found As Boolean = False
        For Each Item As String In Apachein
            If Item.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase) Then

                Apacheout.Add(value)
                found = True
            Else
                Apacheout.Add(Item)
            End If
        Next
        If Not found Then
            Diagnostics.Debug.Print("Error: Did not find " & Name & " to set value of " & value)
        End If
        Apachein.Clear()
        For Each item In Apacheout
            Apachein.Add(item)
        Next
    End Sub

#End Region

#Region "Robust"

    Public Function RegionDBConnection() As String

        Return """" _
        & "Data Source=" & RegionServer _
        & ";Database=" & RegionDBName _
        & ";Port=" & CStr(MySqlRegionDBPort) _
        & ";User ID=" & RegionDBUsername _
        & ";Password=" & RegionDbPassword _
        & ";Old Guids=true;Allow Zero Datetime=true" _
        & ";Connect Timeout=28800;Command Timeout=28800;" _
        & """"

    End Function

    Public Function RegionMySqlConnection() As String

        Return "server=" & RegionServer _
        & ";database=" & RegionDBName _
        & ";port=" & CStr(MySqlRegionDBPort) _
        & ";user=" & RegionDBUsername _
        & ";password=" & RegionDbPassword

    End Function

    Public Function RobustDBConnection() As String

        Return """" _
            & "Data Source=" & RobustServer _
            & ";Database=" & RobustDataBaseName _
            & ";Port=" & CStr(MySqlRobustDBPort) _
            & ";User ID=" & RobustUsername _
            & ";Password=" & RobustPassword _
            & ";Old Guids=true;Allow Zero Datetime=true" _
            & ";Connect Timeout=28800;Command Timeout=28800;" _
            & """"

    End Function

    Public Function RobustMysqlConnection() As String

        Return "server=" & RobustServer _
            & ";database=" & RobustDataBaseName _
            & ";port=" & CStr(MySqlRobustDBPort) _
            & ";user=" & RobustUsername _
            & ";password=" & RobustPassword _
            & ";Old Guids=true;Allow Zero Datetime=true"

    End Function

#End Region

End Class
