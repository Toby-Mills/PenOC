<%@ Register TagPrefix="uc1" TagName="EventSearch" Src="EventSearch.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminEvents.aspx.vb" Inherits="PenOC.AdminEvents"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminEvents</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="ItemBackground">
				<TR class="SectionHeader">
					<TD width="150">
						<asp:Label id="Label1" runat="server">Events</asp:Label></TD>
					<TD align="right">
						<asp:Button id="cmdNewEvent" runat="server" Text="new" Width="50px" CssClass="Button"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<uc1:EventSearch id="EventSearch" runat="server"></uc1:EventSearch></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
