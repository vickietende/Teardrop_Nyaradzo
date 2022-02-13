<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Non_PolicyHolders.aspx.vb" Inherits="Teardrop_Nyaradzo.Non_PolicyHolders" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
     <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Non-Policy Holders</h4>
         
        </div>
    <br>
  
    <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Surname"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtsurname" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
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
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbGender" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>

        </div>
 
      <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvsurname" ControlToValidate="txtsurname"  ValidationGroup="myIndvCus"  ErrorMessage="Surname is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
                      <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtsurname" ErrorMessage="Only alphabets are allowed"   ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
         <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvname" ControlToValidate="txtname" ValidationGroup="myIndvCus"  ErrorMessage="Name is Required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
                       <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtname" ErrorMessage="Only alphabets are allowed"   ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
        </div>
   
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             ID Number 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <div >
                    <asp:TextBox  ID="txtIDNO" runat="server" autocomplete="off" width="200px" data-toggle="tooltip" data-placement="top" ToolTip="Valid format: 012345678A90" ></asp:TextBox>
                </div>
         &nbsp; 
             <div class="col-xs-2 control-label" >
                                             Marital Status 
                                      </div>
       
         &nbsp;&nbsp;  
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbmaritalstatus" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                                <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                 <asp:ListItem Text="Separated" Value="Separated"></asp:ListItem>
                                                   <asp:ListItem Text="Divorced" Value="Divorced"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        
          <div class="col-xs-2 control-label">
                                             D.O.B 
                                      </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

      
        <div >
                    <asp:TextBox  class='input-group date' id='txtDOB' runat="server" width="200px" TextMode="Date" CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>

        </div>
    <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvIDNO" ControlToValidate="txtIDNO"  ValidationGroup="myIndvCus"  ErrorMessage="IDNO is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:20px;">
               <asp:RegularExpressionValidator Display="dynamic" ID="revidno" runat="server" ControlToValidate="txtIDNO" ValidationGroup="myIndvCus" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small" ValidationExpression="\d{8,9}[a-zA-Z]\d{2}" ErrorMessage="Enter a valid ID Number"></asp:RegularExpressionValidator>
</div>
         <div style="padding-left:600px;">
        <asp:RequiredFieldValidator ID="rfvbirthdate" ControlToValidate="txtDOB" ValidationGroup="myIndvCus"  ErrorMessage="DOB is Required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
        </div>
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Address 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div >
                    <asp:TextBox  ID="txtaddress" runat="server" autocomplete="off" TextMode="MultiLine" width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
            <div class="col-xs-2 control-label">
                                             Phone No.  
                                      </div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;     
        <div >
                    <asp:TextBox  ID="txtphone" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
         &nbsp;  &nbsp;&nbsp;    
           <div class="col-xs-2 control-label">
                                             EC Number 
                                      </div>
        &nbsp;&nbsp; 
        <div >
                    <asp:TextBox  ID="txtecNum" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>
       
        </div>
        <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvaddress" ControlToValidate="txtaddress"  ValidationGroup="myIndvCus"  ErrorMessage="Address is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
           <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvphone" ControlToValidate="txtphone"  ValidationGroup="myIndvCus"  ErrorMessage="Phone is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:20px;">
             <asp:RegularExpressionValidator ID="revphone" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtphone" ErrorMessage="Invalid Number" ValidationGroup="myIndvCus"  ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
</div>
         <div style="padding-left:250px;">
      <asp:RegularExpressionValidator ID="revecnnum" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtecNum" ErrorMessage="Invalid EC Number" ValidationGroup="myIndvCus"  ValidationExpression="\d{7}"></asp:RegularExpressionValidator>
            </div>
        </div>
       <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Date 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
          <div style="padding-left:40px;">
                    <asp:TextBox  class='input-group date' id='txtDateCaptured' runat="server" width="200px"  CssClass="xdsoft_datepicker"  ></asp:TextBox>
          
                </div>
         
          <div class="col-xs-2 control-label" style="padding-left:10px;">
                                             Occupation
                                      </div>
          
            <div style="padding-left:20px;">
                    <asp:TextBox  class='input-group date' id='txtOccupation' runat="server" width="200px"  ></asp:TextBox>
                </div>
         &nbsp;
           <div class="col-xs-2 control-label" style="padding-left:13px;" >
                                             Employer
                                      </div>
          &nbsp;
          <div style="padding-left:20px;">
                    <asp:TextBox  class='input-group date' id="txtEmployer" runat="server" width="200px"  CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
         </div>
    <div class="row">
           <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="RfvDatecap" ControlToValidate="txtDateCaptured"  ValidationGroup="myIndvCus"  ErrorMessage="Capture Date is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
        </div>
         <div class="row">
                 <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label6" runat="server" Text="Contact(Bus)"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  
           <div >
                    <asp:TextBox  ID="txtbuscontact" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:17px;">
                                             Email
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <div >
                    <asp:TextBox  ID="txtemail" runat="server" width="200px" autocomplete="off" TextMode="Email" ></asp:TextBox>
                </div>
         &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
          <div class="col-xs-2 control-label">
                                             Branch
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp;  
           &nbsp;&nbsp;  
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbbranches" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                            </asp:Dropdownlist>
  
                                        </div>
         </div>
         <div class="row">
        <div style="padding-left:150px;">
       
             <div style="padding-left:250px;">
             <asp:RegularExpressionValidator ID="Revmail" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtemail" ErrorMessage="Invalid Email" ValidationGroup="myIndvCus"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
