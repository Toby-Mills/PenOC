<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MembershipList.ascx.vb" Inherits="PenOC.MembershipList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="grdMembership" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0"
	BorderWidth="0px" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle CssClass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idMembership"></asp:BoundColumn>
		<asp:TemplateColumn></asp:TemplateColumn>
		<asp:BoundColumn DataField="ClubShortName" HeaderText="Club"></asp:BoundColumn>
		<asp:BoundColumn DataField="strMembershipNumber" HeaderText="Mem. Number"></asp:BoundColumn>
		<asp:BoundColumn DataField="strMembershipType" HeaderText="Mem. Type"></asp:BoundColumn>
		<asp:BoundColumn DataField="PrincipalMember" HeaderText="Principal"></asp:BoundColumn>
		<asp:BoundColumn DataField="MemberCount" HeaderText="Members"></asp:BoundColumn>
		<asp:BoundColumn DataField="LastYear" HeaderText="Last Year"></asp:BoundColumn>
	</Columns>
</asp:DataGrid>
