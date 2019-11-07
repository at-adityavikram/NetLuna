Public Class Form1
    Dim ip As Double
    Dim ag As Double

    Private Function JulianDate(ByVal d As Integer, ByVal m As Integer, ByVal y As Integer) As Integer
        Dim mm, yy As Integer
        Dim k1, k2, k3 As Integer
        Dim j As Integer

        yy = y - Math.Floor((12 - m) / 10)
        mm = m + 9
        If (mm >= 12) Then
            mm = mm - 12
        End If
        k1 = Math.Floor(365.25 * (yy + 4712))
        k2 = Math.Floor(30.6001 * mm + 0.5)
        k3 = Math.Floor(Math.Floor((yy / 100) + 49) * 0.75) - 38
        ' "j" for dates in Julian calendar:
        j = k1 + k2 + d + 59
        If (j > 2299160) Then
            ' For Gregorian calendar:
            j = j - k3  ' "j" is the Julian date at 12h UT (Universal Time)
        End If
        Return j
    End Function

    Private Function MoonAge(ByVal d As Integer, ByVal m As Integer, ByVal y As Integer) As Double
        Dim j As Integer = JulianDate(d, m, y)
        'Calculate the approximate phase of the moon
        ip = (j + 4.867) / 29.53059
        ip = ip - Math.Floor(ip)
        'After several trials I've seen to add the following lines, 
        'which gave the result was not bad
        If (ip < 0.5) Then
            ag = ip * 29.53059 + 29.53059 / 2
        Else
            ag = ip * 29.53059 - 29.53059 / 2
        End If
        ' Moon's age in days
        ag = Math.Floor(ag) + 1
        Return ag
    End Function

    Private Sub PrintAge()
        Dim theAge As String = "Moon age"
        theAge = theAge + " " + ":" + " " + ag.ToString()
        If (ag = 1) Then
            theAge = theAge + " " + "day"
        Else
            theAge = theAge + " " + "days"
        End If
        Me.lblAge.Text = theAge
    End Sub

    Private Sub ClearDraw()
        PicMoon.Image = Nothing
    End Sub

    Private Sub DrawMoon()
        Dim Xpos, Ypos, Rpos As Integer
        Dim Xpos1, Xpos2 As Integer
        Dim Phase As Double

        Phase = ip
        ' Width of 'ImageToDraw' Object = Width of 'PicMoon' control
        Dim PageWidth As Integer = PicMoon.Width
        ' Height of 'ImageToDraw' Object = Height of 'PicMoon' control
        Dim PageHeight As Integer = PicMoon.Height
        ' Initiate 'ImageToDraw' Object with size = size of control 'PicMoon' control
        Dim ImageToDraw As Bitmap = New Bitmap(PageWidth, PageHeight)
        'Create graphics object for alteration.
        Dim newGraphics As Graphics = Graphics.FromImage(ImageToDraw)

        Dim PenB As Pen = New Pen(Color.Black) ' For darkness part of the moon
        Dim PenW As Pen = New Pen(Color.White) ' For the lighted part of the moon

        If My.Settings.minluna = False Then
            PenB = New Pen(Color.Black)
            PenW = New Pen(Color.White)
        Else
            PenB = New Pen(SetFrm.PictureBox2.BackColor)
            PenW = New Pen(Color.Gainsboro)
        End If

        For Ypos = 0 To 45
            Xpos = Math.Floor(Math.Sqrt(45 * 45 - Ypos * Ypos))
            ' Draw darkness part of the moon
            Dim pB1 As Point = New Point(90 - Xpos, Ypos + 90)
            Dim pB2 As Point = New Point(Xpos + 90, Ypos + 90)
            Dim pB3 As Point = New Point(90 - Xpos, 90 - Ypos)
            Dim pB4 As Point = New Point(Xpos + 90, 90 - Ypos)
            newGraphics.DrawLine(PenB, pB1, pB2)
            newGraphics.DrawLine(PenB, pB3, pB4)
            ' Determine the edges of the lighted part of the moon
            Rpos = 2 * Xpos
            If (Phase < 0.5) Then
                Xpos1 = -Xpos
                Xpos2 = Math.Floor(Rpos - 2 * Phase * Rpos - Xpos)
            Else
                Xpos1 = Xpos
                Xpos2 = Math.Floor(Xpos - 2 * Phase * Rpos + Rpos)
            End If
            ' Draw the lighted part of the moon
            Dim pW1 As Point = New Point(Xpos1 + 90, 90 - Ypos)
            Dim pW2 As Point = New Point(Xpos2 + 90, 90 - Ypos)
            Dim pW3 As Point = New Point(Xpos1 + 90, Ypos + 90)
            Dim pW4 As Point = New Point(Xpos2 + 90, Ypos + 90)
            newGraphics.DrawLine(PenW, pW1, pW2)
            newGraphics.DrawLine(PenW, pW3, pW4)
        Next
        ' Display the bitmap in the picture box.
        PicMoon.Image = ImageToDraw
        ' Release graphics object
        PenB.Dispose()
        PenW.Dispose()
        newGraphics.Dispose()
        ImageToDraw = Nothing
    End Sub

    Private Sub YourChoice()
        'user select date from MonthCalendar control
        Dim Aday, Amonth, Ayear As Integer
        Aday = Me.MyCalendar.SelectionStart.Day
        Amonth = Me.MyCalendar.SelectionStart.Month
        Ayear = Me.MyCalendar.SelectionStart.Year
        Me.MoonAge(Aday, Amonth, Ayear)
    End Sub

    Private Sub ShowMoon()
        'draw moon and print age in selected days
        Me.YourChoice() 'select date
        Me.ClearDraw() 'clear PicMoon PictureBox
        Me.DrawMoon() 'draw the moon
        Me.PrintAge() 'print age of moon in days
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As Double = My.Computer.Screen.Bounds.Width
        Location = New Point(x - 416, 10)
        If My.Settings.back = 2 Then
            BackgroundImage = Nothing
            BackColor = SetFrm.PictureBox2.BackColor
            Button1.BackColor = SetFrm.PictureBox2.BackColor
            Button2.BackColor = SetFrm.PictureBox2.BackColor
            Button3.BackColor = SetFrm.PictureBox2.BackColor
            btnToDay.BackColor = SetFrm.PictureBox2.BackColor
            lblAge.ForeColor = Color.White
        ElseIf My.Settings.back = 3 Then
            BackgroundImage = Nothing
            BackColor = SetFrm.PictureBox3.BackColor
            Button1.BackColor = SetFrm.PictureBox3.BackColor
            Button2.BackColor = SetFrm.PictureBox3.BackColor
            Button3.BackColor = SetFrm.PictureBox3.BackColor
            btnToDay.BackColor = SetFrm.PictureBox3.BackColor
            lblAge.ForeColor = Color.Black
        ElseIf My.Settings.Back = 4 Then
            BackgroundImage = Nothing
            BackColor = SetFrm.PictureBox4.BackColor
            Button1.BackColor = SetFrm.PictureBox2.BackColor
            Button2.BackColor = SetFrm.PictureBox2.BackColor
            Button3.BackColor = SetFrm.PictureBox2.BackColor
            btnToDay.BackColor = SetFrm.PictureBox2.BackColor
            lblAge.ForeColor = Color.Black
        End If
        Me.ShowMoon()
        Button1.PerformClick()
    End Sub

    Private Sub MyCalendar_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MyCalendar.DateChanged
        Me.ShowMoon()
    End Sub

    Private Sub btnToDay_Click(sender As Object, e As EventArgs) Handles btnToDay.Click
        Me.MyCalendar.SetDate(Me.MyCalendar.TodayDate.Date)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "" Then
            Width -= 243
            Button1.Location = New Point(Button1.Location.X - 243, Button1.Location.Y)
            Button1.Text = ""
            Location = New Point(Location.X + 243, Location.Y)
            Button3.Visible = False
        Else
            Width += 243
            Button1.Location = New Point(Button1.Location.X + 243, Button1.Location.Y)
            Button1.Text = ""
            Location = New Point(Location.X - 243, Location.Y)
            Button3.Visible = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SetFrm.Show()
    End Sub

End Class
