<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CourseList.ascx.vb" Inherits="PenOC.CourseList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:DataGrid id="grdCourse" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1"
	GridLines="None" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle cssclass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idEvent"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="idCourse"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" runat="server" ImageUrl="images/Delete.bmp" OnClick="cmdDelete_OnClick"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Date" HeaderText="Date"></asp:BoundColumn>
		<asp:BoundColumn DataField="EventName" HeaderText="Event"></asp:BoundColumn>
		<asp:BoundColumn DataField="Venue" HeaderText="Venue"></asp:BoundColumn>
		<asp:BoundColumn DataField="CourseName" HeaderText="Name"></asp:BoundColumn>
		<asp:ButtonColumn Visible="False" DataTextField="CourseName" HeaderText="Name" CommandName="Select"></asp:ButtonColumn>
		<asp:BoundColumn DataField="Length" HeaderText="Length"></asp:BoundColumn>
		<asp:BoundColumn DataField="Climb" HeaderText="Climb"></asp:BoundColumn>
		<asp:BoundColumn DataField="Controls" HeaderText="Controls"></asp:BoundColumn>
		<asp:BoundColumn DataField="Technical" HeaderText="Technical"></asp:BoundColumn>
		<asp:BoundColumn DataField="Competitors" HeaderText="Competitors"></asp:BoundColumn>
		<asp:BoundColumn DataField="Winner" HeaderText="Winner"></asp:BoundColumn>
		<asp:BoundColumn DataField="Winning Time" HeaderText="Win Time"></asp:BoundColumn>
		<asp:BoundColumn DataField="Log" HeaderText="Log"></asp:BoundColumn>
        <asp:HyperLinkColumn DataNavigateUrlField="SplitsURL" HeaderText="Splits" Target="_blank"
            Text="splits"></asp:HyperLinkColumn>
	</Columns>
	<PagerStyle Font-Size="10pt"></PagerStyle>
</asp:DataGrid>
