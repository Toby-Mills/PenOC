<%@ Register TagPrefix="uc1" TagName="EventSearch" Src="EventSearch.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EventSearchPopup.aspx.vb" Inherits="PenOC.EventSearchPopup"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventSearchPopup</title>
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
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:Label id="lblMessage" runat="server" CssClass="text_10"></asp:Label></P>
			<P>
				<uc1:EventSearch id="EventSearch" runat="server"></uc1:EventSearch>
				<asp:TextBox id="txtIDControlID" runat="server"></asp:TextBox>
				<asp:TextBox id="txtNameControlID" runat="server"></asp:TextBox>
				<asp:TextBox id="txtAutoPostback" runat="server"></asp:TextBox></P>
		</form>
	</body>
</HTML>
