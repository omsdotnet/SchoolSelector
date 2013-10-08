<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BL.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <strong>Перечень сервисов</strong><br />
    <hr />
    <br />


    <asp:Repeater ID="Links" runat="server">
       <ItemTemplate>
           <a id="A1" runat="server" href='<%# Container.DataItem %>'><%# Container.DataItem %></a><br />
       </ItemTemplate>
    </asp:Repeater>

    <br />
    <hr />
    <div class="style1">
        <br />
        <a href="../">2013 ПК©</a>
    </div>

    </div>
    </form>
</body>
</html>