</div>
    
            </div>
             </div>
    
    <br>
    <div class="row">

             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Do you have a Funeral Policy?
                                      </div>
          &nbsp; 
             
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbPolicyHolder" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                 <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>
      
     
         </div>
      <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server"  Text="Save Details"   CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myIndvCus" OnClick="btnSave_Click" UseSubmitBehavior="false"/>
            </div>
        
      
        </div> 
    <br>
     <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Financial Details</h4>
        </div>
    <br>
    <div class="row">
              <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label4" runat="server" Text="Search Customer"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchSurname" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍"  OnClick="btnSearchSurname_Click"/>
                </div>
        </div>
    <br>
        <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="false" CssClass="col-xs-12" OnSelectedIndexChanged="lstSurnames_SelectedIndexChanged"></asp:ListBox>
                </div>
          
          </div>
    <br>
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label3" runat="server" Text="ID Number"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  
           <div style="padding-left:17px;">
                    <asp:TextBox  ID="txtCusIDNo" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
        <div class="col-xs-4" style="padding-left:120px;">
                             <asp:CheckBox ID="chkHearse" runat="server"  Text="Hearse"/>
                            </div>
         <div class="col-xs-4" style="padding-left:235px;">
                             <asp:CheckBox ID="ChkBus" runat="server"  Text="Bus"/>
                            </div>
        </div>
    <br>
       <div class="row">
                 <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label1" runat="server" Text="Amount Paid"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  
           <div >
                    <asp:TextBox  ID="txtAmtPaid" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:17px;">
                                             Fuel
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <div style="padding-left:15px;">
                    <asp:TextBox  ID="txtfuel" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
         &nbsp;&nbsp; &nbsp;
          <div class="col-xs-2 control-label">
                                             Coffin Type
                                      </div>
          &nbsp;&nbsp;   
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbCoffintype" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                       
                                             
                                            </asp:Dropdownlist>
  
                                        </div>
         </div>
    <br>
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Coffin Value
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
            <div >
                    <asp:TextBox  ID="txtcoffinvalue" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:3px;">
                                             Other Expenses
                                      </div>
          &nbsp;
            <div >
                    <asp:TextBox  ID="txtOtherExp" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
          <div class="col-xs-2 control-label" style="padding-left:13px;">
                                             Account
                                      </div>
          &nbsp;&nbsp;   
     <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbAccount" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                       
                                             
                                            </asp:Dropdownlist>
  
                                        </div>
        </div>
    <br>
    <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Receiving Acc
                                      </div>
          &nbsp;&nbsp;   
     <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbRecAccount" AutoPostBack="true" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                       
                                             
                                            </asp:Dropdownlist>
  
                                        </div>
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Payment Method
                                      </div>
          
        <div class="col-xs-4" style="padding-left:5px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbPayMethod" runat="server" AutoPostBack="true" width="200px" >
                                            
                                            </asp:DropDownList>
                                        </div>
        </div>
      <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveFin" runat="server"  Text="Process"   CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myIndvPay" OnClick="btnSaveFin_Click"/>
            </div>
        
      
        </div>
    <br>
     <br>
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
                        <h4 class="modal-title">Payment Successful</h4>
                    </div>
                    <div class="modal-body panel-body small">
                        <h5>Customer payment successful with TrxnID: <b><%= lblAgree.Text %></b>.<br />
                             You can now &nbsp;
                        <a href="NonPolicyReceipts.aspx?TrxnID=<%= lblEncAgree.Text %>">Print Receipt</a>.</h5>
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

