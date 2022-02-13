<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="SSBUpload.aspx.vb" Inherits="Teardrop_Nyaradzo.SSBUpload" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">     
    <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Upload SSB File</h4>
        </div>
    <br>
    <div class="container">
        <div class="row">
          <div class="col-xs-2 control-label">
                                            Customer Type 
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp;  
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbType" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbType_SelectedIndexChanged" CausesValidation="true">
                                    <asp:ListItem Text="SSB" Value="SSB"></asp:ListItem>
                                <asp:ListItem Text="Group" Value="Group"></asp:ListItem>
                                    </asp:RadioButtonList>
                  <asp:RequiredFieldValidator ID="rfvtype" ControlToValidate="rdbType" ValidationGroup="myIndvCus"  ErrorMessage="Description is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
                            </div>
         <asp:label class="col-xs-2 control-label" ID="lblgroups"  runat="server" Font-Size="Medium" Visible="false"  style="padding-left:40px;">
                                Groups
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:20px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrps" Font-Size="Medium" runat="server" AutoPostBack="true" width="200px" Visible="false" >
                                            
                                                
                                            </asp:DropDownList>
                                        </div>
        </div>
    </div>
    <br>
    <div class="row">
            
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Select File to Upload
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                                       
                                        </div>
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Payment Date"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtpaymentDate" runat="server" Width="200px" autocomplete="off" AutoPostBack="True" TextMode="Date" OnTextChanged="txtpaymentDate_TextChanged"></asp:TextBox>
                </div>
          </div>
    <br>

     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                           Account
                                      </div>
          
         <div class="col-xs-4" style="padding-left:110px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbAccount" runat="server" CssClass="col-xs-12" Width="250px"
                                                RepeatDirection="Horizontal" >
                                             
                                               
                                            </asp:Dropdownlist>
             </div>
              <div class="col-xs-2 control-label" style="padding-left:170px;">
                                        <asp:Label ID="Label1" runat="server" Text="Ref.No"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div style="padding-left:50px;" >
                    <asp:TextBox  ID="txtRefNo" runat="server" Width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
  
                                        

       
         </div>
    <br>
       <div class="row">
        <div class="col-xs-2" style="padding-left:20px;color:blueviolet;">
       <a data-target="#showProcessedModal" role="button" class="" data-toggle="modal" id="launchProcessedModal">Processed/Unprocessed SSB Report</a>
            </div>
            <div class="col-xs-2" style="padding-left:328px;color:blueviolet;">
       <a data-target="#showGroupModal" role="button" class="" data-toggle="modal" id="launchGroupModal">Processed/Unprocessed Group Report</a>
            </div>
           </div>
     <br>
      <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnUpload" runat="server" Text="Process File" Onclick="btnUpload_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br><br>
       <div id="showProcessedModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Processed/Unprocessed SSB</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">
                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                File Name
                            </asp:label>
                        </div>
                      <div class="col-xs-4" style="padding-left:10px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbFiles" runat="server" Font-Size="Medium" CssClass="col-xs-12" Width="300px"
                                                RepeatDirection="Horizontal" >
                                           </asp:Dropdownlist>
  
                                        </div>
                    </div>

                           <br>
                        <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                               Report
                            </asp:label>
                        </div>
                      <div class="col-xs-4" style="padding-left:35px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbReportType" runat="server" Font-Size="Medium" CssClass="col-xs-12" Width="300px"
                                                RepeatDirection="Horizontal" >
                                                <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                     <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Processed" Value="Processed"></asp:ListItem>
                                                 <asp:ListItem Text="Unprocessed" Value="Unprocessed"></asp:ListItem>
                                           </asp:Dropdownlist>
  
                                        </div>
                    </div>
                      
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btnProcessed" runat="server" Text="Print Report" OnClick="btnProcessed_Click" UseSubmitBehavior="false"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

           <div id="showGroupModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
              
                <div class="modal-header">
                      <div>
                         <h4 class="modal-title">Processed/Unprocessed SSB</h4>
                    </div>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
               
                </div>
                <div class="modal-body panel-body small">

                    <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                                File Name
                            </asp:label>
                        </div>
                      <div class="col-xs-4" style="padding-left:10px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbGrpFiles" runat="server" Font-Size="Medium" CssClass="col-xs-12" Width="300px"
                                                RepeatDirection="Horizontal" >
                                           </asp:Dropdownlist>
  
                                        </div>
                    </div>

                           <br>
                        <div class="row">
                        <div class="col-xs-3 control-label">
                            <asp:label class="col-xs-2 control-label"  runat="server" Font-Size="Medium">
                               Report
                            </asp:label>
                        </div>
                      <div class="col-xs-4" style="padding-left:35px;">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbgrpType" runat="server" Font-Size="Medium" CssClass="col-xs-12" Width="300px"
                                                RepeatDirection="Horizontal" >
                                                <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                     <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Processed" Value="Processed"></asp:ListItem>
                                                 <asp:ListItem Text="Unprocessed" Value="Unprocessed"></asp:ListItem>
                                           </asp:Dropdownlist>
  
                                        </div>
                    </div>
                      
                <br>
                    <div class="row">
                        <div class="col-xs-12 text-center" style="padding-left:200px;">
                            <asp:Button CssClass="btn btn-primary btn-sm" ID="btngrpprint" runat="server" Text="Print Report" OnClick="btngrpprint_Click"/>
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

