<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WhatisSqlCommandBuilder.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial">
    
        <table border="1">
            <tr>
                <td>Student ID:</td>
                <td>
                    <asp:TextBox ID="TextBoxStudnetID" runat="server" Height="26px" Width="265px"></asp:TextBox>
&nbsp;&nbsp;
                    <asp:Button ID="ButtonLoad" runat="server" Text="Load" OnClick="ButtonLoad_Click" />
                </td>
            </tr>
            <tr>
                <td>Name:</td>
                <td>
                    <asp:TextBox ID="TextBoxName" runat="server" Height="29px" Width="264px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Gender:</td>
                <td>
                    <asp:DropDownList ID="DropDownListGender" runat="server" Height="25px" Width="272px">
                        <asp:ListItem Text="Select Gender" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Total Marks:</td>
                <td>
                    <asp:TextBox ID="TextBoxTotalMark" runat="server" Height="24px" Width="261px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="LabelStatus" runat="server" ></asp:Label>
                </td>
            </tr>
        </table>
        <br /><br />
        
        <table>
            <tr>
                <td>
                         <b>Insert</b>
                </td>
                <td>
                    <asp:Label id="lblInsert" runat="server" />
                </td>
            </tr>
             <tr>
                <td>
                    <b>Update</b>
                </td>
                <td>
                     <asp:Label id="lblUpdate" runat="server" />
                </td>
            </tr>
             <tr>
                <td>
                    <b>Delete</b>
                </td>
                <td>
                     <asp:Label id="lblDelete" runat="server" />
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
