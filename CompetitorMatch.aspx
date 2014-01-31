<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompetitorMatch.aspx.vb" Inherits="PenOC.CompetitorMatch"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompetitorMatch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="styles.css" type="text/css" rel="stylesheet">
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
	<body  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:Label id="Label2" runat="server"> Matches for:</asp:Label>
				<asp:Label id="lblCompetitorName" runat="server" Font-Bold="True"></asp:Label></P>
			<P>
				<uc1:CompetitorList id="CompetitorListMatch" runat="server"></uc1:CompetitorList></P>
			<P>
				<asp:Label id="lblNoMatch" runat="server">No matches were found in the database</asp:Label></P>
			<P>
				<asp:Button id="cmdClose" runat="server" Text="close" CssClass="utton"></asp:Button></P>
			<P>
				<asp:TextBox id="txtReturnIDControl" runat="server"></asp:TextBox></P>
		</form>
	</body>
</HTML>
