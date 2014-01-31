<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UploadFile.ascx.vb" Inherits="PenOC.UploadFile" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
	cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD bgColor="#006699" colSpan="2">
			<asp:label id="Label4" runat="server" CssClass="SectionHeader">Upload File:</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label2" runat="server" CssClass="text_10">ID:</asp:Label></TD>
		<TD>
			<asp:TextBox id="txtID" runat="server" Width="40px" Enabled="False"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label5" runat="server" CssClass="Text_10">Description:</asp:Label></TD>
		<TD>
			<asp:TextBox id="txtDescription" runat="server" Width="500px"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD align="left">
			<asp:label id="Label1" runat="server" CssClass="Text_10">File:</asp:label></TD>
		<TD><INPUT id="file1" type="file" size="65" name="file1" runat="server"></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2">&nbsp;
			<asp:Label id="lblUploadValidation" runat="server" CssClass="validator" Visible="False">Please browse to the file to upload</asp:Label></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="2">
			<asp:button id="cmdUpload" runat="server" CssClass="Button" Width="50px" Text="upload"></asp:button>
			<asp:Button id="cmdCancel" CssClass="button" runat="server" Text="cancel"></asp:Button></TD>
	</TR>
</TABLE>
