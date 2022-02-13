<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Users.aspx.vb" Inherits="Teardrop_Nyaradzo.Users" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">  
       <div class="nav nav-tabs bg-info" style="padding-left:20px;color:white;">
        <h4>Add System User</h4>
       
    </div>
    <br>
    
     <div class="row">
                     <div class="col-xs-2 control-label" style="padding-left:20px;" >
                                            Search Surname :
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtSearchUserSurname" autocomplete="off"  runat="server" Width="400px"></asp:TextBox>
                </div>&nbsp;
                <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSearchUserSurname" runat="server" Text="🔍" UseSubmitBehavior="false"/>
                </div>
        </div>
     <br>
     
      <div class="row">
           <div class="col-xs-12 center-block" style="padding-left:20px;">
                    <asp:ListBox ID="lstSurnames" runat="server" AutoPostBack="True" Visible="False" CssClass="col-xs-12" ></asp:ListBox>
                </div>
          </div>
    <br>
       <div class="row">
                           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Surname :
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtusersurname" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>&nbsp;&nbsp;&nbsp;
                     <div class="col-xs-2 control-label">
                                            Forename(s):
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtusername" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>&nbsp;&nbsp;&nbsp;
         <div class="col-xs-2 control-label">
                                             Gender :
                                      </div>
         &nbsp;&nbsp;
         <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbusergender" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            </asp:Dropdownlist>
                                 
                                        </div>

            </div>
      <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvsurname" ControlToValidate="txtusersurname"  ValidationGroup="myUser"  ErrorMessage="Surname is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
          <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtusersurname" ErrorMessage="Only alphabets are allowed" ValidationGroup="myUser"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>

         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvname" ControlToValidate="txtusername" ValidationGroup="myUser"  ErrorMessage="Name is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
             <div style="padding-left:1px;">
         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtusername" ErrorMessage="Only alphabets are allowed" ValidationGroup="myUser"  ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
              </div>
            <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvgender" ControlToValidate="cmbusergender" ValidationGroup="myUser"  ErrorMessage="Gender is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
