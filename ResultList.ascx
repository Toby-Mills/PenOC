<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ResultList.ascx.vb" Inherits="PenOC.ResultList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:datagrid id="grdResultList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
	Width="100%" GridLines="None" CssClass="DataGrid">
	<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
	<ItemStyle CssClass="DataGridItem"></ItemStyle>
	<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
	<Columns>
		<asp:BoundColumn Visible="False" DataField="idEvent" ReadOnly="True"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="idCourse" ReadOnly="True"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="idCompetitor" ReadOnly="True"></asp:BoundColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Edit">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdEdit" runat="server" ImageUrl="images/Edit.bmp" OnClick="cmdEdit_OnClick"></asp:ImageButton>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:ImageButton id="cmdUpdate" runat="server" ImageUrl="images/Update.bmp" OnClick="cmdUpdate_OnClick"></asp:ImageButton>
				<asp:ImageButton id="cmdCancel" runat="server" ImageUrl="images/Cancel.bmp" OnClick="cmdCancel_OnClick"></asp:ImageButton>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" HeaderText="Del.">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:ImageButton id="cmdDelete" runat="server" ImageUrl="images/Delete.bmp" OnClick="cmdDelete_OnClick"></asp:ImageButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Date" HeaderText="Date">
			<ItemStyle Wrap="False"></ItemStyle>
		</asp:BoundColumn>
		<asp:ButtonColumn DataTextField="Name" HeaderText="Event" CommandName="event"></asp:ButtonColumn>
		<asp:BoundColumn DataField="Venue" HeaderText="Venue"></asp:BoundColumn>
		<asp:BoundColumn DataField="Course" HeaderText="Course"></asp:BoundColumn>
		<asp:BoundColumn DataField="Length" HeaderText="Length (m)"></asp:BoundColumn>
		<asp:BoundColumn DataField="Climb" HeaderText="Climb (m)"></asp:BoundColumn>
		<asp:BoundColumn DataField="Controls" HeaderText="Controls"></asp:BoundColumn>
		<asp:BoundColumn DataField="Technical" HeaderText="Technicality"></asp:BoundColumn>
		<asp:BoundColumn DataField="Log" HeaderText="Log"></asp:BoundColumn>
		<asp:BoundColumn DataField="Position" HeaderText="Pos."></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Competitor">
			<ItemTemplate>
				<asp:Label id=lblCompetitor runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Competitor") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:DropDownList id=cmbCompetitor runat="server" DataTextField="Competitor" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"idCompetitor") %>' DataSource="<%# CompetitorTable() %>" DataValueField="idCompetitor">
				</asp:DropDownList>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Category">
			<ItemTemplate>
				<asp:Label id=lblCategory runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:DropDownList id=cmbCategory runat="server" DataTextField="Text" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"idCategory") %>' DataSource="<%# Categories %>" DataValueField="Value">
				</asp:DropDownList>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Club">
			<ItemTemplate>
				<asp:Label id=lblClub runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Club") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:DropDownList id=cmbClub runat="server" DataTextField="Text" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"idClub") %>' DataSource="<%# Clubs %>" DataValueField="Value">
				</asp:DropDownList>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="strRaceNumber" HeaderText="Race Number" SortExpression="strRaceNumber"></asp:BoundColumn>
		<asp:BoundColumn DataField="Time" HeaderText="Time"></asp:BoundColumn>
		<asp:BoundColumn DataField="Points" HeaderText="Points"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="DSQ">
			<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
			<ItemStyle HorizontalAlign="Center"></ItemStyle>
			<ItemTemplate>
				<asp:CheckBox id=CheckBox1 runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Disqualified") %>' Enabled="False">
				</asp:CheckBox>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:CheckBox id=CheckBox2 runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Disqualified") %>'>
				</asp:CheckBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Comment" HeaderText="Comment"></asp:BoundColumn>
	</Columns>
	<PagerStyle Font-Size="10pt" Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
