Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class rptIncomeStatmnt
    Inherits System.Web.UI.Page
    Dim cryRpt As ReportDocument = New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("username") = String.Empty Then
            'not logged in redirect to login page
            Response.Redirect("~/login.aspx", False)
            Exit Sub
        End If
        Dim from = Request.QueryString("from")
        Dim dto = Request.QueryString("dto")
        Dim brnch = Request.QueryString("brnch")
        Dim sql_str = Request.QueryString("sql_str")
        sql_str = ""

        If brnch = "All" Then
            sql_str = "and TrxnDate between '" & from & "' and '" & dto & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptIncomeStatement.rpt")

            'Dim myConnectionInfo As New ConnectionInfo()
            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            'cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt

        Else
            sql_str = "and TrxnDate between '" & from & "' and '" & dto & "' and uu.user_branch='" & brnch & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptIncomeStatement.rpt")

            'Dim myConnectionInfo As New ConnectionInfo()
            'myConnectionInfo.ServerName = "SQL5034.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A641ED_TeardropTest"
            'myConnectionInfo.UserID = "DB_A641ED_TeardropTest_admin"
            'myConnectionInfo.Password = "vick1234"
            'cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A641ED_TeardropTest_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt

        End If

    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
        GC.Collect()

    End Sub

End Class