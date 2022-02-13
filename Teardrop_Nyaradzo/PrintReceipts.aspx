<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="PrintReceipts.aspx.vb" Inherits="Teardrop_Nyaradzo.PrintReceipts" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
      <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Individual Premium Payment Receipts</h4>

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
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍"   UseSubmitBehavior="false" OnClick="btnSearchSurname_Click"/>
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
                        <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                TrxnID
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:23px;">
                            <asp:TextBox  ID="txtTrxnID" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
        <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Policy No.
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:5px;">
                            <asp:TextBox  ID="txtPolicyNo" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
                    </div>
    <br>
    <div class="row">
                        <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                TrxnDate
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:5px;">
                            <asp:TextBox  ID="txtTrxnDate" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
          <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Amount Paid
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:5px;">
                            <asp:TextBox  ID="txtAmntPaid" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>
         <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Payment Method
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:5px;">
                            <asp:TextBox  ID="txtpaymentmethod" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>


                    </div>
    <br>
       <div class="row">
                       
          <div class="col-xs-3 control-label" style="padding-left:20px;">
                            <asp:label class="col-xs-2 control-label"  runat="server" >
                                Created
                            </asp:label>
                        </div>
                        <div class="col-xs-6" style="padding-left:15px;">
                            <asp:TextBox  ID="txtcreatedby" runat="server" Width="200px"  autocomplete="off"></asp:TextBox>
                           
                        </div>

                    </div>
      <br>
       <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server" Text="Print Receipt"  UseSubmitBehavior="false" OnClick="btnSave_Click"/>
            </div>
        </div>
    <br>
</asp:Panel>
       <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

