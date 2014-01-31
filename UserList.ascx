<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserList.ascx.vb" Inherits="PenOC.UserList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="grdUser" BorderWidth=0 CellPadding="0" AllowPaging="True" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle cssclass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
		<Columns>
		<asp:BoundColumn Visible="False" DataField="idCompetitor"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" onclick="cmdDelete_OnClick" runat="server" ImageUrl="images/Delete.bmp"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Enabled">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:CheckBox id=CheckBox1 runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"Enabled") %>' Enabled="False">
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="FullName" HeaderText="Name"></asp:BoundColumn>
		<asp:ButtonColumn Visible="False" DataTextField="FullName" HeaderText="Name" CommandName="Select"></asp:ButtonColumn>
		<asp:BoundColumn DataField="UserName" HeaderText="User Name"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Admin">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:CheckBox id=CheckBox2 runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"Administrator") %>' Enabled="False">
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="&gt;" Font-Size="10pt" PrevPageText="&lt;" Position="TopAndBottom"
		Mode="NumericPages"></PagerStyle>
</asp:datagrid>
