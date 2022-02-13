Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_Login_Click(sender As Object, e As EventArgs)
        Try
            If txtusername.Value = "" And txtpassword.Value = "" Then
                msgbox("Enter Login Credentials")
                Exit Sub
            End If
            If txtpassword.Value = "" Then
                msgbox("Please Provide Password")
                txtpassword.Focus()
                Exit Sub
            End If
            If txtusername.Value = "" Then
                msgbox("Please Provide Userlogin Credentials")
                txtusername.Focus()
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select password,bb.Branch_Name,bb.Branch_Code,* from tbl_users uu left join Branches bb ON uu.user_branch=bb.Branch_Code where user_login='" & txtusername.Value.ToString & "' and lock_user='1'", con)
                    Dim dsPwd As New DataSet
                    Dim dt As New DataTable

                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count() > 0 Then
                            Dim password As String = dt.Rows(0).Item("password")
                            Dim username As String = dt.Rows(0).Item("user_login")
                            Dim lock_user As String = dt.Rows(0).Item("lock_user")
                            Dim branch As String = dt.Rows(0).Item("Branch_Name")
                            Dim branchcode As String = dt.Rows(0).Item("Branch_Code")
                            Dim role As String = dt.Rows(0).Item("user_role")
                            If lock_user = "0" Then
                                msgbox("This User is Locked , contact the Administrator")
                            End If


                            If password <> txtpassword.Value.ToString Then
                                msgbox("Incorrect Password, please retype your Login Credentials")
                                txtpassword.Value = ""
                                Exit Sub
                            ElseIf password = "" Then
                                msgbox("Enter Password")
                                Exit Sub
                            ElseIf username <> txtusername.Value.ToString Then
                                msgbox("Invalid Username")
                                txtusername.Focus()
                                Exit Sub
                            ElseIf username = "" Then
                                msgbox("Enter Username")
                                txtusername.Focus()
                                Exit Sub
                            Else
                                password = txtpassword.Value.ToString And username = txtusername.Value.ToString
                                Session("username") = txtusername.Value.ToString
                                Session("branch") = branch
                                Session("userrole") = role
                                Session("BrnchCode") = branchcode
                                Response.Redirect("index.aspx", True)

                            End If
                        Else
                            msgbox("User Not Found")
                        End If
                    End Using









                End Using
            End Using

        Catch ex As Exception
            'MsgBox(ex.Message)
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

    Protected Sub lnkbtnpassword_Click(sender As Object, e As EventArgs)
        Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("Select Top 1 phone from tbl_users where user_role='admin' and lock_user=1", con)
                Dim dsPwd As New DataSet
                Dim dt As New DataTable
                Using adp = New SqlDataAdapter(cmd)
                    adp.Fill(dt)
                    Dim adminContact As String = dt.Rows(0).Item("phone")
                    msgbox("Please contact the System Administrator in order to RESET your password.You can contact your nearest System Administrator on:" & adminContact)
                End Using
            End Using
        End Using
    End Sub
End Class