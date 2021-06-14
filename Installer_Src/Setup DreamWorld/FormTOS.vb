#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Public Class TosForm

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

    Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click

        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory & "\tos.html"))
                outputFile.WriteLine(RichTextBox.Text)
            End Using

            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\opensim\bin\WifiPages\tos.html"))
                outputFile.WriteLine(RichTextBox.Text)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PreviewButton.Click

        If PropOpensimIsRunning() Then
            Dim webAddress As String = "http://" & CStr(Settings.PublicIP) & ":" & CStr(Settings.HttpPort) & "/wifi/termsofservice.html"
            Try
                Process.Start(webAddress)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        Else
            MsgBox(My.Resources.Not_Running)
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SaveButton.Text = My.Resources.Save_changes_word
        ApplyButton.Text = My.Resources.Apply_word
        PreviewButton.Text = My.Resources.Preview_in_Browser

        Dim reader As System.IO.StreamReader
        reader = System.IO.File.OpenText(IO.Path.Combine(Settings.CurrentDirectory, "tos.html"))
        'now loop through each line
        Dim HTML As String = ""
        While reader.Peek <> -1
            HTML = HTML + reader.ReadLine() + vbCrLf
        End While
        reader.Close()
        RichTextBox.Text = HTML

        SetScreen()

        HelpOnce("TOS")

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "tos.html"))
                outputFile.WriteLine(RichTextBox.Text)
            End Using

            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\opensim\bin\WifiPages\tos.html"))
                outputFile.WriteLine(RichTextBox.Text)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Me.Close()

    End Sub

    Private Sub ToolStripMenuItem30_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem30.Click
        HelpManual("Tos")
    End Sub

#End Region

End Class
