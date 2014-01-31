<%@ Control Language="vb" AutoEventWireup="false" Codebehind="FileList.ascx.vb" Inherits="PenOC.FileList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="grdFile" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
	CellPadding="1" GridLines="None" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle CssClass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idFile" HeaderText="ID"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" onclick="cmdDelete_OnClick" runat="server" ImageUrl="images/Delete.bmp"
					ToolTip="Delete File"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Update">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="Imagebutton1" onclick="cmdUpdate_OnClick" runat="server" ImageUrl="images/Update.bmp"
					ToolTip="Update File"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn Visible="False" DataField="strFileName" HeaderText="File"></asp:BoundColumn>
		<asp:ButtonColumn DataTextField="strFileName" HeaderText="File" CommandName="Select"></asp:ButtonColumn>
		<asp:BoundColumn DataField="strDescription" HeaderText="Description"></asp:BoundColumn>
	</Columns>
</asp:datagrid>
