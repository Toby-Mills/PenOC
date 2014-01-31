<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WorkItems.aspx.vb" Inherits="PenOC.WorkItems"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WorkItems</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet">
		</link>
		<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-17068661-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="Panel1" runat="server">
				<asp:Label id="Label1" runat="server">Work Item</asp:Label>
				<asp:TextBox id="txtWorkItem" runat="server" Width="600px"></asp:TextBox>
				<asp:Button id="cmdAdd" runat="server" Text="add" CssClass="button"></asp:Button>
			</asp:Panel>
			<asp:DataGrid id="grdWorkItem" runat="server" Width="100%" CellPadding="1" GridLines="None" CssClass="DataGrid"
				AutoGenerateColumns="False">
				<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
				<ItemStyle CssClass="DataGridItem"></ItemStyle>
				<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="idWorkItem"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Del.">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton id="cmdDelete" onclick="cmdDelete_OnClick" runat="server" ImageUrl="images/Delete.bmp"
								ToolTip="Delete Event"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Resolved">
						<ItemTemplate>
							<asp:CheckBox id="chkResolved" runat="server" AutoPostBack="True" OnCheckedChanged="chkResolved_OnCheckChanged" Checked='<%# DataBinder.Eval(Container.DataItem, "blnResolved") %>'>
							</asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="strWorkItem" HeaderText="Work Item"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
