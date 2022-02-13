Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class AccountStatement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub btnsearchAcc_Click(sender As Object, e As EventArgs)

        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select PolicyNo, isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(PolicyNo,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(Address,'') as display from Customer_Details where isnull(Surname,'')+' '+isnull(FName,'')+' --- '+isnull(IDNO,'')+' --- '+isnull(ADDRESS,'') like '%" & txtSearchAccount.Text & "%'", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cust")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lstAccounts.Visible = True
                        lstAccounts.DataSource = ds.Tables(0)
                        lstAccounts.DataTextField = "display"
                        lstAccounts.DataValueField = "PolicyNo"
                    Else
                        lstAccounts.DataSource = Nothing
                        msgbox("The searched name was not found")
                    End If
                    'clearAll()
                    lstAccounts.DataBind()

                End Using
            End Using
        Catch ex As Exception

        End Try





    End Sub

    Protected Sub lstAccounts_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSearchAccount.Text = lstAccounts.SelectedValue

    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        If txtdatefrom.Text = "" Then
            msgbox("Provide date from")
        End If
        If txtdateto.Text = "" Then
            msgbox("Provide date to")
        End If
        openReport("rptAccStatmnt.aspx?from=" + txtdatefrom.Text + "&dto=" + txtdateto.Text + "&AccNo=" + lstAccounts.SelectedValue + "")
    End Sub
    Protected Sub openReport(url As String)
        Dim EncQuery As New BankEncryption64
        Dim strscript As String
        strscript = "<script langauage=JavaScript>"
        strscript += "window.open('" & url & "')"
        strscript += "</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "newwin", strscript)
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
End Class