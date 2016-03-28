<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TransactionsinADONET1.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            color: #FFFFFF;
        }
        .auto-style3 {
            color: #FFFFFF;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color:orangered">
    <div>
    
        <table class="auto-style1" border="1">
            <tr>
                <td class="auto-style3">Account Number:</td>
                <td>
                    <asp:Label ID="lblAccountNumber" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAccountNumber2" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Customer Name:</td>
                <td>
                    <asp:Label ID="lblName" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblName2" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Balance:</td>
                <td>
                    <asp:Label ID="lblbalance" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBalance1" runat="server" CssClass="auto-style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnTransfer" runat="server" Text="Tranfer $10 from Account A1 to Account A2." OnClick="btnTransfer_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="auto-style3">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="auto-style3"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
