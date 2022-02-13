Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class rptProcessedGroup
    Inherits System.Web.UI.Page
    Dim cryRpt As ReportDocument = New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("username") = String.Empty Then
            'not logged in redirect to login page
            Response.Redirect("~/login.aspx", False)
            Exit Sub
        End If
        Dim fileName = Request.QueryString("fileName")
        Dim RepType = Request.QueryString("RepType")
        Dim sql_str = Request.QueryString("sql_str")
        sql_str = ""

        If RepType = "All" Then
            sql_str = "and 1=1"
        ElseIf RepType = "Processed" Then
            sql_str = "and Processed=1"
        ElseIf RepType = "Unprocessed" Then
            sql_str = "and Processed=0"
        End If


        Dim kk As String = ""
        kk = Server.MapPath("rptGroupProcessed.rpt")

        'Dim myConnectionInfo As New ConnectionInfo()
        'myConnectionInfo.ServerName = "sql5092.site4now.net"
        'myConnectionInfo.DatabaseName = "DB_A69E83_NyaradzoTeardrop"
        'myConnectionInfo.UserID = "DB_A69E83_NyaradzoTeardrop_admin"
        'myConnectionInfo.Password = "vick1234"
        'cryRpt.Load(kk)
        'cryRpt.SetDatabaseLogon("DB_A69E83_NyaradzoTeardrop_admin", myConnectionInfo.Password, myConnectionInfo.ServerName, myConnectionInfo.DatabaseName)

        cryRpt.SetParameterValue("sql_str", sql_str)
        cryRpt.SetParameterValue("fileName", fileName)

        CrystalReportViewer1.ReportSource = cryRpt
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Unload
        cryRpt.Close()
        cryRpt.Dispose()
        GC.Collect()

    End Sub
End Class