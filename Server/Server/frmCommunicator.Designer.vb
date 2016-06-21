<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCommunicator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnStopServe = New System.Windows.Forms.Button()
        Me.txtServeSend = New System.Windows.Forms.TextBox()
        Me.rtbServer = New System.Windows.Forms.RichTextBox()
        Me.btnServe = New System.Windows.Forms.Button()
        Me.txtServePort = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnClientDisconnect = New System.Windows.Forms.Button()
        Me.btnClientConnect = New System.Windows.Forms.Button()
        Me.txtClientName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtClientIP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClientPort = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtClientSend = New System.Windows.Forms.TextBox()
        Me.rtbClient = New System.Windows.Forms.RichTextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnStopServe)
        Me.GroupBox1.Controls.Add(Me.txtServeSend)
        Me.GroupBox1.Controls.Add(Me.rtbServer)
        Me.GroupBox1.Controls.Add(Me.btnServe)
        Me.GroupBox1.Controls.Add(Me.txtServePort)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(422, 384)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Server"
        '
        'btnStopServe
        '
        Me.btnStopServe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStopServe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnStopServe.Location = New System.Drawing.Point(257, 9)
        Me.btnStopServe.Name = "btnStopServe"
        Me.btnStopServe.Size = New System.Drawing.Size(75, 27)
        Me.btnStopServe.TabIndex = 7
        Me.btnStopServe.Text = "Stop"
        Me.btnStopServe.UseVisualStyleBackColor = True
        '
        'txtServeSend
        '
        Me.txtServeSend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtServeSend.Location = New System.Drawing.Point(8, 352)
        Me.txtServeSend.Name = "txtServeSend"
        Me.txtServeSend.Size = New System.Drawing.Size(406, 20)
        Me.txtServeSend.TabIndex = 6
        '
        'rtbServer
        '
        Me.rtbServer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbServer.Enabled = False
        Me.rtbServer.Location = New System.Drawing.Point(8, 44)
        Me.rtbServer.Name = "rtbServer"
        Me.rtbServer.ReadOnly = True
        Me.rtbServer.Size = New System.Drawing.Size(406, 300)
        Me.rtbServer.TabIndex = 5
        Me.rtbServer.Text = ""
        '
        'btnServe
        '
        Me.btnServe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnServe.Location = New System.Drawing.Point(176, 9)
        Me.btnServe.Name = "btnServe"
        Me.btnServe.Size = New System.Drawing.Size(75, 27)
        Me.btnServe.TabIndex = 4
        Me.btnServe.Text = "Listen"
        Me.btnServe.UseVisualStyleBackColor = True
        '
        'txtServePort
        '
        Me.txtServePort.Location = New System.Drawing.Point(126, 12)
        Me.txtServePort.Name = "txtServePort"
        Me.txtServePort.Size = New System.Drawing.Size(44, 20)
        Me.txtServePort.TabIndex = 3
        Me.txtServePort.Text = "1989"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(91, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Port:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnClientDisconnect)
        Me.GroupBox2.Controls.Add(Me.btnClientConnect)
        Me.GroupBox2.Controls.Add(Me.txtClientName)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtClientIP)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtClientPort)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtClientSend)
        Me.GroupBox2.Controls.Add(Me.rtbClient)
        Me.GroupBox2.Location = New System.Drawing.Point(614, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(416, 384)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Client"
        '
        'btnClientDisconnect
        '
        Me.btnClientDisconnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClientDisconnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClientDisconnect.Location = New System.Drawing.Point(319, 40)
        Me.btnClientDisconnect.Name = "btnClientDisconnect"
        Me.btnClientDisconnect.Size = New System.Drawing.Size(89, 23)
        Me.btnClientDisconnect.TabIndex = 16
        Me.btnClientDisconnect.Text = "Disconnect"
        Me.btnClientDisconnect.UseVisualStyleBackColor = True
        '
        'btnClientConnect
        '
        Me.btnClientConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClientConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClientConnect.Location = New System.Drawing.Point(224, 40)
        Me.btnClientConnect.Name = "btnClientConnect"
        Me.btnClientConnect.Size = New System.Drawing.Size(89, 23)
        Me.btnClientConnect.TabIndex = 15
        Me.btnClientConnect.Text = "Connect"
        Me.btnClientConnect.UseVisualStyleBackColor = True
        '
        'txtClientName
        '
        Me.txtClientName.Location = New System.Drawing.Point(76, 40)
        Me.txtClientName.Name = "txtClientName"
        Me.txtClientName.Size = New System.Drawing.Size(128, 20)
        Me.txtClientName.TabIndex = 14
        Me.txtClientName.Text = "A User"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClientIP
        '
        Me.txtClientIP.Location = New System.Drawing.Point(76, 16)
        Me.txtClientIP.Name = "txtClientIP"
        Me.txtClientIP.Size = New System.Drawing.Size(128, 20)
        Me.txtClientIP.TabIndex = 12
        Me.txtClientIP.Text = "127.0.0.1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Address :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClientPort
        '
        Me.txtClientPort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtClientPort.Location = New System.Drawing.Point(368, 16)
        Me.txtClientPort.Name = "txtClientPort"
        Me.txtClientPort.Size = New System.Drawing.Size(40, 20)
        Me.txtClientPort.TabIndex = 10
        Me.txtClientPort.Text = "2080"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(336, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 20)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Port:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClientSend
        '
        Me.txtClientSend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtClientSend.Location = New System.Drawing.Point(8, 352)
        Me.txtClientSend.Name = "txtClientSend"
        Me.txtClientSend.Size = New System.Drawing.Size(400, 20)
        Me.txtClientSend.TabIndex = 8
        '
        'rtbClient
        '
        Me.rtbClient.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbClient.Location = New System.Drawing.Point(8, 64)
        Me.rtbClient.Name = "rtbClient"
        Me.rtbClient.Size = New System.Drawing.Size(400, 280)
        Me.rtbClient.TabIndex = 7
        Me.rtbClient.Text = ""
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(436, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(136, 381)
        Me.ListBox1.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(1040, 12)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(120, 342)
        Me.ListBox2.TabIndex = 3
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1040, 360)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 27)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Private"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmCommunicator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 399)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmCommunicator"
        Me.Text = "Itcp Chat Server - By Sexy Tex(Teh_VB_Helper) of Killer Gaming"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnServe As System.Windows.Forms.Button
    Friend WithEvents txtServePort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtServeSend As System.Windows.Forms.TextBox
    Friend WithEvents rtbServer As System.Windows.Forms.RichTextBox
    Friend WithEvents txtClientIP As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClientPort As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtClientSend As System.Windows.Forms.TextBox
    Friend WithEvents rtbClient As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClientConnect As System.Windows.Forms.Button
    Friend WithEvents txtClientName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnClientDisconnect As System.Windows.Forms.Button
    Friend WithEvents btnStopServe As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
