<%@ Page Language="C#" Inherits="web01.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default-中文會是亂碼</title>
</head>
<body>
		<h1>This is a webform created by HYJ</h1>
		<ul>
         @for (int i = 0; i < 5; i++) {
            <li>List item @i</li>
         }
        </ul>
	<form id="form1" runat="server">
		<asp:Button id="button1" runat="server" Text="Click me!" OnClick="button1Clicked" />
	</form>
</body>
</html>
