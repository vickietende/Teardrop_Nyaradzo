Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class AccountsManager
    Inherits System.Web.UI.Page
    Public Shared AccNumberEdit, AccNameEdit, AccDescEdit As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadCategories()
            loadCategoryAccs()
        End If
    End Sub
    Protected Sub loadCategories()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select CategoryID,Category+'--'+CategoryDescription Display from [dbo].[AccountCategories]", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbCategories.DataSource = ds.Tables(0)
                        cmbCategories.DataTextField = "Display"
                        cmbCategories.DataValueField = "CategoryID"

                    Else
                        cmbCategories.DataSource = Nothing

                    End If

                    cmbCategories.DataBind()
                    cmbCategories.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadCategoryAccs()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select ma.AccNumber,ma.AccName,ma.Description,ac.CategoryDescription from [dbo].[tbl_MainAccounts] ma Left Join AccountCategories ac ON ma.CategoryID=ac.CategoryID ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdAccountCategories.DataSource = ds.Tables(0)
                        grdAccountCategories.Visible = True
                        grdAccountCategories.DataBind()
                    Else
                        grdAccountCategories.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnAddSection_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO AccountCategories(Category, CategoryDescription)Values('" & txtAddCategory.Text & "','" & txtCategotyDesc.Text & "')", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        msgbox("Category Successfully Saved")
                        txtAddCategory.Text = ""
                        txtCategotyDesc.Text = ""
                    End If

                    con.Close()
                    loadCategories()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If cmbCategories.SelectedValue = Nothing Or cmbCategories.SelectedItem.Text = "--Select Option--" Then
                msgbox("Select Category")
                Exit Sub
            End If
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SaveMainAccount", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@AccName", txtMainAccName.Text)
                    cmd.Parameters.AddWithValue("@Description", txtMaindesc.Text)
                    cmd.Parameters.AddWithValue("@CategoryID", cmbCategories.SelectedValue)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Account Successfully Saved') ; location.href='AccountsManager.aspx'</script>")
                    con.Close()
                    txtMainAccName.Text = ""
                    txtMaindesc.Text = ""

                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub grdAccountCategories_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdAccountCategories.RowEditing
        Try
            AccNumberEdit = DirectCast(grdAccountCategories.Rows(e.NewEditIndex).FindControl("lblAccNumberEdit"), Label).Text

            grdAccountCategories.EditIndex = e.NewEditIndex
            loadCategoryAccs()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdAccountCategories_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdAccountCategories.RowCancelingEdit
        grdAccountCategories.EditIndex = -1
        loadCategoryAccs()
    End Sub
    Protected Sub grdAccountCategories_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdAccountCategories.RowUpdating
        Try

            AccNumberEdit = DirectCast(grdAccountCategories.Rows(e.RowIndex).FindControl("lblAccNumberEdit"), Label).Text
            If Trim(AccNumberEdit) = "" Or IsDBNull(AccNumberEdit) Then
                msgbox("No Account selected for update")
                Exit Sub
            End If

            AccNameEdit = DirectCast(grdAccountCategories.Rows(e.RowIndex).FindControl("txtAccNameEdit"), TextBox).Text
            AccDescEdit = DirectCast(grdAccountCategories.Rows(e.RowIndex).FindControl("txtAccDescEdit"), TextBox).Text



            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update tbl_MainAccounts set AccName='" & AccNameEdit & "',Description='" & AccDescEdit & "' where AccNumber='" & AccNumberEdit & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Account successfully updated")

                    Else
                        msgbox("Error updating Account")
                    End If
                    con.Close()
                    grdAccountCategories.EditIndex = -1
                    loadCategoryAccs()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdAccountCategories_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAccountCategories.RowDeleting
        AccNumberEdit = DirectCast(grdAccountCategories.Rows(e.RowIndex).FindControl("lblAccNumberEdit"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from tbl_MainAccounts where AccNumber='" & AccNumberEdit & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Account successfully deleted")

                Else
                    msgbox("Error deleting Account")
                End If
                con.Close()
                loadCategoryAccs()
            End Using
        End Using
    End Sub

End Class