</div>
    
       <div class="row">
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Email :
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtmail" autocomplete="off"  runat="server" Width="200px" TextMode="Email" ></asp:TextBox>
                </div>&nbsp;&nbsp;&nbsp;

           <div class="col-xs-2 control-label">
                                            Phone:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtuserphone" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>&nbsp;&nbsp;
           <div class="col-xs-2 control-label">
                                            ID Number:
                                      </div>
                    &nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtuserID" autocomplete="off"  runat="server" Width="200px" ToolTip="Format: 59000000V59"></asp:TextBox>
                </div>

          </div>
      <div class="row">
        <div style="padding-left:150px;">
            <asp:RegularExpressionValidator ID="revmail" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtmail" ErrorMessage="Invalid Email" ValidationGroup="myUser"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvuserphone" ControlToValidate="txtuserphone" ValidationGroup="myUser"  ErrorMessage="phone number is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
             <div style="padding-left:20px;">
                   <asp:RegularExpressionValidator ID="revphone" Enabled="true" ForeColor="Red"
                            Font-Italic="true" EnableClientScript="true" Font-Size="XX-Small" runat="server"  ControlToValidate="txtuserphone" ErrorMessage="Invalid Number" ValidationGroup="myUser"  ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                </div>
            <div style="padding-left:180px;">
        <asp:RequiredFieldValidator ID="rfvidno" ControlToValidate="txtuserID" ValidationGroup="myUser"  ErrorMessage="IDNO is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
          <div style="padding-left:20px;">
               <asp:RegularExpressionValidator Display="dynamic" ID="revidno" runat="server" ControlToValidate="txtuserID" ValidationGroup="myUser" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small" ValidationExpression="\d{8,9}[a-zA-Z]\d{2}" ErrorMessage="Enter a valid ID Number"></asp:RegularExpressionValidator>

              </div>
        

        </div>
   
     <div class="row">
             <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Position:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtuserdesc" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>&nbsp;&nbsp;&nbsp;
            <div class="col-xs-2 control-label">
                                            Branch:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbbranch" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                            
                                            </asp:Dropdownlist>
                                   
                                        </div>
        &nbsp;&nbsp;&nbsp;
            <div class="col-xs-2 control-label">
                                            Role:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="col-xs-4">
                                            <asp:Dropdownlist ReadOnly="True" ForeColor="Black" ID="cmbrole" runat="server" CssClass="col-xs-12" Width="200px"
                                                RepeatDirection="Horizontal">
                                                 <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                            
                                            </asp:Dropdownlist>
                                   
                                        </div>
        </div>
     <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvposition" ControlToValidate="txtuserdesc"  ValidationGroup="myUser"  ErrorMessage="Position is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvbranch" ControlToValidate="cmbbranch" ValidationGroup="myUser"  ErrorMessage="Branch is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
            <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvrole" ControlToValidate="cmbrole" ValidationGroup="myUser"  ErrorMessage="Role is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
 </div>
    
        <div class="row">
           <div class="col-xs-2 control-label" style="padding-left:20px;">
                                            Date:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtcapturedate" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                </div>
        &nbsp;&nbsp;&nbsp;&nbsp;
           <div class="col-xs-2 control-label">
                                            Password:
                                      </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtuserpassword" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>
         &nbsp;&nbsp;
           <div class="col-xs-2 control-label">
                                            Username:
                                      </div>
                    &nbsp;&nbsp;
                <div class="col-xs-12" >
                    <asp:TextBox   ID="txtuserlogin" autocomplete="off"  runat="server" Width="200px" ></asp:TextBox>
                </div>
     

        </div>
     <div class="row">
        <div style="padding-left:150px;">
        <asp:RequiredFieldValidator ID="rfvdatecapt" ControlToValidate="txtcapturedate"  ValidationGroup="myUser"  ErrorMessage="Date is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
         <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvpassword" ControlToValidate="txtuserpassword" ValidationGroup="myUser"  ErrorMessage="Password is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>
            <div style="padding-left:250px;">
        <asp:RequiredFieldValidator ID="rfvusername" ControlToValidate="txtuserlogin" ValidationGroup="myUser"  ErrorMessage="UserName is required" ForeColor="Red"
                            Font-Italic="true" Font-Size="XX-Small"  EnableClientScript="true"  runat="server"   ></asp:RequiredFieldValidator>
            </div>

        

        </div>
    <br>
        
    <div class="row">
              
                <div class="col-xs-6" style="padding-left:250px;">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnSaveUser" runat="server" Text="Save User" OnClick="btnSaveUser_Click" CausesValidation="true" ValidateRequestMode="Enabled" ValidationGroup="myUser" UseSubmitBehavior="false"/>
                </div>&nbsp;&nbsp; 
           <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnEditUser" runat="server" Text="Edit User" OnClick="btnEditUser_Click" UseSubmitBehavior="false"/>
                </div>&nbsp;&nbsp;
          <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnLockUser" runat="server" Text="Lock User" OnClick="btnLockUser_Click" UseSubmitBehavior="false"/>
                </div>
           &nbsp;&nbsp;
          <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" UseSubmitBehavior="false"/>
                </div>
          &nbsp;&nbsp;
          <div class="col-xs-6">
                    <asp:Button  CssClass="btn btn-primary btn-sm" ID="btnunlock" runat="server" Text="Unlock User" UseSubmitBehavior="false"/>
                </div>
        </div>
       <div class="row">
                <div class="col-xs-12 text-center" style="padding-left:200px;">
                        <asp:ValidationSummary ID="VSUser" runat="server" HeaderText="Please correct the following errors and save again"
                            ShowMessageBox="false" ShowSummary="true"  DisplayMode="List"  BackColor="Snow" ForeColor="Red"
                            Font-Italic="true" ValidationGroup="myUser" Font-Size="Small"/>
                    </div>
          </div>
     <br>
    <div class="nav nav-tabs bg-info">
    <h4 style="padding-left:20px;color:white;">Users</h4>
        </div>
    <br>
    <div class="row">
                                    <div class="col-xs-12" style="padding-left:20px;">
                                        <asp:GridView ID="grdUsers" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                    
                                        <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lnkBtnUpd" runat="server" CausesValidation="true"
                                        CommandName="Update" Text="Update"></asp:LinkButton>
                                   &nbsp; <asp:LinkButton ID="lnkBtnCan" runat="server" CausesValidation="true"
                                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                     <asp:LinkButton ID="lnkBtnDel" runat="server" CausesValidation="true"
                                        CommandName="Delete"  Text="Delete" ></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnEdt" runat="server" CausesValidation="true"
                                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                  
                           <asp:TemplateField HeaderText="User ID">
                                             
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("user_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Surname">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtsurnameedit" runat="server" Text='<%# Bind("laname") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsurname" runat="server" Text='<%# Bind("laname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtfnameEdit" runat="server" Text='<%# Bind("fname") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfname" runat="server" Text='<%# Bind("fname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Login Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtuser" runat="server" Text='<%# Bind("user_login") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluser" runat="server" Text='<%# Bind("user_login") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Role">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtuserrole" runat="server" Text='<%# Bind("user_role") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluserrole" runat="server" Text='<%# Bind("user_role") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtbranch1" runat="server" Text='<%# Bind("user_branch") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("user_branch") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtEmailEdit" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmtTotal" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtPhoneEditEdit" runat="server" Text='<%# Bind("phone") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblphone" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Created">
                                                    <EditItemTemplate>
                                                     <asp:TextBox CssClass="col-xs-12 form-control input-sm text-uppercase" ID="txtdateEdit" runat="server" Text='<%# Bind("date_created") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%# Bind("date_created") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="rowstyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
    <br><br>
        </asp:Panel>
        <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

