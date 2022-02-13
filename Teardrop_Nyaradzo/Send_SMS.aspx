<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Send_SMS.aspx.vb" Inherits="Teardrop_Nyaradzo.Send_SMS" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
       <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Broadcast SMS</h4>
        </div>
    <br>
    <div class="row">
          <div class="col-xs-2 control-label">
                                            Customer Type 
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp;  
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbType" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true"  CausesValidation="true" OnSelectedIndexChanged="rdbType_SelectedIndexChanged">
                                      <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                              <asp:ListItem Text="Group" Value="Group"></asp:ListItem>
                                    </asp:RadioButtonList>
                  <asp:RequiredFieldValidator ID="rfvtype" ControlToValidate="rdbType" ValidationGroup="myIndvCus"  ErrorMessage="Description is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
                            </div>
         <asp:label class="col-xs-2 control-label" ID="lblgroups"  runat="server" Font-Size="Medium" Visible="false"  style="padding-left:40px;">
                                Groups
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrps" Font-Size="Medium" runat="server" AutoPostBack="true" width="200px" Visible="false" OnSelectedIndexChanged="cmbgrps_SelectedIndexChanged">
                                            
                                                
                                            </asp:DropDownList>
                                        </div>
         <div class="col-xs-2 control-label" style="padding-left:10px;">
                                             Branch
                                      </div>
          
     <div class="col-xs-4" style="padding-left:10px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbbranches" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="cmbbranches_SelectedIndexChanged">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                             
                                            </asp:Dropdownlist>
  
                                        </div>
        </div>
    <br>
    <div class="row">
          
        </div>
    <br>
      <div class="row">
     
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="lblsearch" runat="server" Text="Search Customer"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchSurname" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍" OnClick="btnSearchSurname_Click" UseSubmitBehavior="false"/>
                </div>
            <div class="col-xs-4" style="padding-left:20px;">
                             <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" Text="Select All"/>
                            </div>
            
                </div>
                     
     <br>
     <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" OnSelectedIndexChanged="lstSurnames_SelectedIndexChanged"></asp:ListBox>
                </div>
          </div>
            <br>
       <div class="row">
                         
            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Policy Plan
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:25px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged">
                                      
                                                
                                            </asp:DropDownList>
                                        </div>
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             PolicyNo 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtPolicyNo" runat="server" width="200px" autocomplete="off" Enabled="false" ></asp:TextBox>
                </div>
            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Phone No.
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtphoneNo" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
           
          </div>
    <br>
    <div class="row">
            
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtmessage" runat="server" width="623px" autocomplete="off" TextMode="MultiLine" Placeholder="Your Message here..."></asp:TextBox>
                </div>
        </div>
<br>
    <div class="row">
          <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSend" runat="server" Text="Send SMS" Onclick="btnSend_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
       <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;"> 
        Listing
    </h4>
        </div>
            <br>
    <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdCustomers" runat="server" horizontalalign="Center" AutoGenerateColumns="False" caption="List" captionalign="Top" emptydatatext="No Data" Visible="False" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                 <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F7DE" />
                                 <columns>
                                           <asp:TemplateField ShowHeader="False">
                            
                                <ItemTemplate>
                            
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Surname">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSurname" runat="server" Text='<%# Bind("Surname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Name">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnameedit" runat="server" Text='<%# Bind("FName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("ProdName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Branch">
                                               
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("Branch_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderText="Phone">
                                            
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone" runat="server" Text='<%# Bind("PhoneNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Premium">
                                            
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpremium" runat="server" Text='<%# Bind("Premium") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                  
                                     </columns>
                                 <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                 <SortedAscendingHeaderStyle BackColor="#848384" />
                                 <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                 <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:gridview>
             </div>
   
        </div>
</asp:Panel>
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

