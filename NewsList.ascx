<%@ Control Language="vb" AutoEventWireup="false" Codebehind="NewsList.ascx.vb" Inherits="PenOC.NewsList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:repeater id="RepeaterNews" runat="server">
	<ItemTemplate>

<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" class="ItemBackground"
	border="0">
	<TR>
		<TD width="100" style="HEIGHT: 31px">
			<asp:label id="lblDate" runat="server" Font-Bold="True" CssClass="text_10">
				<%# DataBinder.Eval(Container.DataItem, "Date") %>
			</asp:label></TD>
		<TD style="HEIGHT: 31px">
			<asp:linkbutton id=cmdTitle runat="server" Font-Bold="True" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idNews") %>' CommandName="News" CssClass="text_10"><%# DataBinder.Eval(Container.DataItem, "Title") %>: 
								</asp:linkbutton></TD>
	</TR>
	<TR>
		<TD width="100" valign="top">
			<asp:ImageButton id="cmdEdit" runat="server" ImageUrl="images/Edit.bmp" Visible="False" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idNews") %>' CommandName="Edit" ToolTip="Edit News Item">
			</asp:ImageButton>
			<asp:ImageButton id="cmdDelete" runat="server" CommandName="Delete" ImageUrl="images/Delete.bmp" ToolTip="Delete News Item" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idNews") %>' Visible="False">
			</asp:ImageButton></TD>
		<TD>
			<asp:label id="lblNews" runat="server" CssClass="text_10">
				<%# DataBinder.Eval(Container.DataItem, "News") %>
			</asp:label></TD>
	</TR>
</TABLE>
<br>
	</ItemTemplate>
</asp:repeater>