﻿#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Net
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class NetServer

#Region "Private Fields"

    Private Shared blnFlag As Boolean
    Private Shared singleWebserver As NetServer
    Dim listen As Boolean = True
    Private MyPort As String

    Dim PropRegionClass As RegionMaker = RegionMaker.Instance()
    Private running As Boolean
    Dim Setting As MySettings
    Private WebThread As Thread

#End Region

#Region "Public Properties"

    Public Property PropRegionClass1 As RegionMaker
        Get
            Return PropRegionClass
        End Get
        Set(value As RegionMaker)
            PropRegionClass = value
        End Set
    End Property

#End Region

#Region "Callback"

    Public Sub StartServer(pathinfo As String, settings As MySettings)

        If settings Is Nothing Then Return
        ' stash some globs
        Setting = settings
        MyPort = CStr(settings.DiagnosticPort)
        settings.CurrentDirectory = pathinfo

        If running Then Return

        Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Starting_DiagPort_Webserver)
        WebThread = New Thread(AddressOf Looper)
        Try
            WebThread.SetApartmentState(ApartmentState.STA)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Log(My.Resources.Error_word, ex.Message)
        End Try
        WebThread.Priority = ThreadPriority.Highest
        WebThread.Start()

        running = True

    End Sub

    Public Sub StopWebServer()

        Log(My.Resources.Info_word, Global.Outworldz.My.Resources.Stopping_Webserver)
        listen = False
        WebThread.Abort()

    End Sub

    Private Sub ListenerCallback(ByVal result As IAsyncResult)
        If result Is Nothing Then Return
        Try
            Dim listener As HttpListener = CType(result.AsyncState, HttpListener)
            ' Call EndGetContext to signal the completion of the asynchronous operation.
            Dim context As HttpListenerContext = listener.EndGetContext(result)

            ' The data sent by client
            Dim request As HttpListenerRequest = context.Request

            Dim body As Stream = request.InputStream
            Dim responseString As String = ""
            Using reader As System.IO.StreamReader = New System.IO.StreamReader(body, request.ContentEncoding)

                Dim lcUri = LCase(request.Url.OriginalString)

                If lcUri.Contains("teleports.htm") Then
                    responseString = RegionListHTML(Setting, PropRegionClass1)
                Else
                    If (request.HasEntityBody) Then
                        Dim POST As String = reader.ReadToEnd()
                        Log("POST", POST)
                        responseString = PropRegionClass1.ParsePost(POST, Setting)
                    Else
                        Log("URI", lcUri)
                        responseString = PropRegionClass1.ParsePost(lcUri, Setting)
                    End If

                End If

            End Using

            ' Get the response object to send our confirmation.
            Using response As HttpListenerResponse = context.Response
                ' Construct a minimal response string.
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
                ' Get the response OutputStream and write the response to it.
                response.ContentLength64 = buffer.Length
                ' Identify the content type.
                response.ContentType = "text/html"
                Using output As System.IO.Stream = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    output.Flush()
                End Using

            End Using
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Internal Methods"

    Friend Shared Function GetWebServer() As NetServer

        If Not blnFlag Then
            singleWebserver = New NetServer
            blnFlag = True
            Return singleWebserver
        Else
            Return singleWebserver
        End If

    End Function

#End Region

#Region "Private Methods"

    Private Shared Sub Log(category As String, message As String)
        Debug.Print(message)
        Try
            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Logs\Http.log"), True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture) & ":" & category & ":" & message)
            End Using
        Catch ex As Exception
            ' none to prevent looping
        End Try
    End Sub

    Private Shared Function RegionListHTML(Settings As MySettings, PropRegionClass As RegionMaker) As String

        ' http://localhost:8001/teleports.htm
        ' http://YourURL:8001/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String

        HTML = "Welcome to |" & Settings.SimName & "||" & CStr(Settings.PublicIP) & ":" & CStr(Settings.HttpPort) & ":" & Settings.WelcomeRegion & "||" & vbCrLf
        Dim ToSort As New List(Of String)

        Using NewSQLConn As New MySqlConnection(Settings.RobustMysqlConnection)
            Diagnostics.Debug.Print("Conn:" & Settings.RobustMysqlConnection)
            Dim UserStmt = "SELECT regionName from REGIONS order by regionName ASC"
            Try
                NewSQLConn.Open()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Return HTML
            End Try

            Using cmd As New MySqlCommand(UserStmt, NewSQLConn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                While reader.Read()
                    Dim LongName = reader.GetString(0)
                    Dim RegionUUID As String = PropRegionClass.FindRegionByName(LongName)
                    If RegionUUID.Length > 0 Then
                        If PropRegionClass.Teleport(RegionUUID) = "True" And
                           PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                            ToSort.Add(LongName)
                        End If
                    End If
                End While
            End Using
        End Using

        ' Acquire keys And sort them.
        ToSort.Sort()

        For Each S As String In ToSort
            HTML = HTML & "*|" & S & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & S & "||" & vbCrLf
        Next

        Return HTML

    End Function

    Private Sub Looper()

        listen = True

        Using listener As New System.Net.HttpListener()
            listener.Prefixes.Clear()
            listener.Prefixes.Add("http://+:" & MyPort & "/")

            Try
                listener.Start() ' Throws Exception
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                Log(My.Resources.Error_word, ex.Message)
                Return
            End Try

            Dim result As IAsyncResult
            While listen
                result = listener.BeginGetContext((AddressOf ListenerCallback), listener)
                result.AsyncWaitHandle.WaitOne()
            End While

        End Using

        running = False

    End Sub

#End Region

End Class
