Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadSSBClients()
            loadRevenue()
            loadSumAssuredPaid()
            loadTotalProducts()
            loadTotalMatured()
            loadTotalCash()
            loadTotalEcoCash()
            loadTotalOneMoney()
            loadTotalTelecash()
            loadTotalSwipe()
            loadTotalBaobab()
            loadTotalJacaranda()
            loadTotalMarula()
            loadTotalPine()
            loadTotalPalm()
            loadNewClientsDetails()


        End If
    End Sub
    Protected Sub loadSSBClients()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(ID),0)Total from Customer_Details where Client_Type='SSB'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblssb.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadRevenue()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("select cast(deb.debit-cred.credit as Decimal(10,2)) Revenue from ((select isnull(Sum (Debit),0) debit from Accounts_Transactions at where Description in('Premium Payment','Non-PolicyHolder Payment'))) Deb join (select isnull(Sum (Credit),0) credit from Accounts_Transactions where Description  Not in ('Premium Payment','Non-PolicyHolder Payment') ) Cred on 1=1 ", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblRevenue.Text = dt.Rows(0).Item("Revenue")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadSumAssuredPaid()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Credit),0)as decimal(10,2))Total from Accounts_Transactions where Description Like'%Life Assurance Paid%'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lbltotalassured.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalProducts()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(ProdID),0)Total from Para_Products where Active=1", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblproducts.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalMatured()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(ID),0)Total from Customer_Details where Status=1 and isMatured=1 and isDeceased=0", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblmatured.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalCash()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Debit),0)as decimal(10,2))Total from Accounts_Transactions where Description in ('Premium Payment','Non-PolicyHolder Payment') And PaymentMethod='Cash'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblcash.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalEcoCash()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Debit),0)as decimal(10,2))Total from Accounts_Transactions where Description in ('Premium Payment','Non-PolicyHolder Payment') And PaymentMethod='Ecocash'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblecocash.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalOneMoney()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Debit),0)as decimal(10,2))Total from Accounts_Transactions where Description in ('Premium Payment','Non-PolicyHolder Payment') And PaymentMethod='OneMoney'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblOneMoney.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalTelecash()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Debit),0)as decimal(10,2))Total from Accounts_Transactions where Description in ('Premium Payment','Non-PolicyHolder Payment') And PaymentMethod='Telecash'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lbltelecash.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalSwipe()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select cast(isnull(Sum(Debit),0)as decimal(10,2))Total from Accounts_Transactions where Description in ('Premium Payment','Non-PolicyHolder Payment') And PaymentMethod='Swipe'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblswipe.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalPalm()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(cd.ID),0)Total from Customer_Details cd Left Join Para_Products pp On cd.ProdID=pp.ProdID where pp.ProdName='Silver+Bus'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblSilver.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalPine()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(cd.ID),0)Total from Customer_Details cd Left Join Para_Products pp On cd.ProdID=pp.ProdID where pp.ProdName='Gold+Bus'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblGold.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalMarula()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(cd.ID),0)Total from Customer_Details cd Left Join Para_Products pp On cd.ProdID=pp.ProdID where pp.ProdName='Diamond+Bus'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblDiamond.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalJacaranda()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(cd.ID),0)Total from Customer_Details cd Left Join Para_Products pp On cd.ProdID=pp.ProdID where pp.ProdName='Platinum'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblPlatinum.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadTotalBaobab()

        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select isnull(Count(cd.ID),0)Total from Customer_Details cd Left Join Para_Products pp On cd.ProdID=pp.ProdID where pp.ProdName='Platinum+Bus'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            lblPlatinum1.Text = dt.Rows(0).Item("Total")
                        Else

                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadNewClientsDetails()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select Top 10 PolicyNo,Client_Type,Surname,FName,Gender,PhoneNo,pp.ProdName,b.Branch_Name from Customer_Details cd Left Join Para_Products pp ON cd.ProdID=pp.ProdID Left Join Branches b ON cd.Branch=b.Branch_Code where Date_Joined between Convert(varchar, DATEADD(MONTH,-3,GETDATE()),106) and Convert(varchar,GETDATE(),106)", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdClients.DataSource = ds.Tables(0)
                        grdClients.Visible = True
                        grdClients.DataBind()
                    Else
                        grdClients.DataSource = Nothing
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
End Class