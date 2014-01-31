<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ThreadList.ascx.vb" Inherits="PenOC.ThreadList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="grdThread" runat="server" Width="100%" AutoGenerateColumns="False">
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idThread"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="intForum"></asp:BoundColumn>
		<asp:BoundColumn DataField="strForumName" HeaderText="Forum"></asp:BoundColumn>
		<asp:ButtonColumn DataTextField="strThread" HeaderText="Thread" CommandName="select"></asp:ButtonColumn>
	</Columns>
</asp:DataGrid>
