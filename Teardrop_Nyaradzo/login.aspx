<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Teardrop_Nyaradzo.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <!-- Required meta tags -->
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
  <title>Teardrop.</title>
  <!-- plugins:css -->
  <%--<link rel="stylesheet" href="../../vendors/ti-icons/css/themify-icons.css"/>--%>
    <link rel="stylesheet" href="vendors/ti-icons/css/themify-icons.css"/>
<%--  <link rel="stylesheet" href="../../vendors/base/vendor.bundle.base.css"/>--%>
      <link rel="stylesheet" href="vendors/base/vendor.bundle.base.css"/>
  <!-- endinject -->
  <!-- plugin css for this page -->
  <!-- End plugin css for this page -->
  <!-- inject:css -->
<%--  <link rel="stylesheet" href="../../css/style.css"/>--%>
      <link rel="stylesheet" href="css/style.css"/>
  <!-- endinject -->
 <link rel="shortcut icon" href="images/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <div class="container-scroller">
    <div class="container-fluid page-body-wrapper full-page-wrapper">
      <div class="content-wrapper d-flex align-items-center auth px-0">
        <div class="row w-100 mx-0">
          <div class="col-lg-4 mx-auto">
            <div class="auth-form-light text-left py-5 px-4 px-sm-5">
              <div class="brand-logo">
                <img src="images/TeardropLogo.png" alt="logo"/>
              </div>
              <h5>Teardrop a product of CodeDimensions</h5>
              <h6 class="font-weight-light">Login to Continue</h6>
         <%--     <form class="pt-3">--%>
                <div class="form-group">
                  <input type="text" runat="server" class="form-control form-control-lg" id="txtusername" placeholder="Username" autocomplete="off"/>
                </div>
                <div class="form-group">
                  <input type="password" runat="server" class="form-control form-control-lg" id="txtpassword" placeholder="Password"/>
                </div>
                <div class="mt-3">
                  <asp:Button runat="server" ID="btn_Login" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" Text="Login" OnClick="btn_Login_Click" ></asp:Button>
                </div>
                <div class="my-2 d-flex justify-content-between align-items-center">
                 
            <%--      <a href="#" class="auth-link text-black">Forgot password?</a>--%>
                      <asp:LinkButton runat="server" Text="Forgot password?" ID="lnkbtnpassword" OnClick="lnkbtnpassword_Click"></asp:LinkButton>
                </div>
                   <div class="d-sm-flex justify-content-center justify-content-sm-between">
            <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">Copyright © 2020 <a href="#" target="_blank">CodeDimensions Pvt(Ltd)</a>        All rights reserved.</span>
            <span class="float-none float-sm-right d-block mt-1 mt-sm-0 text-center"><i class="mdi mdi-thumb-up text-danger"></i></span>
          </div>
              
            <%--  </form>--%>
            </div>
          </div>
        </div>
      </div>
      <!-- content-wrapper ends -->
      
    </div>
    <!-- page-body-wrapper ends -->
  </div>
  <!-- container-scroller -->
  <!-- plugins:js -->
  <script src="vendors/base/vendor.bundle.base.js"></script>
  <!-- endinject -->
  <!-- inject:js -->
  <script src="js/off-canvas.js"></script>
  <script src="js/hoverable-collapse.js"></script>
  <script src="js/template.js"></script>
  <script src="js/todolist.js"></script>
  <!-- endinject -->
        </div>
    </form>
</body>
</html>
