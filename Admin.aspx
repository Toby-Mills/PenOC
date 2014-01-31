<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Admin.aspx.vb" Inherits="PenOC.Admin"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Admin</title>
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
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="ItemBackground">
					<TR>
						<TD class="SectionHeader">
							<P>
								<asp:Label id="Label1" runat="server" CssClass="SectionHeader">PenOC Website Admin Tools</asp:Label></P>
						</TD>
						<TD class="SectionHeader" align="right">
							<asp:LinkButton id="cmdLogOut" runat="server" CssClass="text_10">log out</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="text_10" Font-Bold="True" Font-Underline="True">Events</asp:Label></TD>
						<TD>
							<asp:Label id="Label6" runat="server" CssClass="text_10" Font-Bold="True" Font-Underline="True">News</asp:Label></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<P>
								<asp:LinkButton id="cmdAddEvent" runat="server" CssClass="text_10">Add a new event</asp:LinkButton></P>
						</TD>
						<TD>
							<asp:LinkButton id="cmdAddNews" runat="server" CssClass="text_10">Add news item</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<P>
								<asp:LinkButton id="cmdEditEvent" runat="server" CssClass="text_10">Edit event details</asp:LinkButton></P>
						</TD>
						<TD>
							<asp:LinkButton id="cmdEditNews" runat="server" CssClass="text_10">Edit news item</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<P>
								<asp:LinkButton id="cmdImportResults" runat="server" CssClass="text_10">Import event results</asp:LinkButton></P>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 18px">
							<asp:LinkButton id="cmdExportResults" runat="server" CssClass="text_10">Export event results</asp:LinkButton></TD>
						<TD style="HEIGHT: 18px">
							<asp:Label id="Label5" runat="server" CssClass="text_10" Font-Bold="True" Font-Underline="True">Other</asp:Label></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 18px"></TD>
						<TD style="HEIGHT: 18px"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 18px">
							<asp:LinkButton id="cmdUploadPhotos" runat="server" CssClass="text_10">Upload event photos</asp:LinkButton></TD>
						<TD style="HEIGHT: 18px"></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label3" runat="server" CssClass="text_10" Font-Bold="True" Font-Underline="True">Competitors</asp:Label></TD>
						<TD>
							<asp:LinkButton id="cmdUsers" runat="server" CssClass="text_10">Add / Edit user</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:LinkButton id="cmdAddCompetitor" runat="server" CssClass="text_10">Add a new competitor</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="cmdFiles" runat="server" CssClass="text_10">Upload / Update files</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:LinkButton id="cmdEditCompetitor" runat="server" CssClass="text_10">Edit competitor details</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="cmdCommittee" runat="server" CssClass="text_10">Edit committee</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:LinkButton id="cmdMembership" runat="server" CssClass="text_10">Administer membership</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="cmdEditLookupTables" runat="server" CssClass="text_10">Edit lookup tables</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label4" runat="server" CssClass="text_10" Font-Bold="True" Font-Underline="True">Logs</asp:Label></TD>
						<TD>
							<asp:LinkButton id="cmdWorkItems" runat="server" CssClass="text_10">Website work items</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<asp:LinkButton id="cmdLogs" runat="server" CssClass="text_10">Administer logs</asp:LinkButton></TD>
						<TD>&nbsp;</TD>
					</TR>
				</TABLE>
			</P>
		</form>
	</body>
</HTML>
