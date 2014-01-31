<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Downloads.aspx.vb" Inherits="PenOC.Downloads" %>
<%@ Register TagPrefix="uc1" TagName="DownloadList" Src="DownloadList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Downloads</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link id="lnkStylesheet" type="text/css" rel="stylesheet" runat="server" />
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
			<TABLE class="ItemBackground" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="SectionHeader"><asp:label id="lblDownload" runat="server">Downloads</asp:label></TD>
					<TD class="SectionHeader" align="right"><asp:button id="btnNewDownload" runat="server" Text="NEW" CssClass="Button" ToolTip="Create a new Download"></asp:button>
						<asp:Button id="btnRefresh" runat="server" Text="REFRESH" CssClass="Button" ToolTip="Refresh the list of Downloads"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="2"><uc1:downloadlist id="DownloadList" runat="server"></uc1:downloadlist></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
