Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class rptcustomersReport
    Inherits System.Web.UI.Page
    Dim cryRpt As ReportDocument = New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("username") = String.Empty Then
            'not logged in redirect to login page
            Response.Redirect("login.aspx", False)
            Exit Sub
        End If
        Dim from = Request.QueryString("from")
        Dim dto = Request.QueryString("dto")
        Dim branch = Request.QueryString("branch")
        Dim type = Request.QueryString("type")
        Dim status = Request.QueryString("status")
        Dim sql_str = Request.QueryString("sql_str")
        sql_str = ""

        Dim myConnectionInfo As New ConnectionInfo()
        If type = "All" And branch = "All" And status = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        ElseIf type = "All" And status = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and b.Branch_Code='" & branch & "' "
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        ElseIf type = "All" And branch = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and cd.Status='" & status & "' "
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        ElseIf status = "All" And branch = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and Client_Type='" & type & "'"
            Dim kk As String = ""
            'kk = Server.MapPath("rptReportCustomers.rpt")
            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt

        ElseIf type = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and cd.Status='" & status & "' And b.Branch_Code='" & branch & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")
            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        ElseIf status = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and Client_Type='" & type & "' and b.Branch_Code='" & branch & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        ElseIf branch = "All" Then
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and Client_Type='" & type & "' and cd.Status='" & status & "'"
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

            cryRpt.SetParameterValue("sql_str", sql_str)
            CrystalReportViewer1.ReportSource = cryRpt
        Else
            sql_str = "where Date_Joined BETWEEN '" & from & "' and '" & dto & "' and Client_Type='" & type & "' and b.Branch_Code='" & branch & "' and cd.Status='" & status & "' "
            Dim kk As String = ""
            kk = Server.MapPath("rptReportCustomers.rpt")

            'myConnectionInfo.ServerName = "sql5092.site4now.net"
            'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
            'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
            'myConnectionInfo.Password = "vick1234"
            cryRpt.Load(kk)
            'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

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