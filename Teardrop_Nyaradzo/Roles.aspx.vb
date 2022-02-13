Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Roles
    Inherits System.Web.UI.Page
    Public Shared RoleEditID As Integer
    Public Shared RoleName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateID()
            loadExistingRoles()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Roles (Role_Name, Position,Dashboard,Created_By)VALUES('" & txtrolename.Text & "','" & txtposition.Text & "','" & cmbroletype.SelectedValue & "','" & Session("username") & "')", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Role  Successfully Saved') ; location.href='Roles.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnDel_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Delete from Roles where Role_ID='" & txtroleID.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Role  Successfully Deleted') ; location.href='Roles.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

    End Sub
    Protected Sub PopulateID()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Isnull(Max(Role_Id),0)+1 ID from Roles ", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtroleID.Text = dt.Rows(0).Item("ID")


                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSearchRole_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select Role_ID, isnull(Role_Name,'')+' '+isnull(Position,'')as display from Roles where isnull(Role_Name,'')+' '+isnull(Position,'') like '%" & txtSearchRole.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "Role_ID"
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
    Protected Sub loadExistingRoles()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Roles", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdRoles.DataSource = ds.Tables(0)
                        grdRoles.DataBind()
                        grdRoles.Visible = True
                    Else
                        grdRoles.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub lstSurnames_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim Roleid As Integer = lstSurnames.SelectedValue
        loadRoleDetails(Roleid)
    End Sub
    Protected Sub loadRoleDetails(Roleid)

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Roles where Role_ID = '" & Roleid & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtroleID.Text = dt.Rows(0).Item("Role_ID")
                        txtrolename.Text = dt.Rows(0).Item("Role_Name")
                        txtposition.Text = dt.Rows(0).Item("Position")
                        txtSearchRole.Text = dt.Rows(0).Item("Role_Name")
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        txtroleID.Text = ""
        txtrolename.Text = ""
        txtposition.Text = ""
        txtSearchRole.Text = ""
    End Sub
    Protected Sub grdRoles_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdRoles.RowEditing
        Try
            RoleEditID = DirectCast(grdRoles.Rows(e.NewEditIndex).FindControl("lblRoleID"), Label).Text

            grdRoles.EditIndex = e.NewEditIndex
            loadExistingRoles()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdRoles_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdRoles.RowCancelingEdit
        grdRoles.EditIndex = -1
        loadExistingRoles()
    End Sub
    Protected Sub grdRoles_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdRoles.RowUpdating
        Try

            RoleEditID = DirectCast(grdRoles.Rows(e.RowIndex).FindControl("lblRoleID"), Label).Text
            If Trim(RoleEditID) = "" Or IsDBNull(RoleEditID) Then
                msgbox("No user selected for update")
                Exit Sub
            End If

            RoleName = DirectCast(grdRoles.Rows(e.RowIndex).FindControl("txtRoleedit"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Roles set Role_Name='" & RoleName & "' where Role_ID='" & RoleEditID & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Role successfully updated")

                    Else
                        msgbox("Error updating Role")
                    End If
                    con.Close()
                    grdRoles.EditIndex = -1
                    loadExistingRoles()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdRoles_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRoles.RowDeleting
        RoleEditID = DirectCast(grdRoles.Rows(e.RowIndex).FindControl("lblRoleID"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from Roles where Role_ID='" & RoleEditID & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Role successfully deleted")

                Else
                    msgbox("Error deleting Role")
                End If
                con.Close()
                loadExistingRoles()
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