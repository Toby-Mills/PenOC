<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CompetitorSearch.ascx.vb" Inherits="PenOC.CompetitorSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="ItemBackground">
	<TR>
		<TD>
			<asp:Label id="Label1" runat="server" CssClass="Text_10">Name:</asp:Label></TD>
		<TD>
			<asp:TextBox id="txtName" runat="server" Width="400px" Font-Size="10pt"></asp:TextBox></TD>
		<TD>
			<asp:Label id="Label2" runat="server" CssClass="Text_10">Organisers:</asp:Label></TD>
		<TD>
			<asp:CheckBox id="chkOrganisers" runat="server" DESIGNTIMEDRAGDROP="220" CssClass="Text_10"></asp:CheckBox></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label4" runat="server" CssClass="Text_10">Category:</asp:Label></TD>
		<TD colSpan="3">
			<asp:RadioButtonList id="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="Text_10">
				<asp:ListItem Value="0" Selected="True">Unspecified</asp:ListItem>
				<asp:ListItem Value="1">Male</asp:ListItem>
				<asp:ListItem Value="2">Female</asp:ListItem>
				<asp:ListItem Value="3">Group</asp:ListItem>
			</asp:RadioButtonList></TD>
	</TR>
	<TR>
		<TD align="center" colSpan="4" class="SectionHeader">
			<asp:Button id="cmdSearch" runat="server" Text="search" Font-Size="10pt" CssClass="Button"></asp:Button></TD>
	</TR>
</TABLE>
<P>
	<uc1:CompetitorList id="CompetitorListSearch" runat="server"></uc1:CompetitorList></P>
