<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ExportResults.aspx.vb" Inherits="PenOC.ExportResults"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ExportResults</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
				<asp:label id="Label4" runat="server">Event:</asp:label>
				<asp:textbox id="txtEvent" runat="server" Width="224px" ReadOnly="True"></asp:textbox>
				<asp:textbox id="txtEventID" runat="server" DESIGNTIMEDRAGDROP="80"></asp:textbox>
				<asp:linkbutton id="cmdEventSearch" runat="server">Select Event</asp:linkbutton>
				<asp:Button id="cmdExportResults" runat="server" Text="export" CssClass="button"></asp:Button></P>
		</form>
	</body>
</HTML>
