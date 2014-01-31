<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EventDetails.aspx.vb" Inherits="PenOC.EventDetails"%>
<%@ Register TagPrefix="uc1" TagName="EventBrief" Src="EventBrief.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventDetails</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet"></link>
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
				<uc1:EventBrief id="EventBrief" runat="server"></uc1:EventBrief></P>
			<TABLE id="Table1" class="ItemBackground" cellSpacing="0" cellPadding="2" width="100%"
				border="1">
				<TR>
					<TD align="left" class="SectionHeader">
						<asp:PlaceHolder id="phEventType" runat="server"></asp:PlaceHolder></TD>
					<TD align="left" Class="SectionHeader">
						<asp:Label id="lblSpecialNote" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" class="SectionHeader">
						<asp:Label id="Label1" runat="server">Registration:</asp:Label></TD>
					<TD Class="DataGridItem">
						<asp:Label id="lblRegistration" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" class="SectionHeader">
						<asp:Label id="Label2" runat="server">Starts:</asp:Label></TD>
					<TD Class="DataGridAlternatingItem">
						<asp:Label id="lblStarts" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" Class="SectionHeader">
						<asp:Label id="Label3" runat="server">Courses Close:</asp:Label></TD>
					<TD Class="DataGridItem">
						<asp:Label id="lblClose" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" Class="SectionHeader">
						<asp:Label id="Label7" runat="server">Courses:</asp:Label></TD>
					<TD Class="DataGridAlternatingItem">
						<asp:Label id="lblCourses" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" Class="SectionHeader">
						<asp:Label id="Label8" runat="server">Cost:</asp:Label></TD>
					<TD Class="DataGridItem">
						<asp:Label id="lblCost" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" noWrap width="100" Class="SectionHeader">
						<asp:Label id="Label4" runat="server">Directions:</asp:Label></TD>
					<TD Class="DataGridAlternatingItem">
						<asp:Label id="lblDirections" runat="server"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
