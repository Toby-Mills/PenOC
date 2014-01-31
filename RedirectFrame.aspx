<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RedirectFrame.aspx.vb" Inherits="PenOC.RedirectFrame" EnableSessionState="False" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RedirectFrame</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link type="text/css" rel="stylesheet" href="styles.css">
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
			<P align="center">
				<asp:Label id="lblMessage" runat="server" Width="164px" Height="8px" CssClass="text_16" Font-Bold="True">Loading ...</asp:Label></P>
		</form>
	</body>
</HTML>
