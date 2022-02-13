<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="IssueCashBacks.aspx.vb" Inherits="Teardrop_Nyaradzo.IssueCashBacks" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
      <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Issue CashBack</h4>
          
</div>
    
    <br>
       <div class="row">
     
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label1" runat="server" Text="Search Customer"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchSurname" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍" OnClick="btnSearchSurname_Click" />
                </div>
              <div class="col-xs-2 control-label" style="padding-left:50px;">
                                             Date 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtTrxndate" runat="server" width="200px" autocomplete="off"  TextMode="Date"></asp:TextBox>
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
                                             Policy Plan
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:25px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px" >
                                              
                                                
                                            </asp:DropDownList>
                                        </div>
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             PolicyNo 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtPolicyNo" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
                        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Has CashBack?
                                      </div>
             <div class="col-xs-4" style="padding-left:20px;">
                             <asp:CheckBox ID="ChkHasCashBack" runat="server" AutoPostBack="true" />
                            </div>
            
          </div>
    <br>
    <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Surname 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:57px;">
                    <asp:TextBox  ID="txtSurname" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Name 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:41px;">
                    <asp:TextBox  ID="txtname" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             IDNO
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:110px;">
                    <asp:TextBox  ID="txtIDNO" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
         </div>
    <br>
    <div class="row">
    <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            First Payment  
                                      </div>
         
          <div style="padding-left:32px;">
                    <asp:TextBox  class='input-group date' id='txtFirstPaymentdate' runat="server" width="200px"  CssClass="xdsoft_datepicker"></asp:TextBox>
          
                </div>
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Period   
                                      </div>
          
          <div style="padding-left:46px;">
                    <asp:TextBox  class='input-group date' id='txtPeriod' runat="server" width="200px"  CssClass="xdsoft_datepicker"></asp:TextBox>
          
                </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                          Product Period   
                                      </div>
          
          <div style="padding-left:48px;">
                    <asp:TextBox  class='input-group date' id='txtCashbackPeriod' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false"></asp:TextBox>
          
                </div>

        </div>
    <br>
    <div class="row">
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Contributions 
                                      </div>
         
          <div style="padding-left:33px;">
                    <asp:TextBox  class='input-group date' id='txtcontributions' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false"></asp:TextBox>
          
                </div>
          <div class="col-xs-2 control-label" style="padding-left:12px;">
                                            Cashback(%) 
                                      </div>
         
          <div style="padding-left:9px;">
                    <asp:TextBox  class='input-group date' id='txtCashbackPercentage' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false"></asp:TextBox>
          
                </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Amount Paid 
                                      </div>
         
          <div style="padding-left:63px;">
                    <asp:TextBox  class='input-group date' id='txtAmntPaid' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false"></asp:TextBox>
          
                </div>
        </div>
    <br>
      <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Branch
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:56px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbBranch" runat="server" AutoPostBack="true" width="200px" >
                                              </asp:DropDownList>
                                        </div>

            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Account
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:12px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbAccount" runat="server" AutoPostBack="true" width="200px" >
                                              </asp:DropDownList>
                                        </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Payment Method
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:10px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbPayMethod" runat="server" AutoPostBack="true" width="200px" >
                                                 <asp:ListItem Text="Select" Value="-1" ></asp:ListItem> 
                                                <asp:ListItem Text="Cash" Value="Cash" ></asp:ListItem>
                                                <asp:ListItem Text="Ecocash" Value="Ecocash" ></asp:ListItem>
                                                 <asp:ListItem Text="OneMoney" Value="OneMoney" ></asp:ListItem>
                                                 <asp:ListItem Text="Telecash" Value="Telecash" ></asp:ListItem> 
                                                 <asp:ListItem Text="Swipe" Value="Swipe" ></asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>
          </div>
     <br>
    <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server" Text="Process" OnClick="btnSave_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
        <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Previous CashBack Received</h4>
          
</div>
    <br>
    <asp:gridview id="grdPreviousCashBack" runat="server" horizontalalign="Center" AutoGenerateColumns="true" caption="Last Received" captionalign="Top" emptydatatext="No Previous CashBacks" Visible="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                 <EditRowStyle BackColor="#999999" />
                                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                 <columns>
                               
                                
                                     
                                     </columns>
                                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                 <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                 <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                 <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                 <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:gridview>
        </asp:Panel>
     <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

