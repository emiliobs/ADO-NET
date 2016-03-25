<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Disconnecteddataaccessinaspinnet.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial">
        <asp:Button ID="btnGetDataFromDB" runat="server" Text="Get Data From DB" OnClick="btnGetDataFromDB_Click1" />

        <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCancelingEdit="gvStudents_RowCancelingEdit" OnRowDeleting="gvStudents_RowDeleting" OnRowEditing="gvStudents_RowEditing" OnRowUpdated="gvStudents_RowUpdated" OnRowUpdating="gvStudents_RowUpdating">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                <asp:BoundField DataField="TotalMarks" HeaderText="TotalMarks" SortExpression="TotalMarks" />
            </Columns>
        </asp:GridView>    

        <asp:Button ID="btnUpdateDB" runat="server" Text="Update" OnClick="btnUpdateDB_Click"/>
       
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
