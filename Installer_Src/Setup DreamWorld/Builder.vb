#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Module Build

    Public NameList As New List(Of String)
    Public Terrains As New List(Of String)
    Public TreeList As New List(Of String)
    Private _ctr As Integer

#Region "Land"

    Public Sub GenLand(RegionUUID As String)

        If Not RPC_Region_Command(RegionUUID, $"change region {PropRegionClass.RegionName(RegionUUID)}") Then Return
        If Settings.TerrainType = "Flat" Then
            If Not RPC_Region_Command(RegionUUID, $"terrain fill {Settings.FlatLandLevel}") Then BreakPoint.Show("No RPC")
        ElseIf Settings.TerrainType = "Water" Then
            If Not RPC_Region_Command(RegionUUID, "terrain fill {Settings.FlatLandLevel}") Then BreakPoint.Show("No RPC")
        ElseIf Settings.TerrainType = "Random" Then
            Dim r = Between(Terrains.Count - 1, 0)
            Dim Type As String = Terrains(r)

            Type = Type.Replace(".png", ".r32")
            If Not RPC_Region_Command(RegionUUID, $"terrain load {Type}") Then BreakPoint.Show("No RPC")

            Dim Fortytwo = Between(5, 1)
            Select Case Fortytwo
                Case 1
                    If Not RPC_Region_Command(RegionUUID, "terrain flip x") Then BreakPoint.Show("No RPC")
                Case 2
                    If Not RPC_Region_Command(RegionUUID, "terrain flip y") Then BreakPoint.Show("No RPC")
                Case 3
                    If Not RPC_Region_Command(RegionUUID, "terrain flip x") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, "terrain flip y") Then BreakPoint.Show("No RPC")
            End Select

        ElseIf Settings.TerrainType = "AI" Then

            Dim min = Between(40, 22)
            Dim taper = Between(135, 0)

            Dim r = Between(6, 0)

            Select Case r
                Case 1
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify fill {Settings.LandStrength} -taper={taper}") Then BreakPoint.Show("No RPC")
                Case 2
                    If Not RPC_Region_Command(RegionUUID, "terrain fill 21") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify noise 1 0") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify noise 1 0") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify noise 1 0") Then BreakPoint.Show("No RPC")
                Case 3
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify min {Between(40, 20)} -rec=128,128,120 -taper=-{taper}") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify min {Between(40, 20)} -rec=64,64,120 -taper=-{taper}") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify min {Between(40, 20)} -rec=64,64,32 -taper=-{taper}") Then BreakPoint.Show("No RPC")
                Case 4
                    If Not RPC_Region_Command(RegionUUID, "terrain fill 0") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify min {min} -ell=128,128,120 -taper=-{Between(55, 0)}") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify noise 1 0") Then BreakPoint.Show("No RPC")
                Case 5
                    If Not RPC_Region_Command(RegionUUID, "terrain fill 0") Then BreakPoint.Show("No RPC")
                    If Not RPC_Region_Command(RegionUUID, $"terrain modify min {min} -ell=128,128,120 -taper=-{taper}") Then BreakPoint.Show("No RPC")
            End Select

        End If

        Modifiers(RegionUUID)

        'force update - Force the region to send all clients updates about all objects.
        If Not RPC_Region_Command(RegionUUID, "force update") Then BreakPoint.Show("No RPC")

    End Sub

    Public Sub SurroundingLandMaker(RegionUUID As String)

        ' Make a map of occupied areas
        Dim RegionXY As New List(Of String)

        For Each UUID In PropRegionClass.RegionUuids
            Dim SimSize As Integer = CInt(PropRegionClass.SizeX(RegionUUID) / 256)
            For Xstep = 0 To SimSize - 1
                For Ystep = 0 To SimSize - 1
                    RegionXY.Add($"{PropRegionClass.CoordX(UUID) + Xstep}:{PropRegionClass.CoordY(UUID) + Ystep}")
                Next
            Next
        Next

        Dim Xloc = PropRegionClass.CoordX(RegionUUID)
        Dim Yloc = PropRegionClass.CoordY(RegionUUID)
        Dim CenterSize As Integer = CInt(PropRegionClass.SizeX(RegionUUID) / 256)
        Dim xy As New List(Of String)

        ' draw a square around the new sim
        Dim X1 = Xloc - Settings.Skirtsize
        Dim X2 = Xloc + Settings.Skirtsize - 1 + CenterSize
        Dim Y1 = Yloc - Settings.Skirtsize
        Dim Y2 = Yloc + Settings.Skirtsize - 1 + CenterSize

        For XPos As Integer = X1 To X2 Step 1
            For Ypos As Integer = Y1 To Y2 Step 1
                xy.Add($"{XPos}:{Ypos}")
            Next
        Next

        Dim GroupName As String = ""
        Dim Simcount As Integer

        Dim l As New List(Of String)
        For Each possible As String In xy
            If Not RegionXY.Contains(possible) Then
                Dim parts As String() = possible.Split(New Char() {":"c}) ' split at the space
                Dim nX = CInt(CStr(parts(0).Trim))
                Dim nY = CInt(CStr(parts(1).Trim))

                If GroupName.Length = 0 Then
                    GroupName = FantasyName()
                End If
                MakeTempRegion(GroupName, nX, nY)
                Simcount += 1
            End If
        Next

        If Simcount > 0 Then
            Landscaper(GroupName)
        End If

    End Sub

