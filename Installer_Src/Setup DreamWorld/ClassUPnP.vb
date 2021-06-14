#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class UPnp
    Implements IDisposable

    ''' <summary>
    ''' https://www.vbforums.com/showthread.php?666272-VB-NET-and-UPnP
    ''' </summary>
    '''

#Region "Private Fields"

    ReadOnly UPnpnat As NATUPNPLib.UPnPNAT
    Private CacheIP As String = ""

    Private staticEnabled As Boolean = True
    Dim staticMapping As NATUPNPLib.IStaticPortMappingCollection

#End Region

#Region "Public Constructors"

    ''' <summary>
    ''' The UPnp Managed Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        'Create the new NAT Class
        Try
            UPnpnat = New NATUPNPLib.UPnPNAT
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            ErrorLog(ex.Message)

        End Try

        'generate the static mappings
        GetStaticMappings()

    End Sub

#End Region

#Region "Public Enums"

    ''' <summary>
    ''' The different supported protocols
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MyProtocol
        ''' <summary>
        ''' Transmission Control Protocol
        ''' </summary>

        TCP

        ''' <summary>
        ''' User Datagram Protocol
        ''' </summary>
        UDP

    End Enum

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Returns if UPnp is enabled.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UPnpEnabled As Boolean
        Get
            Return staticEnabled = True
        End Get
    End Property

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Attempts to locate the local IP address of this computer.
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function LocalIPForced() As String

        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)

        For Each IPaddress In IPList.AddressList
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) AndAlso IPCheck.IsPrivateIP(IPaddress.ToString()) Then
                Dim ip = IPaddress.ToString()
                Return ip
            End If
        Next

        Return String.Empty
    End Function

    ''' <summary>
    ''' Adds a port mapping to the UPnp enabled device.
    ''' </summary>
    ''' <param name="localIP">The local IP address to map to.</param>
    ''' <param name="Port">The port to forward.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <param name="desc">A small description of the port.</param>
    ''' <remarks></remarks>
    Public Function Add(ByVal localIP As String, ByVal port As Integer, ByVal prot As MyProtocol, ByVal desc As String) As Boolean

        If Not IsPrivateIP(localIP) Then Return False
        If staticMapping Is Nothing Then Return False
        If Not staticEnabled Then Return False

        Dim protocol As String
        If prot = UPnp.MyProtocol.TCP Then
            protocol = "TCP"
        Else
            protocol = "UDP"
        End If

        Try
            ' Okay, continue on
            staticMapping.Add(port, protocol, port, localIP, True, desc & ":" & CStr(port))
        Catch ex As Exception
            TextPrint("Cannot add UPNP port " & CStr(port) & " to router")
            Return False

        End Try
        Return True

    End Function

    ''' <summary>
    ''' Dispose!
    ''' </summary>
    ''' <remarks></remarks>

    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")>
    Public Sub Dispose() Implements IDisposable.Dispose
        Try
            If staticMapping IsNot Nothing Then
                Marshal.ReleaseComObject(staticMapping)
            End If
        Catch
        End Try
        Try
            Marshal.ReleaseComObject(UPnpnat)
            Dispose(True)
            GC.SuppressFinalize(Me)
        Catch
        End Try

        Return
    End Sub

    ''' <summary>
    ''' Checks to see if a port exists in the mapping.
    ''' </summary>
    ''' <param name="Port">The port to check.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <remarks></remarks>
    Public Function Exists(ByVal port As Integer, ByVal prot As MyProtocol) As Boolean

        If staticMapping Is Nothing Then Return False
        Try
            'Dim x = staticMapping.Count

            ' Begin checking
            For Each mapping As NATUPNPLib.IStaticPortMapping In staticMapping
                ' Compare
                If mapping.ExternalPort.Equals(port) AndAlso mapping.Protocol = prot.ToString() Then
                    Debug.Print("Port Exists")
                    Return True
                End If
            Next
        Catch ex As Exception
            ' no break
        End Try

        'Nothing!
        Return False

    End Function

    Public Function LocalIP() As String

        Dim LIP As String = ""
        Try
            If CacheIP.Length = 0 Then
                If Settings.DNSName = "localhost" Or Settings.DNSName = "127.0.0.1" Then
                    Return Settings.DNSName
                End If

                Dim sock As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)
                Try
                    Using sock
                        sock.Connect("8.8.8.8", 65530)  ' try Google
                        Dim EndPoint As IPEndPoint = TryCast(sock.LocalEndPoint, IPEndPoint)
                        LIP = EndPoint.Address.ToString()
                    End Using
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    LIP = LocalIPForced()

                    If LIP.Length = 0 Then
                        LIP = "127.0.0.1"
                    End If
                End Try
                CacheIP = LIP
            Else
                LIP = CacheIP
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return LIP

    End Function

    ''' <remarks></remarks>
    Public Sub Remove(ByVal port As Integer, ByVal prot As MyProtocol)
        If staticMapping Is Nothing Then Return
        Try
            staticMapping.Remove(port, prot.ToString)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Removes a port mapping from the UPnp enabled device.
    ''' </summary>
    ''' <param name="Port">The port to remove.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <summary>
    ''' Disposes of the UPnp class
    ''' </summary>
    ''' <param name="disposing">True or False makes no difference.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub Dispose(disposing As Boolean)
        Try
            If staticMapping IsNot Nothing Then
                Marshal.ReleaseComObject(staticMapping)
            End If
            Marshal.ReleaseComObject(UPnpnat)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Returns all static port mappings
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub GetStaticMappings()
        Try
            staticMapping = UPnpnat.StaticPortMappingCollection()
            If staticMapping Is Nothing Then
                staticEnabled = False
                Log("WARN", "UPNP is not available")
                Debug.Print("No Static UPNP")
                Return
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Log("WARN", "UPNP is not available")
            staticEnabled = False
            Return
        End Try
        Debug.Print("Static UPNP available")
        Log("INFO", "UPNP is available")
    End Sub

#End Region

End Class
