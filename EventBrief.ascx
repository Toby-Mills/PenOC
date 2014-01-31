<%@ Control Language="vb" AutoEventWireup="false" Codebehind="EventBrief.ascx.vb" Inherits="PenOC.EventBrief" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0" class="ItemBackground">
	<TR class="Sectionheader">
		<TD colSpan="3">
			<asp:Label id="Label2" runat="server" CssClass="SectionHeader">Event:</asp:Label>
			<asp:Label id="lblName" runat="server" CssClass="SectionHeaderText"></asp:Label></TD>
		<TD align="right">
			<asp:Button id="cmdEdit" CssClass="button" runat="server" Text="edit" Width="50px" Visible="False"></asp:Button></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 119px">
			<asp:Label id="Label1" Font-Bold="True" runat="server" CssClass="Text_10">Date:</asp:Label></TD>
		<TD>
			<asp:Label id="lblDate" runat="server" CssClass="Text_10"></asp:Label></TD>
		<TD style="WIDTH: 120px">
			<asp:Label id="Label7" Font-Bold="True" runat="server" CssClass="Text_10">Planner:</asp:Label></TD>
		<TD>
			<asp:Label id="lblPlanner" runat="server" CssClass="Text_10"></asp:Label></TD>
	</TR>
	<TR class="ItemBackground">
		<TD style="WIDTH: 119px">
			<asp:Label id="Label5" Font-Bold="True" runat="server" CssClass="Text_10">Venue:</asp:Label></TD>
		<TD>
			<asp:Label id="lblVenue" runat="server" CssClass="Text_10"></asp:Label></TD>
		<TD style="WIDTH: 120px">
			<asp:Label id="Label9" Font-Bold="True" runat="server" CssClass="Text_10">Controller:</asp:Label></TD>
		<TD>
			<asp:Label id="lblController" runat="server" CssClass="Text_10"></asp:Label></TD>
	</TR>
</TABLE>
