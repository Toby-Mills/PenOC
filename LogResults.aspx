<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LogResults.aspx.vb" Inherits="PenOC.LogResults"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LogResults</title>
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
	<body onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="pnlError" runat="server" Visible="False">
				<asp:Label id="lblErrorMessage" runat="server" CssClass="text_10"></asp:Label>
			</asp:Panel>
			<asp:Panel id="pnlLogResult" runat="server">
				<TABLE class="ItemBackground" id="Table2" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD>
							<TABLE class="ItemBackground" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
								border="0">
								<TR class="SectionHeader">
									<TD colSpan="2">
										<asp:Label id="Label1" runat="server" CssClass="sectionheader" Font-Bold="True">Log:</asp:Label>
										<asp:Label id="lblLogDescription" runat="server" CssClass="sectionheader"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="SectionHeader" style="WIDTH: 135px">
										<asp:Label id="Label3" runat="server" CssClass="text_10" Font-Bold="True">Total Events:</asp:Label></TD>
									<TD>
										<asp:Label id="lblTotalEvents" runat="server" CssClass="text_10"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="SectionHeader" style="WIDTH: 135px">
										<asp:Label id="Label5" runat="server" CssClass="text_10" Font-Bold="True">Disregard worst: </asp:Label></TD>
									<TD>
										<asp:Label id="lblDisregardWorst" runat="server" CssClass="text_10"></asp:Label>
										<asp:Label id="Label7" runat="server" CssClass="text_10">events</asp:Label></TD>
								</TR>
							</TABLE>
							<BR>
							<asp:DataGrid id="grdLog" runat="server" CssClass="DataGrid" BorderWidth="1pt" BorderColor="Gainsboro"
								GridLines="Vertical">
								<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
								<ItemStyle CssClass="DataGridItem"></ItemStyle>
								<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
							</asp:DataGrid></TD>
					</TR>
				</TABLE>
			</asp:Panel>
            <asp:Label ID="Label2" runat="server" Text="Link to this page:"></asp:Label>
            <asp:Label ID="lblLogPermalink" runat="server"></asp:Label></form>
	</body>
</HTML>
