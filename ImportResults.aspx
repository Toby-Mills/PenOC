<%@ Register TagPrefix="uc1" TagName="CourseResults" Src="CourseResults.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultList" Src="ResultList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImportResults.aspx.vb" Inherits="PenOC.ImportResults"%>
<%@ Register TagPrefix="uc1" TagName="CourseList" Src="CourseList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImportResults</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link id="lnkStylesheet" type="text/css" rel="stylesheet" runat="server">
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
	<body onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
			<P><asp:label id="Label4" runat="server">Event:</asp:label><asp:textbox id="txtEvent" runat="server" Width="224px" ReadOnly="True"></asp:textbox><asp:textbox id="txtEventID" runat="server" DESIGNTIMEDRAGDROP="80"></asp:textbox><asp:linkbutton id="cmdEventSearch" runat="server">Select Event</asp:linkbutton></P>
			<asp:panel id="pnlUpload" runat="server">
				<P><INPUT id="fileUpload" style="WIDTH: 500px" type="file" size="20" runat="server"></P>
				<P>
					<asp:Button id="cmdUpload" runat="server" Text="upload" CssClass="button"></asp:Button></P>
			</asp:panel><asp:panel id="pnlNewCompetitor" runat="server">
				<P>
					<asp:Label id="Label1" runat="server" CssClass="text_10">The following competitors are not in the database:</asp:Label></P>
				<P>
					<asp:Label id="Label3" runat="server">Use 'suggest' to view possible matches, or 'add' to add them to the database</asp:Label></P>
				<asp:DataGrid id="grdNewCompetitor" runat="server" BorderWidth="1px" BorderColor="#EFF1FA" CellPadding="1"
					AutoGenerateColumns="False">
					<AlternatingItemStyle Font-Size="10pt" BackColor="#EBEBFA"></AlternatingItemStyle>
					<ItemStyle Font-Size="10pt" BackColor="Lavender"></ItemStyle>
					<HeaderStyle Font-Size="10pt" Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="idNewCompetitor"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="idCompetitor"></asp:BoundColumn>
						<asp:ButtonColumn Text="add" CommandName="add"></asp:ButtonColumn>
						<asp:BoundColumn DataField="strFirstName" HeaderText="First Name"></asp:BoundColumn>
						<asp:BoundColumn DataField="strSurname" HeaderText="Surname"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="idGender"></asp:BoundColumn>
						<asp:BoundColumn DataField="strGender" HeaderText="Gender"></asp:BoundColumn>
						<asp:ButtonColumn DataTextField="strBestMatch" HeaderText="Suggestion" CommandName="match"></asp:ButtonColumn>
						<asp:BoundColumn Visible="False" DataField="intBestMatch"></asp:BoundColumn>
						<asp:ButtonColumn DataTextField="blnSuggest" CommandName="suggest"></asp:ButtonColumn>
					</Columns>
				</asp:DataGrid>
				<asp:TextBox id="txtNewCompetitorID" runat="server"></asp:TextBox>
				<asp:TextBox id="txtCompetitorID" runat="server" AutoPostBack="True"></asp:TextBox>
			</asp:panel><asp:panel id="pnlPreview" runat="server">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<asp:Button id="cmdSave" runat="server" Text="save" CssClass="button"></asp:Button></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="text_10">Results Preview</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:CheckBox id="chkAutoResults" runat="server" Text="Make results visible through web page"
								CssClass="text_10" Checked="True"></asp:CheckBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:CheckBox id="chkOverwrite" runat="server" Text="Overwrite existing results" CssClass="text_10"
								Checked="True"></asp:CheckBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:CheckBox id="chkAutoCalculateLogPoints" runat="server" Text="Automatically calculate log points"
								CssClass="Text_10"></asp:CheckBox></TD>
					</TR>
				</TABLE>
				<uc1:CourseList id="CourseListPreview" runat="server"></uc1:CourseList>
				<uc1:ResultList id="ResultListPreview" runat="server"></uc1:ResultList>
			</asp:panel></form>
	</body>
</HTML>
