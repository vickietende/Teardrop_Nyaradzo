Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports MailKit
Imports MimeKit
Imports MailKit.Net.Smtp
Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnchange_Click(sender As Object, e As EventArgs)
        Try
            ViewState("Newpassword") = txtNewpassword.Text
            If txtpassword.Text = "" Then
                msgbox("enter old password")
                txtpassword.Focus()
                Exit Sub

            End If
            If txtNewpassword.Text = "" Then
                msgbox("enter New password")
                txtNewpassword.Focus()
                Exit Sub

            End If
            If txtConfirm.Text = "" Then
                msgbox("Confirm New password")
                txtConfirm.Focus()
                Exit Sub

            End If
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update tbl_users set password = '" & txtNewpassword.Text & "' where user_login='" & Session("username") & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then
                        Response.Write("<script>alert('Password successfully changed') ; location.href='login.aspx'</script>")
                        Dim message = New MimeMessage()
                        message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                        message.[To].Add(New MailboxAddress("Teardrop. User", ViewState("Email")))
                        message.Subject = "User Password Changed"
                        Dim BodyBuilder As New BodyBuilder()
                        BodyBuilder.HtmlBody = Session("username") & " you have successfully changed your Teardrop. password to <strong>" & ViewState("Newpassword") & "Log in with the new password to confirm your credentials.<br> Regards.<br> Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
                        BodyBuilder.TextBody = "Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"

                        message.Body = BodyBuilder.ToMessageBody()

                        Try
                            Using client = New SmtpClient()
                                client.Connect("smtp.gmail.com", 587)
                                client.AuthenticationMechanisms.Remove("XOAUTH2")
                                client.Authenticate("codedimensions.info@gmail.com", "tracieganga&85")

                                client.Send(message)
                                client.Disconnect(True)
                            End Using
                        Catch ex As Exception
                            msgbox("Error:Check your Internet connection...")
                        End Try



                    Else
                        msgbox("Error Changing Password")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Public Sub msgbox(ByVal strMessage As String)
        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub txtpassword_TextChanged(sender As Object, e As EventArgs)
        If CheckUserPassword(Session("username"), txtpassword.Text) Then
            msgbox("Incorrect password!!")
            txtpassword.Text = ""
            txtpassword.Focus()
        Else
            txtpassword.Text = ViewState("password")
        End If


    End Sub
    Protected Function CheckUserPassword(ByVal user_login As String, user_text As String) As Boolean
        Try
            Dim password As String = ""
            Dim email As String = ""

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select password,email from tbl_users where user_login='" & user_login & "'", con)

                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        password = dt.Rows(0).Item("password")
                        ViewState("password") = password
                        email = dt.Rows(0).Item("email")
                        ViewState("Email") = email
                        If password <> user_text Then
                            Return True
                        Else
                            Return False
                        End If
                    End Using

                End Using
            End Using
        Catch ex As Exception

            Return False
        End Try
    End Function

    Protected Function PasswordVerify(ByVal password1 As String, password2 As String) As Boolean
        Try
            If password1 <> password2 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Protected Sub txtConfirm_TextChanged(sender As Object, e As EventArgs)
        If PasswordVerify(txtNewpassword.Text, txtConfirm.Text) Then
            msgbox("ERROR:Passwords do not match!!")
            txtNewpassword.Text = ""
            txtConfirm.Text = ""
            txtNewpassword.Focus()
        End If

    End Sub

    Protected Sub txtNewpassword_TextChanged(sender As Object, e As EventArgs)
        If txtNewpassword.Text = txtpassword.Text Then
            msgbox("Change your password")
            txtNewpassword.Text = ""
        End If
    End Sub
End Class