<%@ Page Language="C#" Inherits="web1.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>預設網頁</title>
</head>
<body>
	<form id="form1" runat="server">
			<input  id="abc" type="text" runat="server" />
		<asp:Button id="button1" runat="server" Text="Click me!" OnClick="button1Clicked" />
	</form>
		<br>
		<a id=abc href="http://www.pccu.edu.tw">PCCU</a>
</body>
</html>
