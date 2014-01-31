<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Lookup_Club.aspx.vb" Inherits="PenOC.Lookup_Club"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Lookup_Club</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link type="text/css" rel="stylesheet" href="styles.css">
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
	<body  onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="pnlNew" runat="server" CssClass="ItemBackground">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="SectionHeader">
							<asp:Label id="Label3" runat="server" CssClass="SectionHeader">Add, Edit & Delete Clubs</asp:Label></TD>
						<TD class="SectionHeader" align="right"></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label4" runat="server" CssClass="text_10" Font-Bold="True">New Club:</asp:Label></TD>
						<TD align="right"></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" CssClass="text_10">Full Name:</asp:Label>
							<asp:TextBox id="txtFullName" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
							<asp:Label id="Label2" runat="server" CssClass="text_10">Short Name:</asp:Label>
							<asp:TextBox id="txtShortName" runat="server" CssClass="textbox"></asp:TextBox></TD>
						<TD align="right">
							<asp:Button id="cmdAdd" runat="server" CssClass="button" Text="add"></asp:Button></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label5" runat="server" CssClass="text_10" Font-Bold="True">Existing Clubs:</asp:Label></TD>
						<TD align="right"></TD>
					</TR>
				</TABLE>
				<asp:datagrid id="grdClub" runat="server" AutoGenerateColumns="False" CssClass="DataGrid" GridLines="None"
					CellPadding="1" Width="100%">
					<AlternatingItemStyle CssClass="DataGridAlternatingItem"></AlternatingItemStyle>
					<ItemStyle CssClass="DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Edit">
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
						<asp:TemplateColumn HeaderText="Del.">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:ImageButton id="cmdDelete" runat="server" ImageUrl="images/Delete.bmp" OnClick="cmdDelete_OnClick"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="idClub" ReadOnly="True" HeaderText="ID"></asp:BoundColumn>
						<asp:BoundColumn DataField="strFullName" HeaderText="Name"></asp:BoundColumn>
						<asp:BoundColumn DataField="strShortName" HeaderText="Short Name"></asp:BoundColumn>
					</Columns>
				</asp:datagrid>
			</asp:Panel>
		</form>
	</body>
</HTML>
