<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CompetitorList.ascx.vb" Inherits="PenOC.CompetitorList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="grdCompetitor" GridLines="None" AutoGenerateColumns="False"
	Width="100%" runat="server" AllowPaging="True" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle cssclass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idCompetitor"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" runat="server" ImageUrl="images/Delete.bmp" OnClick="cmdDelete_OnClick"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="E-Mail">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdEmail" runat="server" ImageUrl="images/Email.bmp" OnClick="cmdEmail_OnClick"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Position" HeaderText="Position"></asp:BoundColumn>
		<asp:BoundColumn DataField="FullName" HeaderText="Name"></asp:BoundColumn>
		<asp:ButtonColumn Visible="False" DataTextField="FullName" HeaderText="Name" CommandName="Select"></asp:ButtonColumn>
		<asp:BoundColumn DataField="Surname" HeaderText="Surname"></asp:BoundColumn>
		<asp:BoundColumn DataField="FirstName" HeaderText="First Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="Competitor" HeaderText="Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="BirthDate" HeaderText="Birth Date"></asp:BoundColumn>
		<asp:BoundColumn DataField="Gender" HeaderText="Gender"></asp:BoundColumn>
		<asp:BoundColumn DataField="Category" HeaderText="Cat."></asp:BoundColumn>
        <asp:BoundColumn DataField="EmitNumber" HeaderText="Emit No."></asp:BoundColumn>
		<asp:BoundColumn DataField="Telephone1" HeaderText="Tel. 1"></asp:BoundColumn>
		<asp:BoundColumn DataField="Telephone2" HeaderText="Tel. 2"></asp:BoundColumn>
	</Columns>
	<PagerStyle NextPageText="&gt;" Font-Size="10pt" PrevPageText="&lt;" Position="TopAndBottom"
		Mode="NumericPages"></PagerStyle>
</asp:datagrid>
