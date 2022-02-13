Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class NonPolicyReceipts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim EncQuery As New BankEncryption64
            If Trim(Request.QueryString("TrxnID")) = "" Then

            Else
                ViewState("TrxnID") = EncQuery.Decrypt(Request.QueryString("TrxnID").Replace(" ", "+"))

                txtTrxnID.Text = ViewState("TrxnID")
                loadPaymentDetails(ViewState("TrxnID"))
            End If
        End If
    End Sub
    Protected Sub loadPaymentDetails(TrxnID As Integer)
        Try

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select TrxnID,TrxnDate,PolicyNo,Description,PaymentMethod,Account,cast(Credit as decimal(10,2))Credit,Authorized_By from Accounts_Transactions where TrxnID= '" & TrxnID & "' ", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtTrxnID.Text = dt.Rows(0).Item("TrxnID")
                            txtIDNO.Text = dt.Rows(0).Item("Account")
                            txtTrxnDate.Text = dt.Rows(0).Item("TrxnDate")
                            txtAmntPaid.Text = dt.Rows(0).Item("Credit")
                            txtpaymentmethod.Text = dt.Rows(0).Item("PaymentMethod")
                            txtcreatedby.Text = dt.Rows(0).Item("Authorized_By")

                        Else
                            MsgBox("Transaction Not Found")
                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearchSurname_Click(sender As Object, e As EventArgs)
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select Max(TrxnID)TrxnID,isnull(PolicyNo,'') as display from Accounts_Transactions where isnull(PolicyNo,'')  like '%" & txtSearchSurname.Text & "%'and Description in('Non-PolicyHolder Payment') Group By PolicyNo", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstSurnames.Visible = True
                        lstSurnames.DataSource = ds.Tables(0)
                        lstSurnames.DataTextField = "display"
                        lstSurnames.DataValueField = "TrxnID"
                    Else
                        lstSurnames.DataSource = Nothing
                        MsgBox("The searched name was not found")
                    End If
                    'clearAll()
                    lstSurnames.DataBind()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lstSurnames_SelectedIndexChanged(sender As Object, e As EventArgs)
        loadPaymentDetails(lstSurnames.SelectedValue)
    End Sub
    Protected Sub openReport(url As String)
        Dim EncQuery As New BankEncryption64
        Dim strscript As String
        strscript = "<script langauage=JavaScript>"
        strscript += "window.open('" & url & "')"
        strscript += "</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        openReport("rptNonPolicyReceipts.aspx?TrxnID=" + txtTrxnID.Text + "&IDNO=" + txtIDNO.Text + "")
    End Sub
End Class