Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports System.Web.Services
Public Class DisburseFacility
    Inherits System.Web.UI.Page
    Public Shared BenEditID As Integer
    Public Shared BenDeceased As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MyMainPage.ActiveViewIndex = 0
            loadPara_Products()
            loadAccounts()
            txtdate.Text = Date.Now.ToString("dd MMMM yyyy")
            txtfuelamnt.Text = 0.00
            txtotherExp.Text = 0.00
            txtdatedeceased.Text = Date.Now.ToString("dd MMMM yyyy")
        End If
    End Sub
    Protected Sub loadAccounts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select AccNumber,AccName+'--'+AccNumber Display from [dbo].[tbl_MainAccounts] ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbAccount.DataSource = ds.Tables(0)
                        cmbAccount.DataTextField = "Display"
                        cmbAccount.DataValueField = "AccNumber"

                    Else
                        cmbAccount.DataSource = Nothing

                    End If

                    cmbAccount.DataBind()
                    cmbAccount.Items.Insert(0, "--Select Option--")
                    cmbAccount.SelectedValue = "MA/000002"
                End Using
            End Using
        Catch ex As Exception

        End Try
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
    Public Sub msgbox(ByVal strMessage As String)
        'finishes server processing, returns to client.
        Dim strScript As String = "<script language=JavaScript>"
        strScript += "window.alert(""" & strMessage & """);"
        strScript += "</script>"
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = strScript
        Page.Controls.Add(lbl)
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
        If CheckifTerminated(lstSurnames.SelectedValue) = True Then
            msgbox("This Customer's Policy was Terminated")
            Exit Sub
        End If
        If CheckforDeceased(lstSurnames.SelectedValue) Then
            msgbox("This Customer is listed as Deceased")
        End If

        If CheckifActive(lstSurnames.SelectedValue) Then
            msgbox("This Customer's Policy is not yet active and cannot receive any Policy Facilities yet.")
            Exit Sub
        End If

        loadCustomerDetails(lstSurnames.SelectedValue)
        loadBenDetails(lstSurnames.SelectedValue)
    End Sub
    Protected Sub loadCustomerDetails(PolicyNo As String)
        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured, cd.Employer,pp.Grocery_Amt, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact, FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then

                            txtPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                            txtname.Text = dt.Rows(0).Item("FName")
                            txtsurname.Text = dt.Rows(0).Item("surname")
                            cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                            txtIDNO.Text = dt.Rows(0).Item("IDNO")
                            txtSumAssured.Text = dt.Rows(0).Item("SumAssured")
                            txtGrocryAmt.Text = dt.Rows(0).Item("Grocery_Amt")

                            ViewState("SumAssured") = txtSumAssured.Text

                        Else
                            msgbox("Customer Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadBenDetails(PolicyNo As String)

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Beneficiary where PolicyNum='" & PolicyNo & "'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdben.DataSource = ds.Tables(0)
                        grdben.Visible = True
                        grdben.DataBind()
                    Else
                        grdben.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)

        If rdbOption.SelectedIndex = -1 Then
            msgbox("Select Transact Type")
            Exit Sub
        End If

        If rdbOption.SelectedIndex = 0 Then
            Try
                If cmbPayMethod.SelectedValue = Nothing Then
                    msgbox("Select Payment method")
                    Exit Sub
                End If
                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveFacilityFunding", con)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@PolicyNo", txtPolicyNo.Text)
                        cmd.Parameters.AddWithValue("@TrxnDate", txtdate.Text)
                        cmd.Parameters.AddWithValue("@SumAssured", txtSumAssured.Text)
                        cmd.Parameters.AddWithValue("@GroceryAmt", txtGrocryAmt.Text)
                        cmd.Parameters.AddWithValue("@FuelAmt", 0.00)
                        cmd.Parameters.AddWithValue("@OtherExpenses", 0.00)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@PaymentMethod", cmbPayMethod.SelectedValue)
                        cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))
                        cmd.Parameters.AddWithValue("@Branch", Session("BrnchCode"))

                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then
                            'Save to Accounts transactions
                            saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "Life Assurance Paid", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtSumAssured.Text, 0, 1, txtPolicyNo.Text, Session("username"))

                            saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "Funeral Groceries", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtGrocryAmt.Text, 0, 1, txtPolicyNo.Text, Session("username"))

                            msgbox("Transaction successfully processed.")
                        End If


                        con.Close()
                        txtPolicyNo.Text = ""
                        txtdate.Text = ""
                        txtSumAssured.Text = ""
                        txtGrocryAmt.Text = ""
                        txtfuelamnt.Text = ""
                        txtotherExp.Text = ""
                        cmbProduct.SelectedValue = Nothing
                        cmbPayMethod.SelectedValue = Nothing
                    End Using
                End Using

            Catch ex As Exception
                msgbox(ex.Message)
            End Try


            'Save if we providing the Policy Facility
        Else

            Try
                If cmbPayMethod.SelectedValue = Nothing Then
                    msgbox("Select Payment method")
                    Exit Sub
                End If
                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveFacilityFunding", con)
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@PolicyNo", txtPolicyNo.Text)
                        cmd.Parameters.AddWithValue("@TrxnDate", txtdate.Text)
                        cmd.Parameters.AddWithValue("@SumAssured", txtSumAssured.Text)
                        cmd.Parameters.AddWithValue("@GroceryAmt", txtGrocryAmt.Text)
                        cmd.Parameters.AddWithValue("@FuelAmt", txtfuelamnt.Text)
                        cmd.Parameters.AddWithValue("@OtherExpenses", txtotherExp.Text)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@PaymentMethod", cmbPayMethod.SelectedValue)
                        cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))
                        cmd.Parameters.AddWithValue("@Branch", Session("BrnchCode"))

                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then

                            'Save to Accounts Transactions
                            If CDbl(txtSumAssured.Text) > 0 Then
                                saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "Life Assurance Paid", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtSumAssured.Text, 0, 1, txtPolicyNo.Text, Session("username"))
                            End If
                            saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "Funeral Groceries", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtGrocryAmt.Text, 0, 1, txtPolicyNo.Text, Session("username"))

                            saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "Funeral Fuel Expenses", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtfuelamnt.Text, 0, 1, txtPolicyNo.Text, Session("username"))

                            If CDbl(txtotherExp.Text) > 0 Then
                                saveTransaction(txtdate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, " Other Funeral Expenses", cmbPayMethod.SelectedItem.Text, txtPolicyNo.Text, cmbAccount.SelectedValue, txtotherExp.Text, 0, 1, txtPolicyNo.Text, Session("username"))
                            End If
                            msgbox("Transaction successfully processed.")
                        End If


                        con.Close()
                        txtPolicyNo.Text = ""
                        txtdate.Text = ""
                        txtSumAssured.Text = ""
                        txtGrocryAmt.Text = ""
                        txtfuelamnt.Text = ""
                        txtotherExp.Text = ""
                        cmbProduct.SelectedValue = Nothing
                        cmbPayMethod.SelectedValue = Nothing
                    End Using
                End Using

            Catch ex As Exception
                msgbox(ex.Message)
            End Try


        End If

    End Sub
    Protected Sub grdben_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdben.RowEditing
        Try
            BenEditID = DirectCast(grdben.Rows(e.NewEditIndex).FindControl("lblID"), Label).Text

            grdben.EditIndex = e.NewEditIndex

            loadBenDetails(txtPolicyNo.Text)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdben_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdben.RowCancelingEdit
        grdben.EditIndex = -1
        loadBenDetails(txtPolicyNo.Text)
    End Sub
    Protected Sub grdben_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdben.RowUpdating
        Try

            BenEditID = DirectCast(grdben.Rows(e.RowIndex).FindControl("lblID"), Label).Text
            If Trim(BenEditID) = "" Or IsDBNull(BenEditID) Then
                msgbox("No Beneficiary selected")
                Exit Sub
            End If


            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update Beneficiary set isDeceased=1 where ID='" & BenEditID & "' ", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Dependant successfully updated as Deceased")

                    Else
                        msgbox("Error updating Product")
                    End If
                    con.Close()
                    grdben.EditIndex = -1
                    loadBenDetails(txtPolicyNo.Text)

                    Using cmd1 = New SqlCommand("Select firstName+' '+Surname FullName from Beneficiary where ID= '" & BenEditID & "' ", con)
                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd1)
                            adp.Fill(dt)
                            If dt.Rows.Count > 0 Then

                                txtfullname.Text = dt.Rows(0).Item("FullName")


                            End If
                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub saveTransaction(TrxnDate As Date, ProdID As Integer, PolicyNo As String, Description As String, PaymentMethod As String, Account As String, ContraAccount As String, Debit As Double, Credit As Double, Authorized As Integer, Reference As String, Authorized_By As String)
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            'Using cmd As New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
            Using cmd = New SqlCommand("AuthorizePremiumPaymentwithContra", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@TrxnDate", TrxnDate)
                cmd.Parameters.AddWithValue("@ProdID", ProdID)
                cmd.Parameters.AddWithValue("@PolicyNo", PolicyNo)
                cmd.Parameters.AddWithValue("@Description", Description)
                cmd.Parameters.AddWithValue("@PaymentMethod", PaymentMethod)
                cmd.Parameters.AddWithValue("@Account", Account)
                cmd.Parameters.AddWithValue("@ContraAccount", ContraAccount)
                cmd.Parameters.AddWithValue("@Debit", Debit)
                cmd.Parameters.AddWithValue("@Credit", Credit)
                cmd.Parameters.AddWithValue("@Authorized", Authorized)
                cmd.Parameters.AddWithValue("@Reference ", Reference)
                cmd.Parameters.AddWithValue("@Authorized_By", Authorized_By)

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub rdbOption_SelectedIndexChanged(sender As Object, e As EventArgs)
        'msgbox(rdbOption.SelectedIndex)
        If rdbOption.SelectedIndex = 0 Then
            lblfuel.Visible = False
            txtfuelamnt.Visible = False
            lblother.Visible = False
            txtotherExp.Visible = False
            txtSumAssured.Text = ViewState("SumAssured")
            'loadCustomerDetails(lstSurnames.SelectedValue)
        Else
            txtSumAssured.Text = 0.00
            lblfuel.Visible = True
            txtfuelamnt.Visible = True
            lblother.Visible = True
            txtotherExp.Visible = True
        End If
    End Sub
    Private Function CheckifActive(ByVal PolicyNo As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select PolicyNo,Date_Joined from Customer_Details where PolicyNo='" & PolicyNo & "' and Status=0", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Active_Client")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function
    Private Function CheckforDeceased(ByVal PolicyNo As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select PolicyNo from Customer_Details where PolicyNo='" & PolicyNo & "' and isDeceased=1", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Active_Client")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function
    Private Function CheckifTerminated(ByVal PolicyNo As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select PolicyNo from Customer_Details where PolicyNo='" & PolicyNo & "' and isTerminated=1", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Active_Client")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function

    Protected Sub lnkNext_Click(sender As Object, e As EventArgs)
        MyMainPage.ActiveViewIndex = 1
    End Sub

    Protected Sub LnkPrev_Click(sender As Object, e As EventArgs)
        MyMainPage.ActiveViewIndex = 0
    End Sub

    Protected Sub btnSaveDeceased_Click(sender As Object, e As EventArgs)
        If chkIsDeceased.Checked = True Then
            Try



                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveDeceasedInfo", con)
                        cmd.CommandType = CommandType.StoredProcedure


                        cmd.Parameters.AddWithValue("@PolicyNo ", txtPolicyNo.Text)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@Description", txtdesc.Text)
                        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text)
                        cmd.Parameters.AddWithValue("@Location", txtlocation.Text)
                        cmd.Parameters.AddWithValue("@Date_Deceased", txtdatedeceased.Text)
                        ViewState("PolicyNo") = txtPolicyNo.Text
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery Then

                            msgbox("Customer details has been successfully updated as deceased")

                        Else
                            msgbox("Error updating details")
                        End If
                        con.Close()

                        loadCustomerDetails(ViewState("PolicyNo"))
                        loadBenDetails(ViewState("PolicyNo"))
                    End Using
                End Using

            Catch ex As Exception

            End Try
        Else
            Try



                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveBeneficiaryDeceased", con)
                        cmd.CommandType = CommandType.StoredProcedure


                        cmd.Parameters.AddWithValue("@PolicyNo ", txtPolicyNo.Text)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@Description", txtdesc.Text)
                        cmd.Parameters.AddWithValue("@FullName", txtfullname.Text)
                        cmd.Parameters.AddWithValue("@Location", txtlocation.Text)
                        cmd.Parameters.AddWithValue("@Date_Deceased", txtdatedeceased.Text)
                        ViewState("PolicyNo") = txtPolicyNo.Text
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery Then

                            msgbox("Dependant has been successfully updated as deceased")

                        Else
                            msgbox("Error updating details")
                        End If
                        con.Close()
                        loadCustomerDetails(ViewState("PolicyNo"))
                        loadBenDetails(ViewState("PolicyNo"))
                    End Using
                End Using

            Catch ex As Exception

            End Try


        End If
    End Sub

    Protected Sub chkIsDeceased_CheckedChanged(sender As Object, e As EventArgs)
        If chkIsDeceased.Checked = True Then
            txtfullname.Text = txtname.Text & " " & txtsurname.Text
        Else
            txtfullname.Text = ""
        End If
    End Sub


End Class