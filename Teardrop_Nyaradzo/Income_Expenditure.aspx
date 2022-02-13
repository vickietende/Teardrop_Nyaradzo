<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Income_Expenditure.aspx.vb" Inherits="Teardrop_Nyaradzo.Income_Expenditure" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">    
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Income and Expenditure</h4>
        </div>
     <br>
        <div class="row">
         <div class="col-xs-2 control-label"style="padding-left:20px;">
                                          Date From
                                      </div>
           <div style="padding-left:60px;">
                    <asp:TextBox  ID="txtdatefrom" runat="server" Width="200px" autocomplete="off" TextMode="Date" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label"style="padding-left:20px;">
                                          Date To
                                      </div>
           <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtdateto" runat="server" Width="200px" autocomplete="off" TextMode="Date" ></asp:TextBox>
                </div>
        </div>
     <br>
          <div class="row">
         <div class="col-xs-2" style="padding-left:330px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnprint" runat="server" Text="Print Report" Onclick="btnprint_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
       <br/>
       </asp:Panel>
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>
