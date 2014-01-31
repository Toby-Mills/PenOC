<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DownloadList.ascx.vb" Inherits="PenOC.DownloadList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<P><asp:repeater id="rptDownload" runat="server">
		<ItemTemplate>

	<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="100%" border="0">
		<TR>
			<TD colSpan="2">
				<asp:label id="lblTitle" runat="server" CssClass='CssClass="text_10"' Font-Bold="True">
					<%# DataBinder.Eval(Container.DataItem, "Title") %>
				</asp:label></TD>
			<TD colSpan="1" align="right">
				<asp:ImageButton id="btnEditDownload" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDownload") %>' ImageUrl="images/Edit.bmp" CommandName="Edit" ToolTip="Edit Download">
				</asp:ImageButton>
				<asp:ImageButton id="btnDeleteDownload" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "idDownload") %>' ImageUrl="images/Delete.bmp" CommandName="Delete" ToolTip="Delete Download">
				</asp:ImageButton></TD>
				</TR>
				</table>
	<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
		<TR>
			<TD width="75" valign="top"><A title=Download href='file.aspx?idFile=<%# DataBinder.Eval(Container.DataItem, "intFile") %>' target=_blank >
					<asp:image id="imgDownload" runat="server" ImageUrl="images/download.gif" AlternateText="Download"
						style="FILTER:progid:DXImagetransform.Microsoft.chroma(color=white)"></asp:image></A></TD>
			<TD colSpan="2" align=left width=100%>
				<asp:label id="lblDescription" runat="server" CssClass='CssClass="text_10"'>
					<%# DataBinder.Eval(Container.DataItem, "Description") %>
				</asp:label></TD>
		</TR>
	</TABLE>
		</ItemTemplate>
		<SeparatorTemplate>
			<HR width="100%" SIZE="1">
		</SeparatorTemplate>
	</asp:repeater></P>
