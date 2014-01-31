<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Login.aspx.vb" Inherits="PenOC.Login"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Login</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet">
		</link>
		<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-17068661-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
	</HEAD>
	<body onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblMain" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="SectionHeader">
						<asp:Label id="Label1" runat="server" Font-Bold="True">Login</asp:Label></TD>
				</TR>
				<tr>
					<td>
						<asp:Panel id="pnlLogin" runat="server" CssClass="ItemBackground">
							<asp:Label id="Label5" runat="server" CssClass="Text_10">Some areas of this website require you to login first</asp:Label>
							<BR>
							<asp:LinkButton id="cmdRequest" runat="server" CssClass="Text_10">Request a login</asp:LinkButton>
							<P></P>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD noWrap>
										<asp:Label id="Label2" runat="server" CssClass="text_10">user name:</asp:Label></TD>
									<TD noWrap>
										<asp:TextBox id="txtUserName" runat="server" Width="150px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD noWrap>
										<asp:Label id="Label3" runat="server" CssClass="text_10">password:</asp:Label></TD>
									<TD noWrap>
										<asp:TextBox id="txtPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD noWrap></TD>
									<TD noWrap align="right">
										<P>
											<asp:Button id="cmdLogin" runat="server" CssClass="button" Width="50px" Text="ok"></asp:Button></P>
									</TD>
								</TR>
							</TABLE>
							<BR>
							<asp:Panel id="pnlError" runat="server">
								<P>
									<asp:Label id="lblError1" runat="server" CssClass="text_10">There was a problem with either the user name or password supplied.</asp:Label><BR>
									<asp:Label id="lblError2" runat="server" CssClass="text_10">Make sure that 'Caps Lock' is not switched on.</asp:Label></P>
							</asp:Panel>
						</asp:Panel>
						<P>
							<asp:Panel id="pnlSuccess" runat="server" CssClass="ItemBackground">
								<asp:Label id="Label4" runat="server" CssClass="text_10">Logged in :</asp:Label>
								<asp:Label id="lblUserName" runat="server" Font-Bold="True" CssClass="text_10"></asp:Label>
							</asp:Panel></P>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
