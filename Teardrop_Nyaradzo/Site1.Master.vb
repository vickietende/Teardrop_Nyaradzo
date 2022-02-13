Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then


            getusers(Session("username"), Session("branch"), Session("userrole"), Session("BrnchCode"))
            SysPermissions(Session("userrole"))

            Session("Reset") = True
            Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
            Dim section As SessionStateSection = CType(config.GetSection("system.web/sessionState"), SessionStateSection)
            Dim timeout As Integer = CInt(section.Timeout.TotalMinutes) * 1000
            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "SessionAlert", "SessionExpireAlert(" & timeout & ");", True)

        End If
    End Sub
    Protected Sub getusers(username As String, branch As String, role As String, brnchcode As Integer)
        lblusername.Text = username
        lblbranch.Text = branch
    End Sub
    Protected Sub SysPermissions(role As String)
        If role = "" Or role = Nothing Then
            Response.Redirect("login.aspx")
        ElseIf role = "admin" Then
            Registration.Visible = True
            PolicyAgreement.Visible = True
            Manage_Groups.Visible = True
            financial_details.Visible = True
            PrintReceipts.Visible = True
            Fund_Policy_Facility.Visible = True
            Non_PolicyHolders.Visible = True
            NonPolicyReceipts.Visible = True
            SSB_Download.Visible = True
            SSB_Upload.Visible = True
            Send_SMS.Visible = True
            Product_Management.Visible = True
            IssueCashBacks.Visible = True
            Manage_Accounts.Visible = True
            Account_Statement.Visible = True
            Trial_Balance.Visible = True
            Income_Expenditure.Visible = True
            Income_Statement.Visible = True
            Customers_Report.Visible = True
            Add_User.Visible = True
            Role_Mgt.Visible = True
            Add_Branch.Visible = True
            System_Modules.Visible = True
            Permissions.Visible = True

        Else
            Try


                Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                    Using cmd = New SqlCommand("Select Role_Name,isnull(ModuleName,'')ModuleName,isnull(ALWView,0)ALWView from [dbo].[Sys_Permissions] where Role_Name = '" & Session("userrole") & "' and ALWView=1", con)
                        Dim dt As New DataTable
                        Using adp = New SqlDataAdapter(cmd)
                            adp.Fill(dt)
                            If Session("userrole") = dt.Rows(0).Item("Role_Name") Then
                                Dim count = 0
                                For Each row As DataRow In dt.Rows
                                    Dim ModName = row.Item("ModuleName")
                                    Dim Permission = row.Item("ALWView")

                                    If ModName = "Registration" And Permission = True Then
                                        Registration.Visible = True
                                    End If

                                    If ModName = "Manage_Groups" And Permission = True Then
                                        Manage_Groups.Visible = True
                                    End If

                                    If ModName = "financial_details" And Permission = True Then
                                        financial_details.Visible = True
                                    End If

                                    If ModName = "PrintReceipts" And Permission = True Then
                                        PrintReceipts.Visible = True
                                    End If

                                    If ModName = "Fund_Policy_Facility" And Permission = True Then
                                        Fund_Policy_Facility.Visible = True
                                    End If
                                    If ModName = "Non_PolicyHolders" And Permission = True Then
                                        Non_PolicyHolders.Visible = True
                                    End If
                                    If ModName = "NonPolicyReceipts" And Permission = True Then
                                        NonPolicyReceipts.Visible = True
                                    End If

                                    If ModName = "IssueCashBacks" And Permission = True Then
                                        IssueCashBacks.Visible = True
                                    End If

                                    If ModName = "SSB_Download" And Permission = True Then
                                        SSB_Download.Visible = True
                                    End If
                                    If ModName = "SSB_Upload" And Permission = True Then
                                        SSB_Upload.Visible = True
                                    End If
                                    If ModName = "Send_SMS" And Permission = True Then
                                        Send_SMS.Visible = True
                                    End If
                                    If ModName = "Product_Management" And Permission = True Then
                                        Product_Management.Visible = True
                                    End If

                                    If ModName = "Manage_Accounts" And Permission = True Then
                                        Manage_Accounts.Visible = True
                                    End If
                                    If ModName = "Account_Statement" And Permission = True Then
                                        Account_Statement.Visible = True
                                    End If
                                    If ModName = "Trial_Balance" And Permission = True Then
                                        Trial_Balance.Visible = True
                                    End If
                                    If ModName = "Income_Expenditure" And Permission = True Then
                                        Income_Expenditure.Visible = True
                                    End If
                                    If ModName = "Income_Statement" And Permission = True Then
                                        Income_Statement.Visible = True
                                    End If
                                    If ModName = "Customers_Report" And Permission = True Then
                                        Customers_Report.Visible = True
                                    End If
                                    If ModName = "Add_User" And Permission = True Then
                                        Add_User.Visible = True
                                    End If
                                    If ModName = "Role_Mgt" And Permission = True Then
                                        Role_Mgt.Visible = True
                                    End If
                                    If ModName = "Add_Branch" And Permission = True Then
                                        Add_Branch.Visible = True
                                    End If
                                    If ModName = "System_Modules" And Permission = True Then
                                        System_Modules.Visible = True
                                    End If
                                    If ModName = "Permissions" And Permission = True Then
                                        Permissions.Visible = True
                                    End If

                                    If ModName = "PolicyAgreement" And Permission = True Then
                                        PolicyAgreement.Visible = True
                                    End If


                                    count += 1
                                Next

                            Else
                                Registration.Visible = False
                                Fund_Policy_Facility.Visible = False
                                financial_details.Visible = False
                                SSB_Download.Visible = False
                                SSB_Upload.Visible = False
                                Product_Management.Visible = False
                                IssueCashBacks.Visible = False
                                Manage_Accounts.Visible = False
                                Account_Statement.Visible = False
                                Trial_Balance.Visible = False
                                Income_Expenditure.Visible = False
                                Income_Statement.Visible = False
                                Customers_Report.Visible = False
                                Add_User.Visible = False
                                Role_Mgt.Visible = False
                                Add_Branch.Visible = False
                                System_Modules.Visible = False
                                Permissions.Visible = False
                            End If

                        End Using

                    End Using
                End Using

            Catch ex As Exception

            End Try
        End If

    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub btnNo_Click(sender As Object, e As EventArgs)

    End Sub
End Class