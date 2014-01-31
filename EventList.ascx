<%@ Control Language="vb" AutoEventWireup="false" Codebehind="EventList.ascx.vb" Inherits="PenOC.EventList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:label id="lblEventCountLabel" Font-Bold="True" runat="server" CssClass="Text_10">Events:</asp:label><asp:label id="lblEventCount" runat="server" CssClass="Text_10"></asp:label>
<asp:datagrid id="grdEvent" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
	CellPadding="1" GridLines="None" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle CssClass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idEvent"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" onclick="cmdDelete_OnClick" runat="server" ToolTip="Delete Event"
					ImageUrl="images/Delete.bmp"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<ItemStyle HorizontalAlign="Right"></ItemStyle>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Date" HeaderText="Date">
			<ItemStyle Wrap="False"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="EventName" HeaderText="Name"></asp:BoundColumn>
		<asp:ButtonColumn Visible="False" DataTextField="EventName" HeaderText="Name" CommandName="Select"></asp:ButtonColumn>
		<asp:BoundColumn DataField="Venue" HeaderText="Venue"></asp:BoundColumn>
		<asp:BoundColumn DataField="Registration" HeaderText="Registration"></asp:BoundColumn>
		<asp:BoundColumn DataField="Starts" HeaderText="Starts"></asp:BoundColumn>
		<asp:BoundColumn DataField="Close" HeaderText="Close"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Photos">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdPhotos" onclick="cmdPhotos_OnClick" runat="server" ToolTip="Event Photos"
					ImageUrl="images/Photos.bmp"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="&gt;" Font-Size="10pt" PrevPageText="&lt;" Position="TopAndBottom"
		Mode="NumericPages"></PagerStyle>
</asp:datagrid>
