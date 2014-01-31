<%@ Register TagPrefix="uc1" TagName="CompetitorSelect" Src="CompetitorSelect.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminCommittee.aspx.vb" Inherits="PenOC.AdminCommittee" EnableEventValidation="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminCommittee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0" class="ItemBackground">
				<TR Class="SectionHeader">
					<TD colSpan="2"><asp:label id="Label1" runat="server">Committee:</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblPerson" runat="server" CssClass="Text_10">person:</asp:label></TD>
					<TD><asp:textbox id="txtPersonName" runat="server" Width="300px" ReadOnly="True"></asp:textbox><asp:imagebutton id="cmdCompetitorSelect" runat="server" ImageUrl="images/person.bmp"></asp:imagebutton><asp:textbox id="txtPersonID" runat="server" Width="12px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:label id="lblPosition" runat="server" CssClass="Text_10">position:</asp:label></TD>
					<TD><asp:textbox id="txtPosition" runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD><asp:button id="cmdAdd" runat="server" CssClass="Button" Text="add"></asp:button></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<uc1:competitorlist id="CompetitorListCommittee" runat="server"></uc1:competitorlist></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
