Public NotInheritable Class Splash

    Private Sub Splash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim x As Double = My.Computer.Screen.Bounds.Width
        Location = New Point(x - 173, 10)
    End Sub

End Class
