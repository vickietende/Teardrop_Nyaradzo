Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.Runtime.CompilerServices
Public Class rptAgreement
    Inherits System.Web.UI.Page
    Dim cryRpt As ReportDocument = New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("username") = String.Empty Then
                'not logged in redirect to login page
                Response.Redirect("~/login.aspx", False)
                Exit Sub
            End If
            Dim PolicyNo = Request.QueryString("PolicyNo")






            Dim kk As String = ""
            kk = Server.MapPath("rptPolicyAgreement.rpt")

            'Dim myConnectionInfo As New ConnectionInfo()
            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            'cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("PolicyNo", PolicyNo)
            CrystalReportViewer1.ReportSource = cryRpt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
        GC.Collect()

    End Sub


End Class