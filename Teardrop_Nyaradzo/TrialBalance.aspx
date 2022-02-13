<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="TrialBalance.aspx.vb" Inherits="Teardrop_Nyaradzo.TrialBalance" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">       
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Trial Balance</h4>
        </div>
     <br>
        <div class="row">
         <div class="col-xs-2 control-label"style="padding-left:20px;">
                                          As at
                                      </div>
           <div style="padding-left:60px;">
                    <asp:TextBox  ID="txtdateAsat" runat="server" Width="200px" autocomplete="off" TextMode="Date" ></asp:TextBox>
                </div>
          
        </div>
     <br>
          <div class="row">
         <div class="col-xs-2" style="padding-left:330px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnprint" runat="server" Text="Print Report" OnClick="btnprint_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
     <br/>
</asp:Panel>
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

