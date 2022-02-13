Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports MailKit
Imports MimeKit
Imports MailKit.Net.Smtp
Public Class financial_details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadPara_Products()
            loadSections()
            loadAccounts()
            loadPaymentMethods()
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
    Protected Sub loadSections()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Sections", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbSection.DataSource = ds.Tables(0)
                        cmbSection.DataTextField = "Section"
                        cmbSection.DataValueField = "ID"

                    Else
                        cmbSection.DataSource = Nothing

                    End If

                    cmbSection.DataBind()
                    cmbSection.Items.Insert(0, "--Select Option")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadAccounts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select AccNumber,AccName+'--'+ AccNumber Display from [dbo].[tbl_MainAccounts] where Description='Premium Payments' ", con)
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
                    'cmbAccount.SelectedValue = "MA/000001"
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearchSurname_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select PolicyNo, isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(PolicyNo,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(Address,'') as display from Customer_Details where isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(ADDRESS,'') like '%" & txtSearchSurname.Text & "%'", con)
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
        grdPreviousPayments.Visible = False
        LoadClientDetails(lstSurnames.SelectedValue)
        loadPaymentDetails(lstSurnames.SelectedValue)
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        ViewState("Plan") = cmbProduct.SelectedItem.Text
        ViewState("PaymentMethod") = cmbMethod.SelectedValue
        ViewState("AmtPaid") = txtAmtpaid.Text
        ViewState("PolicyNo") = txtpolicyNo.Text
        ViewState("Name") = txtname.Text
        Try
            ViewState("PolicyNo") = txtpolicyNo.Text
            If txtAmtpaid.Text = "" Then
                msgbox("Enter Amount Paid!!")
                Exit Sub
            ElseIf txtTrxnDate.Text = "" Then
                msgbox("Provide the Transaction Date")
                Exit Sub
            ElseIf cmbMethod.SelectedItem.Text = "Select" Then
                msgbox("Select Payment Method")
                Exit Sub
            ElseIf cmbAccount.SelectedValue = Nothing Or cmbAccount.SelectedItem.Text = "--Select Option--" Then
                msgbox("Select Account")
                Exit Sub

            End If


            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("ProcessPayment", con)
                    cmd.CommandType = CommandType.StoredProcedure



                    cmd.Parameters.AddWithValue("@TrxnDate", txtTrxnDate.Text)
                    cmd.Parameters.AddWithValue("@ExpectedPaymentDate", txtpaymentDate.Text)
                    cmd.Parameters.AddWithValue("@PolicyPlan", cmbProduct.SelectedValue)
                    cmd.Parameters.AddWithValue("@PolicyNo", txtpolicyNo.Text)
                    cmd.Parameters.AddWithValue("@AmountPaid", txtAmtpaid.Text)
                    cmd.Parameters.AddWithValue("@PaymentMethod", cmbMethod.SelectedValue)
                    cmd.Parameters.AddWithValue("@RefNo", txtpolicyNo.Text)
                    cmd.Parameters.AddWithValue("@Created_By", Session("username"))

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        UpdateNextPaymentDate(ViewState("PolicyNo"))
                        Dim message = New MimeMessage()
                        message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                        message.[To].Add(New MailboxAddress("Dear Client", ViewState("Email")))
                        message.Subject = "Premium Payment"
                        Dim BodyBuilder As New BodyBuilder()
                        BodyBuilder.HtmlBody = ViewState("Name") & " ,you have successfully paid <strong> ZWD " & ViewState("AmtPaid") & "</strong> for Policy Plan <strong>" & ViewState("Plan") & "</strong> with Teardrop Funeral Services. Using <strong>" & ViewState("PaymentMethod") & "</strong>." & ". Your Policy Number is:<strong>" & ViewState("PolicyNo") & "<br> Regards.<br>Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
                        BodyBuilder.TextBody = "Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"

                        message.Body = BodyBuilder.ToMessageBody()


                        Using client = New SmtpClient()
                            client.Connect("smtp.gmail.com", 587)
                            client.AuthenticationMechanisms.Remove("XOAUTH2")
                            client.Authenticate("codedimensions.info@gmail.com", "tracieganga&85")

                            client.Send(message)
                            client.Disconnect(True)
                        End Using
                    End If
                    con.Close()
                    'send to Accounts_Transactions with Contra 
                    saveTransaction(txtTrxnDate.Text, cmbProduct.SelectedValue, txtpolicyNo.Text, "Premium Payment", cmbMethod.SelectedValue, cmbAccount.SelectedValue, txtpolicyNo.Text, txtAmtpaid.Text, 0, 1, txtpolicyNo.Text, Session("username"))

                    'rdbType.SelectedValue = Nothing
                    'txtpolicyNo.Text = ""
                    'txtname.Text = ""
                    'txtsurname.Text = ""
                    'cmbGender.SelectedValue = Nothing
                    'txtIDNO.Text = ""
                    'txtDOB.Text = ""
                    'txtECNum.Text = ""
                    'txtterm.Text = ""
                    'txtMaturity.Text = ""
                    'txtpaymentDate.Text = ""
                    'cmbSection.SelectedValue = Nothing
                    'cmbProduct.SelectedValue = Nothing
                    'txtpremium.Text = ""
                    'txtDOC.Text = ""
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try

    End Sub
    Protected Sub UpdateNextPaymentDate(PolicyNo As String)
        Try


            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update PremiumPayments set ExpectedPaymentDate =(Select DATEADD(DAY,30,MAX(TrxnDate)) from PremiumPayments where PolicyNo='" & PolicyNo & "') where PolicyNo='" & PolicyNo & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                    Else
                        msgbox("Error updating Next Payment date")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub LoadClientDetails(PolicyNo As String)
        Try

            If getNumberOfPayments(PolicyNo) = 0 Then
                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact, Convert(varchar,FirstPaymentDate,106)FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                rdbType.SelectedValue = dt.Rows(0).Item("Client_Type")
                                txtpolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                                txtname.Text = dt.Rows(0).Item("FName")
                                txtsurname.Text = dt.Rows(0).Item("surname")
                                cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                                txtIDNO.Text = dt.Rows(0).Item("IDNO")
                                txtDOB.Text = dt.Rows(0).Item("DOB")
                                txtECNum.Text = dt.Rows(0).Item("ECNO")
                                txtterm.Text = dt.Rows(0).Item("Term")
                                txtMaturity.Text = dt.Rows(0).Item("MaturityDate")
                                txtpaymentDate.Text = dt.Rows(0).Item("FirstPaymentDate")
                                cmbSection.SelectedValue = dt.Rows(0).Item("Section")
                                cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                                txtpremium.Text = dt.Rows(0).Item("Premium")
                                txtDOC.Text = dt.Rows(0).Item("FirstPaymentDate")
                                ViewState("Email") = dt.Rows(0).Item("Email")
                            Else
                                msgbox("Customer Not Found")
                            End If
                        End Using

                    End Using
                End Using
            ElseIf getNumberOfPayments(PolicyNo) > 0 Then
                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact, Convert(varchar,pr.ExpectedPaymentDate,106)FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID left join PremiumPayments pr ON cd.PolicyNo=pr.PolicyNo where cd.PolicyNo='" & PolicyNo & "'", con)
                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                rdbType.SelectedValue = dt.Rows(0).Item("Client_Type")
                                txtpolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                                txtname.Text = dt.Rows(0).Item("FName")
                                txtsurname.Text = dt.Rows(0).Item("surname")
                                cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                                txtIDNO.Text = dt.Rows(0).Item("IDNO")
                                txtDOB.Text = dt.Rows(0).Item("DOB")
                                txtECNum.Text = dt.Rows(0).Item("ECNO")
                                txtterm.Text = dt.Rows(0).Item("Term")
                                txtMaturity.Text = dt.Rows(0).Item("MaturityDate")
                                txtpaymentDate.Text = dt.Rows(0).Item("FirstPaymentDate")
                                cmbSection.SelectedValue = dt.Rows(0).Item("Section")
                                cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                                txtpremium.Text = dt.Rows(0).Item("Premium")
                                txtDOC.Text = dt.Rows(0).Item("FirstPaymentDate")
                                ViewState("Email") = dt.Rows(0).Item("Email")
                            Else
                                msgbox("Customer Not Found")
                            End If
                        End Using

                    End Using
                End Using
            End If



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
                cmd.Parameters.AddWithValue("@Reference", Reference)
                cmd.Parameters.AddWithValue("@Authorized_By", Authorized_By)

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    ViewState("TrxnID") = getNewTrxnID(ViewState("PolicyNo"))
                    Dim EncQuery As New BankEncryption64
                    lblAgree.Text = ViewState("TrxnID")
                    lblEncAgree.Text = EncQuery.Encrypt(ViewState("TrxnID").replace(" ", "+"))

                    ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type=""text/javascript"">showPopup()</script>")

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Protected Function getNewTrxnID(PolicyNo As String) As String
        Dim NewID = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(TrxnID) from Accounts_Transactions where PolicyNo='" & PolicyNo & "'", con)


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
    Protected Function getNumberOfPayments(PolicyNo As String) As Integer
        Dim NumOfPayments = 0
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(PolicyNo),0) from PremiumPayments where PolicyNo='" & PolicyNo & "'", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    NumOfPayments = cmd.ExecuteScalar
                    Return NumOfPayments
                    con.Close()




                End Using
            End Using





        Catch ex As Exception

        End Try

        Return NumOfPayments


    End Function
    Private Function CheckifPaymentMethodExists(ByVal PaymentMethod As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select PaymentMethod from tblPaymentMethods where PaymentMethod LIKE '%" & PaymentMethod & "%'", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Existing_Client")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function
    Protected Sub loadPaymentDetails(PolicyNo As String)

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select Top 5 Convert(varchar,TrxnDate,106)TrxnDate,PolicyNo,Description,PaymentMethod,Cast(Credit as decimal(10,2))Premium from Accounts_Transactions where Account='" & PolicyNo & "' and Description in ('Premium Payment')", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdPreviousPayments.DataSource = ds.Tables(0)
                        grdPreviousPayments.Visible = True
                        grdPreviousPayments.DataBind()
                    Else
                        grdPreviousPayments.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnAddMethod_Click(sender As Object, e As EventArgs)
        Try
            If CheckifPaymentMethodExists(txtAddMethod.Text) Then
                msgbox("This Payment Method already exists!")
                Exit Sub
            End If

            If txtAddMethod.Text = "" Then
                msgbox("Add Payment method description!")
                txtAddMethod.Focus()
                Exit Sub
            End If

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Insert INTO tblPaymentMethods(PaymentMethod)Values('" & txtAddMethod.Text & "')", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        loadPaymentMethods()
                    End If
                    con.Close()



                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub loadPaymentMethods()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from tblPaymentMethods", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbMethod.DataSource = ds.Tables(0)
                        cmbMethod.DataTextField = "PaymentMethod"
                        cmbMethod.DataValueField = "PaymentMethod"

                    Else
                        cmbMethod.DataSource = Nothing

                    End If

                    cmbMethod.DataBind()
                    cmbMethod.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class