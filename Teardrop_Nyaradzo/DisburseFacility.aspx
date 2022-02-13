<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="DisburseFacility.aspx.vb" Inherits="Teardrop_Nyaradzo.DisburseFacility" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <asp:MultiView ID="MyMainPage" runat="server">

        <asp:View ID="Funeralinfo" runat="server">
            <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
            <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Funeral Information</h4>
      </div>
    <br>
             
            <div class="row">
     
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                  <asp:Label ID="Label5" runat="server" Text="Search Customer"   ></asp:Label>          
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchSurname" autocomplete="off"  runat="server" Width="400px" ></asp:TextBox>
                       
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchSurname" runat="server" Text="🔍" OnClick="btnSearchSurname_Click" />
                </div>
                </div>
                     
     <br>
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" OnSelectedIndexChanged="lstSurnames_SelectedIndexChanged"></asp:ListBox>
                </div>
          </div>
            <br>
            <div class="row">
                         
            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Policy Plan
                                      </div>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
        <div class="col-xs-4" style="padding-left:25px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbProduct" runat="server" AutoPostBack="true" width="200px" >
                                      
                                                
                                            </asp:DropDownList>
                                        </div>
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             PolicyNo 
                                      </div>
        &nbsp;&nbsp; 
        <div style="padding-left:20px;">
                    <asp:TextBox  ID="txtPolicyNo" runat="server" width="200px" autocomplete="off" Enabled="false"></asp:TextBox>
                </div>
             <div class="col-xs-4" style="padding-left:20px;">
                             <asp:CheckBox ID="chkIsDeceased" runat="server" OnCheckedChanged="chkIsDeceased_CheckedChanged"/>
                            </div>
                 <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Is Deceased?
                                      </div>
          </div>

            <br>
                      
    <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label2" runat="server" Text="Surname"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div >
                    <asp:TextBox  ID="txtsurname" runat="server" Width="200px" autocomplete="off" onkeypress="return isTextOnly(evt)"></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label">
                                             First Name(s) 
                                      </div>
         &nbsp;&nbsp;
           <div >
                    <asp:TextBox  ID="txtname" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             ID Number 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <div >
                    <asp:TextBox  ID="txtIDNO" runat="server" autocomplete="off" width="200px" data-toggle="tooltip" data-placement="top" ToolTip="Valid format: 012345678A90" ></asp:TextBox>
                </div>

        </div>
            <br>
                <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Description 
                                      </div>
       
        <div style="padding-left:47px;">
                    <asp:TextBox  ID="txtdesc" runat="server" autocomplete="off" TextMode="MultiLine" width="200px" ></asp:TextBox>
                </div>
                       
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Location
                                      </div>
       
        <div style="padding-left:30px;">
                    <asp:TextBox  ID="txtlocation" runat="server" autocomplete="off"  width="200px" ></asp:TextBox>
                </div>
                    <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Date Deceased
                                      </div>
       
        <div style="padding-left:22px;">
                    <asp:TextBox  ID="txtdatedeceased" runat="server" autocomplete="off"  width="200px" ></asp:TextBox>
                </div>
                    </div>
            <br>
              <div class="row">
                            <div class="col-xs-2 control-label" style="padding-left:20px;">
                                <asp:Label ID="lblfullName" runat="server" Text="Full Name" ></asp:Label>
                                      </div>
       
        <div style="padding-left:57px;">
                    <asp:TextBox  ID="txtfullname" runat="server" autocomplete="off"  width="200px" ></asp:TextBox>
                </div>
                  </div>
            <br>
            <div class="row">
                   <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnSaveDeceased" runat="server" Text="Save Deceased Info" OnClick="btnSaveDeceased_Click" UseSubmitBehavior="false" />
            </div>
                </div>
             <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;"> 
        Beneficiaries
    </h4>
        </div>
            <br>
            
     <div class="row">
         <div class="col-xs-12 center-block" style="padding-left:20px;">
                             <asp:gridview id="grdben" runat="server" horizontalalign="Center" AutoGenerateColumns="False" caption="Beneficiaries" captionalign="Top" emptydatatext="No Beneficiaries" Visible="False" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                 <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle CssClass="altrowstyle" BackColor="#F7F7DE" />
                                 <columns>
                                           <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkBtnUpd" runat="server" CausesValidation="true"
                                        CommandName="Update" Text="Mark as Deceased"></asp:LinkButton>
                                   &nbsp; <asp:LinkButton ID="lnkBtnCan" runat="server" CausesValidation="true"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                   
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
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSurname" runat="server" Text='<%# Bind("Surname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Name">
                                                   
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
                                              
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Phone">
                                            
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="isDeceased?">
                                                <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtisDeceasededit" runat="server" Text='<%# Bind("isDeceased") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldeceased" runat="server" Text='<%# Bind("isDeceased") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                     </columns>
                                 <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                 <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                 <SortedAscendingHeaderStyle BackColor="#848384" />
                                 <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                 <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:gridview>
             </div>
   
        </div>

                 <div class="row">
                                       
                                        <div class="col-xs-2 text-right"style="padding-left:900px;">
                                            <asp:LinkButton ID="lnkNext" class="mdi mdi-arrow-right-bold-circle icon-lg mr-3 text-primary " OnClick="lnkNext_Click"  runat="server"></asp:LinkButton>
                                        </div>
                                      
                                    </div>
                </asp:Panel>
 
        </asp:View>
        <asp:View ID="PolicyFacility" runat="server">
            <asp:Panel ID="pnlProvideFacility" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
 <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Provide Policy Facility</h4>
      </div>
    <br>
   <div class="row">
     
                  
       
                              <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Option
                                      </div>
              <asp:Label ID="Label40" runat="server" Text="*"  ForeColor="Red" Font-Size="Small" ></asp:Label>
         &nbsp;&nbsp; &nbsp;&nbsp; 
          <div class="col-xs-4">
                                <asp:RadioButtonList ID="rdbOption" runat="server" Class="form-check-{color}" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbOption_SelectedIndexChanged" >
                                    <asp:ListItem Text="Issue Sum Assured" ></asp:ListItem>
                                    <asp:ListItem Text="Provide Facilty" ></asp:ListItem>
                                    
                                </asp:RadioButtonList>
                            </div>
