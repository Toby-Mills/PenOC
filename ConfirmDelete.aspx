<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfirmDelete.aspx.vb" Inherits="PenOC.ConfirmDelete"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ConfirmDelete</title>
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
	<body MS_POSITIONING="GridLayout"  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="100%" border="0">
				<TR>
					<TD></TD>
					<TD align="center">
						<asp:Label id="lblWarning" runat="server" CssClass="text_10" Font-Bold="True">WARNING!</asp:Label></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="images/warning.jpg"></asp:ImageButton></TD>
					<TD align="center">
						<asp:Label id="lblMessage1" runat="server" CssClass="text_10">Are you sure you want to delete this </asp:Label>
						<asp:Label id="lblMessage2" runat="server" CssClass="text_10"></asp:Label>
						<asp:Label id="lblMessage3" runat="server" CssClass="text_10">?</asp:Label></TD>
					<TD align="right">
						<asp:Image id="Image1" runat="server" ImageUrl="images/warning.jpg"></asp:Image></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="center">
						<asp:Button id="cmdCancel" runat="server" Text="cancel" CssClass="button"></asp:Button>&nbsp;
						<asp:Button id="cmdDelete" runat="server" Text="delete" CssClass="button"></asp:Button></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="center">
						<asp:Label id="lblError" runat="server" CssClass="text_10" ForeColor="Red"></asp:Label></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
