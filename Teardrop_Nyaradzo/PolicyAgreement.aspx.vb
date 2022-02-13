Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class PolicyAgreement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            loadPara_Products()
            Dim EncQuery As New BankEncryption64
            If Trim(Request.QueryString("PolicyNo")) = "" Then

            Else
                ViewState("NewPolicyNo") = EncQuery.Decrypt(Request.QueryString("PolicyNo").Replace(" ", "+"))

                txtNewPolicyNo.Text = ViewState("NewPolicyNo")

                loadPersonalDetails(ViewState("NewPolicyNo"))

            End If
        End If
    End Sub
    Protected Sub loadPersonalDetails(PolicyNo As String)
        Try

            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select  cd.PolicyNo, cd.Client_Type, cd.Title, cd.Surname, cd.FName, cd.Gender, cd.IDNO, cd.Marital_Status, cd.DOB, cd.Address, cd.PhoneNo, cd.ECNO, cd.Date_Joined, cd.Term,MaturityDate, Cast(cd.Premium as decimal (10,2))Premium, cd.Email, cd.Branch, cd.Bus_Contact,Cast(pp.SumAssured as Decimal(10,2))SumAssured,cd.NoOfchildren, cd.Employer, cd.SpouseTitle, cd.SpouseName, cd.SpouseIDNO, cd.SpouseContact,cd.NoOfDependencies ,FirstPaymentDate, cd.Section, cd.ProdID,pp.ProdName, cd.Created_By, cd.Status, cd.isMatured from Customer_Details cd LEFT JOIN Para_Products pp ON cd.ProdID=pp.ProdID where cd.PolicyNo='" & PolicyNo & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            txtNewPolicyNo.Text = dt.Rows(0).Item("PolicyNo")
                            txtname.Text = dt.Rows(0).Item("FName")
                            txtsurname.Text = dt.Rows(0).Item("surname")
                            cmbGender.SelectedValue = dt.Rows(0).Item("Gender")
                            txtIDNO.Text = dt.Rows(0).Item("IDNO")
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
        loadPersonalDetails(lstSurnames.SelectedValue)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        openReport("rptAgreement.aspx?PolicyNo=" + txtNewPolicyNo.Text + "")
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
    Protected Sub openReport(url As String)
        Dim EncQuery As New BankEncryption64
        Dim strscript As String
        strscript = "<script langauage=JavaScript>"
        strscript += "window.open('" & url & "')"
        strscript += "</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
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
                        msgbox("Customer Payment Schedule successfully created!")
                    End If



                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
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

    Protected Sub lnkViewAmortization_Click(sender As Object, e As EventArgs)
        If txtNewPolicyNo.Text = "" Then
            msgbox("Select customer!")
        End If
        openReport("rptSchedule.aspx?PolicyNo=" + txtNewPolicyNo.Text + "")
    End Sub
End Class