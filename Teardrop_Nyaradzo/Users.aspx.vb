Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports MailKit
Imports MimeKit
Imports MailKit.Net.Smtp
Public Class Users
    Inherits System.Web.UI.Page
    Public Shared UserEditID As Integer
    Public Shared userName, userSurname, user_login, user_email, user_branch As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtcapturedate.Text = Date.Now.ToString("dd MMMM yyyy")
            loadExistingUsers()
            loadBranches()
            loadRoles()
        End If

    End Sub
    Protected Sub loadRoles()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Roles", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbrole.DataSource = ds.Tables(0)
                        cmbrole.DataTextField = "Role_Name"
                        cmbrole.DataValueField = "Role_ID"

                    Else
                        cmbrole.DataSource = Nothing

                    End If

                    cmbrole.DataBind()
                    cmbrole.Items.Insert(0, "--select option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadBranches()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Branches", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbbranch.DataSource = ds.Tables(0)
                        cmbbranch.DataTextField = "Branch_Name"
                        cmbbranch.DataValueField = "Branch_Code"

                    Else
                        cmbbranch.DataSource = Nothing

                    End If

                    cmbbranch.DataBind()
                    cmbbranch.Items.Insert(0, "--select option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadExistingUsers()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select user_id,laname,fname,user_login,user_role,user_branch,email,phone,Convert(varchar,date_created,106)date_created from tbl_users", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdUsers.DataSource = ds.Tables(0)
                        grdUsers.DataBind()
                        grdUsers.Visible = True
                    Else
                        grdUsers.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnSaveUser_Click(sender As Object, e As EventArgs)
        ViewState("user_login") = txtuserlogin.Text
        ViewState("Name") = txtusername.Text & txtusersurname.Text
        ViewState("user_Role") = cmbrole.SelectedItem.Text
        ViewState("Email") = txtmail.Text
        ViewState("password") = txtuserpassword.Text

        Try
            If cmbusergender.SelectedItem.Text = "Select" Then
                msgbox("Select User Gender!!")
                Exit Sub
            ElseIf cmbbranch.SelectedIndex = 0 Then
                msgbox("Select User Branch")
                Exit Sub
            ElseIf cmbrole.SelectedIndex = 0 Then
                msgbox("Select User role ")
                Exit Sub

            End If
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Insert INTO tbl_users ( [user_login],[fname],[laname],[IDNO],[password],[user_type],[user_branch],[user_role], [lock_user], [email], [phone], [created_by], [date_created], [modify_date])VALUES('" & txtuserlogin.Text & "','" & txtusername.Text & "','" & txtusersurname.Text & "','" & txtuserID.Text & "','" & txtuserpassword.Text & "','" & cmbrole.SelectedItem.Text & "','" & cmbbranch.SelectedValue & "','" & cmbrole.SelectedItem.Text & "',1,'" & txtmail.Text & "','" & txtuserphone.Text & "','','" & txtcapturedate.Text & "','" & txtcapturedate.Text & "')", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then


                        Dim message = New MimeMessage()
                        message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                        message.[To].Add(New MailboxAddress("Dear User", ViewState("Email")))
                        message.Subject = " Teardrop.System User Confirmation"
                        Dim BodyBuilder As New BodyBuilder()
                        BodyBuilder.HtmlBody = "<strong>" & ViewState("Name") & "</strong>,you have been successfully added as a Teardrop System User.Your system role will be <strong>" & ViewState("user_Role") & "</strong>,username :<strong> " & ViewState("user_login") & "</strong>,password :<strong> " & ViewState("password") & "</strong>.<br> We hope you enjoy your experience with Teardrop. <br> Regards.<br>Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
                        BodyBuilder.TextBody = "Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"

                        message.Body = BodyBuilder.ToMessageBody()


                        Using client = New SmtpClient()
                            client.Connect("smtp.gmail.com", 587)
                            client.AuthenticationMechanisms.Remove("XOAUTH2")
                            client.Authenticate("codedimensions.info@gmail.com", "tracieganga&85")

                            client.Send(message)
                            client.Disconnect(True)
                        End Using
                        Response.Write("<script>alert('User  Successfully Saved') ; location.href='Users.aspx'</script>")
                        con.Close()
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnEditUser_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update tbl_users Set [user_login]='" & txtuserlogin.Text & "',[fname]='" & txtusername.Text & "',[laname]='" & txtusersurname.Text & "',[password]='" & txtuserpassword.Text & "',[user_type]='" & cmbrole.SelectedItem.Text & "',[user_branch]='" & cmbbranch.SelectedItem.Text & "',[user_role]='" & cmbrole.SelectedItem.Text & "', [email]='" & txtmail.Text & "', [phone]='" & txtuserphone.Text & "' where IDNO='" & txtuserID.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('User  Successfully Edited') ; location.href='Users.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnLockUser_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update tbl_users set lock_user='0' where user_login='" & txtuserlogin.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('User  Successfully Locked out of the System') ; location.href='Users.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

    End Sub

    Protected Sub btnSearchUserSurname_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select user_id, isnull(laname,'')+' '+isnull(fname,'')+' --- '+isnull(user_login,'')+' --- '+isnull(IDNO,'') as display from tbl_users where isnull(laname,'')+' '+isnull(fname,'')+' --- '+isnull(IDNO,'') like '%" & txtSearchUserSurname.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "user_id"
                    Else
                        lstSurnames.DataSource = Nothing
                        msgbox("The searched name was not found")
                    End If
                    'clearAll()
                    lstSurnames.DataBind()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lstSurnames_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim user_id As Integer = lstSurnames.SelectedValue
        loadUserDetails(user_id)
    End Sub
    Protected Sub loadUserDetails(user_id As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select *  from tbl_users where user_id = '" & user_id & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)

                        txtusername.Text = dt.Rows(0).Item("fname")
                        txtusersurname.Text = dt.Rows(0).Item("laname")
                        txtuserphone.Text = dt.Rows(0).Item("phone")
                        txtuserID.Text = dt.Rows(0).Item("IDNO")
                        txtuserID.Enabled = False
                        txtmail.Text = dt.Rows(0).Item("email")
                        txtcapturedate.Text = dt.Rows(0).Item("date_created")
                        txtcapturedate.Enabled = False
                        txtuserdesc.Text = dt.Rows(0).Item("user_type")
                        txtuserpassword.Text = dt.Rows(0).Item("password")
                        txtuserlogin.Text = dt.Rows(0).Item("user_login")


                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ClearAll()

        txtusername.Text = ""
        txtusersurname.Text = ""
        txtuserphone.Text = ""
        txtuserID.Text = ""

        txtmail.Text = ""
        txtcapturedate.Text = ""

        txtuserdesc.Text = ""
        txtuserpassword.Text = ""
        txtuserlogin.Text = ""
    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs)
        ClearAll()
    End Sub

    Protected Sub btnunlock_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update tbl_users set lock_user='1' where user_login='" & txtuserlogin.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('User  Successfully UnLocked ') ; location.href='Users.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub



    Protected Sub grdUsers_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdUsers.RowCancelingEdit
        grdUsers.EditIndex = -1
        loadExistingUsers()
    End Sub

    Protected Sub grdUsers_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdUsers.RowEditing
        Try
            UserEditID = DirectCast(grdUsers.Rows(e.NewEditIndex).FindControl("lblid"), Label).Text

            grdUsers.EditIndex = e.NewEditIndex
            loadExistingUsers()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdUsers_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdUsers.RowUpdating
        Try
            Dim UserEditID As Integer
            UserEditID = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("lblid"), Label).Text
            If Trim(UserEditID) = "" Or IsDBNull(UserEditID) Then
                msgbox("No user selected for update")
                Exit Sub
            End If
            UserEditID = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtuserid"), TextBox).Text
            userName = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtfnameEdit"), TextBox).Text
            userSurname = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtsurnameedit"), TextBox).Text
            user_login = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtuser"), TextBox).Text
            user_email = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtEmailEdit"), TextBox).Text
            user_branch = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("txtbranch1"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update tbl_Users set fname='" & userName & "',laname='" & userSurname & "',[user_login]='" & user_login & "',email='" & user_email & "',user_branch='" & user_branch & "' where user_id='" & UserEditID & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("User successfully updated")

                    Else
                        msgbox("Error updating Role")
                    End If
                    con.Close()
                    grdUsers.EditIndex = -1
                    loadExistingUsers()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdUsers_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdUsers.RowDeleting
        UserEditID = DirectCast(grdUsers.Rows(e.RowIndex).FindControl("lblid"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from tbl_Users where user_id='" & UserEditID & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("User successfully deleted")

                Else
                    msgbox("Error deleting User")
                End If
                con.Close()
                loadExistingUsers()
            End Using
        End Using
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


End Class