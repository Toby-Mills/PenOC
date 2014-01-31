<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HomeNews.aspx.vb" Inherits="PenOC.HomeNews"%>
<%@ Register TagPrefix="uc1" TagName="NewsList" Src="NewsList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>HomeNews</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link id="lnkStylesheet" type="text/css" rel="stylesheet" runat="server">
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
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<uc1:NewsList id="NewsListMostRecent" runat="server"></uc1:NewsList></P>
			<P>
				<uc1:NewsList id="NewsListRecent" runat="server"></uc1:NewsList></P>
			<P>
				<asp:LinkButton id="cmdMoreItems" runat="server">More ...</asp:LinkButton></P>
		</form>
	</body>
</HTML>
