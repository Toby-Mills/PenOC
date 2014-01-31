<%@ Page Language="vb" AutoEventWireup="false" Codebehind="menu.aspx.vb" Inherits="PenOC.WebForm1"%>
<%@ Register TagPrefix="osm" Namespace="OboutInc.SlideMenu" Assembly="obout_SlideMenu3_Pro_Net" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body topmargin="0" onbeforeunload="doHourglass()" leftMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="10" width="100%" border="0">
				<TR>
					<TD vAlign="top" align="left" height="1">
						<P>
							<asp:Image id="Image3" runat="server" ImageUrl="images/PenOC.bmp" BackColor="White"></asp:Image><BR>
							<asp:Image id="Image2" runat="server" ImageUrl="images/Logo1.bmp"></asp:Image><br>
							<asp:Label id="lblUser" runat="server" DESIGNTIMEDRAGDROP="57" CssClass="text_10" Font-Bold="True">User:</asp:Label>
							<asp:Label id="lblUserName" runat="server" CssClass="text_10"></asp:Label>
							<osm:slidemenu id="menuPenOC" runat="server" Height="-1" StyleFolder="" UrlTarget="main" ScriptPath="./javascript"
								CSSParent="SMParent" CSSParentOver="SMParentOver" CSSParentSelected="SMParentSelected" CSSChild="SMChild"
								CSSChildOver="SMChildOver" CSSChildSelected="SMChildSelected" CSSMenu="SMMenu" CSSChildrenBox="SMChildrenBox"
								Speed="5"></osm:slidemenu></P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