#End Region

#Region "Landscaper"

    Public Sub GenTrees(RegionUUID As String)

        Dim UseTree As New List(Of String)
        For Each t As String In TreeList
            If GetSetting(t) Then
                UseTree.Add(t)
                Application.DoEvents()
            End If
        Next

        If Not RPC_Region_Command(RegionUUID, $"change region {PropRegionClass.RegionName(RegionUUID)}") Then Return
        If Not RPC_Region_Command(RegionUUID, "tree active true") Then Return

        If Settings.DeleteTreesFirst Then
            For Each TT As String In TreeList
                If Not RPC_Region_Command(RegionUUID, $"tree remove {TT}") Then Return
            Next
        End If

        If UseTree.Count = 0 Then
            If Not RPC_Region_Command(RegionUUID, "tree active false") Then Return
            Return
        End If

        Debug.Print($"Planting {PropRegionClass.RegionName(RegionUUID)}")

        For Each NewType In UseTree
            If Not RPC_Region_Command(RegionUUID, $"tree load Trees/{NewType}.xml") Then Return
            If Not RPC_Region_Command(RegionUUID, $"tree plant {NewType}") Then Return
            If Not RPC_Region_Command(RegionUUID, $"tree rate 1000") Then Return
            If Not RPC_Region_Command(RegionUUID, "tree active true") Then Return
            Sleep(1500)
            If Not RPC_Region_Command(RegionUUID, $"tree freeze {NewType} true") Then Return
            If Not RPC_Region_Command(RegionUUID, "force update") Then BreakPoint.Show("No RPC")
        Next

        If Not RPC_Region_Command(RegionUUID, "tree active false") Then Return
        Application.DoEvents()
        If Not RPC_Region_Command(RegionUUID, $"tree statistics") Then Return
        'force update - Force the region to send all clients updates about all objects.
        If Not RPC_Region_Command(RegionUUID, "force update") Then BreakPoint.Show("No RPC")

    End Sub

    Public Sub InitTrees()

        Try
            Dim TreeDirectoryInfo As New System.IO.DirectoryInfo(IO.Path.Combine(Settings.OpensimBinPath, "Trees"))
            For Each fileSystemInfo In TreeDirectoryInfo.GetFileSystemInfos
                Dim n = fileSystemInfo.Name
                If n.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase) Then
                    Dim part = IO.Path.GetFileName(n)
                    part = part.Replace(".xml", "")
                    TreeList.Add(part)
                End If
            Next
            Debug.Print($"{TreeList.Count} Trees")
        Catch
        End Try

    End Sub

    Private Function GetSetting(Box As String) As Boolean
        Dim b As Boolean
        Select Case Settings.GetMySetting(Box)
            Case ""
                b = True
            Case "True"
                b = True
            Case "False"
                b = False
            Case Else
                b = False
        End Select

        Return b

    End Function

    Private Sub Landscaper(GroupName As String)

        Dim UUIDs = PropRegionClass.RegionUuidListByName(GroupName)

        For Each RegionUUID As String In UUIDs

            ReBoot(RegionUUID)

            ' this mess is need to get the region to join an estate
            ' There only other way is to append to a file and start Robust again.
            Dim Group = PropRegionClass.GroupName(RegionUUID)
            While True
                For Each UUID In PropRegionClass.RegionUuidListByName(Group)
                    If EstateName(RegionUUID).Length = 0 Then
                        SetEstate(RegionUUID, 1999)
                    End If
                Next
            End While

            WaitForBooted(RegionUUID)

            'force update - Force the region to send all clients updates about all objects.
            If Not RPC_Region_Command(RegionUUID, "force update") Then BreakPoint.Show("No RPC")

            For Each UUID In PropRegionClass.RegionUuidListByName(Group)
                RPC_Region_Command(RegionUUID, $"estate link region 1999 {RegionUUID} ")
                Try
                    GenLand(RegionUUID)
                    GenTrees(RegionUUID)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try

            Next
        Next

    End Sub

