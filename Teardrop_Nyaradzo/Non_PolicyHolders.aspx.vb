Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Non_PolicyHolders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            loadBranches()
            loadCoffinTypes()
            loadAccounts()
            loadPaymentMethods()
            txtAmtPaid.Text = 0
            txtcoffinvalue.Text = 0
            txtfuel.Text = 0
            txtOtherExp.Text = 0
            txtDateCaptured.Text = Date.Now.ToString("dd MMMM yyyy")
        End If
    End Sub

    Protected Sub loadPaymentMethods()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from tblPaymentMethods", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbPayMethod.DataSource = ds.Tables(0)
                        cmbPayMethod.DataTextField = "PaymentMethod"
                        cmbPayMethod.DataValueField = "PaymentMethod"

                    Else
                        cmbPayMethod.DataSource = Nothing

                    End If

                    cmbPayMethod.DataBind()
                    cmbPayMethod.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSaveFin_Click(sender As Object, e As EventArgs)
        Try
            If cmbPayMethod.SelectedValue = Nothing Then
                msgbox("Select a Payment method")
                Exit Sub
            End If
            If txtAmtPaid.Text = "" Or txtAmtPaid.Text = 0 Then
                msgbox("Enter amount paid!!")
                Exit Sub
            End If
            ViewState("IDNO") = txtCusIDNo.Text

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update Non_PolicyHolders set Has_Hearse='" & chkHearse.Checked & "',Has_Bus='" & ChkBus.Checked & "',CoffinType='" & cmbCoffintype.SelectedValue & "' where IDNumber='" & txtCusIDNo.Text & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()


                    con.Close()

                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

        'Save Funeral expenses
        If txtcoffinvalue.Text <> "" And CDbl(txtcoffinvalue.Text) > 0 Then
            saveTransaction(txtDateCaptured.Text, "0", txtCusIDNo.Text, "Non-Policy Holder Coffin Expenses", cmbPayMethod.SelectedValue, txtCusIDNo.Text, cmbAccount.SelectedValue, CDbl(txtcoffinvalue.Text), 0, 1, txtphone.Text, Session("username"))
        End If
        If txtfuel.Text <> "" And CDbl(txtfuel.Text) > 0 Then
            saveTransaction(txtDateCaptured.Text, "0", txtCusIDNo.Text, "Non-Policy Holder Fuel Expenses", cmbPayMethod.SelectedValue, txtCusIDNo.Text, cmbAccount.SelectedValue, CDbl(txtfuel.Text), 0, 1, txtphone.Text, Session("username"))
        End If
        If txtOtherExp.Text <> "" And CDbl(txtOtherExp.Text) > 0 Then
            saveTransaction(txtDateCaptured.Text, "0", txtCusIDNo.Text, "Non-Policy Holder Other Expenses", cmbPayMethod.SelectedValue, txtCusIDNo.Text, cmbAccount.SelectedValue, CDbl(txtOtherExp.Text), 0, 1, txtphone.Text, Session("username"))
        End If
        'Save Customers payment to accounts Transactions
        saveTransaction(txtDateCaptured.Text, "0", txtCusIDNo.Text, "Non-PolicyHolder Payment", cmbPayMethod.SelectedValue, cmbRecAccount.SelectedValue, txtCusIDNo.Text, txtAmtPaid.Text, 0, 1, txtphone.Text, Session("username"))

        ViewState("TrxnID") = getNewTrxnID(ViewState("IDNO"))
        Dim EncQuery As New BankEncryption64
        lblAgree.Text = ViewState("TrxnID")
        lblEncAgree.Text = EncQuery.Encrypt(ViewState("TrxnID").replace(" ", "+"))

        ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type=""text/javascript"">showPopup()</script>")
        'Response.Write("<script>alert('Non-Policy Holder details Successfully Updated') ; location.href='Non_PolicyHolders.aspx'</script>")
        'txtsurname.Text = ""
        'txtname.Text = ""
        'cmbGender.SelectedValue = Nothing
        'txtIDNO.Text = ""
        'cmbmaritalstatus.SelectedValue = Nothing
        'txtDOB.Text = ""
        'txtaddress.Text = ""
        'txtphone.Text = ""
        'txtecNum.Text = ""
        'txtDateCaptured.Text = ""
        'txtOccupation.Text = ""
        'txtEmployer.Text = ""
        'txtbuscontact.Text = ""
        'txtemail.Text = ""
        'cmbbranches.SelectedIndex = -1
        'cmbPolicyHolder.SelectedValue = Nothing

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If CheckifIDNOEXISTS(txtIDNO.Text) = True Then
                msgbox("Customer with IDNO  " + txtIDNO.Text + " already exists.")
                Exit Sub
            End If
            If txtsurname.Text = "" Then
                msgbox("Provide Surname")
                Exit Sub
            End If
            If txtname.Text = "" Then
                msgbox("Provide name")
                Exit Sub
            End If

            If cmbGender.SelectedValue = Nothing Then
                msgbox("Provide Gender")
                Exit Sub
            End If
            If txtIDNO.Text = "" Then
                msgbox("Provide IDNO")
                Exit Sub
            End If
            If cmbmaritalstatus.SelectedValue = Nothing Then
                msgbox("Provide Marital Status")
                Exit Sub
            End If
            If txtDOB.Text = "" Then
                msgbox("Provide DOB")
                Exit Sub
            End If
            If txtaddress.Text = "" Then
                msgbox("Provide Address")
                Exit Sub
            End If
            If txtphone.Text = "" Then
                msgbox("Provide Phone Number")
                Exit Sub
            End If

            If txtDateCaptured.Text = "" Then
                msgbox("Provide date captured")
                Exit Sub
            End If

            If cmbbranches.SelectedIndex = 0 Then
                msgbox("Provide branch")
                Exit Sub
            End If
            cmbPolicyHolder.SelectedValue = Nothing

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SaveNonPolicyHolders", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@Surname", txtsurname.Text)
                    cmd.Parameters.AddWithValue("@First_Name", txtname.Text)
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue)
                    cmd.Parameters.AddWithValue("@IDNumber", txtIDNO.Text)
                    cmd.Parameters.AddWithValue("@Marital_Status", cmbmaritalstatus.SelectedValue)
                    cmd.Parameters.AddWithValue("@DOB", txtDOB.Text)
                    cmd.Parameters.AddWithValue("@Address", txtaddress.Text)
                    cmd.Parameters.AddWithValue("@PhoneNo", txtphone.Text)
                    cmd.Parameters.AddWithValue("@ECNO", txtecNum.Text)
                    cmd.Parameters.AddWithValue("@DateCaptured", txtDateCaptured.Text)
                    cmd.Parameters.AddWithValue("@Occupation", txtOccupation.Text)
                    cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text)
                    cmd.Parameters.AddWithValue("@ContactBus", txtbuscontact.Text)
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text)
                    cmd.Parameters.AddWithValue("@Branch", cmbbranches.SelectedValue)
                    cmd.Parameters.AddWithValue("@Has_A_Policy", cmbPolicyHolder.SelectedValue)
                    cmd.Parameters.AddWithValue("@Has_Hearse", chkHearse.Checked)
                    cmd.Parameters.AddWithValue("@Has_Bus", ChkBus.Checked)
                    cmd.Parameters.AddWithValue("@CoffinType", "")
                    cmd.Parameters.AddWithValue("@CapturedBy", Session("username"))
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Non-Policy Holder details Successfully Saved') ; location.href='Non_PolicyHolders.aspx'</script>")

                    con.Close()
                    txtsurname.Text = ""
                    txtname.Text = ""
                    cmbGender.SelectedValue = Nothing
                    txtIDNO.Text = ""
                    cmbmaritalstatus.SelectedValue = Nothing
                    txtDOB.Text = ""
                    txtaddress.Text = ""
                    txtphone.Text = ""
                    txtecNum.Text = ""
                    txtDateCaptured.Text = ""
                    txtOccupation.Text = ""
                    txtEmployer.Text = ""
                    txtbuscontact.Text = ""
                    txtemail.Text = ""
                    cmbbranches.SelectedIndex = -1
                    cmbPolicyHolder.SelectedValue = Nothing
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
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

                        cmbRecAccount.DataSource = ds.Tables(0)
                        cmbRecAccount.DataTextField = "Display"
                        cmbRecAccount.DataValueField = "AccNumber"

                    Else
                        cmbAccount.DataSource = Nothing
                        cmbRecAccount.DataSource = Nothing

                    End If

                    cmbAccount.DataBind()
                    cmbRecAccount.DataBind()

                    cmbAccount.Items.Insert(0, "--Select Option--")
                    cmbAccount.SelectedValue = "MA/000002"

                    cmbRecAccount.Items.Insert(0, "--Select Option--")
                    cmbRecAccount.SelectedValue = "MA/000001"
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearchSurname_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select IDNumber, isnull(Surname,'')+' '+isnull(First_Name,'')+' -- '+isnull(IDNumber,'')+' -- '+isnull(Address,'') as display from Non_PolicyHolders where isnull(Surname,'')+' '+isnull(First_Name,'')+' --- '+isnull(IDNumber,'')+' --- '+isnull(ADDRESS,'') like '%" & txtSearchSurname.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "IDNumber"
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
    Protected Sub loadCustomerDetails(IDNO As String)
        Try

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * From Non_PolicyHolders where IDNumber= '" & IDNO & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtsurname.Text = dt.Rows(0).Item("Surname")
                            txtname.Text = dt.Rows(0).Item("First_Name")
                            cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                            txtIDNO.Text = dt.Rows(0).Item("IDNumber")
                            cmbmaritalstatus.SelectedValue = dt.Rows(0).Item("Marital_Status")
                            txtDOB.Text = dt.Rows(0).Item("DOB")
                            txtaddress.Text = dt.Rows(0).Item("Address")
                            txtphone.Text = dt.Rows(0).Item("PhoneNo")
                            txtecNum.Text = dt.Rows(0).Item("ECNO")
                            txtDateCaptured.Text = dt.Rows(0).Item("DateCaptured")
                            txtOccupation.Text = dt.Rows(0).Item("Occupation")
                            txtEmployer.Text = dt.Rows(0).Item("Employer")
                            txtbuscontact.Text = dt.Rows(0).Item("ContactBus")
                            txtemail.Text = dt.Rows(0).Item("Email")
                            cmbbranches.SelectedValue = dt.Rows(0).Item("Branch")
                            cmbPolicyHolder.SelectedValue = dt.Rows(0).Item("Has_A_Policy")
                            txtCusIDNo.Text = dt.Rows(0).Item("IDNumber")
                        Else
                            msgbox("Customer Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadCoffinTypes()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Coffin_Types", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbCoffintype.DataSource = ds.Tables(0)
                        cmbCoffintype.DataTextField = "CoffinName"
                        cmbCoffintype.DataValueField = "CoffinName"

                    Else
                        cmbCoffintype.DataSource = Nothing

                    End If

                    cmbCoffintype.DataBind()
                    cmbCoffintype.Items.Insert(0, "None")
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
                    msgbox("Transaction Successful")

                End If
                con.Close()
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
    Private Function CheckifIDNOEXISTS(ByVal IDNO As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select IDNumber from Non_PolicyHolders where IDNumber='" & IDNO & "'", con)
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
    Protected Function getNewTrxnID(IDNO As String) As String
        Dim NewID = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(TrxnID) from Accounts_Transactions where Account='" & IDNO & "'", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    NewID = cmd.ExecuteScalar
                    Return NewID
                    con.Close()




                End Using
            End Using





        Catch ex As Exception

        End Try

        Return NewID


    End Function
End Class