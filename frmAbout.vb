Imports System.Windows.Forms

Friend Class FrmAbout

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        frmStart.lbDonation.Visible = False
        Dispose()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim HTMLFile As String
        frmStart.lbDonation.Visible = False
        HTMLFile = "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ptsim%40ptsim%2ecom&item_name=Donation%20for%20SBuilderX&page_style=SBuilderX&no_shipping=1&return=http%3a%2f%2fwww%2eptsim%2ecom%2fsbuilderx%2fthankyou%2easp&cancel_return=http%3a%2f%2fwww%2eptsim%2ecom%2fsbuilderx%2fthankyou%2easp&cn=Optional%20Note&tax=0&currency_code=EUR&lc=GB&bn=PP%2dDonationsBF&charset=UTF%2d8"
        Process.Start(HTMLFile)

    End Sub

End Class
