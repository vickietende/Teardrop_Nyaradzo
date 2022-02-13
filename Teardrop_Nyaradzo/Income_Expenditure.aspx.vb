Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class Income_Expenditure
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        If txtdatefrom.Text = "" Then
            msgbox("Provide date from")
        End If
        If txtdateto.Text = "" Then
            msgbox("Provide date to")
        End If
        openReport("rptIncmExp.aspx?from=" + txtdatefrom.Text + "&dto=" + txtdateto.Text + "")
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