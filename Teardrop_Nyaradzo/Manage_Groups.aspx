<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Manage_Groups.aspx.vb" Inherits="Teardrop_Nyaradzo.Manage_Groups" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Group Management</h4>
      </div>
    <br>
      <div class="row">
             <div class="col-xs-4" style="padding-left:535px;">
              <asp:CheckBox ID="ChkUpdateProd" Text="Upgrade Group Product" runat="server"  AutoPostBack="true" OnCheckedChanged="ChkUpdateProd_CheckedChanged"/>
            
                            </div>
          <div class="col-xs-4" style="padding-left:50px;">
              <asp:CheckBox ID="ChckPremiumEdit" Text="Edit Group Premium" runat="server"  AutoPostBack="true" OnCheckedChanged="ChckPremiumEdit_CheckedChanged"/>
            
                            </div>
        
          </div>
    <div class="row">
                        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label1" runat="server" Text="Search Group"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchGroup" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchGroup" runat="server" Text="🔍" OnClick="btnSearchGroup_Click" />
                </div>
         <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                               Group Number
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:37px;">
                            <asp:TextBox  ID="txtgrpNumber" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
        
        </div>
    <br>
     
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstGroups" runat="server" AutoPostBack="True" Visible="false" CssClass="col-xs-12" OnSelectedIndexChanged="lstGroups_SelectedIndexChanged"></asp:ListBox>
                </div>
          
          </div>
      <br>
       <div class="row">
                        <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                               Group Name
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:15px;">
                            <asp:TextBox  ID="txtgrpname" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
           <div class="col-xs-3 control-label" style="padding-left:10px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Company
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:14px;">
                            <asp:TextBox  ID="txtcompany" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
               <div class="col-xs-3 control-label" style="padding-left:13px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Contact
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:54px;">
                            <asp:TextBox  ID="txtgroupcontact" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
    <br>
     <div class="row">
                        <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                               HR.Rep
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:51px;">
                            <asp:TextBox  ID="txtRepName" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
           <div class="col-xs-3 control-label" style="padding-left:2px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Rep. Contact
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:4px;">
                            <asp:TextBox  ID="txtRepContact" runat="server" Width="200px"   autocomplete="off"></asp:TextBox>
                           
                        </div>
               <div class="col-xs-3 control-label" style="padding-left:10px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Bus. Address
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:14px;">
                            <asp:TextBox  ID="txtBusAddress" runat="server" Width="200px"  autocomplete="off" TextMode="MultiLine"></asp:TextBox>
                           
                        </div>
                    </div>
                        <br>
                    <div class="row" style="padding-left:20px;">
                        <asp:label class="col-xs-2 control-label"  runat="server" >
                                Policy Plan
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:26px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbPolicyPlans"   runat="server"  width="200px">
                                                 </asp:DropDownList>
                                        </div>
                            <asp:label class="col-xs-2 control-label" style="padding-left:3px;" runat="server" >
                                Branch
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:43px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrpBranch"   runat="server"  width="200px">
                                                 </asp:DropDownList>
                                        </div>
                           <div class="col-xs-3 control-label" style="padding-left:12px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Premium
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:43px;">
                            <asp:TextBox  ID="txtgrpPremium" runat="server" Width="200px"  autocomplete="off" ></asp:TextBox>
                           
                        </div>
                        </div>
    <br>
     <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:400px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddGrp" runat="server" Text="Add" OnClick="btnAddGrp_Click" UseSubmitBehavior="false"/>
                        </div>
           <div class="col-xs-12 text-center" style="padding-left:10px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnUpgradeGrpProd" runat="server" Text="Upgrade Product" OnClick="btnUpgradeGrpProd_Click" UseSubmitBehavior="false"/>
                        </div>
         <div class="col-xs-12 text-center" style="padding-left:10px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEditGrp" runat="server" Text="Edit" OnClick="btnEditGrp_Click" UseSubmitBehavior="false"/>
                        </div>
             <div class="col-xs-12 text-center" style="padding-left:10px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEditPremium" runat="server" Text="Edit Premium" OnClick="btnEditPremium_Click" UseSubmitBehavior="false"/>
                        </div>
         <div class="col-xs-12 text-center" style="padding-left:10px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" UseSubmitBehavior="false"/>
                        </div>
                       
                    </div>
<div class="row">
      <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                               Group Total
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:37px;">
                            <asp:TextBox  ID="txtGroupTotal" runat="server" Width="50px"  autocomplete="off" Enabled="false"></asp:TextBox>
                           
                        </div>
    </div>
    <br>
      <br>
     <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdGroupMembers" runat="server" horizontalalign="Center" AllowPaging="true" AutoGenerateColumns="False" caption="Members" captionalign="Top" emptydatatext="No Members" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                 <EditRowStyle BackColor="#999999" />
                                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                 <columns>
                              
                                     <asp:TemplateField HeaderText="PolicyNo">
                                         
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPolicyNo" runat="server" Text='<%# Bind("PolicyNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Name">
                                                  
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgender" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="IDNO">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("IDNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="PhoneNo">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("PhoneNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Address">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroceryAmt" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
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
     <div class="row">
        <asp:Label ID="lblAgree" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblEncAgree" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    <br>
    <br>
         <a data-target="#SubmitModal" role="button" class="btn" data-toggle="modal" id="launchSubmit" style="height: 0;" data-backdrop="static"></a>
     <div id="SubmitModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        <h4 class="modal-title">Group Policy Successful</h4>
                    </div>
                    <div class="modal-body panel-body small">
                        <h5>You have successfully registered a Group funeral policy with Group Number: <b><%= lblAgree.Text %></b>.<br />
                             You can now &nbsp;
                        <a href="Manage_Groups.aspx?PolicyNo=<%= lblEncAgree.Text %>">Click here to continue...</a>.</h5>
                    </div>
                    <div class="modal-footer">
                       <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
  <script type="text/javascript">
        //function showPopup() {
        //    $("#launchSubmit").click();
        //}
      function showPopup(){
          document.getElementById('launchSubmit').click();
      }
   
    </script>
          </asp:Panel>
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