</div>

   

    <br>
   
    <div class="row">
          
                   <div class="col-xs-2 control-label" style="padding-left:20px;">
                                        <asp:Label ID="Label3" runat="server" Text="Date"  ></asp:Label> 
                                      </div>
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 
           <div style="padding-left:30px;">
                    <asp:TextBox  ID="txtdate" runat="server" Width="200px" autocomplete="off" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
                   <div class="col-xs-2 control-label">
                                         Sum Assured     
                                      </div>
         &nbsp;&nbsp;
           <div style="padding-left:5px;">
                    <asp:TextBox  ID="txtSumAssured" runat="server" autocomplete="off" width="200px" ></asp:TextBox>
                </div>
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Account 
                                      </div>
       
         &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
       <div class="col-xs-4" style="padding-left:15px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbAccount" runat="server" AutoPostBack="true" width="200px" >
                                               
                                                
                                            </asp:DropDownList>
                                        </div>

        </div>
    <br>
     <div class="row">
                     <div class="col-xs-2 control-label" style="padding-left:20px;">
                                <asp:Label ID="Label4" runat="server" Text="Grocery" ></asp:Label>
                                      </div>
         &nbsp;&nbsp;
           <div style="padding-left:63px;" >
                    <asp:TextBox  ID="txtGrocryAmt" runat="server" autocomplete="off" width="200px"  ></asp:TextBox>
                </div>
                            
         
         <div class="col-xs-2 control-label" style="padding-left:7px;">
                                <asp:Label ID="lblfuel" runat="server" Text="Fuel Amount" Visible="false"></asp:Label>
                                      </div>
         
           <div style="padding-left:20px;" >
                    <asp:TextBox  ID="txtfuelamnt" runat="server" autocomplete="off" width="200px" Visible="false" ></asp:TextBox>
                </div>
             

                      <div class="col-xs-2 control-label" style="padding-left:5px;">
                                <asp:Label ID="lblother" runat="server" Text="Other Expenses" Visible="false"></asp:Label>
                                      </div>
         &nbsp;&nbsp;
           <div style="padding-left:22px;" >
                    <asp:TextBox  ID="txtotherExp" runat="server" autocomplete="off" width="200px" Visible="false"></asp:TextBox>
                </div>
         </div>
    <br>
    <div class="row">
         <div class="col-xs-2 control-label" style="padding-left:20px;">
                                             Payment Method
                                      </div>
          
        <div class="col-xs-4" style="padding-left:5px;">
                                            <asp:DropDownList ReadOnly="True" ForeColor="Black"  ID="cmbPayMethod" runat="server" AutoPostBack="true" width="200px" >
                                                 <asp:ListItem Text="Select" Value="-1" ></asp:ListItem> 
                                                <asp:ListItem Text="Cash" Value="Cash" ></asp:ListItem>
                                                <asp:ListItem Text="Ecocash" Value="Ecocash" ></asp:ListItem>
                                                 <asp:ListItem Text="OneMoney" Value="OneMoney" ></asp:ListItem>
                                                 <asp:ListItem Text="Telecash" Value="Telecash" ></asp:ListItem> 
                                                 <asp:ListItem Text="Swipe" Value="Swipe" ></asp:ListItem> 
                                            </asp:DropDownList>
                                        </div>
        </div>
      <br>
        <div class="row">
               <div class="col-xs-2 text-right"style="padding-left:20px;">
                                            <asp:LinkButton ID="LnkPrev" class="mdi mdi-arrow-left-bold-circle icon-lg mr-3 text-primary" OnClick="LnkPrev_Click"  runat="server"></asp:LinkButton>
                                        </div>
         <div class="col-xs-2" style="padding-left:400px;">
        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnAdd" runat="server" Text="Process" OnClick="btnAdd_Click" UseSubmitBehavior="false"/>
            </div>
        </div>
    <br>
    
   
    </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
     <ajax:RoundedCornersExtender ID="RoundedCornersExtender1"
        runat="server" Enabled="True" TargetControlID="pnlProvideFacility" Radius="15">
    </ajax:RoundedCornersExtender>


</asp:Content>

