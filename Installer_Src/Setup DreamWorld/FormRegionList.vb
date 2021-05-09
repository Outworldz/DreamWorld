#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions

Public Class FormRegionlist

#Region "Declarations"

#Disable Warning CA2213
    Private ReadOnly colsize As New ScreenPos("Region List")
    Private _ImageListSmall As New ImageList
#Enable Warning CA2213
    Private initted As Boolean
    Private ItemsAreChecked As Boolean
    Private TheView As Integer = ViewType.Details
    Private ViewNotBusy As Boolean

    Private Enum ViewType As Integer
        Icons = 1
        Details = 2
        Avatars = 3
        Users = 4
    End Enum

#End Region

#Region "Const"

    '// Constants
    Const HWND_TOP As Integer = 0

    'Const HWND_TOPMOST As Integer = -1
    'Const HWND_NO_TOPMOST As Integer = -2
    Const NOMOVE As Long = &H2

    Const NOSIZE As Long = &H1
    Dim _order As SortOrder
    Private _SortColumn As Integer

#End Region

#Region "Properties"

    Public Property ImageListSmall As ImageList
        Get
            Return _ImageListSmall
        End Get
        Set(value As ImageList)
            _ImageListSmall = value
        End Set
    End Property

    Public Property ItemsAreChecked1 As Boolean
        Get
            Return ItemsAreChecked
        End Get
        Set(value As Boolean)
            ItemsAreChecked = value
        End Set
    End Property

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    Public Property TheView1 As Integer
        Get
            Return TheView
        End Get
        Set(value As Integer)
            TheView = value
        End Set
    End Property

    Public Property ViewBusy As Boolean
        Get
            Return ViewNotBusy
        End Get
        Set(value As Boolean)
            ViewNotBusy = value
        End Set
    End Property

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)

        ScreenPosition.SaveXY(Me.Left, Me.Top)
        ScreenPosition.SaveHW(Me.Height, Me.Width)

    End Sub

    Private Sub SetScreen(View As Integer)

        Me.Show()
        Try
            ScreenPosition = New ScreenPos(MyBase.Name & "_View_" & CStr(View))
            AddHandler ResizeEnd, Handler
            Dim xy As List(Of Integer) = ScreenPosition.GetXY()
            Me.Left = xy.Item(0)
            Me.Top = xy.Item(1)
            Dim hw As List(Of Integer) = ScreenPosition.GetHW()
            '1106, 460
            If hw.Item(0) = 0 Then
                Me.Height = 460
            Else
                Me.Height = hw.Item(0)
            End If
            If hw.Item(1) = 0 Then
                Me.Width = 1800
            Else
                Me.Width = hw.Item(1)
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Me.Height = 460
            Me.Width = 1800
            Me.Left = 100
            Me.Top = 100
        End Try

    End Sub

#End Region

#Region "Layout"

    Private Sub Avatarview_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles AvatarView.ColumnWidthChanged

        Dim w = AvatarView.Columns(e.ColumnIndex).Width
        Dim name = AvatarView.Columns(e.ColumnIndex).Name
        If name.Length = 0 Or w = 0 Then Return

        ScreenPosition.PutSize(name, w)
        ScreenPosition.SaveFormSettings()

    End Sub

    Private Sub ListView1_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles ListView1.ColumnWidthChanged

        Dim w = ListView1.Columns(e.ColumnIndex).Width
        Dim name = ListView1.Columns(e.ColumnIndex).Name
        If name.Length = 0 Or w = 0 Then Return

        ScreenPosition.PutSize(name, w)
        ScreenPosition.SaveFormSettings()

    End Sub

    Private Sub RegionList_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout

        Dim X = Me.Width - 45
        Dim Y = Me.Height - 150
        ListView1.Size = New System.Drawing.Size(X, Y)
        AvatarView.Size = New System.Drawing.Size(X, Y)
        UserView.Size = New System.Drawing.Size(X, Y)
        IconView.Size = New System.Drawing.Size(X, Y)

    End Sub

    Private Sub Userview_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles UserView.ColumnWidthChanged

        Dim w = UserView.Columns(e.ColumnIndex).Width
        Dim name = UserView.Columns(e.ColumnIndex).Name
        If name.Length = 0 Or w = 0 Then Return

        ScreenPosition.PutSize(name, w)
        ScreenPosition.SaveFormSettings()

    End Sub

#End Region

#Region "Public Enums"

    ' icons image list layout
    Enum DGICON
        bootingup = 0
        shuttingdown = 1
        up = 2
        disabled = 3
        stopped = 4
        recyclingdown = 5
        recyclingup = 6
        warning = 7
        user1 = 8
        user2 = 9
        SmartStart = 10
        Home = 11
        HomeOffline = 12
        Pending = 13
        Suspended = 14
        ErrorIcon = 15
        NoLogin = 16
        NoError = 17

    End Enum

#End Region

