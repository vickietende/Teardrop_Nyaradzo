Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Color
Public Class products
    Inherits System.Web.UI.Page
    Public Shared ProductEditID, MaturityPeriod, GroceryAmt As Integer
    Public Shared SumAssured As Double
    Public Shared ProdName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadProducts()
            loadGroups()
            loadAvailableProducts()
            txtCashbackAmount.Text = 0.00
            loadCoffinTypes()
        End If
    End Sub
    Protected Sub loadProducts()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Para_Products", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbProducts.DataSource = ds.Tables(0)
                        cmbProducts.DataTextField = "ProdName"
                        cmbProducts.DataValueField = "ProdID"

                    Else
                        cmbProducts.DataSource = Nothing

                    End If

                    cmbProducts.DataBind()
                    cmbProducts.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroups()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select GroupNumber,GroupName+'--'+pp.ProdName display from tbl_Groups tg left join Para_Products pp ON tg.ProdID=pp.ProdID", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbgroups.DataSource = ds.Tables(0)
                        cmbgroups.DataTextField = "display"
                        cmbgroups.DataValueField = "GroupNumber"

                    Else
                        cmbgroups.DataSource = Nothing

                    End If

                    cmbgroups.DataBind()
                    cmbgroups.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("SavePara_Products", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.AddWithValue("@CusNo",)
                    cmd.Parameters.AddWithValue("@ProdName", txtprodname.Text)
                    cmd.Parameters.AddWithValue("@MaturityPeriod", txtprodterm.Text)
                    cmd.Parameters.AddWithValue("@SumAssured", txtsumAssured.Text)
                    cmd.Parameters.AddWithValue("@Premium", txtpremium.Text)
                    cmd.Parameters.AddWithValue("@StartPeriod", cmbperiod.SelectedValue)
                    cmd.Parameters.AddWithValue("@Has_CashBack", ChkcashBack.Checked)
                    cmd.Parameters.AddWithValue("@CashBackPeriod", cmbCashBackPeriod.SelectedValue)
                    cmd.Parameters.AddWithValue("@CashBackPercent", txtCashbackAmount.Text)
                    cmd.Parameters.AddWithValue("@Has_Grocery", chkgrocery.Checked)
                    cmd.Parameters.AddWithValue("@Grocery_Amt", txtgroceryAmt.Text)
                    cmd.Parameters.AddWithValue("@CoffinType", cmbcoffintype.SelectedValue)
                    cmd.Parameters.AddWithValue("@Active", 1)
                    cmd.Parameters.AddWithValue("@CreatedBy", Session("username"))
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Product Details Successfully Saved') ; location.href='products.aspx'</script>")
                    con.Close()
                    txtprodname.Text = ""
                    txtprodterm.Text = ""
                    txtsumAssured.Text = ""
                    txtpremium.Text = ""
                    cmbperiod.SelectedValue = Nothing


                    chkgrocery.Checked = False

                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub

    Protected Sub btnDeactivate_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update Para_Products Set Active=0 where ProdName='" & cmbProducts.SelectedItem.Text & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Product Successfully De-Activated') ; location.href='products.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnActivate_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update Para_Products Set Active=1 where ProdName='" & cmbProducts.SelectedItem.Text & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Product Successfully Activated') ; location.href='products.aspx'</script>")
                    con.Close()
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadGroupProductDetails(GroupNumber As String)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select tg.GroupNumber,tg.GroupName,tg.CompanyName,Cast(tg.Premium as decimal(10,2) )GroupPremium,Cast(pp.SumAssured as decimal(10,2) )SumAssured,pp.MaturityPeriod,pp.StartPeriod,pp.Has_CashBack,pp.CashBackPeriod,pp.CashBackPercent,Cast(pp.Grocery_Amt as decimal(10,2) )Grocery_Amt,pp.Has_Grocery,pp.ProdName,tg.ProdID GrpProdID,pp.CoffinType  from [dbo].[tbl_Groups] tg left join Para_Products pp ON tg.ProdID=pp.ProdID where GroupNumber= '" & GroupNumber & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            cmbProducts.SelectedValue = dt.Rows(0).Item("GrpProdID")
                            txtprodname.Text = dt.Rows(0).Item("ProdName")
                            txtcompanyname.Text = dt.Rows(0).Item("CompanyName")
                            txtprodterm.Text = dt.Rows(0).Item("MaturityPeriod")
                            txtsumAssured.Text = dt.Rows(0).Item("SumAssured")
                            txtpremium.Text = dt.Rows(0).Item("GroupPremium")
                            cmbperiod.SelectedValue = dt.Rows(0).Item("StartPeriod")
                            ChkcashBack.Checked = dt.Rows(0).Item("Has_CashBack")
                            cmbCashBackPeriod.SelectedValue = dt.Rows(0).Item("CashBackPeriod")
                            txtCashbackAmount.Text = dt.Rows(0).Item("CashBackPercent")
                            txtgroceryAmt.Text = dt.Rows(0).Item("Grocery_Amt")
                            chkgrocery.Checked = dt.Rows(0).Item("Has_Grocery")
                            cmbcoffintype.SelectedValue = dt.Rows(0).Item("CoffinType")


                            cmbCashBackPeriod.Visible = True
                            txtCashbackAmount.Visible = True
                            lblcashback.Visible = True
                            lblyears.Visible = True
                            lblAmount.Visible = True



                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub loadProductDetails(ProdID As Integer)
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select ProdID,ProdName, MaturityPeriod, Cast(SumAssured as decimal(10,2))[SumAssured], Cast(Premium as decimal(10,2))[Premium], StartPeriod,Has_CashBack,CashBackPeriod,CashBackPercent,Has_Grocery, Cast(Grocery_Amt as decimal(10,2))[GroceryAmt],CoffinType, Active from Para_Products where ProdID='" & ProdID & "'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then

                            txtprodname.Text = dt.Rows(0).Item("ProdName")
                            txtprodterm.Text = dt.Rows(0).Item("MaturityPeriod")
                            txtsumAssured.Text = dt.Rows(0).Item("SumAssured")
                            txtpremium.Text = dt.Rows(0).Item("Premium")
                            cmbperiod.SelectedValue = dt.Rows(0).Item("StartPeriod")
                            ChkcashBack.Checked = dt.Rows(0).Item("Has_CashBack")
                            cmbCashBackPeriod.SelectedValue = dt.Rows(0).Item("CashBackPeriod")
                            txtCashbackAmount.Text = dt.Rows(0).Item("CashBackPercent")
                            txtgroceryAmt.Text = dt.Rows(0).Item("GroceryAmt")
                            chkgrocery.Checked = dt.Rows(0).Item("Has_Grocery")
                            cmbcoffintype.SelectedValue = dt.Rows(0).Item("CoffinType")

                            cmbCashBackPeriod.Visible = True
                            txtCashbackAmount.Visible = True
                            lblcashback.Visible = True
                            lblyears.Visible = True
                            lblAmount.Visible = True



                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ClearAll()
        txtprodname.Text = ""
        txtprodterm.Text = ""
        txtsumAssured.Text = ""
        txtpremium.Text = ""
        cmbperiod.SelectedValue = Nothing
        ChkcashBack.Checked = False
        cmbCashBackPeriod.SelectedValue = Nothing
        txtCashbackAmount.Text = ""
        txtgroceryAmt.Text = ""
        chkgrocery.Checked = False
        cmbcoffintype.SelectedValue = Nothing
        txtCashbackAmount.Visible = False
        cmbCashBackPeriod.Visible = False
        lblAmount.Visible = False
        lblcashback.Visible = False
        lblyears.Visible = False
        txtCashbackAmount.Text = ""
        txtcompanyname.Text = ""
        cmbgroups.SelectedValue = Nothing
        cmbProducts.SelectedValue = Nothing
    End Sub



    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Update Para_Products Set ProdName='" & txtprodname.Text & "',MaturityPeriod='" & txtprodterm.Text & "',SumAssured='" & txtsumAssured.Text & "',StartPeriod='" & cmbperiod.SelectedValue & "',Has_CashBack='" & ChkcashBack.Checked & "',CashBackPeriod='" & cmbCashBackPeriod.SelectedValue & "',CashBackPercent='" & txtCashbackAmount.Text & "',Has_Grocery='" & chkgrocery.Checked & "',CoffinType='" & cmbcoffintype.SelectedValue & "',Grocery_Amt='" & txtgroceryAmt.Text & "' where ProdID='" & cmbProducts.SelectedValue & "' Update Customer_Details set SumAssured='" & txtsumAssured.Text & "',Term='" & txtprodterm.Text & "' where ProdID='" & cmbProducts.SelectedValue & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then

                        Response.Write("<script>alert('Product Successfully Updated') ; location.href='products.aspx'</script>")
                        con.Close()
                    End If

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub UpdateCustomersPremiums(ProdID As Integer)
        Dim NewMaturityDate = ""
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("UpdateMaturityDates", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@ProdID", ProdID)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If

                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        msgbox("Customers Maturity Dates Successfully updated")
                        con.Close()
                    End If






                End Using
            End Using





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

    Protected Sub cmbProducts_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ChckPremiumEdit.Checked = False Then

                txtprodname.Enabled = True
                txtprodterm.Enabled = True
                txtsumAssured.Enabled = True
                txtgroceryAmt.Enabled = True
                txtCashbackAmount.Enabled = True
                cmbperiod.Enabled = True
                ChkcashBack.Enabled = True
                cmbCashBackPeriod.Enabled = True
                txtCashbackAmount.Enabled = True
                chkgrocery.Enabled = True
                cmbcoffintype.Enabled = True
                btnSave.Enabled = True
                btnEdit.Enabled = True
                btnDeactivate.Enabled = True
                btnActivate.Enabled = True
                loadProductDetails(cmbProducts.SelectedValue)




            Else
                txtprodname.Enabled = False
                txtprodterm.Enabled = False
                txtsumAssured.Enabled = False
                txtgroceryAmt.Enabled = False
                txtCashbackAmount.Enabled = False
                cmbperiod.Enabled = False
                ChkcashBack.Enabled = False
                cmbCashBackPeriod.Enabled = False
                txtCashbackAmount.Enabled = False
                chkgrocery.Enabled = False
                cmbcoffintype.Enabled = False
                btnSave.Enabled = False
                btnEdit.Enabled = False
                btnDeactivate.Enabled = False
                btnActivate.Enabled = False
                loadProductDetails(cmbProducts.SelectedValue)
            End If



        Catch ex As Exception

        End Try

    End Sub
    Protected Sub loadAvailableProducts()

        Try


            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select ProdID,ProdName, MaturityPeriod, Cast(SumAssured as decimal(10,2))[SumAssured], Cast(Premium as decimal(10,2))[Premium], StartPeriod,Grocery_Amt,CoffinType from Para_Products where Active=1", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "QGM")
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdProducts.DataSource = ds.Tables(0)
                        grdProducts.DataBind()
                        grdProducts.Visible = True
                    Else
                        grdProducts.DataSource = Nothing
                    End If


                End Using
            End Using

        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub grdProducts_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdProducts.RowEditing
        Try
            ProductEditID = DirectCast(grdProducts.Rows(e.NewEditIndex).FindControl("lblid"), Label).Text

            grdProducts.EditIndex = e.NewEditIndex
            loadAvailableProducts()
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub ChkcashBack_CheckedChanged(sender As Object, e As EventArgs)
        If ChkcashBack.Checked = True Then
            lblcashback.Visible = True
            cmbCashBackPeriod.Visible = True
            lblyears.Visible = True
            lblAmount.Visible = True
            txtCashbackAmount.Visible = True
        Else
            lblcashback.Visible = False
            cmbCashBackPeriod.Visible = False
            lblyears.Visible = False
            lblAmount.Visible = False
            txtCashbackAmount.Visible = False

        End If
    End Sub

    Protected Sub ChkProdType_CheckedChanged(sender As Object, e As EventArgs)
        ClearAll()
        If ChkProdType.Checked = True Then

            lblcompanyname.Visible = True
            txtcompanyname.Visible = True
            lblsearchgroup.Visible = True
            cmbgroups.Visible = True
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnActivate.Enabled = False
            btnDeactivate.Enabled = False
        Else
            lblcompanyname.Visible = False
            txtcompanyname.Visible = False
            lblsearchgroup.Visible = False
            cmbgroups.Visible = False
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnActivate.Enabled = True
            btnDeactivate.Enabled = True


        End If
    End Sub

    Protected Sub cmbgroups_SelectedIndexChanged(sender As Object, e As EventArgs)
        loadGroupProductDetails(cmbgroups.SelectedValue)
    End Sub

    Protected Sub grdProducts_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdProducts.RowCancelingEdit
        grdProducts.EditIndex = -1
        loadAvailableProducts()
    End Sub
    Protected Sub grdProducts_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdProducts.RowUpdating
        Try

            ProductEditID = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("lblid"), Label).Text
            If Trim(ProductEditID) = "" Or IsDBNull(ProductEditID) Then
                msgbox("No Product selected for update")
                Exit Sub
            End If

            ProdName = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("txtProdNameedit"), TextBox).Text
            MaturityPeriod = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("txtMaturityedit"), TextBox).Text
            SumAssured = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("txtSumAssurededit"), TextBox).Text

            GroceryAmt = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("txtGroceryAmtedit"), TextBox).Text
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Para_Products set ProdName='" & ProdName & "',MaturityPeriod= '" & MaturityPeriod & "',SumAssured='" & SumAssured & "',Grocery_Amt='" & GroceryAmt & "' where ProdID='" & ProductEditID & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Product successfully updated")

                    Else
                        msgbox("Error updating Product")
                    End If
                    con.Close()
                    grdProducts.EditIndex = -1
                    loadAvailableProducts()
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdProducts_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdProducts.RowDeleting
        ProductEditID = DirectCast(grdProducts.Rows(e.RowIndex).FindControl("lblid"), Label).Text
        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
            Using cmd = New SqlCommand("delete from Para_Products where ProdID='" & ProductEditID & "'", con)
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()
                If cmd.ExecuteNonQuery Then
                    msgbox("Product successfully deleted")

                Else
                    msgbox("Error deleting Product")
                End If
                con.Close()
                loadAvailableProducts()
            End Using
        End Using
    End Sub

    Protected Sub btnEditPremium_Click(sender As Object, e As EventArgs)
        Try
            ViewState("ProdID") = cmbProducts.SelectedValue
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("update Para_Products set Premium='" & txtpremium.Text & "' where ProdID='" & cmbProducts.SelectedValue & "'", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery Then

                        msgbox("Product Premiumssuccessfully updated")
                        Dim GroupNumber As String = "None"
                        UpdateCustomerPremiums(ViewState("ProdID"), GroupNumber)
                    Else
                        msgbox("Error updating Premium")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ChckPremiumEdit_CheckedChanged(sender As Object, e As EventArgs)
        If ChckPremiumEdit.Checked = True Then
            txtprodname.Enabled = False
            txtprodterm.Enabled = False
            txtsumAssured.Enabled = False
            txtgroceryAmt.Enabled = False
            txtCashbackAmount.Enabled = False
            cmbperiod.Enabled = False
            ChkcashBack.Enabled = False
            cmbCashBackPeriod.Enabled = False
            txtCashbackAmount.Enabled = False
            chkgrocery.Enabled = False
            cmbcoffintype.Enabled = False
            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnDeactivate.Enabled = False
            btnActivate.Enabled = False

        Else
            txtprodname.Enabled = True
            txtprodterm.Enabled = True
            txtsumAssured.Enabled = True
            txtgroceryAmt.Enabled = True
            txtCashbackAmount.Enabled = True
            cmbperiod.Enabled = True
            ChkcashBack.Enabled = True
            cmbCashBackPeriod.Enabled = True
            txtCashbackAmount.Enabled = True
            chkgrocery.Enabled = True
            cmbcoffintype.Enabled = True
            btnSave.Enabled = True
            btnEdit.Enabled = True
            btnDeactivate.Enabled = True
            btnActivate.Enabled = True

        End If
    End Sub

    Protected Sub btnAddCoffin_Click(sender As Object, e As EventArgs)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("INSERT INTO Coffin_Types(CoffinName)Values('" & txtAddCoffin.Text & "')", con)

                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    If cmd.ExecuteNonQuery() Then
                        msgbox("Casket/Coffin Name Saved Successfully")
                        txtAddCoffin.Text = ""
                    End If

                    con.Close()

                End Using
            End Using
            loadCoffinTypes()
        Catch ex As Exception
            msgbox(ex.Message)
        End Try
    End Sub
    Protected Sub loadCoffinTypes()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select * from Coffin_Types", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbcoffintype.DataSource = ds.Tables(0)
                        cmbcoffintype.DataTextField = "CoffinName"
                        cmbcoffintype.DataValueField = "CoffinName"

                    Else
                        cmbcoffintype.DataSource = Nothing

                    End If

                    cmbcoffintype.DataBind()
                    cmbcoffintype.Items.Insert(0, "--Select Option--")
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UpdateCustomerPremiums(ProdID As Integer, GroupNumber As String)
        Try

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("UpdateCustomerPremiums", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    'cmd.Parameters.AddWithValue("@CusNo",)
                    cmd.Parameters.AddWithValue("@ProdID", ProdID)
                    cmd.Parameters.AddWithValue("@GroupNumber", GroupNumber)
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                    con.Open()
                    cmd.CommandTimeout = 0
                    If cmd.ExecuteNonQuery Then

                        msgbox("Customer Premiums successfully updated")

                    Else
                        msgbox("Error updating Premiums")
                    End If
                    con.Close()

                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Function CalculatePrem(ByVal ProdID As Integer) As Double
        Dim Prem As Double = 0
        Try
            Using con = New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select pp.Premium,isnull(cd.NoOfDependencies,0)NoOfDep from Para_Products pp left join Customer_Details cd On pp.ProdID=cd.ProdID where cd.ProdID='" & ProdID & "' and Client_Type='Individual'", con)
                    Dim dt As New DataTable
                    Using adp = New SqlDataAdapter(cmd)
                        adp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim NoOfDep = dt.Rows(0).Item("NoOfDep")
                            Prem = dt.Rows(0).Item("Premium")

                            If NoOfDep = 0 Then
                                Return Prem
                            ElseIf NoOfDep > 0 Then
                                Dim DepPrem As Double = NoOfDep * Prem
                                Prem = DepPrem + Prem
                                Return Prem
                            End If
                            Return Prem





                        End If
                    End Using

                End Using
            End Using

        Catch ex As Exception

        End Try


        Return Prem

    End Function

End Class