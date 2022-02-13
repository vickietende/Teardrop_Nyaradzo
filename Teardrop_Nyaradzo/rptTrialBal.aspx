﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rptTrialBal.aspx.vb" Inherits="Teardrop_Nyaradzo.rptTrialBal" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trial Balance</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False"
                EnableParameterPrompt="False" ReuseParameterValuesOnRefresh="True"/>
        </div>
    </form>
</body>
</html>
