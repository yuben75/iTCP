Public Class frmCommunicator
    Dim user As String = Nothing
    Dim disconected As String = Nothing
    Dim disconnSock As String = Nothing
    Dim sendall As String = Nothing
    Dim exclude_sock As Integer
    Dim new_sock As Integer
    Dim privSock As Integer
    Dim privSock2 As Integer
    Dim privSock3 As Integer
    Dim privString As String = Nothing
    Dim list As String = Nothing
    Dim list1 As String = Nothing
    Dim list2 As String = Nothing
    Dim countListItems As Integer


    Public Sub sendPriv()

        Server.Send(privSock2, "@code1847@" & privString & "   " & CStr(privSock3))
        Server.Send(privSock3, "@code1847@" & privString & "   " & CStr(privSock2))
        privSock3 = Nothing
        privSock2 = Nothing
        privString = Nothing

    End Sub
    Public Sub dodaj_korisnika()
        ListBox1.Items.Add(user + "   " + CStr(privSock))
        countListItems += 1
        user = Nothing
    End Sub
    Public Sub izbrisi_korisnika()
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items.Item(i).ToString = disconected & "   " & disconnSock Then
                ListBox1.Items.RemoveAt(i)
                Exit For
            End If
        Next
        disconected = Nothing
    End Sub
    Public Sub senditall()
        serverSendToAllConnected2("", sendall)
        sendall = Nothing
    End Sub
    Public Sub usersUpdate()
        serverSendToAllConnected3("", sendall)
    End Sub
    Public Sub userLeave()
        serverSendToAllConnected4("", sendall)
    End Sub
    Private Sub serverSendToAllConnected4(ByVal User As String, ByVal Message As String, Optional ByVal ExceptSock As Integer = -1)
        If isArraySafe(InUse) Then
            For i As Integer = 0 To InUse.Length - 1

                If Not (i = ExceptSock) Then
                    If InUse(i) Then

                        list2 = ""
                        For b As Integer = 0 To ListBox1.Items.Count - 1 ' nema potrebe izvrtjeti sve korisnike ponovo i za one kojima su ve?uploadani
                            ' OVAJ DIO JE ZA NOVOULOGIRANE KORISNIKE
                            list2 = list2 & ListBox1.Items.Item(b).ToString + vbCrLf
                        Next
                        Server.Send(i, "@code1840@" & list2)
                    
                    End If
                End If
            Next
        End If
        list2 = Nothing
    End Sub
    Private Sub serverSendToAllConnected3(ByVal User As String, ByVal Message As String, Optional ByVal ExceptSock As Integer = -1)
        If isArraySafe(InUse) Then
            For i As Integer = 0 To InUse.Length - 1

                If Not (i = ExceptSock) Then
                    If InUse(i) Then
                        If new_sock = i Then
                            list1 = ""
                            For b As Integer = 0 To ListBox1.Items.Count - 1 ' nema potrebe izvrtjeti sve korisnike ponovo i za one kojima su ve?uploadani
                                ' OVAJ DIO JE ZA NOVOULOGIRANE KORISNIKE
                                list1 = list1 & ListBox1.Items.Item(b).ToString + vbCrLf
                            Next
                            Server.Send(i, "@code1841@" & list1)
                        Else
                            Server.Send(i, "@code1841@" & list & "   " & CStr(privSock))             ' OVO JE LAGANI UPDATE POPISA KORISNIKA ZA ONE KOJI SU VE?TU
                        End If
                    End If
                End If
            Next
        End If
        list = Nothing
        list1 = Nothing
        new_sock = Nothing
    End Sub
    Private Sub serverSendToAllConnected2(ByVal User As String, ByVal Message As String, Optional ByVal ExceptSock As Integer = -1)
        If isArraySafe(InUse) Then
            For i As Integer = 0 To InUse.Length - 1
                If i <> exclude_sock Then
                    If Not (i = ExceptSock) Then
                        If InUse(i) Then
                            Server.Send(i, "" & Message)
                        End If
                    End If
                End If
            Next
        End If
    End Sub
