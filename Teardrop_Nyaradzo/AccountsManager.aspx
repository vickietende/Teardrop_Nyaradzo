<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="AccountsManager.aspx.vb" Inherits="Teardrop_Nyaradzo.AccountsManager" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark"> 
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Manage Accounts</h4>
        </div>
     
    <br>
  
    <div class="row">
         <div class="col-xs-2 control-label"style="padding-left:20px;">
                                             Account Categories
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbCategories" runat="server" AutoPostBack="true" width="200px" >
                                        
                                                
                                            </asp:DropDownList>
                                        </div>
          <div class="col-xs-2" style="padding-left:20px;">
       <a data-target="#showCategoryModal" role="button" class="" data-toggle="modal" id="launchshowCategoryModal">Add Category</a>
            </div>

        </div>
    <br>
    <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdAccountCategories" AutoGenerateColumns="false" runat="server" horizontalalign="Center"  caption="Main Accounts" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="3" GridLines="Horizontal" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="#F7F7F7" />
                                 <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                 <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle CssClass="altrowstyle" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                 <columns>

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
                  
                           <asp:TemplateField HeaderText="Account Number">
                                             
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccNumberEdit" runat="server" Text='<%# Bind("AccNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtAccNameEdit" runat="server" Text='<%# Bind("AccName") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccNameEdit" runat="server" Text='<%# Bind("Accname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Acc Description">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtAccDescEdit" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccDescEdit" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Category Description">
                                             
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatDescEdit" runat="server" Text='<%# Bind("CategoryDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                     </columns>
                                 <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                 <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                 <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                 <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                 <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:gridview>
             </div>
        </div>
    <br>
    
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Add Main Accounts</h4>
        </div>
    <br>

    
      <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Account Name"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtMainAccName" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label"style="padding-left:20px;">
                                             Description 
                                      </div>
         &nbsp;&nbsp;
           <div >
                    <asp:TextBox  ID="txtMaindesc" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
        

        </div>
    <br>
          <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Add Main Account" OnClick="btnAdd_Click"  UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
     <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdSubAccounts" runat="server" horizontalalign="Center"  caption="Sub-Accounts" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="3" GridLines="Horizontal" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="#F7F7F7" />
                                 <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                 <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle CssClass="altrowstyle" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                 <columns>
                                        
                                     </columns>
                                 <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                 <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                 <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                 <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                 <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:gridview>
             </div>
        </div>
    <br>

    <br>
    <br>
        <div id="showCategoryModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Category</h4>
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
                            <asp:TextBox  ID="txtAddCategory" runat="server" Width="300px" Font-Size="Medium"></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                     <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                                Description
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:20px;">
                            <asp:TextBox  ID="txtCategotyDesc" runat="server" Width="300px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddSection" runat="server" Text="Add" OnClick="btnAddSection_Click" UseSubmitBehavior="false"/>
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

