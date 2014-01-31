<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Log.aspx.vb" Inherits="PenOC.Log"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Log</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link type="text/css" rel="stylesheet" href="styles.css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:Label id="Label1" runat="server">Log:</asp:Label>
				<asp:DropDownList id="cmbLog" runat="server" AutoPostBack="True"></asp:DropDownList></P>
			<asp:DataGrid id="grdLog" runat="server">
				<AlternatingItemStyle Font-Size="10pt" BackColor="#EBEBFA"></AlternatingItemStyle>
				<ItemStyle Font-Size="10pt" BackColor="Lavender"></ItemStyle>
				<HeaderStyle Font-Size="10pt" Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
