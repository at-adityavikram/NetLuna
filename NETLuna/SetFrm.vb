Public Class SetFrm
    Dim ip As Double
    Dim loaded As Boolean = False
    Private Sub SetFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As Double = My.Computer.Screen.Bounds.Width
        Location = New Point(x - Width - 8, 10)
        If My.Settings.minluna = False Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        loaded = True
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        My.Settings.back = 1
        Form1.BackgroundImage = PictureBox1.Image
        Form1.Button1.BackColor = PictureBox1.BackColor
        Form1.Button2.BackColor = PictureBox1.BackColor
        Form1.Button3.BackColor = PictureBox1.BackColor
        Form1.btnToDay.BackColor = PictureBox1.BackColor
        Form1.lblAge.ForeColor = SystemColors.ButtonHighlight
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        My.Settings.back = 2
        Form1.BackgroundImage = Nothing
        Form1.BackColor = PictureBox2.BackColor
        Form1.Button1.BackColor = PictureBox2.BackColor
        Form1.Button2.BackColor = PictureBox2.BackColor
        Form1.Button3.BackColor = PictureBox2.BackColor
        Form1.btnToDay.BackColor = PictureBox2.BackColor
        Form1.lblAge.ForeColor = SystemColors.ButtonHighlight
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        My.Settings.back = 3
        Form1.BackgroundImage = Nothing
        Form1.BackColor = PictureBox3.BackColor
        Form1.Button1.BackColor = PictureBox3.BackColor
        Form1.Button2.BackColor = PictureBox3.BackColor
        Form1.Button3.BackColor = PictureBox3.BackColor
        Form1.btnToDay.BackColor = PictureBox3.BackColor
        Form1.lblAge.ForeColor = SystemColors.ButtonHighlight
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        My.Settings.back = 4
        Form1.BackgroundImage = Nothing
        Form1.BackColor = PictureBox4.BackColor
        Form1.Button1.BackColor = PictureBox2.BackColor
        Form1.Button2.BackColor = PictureBox2.BackColor
        Form1.Button3.BackColor = PictureBox2.BackColor
        Form1.btnToDay.BackColor = PictureBox2.BackColor
        Form1.lblAge.ForeColor = Color.Black
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True And loaded = True Then
            My.Settings.minluna = False
            Form1.MyCalendar.SetDate("2 / 7 / 27")
            Form1.MyCalendar.SetDate(Today)
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True And loaded = True Then
            My.Settings.minluna = True
            Form1.MyCalendar.SetDate("2 / 7 / 27")
            Form1.MyCalendar.SetDate(Today)
        End If
    End Sub


End Class