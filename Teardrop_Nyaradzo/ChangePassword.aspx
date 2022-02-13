<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="Teardrop_Nyaradzo.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Change Password</h4>
        </div>
    <br>
    <div class="row">
                <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Password"  ></asp:Label> 
                                      </div>
       
           <div style="padding-left:62px;">
                    <asp:TextBox  ID="txtpassword" runat="server" Width="200px" ToolTip="Enter Old password" autocomplete="off" AutoPostBack="true"  OnTextChanged="txtpassword_TextChanged"></asp:TextBox>
                </div>
        </div>
    <br>
     <div class="row">
                <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label1" runat="server" Text="New Password"  ></asp:Label> 
                                      </div>
        
           <div style="padding-left:27px;">
                    <asp:TextBox  ID="txtNewpassword" runat="server" ToolTip="Provide a secure unique password" AutoPostBack="true" Width="200px" autocomplete="off" OnTextChanged="txtNewpassword_TextChanged"></asp:TextBox>
                </div>
        </div>
    <br>
       <div class="row">
                <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label3" runat="server" Text="Confirm Password" ></asp:Label> 
                                      </div>
        
           <div style="padding-left:2px;">
                    <asp:TextBox  ID="txtConfirm" runat="server" Width="200px" ToolTip="Re-enter New Password" autocomplete="off" AutoPostBack="true"  OnTextChanged="txtConfirm_TextChanged"></asp:TextBox>
                </div>
        </div>
    <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:150px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnchange" runat="server"  Text="Change Password" OnClick="btnchange_Click"/>
            </div>
            </div>
</asp:Content>

