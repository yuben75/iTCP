Public Module HexideSockets

    'Networking-related functions

    ''' <summary>
    ''' Returns if a comma-seperates string of addresses and hostnames is a valid list of addresses.  NO DNS CHECK.
    ''' </summary>
    ''' <param name="Input"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isValidAddressListing(ByVal Input As String) As Boolean
        Dim Parts() As String = Split(Input, ",", -1, CompareMethod.Text)

        If Parts Is Nothing Then Return False
        If Parts.Length = 0 Then Return False

        For i As Integer = 0 To Parts.Length - 1
            If Parts(i).Trim().Length < 1 Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns if a provided string is a valid IP address.
    ''' </summary>
    ''' <param name="Address"></param>
    ''' <param name="AllowWildcards"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isIPAddress(ByVal Address As String, Optional ByVal AllowWildcards As Boolean = False) As Boolean
        Dim Parts() As String = Split(Address, ".", -1, CompareMethod.Text)
        If Parts.Length <> 4 Then Return False

        For n As Integer = 0 To 3
            If Not IsNumeric(Parts(n)) Then
                If AllowWildcards Then
                    If Not Parts(n) = "*" Then
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                If Integer.Parse(Parts(n)) < 0 Or Integer.Parse(Parts(n)) > 255 Then
                    Return False
                End If
            End If
        Next

        'It's an IP!  Tell Machten Mario, Minor Fault-a-yeah!
        Return True
    End Function

    ''' <summary>
    ''' Converts any valid absolute hostname or absolute IP address in an array of strings into an array of long IPs.
    ''' </summary>
    ''' <param name="Hosts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertMiscToLongIPs(ByVal Hosts() As String) As Int64()
        If Hosts Is Nothing Then Return Nothing
        If Hosts.Length = 0 Then Return Nothing

        Dim Out() As Int64

        For i As Integer = 0 To Hosts.Length - 1
            Try
                Dim Adds() As Net.IPAddress = Net.Dns.GetHostAddresses(Hosts(i))

                If Adds IsNot Nothing Then
                    If Adds.Length > 0 Then
                        For n As Integer = 0 To Adds.Length - 1
                            If Adds(n).AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                                addToArray(Out, Adds(n).Address)
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
            End Try
        Next

        Return Out
    End Function

    ''' <summary>
    ''' Returns just those addresses in an array of strings that are wildcard IP addresses.
    ''' </summary>
    ''' <param name="Addresses"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWildcardAddresses(ByVal Addresses() As String) As String()
        If Addresses Is Nothing Then Return Nothing
        If Addresses.Length = 0 Then Return Nothing

        Dim Out() As String

        For i As Integer = 0 To Addresses.Length - 1
            If isIPAddress(Addresses(i), True) And Addresses(i).IndexOf("*") > -1 Then
                addToArray(Out, Addresses(i))
            End If
        Next

        Return Out
    End Function

    ''' <summary>
    ''' When provided an absolute IP and a wildcard IP, this returns if the absolute falls within the wildcard one.
    ''' </summary>
    ''' <param name="Address"></param>
    ''' <param name="WildAddress"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function doesIPMatchWildIP(ByVal Address As String, ByVal WildAddress As String) As Boolean
        If Not isIPAddress(Address, False) Then Return False
        If Not isIPAddress(WildAddress, True) Then Return False

        Dim PartsAbsolute() As String = Split(Address, ".", -1, CompareMethod.Text)
        Dim PartsWild() As String = Split(WildAddress, ".", -1, CompareMethod.Text)

        For i As Integer = 0 To PartsAbsolute.Length - 1
            If (Not (PartsAbsolute(i) = PartsWild(i))) And (Not (PartsWild(i) = "*")) Then
                'Not a match!
                Return False
            End If
        Next

        Return True
    End Function








    ''' <summary>
    ''' Provides a TCP/IP socket connection to a server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class socketClient
        Private Sock As Net.Sockets.Socket
        Private MonitorThread As Threading.Thread

        Private StopMonitoring As Boolean = False               'Causes the socket to stop monitoring for data

        Public Event Connected()
        Public Event ConnectionError(ByVal Message As String)
        Public Event Disconnected()
        Public Event DisconnectError(ByVal Message As String)
        Public Event IncomingData(ByRef Data As String)
        Public Event IncomingDataBin(ByRef Data() As Byte)
        Public Event IncomingDataError(ByVal Message As String)
        Public Event SendDataError(ByVal Message As String)
        Public Event BytesReceived(ByVal Bytes As Integer)
        Public Event BytesSent(ByVal Bytes As Integer)

        Public Function isConnected() As Boolean
            If Sock IsNot Nothing Then
                Return Sock.Connected
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Connects to a remote IP or hostname on the port provided with a TCP socket over IPv4.
        ''' </summary>
        ''' <param name="Address"></param>
        ''' <param name="Port"></param>
        ''' <remarks></remarks>
        Public Sub Connect(ByVal Address As String, ByVal Port As Integer)
            If Sock IsNot Nothing Then
                If Sock.Connected Then
                    RaiseEvent ConnectionError("This socket is already connected.")
                    Exit Sub
                End If
            End If

            If Address = Nothing Or Port < 0 Or Port > 65536 Then
                RaiseEvent ConnectionError("Bad address or port provided.")
            End If

            Sock = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            Sock.ReceiveBufferSize = 65536
            Sock.SendBufferSize = 65536

            Try
                Sock.Connect(Address, Port)
            Catch ex As Exception
                RaiseEvent ConnectionError(ex.Message)
            End Try

            If Sock.Connected = False Then
                Try
                    Sock.Close(5000)
                    Sock = Nothing
                Catch ex As Exception
                End Try

                RaiseEvent ConnectionError("Unable to connect to remote host.")
            Else
                StopMonitoring = False

                RaiseEvent Connected()

                MonitorThread = New Threading.Thread(AddressOf MonitorSocketForData)
                MonitorThread.Start()
            End If
        End Sub

        ''' <summary>
        ''' Begins the disconnection process by telling the socket monitor to quit.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Disconnect()
            StopMonitoring = True
        End Sub

        ''' <summary>
        ''' Finishes the disconnection process by closing the socket.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FinishDisconnect()
            If Sock IsNot Nothing Then
                Try
                    'Sock.Disconnect(False)
                    Sock.Close()
                    Sock = Nothing
                    RaiseEvent Disconnected()
                Catch ex As Exception
                    RaiseEvent DisconnectError(ex.Message)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Sends a string.
        ''' </summary>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        Public Sub Send(ByRef Data As String)
            Send(getBytesFromRealString(Data))
        End Sub

        ''' <summary>
        ''' Sends a byte array.
        ''' </summary>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        Public Sub Send(ByRef Data() As Byte)
            If Sock IsNot Nothing Then
                If Sock.Connected Then
                    Try
                        Sock.Send(Data)
                        RaiseEvent BytesSent(Data.Length)
                    Catch ex As Exception
                        RaiseEvent SendDataError(ex.Message)
                    End Try
                Else
                    RaiseEvent SendDataError("This socket is not connected to a remote host.")
                End If
            Else
                RaiseEvent SendDataError("This socket hasn't yet been connected to anything.")
            End If
        End Sub

        ''' <summary>
        ''' Monitors the socket for available data and also if it is blatantly disconnected.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MonitorSocketForData()
            Dim LastPollLoops As Integer = 0

            While Not StopMonitoring
                If Sock IsNot Nothing Then
                    If Sock.Connected Then
                        If Sock.Available > 0 Then
                            Try
                                Dim CurrentBytes As Integer = Sock.Available

                                Dim Buffer(CurrentBytes - 1) As Byte
                                Sock.Receive(Buffer, CurrentBytes, Net.Sockets.SocketFlags.None)

                                'Dim TempStr As String = getRealStringFromBytes(Buffer)

                                RaiseEvent BytesReceived(Buffer.Length)
                                RaiseEvent IncomingData(getRealStringFromBytes(Buffer))
                                RaiseEvent IncomingDataBin(Buffer)
                            Catch ex As Exception
                                RaiseEvent IncomingDataError(ex.Message)
                            End Try
                        End If
                    Else
                        Disconnect()
                    End If
                Else
                    Disconnect()
                End If

                LastPollLoops += 1

                If LastPollLoops >= 500 And Sock.Connected Then
                    Try
                        If Sock.Poll(-1, Net.Sockets.SelectMode.SelectRead) And Sock.Available <= 0 Then
                            Disconnect()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    LastPollLoops = 0
                End If

                Threading.Thread.Sleep(25)
            End While

            FinishDisconnect()
        End Sub
    End Class

    ''' <summary>
    ''' Provides a TCP/IP server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class socketServer
        Private Socks() As Net.Sockets.Socket
        Private StopMonitoring() As Boolean
        Private SockMonTHD() As Threading.Thread

        Private Listener As Net.Sockets.TcpListener
        Private ListenerThread As Threading.Thread

        Private StopListening As Boolean = False
        Private ValidRemotes() As String

        Public Event ListenError(ByVal Message As String)

        Public Event Connected(ByVal Sock As Integer, ByVal Remote As String)
        Public Event ConnectionError(ByVal Sock As Integer, ByVal Message As String)
        Public Event ConnectionRefused(ByVal Message As String)
        Public Event Disconnected(ByVal Sock As Integer)
        Public Event DisconnectError(ByVal Sock As Integer, ByVal Message As String)

        Public Event IncomingData(ByVal Sock As Integer, ByRef Data As String)
        Public Event IncomingDataBin(ByVal Sock As Integer, ByRef Data() As Byte)
        Public Event IncomingDataError(ByVal Sock As Integer, ByVal Message As String)
        Public Event SendDataError(ByVal Sock As Integer, ByVal Message As String)
        Public Event BytesReceived(ByVal Sock As Integer, ByVal Bytes As Integer)
        Public Event BytesSent(ByVal Sock As Integer, ByVal Bytes As Integer)

        ''' <summary>
        ''' Causes the server to start listening for connections on a specified port.
        ''' </summary>
        ''' <param name="SocketCount"></param>
        ''' <param name="Port"></param>
        ''' <remarks></remarks>
        Public Sub Listen(ByVal SocketCount As Integer, ByVal Port As Integer, Optional ByVal ValidRemotes() As String = Nothing)
            If Listener IsNot Nothing Then
                RaiseEvent ListenError("The listener is already active.")
                Exit Sub
            End If

            If SocketCount < 1 Then
                RaiseEvent ListenError("You must listen with at least one socket.")
                Exit Sub
            End If

            If Port < 0 Or Port > 65535 Then
                RaiseEvent ListenError("Invalid port.")
                Exit Sub
            End If

            StopListening = False

            If ValidRemotes Is Nothing Then
                Me.ValidRemotes = New String() {"*.*.*.*"}
            Else
                If ValidRemotes.Length = 0 Then
                    Me.ValidRemotes = New String() {"*.*.*.*"}
                Else
                    Me.ValidRemotes = ValidRemotes
                End If
            End If

            ReDim Socks(SocketCount - 1)
            ReDim StopMonitoring(SocketCount - 1)
            ReDim SockMonTHD(SocketCount - 1)

            Listener = New Net.Sockets.TcpListener(Port)
            Listener.Start(16)

            ListenerThread = New Threading.Thread(AddressOf MonitorListenerForConnections)
            ListenerThread.Start()
        End Sub

        ''' <summary>
        ''' Causes the server to stop listening for new connections.  Optionally, it will also disconnect all current ones.
        ''' </summary>
        ''' <param name="KillConnections"></param>
        ''' <remarks></remarks>
        Public Sub stopListen(ByVal KillConnections As Boolean)
            StopListening = True

            If KillConnections Then
                KillAllConnections()
            End If
        End Sub

        ''' <summary>
        ''' Disconnects all connected sockets.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub KillAllConnections()
            If Socks IsNot Nothing Then
                If Socks.Length > 0 Then
                    For i As Integer = 0 To Socks.Length - 1
                        Disconnect(i)
                    Next
                End If
            End If
        End Sub

        ''' <summary>
        ''' Begins the disconnection process by telling the socket monitor to quit.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Disconnect(ByVal Sock As Integer)
            StopMonitoring(Sock) = True
        End Sub

        ''' <summary>
        ''' Finishes the disconnection process by closing the socket.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FinishDisconnect(ByVal Sock As Integer)
            If Socks(Sock) IsNot Nothing Then
                Try
                    'Sock.Disconnect(False)
                    Socks(Sock).Close()
                    Socks(Sock) = Nothing
                    RaiseEvent Disconnected(Sock)
                Catch ex As Exception
                    RaiseEvent DisconnectError(Sock, ex.Message)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Sends a string.
        ''' </summary>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        Public Sub Send(ByVal Sock As Integer, ByRef Data As String)
            Send(Sock, getBytesFromRealString(Data))
        End Sub

        ''' <summary>
        ''' Sends a byte array.
        ''' </summary>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        Public Sub Send(ByVal Sock As Integer, ByRef Data() As Byte)
            If Socks(Sock) IsNot Nothing Then
                If Socks(Sock).Connected Then
                    Try
                        Socks(Sock).Send(Data)
                        RaiseEvent BytesSent(Sock, Data.Length)
                    Catch ex As Exception
                        RaiseEvent SendDataError(Sock, ex.Message)
                    End Try
                Else
                    RaiseEvent SendDataError(Sock, "This socket is not connected to a remote host.")
                End If
            Else
                RaiseEvent SendDataError(Sock, "This socket hasn't yet been connected to anything.")
            End If
        End Sub

        ''' <summary>
        ''' Finds a free socket.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function getFreeSocket() As Integer
            If Socks Is Nothing Then Return -1
            If Socks.Length = 0 Then Return -1

            For i As Integer = 0 To Socks.Length - 1
                If Socks(i) Is Nothing Then
                    Return i
                Else
                    If Socks(i).Connected = False Then
                        Return i
                    End If
                End If
            Next

            Return -1
        End Function

        ''' <summary>
        ''' Monitors a socket for available data and receives it.
        ''' </summary>
        ''' <param name="Args"></param>
        ''' <remarks></remarks>
        Private Sub MonitorSocketForData(ByVal Args As Object)
            Dim SockNum As Integer = Args
            Dim LastPollLoops As Integer = 0

            While Not StopMonitoring(SockNum)
                If Socks(SockNum) IsNot Nothing Then
                    If Socks(SockNum).Connected Then
                        If Socks(SockNum).Available > 0 Then
                            Try
                                Dim CurrentBytes As Integer = Socks(SockNum).Available

                                Dim Buffer(CurrentBytes - 1) As Byte
                                Socks(SockNum).Receive(Buffer, CurrentBytes, Net.Sockets.SocketFlags.None)

                                RaiseEvent BytesReceived(SockNum, Buffer.Length)
                                RaiseEvent IncomingData(SockNum, getRealStringFromBytes(Buffer))
                                RaiseEvent IncomingDataBin(SockNum, Buffer)
                            Catch ex As Exception
                                RaiseEvent IncomingDataError(SockNum, ex.Message)
                            End Try
                        End If
                    Else
                        Disconnect(SockNum)
                    End If
                Else
                    Disconnect(SockNum)
                End If

                LastPollLoops += 1

                If LastPollLoops >= 500 And Socks(SockNum).Connected Then
                    Try
                        If Socks(SockNum).Poll(-1, Net.Sockets.SelectMode.SelectRead) And Socks(SockNum).Available <= 0 Then
                            Disconnect(SockNum)
                        End If
                    Catch ex As Exception
                        MsgBox("HexideSockets.MonitorSocketForData() Error:" & vbCrLf & vbCrLf & ex.Message)
                    End Try

                    LastPollLoops = 0
                End If

                Threading.Thread.Sleep(25)
            End While

            FinishDisconnect(SockNum)
        End Sub

        ''' <summary>
        ''' Returns if an IP (may contain a port in colon form, too) is allowed by this server.
        ''' </summary>
        ''' <param name="RemoteIP"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function isAllowableAddress(ByVal RemoteIP As String) As Boolean
            If ValidRemotes Is Nothing Then Return False 'Error on the side of caution--this should never happen
            If ValidRemotes.Length = 0 Then Return False 'Ditto
            If RemoteIP = Nothing Then Return False

            Dim Parts() As String = Split(RemoteIP, ":", -1, CompareMethod.Text)
            If Parts.Length < 1 Then Return False

            Dim Src() As Net.IPAddress = Net.Dns.GetHostAddresses(Parts(0))

            If Src Is Nothing Then Return False
            If Src.Length < 1 Then Return False

            Dim Wilds() As String = GetWildcardAddresses(ValidRemotes)

            'All Wildcard IPs
            If Wilds IsNot Nothing Then
                If Wilds.Length > 0 Then
                    For i As Integer = 0 To Wilds.Length - 1
                        If doesIPMatchWildIP(Parts(0), Wilds(i)) Then
                            Return True
                        End If
                    Next
                End If
            End If

            Dim Comps() As Long = ConvertMiscToLongIPs(ValidRemotes)

            'All Absolutes
            If Comps IsNot Nothing Then
                If Comps.Length > 0 Then
                    For i As Integer = 0 To Comps.Length - 1
                        If Comps(i) = Src(0).Address Then
                            Return True
                        End If
                    Next
                End If
            End If

            Return False
        End Function

        ''' <summary>
        ''' Monitors for pending connections and accepts them if there are any free spaces.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MonitorListenerForConnections()
            While Not StopListening
                If Listener IsNot Nothing Then
                    Dim Use As Integer = getFreeSocket()

                    While Listener.Pending And Use >= 0
                        Try
                            Dim Temp As Net.Sockets.Socket = Listener.AcceptSocket()

                            If isAllowableAddress(Temp.RemoteEndPoint.ToString()) Then
                                StopMonitoring(Use) = False
                                Socks(Use) = Temp

                                Socks(Use).ReceiveBufferSize = 65536
                                Socks(Use).SendBufferSize = 65536

                                RaiseEvent Connected(Use, Socks(Use).RemoteEndPoint.ToString())

                                SockMonTHD(Use) = New Threading.Thread(AddressOf MonitorSocketForData)
                                SockMonTHD(Use).Start(Use)
                            Else
                                Dim Address As String = Temp.RemoteEndPoint.ToString()
                                Temp.Close()
                                RaiseEvent ConnectionRefused("Remote address " & Address & " isn't on the allowable list.")
                            End If
                        Catch ex As Exception
                            RaiseEvent ConnectionError(Use, ex.Message)
                        End Try

                        Use = getFreeSocket()
                    End While
                End If

                Threading.Thread.Sleep(500)
            End While

            Listener.Stop()
            Listener = Nothing
        End Sub
    End Class
End Module