Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Branches
    Inherits System.Web.UI.Page
    Public Shared BrnchCode As Integer
    Public Shared BranchName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateID()
            loadExistingBranches()
        End If
    End Sub

    Protected Sub btnAddBranch_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Branches (Branch_Name)VALUES('" & txtbranchname.Text & "')", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Branch  Successfully Saved') ; location.href='Branches.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnDelBranch_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Delete from Branches where Branch_Code='" & txtbranchID.Text & "'", con)



                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Branch  Successfully Deleted') ; location.href='Branches.aspx'</script>")
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
                Using cmd = New SqlCommand("Select Isnull(Max(Branch_Code),0)+1 ID from Branches ", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtbranchID.Text = dt.Rows(0).Item("ID")


                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSearchBranch_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select Branch_Code, isnull(Branch_Name,'') as display from Branches where isnull(Branch_Name,'') like '%" & txtSearchBranch.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "Branch_Code"
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
    Protected Sub loadExistingBranches()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Branches", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdBranches.DataSource = ds.Tables(0)
                        grdBranches.DataBind()
                        grdBranches.Visible = True
                    Else
                        grdBranches.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub lstSurnames_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim Branch_Code As Integer = lstSurnames.SelectedValue
        loadBranchDetails(Branch_Code)
    End Sub

    Protected Sub loadBranchDetails(Branch_Code As Integer)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Branches where Branch_Code = '" & Branch_Code & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtbranchID.Text = dt.Rows(0).Item("Branch_Code")
                        txtbranchname.Text = dt.Rows(0).Item("Branch_Name")

                        txtSearchBranch.Text = dt.Rows(0).Item("Branch_Name")
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs)
        txtbranchID.Text = ""
        txtbranchname.Text = ""

        txtSearchBranch.Text = ""
    End Sub
    Protected Sub grdBranches_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdBranches.RowEditing
        Try
            BrnchCode = DirectCast(grdBranches.Rows(e.NewEditIndex).FindControl("lblCode"), Label).Text

            grdBranches.EditIndex = e.NewEditIndex
            loadExistingBranches()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdBranches_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdBranches.RowCancelingEdit
        grdBranches.EditIndex = -1
        loadExistingBranches()
    End Sub

    Protected Sub grdBranches_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdBranches.RowUpdating
        Try

            BrnchCode = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("lblCode"), Label).Text
            If Trim(BrnchCode) = "" Or IsDBNull(BrnchCode) Then
                msgbox("No Branch selected for update")
                Exit Sub
            End If

            BranchName = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("txtBranchNameEdit"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Branches set Branch_Name='" & BranchName & "' where Branch_Code='" & BrnchCode & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Branch successfully updated")

                    Else
                        msgbox("Error updating Branch")
                    End If
                    con.Close()
                    grdBranches.EditIndex = -1
                    loadExistingBranches()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdBranches_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdBranches.RowDeleting
        BrnchCode = DirectCast(grdBranches.Rows(e.RowIndex).FindControl("lblCode"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from Branches where Branch_Code='" & BrnchCode & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Branch successfully deleted")

                Else
                    msgbox("Error deleting Branch")
                End If
                con.Close()
                loadExistingBranches()
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