#Region "Loader"

    Private Shared Sub DoubleBuff(ByVal control As Control, ByVal enable As Boolean)
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Settings.RegionListVisible = False

        _ImageListSmall.Dispose()
        colsize.Dispose()

    End Sub

    Private Sub LoadForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ImageListSmall.ImageSize = New Drawing.Size(16, 16)
        IconView.SmallImageList = ImageListSmall
        ListView1.SmallImageList = ImageListSmall
        AddRegionButton.Text = Global.Outworldz.My.Resources.Add_word
        AllNone.Text = Global.Outworldz.My.Resources.AllNone_word
        AvatarsButton.Text = Global.Outworldz.My.Resources.Avatars_word
        DetailsButton.Text = Global.Outworldz.My.Resources.Details_word
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        IconsButton.Text = Global.Outworldz.My.Resources.Icons_word
        ImportButton.Text = Global.Outworldz.My.Resources.Import_word
        RefreshButton.Text = Global.Outworldz.My.Resources.Refresh_word
        RestartButton.Text = Global.Outworldz.My.Resources.Restart_word
        RunAllButton.Text = Global.Outworldz.My.Resources.Run_All_word
        StopAllButton.Text = Global.Outworldz.My.Resources.Stop_All_word
        KOT.Text = Global.Outworldz.My.Resources.Window_Word
        Users.Text = Global.Outworldz.My.Resources.Users_word
        OnTopToolStripMenuItem.Text = Global.Outworldz.My.Resources.On_Top
        FloatToolStripMenuItem.Text = Global.Outworldz.My.Resources.Float

        ToolTip1.SetToolTip(AddRegionButton, Global.Outworldz.My.Resources.Add_Region_word)
        ToolTip1.SetToolTip(AllNone, Global.Outworldz.My.Resources.Selectallnone)
        ToolTip1.SetToolTip(AvatarsButton, Global.Outworldz.My.Resources.ListAvatars)
        ToolTip1.SetToolTip(DetailsButton, Global.Outworldz.My.Resources.View_Details)
        ToolTip1.SetToolTip(IconsButton, Global.Outworldz.My.Resources.View_as_Icons)
        ToolTip1.SetToolTip(ImportButton, Global.Outworldz.My.Resources.Importtext)
        ToolTip1.SetToolTip(ListView1, Global.Outworldz.My.Resources.ClickStartStoptxt)
        ToolTip1.SetToolTip(RefreshButton, Global.Outworldz.My.Resources.Reload)
        ToolTip1.SetToolTip(RestartButton, Global.Outworldz.My.Resources.Restart_All_Checked)
        ToolTip1.SetToolTip(RunAllButton, Global.Outworldz.My.Resources.StartAll)
        ToolTip1.SetToolTip(StopAllButton, Global.Outworldz.My.Resources.Stopsall)
        ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Row

        ViewBusy = True

        AllNone.Checked = True

        ListView1.Visible = False
        DoubleBuff(ListView1, True)
        DoubleBuff(IconView, True)
        ListView1.LabelWrap = True
        ListView1.AutoArrange = True

        Settings.RegionListVisible = True

        Me.Name = "Region List"

        Me.Text = Global.Outworldz.My.Resources.Region_List

        AvatarView.CheckBoxes = False
        AvatarView.TabIndex = 0
        AvatarView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)
        AvatarView.GridLines = False

        ListView1.TabIndex = 0
        ListView1.CheckBoxes = True
        ListView1.View = View.Details
        ListView1.LabelEdit = True
        ListView1.AllowColumnReorder = True
        ListView1.FullRowSelect = True
        ListView1.GridLines = True
        ListView1.AllowColumnReorder = True
        ListView1.Sorting = SortOrder.Ascending
        AllNone.Visible = True
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        IconView.TabIndex = 0
        IconView.View = View.SmallIcon
        IconView.CheckBoxes = False
        IconView.FullRowSelect = False
        IconView.GridLines = True
        IconView.AllowColumnReorder = False
        IconView.Sorting = SortOrder.Ascending
        AllNone.Visible = False
        IconView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        UserView.TabIndex = 0
        UserView.View = View.Details
        UserView.CheckBoxes = True
        UserView.FullRowSelect = True
        UserView.AllowColumnReorder = True
        UserView.GridLines = True
        UserView.AllowColumnReorder = True
        UserView.Sorting = SortOrder.Ascending
        AllNone.Visible = True
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None)

        If Settings.KeepOnTop Then
            Me.TopMost = True
            OnTopToolStripMenuItem.Checked = True
            FloatToolStripMenuItem.Checked = False
        Else
            Me.TopMost = False
            OnTopToolStripMenuItem.Checked = False
            FloatToolStripMenuItem.Checked = True
        End If

        Dim ctr = 0

        'icons
        IconView.Columns.Add(New ColumnHeader)
        IconView.Columns.Add(My.Resources.Enable_word, colsize.ColumnWidth("Icon" & ctr & "_" & CStr(ViewType.Icons), 400), HorizontalAlignment.Left)
        IconView.Columns(ctr).Name = "Icon" & ctr & "_" & CStr(ViewType.Icons)

        'details
        ListView1.Columns.Add(My.Resources.Enable_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 120), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.DOS_Box_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 120), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Agents_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Status_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 120), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.RAM_Word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.CPU_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add("X".ToUpperInvariant, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add("Y".ToUpperInvariant, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Size_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 40), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Estate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 100), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add("Prims", colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Left)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Region_Ports_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Group_Ports_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 50), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ' optional
        ListView1.Columns.Add(My.Resources.Scripts_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Maps_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Physics_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 120), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Birds_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details) & "_" & CStr(TheView), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Tides_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 60), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Teleport_word, colsize.ColumnWidth("Column" & ctr, 65), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Smart_Start_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Allow_Or_Disallow_Gods_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 75), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Owner_God, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 75), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Manager_God_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.No_Autobackup, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 90), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Publicity_Word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Script_Rate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)
        ctr += 1
        ListView1.Columns.Add(My.Resources.Frame_Rate_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 80), HorizontalAlignment.Center)
        ListView1.Columns(ctr).Name = "Column" & ctr & "_" & CStr(ViewType.Details)

        'Avatars
        ctr = 0
        AvatarView.Columns.Add(My.Resources.Agents_word, colsize.ColumnWidth("Avatar" & ctr & "_" & CStr(ViewType.Details), 150), HorizontalAlignment.Center)
        AvatarView.Columns(ctr).Name = "Agent" & ctr & "_" & CStr(ViewType.Avatars)
        ctr += 1
        AvatarView.Columns.Add(My.Resources.Region_word, colsize.ColumnWidth("Avatar" & ctr & "_" & CStr(ViewType.Details), 150), HorizontalAlignment.Center)
        AvatarView.Columns(ctr).Name = "Agent" & ctr & "_" & CStr(ViewType.Avatars)
        ctr += 1
        AvatarView.Columns.Add(My.Resources.Type_word, colsize.ColumnWidth("Column" & ctr & "_" & CStr(ViewType.Details), 150), HorizontalAlignment.Center)
        AvatarView.Columns(ctr).Name = "Agent" & ctr & "_" & CStr(ViewType.Avatars)

        'Users
        ctr = 0
        UserView.Columns.Add(My.Resources.Avatar_Name_word, colsize.ColumnWidth("User" & ctr & "_" & CStr(ViewType.Users), 250), HorizontalAlignment.Left)
        UserView.Columns(ctr).Name = "User" & ctr & "_" & CStr(ViewType.Users)
        ctr += 1
        UserView.Columns.Add(My.Resources.Email_word, colsize.ColumnWidth("User" & ctr & "_" & CStr(ViewType.Users), 250), HorizontalAlignment.Left)
        ctr += 1
        UserView.Columns.Add(My.Resources.Items_word, colsize.ColumnWidth("Items" & ctr & "_" & CStr(ViewType.Users), 90), HorizontalAlignment.Left)

        ' Connect the ListView.ColumnClick event to the ColumnClick event handler.
        AddHandler ListView1.ColumnClick, AddressOf ColumnClick
        AddHandler IconView.ColumnClick, AddressOf ColumnClick
        AddHandler UserView.ColumnClick, AddressOf ColumnClick

        ' index to display icons
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up2", Globalization.CultureInfo.InvariantCulture))   ' 0 booting up
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down2", Globalization.CultureInfo.InvariantCulture)) ' 1 shutting down
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("check2", Globalization.CultureInfo.InvariantCulture)) ' 2 okay, up
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_cross", Globalization.CultureInfo.InvariantCulture)) ' 3 disabled
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("cube_blue", Globalization.CultureInfo.InvariantCulture))  ' 4 enabled, stopped
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_down", Globalization.CultureInfo.InvariantCulture))  ' 5 Recycling down
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("navigate_up", Globalization.CultureInfo.InvariantCulture))  ' 6 Recycling Up
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("warning", Globalization.CultureInfo.InvariantCulture))  ' 7 Unknown
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("user2", Globalization.CultureInfo.InvariantCulture))  ' 8 - 1 User
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("users1", Globalization.CultureInfo.InvariantCulture))  ' 9 - 2 user
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("media_pause", Globalization.CultureInfo.InvariantCulture))  ' 10 - 2 user
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("home", Globalization.CultureInfo.InvariantCulture))  '  11- home
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("home_02", Globalization.CultureInfo.InvariantCulture))  '  12- home _offline
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("refresh", Globalization.CultureInfo.InvariantCulture))  '  13- Pending
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("media_pause", Globalization.CultureInfo.InvariantCulture))  '  14- Suspended
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("package_error", Globalization.CultureInfo.InvariantCulture))  '  15- Error
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("gear_stop", Globalization.CultureInfo.InvariantCulture))  '  16 - NoLogin
        ImageListSmall.Images.Add(My.Resources.ResourceManager.GetObject("redo", Globalization.CultureInfo.InvariantCulture))  '  17 - NOError

        If TheView1 = ViewType.Details Or TheView1 = ViewType.Icons Then
            Timer1.Interval = 250 ' check for Form1.PropUpdateView immediately
            Timer1.Start() 'Timer starts functioning
        End If

        ViewBusy = False

        ' Set the view to show whatever
        TheView1 = Settings.RegionListView()

        SetScreen(TheView1)

        Timer1.Start()
        LoadMyListView()
        initted = True

    End Sub

    Private Sub MyListView_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles ListView1.AfterLabelEdit

        Debug.Print(e.Label)

    End Sub

    Private Sub ShowTitle()

        Dim TotalSize As Double
        Dim RegionCount As Integer
        Dim TotalRegionCount As Integer
        For Each RegionUUID As String In PropRegionClass.RegionUuids
            TotalSize += PropRegionClass.SizeX(RegionUUID) / 256 * PropRegionClass.SizeY(RegionUUID) / 256
            If PropRegionClass.RegionEnabled(RegionUUID) Then RegionCount += 1
            TotalRegionCount += 1
        Next
        Me.Text = "Regions:  " & CStr(TotalRegionCount) & ".  Enabled: " & CStr(RegionCount) & ". Total Area: " & CStr(TotalSize) & " Regions"

    End Sub

