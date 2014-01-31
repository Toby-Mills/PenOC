<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewsArchive.aspx.vb" Inherits="PenOC.NewsArchive"%>
<%@ Register TagPrefix="uc1" TagName="NewsList" Src="NewsList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>News</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet" />
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
			<uc1:NewsList id="NewsListArchive" runat="server"></uc1:NewsList>
		</form>
	</body>
</HTML>
