<%@ Register TagPrefix="uc1" TagName="EventSearch" Src="EventSearch.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EventsArchive.aspx.vb" Inherits="PenOC.EventsArchive"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventsArchive</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0" class="ItemBackground">
				<TR>
					<TD class="SectionHeader">
						<asp:Label id="Label2" runat="server">Events Archive</asp:Label></TD>
				</TR>
				<TR>
					<TD><asp:label id="Label1" runat="server" Font-Bold="True" CssClass="Text_10">Events in Archive:</asp:label><asp:label id="lblEventCount" runat="server" CssClass="Text_10"></asp:label></TD>
				</TR>
				<TR>
					<TD>
						<uc1:eventsearch id="EventSearchArchive" runat="server"></uc1:eventsearch></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
