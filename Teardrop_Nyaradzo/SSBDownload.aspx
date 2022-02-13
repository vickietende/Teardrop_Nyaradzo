<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="SSBDownload.aspx.vb" Inherits="Teardrop_Nyaradzo.SSBDownload" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">  
    <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Download SSB File</h4>
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
         <asp:label class="col-xs-2 control-label" ID="lblgroups"  runat="server" Font-Size="Medium" Visible="false">
                                Groups
                            </asp:label>
             
        <div class="col-xs-4" style="padding-left:77px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbgrps" Font-Size="Medium" runat="server" AutoPostBack="true" width="200px" Visible="false" >
                                            
                                                
                                            </asp:DropDownList>
                                        </div>
        </div>
       </div>
    <div class="row">
            
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Select File Type
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbFileType" runat="server" AutoPostBack="true" width="400px">
                                                 <asp:ListItem Text="Select"> </asp:ListItem> 
                                                  <asp:ListItem Text="New"></asp:ListItem> 
                                                 <asp:ListItem Text="Matured" ></asp:ListItem> 
                                                   <asp:ListItem Text="Terminated"></asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Select Date"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtfileDate" runat="server" Width="200px" autocomplete="off" TextMode="Date"></asp:TextBox>
                </div>
          </div>
    <br>

     <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                           Start Date 
                                      </div>
           &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
          <div >
                    <asp:TextBox  class='input-group date' id='txtstartDate' runat="server" width="200px" TextMode="Date" CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
       
           <div class="col-xs-2 control-label" style="padding-left:20px;" >
                                             End Date
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;
          <div >
                    <asp:TextBox  class='input-group date' id="txtEnddate" runat="server" width="200px" TextMode="Date" CssClass="xdsoft_datepicker" ></asp:TextBox>
          
                </div>
         </div>
     <br>
      <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnDownload" runat="server" Text="Download File" CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myIndvCus" OnClick="btnDownload_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br><br>
</asp:Panel>
       <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>


