<%@ Register TagPrefix="uc1" TagName="CompetitorSearch" Src="CompetitorSearch.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompetitorSearchPopup.aspx.vb" Inherits="PenOC.CompetitorSearchPopup"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompetitorSearchPopup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<uc1:competitorsearch id="CompetitorSearch" runat="server"></uc1:competitorsearch><asp:textbox id="txtIDControlID" runat="server"></asp:textbox><asp:textbox id="txtNameControlID" runat="server"></asp:textbox></form>
	</body>
</HTML>
