<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UploadPhotos.aspx.vb" Inherits="PenOC.UploadPhotos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>UploadPhotos</title>
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
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:Label id="Label2" runat="server" Font-Bold="True">Note:</asp:Label>&nbsp;<EM>Event 
					photos should be uploaded in a single zip file. The zip file should consist of 
					two folders, called 'thumbnails' and 'images'. The images in the two folders 
					should have exactly the sames names.</EM></P>
			<P><asp:label id="Label4" runat="server">Event:</asp:label><asp:textbox id="txtEvent" runat="server" ReadOnly="True" Width="224px"></asp:textbox><asp:textbox id="txtEventID" runat="server" DESIGNTIMEDRAGDROP="80"></asp:textbox><asp:linkbutton id="cmdEventSearch" runat="server">Select Event</asp:linkbutton></P>
			<P>
				<asp:CheckBox id="chkAutoURL" runat="server" Text="Make photos visible through web page" Checked="True"></asp:CheckBox></P>
			<P>
				<asp:Label id="Label1" runat="server">Zip File:</asp:Label><INPUT id="filePhotos" type="file" runat="server" size="50">
			</P>
			<P>
				<asp:button id="cmdUpload" runat="server" Text="Upload" CssClass="button"></asp:button></P>
		</form>
	</body>
</HTML>
