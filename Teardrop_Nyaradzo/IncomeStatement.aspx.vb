Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class IncomeStatement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadBranches()
        End If
    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        If txtdatefrom.Text = "" Then
            msgbox("Provide date from")
        End If
        If txtdateto.Text = "" Then
            msgbox("Provide date to")
        End If
        openReport("rptIncomeStatmnt.aspx?from=" + txtdatefrom.Text + "&dto=" + txtdateto.Text + "&brnch=" + cmbbranches.SelectedValue + "")
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
                    cmbbranches.Items.Insert(0, "All")
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