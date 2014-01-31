<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Contact.aspx.vb" Inherits="PenOC.Contact"%>
<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Contact</title>
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
	<body  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="ItemBackground">
				<TR>
					<TD class="SectionHeader" colSpan="2">
						<asp:Label id="Label7" runat="server">Contact Us</asp:Label></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label1" runat="server" CssClass="Text_10">General Enquiries can be addressed to:</asp:Label></TD>
					<TD>
						<asp:LinkButton id="cmdEmailEnquiry" runat="server" CssClass="Text_10"></asp:LinkButton></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label3" runat="server" CssClass="Text_10">To Subscribe to the Mailing List:</asp:Label></TD>
					<TD vAlign="top">
						<asp:Label id="Label8" runat="server" CssClass="Text_10">Send an e-mail to</asp:Label>
						<asp:HyperLink id="HyperLink2" runat="server" CssClass="Text_10" Target="_blank" NavigateUrl="mailto:penoc-subscribe@yahoogroups.com&#13;&#10;">penoc-subscribe@yahoogroups.com</asp:HyperLink>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label4" runat="server" CssClass="Text_10">To Unsubscribe from the Mailing List:</asp:Label></TD>
					<TD>
						<asp:Label id="Label5" runat="server" CssClass="Text_10">Send an e-mail to</asp:Label>
						<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="mailto:penoc-unsubscribe@yahoogroups.com&#13;&#10;"
							Target="_blank" CssClass="Text_10">penoc-unsubscribe@yahoogroups.com</asp:HyperLink></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label6" runat="server" CssClass="Text_10">Website problems and suggestions to:</asp:Label></TD>
					<TD>
						<asp:LinkButton id="cmdEmailWebsite" runat="server" CssClass="Text_10"></asp:LinkButton></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<HR width="100%" SIZE="1">
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server" CssClass="Text_10">Committee:</asp:Label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<uc1:CompetitorList id="CompetitorListCommittee" runat="server"></uc1:CompetitorList></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
