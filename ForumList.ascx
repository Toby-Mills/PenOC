<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ForumList.ascx.vb" Inherits="PenOC.ForumList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="grdForum" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle cssclass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idForum"></asp:BoundColumn>
		<asp:ButtonColumn DataTextField="strForumName" HeaderText="Forum" CommandName="select"></asp:ButtonColumn>
	</Columns>
</asp:DataGrid>
