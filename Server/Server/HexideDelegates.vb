Namespace Delegates

    Namespace RichTextBoxes
        Public Module Container
            Private Delegate Sub DELappendText(ByRef Frm As Form, ByRef Box As RichTextBox, ByRef Text As String)
            Public Sub appendText(ByRef Frm As Form, ByRef Box As RichTextBox, ByRef Text As String)
                If Frm Is Nothing Then Exit Sub
                If Frm.IsDisposed Then Exit Sub
                If Box Is Nothing Then Exit Sub
                If Box.IsDisposed Then Exit Sub

                If Frm.InvokeRequired Then
                    Dim DT As New DELappendText(AddressOf appendText)
                    Frm.Invoke(DT, New Object() {Frm, Box, Text})
                Else
                    Box.Text &= Text
                End If
            End Sub
        End Module
    End Namespace

End Namespace

