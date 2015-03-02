<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LogExplanation.aspx.vb" Inherits="PenOC.LogExplanation"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LogExplanation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			<P>
				<TABLE class="ItemBackground" id="Table5" cellSpacing="1" cellPadding="0" width="100%"
					border="0">
					<TR class="SectionHeader">
						<TD>Frequently Asked Questions about the Log</TD>
					</TR>
					<TR>
						<TD>
							<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="#WhatIs" CssClass="Text_10">What is the log?</asp:HyperLink><br>
							<asp:HyperLink id="Hyperlink2" runat="server" NavigateUrl="#Calculated" CssClass="Text_10">How is the log calculated?</asp:HyperLink><br>
							<asp:HyperLink id="Hyperlink3" runat="server" NavigateUrl="#Calculation" CssClass="Text_10">How are log points calculated?</asp:HyperLink><br>
							<asp:HyperLink id="Hyperlink4" runat="server" NavigateUrl="#Handicap" CssClass="Text_10">What is the Handicap Log?</asp:HyperLink>
						</TD>
					</TR>
				</TABLE>
			</P>
			<TABLE class="ItemBackground" id="Table1" cellSpacing="1" cellPadding="1" width="100%"
				border="0">
				<TR>
					<TD class="SectionHeader">
						<P><A name="WhatIs">What is the Log?</A></P>
					</TD>
				</TR>
				<TR>
					<TD>
						<P>
							<asp:Label id="Label2" runat="server" CssClass="Text_10">The Log is a competition which extends over the length of each season. Competitors are awarded points for each result, and the winner is the competitor with the most points at the end of the season.</asp:Label></P>
						<P>
							<asp:Label id="Label3" runat="server" CssClass="Text_10">There are 3 logs each year:</asp:Label></P>
						<OL>
							<LI>
								<asp:Label id="Label4" runat="server" CssClass="Text_10">The Colour-Coded Log. Based on results of colour-coded events (forest events). </asp:Label>
							<LI>
								<asp:Label id="Label5" runat="server" CssClass="Text_10">The Score Log. Based on results of score events. </asp:Label>
							<LI>
								<asp:Label id="Label6" runat="server" CssClass="Text_10">The Park Log. Based on results of park events (street & park events).</asp:Label></LI></OL>
						<P>
							<asp:Label id="Label7" runat="server" CssClass="Text_10">There are also 3 Handicap logs which are based on the same results, but points are adjusted according to age & gender.</asp:Label></P>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="SectionHeader">
						<A name="Calculated">
							<asp:Label id="Label1" runat="server">How is the Log calculated?</asp:Label></A></TD>
				</TR>
				<TR>
					<TD>
						<P>
							<asp:Label id="Label8" runat="server" CssClass="Text_10">The Log is calculated by adding up all points scored during the season for each competitor (see 'How are Log Points calculated?' below). For each competitor the worst 2 or 3 results are disregarded. The number of events diregarded is dependant on the log, and is usually about 20% of the number of events in the season.</asp:Label></P>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="SectionHeader"><A name="Calculation">How are Log Points calculated?</A></TD>
				</TR>
				<TR>
					<TD>
						<P>
							<asp:Label id="Label9" runat="server" CssClass="Text_10">Points for score events are calculated as the total points for controls collected minus penalties for finishing after the time-limit.</asp:Label></P>
						<P>
							<asp:Label id="Label10" runat="server" CssClass="Text_10">Points for colour-coded and championship events are caluculated as follows:</asp:Label></P>
						<P>&nbsp;
							<asp:Label id="Label11" runat="server" CssClass="Text_10">For each event, every course is given a rating based on technical and physical difficulty.</asp:Label>
						</P>
						<UL>
							<LI>
								<asp:Label id="Label12" runat="server" CssClass="Text_10">Physical difficulty is calculated by multiplying the total climb (m) by 10 and adding it to the total distance for the course (m). </asp:Label>
							<LI>
								<asp:Label id="Label13" runat="server" CssClass="Text_10">Technical difficulty is determined by the Planner, and is a simple &#39;Easy&#39;, &#39;Moderate&#39; or &#39;Difficult&#39;. &#39;Difficult&#39; courses rate 1.0 while &#39;Moderate&#39; courses rate 0.75 and &#39;Easy&#39; courses rate 0.25</asp:Label></LI></UL>
						<P>
							<asp:Label id="Label14" runat="server" CssClass="Text_10">The rating for any course is the Physical rating multiplied by the Technical rating.</asp:Label></P>
						<asp:Label id="Label15" runat="server" CssClass="Text_10">example:</asp:Label>
						<TABLE id="Table3" cellSpacing="0" cellPadding="1" border="1" class="Text_10" style="WIDTH: 288px; HEIGHT: 171px">
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader"><STRONG></STRONG></TD>
								<TD style="WIDTH: 72px" class="SectionHeader"><STRONG>Course1</STRONG></TD>
								<TD class="SectionHeader"><STRONG>Course 2</STRONG></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader">Length (m)</TD>
								<TD style="WIDTH: 72px" class="DataGridItem">6750</TD>
								<TD class="DataGridItem">4275</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader">Climb (m)</TD>
								<TD style="WIDTH: 72px" class="DataGridItem">120</TD>
								<TD class="DataGridItem">95</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader"><STRONG>Physical Rating</STRONG></TD>
								<TD style="WIDTH: 72px" class="DataGridItem"><STRONG>7950</STRONG></TD>
								<TD class="DataGridItem"><STRONG>5225</STRONG></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader">Difficulty</TD>
								<TD style="WIDTH: 72px" class="DataGridItem">Difficult</TD>
								<TD class="DataGridItem">Moderate</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader"><STRONG>Technical Rating</STRONG></TD>
								<TD style="WIDTH: 72px" class="DataGridItem"><STRONG>1.0</STRONG></TD>
								<TD class="DataGridItem"><STRONG>0.75</STRONG></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 122px" class="SectionHeader"><STRONG>Total Rating</STRONG></TD>
								<TD style="WIDTH: 72px" class="DataGridItem"><STRONG>7950</STRONG></TD>
								<TD class="DataGridItem"><strong>3919</strong></TD>
							</TR>
						</TABLE>
						<P>
							<asp:Label id="Label16" runat="server" CssClass="Text_10">The course with the highest rating is selected as the 'benchmark'. The winner of this course is awarded the maximum points for the event (usually 800 points for a colour-coded event and 1000 points for a championship event). Every other competitor on this course is awarded points pro-rata based on their finish times. The winner's time is divided by the competitor's time, and multiplied by the winner's points.)</asp:Label></P>
						<P>
							<asp:Label id="Label17" runat="server" CssClass="Text_10">example: </asp:Label>
							<TABLE id="Table2" cellSpacing="0" cellPadding="1" border="1" class="Text_10">
								<TR>
									<TD class="SectionHeader"><STRONG></STRONG></TD>
									<TD class="SectionHeader"><STRONG>Course 1 Winner</STRONG></TD>
									<TD class="SectionHeader"><STRONG>Competitor 1</STRONG></TD>
									<TD class="SectionHeader"><STRONG>Competitor 2</STRONG></TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Time</TD>
									<TD class="DataGridItem">1:07:35</TD>
									<TD class="DataGridItem">1:15:27</TD>
									<TD class="DataGridItem">1:52:09</TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Minutes</TD>
									<TD class="DataGridItem">67.58</TD>
									<TD class="DataGridItem">75.45</TD>
									<TD class="DataGridItem">112.15</TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Calculation</TD>
									<TD class="DataGridItem"></TD>
									<TD class="DataGridItem">(67.58 / 75.45) * 800</TD>
									<TD class="DataGridItem">(67.58 / 112.15) * 800</TD>
								</TR>
								<TR>
									<TD class="SectionHeader"><STRONG>Points</STRONG></TD>
									<TD class="DataGridItem"><STRONG>800</STRONG></TD>
									<TD class="DataGridItem"><STRONG>717</STRONG></TD>
									<TD class="DataGridItem"><STRONG>482</STRONG></TD>
								</TR>
							</TABLE>
						</P>
						<P>
							<asp:Label id="Label18" runat="server" CssClass="Text_10">Points for competitors on other courses are calculated the same way, and then multiplied by the relative rating of their course compared to the benchmark course.</asp:Label></P>
						<P>
							<asp:Label id="Label20" runat="server" CssClass="Text_10">example:</asp:Label>
							<TABLE id="Table4" cellSpacing="0" cellPadding="1" border="1" class="Text_10" style="BORDER-RIGHT: 1pt solid; BORDER-TOP: 1pt solid; BORDER-LEFT: 1pt solid; BORDER-BOTTOM: 1pt solid; BORDER-COLLAPSE: separate">
								<TR>
									<TD class="SectionHeader"><STRONG> </STRONG>
									</TD>
									<TD class="SectionHeader"><STRONG>Course 1 Winner</STRONG></TD>
									<TD class="SectionHeader"><STRONG>Competitor 3</STRONG></TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Course</TD>
									<TD class="DataGridItem">Course 1</TD>
									<TD class="DataGridItem">Course 2</TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Course Rating</TD>
									<TD class="DataGridItem">7950</TD>
									<TD class="DataGridItem">3919</TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Time</TD>
									<TD class="DataGridItem">1:07:35</TD>
									<TD class="DataGridItem">52:56</TD>
								</TR>
								<TR>
									<TD class="SectionHeader">Minutes</TD>
									<TD class="DataGridItem">67.58</TD>
									<TD class="DataGridItem">52.93</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 22px" class="SectionHeader">Calulation</TD>
									<TD style="HEIGHT: 22px" class="DataGridItem"></TD>
									<TD style="HEIGHT: 22px" class="DataGridItem">(67.58 / 52.93) * 800 * (3919 / 
										7950)&nbsp;</TD>
								</TR>
								<TR>
									<TD class="SectionHeader"><STRONG>Points</STRONG></TD>
									<TD class="DataGridItem"><STRONG>800</STRONG></TD>
									<TD class="DataGridItem"><STRONG>504</STRONG></TD>
								</TR>
							</TABLE>
						</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="SectionHeader"><A name="Handicap"></A>What is the Handicap Log?</A></TD>
				</TR>
				<TR>
					<TD>
						<P>
							<asp:Label id="Label19" runat="server" CssClass="Text_10">The Handicap log is used to provide an environment in which all competitors can compete equally regardless of age or gender. A set of correction factors are used to adjust existing log points depending on the category of the competitor.</asp:Label></P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
