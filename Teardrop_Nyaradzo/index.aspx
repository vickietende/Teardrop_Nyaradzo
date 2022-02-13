<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="index.aspx.vb" Inherits="Teardrop_Nyaradzo.index" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajax"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:Panel ID="pnlContent" runat="server" BorderColor="SkyBlue" BorderWidth="1px" CssClass="alert-dark">
    
      
    <div  class="nav nav-tabs alert-success">
       <h4>Dashboard</h4>
          </div>
              <div class="row">
            <div class="col-md-12 grid-margin stretch-card">
              <div class="card">
                <div class="card-body dashboard-tabs p-0">
                  <ul class="nav nav-tabs px-4" role="tablist">
                    <li class="nav-item">
                      <a class="nav-link active" id="overview-tab" data-toggle="tab" href="#overview" role="tab" aria-controls="overview" aria-selected="true">Overview</a>
                    </li>
                    <li class="nav-item">
                      <a class="nav-link" id="sales-tab" data-toggle="tab" href="#sales" role="tab" aria-controls="sales" aria-selected="false">Premiums</a>
                    </li>
                    <li class="nav-item">
                      <a class="nav-link" id="purchases-tab" data-toggle="tab" href="#purchases" role="tab" aria-controls="purchases" aria-selected="false">Product Distribution</a>
                    </li>
                  </ul>
                  <div class="tab-content py-0 px-0">
                    <div class="tab-pane fade show active" id="overview" role="tabpanel" aria-labelledby="overview-tab">
                      <div class="d-flex flex-wrap justify-content-xl-between">
                        <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-folder-account icon-lg mr-3 text-primary"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">SSB Clients</small>
                            <div>
                      <asp:Label runat="server" ID="lblssb" Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                            </div>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-currency-usd mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Revenue</small>
                              <asp:Label runat="server" ID="lblRevenue" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-square-inc-cash mr-3 icon-lg text-success"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Assurance Paid</small>
                           <asp:Label runat="server" ID="lbltotalassured" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-cart-outline mr-3 icon-lg text-warning"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Products on Offer</small>
                             <asp:Label runat="server" ID="lblproducts" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-flag mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Matured</small>
                           <asp:Label runat="server" ID="lblmatured" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="tab-pane fade" id="sales" role="tabpanel" aria-labelledby="sales-tab">
                      <div class="d-flex flex-wrap justify-content-xl-between">
                        <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-cash-100 icon-lg mr-3 text-primary"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Cash</small>
                            <asp:Label runat="server" ID="lblcash" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-cellphone-android mr-3 icon-lg text-warning"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Ecocash</small>
                           <asp:Label runat="server" ID="lblecocash" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-cellphone-iphone mr-3 icon-lg text-success"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">OneMoney</small>
                          <asp:Label runat="server" ID="lblOneMoney" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-cellphone mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Telecash</small>
                         <asp:Label runat="server" ID="lbltelecash" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-credit-card mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Swipe</small>
                               <asp:Label runat="server" ID="lblswipe" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="tab-pane fade" id="purchases" role="tabpanel" aria-labelledby="purchases-tab">
                      <div class="d-flex flex-wrap justify-content-xl-between">
                        <div class="d-none d-xl-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-star-outline icon-lg mr-3 text-primary"></i>
                          <div class="d-flex flex-column justify-content-around">
                              <small class="mb-1 text-muted">Silver + Bus Plan</small>
                            <asp:Label runat="server" ID="lblSilver" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-seal mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                            <small class="mb-1 text-muted">Gold + Bus Plan</small>
                                <asp:Label runat="server" ID="lblplan2" CssClass="mb-1 text-muted"></asp:Label>
                               <asp:Label runat="server" ID="lblGold" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-diamond mr-3 icon-lg text-success"></i>
                          <div class="d-flex flex-column justify-content-around">
                                 <small class="mb-1 text-muted">Diamond + Bus Plan</small>
                             <asp:Label runat="server" ID="lblDiamond" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-shield-outline mr-3 icon-lg text-warning"></i>
                          <div class="d-flex flex-column justify-content-around">
                                 <small class="mb-1 text-muted">Platinum Plan</small>
                               <asp:Label runat="server" ID="lblPlatinum" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                        <div class="d-flex py-3 border-md-right flex-grow-1 align-items-center justify-content-center p-3 item">
                          <i class="mdi mdi-shield mr-3 icon-lg text-danger"></i>
                          <div class="d-flex flex-column justify-content-around">
                                <small class="mb-1 text-muted">Platinum + Bus Plan</small>
                               <asp:Label runat="server" ID="lblPlatinum1" Font-Bold="true" ></asp:Label>
                          </div>
                        </div>
                      
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

              <div class="row">
            <div class="col-md-12 stretch-card">
              <div class="card">
                <div class="card-body">
                  <p class="card-title">Recent Registrations</p>
                  <div class="table-responsive">
                              <asp:gridview id="grdClients" runat="server" horizontalalign="Center"  caption="Top 10 New Clients" captionalign="Top" emptydatatext="No Clients" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle CssClass="altrowstyle" BackColor="White" />
                                  <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="headerstyle" HorizontalAlign="center" BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                  <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle CssClass="altrowstyle" BackColor="#FFFBD6" ForeColor="#333333" />
                                 <columns>
                              
                                     </columns>
                                  <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                  <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                  <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                  <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                  <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:gridview>
                  </div>
                </div>
              </div>
            </div>
          </div>
</asp:Panel>
   <ajax:RoundedCornersExtender ID="Panel1_RoundedCornersExtender"
        runat="server" Enabled="True" TargetControlID="pnlContent" Radius="15">
    </ajax:RoundedCornersExtender>
</asp:Content>

