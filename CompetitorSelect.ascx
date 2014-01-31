<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CompetitorSelect.ascx.vb" Inherits="PenOC.CompetitorSelect" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD><asp:label id="Label2" runat="server" CssClass="Text_10">Gender:</asp:label></TD>
		<TD><asp:radiobuttonlist id="rblGender" runat="server" AutoPostBack="True" Width="308px" RepeatDirection="Horizontal"
				CssClass="Text_10"></asp:radiobuttonlist></TD>
		<TD align="right">
			<asp:Button id="cmdEditCompetitor" runat="server" Width="50px" Text="edit" Visible="False" CssClass="Button"></asp:Button>&nbsp;<asp:button id="cmdAddNew" runat="server" Width="50px" Text="new" Visible="False" CssClass="Button"></asp:button></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label3" runat="server" CssClass="Text_10">Surname:</asp:label></TD>
		<TD colSpan="2"><asp:radiobuttonlist id="rblSurname" runat="server" AutoPostBack="True" Width="314px" RepeatDirection="Horizontal"
				CssClass="Text_10">
				<asp:ListItem Value="All" Selected="True">All</asp:ListItem>
				<asp:ListItem Value="1">A-F</asp:ListItem>
				<asp:ListItem Value="2">G-M</asp:ListItem>
				<asp:ListItem Value="3">N-R</asp:ListItem>
				<asp:ListItem Value="4">S-Z</asp:ListItem>
			</asp:radiobuttonlist></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label1" runat="server" CssClass="Text_10">Competitor:</asp:label></TD>
		<TD colSpan="2">
			<P>
				<asp:DropDownList id="cmbCompetitor" runat="server" AutoPostBack="True" Font-Size="10pt" Width="300px"></asp:DropDownList>
				<asp:Button id="cmdRefresh" runat="server" Text="refresh" CssClass="Button"></asp:Button></P>
		</TD>
	</TR>
</TABLE>
