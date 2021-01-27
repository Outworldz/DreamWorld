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

Imports System.Text.RegularExpressions

Public Class FormIcecast

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

    Private Sub AdminPassword_Click(sender As Object, e As EventArgs) Handles AdminPassword.Click

        AdminPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub AdminPassword_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        Settings.SCAdminPassword = AdminPassword.Text

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        HelpManual("Icecast")

    End Sub

    Private Sub FormisClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        FormSetup.PropViewedSettings = True
        Settings.SaveSettings()
        DoIceCast()

    End Sub

    Private Sub LoadURL_Click(sender As Object, e As EventArgs) Handles LoadURL.Click
        If PropOpensimIsRunning() Then
            Dim webAddress As String = "http://" + Settings.PublicIP + ":" + ShoutcastPort.Text
            TextPrint(My.Resources.Icecast_Desc & " " + webAddress)
            Try
                Process.Start(webAddress)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        ElseIf Settings.SCEnable = False Then
            TextPrint(My.Resources.IceCast_disabled)
        Else
            TextPrint(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        HelpManual("Icecast")

    End Sub

    Private Sub SC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        GroupBox1.Text = Global.Outworldz.My.Resources.IceCast_Server_word
        Label1.Text = Global.Outworldz.My.Resources.Password_word
        Label2.Text = Global.Outworldz.My.Resources.Port1
        Label3.Text = Global.Outworldz.My.Resources.Admin_Password_word
        Label4.Text = Global.Outworldz.My.Resources.port2
        LoadURL.Text = Global.Outworldz.My.Resources.Admin_Web_Page_word
        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        PictureBox1.Image = Global.Outworldz.My.Resources.about
        ShoutcastEnable.Text = Global.Outworldz.My.Resources.Enable_word
        Text = Global.Outworldz.My.Resources.Icecast_word
        ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word

        ShoutcastPort.Text = Settings.SCPortBase.ToString(Globalization.CultureInfo.InvariantCulture)
        ShoutcastPort1.Text = Settings.SCPortBase1.ToString(Globalization.CultureInfo.InvariantCulture)
        AdminPassword.Text = Settings.SCAdminPassword
        ShoutcastPassword.Text = Settings.SCPassword
        ShoutcastEnable.Checked = Settings.SCEnable

        AdminPassword.UseSystemPasswordChar = True
        ShoutcastPassword.UseSystemPasswordChar = True
        SetScreen()
        HelpOnce("Icecast")

    End Sub

    Private Sub ShoutcastEnable_CheckedChanged(sender As Object, e As EventArgs) Handles ShoutcastEnable.CheckedChanged

        Settings.SCEnable = ShoutcastEnable.Checked

    End Sub

    Private Sub ShoutcastPassword_CLickChanged(sender As Object, e As EventArgs) Handles ShoutcastPassword.Click

        ShoutcastPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub ShoutcastPassword_TextChanged(sender As Object, e As EventArgs) Handles ShoutcastPassword.TextChanged

        Settings.SCPassword = ShoutcastPassword.Text

    End Sub

    Private Sub ShoutcastPortTextChanged(sender As Object, e As EventArgs) Handles ShoutcastPort.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ShoutcastPort.Text = digitsOnly.Replace(ShoutcastPort.Text, "")

        If Not Integer.TryParse(ShoutcastPort.Text, Settings.SCPortBase) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles ShoutcastPort1.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ShoutcastPort1.Text = digitsOnly.Replace(ShoutcastPort1.Text, "")

        If Not Integer.TryParse(ShoutcastPort1.Text, Settings.SCPortBase1) Then
            MsgBox(My.Resources.Must_be_A_Number, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

    End Sub

#End Region

End Class
