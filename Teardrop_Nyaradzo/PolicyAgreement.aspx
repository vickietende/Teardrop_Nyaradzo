<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="PolicyAgreement.aspx.vb" Inherits="Teardrop_Nyaradzo.PolicyAgreement" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
     <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Policy Agreement</h4>
        </div>
    <br>
    <div class="row">
              <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label3" runat="server" Text="Search Customer"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchSurname" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍" OnClick="btnSearchSurname_Click" UseSubmitBehavior="false"/>
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
                                        <asp:Label ID="Label2" runat="server" Text="Policy No."  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtNewPolicyNo" runat="server" Width="200px" autocomplete="off" Eaabled="false"></asp:TextBox>
                </div>
        <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Policy Plan
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                            <div class="col-xs-4">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px">
                                               </asp:DropDownList>
                                        </div>
        </div>
    <br>

       <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label1" runat="server" Text="Surname"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div style="padding-left:8px;">
                    <asp:TextBox  ID="txtsurname" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label" style="padding-left:3px;">
                                             First Name(s) 
                                      </div>
         &nbsp;&nbsp;
           <div style="padding-left:8px;">
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
    <br>
    <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             ID Number 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <div style="padding-left:8px;">
                    <asp:TextBox  ID="txtIDNO" runat="server" autocomplete="off" width="200px" data-toggle="tooltip" data-placement="top" ToolTip="Valid format: 012345678A90" Enabled="false"></asp:TextBox>
                </div>
             <div class="col-xs-2 control-label"style="padding-left:10px;">
                                             EC Number 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:25px;" >
                    <asp:TextBox  ID="txtecNum" runat="server" width="200px" autocomplete="off" ></asp:TextBox>
                </div>

        </div>
        <br>
        <div class="row">
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSave" runat="server"  Text="View Agreement"  OnClick="btnSave_Click" UseSubmitBehavior="false"/>
            </div>
        
      
        </div>
    <br>
     <div class="row">
         <div style="color:blueviolet;padding-left:400px;">
             <asp:LinkButton ID="lnkViewAmortization" runat="server" OnClick="lnkViewAmortization_Click">View Payment Schedule</asp:LinkButton>
             </div>
         </div>
    </asp:Panel>  
      <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

