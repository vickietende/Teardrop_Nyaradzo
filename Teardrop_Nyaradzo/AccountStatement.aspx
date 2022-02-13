<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="AccountStatement.aspx.vb" Inherits="Teardrop_Nyaradzo.AccountStatement" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">      
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Account Statement</h4>
        </div>
     <br>


    <div class="row">
         <div class="col-xs-2 control-label"style="padding-left:20px;">
                                          Search Account 
                                      </div>
         <div class="col-xs-12" style="padding-left:20px;" >
                    <asp:TextBox   ID="txtSearchAccount" autocomplete="off"   runat="server" Width="400px" ></asp:TextBox>
                       
                </div>
          <div class="col-xs-2" style="padding-left:20px;">
    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" ID="btnsearchAcc" Text="🔍" OnClick="btnsearchAcc_Click"></asp:Button>
            </div>
        
    </div>
    <br>
    <div class="row">
          <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstAccounts" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" OnSelectedIndexChanged="lstAccounts_SelectedIndexChanged"></asp:ListBox>
                </div>
        </div>
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
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnprint" runat="server" Text="Print Statement" Onclick="btnprint_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
     <br/> <br/>
</asp:Panel>
        <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

