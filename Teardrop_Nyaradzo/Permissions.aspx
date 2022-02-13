<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Permissions.aspx.vb" Inherits="Teardrop_Nyaradzo.Permissions" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">    
     <div class="nav nav-tabs bg-info">
        <h4 style="padding-left:20px;color:white;">System Permissions</h4>
       </div>
    
    <br>
       <div class="row">
           
                                    <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Role:
                                      </div>
         &nbsp;&nbsp;  
           
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbRoles" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="cmbRoles_SelectedIndexChanged">
                                             
                                            </asp:Dropdownlist>
                                   
                                        </div>
                            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Category:
                                      </div>
         &nbsp;&nbsp;  
           
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbcategory" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                             
                                            </asp:Dropdownlist>
                                   
                                        </div>
       
          </div>
    <br>
    
    <br>
    <div class="row">
         <div class="col-xs-12 center-block label-info control-label" style="padding-left:50px;">
                    <asp:Label ID="lblnewmodules" runat="server" Text="New Modules" visible="false"></asp:Label>
                </div>
        </div>
            <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:50px;">
                             <asp:gridview id="grdNewModules" runat="server" horizontalalign="Center" AutoGenerateColumns="False"  captionalign="Top" emptydatatext="No Modules" Visible="False" CellPadding="4" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" />
                                 <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                 <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#330099" />
                                 <columns>
                                   <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkMod" runat="server" AutoPostBack="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Module ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModId" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField HeaderText="Module">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModName" runat="server" Text='<%# Bind("Module_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                             
                                                <asp:TemplateField HeaderText="URL">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblURLName1" runat="server" Text='<%# Bind("URL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                                <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblcusNo" runat="server" Text='<%# Bind("Module_Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                               
                                     </columns>
                                 <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                 <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                 <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                 <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                 <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:gridview>
             </div>
   
        </div>

        <div class="row">
                <div class="col-xs-12 center-block label-info control-label" style="padding-left:50px;">
                    <asp:Label ID="Label29" runat="server" Text="Modules" ></asp:Label>
                </div>
            </div>
            <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:50px;">
                             <asp:gridview id="grdModuleDetails" runat="server" horizontalalign="Center" AutoGenerateColumns="False" caption="Modules" captionalign="Top" emptydatatext="No Modules" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                 <EditRowStyle BackColor="#2461BF" />
                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#EFF3FB" />
                                 <columns>
                                   <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkModule" runat="server" AutoPostBack="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Module ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleId" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField HeaderText="Module">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("Module_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                             
                                                <asp:TemplateField HeaderText="URL">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblURLName" runat="server" Text='<%# Bind("URL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                                <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblcusNo" runat="server" Text='<%# Bind("Module_Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                               
                                     </columns>
                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                 <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                 <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                 <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                 <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:gridview>
             </div>
   
        </div>
    <br>

 
      <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:40px;">
                             <asp:gridview id="grdPermissions" runat="server" horizontalalign="Center" AutoGenerateColumns="False" caption="Modules" captionalign="Top" emptydatatext="No Modules" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                 <EditRowStyle BackColor="#2461BF" />
                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#EFF3FB" />
                                 <columns>
                                   <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkModule" runat="server" AutoPostBack="true" Checked='<%# Bind("ALWView") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Module ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleId" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField HeaderText="Module">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("Module_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                             
                                                <asp:TemplateField HeaderText="URL">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblURLName" runat="server" Text='<%# Bind("URL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                                <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblcusNo" runat="server" Text='<%# Bind("Module_Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     
                                               
                                     </columns>
                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                 <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                 <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                 <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                 <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:gridview>
             </div>
   
        </div>
    <br>
    <div class="row">
          <div class="col-xs-6" style="padding-left:250px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click" UseSubmitBehavior="false"/>
                </div>
           <div class="col-xs-6" style="padding-left:20px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" UseSubmitBehavior="false"/>
                </div>
        </div>
    <br><br>
     </asp:Panel>
           <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

