<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BL.DefPage"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">

    <title>Хостинг .Net сборок</title>

    <meta name="keywords" content=".Net, JavaScript, Assembly, Hosting">
    <meta name="description" content="Сервис кросдоменного доступа к .Net сборкам из Javascript">


    <meta http-equiv="content-language" content="ru">
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
    <meta name="author" Content="BCWD">
    <meta name="copyright" Content="BCWD"> 

    
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
        <a href="../">2013 BCWD ©</a>
    </div>

    </div>
    </form>
</body>
</html>
