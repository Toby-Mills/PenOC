<%@ Register TagPrefix="uc1" TagName="UploadFile" Src="UploadFile.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminFiles.aspx.vb" Inherits="PenOC.AdminFiles"%>
<%@ Register TagPrefix="uc1" TagName="FileList" Src="FileList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminFiles</title>
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
	<body onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0" class="ItemBackground">
				<TR class="SectionHeader">
					<TD>
						<asp:Label id="Label1" runat="server">Files</asp:Label></TD>
					<TD align="right">
						<P>
							<asp:Button id="cmdAdd" runat="server" Text="add" CssClass="button"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<br>
						<P>
							<uc1:UploadFile id="UploadFile" runat="server" Visible="False"></uc1:UploadFile></P>
						<P>
							<uc1:FileList id="FileList" runat="server"></uc1:FileList></P>
					</TD>
				</TR>
			</TABLE>
			<asp:Label id="Label2" runat="server" CssClass="Text_10">A direct download link to any of these files can be written as:</asp:Label>
			<BR>
			<asp:Label id="Label3" runat="server" CssClass="Text_10"> http://www.penoc.org.za/File.aspx?idFile=XXX</asp:Label>
			<BR>
			<asp:Label id="Label4" runat="server" Font-Italic="True" CssClass="Text_10">(Replace XXX with the ID of the file)</asp:Label>
		</form>
	</body>
</HTML>
