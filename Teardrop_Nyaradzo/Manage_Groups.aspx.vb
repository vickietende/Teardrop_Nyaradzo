Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Manage_Groups
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadBranches()
            loadPara_Products()
            'msgbox(cmbPolicyPlans.SelectedIndex)
        End If
    End Sub
    Protected Sub loadPara_Products()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Para_Products", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbPolicyPlans.DataSource = ds.Tables(0)
                        cmbPolicyPlans.DataTextField = "ProdName"
                        cmbPolicyPlans.DataValueField = "ProdID"

                    Else
                        cmbPolicyPlans.DataSource = Nothing

                    End If

                    cmbPolicyPlans.DataBind()

                    cmbPolicyPlans.Items.Insert(0, "--Select Option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdGroupMembers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdGroupMembers.PageIndexChanging

        Try
            grdGroupMembers.PageIndex = e.NewPageIndex
            loadGroupMembers(lstGroups.SelectedValue)


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




                        cmbgrpBranch.DataSource = ds.Tables(0)
                        cmbgrpBranch.DataTextField = "Branch_Name"
                        cmbgrpBranch.DataValueField = "Branch_Code"

                    Else

                        cmbgrpBranch.DataSource = Nothing

                    End If


                    cmbgrpBranch.DataBind()

                    cmbgrpBranch.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddGrp_Click(sender As Object, e As EventArgs)
        Try
            If GroupExists(txtgrpname.Text, cmbPolicyPlans.SelectedValue) = True Then
                msgbox("Group Exists")
                Exit Sub
            End If
            If CheckGroupExists(cmbPolicyPlans.SelectedValue, txtcompany.Text) = True Then
                msgbox("Group Exists")
                Exit Sub
            End If
            If cmbPolicyPlans.SelectedIndex = 0 Then
                msgbox("Select Plan for Group")
                Exit Sub
            End If

            If cmbgrpBranch.SelectedIndex = 0 Then
                msgbox("Select Branch")
                Exit Sub
            End If

            If txtgrpname.Text = "" Then
                msgbox("Enter Group Name")
                txtgrpname.Focus()
                Exit Sub
            End If
            If txtgrpPremium.Text = "" Then
                msgbox("Enter Premium")
                txtgrpPremium.Focus()
                Exit Sub
            End If
            If txtcompany.Text = "" Then

                msgbox("Enter Company Name")
                txtcompany.Focus()
                Exit Sub
            End If
            If txtgroupcontact.Text = "" Then
                msgbox("Enter Group Contact")
                txtgroupcontact.Focus()
                Exit Sub
            End If
            If txtRepName.Text = "" Then
                msgbox("Enter Representative")
                txtRepName.Focus()
                Exit Sub
            End If
            If txtRepContact.Text = "" Then
                msgbox("Enter Representative Contact")
                txtRepContact.Focus()
                Exit Sub
            End If
            If txtBusAddress.Text = "" Then
                msgbox("Enter Business Address")
                txtBusAddress.Focus()
                Exit Sub
            End If


            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SaveNewGroup", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@GroupName", txtgrpname.Text)
                    cmd.Parameters.AddWithValue("@CompanyName", txtcompany.Text)
                    cmd.Parameters.AddWithValue("@Contact", txtgroupcontact.Text)
                    cmd.Parameters.AddWithValue("@HRRep", txtRepName.Text)
                    cmd.Parameters.AddWithValue("@RepContact", txtRepContact.Text)
                    cmd.Parameters.AddWithValue("@Address", txtBusAddress.Text)
                    cmd.Parameters.AddWithValue("@Premium", txtgrpPremium.Text)
                    cmd.Parameters.AddWithValue("@ProdID", cmbPolicyPlans.SelectedValue)
                    cmd.Parameters.AddWithValue("@Branch", cmbgrpBranch.SelectedValue)
                    cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        ViewState("NewGroupNo") = getNewGroupNo()
                        Dim EncQuery As New BankEncryption64
                        lblAgree.Text = ViewState("NewGroupNo")
                        lblEncAgree.Text = EncQuery.Encrypt(ViewState("NewGroupNo").replace(" ", "+"))

                        ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type=""text/javascript"">showPopup()</script>")
                        con.Close()
                    End If

                End Using
            End Using
            getGroupNo()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub getGroupNo()
        Try
            Dim GroupNumber As String = ""
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(GrpID),GroupNumber from tbl_Groups group by GroupNumber", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            GroupNumber = dt.Rows(0).Item("GroupNumber")
                            msgbox("The new Group's Group Number is: " & GroupNumber)
                        Else
                            msgbox("Group Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Private Function CheckGroupExists(ByVal Prod As Integer, Company As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("Select * from tbl_Groups where ProdID='" & Prod & "' and CompanyName='" & Company & "'", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Groups")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function

    Protected Sub btnEditGrp_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("EditGroupDetails", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@GroupName", txtgrpname.Text)
                    cmd.Parameters.AddWithValue("@GroupNumber", txtgrpNumber.Text)
                    cmd.Parameters.AddWithValue("@HRRep", txtRepName.Text)
                    cmd.Parameters.AddWithValue("@RepContact", txtRepContact.Text)
                    cmd.Parameters.AddWithValue("@Address", txtBusAddress.Text)
                    cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Group Details Successfully Updated') ; location.href='Manage_Groups.aspx'</script>")
                    con.Close()
                    txtgrpNumber.Text = ""
                    txtgrpname.Text = ""
                    txtcompany.Text = ""
                    txtgroupcontact.Text = ""
                    txtRepName.Text = ""
                    txtRepContact.Text = ""
                    txtBusAddress.Text = ""
                    txtgrpPremium.Text = ""
                    cmbPolicyPlans.SelectedValue = Nothing
                    cmbgrpBranch.SelectedValue = Nothing
                End Using
            End Using



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearchGroup_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select GroupNumber, isnull(GroupName,'')+' '+isnull(GroupNumber,'')+' --- '+isnull(CompanyName,'')+' --- '+isnull(Address,'') as display from tbl_Groups where  isnull(GroupName,'')+' '+isnull(GroupNumber,'')+' --- '+isnull(CompanyName,'')+' --- '+isnull(Address,'') like '%" & txtSearchGroup.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstGroups.Visible = True
                        lstGroups.DataSource = ds.Tables(0)
                        lstGroups.DataTextField = "display"
                        lstGroups.DataValueField = "GroupNumber"
                    Else
                        lstGroups.DataSource = Nothing
                        msgbox("The searched name was not found")
                    End If
                    'clearAll()
                    lstGroups.DataBind()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lstGroups_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ChckPremiumEdit.Checked = False Then

            txtgrpname.Enabled = True
            btnAddGrp.Enabled = True
            btnEditGrp.Enabled = True


            txtcompany.Enabled = True
            txtgroupcontact.Enabled = True
            txtRepName.Enabled = True
            txtRepContact.Enabled = True
            txtBusAddress.Enabled = True
            loadGroupDetails(lstGroups.SelectedValue)
            loadGroupMembers(lstGroups.SelectedValue)
            loadGroupCount(lstGroups.SelectedValue)




        Else



            txtgrpNumber.Enabled = False
            txtgrpname.Enabled = False
            txtcompany.Enabled = False
            txtgroupcontact.Enabled = False
            txtRepName.Enabled = False
            txtRepContact.Enabled = False
            txtBusAddress.Enabled = False
            btnAddGrp.Enabled = False
            btnEditGrp.Enabled = False

            loadGroupDetails(lstGroups.SelectedValue)
            loadGroupMembers(lstGroups.SelectedValue)
            loadGroupCount(lstGroups.SelectedValue)
        End If

    End Sub
    Protected Sub loadGroupCount(GroupNumber As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select COUNT(ID)Members from Customer_Details where GroupNumber= '" & GroupNumber & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtGroupTotal.Text = dt.Rows(0).Item("Members")







                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroupDetails(GroupNumber As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select tg.GroupNumber,tg.GroupName,tg.CompanyName,tg.Contact,tg.HRRep,tg.RepContact,tg.Address,tg.Branch,Cast(tg.Premium as decimal(10,2) )GroupPremium,Cast(pp.SumAssured as decimal(10,2) )SumAssured,pp.MaturityPeriod,pp.StartPeriod,pp.Has_CashBack,pp.CashBackPeriod,pp.CashBackPercent,Cast(pp.Grocery_Amt as decimal(10,2) )Grocery_Amt,pp.Has_Grocery,pp.ProdName,tg.ProdID GrpProdID,pp.CoffinType  from [dbo].[tbl_Groups] tg left join Para_Products pp ON tg.ProdID=pp.ProdID where GroupNumber= '" & GroupNumber & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtgrpNumber.Text = dt.Rows(0).Item("GroupNumber")
                            txtgrpname.Text = dt.Rows(0).Item("GroupName")
                            txtcompany.Text = dt.Rows(0).Item("CompanyName")
                            txtgroupcontact.Text = dt.Rows(0).Item("Contact")
                            txtRepName.Text = dt.Rows(0).Item("HRRep")
                            txtRepContact.Text = dt.Rows(0).Item("RepContact")
                            txtBusAddress.Text = dt.Rows(0).Item("Address")
                            txtgrpPremium.Text = dt.Rows(0).Item("GroupPremium")
                            cmbPolicyPlans.SelectedValue = dt.Rows(0).Item("GrpProdID")
                            cmbgrpBranch.SelectedValue = dt.Rows(0).Item("Branch")


                            txtgrpNumber.Enabled = False
                            txtgrpname.Enabled = True
                            txtcompany.Enabled = True
                            txtgroupcontact.Enabled = True
                            txtRepName.Enabled = True
                            txtRepContact.Enabled = True
                            txtBusAddress.Enabled = True
                            btnAddGrp.Enabled = False
                            btnEditGrp.Enabled = True

                            cmbPolicyPlans.Enabled = False
                            btnUpgradeGrpProd.Enabled = False






                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroupMembers(GroupNumber As String)

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select PolicyNo,Surname+' '+FName Name,Gender,IDNO,PhoneNo,Address from Customer_Details where GroupNumber='" & GroupNumber & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdGroupMembers.DataSource = ds.Tables(0)
                        grdGroupMembers.DataBind()
                        grdGroupMembers.Visible = True
                    Else
                        grdGroupMembers.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        txtgrpNumber.Text = ""
        txtgrpname.Text = ""
        txtcompany.Text = ""
        txtgroupcontact.Text = ""
        txtRepName.Text = ""
        txtRepContact.Text = ""
        txtBusAddress.Text = ""
        txtgrpPremium.Text = ""
        cmbPolicyPlans.SelectedValue = Nothing
        cmbgrpBranch.SelectedValue = Nothing
        lstGroups.DataSource = Nothing
        lstGroups.Visible = False
        txtGroupTotal.Text = ""
        grdGroupMembers.DataSource = Nothing
        grdGroupMembers.Visible = False
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
    Protected Function GroupExists(GroupName As String, Prod As Integer)
        Try



            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select * from tbl_Groups where GroupName='" & GroupName & "' and ProdID ='" & Prod & "'", con)

                    Dim ds = New DataSet
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


        Catch ex As Exception

            Return False

        End Try
        Return False
    End Function

    Protected Sub btnEditPremium_Click(sender As Object, e As EventArgs)
        Try
            ViewState("ProdID") = cmbPolicyPlans.SelectedValue
            ViewState("GroupNumber") = lstGroups.SelectedValue
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update tbl_Groups set Premium='" & txtgrpPremium.Text & "' where GroupNumber='" & lstGroups.SelectedValue & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()

                    If cmd.ExecuteNonQuery Then

                        msgbox("Product Premiums successfully updated")
                        UpdateCustomerPremiums(ViewState("ProdID"), ViewState("GroupNumber"))
                    Else
                        msgbox("Error updating Premium")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ChckPremiumEdit_CheckedChanged(sender As Object, e As EventArgs)
        If ChckPremiumEdit.Checked = True Then
            txtgrpNumber.Enabled = False
            txtgrpname.Enabled = False
            txtcompany.Enabled = False
            txtgroupcontact.Enabled = False
            txtRepName.Enabled = False
            txtRepContact.Enabled = False
            txtBusAddress.Enabled = False
            btnAddGrp.Enabled = False
            btnEditGrp.Enabled = False
            btnEditPremium.Enabled = True
            btnUpgradeGrpProd.Enabled = False
            If ChkUpdateProd.Checked = True Then
                ChkUpdateProd.Checked = False
            End If
            txtgrpPremium.Enabled = True
            cmbgrpBranch.Enabled = False
            cmbPolicyPlans.Enabled = False
        Else
            txtgrpNumber.Enabled = False
            txtgrpname.Enabled = True
            txtcompany.Enabled = True
            txtgroupcontact.Enabled = True
            txtRepName.Enabled = True
            txtRepContact.Enabled = True
            txtBusAddress.Enabled = True
            btnAddGrp.Enabled = False
            btnEditGrp.Enabled = True
            btnEditPremium.Enabled = False
            btnUpgradeGrpProd.Enabled = False
            cmbgrpBranch.Enabled = True
            cmbPolicyPlans.Enabled = False
            txtgrpPremium.Text = False
        End If
    End Sub
    Protected Sub UpdateCustomerPremiums(ProdID As Integer, GroupNumber As String)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("UpdateCustomerPremiums", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@ProdID", ProdID)
                    cmd.Parameters.AddWithValue("@GroupNumber", GroupNumber)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.CommandTimeout = 0
                    If cmd.ExecuteNonQuery Then

                        msgbox("Customer Premiums successfully updated")

                    Else
                        msgbox("Error updating Premiums")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Function CalculatePrem(ByVal GroupNumber As String) As Double
        Dim Prem As Double = 0
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select tg.Premium,isnull(cd.NoOfDependencies,0)NoOfDep from tbl_Groups tg left join Customer_Details cd On tg.GroupNumber=cd.GroupNumber where cd.GroupNumber='" & GroupNumber & "'and Client_Type in ('SSB','Group')", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim NoOfDep = dt.Rows(0).Item("NoOfDep")
                            Prem = dt.Rows(0).Item("Premium")

                            If NoOfDep = 0 Then
                                Return Prem
                            ElseIf NoOfDep > 0 Then
                                Dim DepPrem As Double = NoOfDep * Prem
                                Prem = DepPrem + Prem
                                Return Prem
                            End If
                            Return Prem





                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try


        Return Prem

    End Function
    Protected Function getNewGroupNo() As String
        Dim NewGroupNumber = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(GroupNumber) from tbl_Groups", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    NewGroupNumber = cmd.ExecuteScalar
                    Return NewGroupNumber
                    con.Close()




                End Using
            End Using





        Catch ex As Exception

        End Try

        Return NewGroupNumber


    End Function

    Protected Sub ChkUpdateProd_CheckedChanged(sender As Object, e As EventArgs)
        If ChkUpdateProd.Checked = True Then
            txtgrpNumber.Enabled = False
            txtgrpname.Enabled = False
            txtcompany.Enabled = False
            txtgroupcontact.Enabled = False
            txtRepName.Enabled = False
            txtRepContact.Enabled = False
            txtBusAddress.Enabled = False
            btnAddGrp.Enabled = False
            btnEditGrp.Enabled = False
            cmbPolicyPlans.Enabled = True
            btnEditPremium.Enabled = False
            If ChckPremiumEdit.Checked = True Then
                ChckPremiumEdit.Checked = False
            End If
            btnUpgradeGrpProd.Enabled = True
            cmbgrpBranch.Enabled = False
            txtgrpPremium.Enabled = True

        ElseIf ChkUpdateProd.Checked = False Then
            txtgrpNumber.Enabled = False
            txtgrpname.Enabled = True
            txtcompany.Enabled = True
            txtgroupcontact.Enabled = True
            txtRepName.Enabled = True
            txtRepContact.Enabled = True
            txtBusAddress.Enabled = True
            btnAddGrp.Enabled = False
            btnEditGrp.Enabled = True
            cmbPolicyPlans.Enabled = False
            btnEditPremium.Enabled = False
            btnUpgradeGrpProd.Enabled = False
            cmbgrpBranch.Enabled = True
            txtgrpPremium.Enabled = True
        End If
    End Sub

    Protected Sub btnUpgradeGrpProd_Click(sender As Object, e As EventArgs)
        Try
            ViewState("ProdID") = cmbPolicyPlans.SelectedValue
            ViewState("GroupNumber") = lstGroups.SelectedValue
            ViewState("Premium") = txtgrpPremium.Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update tbl_Groups set ProdID = '" & cmbPolicyPlans.SelectedValue & "',Premium='" & txtgrpPremium.Text & "' where GroupNumber='" & lstGroups.SelectedValue & "' ", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Group Product & Premiums successfully updated")
                        UpdateGrpCustomerNewProdandNewPrem(ViewState("ProdID"), ViewState("GroupNumber"))
                    Else
                        msgbox("Error updating Premiums")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub UpdateGrpCustomerNewProdandNewPrem(ProdID As Integer, GroupNumber As String)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("UpdateGroupProduct", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@ProdID", ProdID)
                    cmd.Parameters.AddWithValue("@GroupNumber", GroupNumber)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Customer Product and Premiums successfully updated")

                    Else
                        msgbox("Error updating Premiums")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
End Class