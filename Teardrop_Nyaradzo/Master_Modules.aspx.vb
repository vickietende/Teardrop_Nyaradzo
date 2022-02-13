Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Master_Modules
    Inherits System.Web.UI.Page
    Public Shared ModuleEditID As Integer
    Public Shared ModuleName, ModuleURL, ModuleCategory As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateID()
            loadExistingModules()
            loadModuleCategories()

        End If
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Modules (Module_Name,URL,Module_Category,Created_By)VALUES('" & txtmodule.Text & "','" & txturl.Text & "','" & cmbcategory.SelectedValue & "','" & Session("username") & "')", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Module Successfully Saved') ; location.href='Master_Modules.aspx'</script>")
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
                Using cmd = New SqlCommand("Delete from Modules where ModuleID='" & txtmoduleID.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Module Successfully Deleted') ; location.href='Master_Modules.aspx'</script>")
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
                Using cmd = New SqlCommand("Select Isnull(Max(ModuleID),0)+1 ID from Modules ", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtmoduleID.Text = dt.Rows(0).Item("ID")


                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub loadExistingModules()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Modules", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdModules.DataSource = ds.Tables(0)
                        grdModules.DataBind()
                        grdModules.Visible = True
                    Else
                        grdModules.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnSearchModule_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select ModuleID, isnull(Module_Name,'')+' '+isnull(ModuleID,'')as display from Modules where isnull(Module_Name,'')+' '+isnull(Module_Category,'') like '%" & txtSearchModule.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "ModuleID"
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
        loadModuleDetails(lstSurnames.SelectedValue)
    End Sub
    Protected Sub loadModuleDetails(ModuleID)

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Modules where ModuleID = '" & ModuleID & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtmoduleID.Text = dt.Rows(0).Item("ModuleID")
                        txtmodule.Text = dt.Rows(0).Item("Module_Name")
                        txturl.Text = dt.Rows(0).Item("URL")
                        txtSearchModule.Text = dt.Rows(0).Item("Module_Name")
                        cmbcategory.SelectedValue = dt.Rows(0).Item("Module_Category")
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        txtmoduleID.Text = ""
        txtmodule.Text = ""
        txturl.Text = ""
        txtSearchModule.Text = ""
        cmbcategory.SelectedValue = Nothing
    End Sub
    Protected Sub grdModules_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdModules.RowEditing
        Try
            ModuleEditID = DirectCast(grdModules.Rows(e.NewEditIndex).FindControl("lblModuleID"), Label).Text

            grdModules.EditIndex = e.NewEditIndex
            loadExistingModules()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdModules_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdModules.RowCancelingEdit
        grdModules.EditIndex = -1
        loadExistingModules()
    End Sub
    Protected Sub grdModules_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdModules.RowUpdating
        Try

            ModuleEditID = DirectCast(grdModules.Rows(e.RowIndex).FindControl("lblModuleID"), Label).Text
            If Trim(ModuleEditID) = "" Or IsDBNull(ModuleEditID) Then
                msgbox("No module selected for update")
                Exit Sub
            End If

            ModuleName = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txtmoduleName"), TextBox).Text
            ModuleURL = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txturlEdit"), TextBox).Text
            ModuleCategory = DirectCast(grdModules.Rows(e.RowIndex).FindControl("txtcategoryEdit"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Modules set Module_Name='" & ModuleName & "',URL='" & ModuleURL & "',Module_Category='" & ModuleCategory & "' where ModuleID='" & ModuleEditID & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Module successfully updated")

                    Else
                        msgbox("Error updating Module")
                    End If
                    con.Close()
                    grdModules.EditIndex = -1
                    loadExistingModules()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub loadModuleCategories()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Module_Category", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbcategory.DataSource = ds.Tables(0)
                        cmbcategory.DataTextField = "Module_Category"
                        cmbcategory.DataValueField = "ID"
                        cmbcategoryEdit.DataSource = ds.Tables(0)
                        cmbcategoryEdit.DataTextField = "Module_Category"
                        cmbcategoryEdit.DataValueField = "ID"
                    Else
                        cmbcategory.DataSource = Nothing
                        cmbcategoryEdit.DataSource = Nothing
                    End If

                    cmbcategory.DataBind()
                    cmbcategoryEdit.DataBind()
                    cmbcategory.Items.Insert(0, "--select option--")
                    cmbcategoryEdit.Items.Insert(0, "--select option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddCategory_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Module_Category(Module_Category)Values('" & txtAddCategory.Text & "')", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then

                        Response.Write("<script>alert('Category Successfully Saved') ; location.href='Master_Modules.aspx'</script>")
                    End If

                    con.Close()

                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnEditCategory_Click(sender As Object, e As EventArgs)
        Try
            If cmbcategoryEdit.SelectedIndex <> -1 Then

                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Update  Module_Category set Module_Category ='" & txtAddCategory.Text & "' where ID = '" & cmbcategoryEdit.SelectedValue & "'", con)

                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then

                            Response.Write("<script>alert('Category Successfully Updated') ; location.href='Master_Modules.aspx'</script>")
                        End If

                        con.Close()

                    End Using
                End Using
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub grdModules_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdModules.RowDeleting
        ModuleEditID = DirectCast(grdModules.Rows(e.RowIndex).FindControl("lblModuleID"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from Modules where ModuleID='" & ModuleEditID & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Module successfully deleted")

                Else
                    msgbox("Error deleting Module")
                End If
                con.Close()
                loadExistingModules()
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