#Region "Server Code"
    Private Server As socketServer
    Private ServerOn As Boolean = False
    Private InUse() As Boolean


    Private Sub serverLogMessage(ByVal Message As String)
        Delegates.RichTextBoxes.appendText(Me, rtbServer, vbCrLf & Message)
    End Sub

    Private Sub serverSendToAllConnected(ByVal User As String, ByVal Message As String, Optional ByVal ExceptSock As Integer = -1)
        If isArraySafe(InUse) Then
            For i As Integer = 0 To InUse.Length - 1
                If Not (i = ExceptSock) Then
                    If InUse(i) Then
                        Server.Send(i, "Server:  " & Message)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub txtServeSend_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtServeSend.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            If Server IsNot Nothing Then
                serverSendToAllConnected("Server", txtServeSend.Text)
                serverLogMessage("Server:  " & txtServeSend.Text)
                txtServeSend.Text = ""
            End If
        End If
    End Sub

    Private Sub btnStopServe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopServe.Click
        If Server Is Nothing Then
            Exit Sub
        Else
            If ServerOn = False Then
                Exit Sub
            Else
                Server.stopListen(True)
                serverLogMessage("No longer serving.")
                ServerOn = False
            End If
        End If
    End Sub
    '#################################  FOR LONG IP ADDRESS  -  NEBITNO ######################################
    Public Function Dotted2LongIP(ByVal DottedIP As String) As Object
        ' errors will result in a zero value
        On Error Resume Next

        Dim i As Byte, pos As Integer
        Dim PrevPos As Integer, num As Integer

        ' string cruncher
        For i = 1 To 4
            ' Parse the position of the dot
            pos = InStr(PrevPos + 1, DottedIP, ".", 1)

            ' If its past the 4th dot then set pos to the last
            'position + 1

            If i = 4 Then pos = Len(DottedIP) + 1

            ' Parse the number from between the dots

            num = Int(Mid(DottedIP, PrevPos + 1, pos - PrevPos - 1))

            ' Set the previous dot position
            PrevPos = pos

            ' No dot value should ever be larger than 255
            ' Technically it is allowed to be over 255 -it just
            ' rolls over e.g.
            '256 => 0 -note the (4 - i) that's the 
            'proper exponent for this calculation


            Dotted2LongIP = ((num Mod 256) * (256 ^ (4 - i))) + Dotted2LongIP

        Next
        Return Dotted2LongIP
    End Function
    '#############################################################################################
    Private Sub btnServe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServe.Click

        If Server Is Nothing Then
            Server = New socketServer()
        Else
            If ServerOn = False Then
                Server = New socketServer()
            Else
                Exit Sub
            End If
        End If

        ServerOn = True

        AddHandler Server.IncomingData, AddressOf handleServerIncomingData
        AddHandler Server.Connected, AddressOf handleServerConnected
        AddHandler Server.ConnectionError, AddressOf handleServerConnectionError
        AddHandler Server.ConnectionRefused, AddressOf handleServerConnectionRefused
        AddHandler Server.Disconnected, AddressOf handleServerDisconnected
        AddHandler Server.DisconnectError, AddressOf handleServerDisconnectError
        AddHandler Server.IncomingDataError, AddressOf handleServerIncomingDataError
        AddHandler Server.ListenError, AddressOf handleServerListenError
        AddHandler Server.SendDataError, AddressOf handleServerSendDataError

        ReDim InUse(63)

        Server.Listen(64, txtServePort.Text)

        serverLogMessage("Now serving.")
    End Sub

    '************************************************************
    'Primary Socket Functionality
    '************************************************************
    Public Sub handleServerIncomingData(ByVal Sock As Integer, ByRef Data As String)
        If InStr(Data, "@code1843@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1843@", "")
            user = Data
            list = Data
            list1 = Data
            new_sock = Sock
            privSock = Sock
        ElseIf InStr(Data, "@code1842@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1842@", "")
            disconected = Trim(Mid(Data, 1, Data.Length))
            disconnSock = CStr(Sock)
        ElseIf InStr(Data, "@code1839@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1839@", "")
            privString = LSet(Data, Data.Length - 2)
            privSock2 = CInt(Trim(Mid(Data, Data.Length - 2)))
            privSock3 = Sock
            sendPriv()
        Else
            If Data.Length > 0 Then
                serverLogMessage(Data)
                sendall = Data
                exclude_sock = Sock
            End If
        End If
    End Sub

    Private Sub handleServerConnected(ByVal Sock As Integer, ByVal RemoteAddress As String)
        serverLogMessage("Connection from " & RemoteAddress & " to socket space " & Sock & ".")
        InUse(Sock) = True
    End Sub

    Private Sub handleServerConnectionRefused(ByVal Message As String)
        serverLogMessage(Message)
    End Sub

    Private Sub handleServerDisconnected(ByVal Sock As Integer)
        serverLogMessage("Socket " & Sock & ":  Disconnected.")
        InUse(Sock) = False
    End Sub

    '************************************************************
    'Functional Error Reporting (Below)
    '************************************************************
    Private Sub handleServerConnectionError(ByVal Sock As Integer, ByVal Message As String)
        serverLogMessage("Socket " & Sock & ":  " & Message)
    End Sub

    Private Sub handleServerDisconnectError(ByVal Sock As Integer, ByVal Message As String)
        serverLogMessage("Socket " & Sock & ":  " & Message)
    End Sub

    Private Sub handleServerIncomingDataError(ByVal Sock As Integer, ByVal Message As String)
        serverLogMessage("Socket " & Sock & ":  " & Message)
    End Sub

    Private Sub handleServerListenError(ByVal Message As String)
        serverLogMessage("Error:  " & Message)
        ServerOn = False
    End Sub

    Private Sub handleServerSendDataError(ByVal Sock As Integer, ByVal Message As String)
        serverLogMessage("Socket " & Sock & ":  " & Message)
    End Sub
