Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class Send_SMS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadPara_Products()
            loadGroups()
            loadBranches()
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

                        cmbProduct.DataSource = ds.Tables(0)
                        cmbProduct.DataTextField = "ProdName"
                        cmbProduct.DataValueField = "ProdID"

                    Else
                        cmbProduct.DataSource = Nothing

                    End If

                    cmbProduct.DataBind()
                    cmbProduct.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroups()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select GroupNumber,GroupName+'--'+pp.ProdName display from tbl_Groups tg left join Para_Products pp ON tg.ProdID=pp.ProdID ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbgrps.DataSource = ds.Tables(0)
                        cmbgrps.DataTextField = "display"
                        cmbgrps.DataValueField = "GroupNumber"

                    Else
                        cmbgrps.DataSource = Nothing

                    End If

                    cmbgrps.DataBind()

                    cmbgrps.Items.Insert(0, "--Select Option--")

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

                        cmbbranches.DataSource = ds.Tables(0)
                        cmbbranches.DataTextField = "Branch_Name"
                        cmbbranches.DataValueField = "Branch_Code"

                    Else
                        cmbbranches.DataSource = Nothing


                    End If

                    cmbbranches.DataBind()

                    cmbbranches.Items.Insert(0, "--Select Option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ClearAll()
        Try
            If rdbType.SelectedValue = "Individual" Then

                loadgrdDetailsbyType(rdbType.SelectedValue)
                lblgroups.Visible = False
                cmbgrps.Visible = False
            ElseIf rdbType.SelectedValue = "Group" Then

                lblgroups.Visible = True
                cmbgrps.Visible = True
            End If
            loadPara_Products()
            loadGroups()
            loadBranches()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSearchSurname_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select PolicyNo, isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(PolicyNo,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(Address,'') as display from Customer_Details where isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(ADDRESS,'') like '%" & txtSearchSurname.Text & "%' ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "PolicyNo"
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
        loadCustomerDetails(lstSurnames.SelectedValue)
    End Sub
    Protected Sub loadCustomerDetails(PolicyNo As String)
        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select  cd.PolicyNo,cd.PhoneNo ,cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured, cd.Employer,pp.Grocery_Amt, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact, FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then

                            txtPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                            txtphoneNo.Text = dt.Rows(0).Item("PhoneNo")
                            cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")


                        Else
                            msgbox("Customer Not Found")
                        End If
                    End Using

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
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim cnt = 0
            For Each row As GridViewRow In grdCustomers.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSelect As CheckBox = DirectCast(row.FindControl("chkSelect"), CheckBox)
                    Dim lblID As Label = DirectCast(row.FindControl("lblID"), Label)

                    If chkSelectAll.Checked = True Then
                        chkSelect.Checked = True

                    Else
                        chkSelect.Checked = False
                    End If

                    'Dim Surname As String = ""
                    'Dim FName As String = ""
                    'Dim ProdName As String = ""
                    'Dim Branch As String = ""
                    'Dim Phone As String = ""
                    'Dim Premium As Double = ""


                    'Dim lblSurname As Label = CType(row.FindControl("lblSurname"), Label)
                    'Dim lblnameedit As Label = CType(row.FindControl("lblnameedit"), Label)
                    'Dim lblProdName As Label = CType(row.FindControl("lblProdName"), Label)
                    'Dim lblBranch As Label = CType(row.FindControl("lblBranch"), Label)
                    'Dim lblphone As Label = CType(row.FindControl("lblphone"), Label)
                    'Dim lblpremium As Label = CType(row.FindControl("lblpremium"), Label)
                    'Surname = lblSurname.Text
                    'FName = lblnameedit.Text
                    'ProdName = lblProdName.Text
                    'Branch = lblBranch.Text
                    'Phone = lblphone.Text
                    'Premium = lblpremium.Text



                    cnt += 1
                End If

            Next



        Catch ex As Exception
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyType(Type As String)
        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID,cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.Client_Type='" & Type & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyGroup(GroupNumber As String)
        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.GroupNumber='" & GroupNumber & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadgrdDetailsbyBrnch(Branch As Integer, Type As String)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.Branch='" & Branch & "' and cd.Client_Type='" & Type & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyGrpBranch(Branch As Integer, Type As String, GroupNumber As String)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.Branch='" & Branch & "' and cd.Client_Type='" & Type & "' and cd.GroupNumber='" & GroupNumber & "' ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyGrpPlan(GroupNumber As String, ProdID As Integer)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.GroupNumber='" & GroupNumber & "' and cd.ProdID='" & ProdID & "'  ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyGrpPlanBranch(GroupNumber As String, ProdID As Integer, Branch As Integer)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.GroupNumber='" & GroupNumber & "' and cd.ProdID='" & ProdID & "' and cd.Branch='" & Branch & "' ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadgrdDetailsbyIndvPlan(ProdID As Integer, Type As String)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.ProdID='" & ProdID & "' and cd.Client_Type='" & Type & "' and cd.ProdID='" & ProdID & "' and cd.Client_Type='" & Type & "' ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadgrdDetailsbyIndvPlanbyBrnch(ProdID As Integer, Type As String, Branch As Integer)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Customer Type")
                Exit Sub
            End If

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cd.ID, cd.Surname,cd.FName,pp.ProdName,b.Branch_Name,cd.PhoneNo,cast(cd.Premium as decimal(10,2))Premium from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.branch=b.Branch_Code where cd.Branch='" & Branch & "' and cd.Client_Type='" & Type & "' and cd.ProdID='" & ProdID & "' and cd.Client_Type='" & Type & "' and cd.Branch='" & Branch & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdCustomers.DataSource = ds.Tables(0)
                        grdCustomers.Visible = True
                        grdCustomers.DataBind()
                    Else
                        grdCustomers.DataSource = Nothing
                        grdCustomers.Visible = False
                        msgbox("No Records!!")
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Sub ClearAll()
        grdCustomers.DataSource = Nothing
        grdCustomers.Visible = False
        txtSearchSurname.Text = ""
        loadBranches()
        chkSelectAll.Checked = False
        loadPara_Products()
        txtPolicyNo.Text = ""
        txtphoneNo.Text = ""
        lstSurnames.DataSource = Nothing
        lstSurnames.Visible = False
        loadGroups()

    End Sub

    Protected Sub cmbgrps_SelectedIndexChanged(sender As Object, e As EventArgs)
        loadgrdDetailsbyGroup(cmbgrps.SelectedValue)
    End Sub

    Protected Sub cmbbranches_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rdbType.SelectedValue = "Individual" Then
            loadgrdDetailsbyBrnch(cmbbranches.SelectedValue, rdbType.SelectedValue)

        ElseIf rdbType.SelectedValue = "Group" Then
            loadgrdDetailsbyGrpBranch(cmbbranches.SelectedValue, rdbType.SelectedValue, cmbgrps.SelectedValue)

        End If

    End Sub

    Protected Sub cmbProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rdbType.SelectedValue = "Individual" Then

            loadgrdDetailsbyIndvPlan(cmbProduct.SelectedValue, rdbType.SelectedValue)

        ElseIf rdbType.SelectedValue = "Individual" And cmbbranches.SelectedValue <> Nothing Then

            loadgrdDetailsbyIndvPlanbyBrnch(cmbProduct.SelectedValue, rdbType.SelectedValue, cmbbranches.SelectedValue)

        ElseIf rdbType.SelectedValue = "Group" Then

            loadgrdDetailsbyGrpPlan(cmbgrps.SelectedValue, cmbProduct.SelectedValue)

        ElseIf rdbType.SelectedValue = "Group" And cmbbranches.SelectedValue <> Nothing Then

            loadgrdDetailsbyGrpPlanBranch(cmbgrps.SelectedValue, cmbProduct.SelectedValue, cmbbranches.SelectedValue)
        End If
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs)

    End Sub
End Class