<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Roles.aspx.vb" Inherits="Teardrop_Nyaradzo.Roles" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">     
     <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Add User Role</h4>
       
    </div>
     <br>
      <div class="row">
                   <div class="col-xs-2 control-label"style="padding-left:20px;">
                                            Search Role :
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchRole" autocomplete="off"  runat="server" Width="400px"></asp:TextBox>
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchRole" runat="server" Text="🔍"  />
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
                                            Role Name:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtrolename" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>
              &nbsp;&nbsp;
                      <div class="col-xs-2 control-label">
                                            Role ID:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtroleID" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                </div>
            &nbsp;&nbsp;
                  <div class="col-xs-2 control-label">
                                            Position:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtposition" autocomplete="off"  runat="server" Width="200px"></asp:TextBox>
                </div>
             &nbsp;&nbsp;
            </div>
   <br>
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Dashboard :
                                      </div>
         &nbsp;&nbsp;&nbsp;
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbroletype" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                                <asp:ListItem Text="Overall" Value="Overall"></asp:ListItem>
                                            </asp:Dropdownlist>
                                    
                                        </div>
        </div>
    <br>
        <div class="row">
               
          <div class="col-xs-6" style="padding-left:300px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Add Role" OnClick="btnAdd_Click" UseSubmitBehavior="false"/>
                </div>
              &nbsp;&nbsp;
              <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnDel" runat="server" Text="Delete" OnClick="btnDel_Click" UseSubmitBehavior="false"/>
                </div>
                  &nbsp;&nbsp;
              <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" UseSubmitBehavior="false"/>
                </div>
            </div>
    <br>
       
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Roles</h4>
        </div><br>
    <div class="row">
                                    <div class="col-xs-12"  style="padding-left:20px;">
                                        <asp:GridView ID="grdRoles" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                                             
                                             
                                                <asp:TemplateField HeaderText="Role Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtRoleedit" runat="server" Text='<%# Bind("Role_Name") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrole" runat="server" Text='<%# Bind("Role_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role ID">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("Role_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   
                                                
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="rowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
    <br/>
    </asp:Panel>
       <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>
