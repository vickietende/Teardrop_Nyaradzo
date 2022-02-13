Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Imports Microsoft.VisualBasic
Public Class Permissions
    Inherits System.Web.UI.Page
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)

    Shared adp As New SqlDataAdapter
    Shared IPAdd, machName, browser, url, pageViewID As String
    Shared actionTime As Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            loadModuleCategories()
            loadRoles()
            loadModules()
            If cmbRoles.SelectedItem.Text = "--select option--" Then
                cmbRoles.SelectedValue = 0
            End If
        End If
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

                    Else
                        cmbcategory.DataSource = Nothing

                    End If

                    cmbcategory.DataBind()
                    cmbcategory.Items.Insert(0, "--select option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadRoles()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Roles", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbRoles.DataSource = ds.Tables(0)
                        cmbRoles.DataTextField = "Role_Name"
                        cmbRoles.DataValueField = "Role_ID"

                    Else
                        cmbRoles.DataSource = Nothing

                    End If

                    cmbRoles.DataBind()
                    cmbRoles.Items.Insert(0, "--select option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadModules()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select m.ModuleID,m.Module_Name,m.URL,mc.Module_Category from Modules m Left Join Module_Category mc ON m.Module_Category=mc.ID", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdModuleDetails.DataSource = ds.Tables(0)
                        grdModuleDetails.DataBind()
                        grdModuleDetails.Visible = True 
                    Else
                        grdModuleDetails.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub cmbRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            GetNewModules()

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select sp.ALWView, m.ModuleID,m.Module_Name,m.URL,mc.Module_Category from Modules m Left Join Module_Category mc ON m.Module_Category=mc.ID Left Join Sys_Permissions sp ON m.ModuleID=sp.Module_ID where sp.Role_ID='" & cmbRoles.SelectedValue & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdPermissions.DataSource = ds.Tables(0)
                        grdPermissions.DataBind()
                        grdPermissions.Visible = True
                        grdModuleDetails.Visible = False
                        grdModuleDetails.DataSource = Nothing
                    Else
                        grdPermissions.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub GetNewModules()
        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(sp.ALWView,0), m.ModuleID,m.Module_Name,m.URL,mc.Module_Category from Modules m Left Join Module_Category mc ON m.Module_Category=mc.ID Left Join Sys_Permissions sp ON m.ModuleID=sp.Module_ID where m.Module_Name not in (select ModuleName from Sys_Permissions where Role_ID='" & cmbRoles.SelectedValue & "')", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then

                        grdNewModules.DataSource = ds.Tables(0)
                        grdNewModules.DataBind()
                        grdNewModules.Visible = True

                    Else
                        grdNewModules.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

    End Sub
    Protected Sub btnupdate_Click(sender As Object, e As EventArgs)
        Try
            If YouExist(cmbRoles.SelectedValue) Then
                msgbox("Existing Permissions Deleted")
            End If
            Dim cnt = 0
            If grdNewModules.Visible = True Then

                For Each row As GridViewRow In grdNewModules.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkView As CheckBox = DirectCast(row.FindControl("chkMod"), CheckBox)
                        Dim chkd = chkView.Checked
                        Dim userId As String
                        Dim moduleId As Integer = 0
                        Dim moduleName As String = ""
                        Dim RoleIdId As Integer = 0
                        'Dim URL_NAME As String = ""
                        Dim roleName = cmbRoles.SelectedItem.Text.Trim()
                        userId = Session("username")

                        Dim lblMod As Label = CType(row.FindControl("lblModId"), Label)
                        Dim lblModName As Label = CType(row.FindControl("lblModName"), Label)
                        moduleId = Convert.ToInt32(lblMod.Text)
                        moduleName = lblModName.Text
                        Dim URL_NAME1 As Label = CType(row.FindControl("lblURLName1"), Label)
                        Dim sUrl As String = URL_NAME1.Text
                        RoleIdId = Convert.ToInt32(cmbRoles.SelectedValue.ToString().Trim())
                        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                            Using cmd As New SqlCommand("SavePermissions", con)
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("@Role_ID", RoleIdId)
                                cmd.Parameters.AddWithValue("@Role_Name", roleName)
                                cmd.Parameters.AddWithValue("@Module_ID", moduleId)
                                cmd.Parameters.AddWithValue("@ModuleName", moduleName)
                                cmd.Parameters.AddWithValue("@URLName", sUrl)
                                cmd.Parameters.AddWithValue("@ALWView", chkd)
                                cmd.Parameters.AddWithValue("@Created_By", userId)
                                If con.State = ConnectionState.Open Then
                                    con.Close()
                                End If
                                con.Open()
                                cmd.ExecuteNonQuery()
                                con.Close()

                            End Using
                        End Using


                        cnt += 1
                    End If

                Next
                msgbox(" New Permissions Successfully added for Role , " & " " & cmbRoles.SelectedItem.Text)
                grdNewModules.Visible = False
            End If
            For Each row As GridViewRow In grdPermissions.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkView As CheckBox = DirectCast(row.FindControl("chkModule"), CheckBox)
                    Dim chkd = chkView.Checked
                    Dim userId As String
                    Dim moduleId As Integer = 0
                    Dim moduleName As String = ""
                    Dim RoleIdId As Integer = 0
                    'Dim URL_NAME As String = ""
                    Dim roleName = cmbRoles.SelectedItem.Text.Trim()
                    userId = Session("username")

                    Dim lblModule As Label = CType(row.FindControl("lblModuleId"), Label)
                    Dim lblModuleName As Label = CType(row.FindControl("lblModuleName"), Label)
                    moduleId = Convert.ToInt32(lblModule.Text)
                    moduleName = lblModuleName.Text
                    Dim URL_NAME As Label = CType(row.FindControl("lblURLName"), Label)
                    Dim sUrl As String = URL_NAME.Text
                    RoleIdId = Convert.ToInt32(cmbRoles.SelectedValue.ToString().Trim())
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SavePermissions", con)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@Role_ID", RoleIdId)
                            cmd.Parameters.AddWithValue("@Role_Name", roleName)
                            cmd.Parameters.AddWithValue("@Module_ID", moduleId)
                            cmd.Parameters.AddWithValue("@ModuleName", moduleName)
                            cmd.Parameters.AddWithValue("@URLName", sUrl)
                            cmd.Parameters.AddWithValue("@ALWView", chkd)
                            cmd.Parameters.AddWithValue("@Created_By", userId)
                            If con.State = ConnectionState.Open Then
                                con.Close()
                            End If
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()

                        End Using
                    End Using


                    cnt += 1
                End If

            Next
            msgbox("Permissions Successfully added for Role , " & " " & cmbRoles.SelectedItem.Text)
            cmbRoles.SelectedValue = Nothing
            grdPermissions.DataSource = Nothing
            grdPermissions.Visible = False
            loadModules()


        Catch ex As Exception
        End Try
    End Sub

    Protected Function YouExist(RoleID As Integer)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select * from Sys_Permissions where Role_ID='" & cmbRoles.SelectedValue & "'", con)
                    cmd.Parameters.AddWithValue("@Role_ID", RoleID)
                    Dim ds As New DataSet
                    ds = New DataSet
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(ds, "customer")
                    End Using
                    If ds.Tables(0).Rows.Count > 0 Then
                        Using cmd1 = New SqlCommand("delete from Sys_Permissions where Role_ID='" & cmbRoles.SelectedValue & "'", con)
                            If con.State = ConnectionState.Open Then
                                con.Close()
                            End If
                            con.Open()
                            cmd1.ExecuteNonQuery()
                            con.Close()
                        End Using
                    Else
                        Return False
                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try
        Return False
    End Function
    Protected Function IExist(RoleID As Integer)
        Try
            Dim cnt = -1
            For Each row As GridViewRow In grdModuleDetails.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim ds As New DataSet
                    Dim modName = DirectCast(row.FindControl("lblModuleName"), Label).Text
                    Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd = New SqlCommand("select * from Sys_Permissions where Role_ID='" & cmbRoles.SelectedValue & "' and ModuleName='" & modName & "'", con)
                            cmd.Parameters.AddWithValue("@Role_ID", RoleID)
                            ds = New DataSet
                            Using adp = New SqlDataAdapter(cmd)
                                adp.Fill(ds, "customer")
                            End Using
                            If ds.Tables(0).Rows.Count > 0 Then
                                Return True

                            Else
                                Return False
                            End If
                        End Using
                    End Using
                    cnt += 1
                End If
            Next
        Catch ex As Exception

            Return False

        End Try
        Return False
    End Function
    Protected Sub grdModuleDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdModuleDetails.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chkView As CheckBox
                chkView = DirectCast(e.Row.FindControl("chkModule"), CheckBox)
                Dim modName = DirectCast(e.Row.FindControl("lblModuleName"), Label).Text
                'msgbox(modName)
                Dim roleID = cmbRoles.SelectedValue
                'chkView.Checked = getRolePermissions(roleID, modName)
            End If
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If YouExist(cmbRoles.SelectedValue) Then
                msgbox("Existing Permissions Deleted")
                Exit Sub
            End If
            Dim cnt = 0
            For Each row As GridViewRow In grdModuleDetails.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkView As CheckBox = DirectCast(row.FindControl("chkModule"), CheckBox)
                    Dim chkd = chkView.Checked
                    Dim userId As String
                    Dim moduleId As Integer = 0
                    Dim moduleName As String = ""
                    Dim RoleIdId As Integer = 0
                    'Dim URL_NAME As String = ""
                    Dim roleName = cmbRoles.SelectedItem.Text.Trim()
                    userId = Session("username")

                    Dim lblModule As Label = CType(row.FindControl("lblModuleId"), Label)
                    Dim lblModuleName As Label = CType(row.FindControl("lblModuleName"), Label)
                    moduleId = Convert.ToInt32(lblModule.Text)
                    moduleName = lblModuleName.Text
                    Dim URL_NAME As Label = CType(row.FindControl("lblURLName"), Label)
                    Dim sUrl As String = URL_NAME.Text
                    RoleIdId = Convert.ToInt32(cmbRoles.SelectedValue.ToString().Trim())
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SavePermissions", con)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@Role_ID", RoleIdId)
                            cmd.Parameters.AddWithValue("@Role_Name", roleName)
                            cmd.Parameters.AddWithValue("@Module_ID", moduleId)
                            cmd.Parameters.AddWithValue("@ModuleName", moduleName)
                            cmd.Parameters.AddWithValue("@URLName", sUrl)
                            cmd.Parameters.AddWithValue("@ALWView", chkd)
                            cmd.Parameters.AddWithValue("@Created_By", userId)
                            If con.State = ConnectionState.Open Then
                                con.Close()
                            End If
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()

                        End Using
                    End Using


                    cnt += 1
                End If

            Next


            msgbox("Permissions Successfully added for Role , " & " " & cmbRoles.SelectedItem.Text)

            cmbRoles.SelectedValue = Nothing
            grdPermissions.DataSource = Nothing
            grdPermissions.Visible = False
            loadModules()


        Catch ex As Exception
        End Try
    End Sub
    Shared Sub recordAction(ByVal button As String, ByVal action As String)
        Try
            'actionTime = DateFormat.getSaveDateTime(HttpContext.Current.Timestamp)
            IPAdd = HttpContext.Current.Request.UserHostAddress
            machName = System.Environment.MachineName
            browser = HttpContext.Current.Request.UserAgent
            url = HttpContext.Current.Request.Url.AbsoluteUri
            Dim User = HttpContext.Current.Session("username")
            pageViewID = HttpContext.Current.Session("PageViewID")

            Dim cmd As New SqlCommand
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            cmd = New SqlCommand("insert into SECURITY_LOG (SESSION_ID,USERID,CLIENT_IP_ADDRESS,CLIENT_MACH_NAME,PAGE,BUTTON,ACTION,ACTION_DATE,BROWSER,PAGE_VIEW_ID) values ('" & HttpContext.Current.Session("SessionID") & "','" & User & "','" & IPAdd & "','" & machName & "','" & url & "','" & button & "','" & User & "',getdate(),'" & browser & "','" & pageViewID & "')", con)

            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class