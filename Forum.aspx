<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Forum.aspx.vb" Inherits="PenOC.Forum"%>
<%@ Register TagPrefix="uc1" TagName="ThreadList" Src="ThreadList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Forum</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			<P>
				<asp:Label id="Label1" runat="server" CssClass="text_10">Forum:</asp:Label>
				<asp:Label id="lblForumName" runat="server" CssClass="text_10"></asp:Label>
				<asp:TextBox id="txtForumID" runat="server"></asp:TextBox></P>
			<P>
				<uc1:ThreadList id="ThreadListForum" runat="server"></uc1:ThreadList></P>
		</form>
	</body>
</HTML>
