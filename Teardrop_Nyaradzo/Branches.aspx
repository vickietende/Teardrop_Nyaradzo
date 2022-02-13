<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Branches.aspx.vb" Inherits="Teardrop_Nyaradzo.Branches" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">   
        <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Add Branches</h4>
       
    </div>
     <br>
       <div class="row">
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Search Branch :
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchBranch" runat="server" autocomplete="off"  Width="400px"></asp:TextBox>
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchBranch" runat="server" Text=">>" />
                </div>
        </div>
       <br>
     
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" ></asp:ListBox>
                </div>
          </div>
    <br>
        <div class="row">
                       <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Branch Name:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtbranchname" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>
            &nbsp;&nbsp;
                      <div class="col-xs-2 control-label">
                                            Branch Code:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtbranchID" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                </div>
            </div>
    <br>
          <div class="row">
             
          <div class="col-xs-6" style="padding-left:300px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnAddBranch" runat="server" Text="Add Branch" OnClick="btnAddBranch_Click"  />
                </div>
              &nbsp;&nbsp;
              <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnDelBranch" runat="server" Text="Delete" OnClick="btnDelBranch_Click" />
                </div>
                 &nbsp;&nbsp;
              <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" />
                </div>
            </div>
     
    <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Branches</h4>
        </div><br>
    <div class="row">
                                    <div class="col-xs-12" style="padding-left:20px;">
                                        <asp:GridView ID="grdBranches" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                            <Columns>
               <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkBtnUpd" runat="server" CausesValidation="true"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                   &nbsp; <asp:LinkButton ID="lnkBtnCan" runat="server" CausesValidation="true"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                     <asp:LinkButton ID="lnkBtnDel" runat="server" CausesValidation="true"
                                        CommandName="Delete"  Text="Delete" ></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnEdt" runat="server" CausesValidation="true"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                                             
                                             
                                                <asp:TemplateField HeaderText="Branch Code">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Branch_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtBranchNameEdit" runat="server" Text='<%# Bind("Branch_Name") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("Branch_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="rowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                                            <sortedascendingheaderstyle backcolor="#506C8C" />
                                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
    <br/>
    </asp:Panel>
        <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

