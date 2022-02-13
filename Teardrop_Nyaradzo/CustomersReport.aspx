<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="CustomersReport.aspx.vb" Inherits="Teardrop_Nyaradzo.CustomersReport" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">      
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;" >Customers Report</h4>
      </div>
    <br>
    <div class="row">
          <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Customer Type 
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4"style="padding-left:5px;">
                                <asp:RadioButtonList ID="rdbType" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true"  CausesValidation="true">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="SSB" Value="SSB"></asp:ListItem>
                                     <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
        <div class="col-xs-2 control-label" style="padding-left:30px;">
                                            Status
                                      </div>
              <asp:Label ID="Label1" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4"style="padding-left:5px;">
                                <asp:RadioButtonList ID="rdbStatus" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true"  CausesValidation="true">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
         <div class="col-xs-2 control-label" style="padding-left:30px;" >
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
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
            </div>
        </div>
  </asp:Panel>  
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