#End Region

#Region "JSON"

    Public Sub InitLand()

        Try
            Dim TerrainDirectoryInfo As New System.IO.DirectoryInfo(IO.Path.Combine(Settings.OpensimBinPath, "Terrains"))
            Dim fileSystemInfo As System.IO.FileSystemInfo
            For Each fileSystemInfo In TerrainDirectoryInfo.GetFileSystemInfos
                Dim n = fileSystemInfo.Name
                If n.EndsWith(".r32", StringComparison.InvariantCultureIgnoreCase) Then
                    Terrains.Add(fileSystemInfo.FullName)
                End If
            Next
            Debug.Print($"{Terrains.Count} Terrains")

            Dim l As New List(Of String)
            Dim names = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\RegionNames.txt")
            If System.IO.File.Exists(names) Then
                Using reader As StreamReader = System.IO.File.OpenText(names)
                    While reader.Peek <> -1
                        l.Add(reader.ReadLine.Trim)
                    End While
                End Using
            End If
            NameList = l
            Return
        Catch
        End Try

    End Sub

    Private Function FantasyName() As String

        Try
            Dim Existing As New List(Of String)
            For Each UUID In PropRegionClass.RegionUuids
                If PropRegionClass.RegionName(UUID).Length > 0 Then
                    Existing.Add(PropRegionClass.RegionName(UUID))
                End If
            Next
            If NameList.Count = 0 Then
                _ctr += 1
                Return "SimSurround " & CStr(_ctr)
            End If
            While True
                Dim index = RandomNumber.Between(NameList.Count - 1, 1)
                Dim proposedName = NameList.Item(index)
                If Not Existing.Contains(proposedName) Then
                    Return proposedName
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical Or MsgBoxStyle.MsgBoxSetForeground)
        End Try

        Return ""

    End Function

    Private Function MakeTempRegion(Group As String, X As Integer, Y As Integer) As String

        Dim shortname = FantasyName()

        If IO.File.Exists(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")) Then
            Return ""
        End If

        'kill it
        DeRegisterPosition(X, Y)

        ' build it
        Dim RegionUUID = PropRegionClass.CreateRegion(shortname, "")

        PropRegionClass.CrashCounter(RegionUUID) = 0
        PropRegionClass.CoordX(RegionUUID) = X
        PropRegionClass.CoordY(RegionUUID) = Y
        PropRegionClass.SmartStart(RegionUUID) = "True"
        PropRegionClass.Teleport(RegionUUID) = "True"
        PropRegionClass.SizeX(RegionUUID) = 256
        PropRegionClass.SizeY(RegionUUID) = 256
        PropRegionClass.GroupName(RegionUUID) = Group

        PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region\{shortname}.ini")
        PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}\Region")
        PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{Group}")

        Dim port = PropRegionClass.LargestPort + 1
        PropRegionClass.GroupPort(RegionUUID) = port
        PropRegionClass.RegionPort(RegionUUID) = port

        '   TODO: delete from disk, use HTTP
        PropRegionClass.WriteRegionObject(Group, shortname)

        Return RegionUUID

    End Function

    Private Sub Modifiers(RegionUUID As String)

        If Settings.LandSmooth Then
            If Not RPC_Region_Command(RegionUUID, $"terrain modify smooth {Settings.LandSmoothValue} -taper={Settings.LandTaper}") Then Return
            If Not RPC_Region_Command(RegionUUID, $"terrain modify smooth {Settings.LandSmoothValue} -taper={Settings.LandTaper}") Then Return
        End If

        If Settings.LandNoise Then
            If Not RPC_Region_Command(RegionUUID, "terrain modify noise 1") Then Return
            If Not RPC_Region_Command(RegionUUID, "terrain modify noise 1") Then Return
        End If

    End Sub

#End Region

End Module