#End Region

#Region "Timer"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If TheView1 = ViewType.Users Then
            Timer1.Stop()
            Return
        End If

        If PropUpdateView() Then ' force a refresh
            If ViewBusy = True Then
                Timer1.Interval = 5000 ' check for Form1.PropUpdateView later
                Return
            End If
            LoadMyListView()
            Timer1.Interval = 1000
        End If

    End Sub

#End Region

#Region "LoadListView"

    Private Shared Function GetStatus(RegionUUID As String, ByRef Num As Integer, ByRef Letter As String) As Integer

        Dim Status As Integer = PropRegionClass.Status(RegionUUID)

        If Not PropRegionClass.RegionEnabled(RegionUUID) Then
            Letter = "Disabled"
            If PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                Num = DGICON.HomeOffline
            Else
                Num = DGICON.disabled
            End If
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Stopped And PropRegionClass.SmartStart(RegionUUID) = "True" And Settings.SmartStart Then
            Letter = My.Resources.Waiting
            Num = DGICON.SmartStart
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Stopped And PropRegionClass.SmartStart(RegionUUID) = "True" And Not Settings.SmartStart Then
            Letter = My.Resources.Stopped_word
            Num = DGICON.stopped
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Stopped And PropRegionClass.SmartStart(RegionUUID) <> "True" Then
            Letter = My.Resources.Stopped_word
            Num = DGICON.stopped
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Error Then
            Letter = My.Resources.Error_word
            Num = DGICON.ErrorIcon
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Suspended Then
            Letter = My.Resources.Suspended_word
            Num = DGICON.Suspended
        ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingDown Then
            Letter = My.Resources.Recycling_Down_word
            Num = DGICON.recyclingdown
        ElseIf Status = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
            Letter = My.Resources.Recycling_Up_word
            Num = DGICON.recyclingup
        ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartPending Then
            Letter = My.Resources.Restart_Pending_word
            Num = DGICON.recyclingup
        ElseIf Status = RegionMaker.SIMSTATUSENUM.RetartingNow Then
            Letter = My.Resources.Restarting_Now_word
            Num = DGICON.recyclingup
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Resume Then
            Letter = "Restarting Now"
            Num = DGICON.recyclingup
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Booting Then
            Letter = My.Resources.Booting_word
            Num = DGICON.bootingup
        ElseIf Status = RegionMaker.SIMSTATUSENUM.NoLogin Then
            Letter = My.Resources.NoLogin_word
            Num = DGICON.NoLogin
        ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood Then
            Letter = My.Resources.Quitting_word
            Num = DGICON.shuttingdown
        ElseIf Status = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
            Letter = My.Resources.Stopping_word
            Num = DGICON.shuttingdown
        ElseIf Status = RegionMaker.SIMSTATUSENUM.RestartStage2 Then
            Letter = My.Resources.Pending_word
        ElseIf Status = RegionMaker.SIMSTATUSENUM.NoError Then
            Letter = My.Resources.Stopped_word
            Num = DGICON.NoError
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And PropRegionClass.AvatarCount(RegionUUID) = 1 Then
            Letter = My.Resources.Running_word
            Num = DGICON.user1
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted And PropRegionClass.AvatarCount(RegionUUID) > 1 Then
            Letter = CStr(PropRegionClass.AvatarCount(RegionUUID) & " " & My.Resources.Avatars_word)
            Num = DGICON.user2
        ElseIf Status = RegionMaker.SIMSTATUSENUM.Booted Then
            If PropRegionClass.RegionName(RegionUUID) = Settings.WelcomeRegion Then
                Num = DGICON.Home
                Letter = My.Resources.Home_word
            Else
                Letter = My.Resources.Running_word
                Num = DGICON.up
            End If
        Else
            Num = DGICON.warning ' warning
        End If

        Return Status

    End Function

    Private Sub LoadMyListView()

        BringToFront()

        Select Case TheView1
            Case ViewType.Avatars
                ShowAvatars()
            Case ViewType.Users
                ShowUsers()
            Case ViewType.Details
                ShowDetails()
            Case ViewType.Icons
                ShowIcons()
        End Select

    End Sub

    Private Sub ShowAvatars()
        Try
            Me.Text = ""
            ViewBusy = True
            AvatarView.Show()
            ListView1.Hide()
            UserView.Hide()
            IconView.Hide()

            AvatarView.BeginUpdate()
            AvatarView.Items.Clear()

            Dim Index = 0

            ' Create items and sub items for each item.
            Dim L As New Dictionary(Of String, String)

            If MysqlInterface.IsMySqlRunning() Then
                L = MysqlInterface.GetAgentList()
            End If

            For Each Agent In L
                Dim item1 As New ListViewItem(Agent.Key, Index)
                item1.SubItems.Add(Agent.Value)
                item1.SubItems.Add(My.Resources.Local)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
                Index += 1
            Next

            If L.Count = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.Local_Grid)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
                Index += 1
            End If

            ' Hypergrid
            '
            ' Create items and sub items for each item.
            Dim M As New Dictionary(Of String, String)
            If MysqlInterface.IsMySqlRunning() Then
                M = GetHGAgentList()
            End If

            For Each Agent In M
                If Agent.Value.Length > 0 Then
                    Dim item1 As New ListViewItem(Agent.Key, Index)
                    item1.SubItems.Add(Agent.Value)
                    item1.SubItems.Add(My.Resources.Hypergrid_word)
                    AvatarView.Items.AddRange(New ListViewItem() {item1})
                    Index += 1
                End If
            Next

            If Index = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.Hypergrid_word)
                AvatarView.Items.AddRange(New ListViewItem() {item1})
            End If

            For Each col In AvatarView.Columns
                Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
                    Dim w = colsize.ColumnWidth(CStr(col.name))
                    If w > 0 Then col.Width = w
                End Using
            Next

            AvatarView.EndUpdate()
            AvatarView.Show()
            AvatarView.Visible = True
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Log(My.Resources.Error_word, " RegionList " & ex.Message)
        Finally
            PropUpdateView() = False
            ViewBusy = False
        End Try

    End Sub

    Private Sub ShowDetails()

        ShowTitle()
        Users.Text = My.Resources.Users_word
        AllNone.Visible = True

        If ViewBusy = True Then
            Return
        End If

        ListView1.Show()
        ListView1.Visible = True
        IconView.Hide()
        AvatarView.Hide()
        UserView.Hide()

        ListView1.TabIndex = 0
        ViewBusy = True
        ListView1.BeginUpdate()
        ListView1.Items.Clear()

        Dim p As PerformanceCounter = Nothing

        Try
            For Each RegionUUID As String In PropRegionClass.RegionUuids
                ' Application.DoEvents() ' bad idea

                Dim Num As Integer = 0
                Dim Letter As String = ""
                Dim status = GetStatus(RegionUUID, Num, Letter)

                ' Create items and sub items for each item. Place a check mark next to the item.
                Dim item1 As New ListViewItem(PropRegionClass.RegionName(RegionUUID), Num) With
                    {
                        .Checked = PropRegionClass.RegionEnabled(RegionUUID)
                    }
                item1.SubItems.Add(PropRegionClass.GroupName(RegionUUID).ToString(Globalization.CultureInfo.CurrentCulture))
                item1.SubItems.Add(PropRegionClass.AvatarCount(RegionUUID).ToString(Globalization.CultureInfo.CurrentCulture))

                item1.SubItems.Add(Letter)
                Dim fmtXY = "00000" ' 65536
                Dim fmtRam = "0.0" ' 9999 MB
                ' RAM

                If status = RegionMaker.SIMSTATUSENUM.Booting _
                        Or status = RegionMaker.SIMSTATUSENUM.Booted _
                        Or status = RegionMaker.SIMSTATUSENUM.RecyclingUp _
                        Or status = RegionMaker.SIMSTATUSENUM.RecyclingDown _
                        Then

                    Try
                        Dim PID = PropRegionClass.ProcessID(RegionUUID)
                        Dim component1 As Process = Process.GetProcessById(PID)
                        Dim Memory As Double = (component1.WorkingSet64 / 1024) / 1024
                        item1.SubItems.Add(Memory.ToString("0.0", Globalization.CultureInfo.CurrentCulture))
                    Catch ex As Exception
                        item1.SubItems.Add("0")
                    End Try
                Else
                    item1.SubItems.Add("0")
                End If

                Dim cpupercent As Double = 0
                If Not status = RegionMaker.SIMSTATUSENUM.Stopped And Not status = RegionMaker.SIMSTATUSENUM.Error Then
                    Dim Groupname As String = PropRegionClass.GroupName(RegionUUID)
                    CPUValues.TryGetValue(Groupname, cpupercent)
                End If

                item1.SubItems.Add(CStr(cpupercent))
                Dim c As Color = SystemColors.ControlText
                If cpupercent > 1 Then
                    c = Color.Red
                End If

                item1.SubItems.Add(PropRegionClass.CoordX(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.CurrentCulture))
                item1.SubItems.Add(PropRegionClass.CoordY(RegionUUID).ToString(fmtXY, Globalization.CultureInfo.CurrentCulture))

                ' Size of region
                Dim s As Double = PropRegionClass.SizeX(RegionUUID) / 256
                Dim size As String = CStr(s) & "X" & CStr(s)
                item1.SubItems.Add(size)

                ' add estate name
                Dim Estate = "-".ToUpperInvariant
                If MysqlInterface.IsRunning() Then
                    Estate = MysqlInterface.EstateName(RegionUUID)
                End If
                item1.SubItems.Add(Estate)

                item1.SubItems.Add(GetPrimCount(RegionUUID).ToString("00000", Globalization.CultureInfo.CurrentCulture))

                item1.SubItems.Add(PropRegionClass.RegionPort(RegionUUID).ToString(Globalization.CultureInfo.CurrentCulture))
                item1.SubItems.Add(PropRegionClass.GroupPort(RegionUUID).ToString(Globalization.CultureInfo.CurrentCulture))

                'Scripts XEngine or YEngine
                Select Case PropRegionClass.ScriptEngine(RegionUUID)
                    Case "YEngine"
                        item1.SubItems.Add(My.Resources.YEngine_word)
                    Case "XEngine"
                        item1.SubItems.Add(My.Resources.XEngine_word)
                    Case Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                End Select

                'Map
                If PropRegionClass.MapType(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.MapType(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                ' physics
                Select Case PropRegionClass.Physics(RegionUUID)
                    Case ""
                        item1.SubItems.Add("-".ToUpperInvariant)
                    Case "0"
                        item1.SubItems.Add(My.Resources.None)
                    Case "1"
                        item1.SubItems.Add(My.Resources.ODE_word_NT)
                    Case "2"
                        item1.SubItems.Add(My.Resources.Bullet_word_NT)
                    Case "3"
                        item1.SubItems.Add(My.Resources.Bullet_Threaded_word)
                    Case "4"
                        item1.SubItems.Add(My.Resources.ubODE_word)
                    Case "5"
                        item1.SubItems.Add(My.Resources.ubODE_Hybrid_word)
                    Case Else
                        item1.SubItems.Add("-".ToUpperInvariant)
                End Select

                'birds

                If PropRegionClass.Birds(RegionUUID) = "True" Then
                    item1.SubItems.Add(My.Resources.Yes_word)
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                'Tides
                If PropRegionClass.Tides(RegionUUID) = "True" Then
                    item1.SubItems.Add(My.Resources.Yes_word)
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                'teleport
                If PropRegionClass.Teleport(RegionUUID) = "True" Then
                    item1.SubItems.Add(My.Resources.Yes_word)
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.SmartStart(RegionUUID) = "True" Then
                    item1.SubItems.Add(My.Resources.Yes_word)
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.AllowGods(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.AllowGods(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.RegionGod(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.RegionGod(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.ManagerGod(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.ManagerGod(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.SkipAutobackup(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.SkipAutobackup(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.RegionSnapShot(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.RegionSnapShot(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.MinTimerInterval(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.MinTimerInterval(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If

                If PropRegionClass.FrameTime(RegionUUID).Length > 0 Then
                    item1.SubItems.Add(PropRegionClass.FrameTime(RegionUUID))
                Else
                    item1.SubItems.Add("-".ToUpperInvariant)
                End If
                item1.ForeColor = c
                ListView1.Items.AddRange(New ListViewItem() {item1})

            Next
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Log(My.Resources.Error_word, " RegionList " & ex.Message)
        End Try

        For Each col In ListView1.Columns
            Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
                Dim w = colsize.ColumnWidth(CStr(col.name))
                If w > 0 Then col.Width = w
            End Using
        Next

        ListView1.EndUpdate()
        PropUpdateView() = False
        ViewBusy = False

    End Sub

    Private Sub ShowIcons()

        ShowTitle()
        Users.Text = My.Resources.Users_word
        AllNone.Visible = False

        IconView.TabIndex = 0
        IconView.Show()
        UserView.Visible = True
        ListView1.Hide()
        AvatarView.Hide()
        UserView.Hide()

        IconView.BeginUpdate()
        IconView.Items.Clear()
        Dim max_length As Integer
        For Each RegionUUID As String In PropRegionClass.RegionUuids
            Try
                Dim Num As Integer = 0
                Dim Letter As String = ""
                GetStatus(RegionUUID, Num, Letter)
                Dim name = PropRegionClass.RegionName(RegionUUID)
                If name.Length > max_length Then max_length = name.Length
                ' Create items and sub items for each item. Place a check mark next to the item.
                Dim item1 As New ListViewItem(name, Num) With
                {
                    .Checked = PropRegionClass.RegionEnabled(RegionUUID)
                }
                IconView.Items.AddRange(New ListViewItem() {item1})
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Log(My.Resources.Error_word, " RegionList " & ex.Message)
            End Try
        Next

        For Each l In IconView.Items
            l.Text = l.Text.PadRight(max_length)
        Next
        PropUpdateView() = False
        IconView.EndUpdate()

    End Sub

    Private Sub ShowUsers()

        Me.Text = ""
        AllNone.Visible = True
        Users.Text = My.Resources.Email_word
        UserView.TabIndex = 0
        ViewBusy = True

        UserView.Show()
        UserView.Visible = True
        ListView1.Hide()
        AvatarView.Hide()
        IconView.Hide()

        UserView.BeginUpdate()
        UserView.Items.Clear()
        UserView.CheckBoxes = True
        Dim Index = 0

        Try

            ' Create items and sub items for each item.
            Dim M As New Dictionary(Of String, String)

            If MysqlInterface.IsMySqlRunning() Then
                M = MysqlInterface.GetEmailList()
            End If

            For Each Agent In M
                Dim item1 As New ListViewItem(Agent.Key, Index)
                If Agent.Value.Length = 0 Then
                    item1.BackColor = Color.LightGray
                    item1.ForeColor = Color.White
                End If
                item1.SubItems.Add(Agent.Value)
                UserView.Items.AddRange(New ListViewItem() {item1})
                item1.SubItems.Add(CStr(MysqlInterface.AssetCount(Agent.Key)))
                Index += 1
            Next

            Me.Text = M.Count & " " & My.Resources.Users_word

            If Index = 0 Then
                Dim item1 As New ListViewItem(My.Resources.No_Avatars, Index)
                item1.SubItems.Add("-".ToUpperInvariant)
                item1.SubItems.Add(My.Resources.Hypergrid_word)
                UserView.Items.AddRange(New ListViewItem() {item1})
            End If

            For Each col In UserView.Columns
                Using colsize As New ScreenPos(MyBase.Name & "ColumnSize")
                    Dim w = colsize.ColumnWidth(CStr(col.name))
                    If w > 0 Then col.Width = w
                End Using
            Next
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Log(My.Resources.Error_word, " RegionList " & ex.Message)
        End Try

        UserView.EndUpdate()
        ViewBusy = False
        PropUpdateView() = False

    End Sub

#End Region

#Region "Click Methods"

    Private Sub Addregion_Click(sender As Object, e As EventArgs) Handles AddRegionButton.Click

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.BringToFront()
        RegionForm.Init("")
        RegionForm.Activate()
        Application.DoEvents()
        RegionForm.Visible = True
        RegionForm.Select()

    End Sub

    Private Sub AvatarView_Click(sender As Object, e As EventArgs) Handles AvatarView.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.AvatarView.SelectedItems
        Dim item As ListViewItem

        For Each item In regions
            Dim RegionName = item.SubItems(1).Text
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                ' TODO: Needs to be HGV3
                Dim webAddress As String = "hop://" & Settings.DNSName & ":" & Settings.HttpPort & "/" & RegionName
                Try
                    Dim result = Process.Start(webAddress)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            End If
        Next
        PropUpdateView() = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click

        PropRegionClass.GetAllRegions()
        LoadMyListView()

    End Sub

    ' ColumnClick event handler.
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)

        '// Determine if clicked column Is already the column that is being sorted.
        If e.Column = _SortColumn Then
            ' // Reverse the current sort direction for this column.
            If (_order = SortOrder.Ascending) Then
                _order = SortOrder.Descending
            Else
                _order = SortOrder.Ascending
            End If
        Else
            _order = SortOrder.Ascending
        End If

        If TheView1 = ViewType.Details Then
            ListView1.ListViewItemSorter = New ListViewColumnSorter(e.Column, _order)
            _SortColumn = e.Column

            ListView1.Sort()
            ListView1.ResumeLayout()
        End If

        If TheView1 = ViewType.Users Then
            UserView.ListViewItemSorter = New ListViewColumnSorter(e.Column, _order)
            _SortColumn = e.Column

            UserView.Sort()
            UserView.ResumeLayout()
        End If

        If TheView1 = ViewType.Avatars Then
            UserView.ListViewItemSorter = New ListViewColumnSorter(e.Column, _order)
            _SortColumn = e.Column

            AvatarView.Sort()
            UserView.ResumeLayout()
        End If

    End Sub

    Private Sub IconClick(sender As Object, e As EventArgs) Handles IconView.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.IconView.SelectedItems
        Dim item As ListViewItem
        For Each item In regions
            Dim RegionName = item.SubItems(0).Text.Trim
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                StartStopEdit(RegionUUID, RegionName)
            End If
        Next

    End Sub

    Private Sub ListClick(sender As Object, e As EventArgs) Handles ListView1.Click

        Dim regions As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In regions
            Dim RegionName = item.SubItems(0).Text.Trim
            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
            If RegionUUID.Length > 0 Then
                StartStopEdit(RegionUUID, RegionName)
            End If
        Next

    End Sub

    Private Sub ListView1_ItemCheck1(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck

        Dim Item As ListViewItem = Nothing

        Try
            Item = ListView1.Items.Item(e.Index)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
        If Item.Text.Length = 0 Then Return

        Dim UUID As String = PropRegionClass.FindRegionByName(Item.Text)
        Dim out As New Guid
        If Not Guid.TryParse(UUID, out) Then Return
        Dim GroupName = PropRegionClass.GroupName(UUID)

        For Each RegionUUID In PropRegionClass.RegionUuidListByName(GroupName)

            If (e.NewValue = CheckState.Unchecked) Then
                PropRegionClass.RegionEnabled(RegionUUID) = False
            Else
                PropRegionClass.RegionEnabled(RegionUUID) = True
            End If
            Dim INI = Settings.LoadIni(PropRegionClass.RegionIniFilePath(RegionUUID), ";")
            If INI Is Nothing Or INI.Length = 0 Then Return
            Settings.SetIni(PropRegionClass.RegionName(RegionUUID), "Enabled", CStr(PropRegionClass.RegionEnabled(RegionUUID)))
            Settings.SaveINI(INI, System.Text.Encoding.UTF8)
        Next

    End Sub

#End Region

#Region "Private Methods"

    Private Shared Function LoadImage(S As String) As Image

        Dim bmp As Bitmap = Nothing
        Dim u As New Uri(S)
        Dim request As System.Net.WebRequest = Net.WebRequest.Create(u)
        Dim response As System.Net.WebResponse = Nothing
        Try
            response = request.GetResponse()
        Catch ex As Exception
            ' BreakPoint.Show(ex.Message)
        End Try

        Dim responseStream As System.IO.Stream = Nothing
        Try
            responseStream = response.GetResponseStream()
        Catch ex As Exception
            'BreakPoint.Show(ex.Message)
        End Try

        If responseStream IsNot Nothing Then
            bmp = New Bitmap(responseStream)
            responseStream.Dispose()
        End If

        Return bmp

    End Function

    Private Shared Sub SetWindowOnTop(ByVal lhWnd As Int32)

        On Error GoTo SetWindowOnTop_Err

        SetWindowPos(lhWnd, HWND_TOP, 0, 0, 0, 0, NOMOVE Or NOSIZE)

SetWindowOnTop_Exit:
        Exit Sub

SetWindowOnTop_Err:
        Resume SetWindowOnTop_Exit

    End Sub

    Private Shared Sub StartStopEdit(RegionUUID As String, RegionName As String)

        Dim Choices As New FormRegionPopup
        Dim chosen As String = ""
        Choices.Init(RegionName)
        Choices.BringToFront()
        Choices.ShowDialog()

        ' Read the chosen sim name
        chosen = Choices.Choice()
        Choices.Dispose()

        If chosen = "Start" Then

            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
                Return
            End If

            FormSetup.Buttons(FormSetup.BusyButton)

            If Not StartMySQL() Then
                TextPrint(My.Resources.Stopped_word)
            End If
            If Not StartRobust() Then
                TextPrint(My.Resources.Stopped_word)
            End If

            Log("Starting", PropRegionClass.RegionName(RegionUUID))

            PropAborting = False

            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RestartPending

            Application.DoEvents()
            FormSetup.Timer1.Interval = 1000
            FormSetup.Timer1.Start() 'Timer starts functioning
            FormSetup.Buttons(FormSetup.StopButton)
            PropOpensimIsRunning() = True
            FormSetup.ToolBar(True)

        ElseIf chosen = "Stop" Then

            ' if any avatars in any region, give them a choice.
            Dim StopIt As Boolean = True
            For Each num In PropRegionClass.RegionUuidListByName(PropRegionClass.GroupName(RegionUUID))
                ' Ask before killing any people
                If PropRegionClass.AvatarCount(num) > 0 Then
                    Dim response As MsgBoxResult
                    If PropRegionClass.AvatarCount(num) = 1 Then
                        response = MsgBox(My.Resources.OneAvatar & " " & PropRegionClass.RegionName(num) & " " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
                    Else
                        response = MsgBox(PropRegionClass.AvatarCount(num).ToString(Globalization.CultureInfo.InvariantCulture) + " " & Global.Outworldz.My.Resources.people_are_in & " " + PropRegionClass.RegionName(num) + ". " & Global.Outworldz.My.Resources.Do_you_still_want_to_Stop_word, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
                    End If
                    If response = vbNo Then
                        StopIt = False
                    End If
                End If
            Next

            If (StopIt) Then

                Dim hwnd As IntPtr = GetHwnd(PropRegionClass.GroupName(RegionUUID))
                If ShowDOSWindow(hwnd, SHOWWINDOWENUM.SWRESTORE) Then
                    FormSetup.SequentialPause()

                    TextPrint(My.Resources.Not_Running & " " & Global.Outworldz.My.Resources.Stopping_word)
                    ShutDown(RegionUUID)

                    ' shut down all regions in the DOS box
                    For Each RegionUUID In PropRegionClass.RegionUuidListByName(PropRegionClass.GroupName(RegionUUID))
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown ' request a Stop
                    Next
                Else
                    ' shut down all regions in the DOS box
                    For Each UUID As String In PropRegionClass.RegionUuidListByName(PropRegionClass.GroupName(RegionUUID))
                        PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                    Next
                End If

                PropUpdateView = True ' make form refresh
            End If

        ElseIf chosen = "Console" Then

            ReBoot(RegionUUID)

            Dim hwnd = GetHwnd(PropRegionClass.GroupName(RegionUUID))

            If hwnd = IntPtr.Zero Then
                ' shut down all regions in the DOS box
                For Each UUID As String In PropRegionClass.RegionUuidListByName(PropRegionClass.GroupName(RegionUUID))
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Stopped ' already shutting down
                Next
                PropUpdateView = True ' make form refresh
            Else
                Dim tmp As String = Settings.ConsoleShow
                'temp show console
                Settings.ConsoleShow = "True"
                ShowDOSWindow(hwnd, SHOWWINDOWENUM.SWRESTORE)

                SetWindowOnTop(hwnd.ToInt32)
                Settings.ConsoleShow = tmp
            End If

        ElseIf chosen = "Edit" Then

#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
            RegionForm.BringToFront()
            RegionForm.Init(RegionName)
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()

        ElseIf chosen = "Restart" Then

            FormSetup.Buttons(FormSetup.BusyButton)
            FormSetup.SequentialPause()

            ' shut down all regions in the DOS box
            Dim GroupName = PropRegionClass.GroupName(RegionUUID)
            Logger("RecyclingDown", GroupName, "Teleport")
            For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
                PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
                Logger("RecyclingDown", PropRegionClass.RegionName(UUID), "Teleport")
            Next

            FormSetup.Buttons(FormSetup.StopButton)

            TextPrint(My.Resources.Recycle1 & "  " + PropRegionClass.GroupName(RegionUUID))
            ShutDown(RegionUUID)
            PropUpdateView = True ' make form refresh

        ElseIf chosen = "Teleport" Then

            ReBoot(RegionUUID)

            'secondlife://http|!!hg.osgrid.org|80+Lbsa+Plaza

            Dim link = "secondlife://http|!!" & Settings.PublicIP & "|" & Settings.HttpPort & "+" & RegionName
            Try
                System.Diagnostics.Process.Start(link)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

        ElseIf chosen = "Load" Then
            ReBoot(RegionUUID)
            LoadOar(RegionName)

        ElseIf chosen = "Save" Then
            ReBoot(RegionUUID)
            SaveOar(RegionName)

        End If

    End Sub

#End Region

#Region "Clicks"

    Private Shared Function PickGroup() As String

        Dim Chooseform As New FormChooser ' form for choosing a set of regions
        ' Show testDialog as a modal dialog and determine if DialogResult = OK.

        Chooseform.FillGrid("Group")
        Chooseform.BringToFront()
        Dim chosen As String
        Chooseform.ShowDialog()
        Try
            ' Read the chosen GROUP name
            chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            chosen = ""
        End Try

        Chooseform.Dispose()
        Return chosen

    End Function

    Private Sub AllNone_CheckedChanged(sender As Object, e As EventArgs) Handles AllNone.CheckedChanged

        If Not initted Then Return

        If TheView1 = ViewType.Users Then

            For Each X As ListViewItem In UserView.Items
                If ItemsAreChecked1 Then
                    X.Checked = CType(CheckState.Checked, Boolean)
                Else
                    X.Checked = CType(CheckState.Unchecked, Boolean)
                End If
            Next

        ElseIf TheView1 = ViewType.Details Then

            For Each X As ListViewItem In ListView1.Items
                Dim RegionUUID As String
                If ItemsAreChecked1 Then
                    X.Checked = CType(CheckState.Checked, Boolean)
                Else
                    X.Checked = CType(CheckState.Unchecked, Boolean)
                End If
                Dim name = X.Text
                If name.Length > 0 Then
                    RegionUUID = PropRegionClass.FindRegionByName(name)
                    PropRegionClass.RegionEnabled(RegionUUID) = X.Checked
                    Dim INI = Settings.LoadIni(PropRegionClass.RegionIniFilePath(RegionUUID), ";")
                    If INI Is Nothing Then Return
                    Settings.SetIni(PropRegionClass.RegionName(RegionUUID), "Enabled", CStr(X.Checked))
                    Settings.SaveINI(INI, System.Text.Encoding.UTF8)
                End If

            Next
        End If

        If ItemsAreChecked1 Then
            ItemsAreChecked1 = False
        Else
            ItemsAreChecked1 = True
        End If
        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles RestartButton.Click

        FormSetup.RestartAllRegions()

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("RegionList")

    End Sub

    Private Sub RunAllButton_Click(sender As Object, e As EventArgs) Handles RunAllButton.Click

        FormSetup.StartOpensimulator()

    End Sub

    Private Sub StopAllButton_Click(sender As Object, e As EventArgs) Handles StopAllButton.Click

        FormSetup.DoStopActions()

    End Sub

    Private Sub ViewAvatars_Click(sender As Object, e As EventArgs) Handles AvatarsButton.Click

        Settings.RegionListView() = ViewType.Avatars
        Settings.SaveSettings()
        TheView1 = ViewType.Avatars

        SetScreen(TheView1)
        ListView1.View = View.Details

        ListView1.Hide()
        UserView.Hide()
        AvatarView.Show()
        LoadMyListView()
        Timer1.Start()

    End Sub

    Private Sub ViewCompact_Click(sender As Object, e As EventArgs) Handles IconsButton.Click

        Settings.RegionListView() = ViewType.Icons
        Settings.SaveSettings()
        TheView1 = ViewType.Icons
        SetScreen(TheView1)
        ListView1.View = View.SmallIcon
        IconView.Show()
        AvatarView.Hide()
        UserView.Hide()

        ListView1.CheckBoxes = False
        Timer1.Start()
        LoadMyListView()

    End Sub

    Private Sub ViewDetail_Click(sender As Object, e As EventArgs) Handles DetailsButton.Click

        Settings.RegionListView() = ViewType.Details
        Settings.SaveSettings()
        TheView1 = ViewType.Details
        SetScreen(TheView1)
        ListView1.View = View.Details
        ListView1.Show()
        AvatarView.Hide()
        UserView.Hide()

        ListView1.CheckBoxes = True
        Timer1.Start()
        LoadMyListView()

    End Sub

#End Region

#Region "Mysql"

    Private Shared Function GetRegionsName(Region As String) As String

        Dim p1 As String = ""
        Using reader = New StreamReader(Region)
            While reader.Peek <> -1 And p1.Length = 0
                Dim line = reader.ReadLine
                Dim pattern1 As Regex = New Regex("^ *\[(.*?)\] *$")
                Dim match1 As Match = pattern1.Match(line)
                If match1.Success Then
                    p1 = match1.Groups(1).Value
                End If
            End While
        End Using
        Return p1

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ImportButton.Click

        Using ofd As New OpenFileDialog With {
            .InitialDirectory = "\",
            .Filter = Global.Outworldz.My.Resources.INI_Filter,
            .FilterIndex = 1,
            .RestoreDirectory = True,
            .Multiselect = True
        }

            If ofd.ShowDialog = DialogResult.OK Then
                If ofd.CheckFileExists Then

                    Dim dirpathname = PickGroup()
                    If dirpathname.Length = 0 Then
                        TextPrint(My.Resources.Aborted_word)
                        Return
                    End If

                    If dirpathname = "! Add New Name" Then
                        dirpathname = InputBox(My.Resources.Enter_Dos_Name)
                    End If
                    If dirpathname.Length = 0 Then
                        Return
                    End If

                    For Each ofdFilename As String In ofd.FileNames

                        Dim noquotes As Regex = New Regex("'")
                        dirpathname = noquotes.Replace(dirpathname, "")

                        Dim extension As String = Path.GetExtension(ofdFilename)
                        extension = Mid(extension, 2, 5)
                        If extension.ToUpper(Globalization.CultureInfo.InvariantCulture) = "INI" Then

                            Dim filename = GetRegionsName(ofdFilename)
                            Dim RegionUUID As String = PropRegionClass.FindRegionByName(filename)

                            If RegionUUID.Length > 0 Then
                                MsgBox(My.Resources.Region_Already_Exists, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
                                Return
                            End If

                            If dirpathname.Length = 0 Then dirpathname = filename

                            Dim NewFilepath = Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region\"
                            If Not Directory.Exists(NewFilepath) Then
                                Try
                                    Directory.CreateDirectory(Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region")
                                Catch ex As Exception
                                    BreakPoint.Show(ex.Message)
                                End Try
                            End If
                            File.Copy(ofdFilename, Settings.OpensimBinPath & "Regions\" + dirpathname + "\Region\" + filename + ".ini")
                        Else
                            TextPrint(My.Resources.Unrecognized & " " & extension & ". ")
                        End If
                    Next

                    PropRegionClass.GetAllRegions()
                    LoadMyListView()
                End If
            End If
        End Using

    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs)

#Disable Warning CA2000
        Dim EmailForm = New FormEmail
#Enable Warning CA2000
        EmailForm.BringToFront()
        EmailForm.Init(UserView)
        EmailForm.Activate()
        EmailForm.Visible = True
        EmailForm.Select()
    End Sub

    Private Sub FloatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FloatToolStripMenuItem.Click

        FloatToolStripMenuItem.Checked = True
        OnTopToolStripMenuItem.Checked = False
        Me.TopMost = False
        Settings.KeepOnTop = False
        Settings.SaveSettings()

    End Sub

    Private Sub OnTopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnTopToolStripMenuItem.Click

        OnTopToolStripMenuItem.Checked = True
        FloatToolStripMenuItem.Checked = False
        Me.TopMost = True
        Settings.KeepOnTop = True
        Settings.SaveSettings()

    End Sub

    Private Sub Users_Click(sender As Object, e As EventArgs) Handles Users.Click

        If Users.Text = My.Resources.Email_word Then

            Try
#Disable Warning CA2000
                Dim EmailForm = New FormEmail
#Enable Warning CA2000
                EmailForm.BringToFront()
                EmailForm.Init(UserView)
                EmailForm.Visible = True
                EmailForm.Select()
                EmailForm.Activate()
                Return
            Catch
            End Try
        End If

        Settings.RegionListView() = ViewType.Users
        Settings.SaveSettings()
        TheView1 = ViewType.Users
        SetScreen(TheView1)
        ListView1.View = View.List
        ListView1.Hide()
        UserView.Show()
        AvatarView.Hide()
        LoadMyListView()
    End Sub

#End Region

End Class