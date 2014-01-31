<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UploadImage.aspx.vb" Inherits="PenOC.UploadImage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadNewsImages</title>
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
			<TABLE id="Table1" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD bgColor="#006699" colSpan="2"><asp:label id="Label4" runat="server" CssClass="SectionHeader">Upload Images:</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label5" runat="server" CssClass="Text_10">Destination directory:</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtPath" runat="server" Width="500px"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD align="right"><asp:label id="Label1" runat="server" CssClass="Text_10">File 1:</asp:label></TD>
					<TD><INPUT id="file1" type="file" size="40" runat="server" accept="image/*"></TD>
				</TR>
				<TR>
					<TD align="right"><asp:label id="Label2" runat="server" CssClass="Text_10">File 2:</asp:label></TD>
					<TD><INPUT id="file2" type="file" size="40" runat="server" accept="image/*"></TD>
				</TR>
				<TR>
					<TD align="right"><asp:label id="Label3" runat="server" accept="image/*" CssClass="Text_10">File 3:</asp:label></TD>
					<TD><INPUT id="file3" type="file" size="40" runat="server"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="cmdUpload" runat="server" Text="upload" Width="50px" CssClass="Button"></asp:button></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