#End Region

#Region "Client Code"
    Dim sr As IO.StringReader
    Dim users As String = Nothing
    Dim refresh1 As String = Nothing

    Dim formNo As String = Nothing
    Dim poruka As String = Nothing
    Dim br As String = Nothing

    Public Sub findForm1()

        If Trim(Mid(My.Forms.Private1.Text, My.Forms.Private1.Text.Length - 2)) = formNo Then
            My.Forms.Private1.RichTextBox1.Text = My.Forms.Private1.RichTextBox1.Text & poruka + vbCrLf

        ElseIf Trim(Mid(My.Forms.Private2.Text, My.Forms.Private2.Text.Length - 2)) = formNo Then
            My.Forms.Private2.RichTextBox1.Text = My.Forms.Private2.RichTextBox1.Text & poruka + vbCrLf
        Else
            If My.Forms.Private1.Visible = False Then
                Dim name As String
                For i As Integer = 1 To poruka.Length
                    If Mid(poruka, i, 2) = ": " Then
                        Exit For
                    End If
                    name = name & Mid(poruka, i, 1)
                Next
                My.Forms.Private1.Show()
                My.Forms.Private1.Text = Trim(name) & "   " & br
                My.Forms.Private1.RichTextBox1.Text = My.Forms.Private1.RichTextBox1.Text & poruka + vbCrLf
            Else
                Dim name As String
                For i As Integer = 1 To poruka.Length
                    If Mid(poruka, i, 2) = ": " Then
                        Exit For
                    End If
                    name = name & Mid(poruka, i, 1)
                Next
                My.Forms.Private2.Show()
                My.Forms.Private2.Text = Trim(name) & "   " & br
                My.Forms.Private2.RichTextBox1.Text = My.Forms.Private2.RichTextBox1.Text & poruka + vbCrLf
            End If
        End If

        formNo = Nothing
        poruka = Nothing

    End Sub

    Public Sub addUsers()
        sr = New IO.StringReader(users)
        Do Until sr.Peek < 0
            ListBox2.Items.Add(sr.ReadLine)
        Loop
        users = Nothing
    End Sub
    Public Sub refUsers()
        ListBox2.Items.Clear()
        sr = New IO.StringReader(refresh1)
        Do Until sr.Peek < 0
            ListBox2.Items.Add(sr.ReadLine)
        Loop
        refresh1 = Nothing
    End Sub

    Private Client As socketClient

    Private Sub clientLogMessage(ByVal Message As String)
        Delegates.RichTextBoxes.appendText(Me, rtbClient, vbCrLf & Message)
    End Sub

    Private Sub btnClientConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientConnect.Click
        If InStr(txtClientName.Text, "@code1843@") > 0 Then
            MsgBox("Nickname nesmije sadržavati niz '@code1843@' !")
        ElseIf InStr(txtClientName.Text, " ") > 0 Then
            MsgBox("Nickname nesmije sadržavati razmak !")
        Else

            Client = New socketClient()

            AddHandler Client.Connected, AddressOf handleClientConnected
            AddHandler Client.ConnectionError, AddressOf handleClientConnectionError
            AddHandler Client.Disconnected, AddressOf handleClientDisconnected
            AddHandler Client.DisconnectError, AddressOf handleClientDisconnectError
            AddHandler Client.IncomingData, AddressOf handleClientIncomingData
            AddHandler Client.IncomingDataError, AddressOf handleClientIncomingDataError
            AddHandler Client.SendDataError, AddressOf handleClientSendDataError

            Client.Connect(txtClientIP.Text, txtClientPort.Text)

            '#################################### information about new user ###########################
            If Client.isConnected Then
                Client.Send("@code1843@" & txtClientName.Text)
                clientLogMessage(txtClientName.Text)
                txtClientSend.Text = ""

                txtClientIP.Enabled = False
                txtClientName.Enabled = False
                txtClientPort.Enabled = False
            End If
            '###########################################################################################
        End If
    End Sub

    Private Sub txtClientSend_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClientSend.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            If Client IsNot Nothing Then
                If Client.isConnected Then
                    Client.Send(txtClientName.Text & ":  " & txtClientSend.Text)
                    clientLogMessage(txtClientName.Text & ":  " & txtClientSend.Text)
                    txtClientSend.Text = ""
                End If
            End If
        End If
    End Sub

    '************************************************************
    'Primary Socket Functionality
    '************************************************************
    Private Sub handleClientConnected()
        clientLogMessage("Connected!")
    End Sub

    Private Sub handleClientDisconnected()
        clientLogMessage("Disconnected!")
    End Sub

    Private Sub handleClientIncomingData(ByRef Data As String)
        If InStr(Data, "@code1841@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1841@", "")
            users = Data
        ElseIf InStr(Data, "@code1840@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1840@", "")
            refresh1 = Data
        ElseIf InStr(Data, "@code1847@") > 0 And Data.Length > 0 Then
            Data$ = Replace(Data$, "@code1847@", "")
            formNo = Trim(Mid(Data, Data.Length - 2))
            poruka = Mid(Data, 1, Data.Length - 2)
            br = Trim(Mid(Data, Data.Length - 2))
        Else
            If Data.Length > 0 Then
                clientLogMessage(Data)
            End If
        End If
    End Sub


    '************************************************************
    'Functional Error Reporting (Below)
    '************************************************************
    Private Sub handleClientConnectionError(ByVal Message As String)
        clientLogMessage(Message)
    End Sub

    Private Sub handleClientDisconnectError(ByVal Message As String)
        clientLogMessage(Message)
    End Sub

    Private Sub handleClientIncomingDataError(ByVal Message As String)
        clientLogMessage(Message)
    End Sub

    Private Sub handleClientSendDataError(ByVal Message As String)
        clientLogMessage(Message)
    End Sub
