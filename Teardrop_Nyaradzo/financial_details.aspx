<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="financial_details.aspx.vb" Inherits="Teardrop_Nyaradzo.financial_details" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">    
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Individual Premium Payment Details</h4>

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
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍"  OnClick="btnSearchSurname_Click"/>
                </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                              <div class="col-xs-2 control-label">
                                            Customer Type 
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbType" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                    <asp:ListItem Text="SSB" Value="SSB"></asp:ListItem>
                                    
                                </asp:RadioButtonList>
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
                                           Payment Date 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
          <div >
                    <asp:TextBox  class='input-group date' id='txtpaymentDate' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false" ></asp:TextBox>
          
                </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;" >
                                             Date Paid
                                      </div>
          &nbsp;&nbsp;&nbsp; 

          <div >
                    <asp:TextBox  class='input-group date' id="txtTrxnDate" runat="server" width="200px" TextMode="Date"  CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
    </div>
    <br>
    <div class="row">
                          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Title
                                      </div>
              <asp:Label ID="Label3" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbtitle" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                    <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                    <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                                    <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                    <asp:ListItem Text="Dr" Value="Dr"></asp:ListItem>
                                    <asp:ListItem Text="Prof" Value="Prof"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
          &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
             <div class="col-xs-2 control-label">
                                             Policy Plan
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px">
                                               
                                                
                                            </asp:DropDownList>
                                        </div>
        
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label4" runat="server" Text="PolicyNo" Enabled="false" ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; 
           <div >
                    <asp:TextBox  ID="txtpolicyNo" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>

          </div>
    <br>
       <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Surname"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtsurname" runat="server" Width="200px" autocomplete="off" onkeypress="return isTextOnly(evt)"></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label">
                                             First Name(s) 
                                      </div>
         &nbsp;&nbsp;
           <div >
                    <asp:TextBox  ID="txtname" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div class="col-xs-2 control-label">
                                             Gender 
                                      </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbGender" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>

        </div>
      <br>
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             ID Number 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <div >
                    <asp:TextBox  ID="txtIDNO" runat="server" autocomplete="off" width="200px" data-toggle="tooltip" data-placement="top" ToolTip="Valid format: 012345678A90" ></asp:TextBox>
                </div>
         &nbsp;&nbsp; 
             <div class="col-xs-2 control-label" >
                                             EC Number 
                                      </div>
       
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <div >
                    <asp:TextBox  ID="txtECNum" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        
          <div class="col-xs-2 control-label">
                                             D.O.B 
                                      </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

      
        <div >
                    <asp:TextBox  class='input-group date' id='txtDOB' runat="server" width="200px"  CssClass="xdsoft_datepicker" Enabled="false" ></asp:TextBox>
          
                </div>

        </div>
         <br>
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                           Start Date 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
          <div >
                    <asp:TextBox  class='input-group date' id='txtDOC' runat="server" width="200px"  CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
         &nbsp;&nbsp;
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Term
                                      </div>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;  
          <div >
                    <asp:TextBox  class='input-group date' id="txtterm" runat="server" width="200px"  CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
         &nbsp;
           <div class="col-xs-2 control-label" >
                                             Maturity Date
                                      </div>
          &nbsp;
          <div >
                    <asp:TextBox  class='input-group date' id="txtMaturity" runat="server" width="200px"  CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
         </div>
      <br>
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Premium
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <div >
                    <asp:TextBox  ID="txtpremium" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:10px;">
                                          Amount Paid
                                      </div>
          &nbsp;&nbsp;&nbsp;  
            <div >
                    <asp:TextBox  ID="txtAmtpaid" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
         &nbsp;
          <div class="col-xs-2 control-label">
                                             Payment Method
                                      </div>
          &nbsp;  
            <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbMethod" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                              
                                            
                                            </asp:Dropdownlist>
  
                                        </div>
               <div class="col-xs-2" style="padding-left:2px;color:blueviolet;">
       <a data-target="#showPayMethodModal" role="button" class="" data-toggle="modal" id="launchPayMethodModal" >Add</a>
            </div>
         </div>
     <br>
   <div class="row">
                 <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Section
                                      </div>
          &nbsp;  
            <div class="col-xs-4" style="padding-left:70px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbSection" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                               
                                            
                                            </asp:Dropdownlist>
  
                                        </div>
       <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Account
                                      </div>
          &nbsp;  
            <div class="col-xs-4" style="padding-left:30px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbAccount" runat="server" CssClass="col-xs-12" Width="250px"
                                                RepeatDirection="Horizontal" >
                                               
                                            
                                            </asp:Dropdownlist>
  
                                        </div>
       </div>
    <br>
       <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server" Text="Process" Onclick="btnSave_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
      <div class="row">
        <asp:Label ID="lblAgree" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblEncAgree" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Recent Payments</h4>
        </div>
    <br>
      <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdPreviousPayments" runat="server" horizontalalign="Center"  caption="Previous Payments" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                 <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F7DE" />
                                 <columns>
                                   
                                     </columns>
                                 <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                 <SortedAscendingHeaderStyle BackColor="#848384" />
                                 <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                 <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:gridview>
             </div>
   
        </div>
     <a data-target="#SubmitModal" role="button" class="btn" data-toggle="modal" id="launchSubmit" style="height: 0;" data-backdrop="static"></a>
     <div id="SubmitModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        <h4 class="modal-title">Payment Successful</h4>
                    </div>
                    <div class="modal-body panel-body small">
                        <h5>Customer monthly premium successfully saved with TrxnID: <b><%= lblAgree.Text %></b>.<br />
                             You can now &nbsp;
                        <a href="PrintReceipts.aspx?TrxnID=<%= lblEncAgree.Text %>">Print Receipt</a>.</h5>
                    </div>
                    <div class="modal-footer">
                       <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
     <div id="showPayMethodModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Payment Method</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small" style="background-color:cornsilk;">
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                               Method
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:10px;">
                            <asp:TextBox  ID="txtAddMethod" runat="server" PlaceHolder="e.g Swipe,Ecocash,Telecash,OneMoney,Cash" Width="300px" Font-Size="Medium"></asp:TextBox>
                           
                        </div>
                    </div>
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAddMethod" runat="server" Text="Add" OnClick="btnAddMethod_Click" UseSubmitBehavior="false"/>
                        </div>
                    </div>
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

