<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Master_Modules.aspx.vb" Inherits="Teardrop_Nyaradzo.Master_Modules" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark"> 
     <div class="nav nav-tabs bg-info">
        <h4 style="padding-left:20px;color:white;">Add Modules</h4>
       </div>
     
 
    <br>
        <div class="row">
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Search Role :
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchModule" autocomplete="off"  runat="server" Width="400px"></asp:TextBox>
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchModule" runat="server" Text=">>" OnClick="btnSearchModule_Click" UseSubmitBehavior="false"/>
                </div>
        </div>
       <br>
     
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" OnSelectedIndexChanged="lstSurnames_SelectedIndexChanged" ></asp:ListBox>
                </div>
          </div>
    <br>
        <div class="row">
                       <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Module:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtmodule" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>
              &nbsp;&nbsp;
                      <div class="col-xs-2 control-label">
                                            Module ID:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtmoduleID" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                </div>
            &nbsp;&nbsp;
                  <div class="col-xs-2 control-label">
                                            URL:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txturl" autocomplete="off"  runat="server" Width="250px"></asp:TextBox>
                </div>
             &nbsp;&nbsp;
            </div>
    <br>
      <div class="row">
           
                                    <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Category:
                                      </div>
         &nbsp;&nbsp;  
           
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbcategory" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                             
                                            </asp:Dropdownlist>
                                   
                                        </div>
              <div class="col-xs-2" style="padding-left:20px;">
       <a data-target="#showCategoryModal" role="button" class="" data-toggle="modal" id="launchCategoryModal">Add/Edit Module Category</a>
            </div>
          </div>
       
    <br>
 
   
    
            <div class="row">
             
          <div class="col-xs-6" style="padding-left:250px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Add Module" OnClick="btnAdd_Click" UseSubmitBehavior="false"/>
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
    <h4 style="padding-left:20px;color:white;">Modules</h4>
        </div><br>
    <div class="row">
                                    <div class="col-xs-12" style="padding-left:20px;">
                                        <asp:GridView ID="grdModules" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                                             
                                             
                                                <asp:TemplateField HeaderText="Module ID">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleID" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                   <EditItemTemplate>
                                                       <asp:TextBox ID="txtmoduleName" runat="server" Text='<%# Bind("Module_Name") %>'>></asp:TextBox>
                                                       </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("Module_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="URL">
                                                   <EditItemTemplate>
                                                       <asp:TextBox ID="txturlEdit" runat="server" Text='<%# Bind("URL") %>'>></asp:TextBox>
                                                       </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblurl" runat="server" Text='<%# Bind("URL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Category">
                                                 <EditItemTemplate>
                                                      <asp:TextBox ID="txtcategoryEdit" runat="server" Text='<%# Bind("Module_Category") %>'>></asp:TextBox>
                                                     </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("Module_Category") %>'></asp:Label>
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
    <br><br>
         <div id="showCategoryModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Module Category</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                                Category
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:40px;">
                            <asp:TextBox  ID="txtAddCategory" runat="server" Width="300px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                    <div class="row">
                          <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                                Category List
                            </asp:label>
                        </div>
                         <div class="col-xs-4" style="padding-left:5px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbcategoryEdit" runat="server" CssClass="col-xs-12" Width="300px" Font-Size="Medium"
                                                RepeatDirection="Horizontal">
                                             
                                            </asp:Dropdownlist>
                                   
                                        </div>
                        </div>
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddCategory" runat="server" Text="Add" OnClick="btnAddCategory_Click" UseSubmitBehavior="false"/>
                        </div>
                          <div class="col-xs-12 text-center" style="padding-left:20px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEditCategory" runat="server" Text="Edit" OnClick="btnEditCategory_Click" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
        </asp:Panel>
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

