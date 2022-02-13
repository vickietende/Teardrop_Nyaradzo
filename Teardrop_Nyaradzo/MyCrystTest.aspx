<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MyCrystTest.aspx.vb" Inherits="Teardrop_Nyaradzo.MyCrystTest" %>

<%@ register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cr:crystalreportviewer runat="server" autodatabind="true"></cr:crystalreportviewer>
        </div>
    </form>
</body>
</html>
