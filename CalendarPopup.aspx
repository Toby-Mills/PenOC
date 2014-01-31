<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CalendarPopup.aspx.vb" Inherits="PenOC.CalendarPopup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		<center>
			<form id="Form1" runat="server">
				<P></P>
				<P>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD align="center" colSpan="2"><asp:calendar id="thedate" runat="server" BorderColor="White" Font-Names="Verdana" Font-Size="9pt"
									ForeColor="Black" BackColor="MintCream" NextPrevFormat="ShortMonth" BorderWidth="1px" Width="250px" Height="200px" SelectionChanged="thedate_SelectionChanged">
									<TodayDayStyle BackColor="#CCCCCC"></TodayDayStyle>
									<DayStyle ForeColor="DarkBlue"></DayStyle>
									<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom"></NextPrevStyle>
									<DayHeaderStyle Font-Size="8pt" Font-Bold="True" ForeColor="DarkBlue"></DayHeaderStyle>
									<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
									<TitleStyle Font-Size="12pt" Font-Bold="True" BorderWidth="4px" ForeColor="#333399" BorderColor="Black"
										BackColor="White"></TitleStyle>
									<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
								</asp:calendar></TD>
						</TR>
						<TR>
							<TD align="center" colSpan="2"><asp:label id="lblTime" runat="server" Font-Names="Verdana">Time</asp:label><asp:textbox id="txtHour" runat="server" Width="26px" Font-Names="Verdana"></asp:textbox><asp:label id="lblHourMinSeparator" runat="server">:</asp:label><asp:textbox id="txtMinute" runat="server" Width="26px" Font-Names="Verdana"></asp:textbox><asp:label id="lblMinSecSeparator" runat="server">:</asp:label><asp:textbox id="txtSecond" runat="server" Width="26px" Font-Names="Verdana"></asp:textbox></TD>
						</TR>
						<TR>
							<TD><asp:linkbutton id="lnkPreviousYear" runat="server" Font-Names="Verdana"></asp:linkbutton></TD>
							<TD>
								<P align="right"><asp:linkbutton id="lnkNextYear" runat="server" Font-Names="Verdana"></asp:linkbutton></P>
							</TD>
						</TR>
						<TR>
							<TD align="center" colSpan="2"><input id="control" type="hidden" name="control" runat="server"></TD>
						</TR>
					</TABLE>
				</P>
			</form>
		</center>
	</body>
</HTML>
