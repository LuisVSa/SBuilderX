


Friend Class FrmImage

    Private img As Image
    Private myWidth As Integer
    Private myHeight As Integer

    Private Sub FrmImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

        Dispose()

    End Sub

    Private Sub FrmImage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        img = System.Drawing.Image.FromFile(ImageFileName)
        Width = img.Width
        Height = img.Height + 32
        myWidth = img.Width
        myHeight = img.Height

    End Sub



    Private Sub FrmImage_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        e.Graphics.DrawImage(img, 0, 0, myWidth, myHeight)

    End Sub
End Class