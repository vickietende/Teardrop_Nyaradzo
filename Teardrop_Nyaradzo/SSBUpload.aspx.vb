Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Configuration
Imports System.IO
Public Class SSBUpload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadAccounts()
            loadGroups()
            loadFiles()
            loadGroupFiles()
        End If
    End Sub
    Protected Sub generateRefNo(trxnDate As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select ABS(CAST(CAST(NEWID()AS VARBINARY)AS INT ))AS[RandomNumber]", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        txtRefNo.Text = dt.Rows(0).Item("RandomNumber") & txtpaymentDate.Text
                        ViewState("BatchNo") = txtRefNo.Text


                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadAccounts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select AccNumber,AccName+'--'+ AccNumber Display from [dbo].[tbl_MainAccounts] ", con)
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
                    cmbAccount.SelectedValue = "MA/000001"
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtpaymentDate_TextChanged(sender As Object, e As EventArgs)
        generateRefNo(txtpaymentDate.Text)
    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If rdbType.SelectedValue = "SSB" Then

                Dim fileName As String = FileUpload1.FileName
                If CheckFileExists_Approved(fileName) = True Then
                    msgbox("File already Processed")
                    Exit Sub
                End If
                If txtpaymentDate.Text = "" Then
                    msgbox("Select Date")
                    Exit Sub
                End If
                If FileUpload1.FileName = Nothing Then
                    msgbox("Select file to Upload")
                    Exit Sub
                End If
                If rdbType.SelectedValue = Nothing Then
                    msgbox("select file type")
                    Exit Sub
                End If
                DeleteSSBRecords()
                'Upload and save the file
                Dim excelPath As String = Server.MapPath("~/Uploads/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.SaveAs(excelPath)


                Dim connString As String = String.Empty
                Dim extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                Select Case extension
                    Case ".xls"
                        'Excel 97-03
                        connString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                        Exit Select
                    Case ".xlsx"
                        'Excel 07 or higher
                        connString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                        Exit Select

                End Select
                connString = String.Format(connString, excelPath)
                Using excel_con As New OleDbConnection(connString)
                    excel_con.Open()
                    Dim sheet1 As String = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    Dim dtExcelData As New DataTable()

                    '[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(New DataColumn(10) {New DataColumn("PaymentDate", GetType(Date)),
                                                            New DataColumn("PolicyNo", GetType(String)),
                                                            New DataColumn("Surname", GetType(String)),
                                                            New DataColumn("FName", GetType(String)),
                                                            New DataColumn("Gender", GetType(String)),
                                                            New DataColumn("IDNO", GetType(String)),
                                                            New DataColumn("ECNO", GetType(String)),
                                                            New DataColumn("Date_Joined", GetType(String)),
                                                            New DataColumn("Section", GetType(String)),
                                                            New DataColumn("ProdName", GetType(String)),
                                                            New DataColumn("Premium", GetType(Decimal))})

                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using
                    excel_con.Close()

                    'Dim conString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
                    Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        'Using con As New SqlConnection(conString)
                        Using sqlBulkCopy As New SqlBulkCopy(con)
                            'Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.SSB_Temp"

                            '[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("PaymentDate", "TrxnDate")
                            sqlBulkCopy.ColumnMappings.Add("PolicyNo", "PolicyNo")
                            sqlBulkCopy.ColumnMappings.Add("Surname", "Surname")
                            sqlBulkCopy.ColumnMappings.Add("FName", "First_Name")
                            sqlBulkCopy.ColumnMappings.Add("Gender", "Gender")
                            sqlBulkCopy.ColumnMappings.Add("IDNO", "IDNO")
                            sqlBulkCopy.ColumnMappings.Add("ECNO", "ECNO")
                            sqlBulkCopy.ColumnMappings.Add("Date_Joined", "Date_Joined")
                            sqlBulkCopy.ColumnMappings.Add("Section", "Section")
                            sqlBulkCopy.ColumnMappings.Add("ProdName", "ProdName")
                            sqlBulkCopy.ColumnMappings.Add("Premium", "Premium")

                            con.Open()
                            sqlBulkCopy.WriteToServer(dtExcelData)
                            con.Close()
                            UpdateSSBRecords()
                            insertNew()
                            SavePayments()
                            SavetoAccountsTrans()

                        End Using
                    End Using
                End Using

            ElseIf rdbType.SelectedValue = "Group" Then
                Dim fileName As String = FileUpload1.FileName
                If CheckGroupFileExists_Approved(fileName) = True Then
                    msgbox("File already Processed")
                    Exit Sub
                End If
                If txtpaymentDate.Text = "" Then
                    msgbox("Select Date")
                    Exit Sub
                End If
                If FileUpload1.FileName = Nothing Then
                    msgbox("Select file to Upload")
                    Exit Sub
                End If
                If rdbType.SelectedValue = Nothing Then
                    msgbox("select file type")
                    Exit Sub
                End If
                DeleteGroupRecords()
                'Upload and save the file
                Dim excelPath As String = Server.MapPath("~/Uploads/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.SaveAs(excelPath)


                Dim connString As String = String.Empty
                Dim extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                Select Case extension
                    Case ".xls"
                        'Excel 97-03
                        connString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                        Exit Select
                    Case ".xlsx"
                        'Excel 07 or higher
                        connString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                        Exit Select

                End Select
                connString = String.Format(connString, excelPath)
                Using excel_con As New OleDbConnection(connString)
                    excel_con.Open()
                    Dim sheet1 As String = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    Dim dtExcelData As New DataTable()

                    '[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    dtExcelData.Columns.AddRange(New DataColumn(10) {New DataColumn("PaymentDate", GetType(Date)),
                                                            New DataColumn("PolicyNo", GetType(String)),
                                                            New DataColumn("Surname", GetType(String)),
                                                            New DataColumn("FName", GetType(String)),
                                                            New DataColumn("Gender", GetType(String)),
                                                            New DataColumn("IDNO", GetType(String)),
                                                            New DataColumn("Date_Joined", GetType(String)),
                                                            New DataColumn("CompanyName", GetType(String)),
                                                            New DataColumn("ProdName", GetType(String)),
                                                            New DataColumn("Premium", GetType(Decimal)),
                                                            New DataColumn("GroupNumber", GetType(String))})

                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using
                    excel_con.Close()

                    'Dim conString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
                    Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        'Using con As New SqlConnection(conString)
                        Using sqlBulkCopy As New SqlBulkCopy(con)
                            'Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.Groups_Temp"

                            '[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("PaymentDate", "Trxndate")
                            sqlBulkCopy.ColumnMappings.Add("PolicyNo", "PolicyNo")
                            sqlBulkCopy.ColumnMappings.Add("Surname", "Surname")
                            sqlBulkCopy.ColumnMappings.Add("FName", "First_Name")
                            sqlBulkCopy.ColumnMappings.Add("Gender", "Gender")
                            sqlBulkCopy.ColumnMappings.Add("IDNO", "IDNO")
                            sqlBulkCopy.ColumnMappings.Add("Date_Joined", "Date_Joined")
                            sqlBulkCopy.ColumnMappings.Add("CompanyName", "CompanyName")
                            sqlBulkCopy.ColumnMappings.Add("ProdName", "ProdName")
                            sqlBulkCopy.ColumnMappings.Add("Premium", "Premium")
                            sqlBulkCopy.ColumnMappings.Add("GroupNumber", "GroupNumber")
                            con.Open()
                            sqlBulkCopy.WriteToServer(dtExcelData)
                            con.Close()
                            UpdateGroupRecords()
                            insertNewGroup()
                            SaveGroupPayments()
                            SaveGrouptoAccountsTrans()

                        End Using
                    End Using
                End Using


            End If

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub rdbType_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rdbType.SelectedValue = "Group" Then
            lblgroups.Visible = True
            cmbgrps.Visible = True
        Else
            lblgroups.Visible = False
            cmbgrps.Visible = False

        End If
    End Sub
    Protected Sub loadFiles()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Distinct(ReferenceNo),SSB_File+'-'+ReferenceNo Display from [dbo].[SSB_Payments]  ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbFiles.DataSource = ds.Tables(0)
                        cmbFiles.DataTextField = "Display"
                        cmbFiles.DataValueField = "ReferenceNo"

                    Else
                        cmbFiles.DataSource = Nothing

                    End If

                    cmbFiles.DataBind()

                    cmbFiles.Items.Insert(0, "--Select Option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroupFiles()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Distinct(ReferenceNo),Group_File+'-'+ReferenceNo Display from [dbo].[Group_Payments]  ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbGrpFiles.DataSource = ds.Tables(0)
                        cmbGrpFiles.DataTextField = "Display"
                        cmbGrpFiles.DataValueField = "ReferenceNo"

                    Else
                        cmbGrpFiles.DataSource = Nothing

                    End If

                    cmbGrpFiles.DataBind()

                    cmbGrpFiles.Items.Insert(0, "--Select Option--")

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
    Sub insertNew()
        Dim fileName As String = FileUpload1.FileName
        If CheckFileExists_Approved(fileName) = True Then
            msgbox("File already Processed")
            Exit Sub
        End If

        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("INSERT INTO SSB_Payments ([Transdate], [PolicyNo], [EC_NO], [Premium_Payment], [Surname], [Section], [Processed], [SSB_File], [ReferenceNo]) Select TrxnDate,PolicyNo,ECNO,Premium,Surname,Section,1 Processed,SSB_File,RefNo from SSB_Temp", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Sub insertNewGroup()
        Dim fileName As String = FileUpload1.FileName
        If CheckGroupFileExists_Approved(fileName) = True Then
            msgbox("File already Processed")
            Exit Sub
        End If

        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("INSERT INTO Group_Payments (TraxnDate, PolicyNo, IDNO, Premiums, Surname, GroupNumber, Processed, Group_File, ReferenceNo) Select Trxndate, PolicyNo,IDNO,Premium,Surname, GroupNumber,1,Group_File,RefNo from Groups_Temp", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Sub SavePayments()
        Dim fileName As String = FileUpload1.FileName
        If CheckPaymentExists_Approved(fileName, txtRefNo.Text) = True Then
            msgbox("File already Processed")
            Exit Sub
        End If

        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("INSERT INTO [dbo].[PremiumPayments] (TrxnDate, ExpectedPaymentDate, PolicyPlan, PolicyNo, AmountPaid, PaymentMethod, Created_By, RefNo) Select  sp.Transdate,(Select isnull(DATEADD(DAY,30,MAX(TrxnDate)),'')from [dbo].[PremiumPayments])ExpectedPaymentDate, cd.ProdID,sp.PolicyNo, sp.Premium_Payment,sp.SSB_File,'" & Session("username") & "'Created_By,ReferenceNo from SSB_Payments sp left join Customer_Details cd ON sp.PolicyNo=cd.PolicyNo where sp.ReferenceNo='" & txtRefNo.Text & "' and sp.SSB_File='" & fileName & "'", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using


    End Sub
    Sub SaveGroupPayments()
        Dim fileName As String = FileUpload1.FileName
        'If CheckGroupFileExists_Approved(fileName) = True Then
        '    msgbox("File already Processed")
        '    Exit Sub
        'End If

        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("INSERT INTO [dbo].[PremiumPayments] (TrxnDate, ExpectedPaymentDate, PolicyPlan, PolicyNo, AmountPaid, PaymentMethod, Created_By, RefNo) Select  gp.Traxndate,(Select isnull(DATEADD(DAY,30,MAX(TrxnDate)),'')from [dbo].[PremiumPayments])ExpectedPaymentDate, cd.ProdID,gp.PolicyNo, gp.Premiums,gp.Group_File,'" & Session("username") & "'Created_By,gp.ReferenceNo from Group_Payments gp left join Customer_Details cd ON gp.PolicyNo=cd.PolicyNo where gp.ReferenceNo='" & txtRefNo.Text & "' and gp.Group_File='" & fileName & "'", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using


    End Sub
    Protected Sub SavetoAccountsTrans()
        Try
            Dim fileName As String = FileUpload1.FileName
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Accounts_Transactions (TrxnDate, ProdID, PolicyNo, Description, PaymentMethod, Account, ContraAccount, Debit, Credit, Authorized, Reference, Authorized_By)Select TrxnDate,PolicyPlan, PolicyNo,'Premium Payment',PaymentMethod,'" & cmbAccount.SelectedValue & "',PolicyNo,AmountPaid,0,1, RefNo,Created_By from PremiumPayments where PaymentMethod='" & fileName & "' and RefNo='" & txtRefNo.Text & "'", con)

                    If (con.State = ConnectionState.Open) Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then

                    End If
                    con.Close()

                    fileName = FileUpload1.FileName

                    Using cmd1 = New SqlCommand("INSERT INTO Accounts_Transactions (TrxnDate, ProdID, PolicyNo, Description, PaymentMethod, Account, ContraAccount, Debit, Credit, Authorized, Reference, Authorized_By)Select TrxnDate,PolicyPlan, PolicyNo,'Premium Payment',PaymentMethod,PolicyNo,'" & cmbAccount.SelectedValue & "',0,AmountPaid,1, RefNo,Created_By from PremiumPayments where PaymentMethod='" & fileName & "' and RefNo='" & txtRefNo.Text & "'", con)

                        If (con.State = ConnectionState.Open) Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd1.ExecuteNonQuery() Then
                            Response.Write("<script>alert('Payment File Successfully Processed') ; location.href='SSBUpload.aspx'</script>")
                        End If
                        con.Close()
                        'Dim dt As New DataTable
                        'Using adp = New SqlDataAdapter(cmd)
                        '    adp.Fill(dt)
                        '    Dim count = 0

                        '    For Each row As DataRow In dt.Rows
                        '        Dim TrxnDate = dt.Rows(0).Item("TrxnDate")
                        '        Dim PolicyPlan = dt.Rows(0).Item("PolicyPlan")
                        '        Dim PolicyNo = dt.Rows(0).Item("PolicyNo")
                        '        Dim PaymentMethod = dt.Rows(0).Item("PaymentMethod")
                        '        Dim AmountPaid = dt.Rows(0).Item("AmountPaid")
                        '        Dim RefNo = dt.Rows(0).Item("RefNo")
                        '        'Post to Accounts_Transactions

                        '        saveTransaction(TrxnDate, PolicyPlan, PolicyNo, "SSB Premium Payment", PaymentMethod, cmbAccount.SelectedValue, PolicyNo, AmountPaid, 0, 1, RefNo, Session("username"))


                        '        count += 1

                        '    Next
                    End Using
                End Using
            End Using


        Catch ex As Exception


        End Try
    End Sub
    Protected Sub SaveGrouptoAccountsTrans()
        Try
            Dim fileName As String = FileUpload1.FileName
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Accounts_Transactions (TrxnDate, ProdID, PolicyNo, Description, PaymentMethod, Account, ContraAccount, Debit, Credit, Authorized, Reference, Authorized_By)Select TrxnDate,PolicyPlan, PolicyNo,'Premium Payment',PaymentMethod,'" & cmbAccount.SelectedValue & "',PolicyNo,AmountPaid,0,1, RefNo,Created_By from PremiumPayments where PaymentMethod='" & fileName & "' and RefNo='" & txtRefNo.Text & "'", con)

                    If (con.State = ConnectionState.Open) Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then

                    End If
                    con.Close()

                    fileName = FileUpload1.FileName

                    Using cmd1 = New SqlCommand("INSERT INTO Accounts_Transactions (TrxnDate, ProdID, PolicyNo, Description, PaymentMethod, Account, ContraAccount, Debit, Credit, Authorized, Reference, Authorized_By)Select TrxnDate,PolicyPlan, PolicyNo,'Premium Payment',PaymentMethod,PolicyNo,'" & cmbAccount.SelectedValue & "',0,AmountPaid,1, RefNo,Created_By from PremiumPayments where PaymentMethod='" & fileName & "' and RefNo='" & txtRefNo.Text & "'", con)

                        If (con.State = ConnectionState.Open) Then
                            con.Close()
                        End If
                        con.Open()
                        If cmd1.ExecuteNonQuery() Then
                            Response.Write("<script>alert('Payment File Successfully Processed') ; location.href='SSBUpload.aspx'</script>")
                        End If
                        con.Close()

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
                cmd.Parameters.AddWithValue("@Reference", Reference)
                cmd.Parameters.AddWithValue("@Authorized_By", Authorized_By)

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then
                    Response.Write("<script>alert('Payment File Successfully Processed') ; location.href='SSBUpload.aspx'</script>")
                End If
                con.Close()
            End Using
        End Using
    End Sub
    Private Function CheckFileExists_Approved(ByVal filename As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select * from SSB_PAYMENTS Where SSB_File='" & filename & "' and Processed=1", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "SSB_PAYMENTS")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function
    Private Function CheckPaymentExists_Approved(ByVal fileName As String, RefNo As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select * from PremiumPayments Where PaymentMethod='" & fileName & "' and RefNo='" & RefNo & "' ", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "SSB_PAYMENTS")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function
    Private Function CheckGroupFileExists_Approved(ByVal filename As String) As Boolean
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("select * from Group_Payments Where Group_File='" & filename & "' and Processed=1", con)
                Dim ds As New DataSet
                Dim adp = New SqlDataAdapter(cmd)
                adp.Fill(ds, "Group_PAYMENTS")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Using
    End Function

    Protected Sub DeleteSSBRecords()
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("truncate table SSB_Temp", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Protected Sub DeleteGroupRecords()
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("truncate table Groups_Temp", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Protected Sub UpdateSSBRecords()
        Dim fileName As String = FileUpload1.FileName
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("UPDATE SSB_Temp SET RefNo='" & txtRefNo.Text & "',TrxnDate='" & txtpaymentDate.Text & "',SSB_File='" & fileName & "'", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

                End If
                con.Close()
            End Using
        End Using
    End Sub
    Protected Sub UpdateGroupRecords()
        Dim fileName As String = FileUpload1.FileName
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("UPDATE Groups_Temp SET RefNo='" & txtRefNo.Text & "',Trxndate='" & txtpaymentDate.Text & "',Group_File='" & fileName & "'", con)
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery() Then

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

    Protected Sub btnProcessed_Click(sender As Object, e As EventArgs)
        openReport("rptProcessedSSB.aspx?fileName=" + cmbFiles.SelectedValue + "&RepType=" + cmbReportType.SelectedValue + "")
    End Sub
    Protected Sub openReport(url As String)
        Dim EncQuery As New BankEncryption64
        Dim strscript As String
        strscript = "<script langauage=JavaScript>"
        strscript += "window.open('" & url & "')"
        strscript += "</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
    End Sub


    Protected Sub btngrpprint_Click(sender As Object, e As EventArgs)
        openReport("rptProcessedGroup.aspx?fileName=" + cmbGrpFiles.SelectedValue + "&RepType=" + cmbgrpType.SelectedValue + "")
    End Sub
End Class