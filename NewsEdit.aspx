<%@ Register TagPrefix="uc1" TagName="HTMLEditor" Src="HTMLEditor.ascx" %>
<%@ Page Language="vb" validaterequest="false" AutoEventWireup="false" Codebehind="NewsEdit.aspx.vb" Inherits="PenOC.NewsEdit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>NewsEdit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet" />
		<SCRIPT language="javascript">
			function storeCaret (textEl) {
				if (textEl.createTextRange) 
					textEl.caretPos = document.selection.createRange().duplicate();
				}
			function insertAtCaret (textEl, text) {
				if (textEl.createTextRange && textEl.caretPos) {
					var caretPos = textEl.caretPos;
					caretPos.text =
					caretPos.text.charAt(caretPos.text.length - 1) == ' ' ?
					text + ' ' : text;
				}
				else
					textEl.value  = text;
				}
		</SCRIPT>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD bgColor="#006699" noWrap>
						<asp:Label id="Label4" runat="server" CssClass="SectionHeader">News Item:</asp:Label>
						<asp:TextBox id="txtID" runat="server" Width="36px"></asp:TextBox></TD>
					<TD align="right" bgColor="#006699">
						<asp:Button id="cmdEdit" runat="server" Text="edit" Width="50px" Font-Size="10pt" CssClass="Button"></asp:Button>&nbsp;
						<asp:Button id="cmdSave" runat="server" Text="save" Width="50px" Font-Size="10pt" CssClass="Button"></asp:Button>&nbsp;
						<asp:Button id="cmdCancel" runat="server" Text="cancel" Width="50px" Font-Size="10pt" CssClass="Button"></asp:Button></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label1" runat="server" Font-Bold="True" CssClass="Text_10">Date:</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtDate" runat="server" Font-Size="10pt"></asp:TextBox>
						<asp:ImageButton id="cmdCalDate" runat="server" ImageUrl="images/cal.gif"></asp:ImageButton>
                        <asp:Label ID="lblValidationDate" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server" Font-Bold="True" CssClass="Text_10">Title:</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtTitle" runat="server" Width="500px" Font-Size="10pt"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="right">
						<asp:Button id="cmdUploadImage" runat="server" CssClass="button" Text="upload image"></asp:Button></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:Label id="Label3" runat="server" Font-Bold="True" CssClass="Text_10">Text:</asp:Label></TD>
					<TD>
						<uc1:HTMLEditor id="HTMLEditorNews" runat="server"></uc1:HTMLEditor></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
