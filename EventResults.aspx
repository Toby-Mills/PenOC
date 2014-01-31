<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EventResults.aspx.vb" Inherits="PenOC.EventResults"%>
<%@ Register TagPrefix="uc1" TagName="CourseList" Src="CourseList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CourseResults" Src="CourseResults.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EventBrief" Src="EventBrief.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventResults</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<P>
				<uc1:eventbrief id="EventBrief" runat="server" DESIGNTIMEDRAGDROP="31"></uc1:eventbrief></P>
			<TABLE id="Table1" class="ItemBackground" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="SectionHeader">
						<asp:Label id="Label1" runat="server" CssClass="SectionHeader">Courses</asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<uc1:courselist id="CourseList" runat="server"></uc1:courselist></TD>
				</TR>
			</TABLE>
			<br>
			<uc1:courseresults id="CourseResults" runat="server"></uc1:courseresults></P>
			<asp:Panel id="pnlReport" runat="server" CssClass="ItemBackground">
				<DIV class="text_10">
					<asp:Literal id="litReport" runat="server"></asp:Literal></DIV>
			</asp:Panel></form>
	</body>
</HTML>
