<%@ Control Language="vb" AutoEventWireup="false" Codebehind="LogList.ascx.vb" Inherits="PenOC.LogList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="grdLog" Width="100%" AutoGenerateColumns="False" runat="server" CellPadding="0"
	BorderWidth="0px" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle CssClass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idLog"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Del">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="ImageButton1" runat="server" onclick="cmdDelete_OnClick" ImageUrl="images/Delete.bmp"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:ButtonColumn DataTextField="Description" HeaderText="Log" CommandName="log"></asp:ButtonColumn>
		<asp:BoundColumn DataField="intYear" HeaderText="Year"></asp:BoundColumn>
		<asp:BoundColumn DataField="strLog" HeaderText="Name"></asp:BoundColumn>
		<asp:BoundColumn DataField="Events" HeaderText="Events"></asp:BoundColumn>
		<asp:BoundColumn DataField="intDisregardWorst" HeaderText="Disregard Events"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Current">
			<ItemTemplate>
				<asp:CheckBox id=chkCurrent runat="server" AutoPostBack=True Checked='<%# DataBinder.Eval(Container.DataItem, "blnCurrent") %>' OnCheckedChanged="chkCurrent_OnCheckChanged">
				</asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
