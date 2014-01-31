<%@ Control Language="vb" AutoEventWireup="false" Codebehind="HTMLEditor.ascx.vb" Inherits="PenOC.HTMLEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD colSpan="2">
			<asp:textbox id="txtText" runat="server" Height="400px" TextMode="MultiLine" Width="100%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>
			<asp:ImageButton id="cmdBold" runat="server" ImageUrl="images/Bold.bmp"></asp:ImageButton>
			<asp:ImageButton id="cmdItalic" runat="server" ImageUrl="images/Italic.bmp"></asp:ImageButton>
			<asp:ImageButton id="cmdUnderline" runat="server" ImageUrl="images/Underline.bmp"></asp:ImageButton>&nbsp;
			<asp:ImageButton id="cmdCenter" runat="server" ImageUrl="images/Center.bmp"></asp:ImageButton>&nbsp;
			<asp:ImageButton id="cmdRule" runat="server" ImageUrl="images/HR.bmp"></asp:ImageButton>
			<asp:ImageButton id="cmdLineBreak" runat="server" ImageUrl="images/LineBreak.bmp"></asp:ImageButton></TD>
		<TD align="right">
			<asp:textbox id="txtInput" runat="server" Width="251px"></asp:textbox>
			<asp:ImageButton id="cmdLink" runat="server" ImageUrl="images/Link.bmp"></asp:ImageButton>
			<asp:ImageButton id="cmdImage" runat="server" ImageUrl="images/Image.bmp"></asp:ImageButton>
			<asp:Button id="cmdFile" runat="server" Text="file" CssClass="button"></asp:Button></TD>
	</TR>
</TABLE>
