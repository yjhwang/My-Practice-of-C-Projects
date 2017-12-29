<%@ Page Language="C#" Inherits="webform1.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:Button id="button1" runat="server" Text="Click me!" OnClick="button1Clicked" />
        <asp:Button id="button2" runat="server" Text="Click B2!" OnClick="button1Clicked" />

	</form>
   <p id="demo"></p>
<script>
var i = 10;
while (i > 0 ) {
    document.getElementById("demo").innerHTML += i + "<br>";
    i--;
}
</script>
</body>
</html>
