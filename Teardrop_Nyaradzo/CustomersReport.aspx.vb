Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class CustomersReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadBranches()
        End If
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

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
        Try
            If rdbType.SelectedValue = Nothing Then
                msgbox("Select Client Type")

                Exit Sub
            ElseIf rdbStatus.SelectedValue = Nothing Then
                msgbox("Select Status")
            End If

            openReport("rptcustomersReport.aspx?from=" + txtstartDate.Text + "&dto=" + txtEnddate.Text + "&type=" + rdbType.SelectedValue + "&status=" + rdbStatus.SelectedValue + "&branch=" + cmbbranches.SelectedValue + "")
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
End Class