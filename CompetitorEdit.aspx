<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompetitorEdit.aspx.vb" Inherits="PenOC.CompetitorEdit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CompetitorEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body onbeforeunload="doHourglass()">
		<form id="Form1" method="post" runat="server">
            <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
			<asp:panel id="pnlCompetitorDetails" runat="server">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD bgColor="#006699">
							<asp:label id="Label1" runat="server" Font-Bold="True" CssClass="SectionHeader">Competitor:</asp:label>
							<asp:textbox id="txtID" runat="server" Width="55px"></asp:textbox></TD>
						<TD align="right" bgColor="#006699"></TD>
						<TD align="right" bgColor="#006699" colSpan="2">
							<asp:button id="cmdEdit" runat="server" CssClass="Button" Width="50px" Text="edit"></asp:button>&nbsp;
							<asp:button id="cmdSave" tabIndex="9" runat="server" CssClass="Button" Width="50px" Text="save"></asp:button>&nbsp;
							<asp:button id="cmdCancel" tabIndex="10" runat="server" CssClass="Button" Width="50px" Text="cancel"></asp:button></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label11" runat="server" CssClass="Text_10">Gender:</asp:Label></TD>
						<TD colSpan="3">
							<asp:RadioButtonList id="rblGender" runat="server" CssClass="Text_10" RepeatDirection="Horizontal"></asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label3" runat="server" CssClass="Text_10">Surname / Group Name:</asp:label></TD>
						<TD>
							<asp:textbox id="txtSurname" tabIndex="1" runat="server" Width="300px"></asp:textbox></TD>
						<TD>
							<asp:label id="Label4" runat="server" CssClass="Text_10">Birth Date:</asp:label><BR>
							<asp:Label id="lblDateFormat" runat="server" Font-Size="XX-Small"></asp:Label></TD>
						<TD>
							<asp:textbox id="txtBirthDate" tabIndex="3" runat="server" Width="200px"></asp:textbox>
							<asp:imagebutton id="cmdCalBirthDate" tabIndex="4" runat="server" ImageUrl="images/cal.gif"></asp:imagebutton></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label2" runat="server" CssClass="Text_10">First Name:</asp:label></TD>
						<TD>
							<asp:textbox id="txtFirstName" tabIndex="2" runat="server" Width="300px"></asp:textbox></TD>
						<TD>
							<asp:label id="Label5" runat="server" CssClass="Text_10">Category:</asp:label></TD>
						<TD>
							<asp:dropdownlist id="cmbCategory" tabIndex="5" runat="server" Width="200px"></asp:dropdownlist></TD>
					</TR>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" CssClass="Text_10" Text="Emit No."></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtEmitNumber" runat="server" Width="200px"></asp:TextBox></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
					<TR>
						<TD>
							<asp:label id="Label6" runat="server" CssClass="Text_10">Telephone 1:</asp:label></TD>
						<TD>
							<asp:textbox id="txtTelephone1" tabIndex="6" runat="server" Width="200px"></asp:textbox></TD>
						<TD>
							<asp:label id="Label7" runat="server" CssClass="Text_10">Telephone 2:</asp:label></TD>
						<TD>
							<asp:textbox id="txtTelephone2" tabIndex="7" runat="server" Width="200px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD>
							<asp:label id="Label8" runat="server" CssClass="Text_10">E-Mail:</asp:label></TD>
						<TD colSpan="3">
							<asp:textbox id="txtEmail" tabIndex="8" runat="server" Width="500px"></asp:textbox></TD>
					</TR>
				</TABLE>
			</asp:panel><br>
			<asp:panel id="pnlSimilarCompetitors" runat="server" Visible="False">
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3">
							<P>
								<asp:Label id="Label9" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red">The following Competitors have similar names to the name you entered.</asp:Label></P>
						</TD>
					</TR>
					<TR>
						<TD colSpan="3">
							<P>
								<asp:Label id="Label10" runat="server" CssClass="Text_10">Are you sure you want to add this new Competitor?</asp:Label></P>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="3">
							<asp:Button id="cmdAddNew" runat="server" CssClass="Button" Width="50px" Text="add" Font-Size="10pt"></asp:Button>&nbsp;
							<asp:Button id="cmdCancelNew" runat="server" CssClass="Button" Width="50px" Text="cancel" Font-Size="10pt"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="3">
							<uc1:CompetitorList id="CompetitorListSimilar" runat="server"></uc1:CompetitorList></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:TextBox id="txtReturnIDControl" runat="server"></asp:TextBox></form>
	</body>
</HTML>
