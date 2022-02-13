Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class IssueCashBacks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadProducts()
            loadAccounts()
            loadBranches()

        End If
    End Sub
    Protected Sub loadProducts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Para_Products where Active=1", con)
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
                    cmbAccount.SelectedValue = "MA/000003"
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
                Using cmd = New SqlCommand("select PolicyNo, isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(PolicyNo,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(Address,'') as display from Customer_Details cd left join Para_Products pp ON cd.ProdID=pp.ProdID where isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(ADDRESS,'') like '%" & txtSearchSurname.Text & "%' and DATEDIFF(Year,cd.FirstPaymentDate,Getdate())>=pp.CashBackPeriod and pp.Has_CashBack=1", con)
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
        loadPreviouscashBacks(lstSurnames.SelectedValue)
    End Sub
    Protected Sub loadCustomerDetails(PolicyNo As String)
        Try

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select  cd.PolicyNo,cd.ProdID , cd.Surname, cd.FName,  cd.IDNO, cd.FirstPaymentDate,(Select Cast(Isnull(SUM(Credit-Debit),0)as decimal (10,2) ) from Accounts_Transactions where Account='" & PolicyNo & "')Contribution,cd.Branch,pp.Has_CashBack,pp.CashBackPeriod,pp.CashBackPercent,DATEDIFF(Year,cd.FirstPaymentDate,Getdate())PeriodtoDate from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID left join Branches b ON cd.Branch=b.Branch_Code where cd.PolicyNo='" & PolicyNo & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then


                            txtPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                            txtname.Text = dt.Rows(0).Item("FName")
                            txtSurname.Text = dt.Rows(0).Item("surname")
                            cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                            txtIDNO.Text = dt.Rows(0).Item("IDNO")
                            txtcontributions.Text = dt.Rows(0).Item("Contribution")
                            txtFirstPaymentdate.Text = dt.Rows(0).Item("FirstPaymentDate")
                            'cmbBranch.SelectedValue = dt.Rows(0).Item("Branch")
                            ChkHasCashBack.Checked = dt.Rows(0).Item("Has_CashBack")
                            txtCashbackPeriod.Text = dt.Rows(0).Item("CashBackPeriod")
                            txtCashbackPercentage.Text = dt.Rows(0).Item("CashBackPercent")
                            txtPeriod.Text = dt.Rows(0).Item("PeriodtoDate")

                            Dim contribution As Decimal = CDbl(txtcontributions.Text)
                            Dim cashback = txtCashbackPercentage.Text
                            Dim totCashPaid = cashback / 100 * contribution
                            txtAmntPaid.Text = totCashPaid

                        Else
                            msgbox("Customer Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If txtTrxndate.Text = "" Then
            msgbox("Please Select Transaction date")
            Exit Sub
        End If
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            'Using cmd As New SqlCommand("SaveAccountsTrxnsTempWithContra", con)
            Using cmd = New SqlCommand("IssueCashBack", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@TrxnDate", txtTrxndate.Text)
                cmd.Parameters.AddWithValue("@PolicyNo", txtPolicyNo.Text)
                cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                cmd.Parameters.AddWithValue("@Surname", txtSurname.Text)
                cmd.Parameters.AddWithValue("@Forenames", txtname.Text)
                cmd.Parameters.AddWithValue("@IDNO", txtIDNO.Text)
                cmd.Parameters.AddWithValue("@CashBackPercentage", txtCashbackPercentage.Text)
                cmd.Parameters.AddWithValue("@AmntPaid", txtAmntPaid.Text)
                cmd.Parameters.AddWithValue("@isAuthorized", 1)
                cmd.Parameters.AddWithValue("@Branch", cmbBranch.SelectedValue)
                cmd.Parameters.AddWithValue("@Created_By", Session("username"))

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    saveTransaction(txtTrxndate.Text, cmbProduct.SelectedValue, txtPolicyNo.Text, "CashBack Paid", cmbPayMethod.SelectedValue, txtPolicyNo.Text, cmbAccount.SelectedValue, txtAmntPaid.Text, 0, 1, txtPolicyNo.Text, Session("username"))
                End If
                con.Close()




            End Using
        End Using

    End Sub
    Protected Sub loadPreviouscashBacks(PolicyNo As String)

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select TrxnDate,PolicyNo,ProdID,AmntPaid from [dbo].[Issued_CashBacks] where PolicyNo='" & PolicyNo & "'", con)

                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdPreviousCashBack.DataSource = ds.Tables(0)
                        grdPreviousCashBack.DataBind()
                        grdPreviousCashBack.Visible = True
                    Else
                        grdPreviousCashBack.DataSource = Nothing
                    End If


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

                        cmbBranch.DataSource = ds.Tables(0)
                        cmbBranch.DataTextField = "Branch_Name"
                        cmbBranch.DataValueField = "Branch_Code"



                    Else
                        cmbBranch.DataSource = Nothing

                    End If

                    cmbBranch.DataBind()

                    cmbBranch.Items.Insert(0, "--Select Option--")
                    cmbBranch.SelectedValue = Session("BrnchCode")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub saveTransaction(TrxnDate As Date, ProdID As Integer, PolicyNo As String, Description As String, PaymentMethod As String, Account As String, ContraAccount As String, Debit As Double, Credit As Double, Authorized As Integer, Reference As String, Authorized_By As String)
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)

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
                    Response.Redirect("IssueCashBacks.aspx")
                End If
                con.Close()
            End Using
        End Using
    End Sub
End Class