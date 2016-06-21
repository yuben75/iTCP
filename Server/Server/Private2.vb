Public Class Private2

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            frmCommunicator.privatno2(Trim(Mid(Me.Text, CInt(Me.Text.Length) - 2)))
        End If
    End Sub

End Class