#End Region

    Private Sub frmCommunicator_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub btnClientDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientDisconnect.Click
        Try
            If Client.isConnected Then
                Client.Send("@code1842@" & txtClientName.Text)
                clientLogMessage("Odlogirani ste!")
                txtClientSend.Text = ""
            End If
        Catch ex As Exception

        End Try

        Client.Disconnect()

        Try
            txtClientIP.Enabled = True
            txtClientName.Enabled = True
            txtClientPort.Enabled = True
        Catch ex As Exception

        End Try
        ListBox2.Items.Clear()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If user <> Nothing Then
            dodaj_korisnika()
            usersUpdate()
        End If
        If list <> Nothing Then

        End If
        If disconected <> Nothing Then
            izbrisi_korisnika()
        End If
        If sendall <> Nothing Then
            senditall()
        End If
        If countListItems > ListBox1.Items.Count Then
            userLeave()
            countListItems -= 1
        End If
        If privSock3 <> Nothing Then
            MsgBox("True")
            sendPriv()
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If users <> Nothing Then
            addUsers()
        End If
        If refresh1 <> Nothing Then
            refUsers()
        End If
        If poruka <> Nothing Then
            findForm1()
        End If
    End Sub

    Private Sub txtServeSend_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServeSend.TextChanged

    End Sub

    Private Sub rtbServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtbServer.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Client.isConnected = True And ListBox2.SelectedItem.ToString <> Nothing Then
                If Private1.Visible = False Then
                    Private1.Text = ListBox2.SelectedItem.ToString
                    Private1.Show()
                Else
                    Private2.Text = ListBox2.SelectedItem.ToString
                    Private2.Show()
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub privatno1(ByVal br As String)

        Client.Send("@code1839@" & txtClientName.Text & ": " & Private1.TextBox1.Text & "   " & br)

        Private1.TextBox1.Text = ""

    End Sub
    Public Sub privatno2(ByVal br As String)

        Client.Send("@code1839@" & txtClientName.Text & ": " & Private2.TextBox1.Text & "   " & br)

        Private2.TextBox1.Text = ""

    End Sub

End Class
