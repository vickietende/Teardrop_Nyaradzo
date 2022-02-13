<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="customer_registration.aspx.vb" Inherits="Teardrop_Nyaradzo.customer_registration" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
      <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
 
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Customer Registration</h4>
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
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍"  OnClick="btnSearchSurname_Click" UseSubmitBehavior="false"/>
                </div>
         &nbsp;&nbsp; &nbsp; 
                              <div class="col-xs-2 control-label">
                                            Customer Type 
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp;  
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbType" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbType_SelectedIndexChanged" CausesValidation="true">
                                    <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                <asp:ListItem Text="Group" Value="Group"></asp:ListItem>
                                    
                                    </asp:RadioButtonList>
                  <asp:RequiredFieldValidator ID="rfvtype" ControlToValidate="rdbType" ValidationGroup="myIndvCus"  ErrorMessage="Description is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
                            </div>
              <div class="col-xs-2" style="padding-left:20px;color:blueviolet;">
       <a data-target="#showGroupModal" role="button" class="" data-toggle="modal" id="launchGroupModal">New Group</a>
            </div>
</div>
     <br>
     
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="false" CssClass="col-xs-12" OnSelectedIndexChanged="lstSurnames_SelectedIndexChanged"></asp:ListBox>
                </div>
          
          </div>
      <br>
     <div class="row" style="padding-left:20px;">
            <asp:label class="col-xs-2 control-label" ID="lblgroups"  runat="server" Font-Size="Medium" Visible="false">
                                Groups
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:77px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrps" Font-Size="Medium" runat="server" AutoPostBack="true" width="200px" Visible="false" OnSelectedIndexChanged="cmbgrps_SelectedIndexChanged">
                                            
                                                
                                            </asp:DropDownList>
                                        </div>
           <asp:label class="col-xs-2 control-label" ID="lblgrpcompany" style="padding-left:20px;" runat="server" Font-Size="Medium" Visible="false">
                               Company
                            </asp:label>
        &nbsp;&nbsp; 
        <div style="padding-left:13px;">
                    <asp:TextBox  ID="txtgrpcompany" runat="server" width="200px" autocomplete="off" Enabled="false" Visible="false"></asp:TextBox>
                </div>
          <asp:label class="col-xs-2 control-label" ID="Label10" style="padding-left:20px;" runat="server" Font-Size="Medium" Visible="false">
                               Group
                            </asp:label>
        &nbsp;&nbsp; 
        <div style="padding-left:13px;">
                    <asp:TextBox  ID="txtgroup" runat="server" width="200px" autocomplete="off" Enabled="false" Visible="false"></asp:TextBox>
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
                                <asp:RadioButtonList ID="rdbtitle" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" >
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
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged">
                                                 <asp:ListItem Text="Select" Value="-1" ></asp:ListItem> 
                                                
                                            </asp:DropDownList>
                                        </div>
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             PolicyNo 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:15px;">
                    <asp:TextBox  ID="txtPolicyNo" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
          </div>
      <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvtitle" ControlToValidate="rdbtitle"  ValidationGroup="myIndvCus"  ErrorMessage="Title is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvpolicyplan" ControlToValidate="cmbProduct" ValidationGroup="myIndvCus"  ErrorMessage="Product is Required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
        </div>
  
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
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtsurname" ErrorMessage="Only alphabets are allowed" ValidationGroup="myIndvCus"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
     <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvname" ControlToValidate="txtname" ValidationGroup="myIndvCus"  ErrorMessage="Name is Required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
             <div >
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtname" ErrorMessage="Only alphabets are allowed" ValidationGroup="myIndvCus"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

      
        <div >
                    <asp:TextBox  class='input-group date' id='txtDOB' runat="server" width="200px" AutoPostBack="true"  TextMode="Date" CssClass="xdsoft_datepicker" OnTextChanged="txtDOB_TextChanged"></asp:TextBox>
          
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
                    <asp:TextBox  class='input-group date' id='txtDatejoined' runat="server" width="200px"  CssClass="xdsoft_datepicker"  ></asp:TextBox>
          
                </div>
         &nbsp;&nbsp;
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Term
                                      </div>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;  
            <div >
                    <asp:TextBox  class='input-group date' id='txtTerm' runat="server" width="200px"  ></asp:TextBox>
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
      <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvdatejoined" ControlToValidate="txtDatejoined"  ValidationGroup="myIndvCus"  ErrorMessage="Date is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
           <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvterm" ControlToValidate="txtTerm"  ValidationGroup="myIndvCus"  ErrorMessage="Term is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
 
         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvMaturity" ControlToValidate="txtMaturity"  ValidationGroup="myIndvCus"  ErrorMessage="Maturity date is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
        </div>
  
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Premium
                                      </div>
          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <div >
                    <asp:TextBox  ID="txtpremium" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
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
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbbranches"  runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                               </asp:Dropdownlist>
  
                                        </div>
         </div>
     <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvpremium" ControlToValidate="txtpremium"  ValidationGroup="myIndvCus"  ErrorMessage="Premium is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
             <div style="padding-left:250px;">
             <asp:RegularExpressionValidator ID="Revmail" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtemail" ErrorMessage="Invalid Email" ValidationGroup="myIndvCus"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
         &nbsp;&nbsp;
                  <div class="col-xs-2 control-label" >
                                        <asp:Label ID="Label8" runat="server" Text="Sum Assured"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp;      
           <div >
                    <asp:TextBox  ID="txtsumassured" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <div class="col-xs-2 control-label" >
                                        <asp:Label ID="Label9" runat="server" Text="Employer"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp;      
           <div >
                    <asp:TextBox  ID="txtEmployer" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         </div>
    <br>
     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             1st Payment Date 
                                      </div>
          &nbsp;
            <div >
                    <asp:TextBox  ID="txtDOC" runat="server" width="200px" autocomplete="off" TextMode="Date"  ></asp:TextBox>
                </div>
            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Section 
                                      </div>
          &nbsp;
            <div class="col-xs-4" style="padding-left:35px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbSection" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" AutoPostBack="true">
                                                
                                            </asp:Dropdownlist>
  
                                        </div>
          <div class="col-xs-2" style="padding-left:20px;">
       <a data-target="#showSectionModal" role="button" class="" data-toggle="modal" id="launchSectionModal">Add Section</a>
            </div>
       <%--       <div class="col-xs-2" style="padding-left:20px;">
       <a data-target="#SubmitModal" role="button" class="" data-toggle="modal" id="launchSubmit">Trial</a>
            </div>--%>
         </div>
    <br>
    <div class="row">
            <div class="col-xs-2 control-label" style="padding-left:20px;" >
                                             No.of Dependants 
                                      </div>
       
         &nbsp;  
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbNoOfDependencies" runat="server" CssClass="col-xs-12" Width="50px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="" Value="0"></asp:ListItem>
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
                  <div class="col-xs-2 control-label" style="padding-left:150px;" >
                                             No.of Children
                                      </div>
       
         &nbsp;  
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbChidren" runat="server" CssClass="col-xs-12" Width="50px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="" Value="0"></asp:ListItem>
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
        </div>
 
    <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Spouse Details</h4>
        </div>
    <div class="row">
                      <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Title
                                      </div>
              <asp:Label ID="Label4" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbSpouseTitle" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                    <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                    <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                                    <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                    <asp:ListItem Text="Dr" Value="Dr"></asp:ListItem>
                                    <asp:ListItem Text="Prof" Value="Prof"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
        </div>
        <br>
        <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:30px;">
                                        <asp:Label ID="Label5" runat="server" Text="Full Name"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtspouseName" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label">
                                             ID Number 
                                      </div>
         &nbsp;&nbsp;
           <div >
                    <asp:TextBox  ID="txtspouseID" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div class="col-xs-2 control-label">
                                             Phone 
                                      </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div>
                                    <asp:TextBox  ID="txtspousephone" runat="server" autocomplete="off" width="200px" ></asp:TextBox>      
  
                                        </div>

        </div>
         <div class="row">
                 <div style="padding-left:150px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtspouseName" ErrorMessage="Only alphabets are allowed" ValidationGroup="myIndvCus"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
        
         <div style="padding-left:500px;">
          
               <asp:RegularExpressionValidator Display="dynamic" ID="revspouseid" runat="server" ControlToValidate="txtspouseID" ValidationGroup="myIndvCus" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small" ValidationExpression="\d{8,9}[a-zA-Z]\d{2}" ErrorMessage="Enter a valid ID Number"></asp:RegularExpressionValidator>

