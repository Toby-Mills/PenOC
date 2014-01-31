<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DownloadEdit_Popup.aspx.vb" Inherits="PenOC.DownloadEdit_Popup"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DownloadEdit_Popup</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link id="lnkStylesheet" type="text/css" rel="stylesheet" runat="server" />
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="WIDTH: 115px">
						<asp:Label id="Label4" runat="server">Download ID:</asp:Label></TD>
					<TD style="WIDTH: 657px">
						<asp:TextBox id="txtDownloadID" runat="server" Width="56px" ReadOnly="True"></asp:TextBox>
						<asp:Label id="Label5" runat="server">(system assigned)</asp:Label></TD>
					<TD align="right">
						<asp:ImageButton id="btnCancel" runat="server" ImageUrl="images/Cancel.bmp" ToolTip="Cancel"></asp:ImageButton>
						<asp:ImageButton id="btnSave" runat="server" ImageUrl="images/Save.bmp" ToolTip="Save"></asp:ImageButton></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 115px">
						<asp:Label id="Label1" runat="server">File ID:</asp:Label></TD>
					<TD style="WIDTH: 657px">
						<asp:TextBox id="txtFileID" runat="server" Width="56px"></asp:TextBox></TD>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 115px">
						<asp:Label id="Label2" runat="server">Title:</asp:Label></TD>
					<TD style="WIDTH: 657px">
						<asp:TextBox id="txtTitle" runat="server" Width="584px"></asp:TextBox></TD>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 115px">
						<asp:Label id="Label3" runat="server">Description:</asp:Label></TD>
					<TD style="WIDTH: 657px"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<asp:TextBox id="txtDescription" runat="server" Width="100%" TextMode="MultiLine" Height="224px"></asp:TextBox></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
