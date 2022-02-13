Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel


Public Class SSBDownload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadGroups()
        End If
    End Sub
    Public Sub generateSSBfile(ByVal CusType As String)
        Try
            Dim f_type As String = cmbFileType.SelectedValue
            If CusType = "SSB" Then
                If f_type = "New" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT isnull(DATEADD(DAY,30,cd.Date_Joined),'')PaymentDate,cd.PolicyNo,Surname,FName,Gender,IDNO,ECNO,Date_Joined,S.Section,pp.ProdName,cd.Premium FROM Customer_Details cd left join Sections S on cd.Section=S.ID left join Para_Products pp ON cd.ProdID=pp.ProdID left join PremiumPayments pr on cd.PolicyNo=pr.PolicyNo  where Client_Type='SSB' and Date_Joined between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "'")

                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=SSBDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                ElseIf f_type = "Matured" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT PolicyNo,Surname,FName,Gender,IDNO,ECNO,Date_Joined,S.Section,pp.ProdName,cd.Premium FROM Customer_Details cd left join Sections S ON cd.Section=S.ID left join Para_Products pp ON cd.ProdID=pp.ProdID where Client_Type='SSB' and MaturityDate between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "' and isMatured=1 ")

                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=SSBDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                ElseIf f_type = "Terminated" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT PolicyNo,Surname,FName,Gender,IDNO,ECNO,Date_Joined,S.Section,pp.ProdName,cd.Premium FROM Customer_Details cd left join Sections S ON cd.Section=S.ID left join Para_Products pp ON cd.ProdID=pp.ProdID where Client_Type='SSB' and TerminationDate between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "' and isTerminated=1 ")

                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=SSBDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                End If
            ElseIf CusType = "Group" Then
                If f_type = "New" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT isnull(DATEADD(DAY,30,cd.Date_Joined),'')PaymentDate,cd.PolicyNo,Surname,FName,Gender,IDNO,Date_Joined,tg.CompanyName,pp.ProdName,cd.Premium,cd.GroupNumber FROM Customer_Details cd left join tbl_Groups tg on cd.GroupNumber=tg.GroupNumber left join Para_Products pp ON cd.ProdID=pp.ProdID left join PremiumPayments pr on cd.PolicyNo=pr.PolicyNo   where cd.Client_Type='Group' and cd.Date_Joined between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "' and cd.GroupNumber='" & cmbgrps.SelectedValue & "'")
                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=GroupDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                ElseIf f_type = "Matured" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT cd.PolicyNo,cd.Surname,cd.FName,cd.Gender,cd.IDNO,cd.Date_Joined,tg.CompanyName,pp.ProdName,cd.Premium FROM Customer_Details cd left join tbl_Groups tg ON cd.GroupNumber=tg.GroupNumber left join Para_Products pp ON cd.ProdID=pp.ProdID where cd.Client_Type='Group' and cd.MaturityDate between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "' and cd.GroupNumber='" & cmbgrps.SelectedValue & "' ")
                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=GroupDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using

                ElseIf f_type = "Terminated" Then
                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                        Using cmd As New SqlCommand("SELECT cd.PolicyNo,cd.Surname,cd.FName,cd.Gender,cd.IDNO,cd.Date_Joined,tg.CompanyName,pp.ProdName,cd.Premium FROM Customer_Details cd left join tbl_Groups tg ON cd.GroupNumber=tg.GroupNumber left join Para_Products pp ON cd.ProdID=pp.ProdID  where cd.Client_Type='Group' and cd.TerminationDate between '" & txtstartDate.Text & "' and '" & txtEnddate.Text & "' and cd.GroupNumber='" & cmbgrps.SelectedValue & "' ")
                            Using sda As New SqlDataAdapter()
                                cmd.Connection = con
                                sda.SelectCommand = cmd
                                Using dt As New DataTable()
                                    sda.Fill(dt)
                                    Using wb As New XLWorkbook()
                                        wb.Worksheets.Add(dt, txtfileDate.Text.ToString)

                                        Response.Clear()
                                        Response.Buffer = True
                                        Response.Charset = ""
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                                        Response.AddHeader("content-disposition", "attachment;filename=GroupDownload.xlsx")
                                        Using MyMemoryStream As New MemoryStream()
                                            wb.SaveAs(MyMemoryStream)
                                            MyMemoryStream.WriteTo(Response.OutputStream)
                                            Response.Flush()
                                            Response.End()
                                        End Using
                                    End Using
                                End Using
                            End Using
                        End Using
                    End Using
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        generateSSBfile(rdbType.SelectedValue)
    End Sub

    Protected Sub rdbType_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rdbType.SelectedValue = "Group" Then
            cmbgrps.Visible = True
        Else
            cmbgrps.Visible = False

        End If
    End Sub
    Protected Sub loadGroups()
        Try
            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("Constring").ConnectionString)
                Using cmd = New SqlCommand("Select GroupNumber,GroupName+'--'+pp.ProdName display from tbl_Groups tg left join Para_Products pp ON tg.ProdID=pp.ProdID ", con)
                    Dim ds As New DataSet
                    Dim adp = New SqlDataAdapter(cmd)
                    adp.Fill(ds, "cou")
                    If ds.Tables(0).Rows.Count > 0 Then

                        cmbgrps.DataSource = ds.Tables(0)
                        cmbgrps.DataTextField = "display"
                        cmbgrps.DataValueField = "GroupNumber"

                    Else
                        cmbgrps.DataSource = Nothing

                    End If

                    cmbgrps.DataBind()

                    cmbgrps.Items.Insert(0, "--Select Option--")

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class