</div>
         <div style="padding-left:250px;">
        <asp:RegularExpressionValidator ID="revspousephone" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtspousephone" ErrorMessage="Invalid Number" ValidationGroup="myIndvCus"  ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
            </div>
        </div>
  
    <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:150px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server"  Text="Save Customer Details"  OnClick="btnSave_Click" CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myIndvCus" UseSubmitBehavior="false"/>
            </div>
                      <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnUpdatePolicy" runat="server" Text="Upgrade Policy" Onclick="btnUpdatePolicy_Click"  UseSubmitBehavior="false"/>
            </div>
        
                 <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnEdit" runat="server" Text="Edit Customer Details" OnClick="btnEdit_Click"  UseSubmitBehavior="false"/>
            </div>
                     <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnTerminate" runat="server" Text="Terminate Policy" OnClick="btnTerminate_Click" UseSubmitBehavior="false" />
            </div>
                 <div class="col-xs-2" style="padding-left:20px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnRefresh" runat="server" Text="🔁"  Onclick="btnRefresh_Click" UseSubmitBehavior="false"/>
            </div>
        </div> 
    <br>
            <div class="row">
                    <div class="col-xs-12 text-center" style="padding-left:200px;">
                        <asp:ValidationSummary ID="VSIndvCus" runat="server" HeaderText="Please correct the following errors and save again"
                            ShowMessageBox="false" ShowSummary="true"  DisplayMode="List"  BackColor="Snow" ForeColor="Red"
                            Font-Italic="true" ValidationGroup="myIndvCus" Font-Size="Small"/>
                    </div>
                </div>     
   <br>
    
    <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Beneficiaries</h4>
        </div>
    <br>
 
        <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label7" runat="server" Text="Surname"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtBenSurname" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label">
                                             First Name(s) 
                                      </div>
         &nbsp;&nbsp;
           <div >
                    <asp:TextBox  ID="txtBenfName" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div class="col-xs-2 control-label">
                                             Gender 
                                      </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbSex" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            </asp:Dropdownlist>
  
                                        </div>

        </div>
     <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvbensurname" ControlToValidate="txtBenSurname"  ValidationGroup="myIndvBen"  ErrorMessage="Surname is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
                  <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtBenSurname" ErrorMessage="Only alphabets are allowed" ValidationGroup="myIndvBen"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
        
         <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvbenname" ControlToValidate="txtBenfName" ValidationGroup="myIndvBen"  ErrorMessage="Name is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
                    <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtBenfName" ErrorMessage="Only alphabets are allowed" ValidationGroup="myIndvBen"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
      </div>

    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             ID Number 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <div >
                    <asp:TextBox  ID="txtBenIDNO" runat="server" autocomplete="off" width="200px" data-toggle="tooltip" data-placement="top" ToolTip="Valid format: 012345678A90" ></asp:TextBox>
                </div>
         &nbsp; 
             <div class="col-xs-2 control-label" >
                                             Marital Status 
                                      </div>
       
         &nbsp;&nbsp;  
     <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbBenMaritalStatus" runat="server" CssClass="col-xs-12" Width="200px"
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

      
        <div >
                    <asp:TextBox  class='input-group date' id='txtBenDOB' runat="server" width="200px" TextMode="Date" CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>

        </div>
        <div class="row">
      
         <div style="padding-left:20px;">
       <asp:RegularExpressionValidator Display="dynamic" ID="Revbenid" runat="server" ControlToValidate="txtBenIDNO" ValidationGroup="myIndvBen" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small" ValidationExpression="\d{8,9}[a-zA-Z]\d{2}" ErrorMessage="Enter a valid ID Number"></asp:RegularExpressionValidator>
            </div>
              <div style="padding-left:750px;">
        <asp:RequiredFieldValidator ID="rfvbendob" ControlToValidate="txtBenDOB"  ValidationGroup="myIndvBen"  ErrorMessage="DOB is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>

      </div>
       
    <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Address 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div >
                    <asp:TextBox  ID="txtBenAddress" runat="server" autocomplete="off" TextMode="MultiLine" width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
            <div class="col-xs-2 control-label">
                                             Phone No.  
                                      </div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;     
        <div >
                    <asp:TextBox  ID="txtBenContact" runat="server" width="200px" autocomplete="off"  ></asp:TextBox>
                </div>
         &nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
           <div class="col-xs-2 control-label">
                                             Employer 
                                      </div>
        &nbsp;&nbsp; 
        <div >
                    <asp:TextBox  ID="txtBenEmployer" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>
       
        </div>
       <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvbenaddress" ControlToValidate="txtBenAddress"  ValidationGroup="myIndvBen"  ErrorMessage="Address is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:250px;">
       <asp:RegularExpressionValidator Display="dynamic" ID="revbencontact" runat="server" ControlToValidate="txtBenContact" ValidationGroup="myIndvBen" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small" ValidationExpression="\d{10}" ErrorMessage="Invalid  Number"></asp:RegularExpressionValidator>
            </div>
      

      </div>
   
    <div class="row">
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Relationship
                                      </div>
        <div class="col-xs-4"  style="padding-left:40px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbRelationship" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal" >
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Child" Value="Child"></asp:ListItem>
                                                <asp:ListItem Text="Dependant" Value="Dependant"></asp:ListItem>
                                               
                                            </asp:Dropdownlist>
  
                                        </div>
        </div>
     
    <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Add Beneficiary" OnClick="btnAdd_Click" CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myIndvBen" UseSubmitBehavior="false" />
            </div>
        </div>
    <div class="row">
        <asp:Label ID="lblAgree" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblEncAgree" runat="server" Visible="false" Text=""></asp:Label>
        </div>
      <div class="row">
     <div class="col-xs-12 text-center" style="padding-left:200px;">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following errors and save again"
                            ShowMessageBox="false" ShowSummary="true"  DisplayMode="List"  BackColor="Snow" ForeColor="Red"
                            Font-Italic="true" ValidationGroup="myIndvBen" Font-Size="Small"/>
                    </div>
    </div>
    <br>
     <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdben" runat="server" horizontalalign="Center" AutoGenerateColumns="false" caption="Beneficiaries" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="Both">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                 <EditRowStyle BackColor="#999999" />
                                 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                 <sortedascendingcellstyle backcolor="#E9E7E2" />
                                 <sortedascendingheaderstyle backcolor="#506C8C" />
                                 <sorteddescendingcellstyle backcolor="#FFFDF8" />
                                 <sorteddescendingheaderstyle backcolor="#6F8DAE" />
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
                                        <asp:TemplateField HeaderText="ID">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Surname">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtsurnameedit" runat="server" Text='<%# Bind("Surname") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSurname" runat="server" Text='<%# Bind("Surname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtnameedit" runat="server" Text='<%# Bind("firstName") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnameedit" runat="server" Text='<%# Bind("firstName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sex">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsexedit" runat="server" Text='<%# Bind("sex") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="IDNO">
                                               
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDNOedit" runat="server" Text='<%# Bind("IDNum") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Address">
                                                <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtAddressedit" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Phone">
                                                <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtPhoneedit" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     </columns>
                        </asp:gridview>
             </div>
   
        </div>
     <div id="showSectionModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Section</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Large">
                                Section
                            </asp:label>
                        </div>
                        <div class="col-xs-6">
                            <asp:TextBox  ID="txtAddSection" runat="server" Width="400px" Font-Size="Medium"></asp:TextBox>
                           
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
    <!--Add Group Modal Starts Here-->
         <div id="showGroupModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Add Group</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">
                  
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                               Group Name
                            </asp:label>
                        </div>
                        <div class="col-xs-6"style="padding-left:22px;">
                            <asp:TextBox  ID="txtgrpname" runat="server" Width="200px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                <br>
                     <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Company
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:44px;">
                            <asp:TextBox  ID="txtcompany" runat="server" Width="200px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                     <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Contact
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:56px;">
                            <asp:TextBox  ID="txtgroupcontact" runat="server" Width="200px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                      <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                HR Rep.
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:55px;">
                            <asp:TextBox  ID="txtHRRep" runat="server" Width="200px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                     <br>
                      <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Rep. Contact
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:22px;">
                            <asp:TextBox  ID="txtRepContact" runat="server" Width="200px" Font-Size="Medium" autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
                      <br>
                      <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Bus. Address
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:21px;">
                            <asp:TextBox  ID="txtBusAddress" runat="server" Width="200px" Font-Size="Medium" autocomplete="off" TextMode="MultiLine"></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Policy Plan
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:38px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrpPlans" Font-Size="Medium" runat="server"  width="200px">
                                                 </asp:DropDownList>
                                        </div>
                        </div>
                        <br>
                    <div class="row">
                        <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Branch
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:66px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrpBranch" Font-Size="Medium" runat="server"  width="200px">
                                                 </asp:DropDownList>
                                        </div>
                        </div>
                      <br>
                      <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                Premium
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:52px;">
                            <asp:TextBox  ID="txtgrpPremium" runat="server" Width="200px" Font-Size="Medium" autocomplete="off" ></asp:TextBox>
                           
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:150px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveGrp" runat="server" Text="Add" OnClick="btnSaveGrp_Click" UseSubmitBehavior="false"/>
                        </div>
                       
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
     <a data-target="#SubmitModal" role="button" class="btn" data-toggle="modal" id="launchSubmit" style="height: 0;" data-backdrop="static"></a>
     <div id="SubmitModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        <h4 class="modal-title">Funeral Policy Successful</h4>
                    </div>
                    <div class="modal-body panel-body small">
                        <h5>You have successfully registered for funeral policy with Policy Number: <b><%= lblAgree.Text %></b>.<br />
                             You can now &nbsp;
                        <a href="PolicyAgreement.aspx?PolicyNo=<%= lblEncAgree.Text %>">Create Policy Agreement</a>.</h5>
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

