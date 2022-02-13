<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="products.aspx.vb" Inherits="Teardrop_Nyaradzo.products" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">    
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Product Management</h4>
      </div>
    <br>
   <div class="row">
     
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label1" runat="server" Text="Search Product"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbProducts" runat="server" CssClass="col-xs-12" Width="400px"
                                                RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="cmbProducts_SelectedIndexChanged">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                               
                                            </asp:Dropdownlist>
  
                                        </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                              <div class="col-xs-2 control-label">
                                            Custom Plan 
                                      </div>
      
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
              <asp:CheckBox ID="ChkProdType" Text="Group" runat="server" OnCheckedChanged="ChkProdType_CheckedChanged" AutoPostBack="true"/>
            
                            </div>
                       
          <div class="col-xs-4" style="padding-left:30px;">
              <asp:CheckBox ID="ChckPremiumEdit" Text="Edit Product Premium" runat="server" OnCheckedChanged="ChckPremiumEdit_CheckedChanged" AutoPostBack="true"/>
            
                            </div>
      </div>
    <br>
       <div class="row">
     
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="lblsearchgroup" runat="server" Text="Group Name"  Visible="false" ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbgroups" runat="server" CssClass="col-xs-12" Width="400px"
                                                RepeatDirection="Horizontal" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="cmbgroups_SelectedIndexChanged">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                               
                                            </asp:Dropdownlist>
  
                                        </div>
      
      </div>
    <br>
    <div class="row">
     
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="lblcompanyname" runat="server" Text="Company Name" Visible="false"  ></asp:Label>          
                                      </div>
                    &nbsp;
         
           <div>
                    <asp:TextBox  ID="txtcompanyname" runat="server" autocomplete="off" width="200px" Visible="false"  ></asp:TextBox>
                </div>

        </div>
    <br>
    
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label2" runat="server" Text="Product Name"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp; &nbsp;
         
           <div >
                    <asp:TextBox  ID="txtprodname" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label3" runat="server" Text="Maturity Period(years)"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp; &nbsp;&nbsp;
              <div >
                    <asp:TextBox  ID="txtprodterm" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label4" runat="server" Text="Sum Assured"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp; &nbsp;
              <div >
                    <asp:TextBox  ID="txtsumAssured" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         </div>
    <br>
    <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label5" runat="server" Text="Premium"   ></asp:Label>          
                                      </div>
                   
         
           <div style="padding-left:54px;">
                    <asp:TextBox  ID="txtpremium" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
             <div class="col-xs-2 control-label" style="padding-left:39px;">
                                  <asp:Label ID="Label6" runat="server" Text="Start Period(Months)"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;  
         
           <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbperiod" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                 <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Has Cashback?
                                      </div>
              <asp:Label ID="Label11" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
                                             <asp:CheckBox ID="ChkcashBack" runat="server" AutoPostBack="true" OnCheckedChanged="ChkcashBack_CheckedChanged"/>
                            </div>
        
        </div>
    <br>
  <div class="row">
      <div class="col-xs-2 control-label" style="padding-left:10px;">
                                  <asp:Label ID="lblcashback" runat="server" Text="CashBack Period"  Visible="false" ></asp:Label>          
                                      </div>
        <div class="col-xs-4" style="padding-left:5px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbCashBackPeriod" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" Visible="false">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                  <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                 <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>
      <div class="col-xs-2 control-label" style="padding-left:5px;">
                                  <asp:Label ID="lblyears" runat="server" Text="(Years)" Visible="false"  ></asp:Label>          
                                      </div>
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="lblAmount" runat="server" Text="Amount(%)" Visible="false"  ></asp:Label>          
                                      </div>
               <div style="padding-left:41px;">
                    <asp:TextBox  ID="txtCashbackAmount" runat="server" autocomplete="off" width="200px" Visible="false" ></asp:TextBox>
                </div>
      </div>
     <br>
        <div class="row">
            <div class="col-xs-2" style="padding-left:20px;">
              <asp:CheckBox ID="chkgrocery" runat="server" Text="Grocery" CausesValidation="true"/>
                    
            </div>
            </div>
    <br>
    <div class="row">
       
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label8" runat="server" Text="Grocery Amount"   ></asp:Label>          
                                      </div>
               <div style="padding-left:5px;">
                    <asp:TextBox  ID="txtgroceryAmt" runat="server" autocomplete="off" width="200px"  ></asp:TextBox>
                </div>
       
          <div class="col-xs-2 control-label" style="padding-left:17px;">
                                  <asp:Label ID="Label7" runat="server" Text="Casket/Coffin Type"   ></asp:Label>          
                                      </div>
        <div class="col-xs-4" style="padding-left:42px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbcoffintype" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                          
                                              
                                               
                                            </asp:Dropdownlist>
  
                                        </div>
                      <div class="col-xs-2" style="padding-left:20px;color:blueviolet;">
       <a data-target="#showCoffinModal" role="button" class="" data-toggle="modal" id="launchCoffinModal">Add Coffin/Casket Type</a>
            </div>
         
        </div>
    
    <br>
    <div class="row">
         <div class="col-xs-2" style="padding-left:250px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server" Text="Add Product" OnClick="btnSave_Click" UseSubmitBehavior="false"/>
            </div>
         <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" UseSubmitBehavior="false"/>
            </div>
         <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEditPremium" runat="server" Text="Edit Product Premium" OnClick="btnEditPremium_Click" UseSubmitBehavior="false"/>
            </div>
         <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnDeactivate" runat="server" Text="De-Activate Product" OnClick="btnDeactivate_Click" UseSubmitBehavior="false"/> 
            </div>
         <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnActivate" runat="server" Text="Activate Product" OnClick="btnActivate_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
    
    <br>
    <div class="nav nav-tabs bg-info">
      <h4 style="padding-left:20px;color:white;">Available Products</h4>
</div>
    <br>
     <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdProducts" runat="server" horizontalalign="Center" AutoGenerateColumns="False" caption="Products" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                 <EditRowStyle BackColor="#999999" />
                                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
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
                                     <asp:TemplateField HeaderText="ProdID">
                                         
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("ProdID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtProdNameedit" runat="server" Text='<%# Bind("ProdName") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprodName" runat="server" Text='<%# Bind("ProdName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maturity">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtMaturityedit" runat="server" Text='<%# Bind("MaturityPeriod") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaturity" runat="server" Text='<%# Bind("MaturityPeriod") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="SumAssured">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtSumAssurededit" runat="server" Text='<%# Bind("SumAssured") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSumAssured" runat="server" Text='<%# Bind("SumAssured") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Premium">
                                                 <ItemTemplate>
                                                        <asp:Label ID="lblPremium" runat="server" Text='<%# Bind("Premium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Grocery Amt">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtGroceryAmtedit" runat="server" Text='<%# Bind("Grocery_Amt") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroceryAmt" runat="server" Text='<%# Bind("Grocery_Amt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                
                                     
                                     </columns>
                                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                 <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                 <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                 <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                 <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:gridview>
             </div>
   
        </div>
    <br>
       <div id="showCoffinModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Coffin/Casket</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                                Name
                            </asp:label>
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox  ID="txtAddCoffin" runat="server" Width="400px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddCoffin" runat="server" Text="Add" OnClick="btnAddCoffin_Click" UseSubmitBehavior="false"/>
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

