<%@ Control Language="vb" AutoEventWireup="false" Codebehind="EventSearch.ascx.vb" Inherits="PenOC.EventSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="EventList" Src="EventList.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0" class=Itembackground>
	<TR>
		<TD><asp:label id="Label1" runat="server" Font-Bold="True" CssClass="Text_10">After:</asp:label></TD>
		<TD><asp:textbox id="txtDateFrom" runat="server"></asp:textbox><asp:imagebutton id="cmdCalDateFrom" runat="server" ImageUrl="images/cal.gif"></asp:imagebutton></TD>
		<TD><asp:label id="Label5" runat="server" Font-Bold="True" CssClass="Text_10">Club:</asp:label></TD>
		<TD><asp:dropdownlist id="cmbClub" runat="server"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label2" runat="server" Font-Bold="True" CssClass="Text_10">Before:</asp:label></TD>
		<TD><asp:textbox id="txtDateTo" runat="server"></asp:textbox><asp:imagebutton id="cmdCalDateTo" runat="server" ImageUrl="images/cal.gif"></asp:imagebutton></TD>
		<TD><asp:label id="Label3" runat="server" Font-Bold="True" CssClass="Text_10">Organiser:</asp:label></TD>
		<TD><asp:dropdownlist id="cmbOrganiser" runat="server"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label4" runat="server" Font-Bold="True" CssClass="Text_10">Venue:</asp:label></TD>
		<TD><asp:dropdownlist id="cmbVenue" runat="server"></asp:dropdownlist></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD align="center" class=SectionHeader colSpan="4"><asp:button id="cmdSearch" runat="server" Text="search" CssClass="Button"></asp:button></TD>
	</TR>
</TABLE>
<P><uc1:eventlist id="EventListSearch" runat="server"></uc1:eventlist></P>
