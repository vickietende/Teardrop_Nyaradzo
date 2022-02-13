Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports System.IO
Imports System.Net
'Imports System.Net.Mail
'Imports System.Net.Mime
Imports MailKit
Imports MimeKit
Imports MailKit.Net.Smtp
Imports System.Web.Mail
Imports System.Security.Cryptography.X509Certificates
'Imports Google.Apis.Auth.OAuth2
Imports MailKit.Security

Public Class customer_registration
    Inherits System.Web.UI.Page
    Public Shared BenEditID As Integer
    Public Shared Surname, Name, Address, Phone As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            loadPara_Products()
            loadModalPara_Products()
            loadSections()
            txtpremium.Text = 0.00
            loadBranches()
            loadGroups()
            'msgbox(cmbbranches.SelectedIndex)


            txtDatejoined.Text = Date.Now.ToString("dd MMMM yyyy")
        End If
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        ViewState("Product") = cmbProduct.SelectedItem.Text
        ViewState("FirstPaymentDate") = txtDOC.Text
        ViewState("Email") = txtemail.Text
        ViewState("Name") = txtsurname.Text & " " & txtname.Text

        If rdbType.SelectedValue = "Individual" Then
            Try
                If rdbType.SelectedValue = Nothing Then
                    msgbox("Select Client Type")
                    Exit Sub
                ElseIf cmbProduct.SelectedItem.Text = "--Select Option--" Then
                    msgbox("Select a product ")
                    Exit Sub

                ElseIf cmbmaritalstatus.SelectedItem.Text = "Select" Then
                    msgbox("select marital status")
                    Exit Sub
                ElseIf cmbGender.SelectedItem.Text = "Select" Then
                    msgbox("select Gender!!")
                    Exit Sub
                ElseIf txtDOC.Text = "" Then
                    msgbox("Provide first payment date")
                    Exit Sub
                End If
                If cmbSection.SelectedIndex = -1 Then
                    msgbox("Select Section for the  client!!")
                    Exit Sub

                End If
                If cmbbranches.SelectedIndex = 0 Then
                    msgbox("Select Branch for the  client!!")
                    Exit Sub
                End If
                If CheckifIDNOEXISTS(txtIDNO.Text) Then
                    msgbox("This ID number already exists with a running Policy.")
                    Exit Sub
                End If

                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveIndividualCustomer", con)
                        cmd.CommandType = CommandType.StoredProcedure
                        'cmd.Parameters.AddWithValue("@CusNo",)

                        cmd.Parameters.AddWithValue("@Client_Type", rdbType.SelectedValue)
                        cmd.Parameters.AddWithValue("@Title", rdbtitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@Surname", txtsurname.Text)
                        cmd.Parameters.AddWithValue("@FName", txtname.Text)
                        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue)
                        cmd.Parameters.AddWithValue("@IDNO", txtIDNO.Text)
                        cmd.Parameters.AddWithValue("@Marital_Status", cmbmaritalstatus.SelectedValue)
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text)
                        cmd.Parameters.AddWithValue("@Address", txtaddress.Text)
                        cmd.Parameters.AddWithValue("@PhoneNo", txtphone.Text)
                        cmd.Parameters.AddWithValue("@ECNO", txtecNum.Text)
                        cmd.Parameters.AddWithValue("@Date_Joined", txtDatejoined.Text)
                        cmd.Parameters.AddWithValue("@Term", txtTerm.Text)
                        cmd.Parameters.AddWithValue("@MaturityDate", txtMaturity.Text)
                        cmd.Parameters.AddWithValue("@Premium", CalculatePrem(cmbNoOfDependencies.SelectedValue))
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text)
                        cmd.Parameters.AddWithValue("@Branch", cmbbranches.SelectedValue)
                        cmd.Parameters.AddWithValue("@Bus_Contact", txtbuscontact.Text)
                        cmd.Parameters.AddWithValue("@SumAssured", txtsumassured.Text)
                        cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text)
                        cmd.Parameters.AddWithValue("@SpouseTitle", rdbSpouseTitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@SpouseName", txtspouseName.Text)
                        cmd.Parameters.AddWithValue("@SpouseIDNO", txtspouseID.Text)
                        cmd.Parameters.AddWithValue("@SpouseContact", txtspousephone.Text)
                        cmd.Parameters.AddWithValue("@NoOfDependencies", cmbNoOfDependencies.SelectedValue)
                        cmd.Parameters.AddWithValue("@NoOfChildren", cmbChidren.SelectedValue)
                        cmd.Parameters.AddWithValue("@FirstPaymentDate", txtDOC.Text)
                        cmd.Parameters.AddWithValue("@Section", cmbSection.SelectedValue)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@Created_By", Session("username"))
                        cmd.Parameters.AddWithValue("@Status", 0)
                        cmd.Parameters.AddWithValue("@isMatured", 0)
                        cmd.Parameters.AddWithValue("@isDeceased", 0)
                        cmd.Parameters.AddWithValue("@isTerminated", 0)
                        cmd.Parameters.AddWithValue("@TerminationDate", "")
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then

                            ViewState("NewPolicyNo") = getNewPolicyNo()
                            ViewState("Premium") = getNewCustomerPrem(ViewState("NewPolicyNo"))
                            Dim EncQuery As New BankEncryption64
                            lblAgree.Text = ViewState("NewPolicyNo")
                            lblEncAgree.Text = EncQuery.Encrypt(ViewState("NewPolicyNo").replace(" ", "+"))
                            ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type=""text/javascript"">showPopup()</script>")
                        End If
                        Dim message = New MimeMessage()
                        message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                        message.[To].Add(New MailboxAddress("Dear Client", ViewState("Email")))
                        message.Subject = "Policy Registration Confirmation"
                        Dim BodyBuilder As New BodyBuilder()
                        BodyBuilder.HtmlBody = ViewState("Name") & " ,you have successfully registered for Policy Plan <strong>" & ViewState("Product") & "</strong> with Teardrop Funeral Services. Your monthly Premiums will be starting <strong>" & ViewState("FirstPaymentDate") & "</strong>." & " Your new Policy Number is :<strong>" & ViewState("NewPolicyNo") & "</strong>.Your monthly Premium will be ZWD <strong>" & ViewState("Premium") & "</strong><br> Regards.<br> Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
                        BodyBuilder.TextBody = "Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"

                        message.Body = BodyBuilder.ToMessageBody()



                        Using client = New SmtpClient()
                            client.Connect("smtp.gmail.com", 587)
                            client.AuthenticationMechanisms.Remove("XOAUTH2")
                            client.Authenticate("codedimensions.info@gmail.com", "tracieganga&85")

                            client.Send(message)
                            client.Disconnect(True)
                        End Using
                        Amortization(ViewState("NewPolicyNo"))
                    End Using
                End Using

            Catch ex As Exception
                msgbox(ex.Message)
            End Try
        ElseIf rdbType.SelectedValue = "Group" And txtgrpcompany.Text = "Government" Then
            Try
                If rdbType.SelectedValue = Nothing Then
                    msgbox("Select Client Type")
                    Exit Sub
                ElseIf cmbProduct.SelectedIndex = 0 Then
                    msgbox("Select a product ")
                    Exit Sub

                ElseIf cmbmaritalstatus.SelectedItem.Text = "Select" Then
                    msgbox("select marital status")
                    Exit Sub
                ElseIf cmbGender.SelectedItem.Text = "Select" Then
                    msgbox("select Gender!!")
                    Exit Sub
                ElseIf txtDOC.Text = "" Then
                    msgbox("Provide first payment date")
                    Exit Sub
                End If


                If rdbType.SelectedValue = "Group" And txtgrpname.Text = "SSB" And cmbSection.SelectedItem.Text = "--Select Option--" Then
                    msgbox("Select Section for the SSB client!!")
                    Exit Sub
                ElseIf rdbType.SelectedValue = "Group" And txtgrpname.Text = "SSB" And txtecNum.Text = "" Then
                    msgbox("An EC Number is expected for every SSB client")
                    Exit Sub
                End If



                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveCustomer", con)
                        cmd.CommandType = CommandType.StoredProcedure


                        cmd.Parameters.AddWithValue("@Client_Type", "SSB")
                        cmd.Parameters.AddWithValue("@Title", rdbtitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@Surname", txtsurname.Text)
                        cmd.Parameters.AddWithValue("@FName", txtname.Text)
                        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue)
                        cmd.Parameters.AddWithValue("@IDNO", txtIDNO.Text)
                        cmd.Parameters.AddWithValue("@Marital_Status", cmbmaritalstatus.SelectedValue)
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text)
                        cmd.Parameters.AddWithValue("@Address", txtaddress.Text)
                        cmd.Parameters.AddWithValue("@PhoneNo", txtphone.Text)
                        cmd.Parameters.AddWithValue("@ECNO", txtecNum.Text)
                        cmd.Parameters.AddWithValue("@Date_Joined", txtDatejoined.Text)
                        cmd.Parameters.AddWithValue("@Term", txtTerm.Text)
                        cmd.Parameters.AddWithValue("@MaturityDate", txtMaturity.Text)
                        cmd.Parameters.AddWithValue("@Premium", CalculatePrem(cmbNoOfDependencies.SelectedValue))
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text)
                        cmd.Parameters.AddWithValue("@Branch", cmbbranches.SelectedValue)
                        cmd.Parameters.AddWithValue("@Bus_Contact", txtbuscontact.Text)
                        cmd.Parameters.AddWithValue("@SumAssured", txtsumassured.Text)
                        cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text)
                        cmd.Parameters.AddWithValue("@SpouseTitle", rdbSpouseTitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@SpouseName", txtspouseName.Text)
                        cmd.Parameters.AddWithValue("@SpouseIDNO", txtspouseID.Text)
                        cmd.Parameters.AddWithValue("@SpouseContact", txtspousephone.Text)
                        cmd.Parameters.AddWithValue("@NoOfDependencies", cmbNoOfDependencies.SelectedValue)
                        cmd.Parameters.AddWithValue("@NoOfChildren", cmbChidren.SelectedValue)
                        cmd.Parameters.AddWithValue("@FirstPaymentDate", txtDOC.Text)
                        cmd.Parameters.AddWithValue("@Section", cmbSection.SelectedValue)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@GroupNumber", cmbgrps.SelectedValue)
                        cmd.Parameters.AddWithValue("@Created_By", Session("username"))
                        cmd.Parameters.AddWithValue("@Status", 0)
                        cmd.Parameters.AddWithValue("@isMatured", 0)
                        cmd.Parameters.AddWithValue("@isDeceased", 0)
                        cmd.Parameters.AddWithValue("@isTerminated", 0)
                        cmd.Parameters.AddWithValue("@TerminationDate", "")
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then
                            ViewState("NewPolicyNo") = getNewPolicyNo()

                            Dim EncQuery As New BankEncryption64
                            lblAgree.Text = ViewState("NewPolicyNo")
                            lblEncAgree.Text = EncQuery.Encrypt(ViewState("NewPolicyNo").replace(" ", "+"))
                            ViewState("Premium") = getNewCustomerPrem(ViewState("NewPolicyNo"))
                            ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type="" text/javascript"">showPopup()</script>")
                            con.Close()
                            Amortization(ViewState("NewPolicyNo"))
                            Dim message = New MimeMessage()
                            message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                            message.[To].Add(New MailboxAddress("Dear Client", ViewState("Email")))
                            message.Subject = "Policy Registration Confirmation"
                            Dim BodyBuilder As New BodyBuilder()
                            BodyBuilder.HtmlBody = ViewState("Name") & " ,you have successfully registered for Policy Plan <strong>" & ViewState("Product") & "</strong> with Teardrop Funeral Services. Your monthly Premiums will be starting <strong>" & ViewState("FirstPaymentDate") & "</strong>." & " Your new Policy Number is :<strong>" & ViewState("NewPolicyNo") & "</strong>.Your monthly Premium will be ZWD <strong>" & ViewState("Premium") & " </strong><br> Regards.<br> Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
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


                    End Using
                End Using

            Catch ex As Exception
                msgbox(ex.Message)
            End Try
        ElseIf rdbType.SelectedValue = "Group" And txtgroup.Text <> "Government" Then
            Try
                If rdbType.SelectedValue = Nothing Then
                    msgbox("Select Client Type")
                    Exit Sub
                ElseIf cmbProduct.SelectedIndex = 0 Then
                    msgbox("Select a product ")
                    Exit Sub

                ElseIf cmbmaritalstatus.SelectedItem.Text = "Select" Then
                    msgbox("select marital status")
                    Exit Sub
                ElseIf cmbGender.SelectedItem.Text = "Select" Then
                    msgbox("select Gender!!")
                    Exit Sub
                ElseIf txtDOC.Text = "" Then
                    msgbox("Provide first payment date")
                    Exit Sub
                End If
                If rdbType.SelectedValue = "Group" And cmbSection.SelectedItem.Text = "--Select Option--" Then
                    msgbox("Select Section for the client!!")
                    Exit Sub

                End If



                Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("SaveCustomer", con)
                        cmd.CommandType = CommandType.StoredProcedure


                        cmd.Parameters.AddWithValue("@Client_Type", rdbType.SelectedValue)
                        cmd.Parameters.AddWithValue("@Title", rdbtitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@Surname", txtsurname.Text)
                        cmd.Parameters.AddWithValue("@FName", txtname.Text)
                        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedValue)
                        cmd.Parameters.AddWithValue("@IDNO", txtIDNO.Text)
                        cmd.Parameters.AddWithValue("@Marital_Status", cmbmaritalstatus.SelectedValue)
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text)
                        cmd.Parameters.AddWithValue("@Address", txtaddress.Text)
                        cmd.Parameters.AddWithValue("@PhoneNo", txtphone.Text)
                        cmd.Parameters.AddWithValue("@ECNO", txtecNum.Text)
                        cmd.Parameters.AddWithValue("@Date_Joined", txtDatejoined.Text)
                        cmd.Parameters.AddWithValue("@Term", txtTerm.Text)
                        cmd.Parameters.AddWithValue("@MaturityDate", txtMaturity.Text)
                        cmd.Parameters.AddWithValue("@Premium", CalculatePrem(cmbNoOfDependencies.SelectedValue))
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text)
                        cmd.Parameters.AddWithValue("@Branch", cmbbranches.SelectedValue)
                        cmd.Parameters.AddWithValue("@Bus_Contact", txtbuscontact.Text)
                        cmd.Parameters.AddWithValue("@SumAssured", txtsumassured.Text)
                        cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text)
                        cmd.Parameters.AddWithValue("@SpouseTitle", rdbSpouseTitle.SelectedValue)
                        cmd.Parameters.AddWithValue("@SpouseName", txtspouseName.Text)
                        cmd.Parameters.AddWithValue("@SpouseIDNO", txtspouseID.Text)
                        cmd.Parameters.AddWithValue("@SpouseContact", txtspousephone.Text)
                        cmd.Parameters.AddWithValue("@NoOfDependencies", cmbNoOfDependencies.SelectedValue)
                        cmd.Parameters.AddWithValue("@NoOfChildren", cmbChidren.SelectedValue)
                        cmd.Parameters.AddWithValue("@FirstPaymentDate", txtDOC.Text)
                        cmd.Parameters.AddWithValue("@Section", cmbSection.SelectedValue)
                        cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                        cmd.Parameters.AddWithValue("@GroupNumber", cmbgrps.SelectedValue)
                        cmd.Parameters.AddWithValue("@Created_By", Session("username"))
                        cmd.Parameters.AddWithValue("@Status", 0)
                        cmd.Parameters.AddWithValue("@isMatured", 0)
                        cmd.Parameters.AddWithValue("@isDeceased", 0)
                        cmd.Parameters.AddWithValue("@isTerminated", 0)
                        cmd.Parameters.AddWithValue("@TerminationDate", "")

                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd.ExecuteNonQuery() Then
                            ViewState("NewPolicyNo") = getNewPolicyNo()

                            Dim EncQuery As New BankEncryption64
                            lblAgree.Text = ViewState("NewPolicyNo")
                            lblEncAgree.Text = EncQuery.Encrypt(ViewState("NewPolicyNo").replace(" ", "+"))
                            ViewState("Premium") = getNewCustomerPrem(ViewState("NewPolicyNo"))
                            ClientScript.RegisterStartupScript(Me.GetType(), "HideLabel", "<script type="" text/javascript"">showPopup()</script>")
                            con.Close()

                            Amortization(ViewState("NewPolicyNo"))

                            Dim message = New MimeMessage()
                            message.From.Add(New MailboxAddress("Teardrop. Funeral Services", "codedimensions.info@gmail.com"))
                            message.[To].Add(New MailboxAddress("Dear Client", ViewState("Email")))
                            message.Subject = "Policy Registration Confirmation"
                            Dim BodyBuilder As New BodyBuilder()
                            BodyBuilder.HtmlBody = ViewState("Name") & " ,you have successfully registered for Policy Plan <strong>" & ViewState("Product") & "</strong> with Teardrop Funeral Services. Your monthly Premiums will be starting <strong>" & ViewState("FirstPaymentDate") & "</strong>." & " Your new Policy Number is :<strong>" & ViewState("NewPolicyNo") & "</strong>.Your monthly Premium will be ZWD <strong>" & ViewState("Premium") & " </strong><br> Regards.<br> Sent via Teardrop. a product of CodeDimensions Pvt (Ltd)"
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

                    End Using
                End Using

            Catch ex As Exception
                msgbox(ex.Message)
            End Try

        End If

    End Sub



    Protected Sub ClearAll()
        rdbType.SelectedValue = Nothing
        txtname.Text = ""
        txtsurname.Text = ""
        cmbGender.SelectedValue = Nothing
        txtIDNO.Text = ""
        cmbmaritalstatus.SelectedValue = Nothing
        txtDOB.Text = ""
        txtaddress.Text = ""
        txtphone.Text = ""
        txtecNum.Text = ""
        txtDatejoined.Text = ""
        txtTerm.Text = ""
        txtMaturity.Text = ""
        txtpremium.Text = ""
        txtemail.Text = ""
        cmbbranches.SelectedValue = Nothing
        txtbuscontact.Text = ""
        txtsumassured.Text = ""
        txtEmployer.Text = ""
        rdbSpouseTitle.SelectedValue = Nothing
        txtspouseName.Text = ""
        txtspouseID.Text = ""
        txtspousephone.Text = ""
        txtDOC.Text = ""
        cmbSection.SelectedValue = Nothing
        cmbProduct.SelectedValue = Nothing
        txtBenSurname.Text = ""
        txtBenfName.Text = ""
        cmbSex.SelectedValue = Nothing
        txtBenIDNO.Text = ""
        cmbBenMaritalStatus.SelectedValue = Nothing
        txtBenDOB.Text = ""
        txtBenAddress.Text = ""
        txtBenContact.Text = ""
        txtBenEmployer.Text = ""
        cmbgrps.Visible = False
        lblgroups.Visible = False
        cmbgrps.SelectedValue = Nothing
        txtgrpcompany.Visible = False
        lblgrpcompany.Visible = False
        txtgrpcompany.Text = ""
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            If cmbSex.SelectedItem.Text = "Select" Then
                msgbox("Select Gender for Beneficiary")
                Exit Sub
            End If

            If cmbRelationship.SelectedValue = Nothing Or cmbRelationship.SelectedItem.Text = "Select" Then
                msgbox("Select Relation of Beneficiary to Client")
                Exit Sub
            End If
            If VerifyBeneficiries(txtPolicyNo.Text) = True Then
                msgbox("The Number of Beneficiaries Specified at Registration has been reached. Please revise details submitted at registration if you wish to proceed.")
                Exit Sub
            End If
            If VerifyNoOfChildren(txtPolicyNo.Text) = True Then
                msgbox("The Number of Children Specified at Registration has been reached. Please revise details submitted at registration if you wish to proceed.")
                Exit Sub
            End If
            If cmbRelationship.SelectedValue = "Child" Then
                If isChild(txtBenDOB.Text) Then
                    msgbox("This Beneficiary you are trying to add is 23 Years or above and cannot be saved as a Child, to proceed Select Relationship as Dependant.")
                    Exit Sub
                End If


            End If

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SaveBeneficiary", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@Surname", txtBenSurname.Text)
                    cmd.Parameters.AddWithValue("@firstName", txtBenfName.Text)
                    cmd.Parameters.AddWithValue("@sex", cmbSex.SelectedValue)
                    cmd.Parameters.AddWithValue("@IDNum", txtBenIDNO.Text)
                    cmd.Parameters.AddWithValue("@Marital_Status", cmbBenMaritalStatus.SelectedValue)
                    cmd.Parameters.AddWithValue("@DOB", txtBenDOB.Text)
                    cmd.Parameters.AddWithValue("@Address", txtBenAddress.Text)
                    cmd.Parameters.AddWithValue("@Phone", txtBenContact.Text)
                    cmd.Parameters.AddWithValue("@Employer", txtBenEmployer.Text)
                    cmd.Parameters.AddWithValue("@Relationship", cmbRelationship.SelectedValue)
                    cmd.Parameters.AddWithValue("@PolicyNum", txtPolicyNo.Text)
                    cmd.Parameters.AddWithValue("@isDeceased", 0)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Beneficiary Details Successfully Saved') ; location.href='customer_registration.aspx'</script>")

                    con.Close()
                    txtBenSurname.Text = ""
                    txtBenfName.Text = ""
                    cmbSex.SelectedValue = Nothing
                    txtBenIDNO.Text = ""
                    cmbBenMaritalStatus.SelectedValue = Nothing
                    txtBenDOB.Text = ""
                    txtBenAddress.Text = ""
                    txtBenContact.Text = ""
                    txtBenEmployer.Text = ""
                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
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
    Protected Sub getClient()
        Try
            Dim PolicyNo As String = ""
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(ID),PolicyNo from Customer_Details group by PolicyNo", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            PolicyNo = dt.Rows(0).Item("PolicyNo")
                            msgbox("The new Customer's PolicyNo is: " & PolicyNo)
                        Else
                            msgbox("Customer Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

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
    Protected Sub loadCustomerDetails(PolicyNo As String)
        Try
            Dim ClientType As String = ""


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured,cd.NoOfchildren, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact,cd.NoOfDependencies ,FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            'rdbType.SelectedValue = dt.Rows(0).Item("Client_Type")
                            ClientType = dt.Rows(0).Item("Client_Type")
                        End If
                    End Using
                End Using
            End Using

            If ClientType = "Individual" Then
                Try

                    Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured,cd.NoOfchildren, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact,cd.NoOfDependencies ,FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                            Dim dt As New DataTable
                            Using adp = New SqlDataAdapter(cmd)
                                adp.Fill(dt)
                                If dt.Rows.Count > 0 Then
                                    rdbType.SelectedValue = dt.Rows(0).Item("Client_Type")
                                    txtPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                                    rdbtitle.SelectedValue = dt.Rows(0).Item("Title")
                                    txtname.Text = dt.Rows(0).Item("FName")
                                    txtsurname.Text = dt.Rows(0).Item("surname")
                                    cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                                    txtIDNO.Text = dt.Rows(0).Item("IDNO")
                                    cmbmaritalstatus.SelectedValue = dt.Rows(0).Item("Marital_Status")
                                    txtDOB.Text = dt.Rows(0).Item("DOB")
                                    txtaddress.Text = dt.Rows(0).Item("Address")
                                    txtphone.Text = dt.Rows(0).Item("PhoneNo")
                                    txtecNum.Text = dt.Rows(0).Item("ECNO")
                                    txtDatejoined.Text = dt.Rows(0).Item("Date_Joined")
                                    txtTerm.Text = dt.Rows(0).Item("Term")
                                    txtMaturity.Text = dt.Rows(0).Item("MaturityDate")
                                    txtpremium.Text = dt.Rows(0).Item("Premium")
                                    txtemail.Text = dt.Rows(0).Item("Email")
                                    cmbbranches.Text = dt.Rows(0).Item("Branch")
                                    txtbuscontact.Text = dt.Rows(0).Item("Bus_Contact")
                                    txtsumassured.Text = dt.Rows(0).Item("SumAssured")
                                    txtEmployer.Text = dt.Rows(0).Item("Employer")
                                    rdbSpouseTitle.SelectedValue = dt.Rows(0).Item("SpouseTitle")
                                    txtspouseName.Text = dt.Rows(0).Item("SpouseName")
                                    txtspouseID.Text = dt.Rows(0).Item("SpouseIDNO")
                                    txtspousephone.Text = dt.Rows(0).Item("SpouseContact")
                                    cmbNoOfDependencies.SelectedValue = dt.Rows(0).Item("NoOfDependencies")
                                    cmbChidren.SelectedValue = dt.Rows(0).Item("NoOfchildren")
                                    txtDOC.Text = dt.Rows(0).Item("FirstPaymentDate")
                                    cmbSection.SelectedValue = dt.Rows(0).Item("Section")
                                    cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                                Else
                                    msgbox("Customer Not Found")
                                End If
                            End Using

                        End Using
                    End Using

                Catch ex As Exception

                End Try
            ElseIf ClientType = "Group" Or ClientType = "SSB" Then
                Try
                    cmbgrps.Visible = True
                    lblgroups.Visible = True
                    lblgrpcompany.Visible = True
                    txtgrpcompany.Visible = True
                    Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured,cd.NoOfchildren, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact,cd.NoOfDependencies ,FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured,tg.CompanyName,tg.GroupNumber from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID left join tbl_Groups tg On cd.GroupNumber=tg.GroupNumber where cd.PolicyNo='" & PolicyNo & "'", con)
                            Dim dt As New DataTable
                            Using adp = New SqlDataAdapter(cmd)
                                adp.Fill(dt)
                                If dt.Rows.Count > 0 Then
                                    rdbType.SelectedValue = "Group"
                                    cmbgrps.SelectedValue = dt.Rows(0).Item("GroupNumber")
                                    txtgrpcompany.Text = dt.Rows(0).Item("CompanyName")
                                    txtPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                                    rdbtitle.SelectedValue = dt.Rows(0).Item("Title")
                                    txtname.Text = dt.Rows(0).Item("FName")
                                    txtsurname.Text = dt.Rows(0).Item("surname")
                                    cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                                    txtIDNO.Text = dt.Rows(0).Item("IDNO")
                                    cmbmaritalstatus.SelectedValue = dt.Rows(0).Item("Marital_Status")
                                    txtDOB.Text = dt.Rows(0).Item("DOB")
                                    txtaddress.Text = dt.Rows(0).Item("Address")
                                    txtphone.Text = dt.Rows(0).Item("PhoneNo")
                                    txtecNum.Text = dt.Rows(0).Item("ECNO")
                                    txtDatejoined.Text = dt.Rows(0).Item("Date_Joined")
                                    txtTerm.Text = dt.Rows(0).Item("Term")
                                    txtMaturity.Text = dt.Rows(0).Item("MaturityDate")
                                    txtpremium.Text = dt.Rows(0).Item("Premium")
                                    txtemail.Text = dt.Rows(0).Item("Email")
                                    cmbbranches.Text = dt.Rows(0).Item("Branch")
                                    txtbuscontact.Text = dt.Rows(0).Item("Bus_Contact")
                                    txtsumassured.Text = dt.Rows(0).Item("SumAssured")
                                    txtEmployer.Text = dt.Rows(0).Item("Employer")
                                    rdbSpouseTitle.SelectedValue = dt.Rows(0).Item("SpouseTitle")
                                    txtspouseName.Text = dt.Rows(0).Item("SpouseName")
                                    txtspouseID.Text = dt.Rows(0).Item("SpouseIDNO")
                                    txtspousephone.Text = dt.Rows(0).Item("SpouseContact")
                                    cmbNoOfDependencies.SelectedValue = dt.Rows(0).Item("NoOfDependencies")
                                    cmbChidren.SelectedValue = dt.Rows(0).Item("NoOfchildren")
                                    txtDOC.Text = dt.Rows(0).Item("FirstPaymentDate")
                                    cmbSection.SelectedValue = dt.Rows(0).Item("Section")
                                    cmbProduct.SelectedValue = dt.Rows(0).Item("ProdID")
                                Else
                                    msgbox("Customer Not Found")
                                End If
                            End Using

                        End Using
                    End Using

                Catch ex As Exception

                End Try


            End If
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

    Protected Sub lstSurnames_SelectedIndexChanged(sender As Object, e As EventArgs)
        ClearAll()
        loadCustomerDetails(lstSurnames.SelectedValue)
        loadBenDetails(lstSurnames.SelectedValue)
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
    Protected Sub loadModalPara_Products()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Para_Products", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbgrpPlans.DataSource = ds.Tables(0)
                        cmbgrpPlans.DataTextField = "ProdName"
                        cmbgrpPlans.DataValueField = "ProdID"

                    Else
                        cmbgrpPlans.DataSource = Nothing

                    End If

                    cmbgrpPlans.DataBind()

                    cmbgrpPlans.Items.Insert(0, "--Select Option--")

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


    Protected Sub btnAddSection_Click(sender As Object, e As EventArgs)

        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Sections(Section)Values('" & txtAddSection.Text & "')", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()

                    con.Close()

                End Using
            End Using
            loadSections()
        Catch ex As Exception
            msgbox(ex.Message)
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
                    cmbSection.Items.Insert(0, "--Select Option--")
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

                        cmbgrpBranch.DataSource = ds.Tables(0)
                        cmbgrpBranch.DataTextField = "Branch_Name"
                        cmbgrpBranch.DataValueField = "Branch_Code"

                    Else
                        cmbbranches.DataSource = Nothing
                        cmbgrpBranch.DataSource = Nothing

                    End If

                    cmbbranches.DataBind()
                    cmbgrpBranch.DataBind()
                    cmbbranches.Items.Insert(0, "--Select Option--")
                    cmbgrpBranch.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub setFirstPaymentDate()
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Convert(varchar,Convert(smalldatetime,DATEADD(DAY, 30,getDate())),106)FirstPaymentDate", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtDOC.Text = dt.Rows(0).Item("FirstPaymentDate")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub cmbProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        setMaturityDate()
        'setFirstPaymentDate()
    End Sub
    Protected Sub setMaturityDate()
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(pp.Premium as decimal(10,2))Premium,cast(pp.SumAssured as decimal(10,2))SumAssured,pp.MaturityPeriod,Convert(varchar,DATEADD(YEAR,pp.MaturityPeriod,Convert(smalldatetime,getdate())),106)MaturityDate from Para_Products pp Left Join Customer_Details cd ON pp.ProdID=cd.ProdID where pp.ProdID='" & cmbProduct.SelectedValue & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtMaturity.Text = dt.Rows(0).Item("MaturityDate")
                            txtTerm.Text = dt.Rows(0).Item("MaturityPeriod")

                            txtsumassured.Text = dt.Rows(0).Item("SumAssured")
                        Else

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

            BenEditID = DirectCast(grdben.Rows(e.RowIndex).FindControl("lblBatchid"), Label).Text
            If Trim(BenEditID) = "" Or IsDBNull(BenEditID) Then
                msgbox("No Beneficiary selected for update")
                Exit Sub
            End If

            Surname = DirectCast(grdben.Rows(e.RowIndex).FindControl("txtsurnameedit"), TextBox).Text
            Name = DirectCast(grdben.Rows(e.RowIndex).FindControl("txtnameedit"), TextBox).Text
            Address = DirectCast(grdben.Rows(e.RowIndex).FindControl("txtAddressedit"), TextBox).Text
            Phone = DirectCast(grdben.Rows(e.RowIndex).FindControl("txtPhoneedit"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Beneficiary set Surname ='" & Surname & "', firstName='" & Name & "',Adress='" & Address & "',Phone='" & Phone & "' where ID='" & BenEditID & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Beneficiary successfully updated")

                    Else
                        msgbox("Error updating Beneficiary")
                    End If
                    con.Close()
                    grdben.EditIndex = -1
                    loadBenDetails(txtPolicyNo.Text)
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        ClearAll()
        Response.Redirect("customer_registration.aspx")
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Customer_Details set Surname='" & txtsurname.Text & "',FName='" & txtname.Text & "',Title='" & rdbtitle.SelectedValue & "',Gender='" & cmbGender.SelectedValue & "',IDNO='" & txtIDNO.Text & "',Marital_Status='" & cmbmaritalstatus.SelectedValue & "',ECNO='" & txtecNum.Text & "',Address='" & txtaddress.Text & "',PhoneNo='" & txtphone.Text & "',Premium='" & CalculatePrem(cmbNoOfDependencies.SelectedValue) & "',Bus_Contact='" & txtbuscontact.Text & "',Employer='" & txtEmployer.Text & "',NoOfDependencies='" & cmbNoOfDependencies.SelectedValue & "',NoOfChildren='" & cmbChidren.SelectedValue & "', SpouseName='" & txtspouseName.Text & "',SpouseIDNO='" & txtspouseID.Text & "',SpouseContact='" & txtspousephone.Text & "' where PolicyNo='" & txtPolicyNo.Text & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Customer Details successfully updated")

                    Else
                        msgbox("Error updating Customer Details")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdbType_SelectedIndexChanged(sender As Object, e As EventArgs)


        Try
            If rdbType.SelectedValue = "Individual" Then
                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select * from Sections where Section='Private'", con)
                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                cmbSection.SelectedValue = dt.Rows(0).Item("ID")

                            Else

                            End If
                        End Using

                    End Using
                End Using
            ElseIf rdbType.SelectedValue = "Group" Then
                cmbgrps.Visible = True
                lblgroups.Visible = True
                txtgrpcompany.Visible = True
                lblgrpcompany.Visible = True
            Else
                cmbgrps.Visible = False
                lblgroups.Visible = False
                txtgrpcompany.Visible = False
                lblgrpcompany.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSaveGrp_Click(sender As Object, e As EventArgs)
        Try
            If GroupExists(txtgrpname.Text, cmbgrpPlans.SelectedValue) = True Then
                msgbox("Group Exists")
                Exit Sub
            End If
            If CheckGroupExists(cmbgrpPlans.SelectedValue, txtcompany.Text) = True Then
                msgbox("Group Exists")
                Exit Sub
            End If
            If cmbgrpPlans.SelectedIndex = -1 Then
                msgbox("Select Plan for Group")
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
            If txtHRRep.Text = "" Then
                msgbox("Enter Representative")
                txtHRRep.Focus()
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
                    cmd.Parameters.AddWithValue("@HRRep", txtHRRep.Text)
                    cmd.Parameters.AddWithValue("@RepContact", txtRepContact.Text)
                    cmd.Parameters.AddWithValue("@Address", txtBusAddress.Text)
                    cmd.Parameters.AddWithValue("@Premium", txtgrpPremium.Text)
                    cmd.Parameters.AddWithValue("@ProdID", cmbgrpPlans.SelectedValue)
                    cmd.Parameters.AddWithValue("@Branch", cmbgrpBranch.SelectedValue)
                    cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Group Details Successfully Saved') ; location.href='customer_registration.aspx'</script>")
                    con.Close()
                    txtgrpname.Text = ""
                    txtcompany.Text = ""
                    txtgroupcontact.Text = ""
                    txtHRRep.Text = ""
                    txtRepContact.Text = ""
                    txtBusAddress.Text = ""
                    txtgrpPremium.Text = ""
                    cmbgrpPlans.SelectedValue = Nothing
                End Using
            End Using
            getGroupNo()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
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

    Protected Sub btnEditgrp_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update tbl_Groups set GroupName ='" & txtgrpname.Text & "', Contact='" & txtgroupcontact.Text & "',HRRep='" & txtHRRep.Text & "',RepContact='" & txtRepContact.Text & "',Address='" & txtBusAddress.Text & "',Premium='" & txtgrpPremium.Text & "',ProdID='" & cmbgrpPlans.SelectedValue & "' where GroupNumber='" & cmbgrpPlans.SelectedValue & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Group Details successfully updated")

                    Else
                        msgbox("Error updating Group Details")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbgrps_SelectedIndexChanged(sender As Object, e As EventArgs)
        loadGroupProductDetails(cmbgrps.SelectedValue)
        setMaturityDate()
        'setFirstPaymentDate()
    End Sub
    Protected Sub loadGroupProductDetails(GroupNumber As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select tg.GroupNumber,tg.GroupName,tg.CompanyName,Cast(tg.Premium as decimal(10,2) )GroupPremium,Cast(pp.SumAssured as decimal(10,2) )SumAssured,pp.MaturityPeriod,pp.StartPeriod,pp.Has_CashBack,pp.CashBackPeriod,pp.CashBackPercent,Cast(pp.Grocery_Amt as decimal(10,2) )Grocery_Amt,pp.Has_Grocery,pp.ProdName,tg.ProdID GrpProdID,pp.CoffinType  from [dbo].[tbl_Groups] tg left join Para_Products pp ON tg.ProdID=pp.ProdID where GroupNumber= '" & GroupNumber & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            cmbProduct.SelectedValue = dt.Rows(0).Item("GrpProdID")
                            txtgrpcompany.Text = dt.Rows(0).Item("CompanyName")
                            txtgroup.Text = dt.Rows(0).Item("GroupName")
                            txtTerm.Text = dt.Rows(0).Item("MaturityPeriod")
                            txtsumassured.Text = dt.Rows(0).Item("SumAssured")
                            txtpremium.Text = dt.Rows(0).Item("GroupPremium")







                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdben_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdben.RowDeleting
        BenEditID = DirectCast(grdben.Rows(e.RowIndex).FindControl("lblBatchid"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from Beneficiary where ID='" & BenEditID & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Beneficiary successfully deleted")

                Else
                    msgbox("Error deleting Beneficiary")
                End If
                con.Close()
                loadBenDetails(txtPolicyNo.Text)
            End Using
        End Using
    End Sub

    Protected Sub btnTerminate_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Customer_Details set isTerminated = 1 ,TerminationDate=Convert(varchar,GetDate(),106)", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("PolicyNo  " & txtPolicyNo.Text & "successfully Terminated")

                    Else
                        msgbox("Error Terminating Policy")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Function VerifyBeneficiries(PolicyNo As String)
        Try
            If cmbRelationship.SelectedValue = "Dependant" Then
                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select isnull(Count(b.ID),0)RegDep,isnull(cd.NoOfDependencies,0)NoOfDependencies from Beneficiary b left join Customer_Details cd ON b.PolicyNum=cd.PolicyNo where PolicyNum='" & PolicyNo & "' and b.Relationship<>'Child' Group By cd.NoOfDependencies", con)

                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)

                            Dim RegDep = dt.Rows(0).Item("RegDep")
                            Dim ReqDep = dt.Rows(0).Item("NoOfDependencies")

                            If RegDep = ReqDep Then
                                Return True
                            Else Return False

                            End If
                        End Using
                    End Using
                End Using
            End If
        Catch ex As Exception

            Return False
        End Try
        Return False
    End Function
    Protected Function VerifyNoOfChildren(PolicyNo As String)
        Try
            If cmbRelationship.SelectedValue = "Child" Then
                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select isnull(Count(b.ID),0)RegChildren,isnull(cd.NoOfChildren,0)NoOfChildren from Beneficiary b left join Customer_Details cd ON b.PolicyNum=cd.PolicyNo where PolicyNum='" & PolicyNo & "' and b.Relationship='Child' Group By cd.NoOfChildren", con)

                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)

                            Dim RegChildren = dt.Rows(0).Item("RegChildren")
                            Dim ReqChildren = dt.Rows(0).Item("NoOfChildren")

                            If RegChildren = ReqChildren Then
                                Return True
                            Else Return False

                            End If
                        End Using
                    End Using
                End Using
            End If
        Catch ex As Exception

            Return False
        End Try

        Return False



    End Function

    Protected Sub btnUpdatePolicy_Click(sender As Object, e As EventArgs)

        Try
            ViewState("PolicyNo") = txtPolicyNo.Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)


                Using cmd = New SqlCommand("UpdateIndividualProduct", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@ProdID", cmbProduct.SelectedValue)
                    cmd.Parameters.AddWithValue("@PolicyNo", txtPolicyNo.Text)
                    cmd.Parameters.AddWithValue("@Created_By", Session("username"))
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then
                        msgbox("Policy Plan Successfully Changed")
                        loadCustomerDetails(ViewState("PolicyNo"))

                    Else
                        msgbox("Error updating Customer Product")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try


    End Sub



    Protected Function isChild(ByVal DOB As String) As Boolean
        Try
            Dim Age As Integer = 0

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select DATEDIFF(YEAR,'" & DOB & "' , getDate())Age", con)

                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        Age = dt.Rows(0).Item("Age")
                    End Using
                    If Age >= 23 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception

            Return False
        End Try
    End Function
    Protected Function CalculatePrem(ByVal Dep As Integer) As Double
        Dim Prem As Double = 0
        If rdbType.SelectedValue = "Individual" Then

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Premium,0)Premium from Para_Products where ProdID='" & cmbProduct.SelectedValue & "' ", con)

                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        Prem = dt.Rows(0).Item("Premium")

                    End Using
                    If Dep = 0 Then
                        Return Prem
                    ElseIf Dep > 0 Then
                        Dim DepPrem As Double = Dep * Prem
                        Prem = DepPrem + Prem
                        Return Prem
                    End If
                    Return Prem
                End Using
            End Using


            Return Prem

        ElseIf rdbType.SelectedValue = "Group" Then
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Premium,0)Premium from tbl_Groups where ProdID='" & cmbProduct.SelectedValue & "'", con)

                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        Prem = dt.Rows(0).Item("Premium")

                    End Using
                    If Dep = 0 Then
                        Return Prem
                    ElseIf Dep > 0 Then
                        Dim DepPrem As Double = Dep * Prem
                        Prem = DepPrem + Prem
                        Return Prem
                    End If
                    Return Prem
                End Using
            End Using


            Return Prem

        End If




        Return Prem



    End Function

    Protected Function getNewPolicyNo() As String
        Dim NewPolicyNo = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Max(PolicyNo) from Customer_Details", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    NewPolicyNo = cmd.ExecuteScalar
                    Return NewPolicyNo
                    con.Close()




                End Using
            End Using





        Catch ex As Exception

        End Try

        Return NewPolicyNo


    End Function

    Protected Sub txtDOB_TextChanged(sender As Object, e As EventArgs)
        If CheckDOB(txtDOB.Text) Then
            msgbox("Client cannot be less than 18 years old.")
            txtDOB.Text = ""

        End If
    End Sub
    Protected Sub Amortization(PolicyNo As String)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Amortize_Client", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@PolicyNo", PolicyNo)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then

                    End If



                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try



    End Sub
    Private Function CheckifIDNOEXISTS(ByVal IDNO As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select IDNO from Customer_Details where IDNO='" & IDNO & "' and isTerminated=0", con)
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
    Protected Function getNewCustomerPrem(PolicyNo As String) As Double
        Dim CusPremium = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(Premium as decimal(10,2))Premium from Customer_Details where PolicyNo='" & PolicyNo & "'", con)


                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    CusPremium = cmd.ExecuteScalar
                    Return CusPremium
                    con.Close()




                End Using
            End Using





        Catch ex As Exception

        End Try

        Return CusPremium


    End Function
    Protected Function CheckDOB(ByVal DOB As String) As Boolean
        Try
            Dim Age As Integer = 0

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select DATEDIFF(YEAR,'" & DOB & "' , getDate())Age", con)

                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        Age = dt.Rows(0).Item("Age")
                    End Using
                    If Age < 18 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception

            Return False
        End Try
    End Function




End Class