<%@ Register TagPrefix="uc1" TagName="EventList" Src="EventList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EventsRecent_Small.aspx.vb" Inherits="PenOC.EventsRecent_Small"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventsRecent_Small</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD bgColor="#006699"><asp:label id="Label1" runat="server" CssClass="SectionHeader">Recent Events</asp:label></TD>
				</TR>
				<TR>
					<TD><uc1:eventlist id="EventListRecent" runat="server"></uc1:eventlist></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
