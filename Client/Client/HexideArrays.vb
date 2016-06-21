Public Module HexideArrays

    ''' <summary>
    ''' Returns true if an array both exists and has at least one member.
    ''' </summary>
    ''' <param name="Arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isArraySafe(ByRef Arr As Array) As Boolean
        If Arr Is Nothing Then Return False
        If Arr.Length < 1 Then Return False

        Return True
    End Function

    ''' <summary>
    ''' Adds an item to the end of an array.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="Arr"></param>
    ''' <param name="Item"></param>
    ''' <remarks></remarks>
    Public Sub addToArray(Of T)(ByRef Arr() As T, ByRef Item As T)
        If Arr Is Nothing Then
            ReDim Arr(0)
        Else
            ReDim Preserve Arr(Arr.Length)
        End If

        Arr(Arr.Length - 1) = Item
    End Sub

End Module