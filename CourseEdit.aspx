<%@ Register TagPrefix="uc1" TagName="EventBrief" Src="EventBrief.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultList" Src="ResultList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CourseEdit.aspx.vb" Inherits="PenOC.CourseEdit"%>
<%@ Register TagPrefix="uc1" TagName="CompetitorSelect" Src="CompetitorSelect.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CourseEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet"></link>
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
			<P><uc1:eventbrief id="EventBrief" runat="server"></uc1:eventbrief></P>
			<P>&nbsp;<asp:panel id="pnlCourseDetails" runat="server">
					<TABLE id="Table1" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
						cellSpacing="0" cellPadding="0" width="100%" bgColor="#d7f5ff" border="0">
						<TR>
							<TD bgColor="#006699">
								<asp:label id="Label1" runat="server" CssClass="SectionHeader">Course:</asp:label>
								<asp:textbox id="txtID" runat="server" Width="36px"></asp:textbox>
								<asp:textbox id="txtEventID" runat="server" Width="36px"></asp:textbox></TD>
							<TD bgColor="#006699"></TD>
							<TD bgColor="#006699"></TD>
							<TD align="right" bgColor="#006699">
								<asp:button id="cmdEdit" tabIndex="1" runat="server" CssClass="Button" Width="50px" Text="edit"></asp:button>&nbsp;
								<asp:button id="cmdSave" tabIndex="2" runat="server" CssClass="Button" Width="50px" Text="save"></asp:button>&nbsp;
								<asp:button id="cmdCancel" tabIndex="3" runat="server" CssClass="Button" Width="50px" Text="cancel"></asp:button></TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label2" runat="server" CssClass="Text_10">Name:</asp:label></TD>
							<TD>
								<asp:textbox id="txtCourseName" tabIndex="4" runat="server" Width="300px" Font-Size="10pt"></asp:textbox></TD>
							<TD>
								<asp:label id="Label5" runat="server" CssClass="Text_10">Length:</asp:label></TD>
							<TD>
								<asp:textbox id="txtLength" tabIndex="7" runat="server" Width="100px" Font-Size="10pt"></asp:textbox>
								<asp:label id="Label8" runat="server" CssClass="Text_10">(m)</asp:label></TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label3" runat="server" CssClass="Text_10">Controls:</asp:label></TD>
							<TD>
								<asp:textbox id="txtControls" tabIndex="5" runat="server" Width="100px" Font-Size="10pt"></asp:textbox></TD>
							<TD>
								<asp:label id="Label6" runat="server" CssClass="Text_10">Climb:</asp:label></TD>
							<TD>
								<asp:textbox id="txtClimb" tabIndex="8" runat="server" Width="100px" Font-Size="10pt"></asp:textbox>
								<asp:label id="Label9" runat="server" CssClass="Text_10">(m)</asp:label></TD>
						</TR>
						<TR>
							<TD>
								<asp:label id="Label4" runat="server" CssClass="Text_10">Technicality:</asp:label></TD>
							<TD>
								<asp:dropdownlist id="cmbTechnical" tabIndex="6" runat="server" Width="200px" Font-Size="10pt"></asp:dropdownlist></TD>
							<TD>
								<asp:label id="Label7" runat="server" CssClass="Text_10">Log:</asp:label></TD>
							<TD>
								<asp:dropdownlist id="cmbLog" tabIndex="9" runat="server" Width="200px" Font-Size="10pt"></asp:dropdownlist></TD>
						</TR>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" CssClass="Text_10">Splits URL:</asp:Label></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtSplitsURL" runat="server" Font-Size="10pt" TabIndex="4" Width="500px"></asp:TextBox></td>
                        </tr>
					</TABLE>
				</asp:panel></P>
			<TABLE id="Table2" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
				cellSpacing="0" cellPadding="0" width="100%" bgColor="#d7f5ff" border="0">
				<TR>
					<TD style="HEIGHT: 14px" bgColor="#006699"><asp:label id="Label10" runat="server" CssClass="SectionHeader">Results:</asp:label></TD>
					<TD style="HEIGHT: 14px" align="right" bgColor="#006699"><asp:button id="cmdStartEditResults" tabIndex="10" runat="server" CssClass="Button" Width="100px"
							Text="start editing"></asp:button>&nbsp;
						<asp:button id="cmdCalculatePoints" runat="server" CssClass="button" Text="calc. log points"></asp:button><asp:button id="cmdEndEditingResults" tabIndex="11" runat="server" CssClass="Button" Width="100px"
							Text="end editing"></asp:button></TD>
				</TR>
				<TR>
					<TD align="left" colSpan="2"><asp:panel id="pnlAddResult" runat="server">
							<uc1:CompetitorSelect id="CompetitorSelectResult" runat="server"></uc1:CompetitorSelect>
							<asp:Label id="Label13" runat="server" CssClass="Text_10">Position:</asp:Label>
							<asp:TextBox id="txtPosition" tabIndex="12" runat="server" Width="100px" Font-Size="10pt"></asp:TextBox>
							<asp:Label id="lblTime" runat="server" CssClass="Text_10">Time:</asp:Label>
							<asp:TextBox id="txtTime" tabIndex="13" runat="server" Width="100px" Font-Size="10pt"></asp:TextBox>
							<asp:Label id="Label11" runat="server" CssClass="Text_10">Category:</asp:Label>
							<asp:DropDownList id="cmbCategory" tabIndex="14" runat="server" Width="100px" Font-Size="10pt"></asp:DropDownList>
							<asp:Label id="Label15" runat="server" CssClass="Text_10" Font-Size="10pt">Points:</asp:Label>
							<asp:TextBox id="txtPoints" tabIndex="15" runat="server" Width="100px" Font-Size="10pt"></asp:TextBox>
							<asp:Label id="Label12" runat="server" CssClass="Text_10">Club:</asp:Label>
							<asp:DropDownList id="cmbClub" tabIndex="16" runat="server" Width="100px" Font-Size="10pt"></asp:DropDownList>
							&nbsp;Race Number:<asp:TextBox ID="txtRaceNumber" runat="server"></asp:TextBox>
							<BR>
							<asp:Label id="Label14" runat="server" CssClass="Text_10">Disqualified:</asp:Label>
							<asp:CheckBox id="chkDisqualified" tabIndex="17" runat="server" CssClass="Text_10"></asp:CheckBox>
							<asp:Label id="Label17" runat="server" CssClass="Text_10">Comment:</asp:Label>
							<asp:TextBox id="txtComment" tabIndex="18" runat="server" Width="500px" Font-Size="10pt"></asp:TextBox>
							<asp:Button id="cmdAddResult" tabIndex="19" runat="server" CssClass="Button" Width="50px" Text="add"></asp:Button>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<P><uc1:resultlist id="ResultListCourse" runat="server"></uc1:resultlist></P>
					</TD>
				</TR>
			</TABLE>
			<P></P>
		</form>
	</body>
</HTML>
