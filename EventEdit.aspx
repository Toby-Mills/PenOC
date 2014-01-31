<%@ Register TagPrefix="uc1" TagName="CourseList" Src="CourseList.ascx" %>
<%@ Page Language="vb"  ValidateRequest="false" AutoEventWireup="false" Codebehind="EventEdit.aspx.vb" Inherits="PenOC.EventEdit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EventEdit</title>
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
			<TABLE id="Table1" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="150" bgColor="#006699"><asp:label id="Label12" runat="server" CssClass="SectionHeader">Event:</asp:label></TD>
					<TD align="left" bgColor="#006699"><asp:textbox id="txtID" runat="server" Width="50px" Height="18px" ReadOnly="True"></asp:textbox></TD>
					<TD align="right" bgColor="#006699" colSpan="2"><asp:button id="cmdEdit" runat="server" Text="edit" Width="50px" CssClass="Button"></asp:button>&nbsp;
						<asp:button id="cmdSave" runat="server" Text="save" Width="50px" CssClass="Button"></asp:button>&nbsp;
						<asp:button id="cmdCancel" runat="server" Text="cancel" Width="50px" CssClass="Button"></asp:button></TD>
				</TR>
				<TR>
					<TD width="150"><asp:label id="Label8" runat="server" CssClass="Text_10">Name:</asp:label></TD>
					<TD><asp:textbox id="txtEventName" runat="server" Font-Size="10pt" Width="300px"></asp:textbox></TD>
					<TD><asp:label id="Label2" runat="server" CssClass="Text_10">Date:</asp:label></TD>
					<TD><asp:textbox id="txtDate" runat="server" Font-Size="10pt" Width="100px"></asp:textbox><asp:imagebutton id="cmdCalEventDate" runat="server" ImageUrl="images/cal.gif"></asp:imagebutton></TD>
				</TR>
				<TR>
					<TD width="150"><asp:label id="Label3" runat="server" CssClass="Text_10">Venue:</asp:label></TD>
					<TD><asp:dropdownlist id="cmbVenue" runat="server" Font-Size="10pt" Width="300px"></asp:dropdownlist></TD>
					<TD><asp:label id="Label4" runat="server" CssClass="Text_10">Club:</asp:label></TD>
					<TD><asp:dropdownlist id="cmbClub" runat="server" Font-Size="10pt" Width="200px"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD width="150"><asp:label id="Label16" runat="server" CssClass="Text_10">Courses:</asp:label></TD>
					<TD vAlign="top" rowSpan="3"><asp:textbox id="txtCourses" runat="server" Width="300px" Height="70px" TextMode="MultiLine"></asp:textbox></TD>
					<TD><asp:label id="Label5" runat="server" CssClass="Text_10">Planner:</asp:label></TD>
					<TD vAlign="top" rowSpan="1">
						<asp:TextBox id="txtPlannerName" runat="server" ReadOnly="True"></asp:TextBox>
						<asp:ImageButton id="cmdPlannerSearch" runat="server" ImageUrl="images/person.bmp"></asp:ImageButton>
						<asp:TextBox id="txtPlannerID" runat="server" Width="24px"></asp:TextBox>
						<asp:LinkButton id="cmdClearPlanner" runat="server" CssClass="Text_10">clear</asp:LinkButton></TD>
				</TR>
				<TR>
					<TD width="150"></TD>
					<TD><asp:label id="Label6" runat="server" CssClass="Text_10">Controller:</asp:label></TD>
					<TD>
						<asp:TextBox id="txtControllerName" runat="server" ReadOnly="True"></asp:TextBox>
						<asp:ImageButton id="cmdControllerSearch" runat="server" ImageUrl="images/person.bmp"></asp:ImageButton>
						<asp:TextBox id="txtControllerID" runat="server" Width="24px"></asp:TextBox>
						<asp:LinkButton id="cmdClearController" runat="server" CssClass="Text_10">clear</asp:LinkButton></TD>
				</TR>
				<TR>
					<TD width="150"></TD>
					<TD><asp:label id="Label14" runat="server" CssClass="Text_10">Registration:</asp:label></TD>
					<TD><asp:textbox id="txtRegistration" runat="server" Width="300px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="150"><asp:label id="Label17" runat="server" CssClass="Text_10">Cost:</asp:label></TD>
					<TD rowSpan="3" vAlign="top"><asp:textbox id="txtCost" runat="server" Width="300px" Height="70px" TextMode="MultiLine"></asp:textbox></TD>
					<TD><asp:label id="Label13" runat="server" CssClass="Text_10">Starts:</asp:label></TD>
					<TD><asp:textbox id="txtStarts" runat="server" Width="300px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 19px" width="150"></TD>
					<TD style="HEIGHT: 19px"><asp:label id="Label15" runat="server" CssClass="Text_10">Courses Close:</asp:label></TD>
					<TD style="HEIGHT: 19px"><asp:textbox id="txtClose" runat="server" Width="300px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="150"></TD>
					<TD><asp:label id="Label7" runat="server" CssClass="Text_10">Max Log Points:</asp:label></TD>
					<TD><asp:textbox id="txtMaxLogPoints" runat="server" Font-Size="10pt" Width="100px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD width="150" vAlign="top">
						<asp:Label id="lblDirections" runat="server" CssClass="text_10">Directions</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtDirections" runat="server" Height="70px" Width="300px" TextMode="MultiLine"></asp:TextBox></TD>
					<TD colSpan="2" vAlign="top">
						<asp:CheckBoxList id="cblEventType" runat="server" RepeatColumns="3" CssClass="Text_10"></asp:CheckBoxList></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150" rowSpan="4"><asp:label id="Label19" runat="server" CssClass="Text_10">Report:</asp:label></TD>
					<TD rowSpan="4" vAlign="top"><asp:textbox id="txtReport" runat="server" Width="300px" Height="107px" TextMode="MultiLine"></asp:textbox></TD>
					<TD><asp:label id="Label18" runat="server" CssClass="Text_10">Special Note:</asp:label></TD>
					<TD><asp:textbox id="txtSpecialNote" runat="server" Font-Size="10pt" Width="300px"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150" style="HEIGHT: 4px"><asp:label id="Label9" runat="server" CssClass="Text_10">Notice URL:</asp:label></TD>
					<TD style="HEIGHT: 4px"><asp:textbox id="txtNotice" runat="server" Font-Size="10pt" Width="300px"></asp:textbox><asp:button id="cmdAutoNotice" runat="server" Text="auto" CausesValidation="False" CssClass="Button"></asp:button></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150"><asp:label id="Label10" runat="server" CssClass="Text_10">Results URL:</asp:label></TD>
					<TD><asp:textbox id="txtResults" runat="server" Font-Size="10pt" Width="300px"></asp:textbox><asp:button id="cmdAutoResults" runat="server" Text="auto" CssClass="Button"></asp:button></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150"><asp:label id="Label11" runat="server" CssClass="Text_10">Photos URL:</asp:label></TD>
					<TD><asp:textbox id="txtPhotos" runat="server" Font-Size="10pt" Width="300px"></asp:textbox><asp:button id="cmdAutoPhotos" runat="server" Text="auto" CssClass="Button"></asp:button></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150">
						<asp:Label id="Label20" runat="server" CssClass="text_10"> Notice:</asp:Label></TD>
					<TD colSpan="3">
						<asp:Label id="lblNoticePermaLink" runat="server" CssClass="text_10"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="150">
						<asp:Label id="Label21" runat="server" CssClass="text_10"> Results:</asp:Label></TD>
					<TD colSpan="3">
						<asp:Label id="lblResultsPermaLink" runat="server" CssClass="text_10"></asp:Label></TD>
				</TR>
			</TABLE>
			<BR>
			<TABLE id="Table2" style="BORDER-RIGHT: midnightblue 1pt solid; BORDER-TOP: midnightblue 1pt solid; BORDER-LEFT: midnightblue 1pt solid; BORDER-BOTTOM: midnightblue 1pt solid"
				cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="150" bgColor="#006699"><asp:label id="Label1" runat="server" CssClass="SectionHeader">Courses:</asp:label></TD>
					<TD align="right" bgColor="#006699"><asp:button id="cmdNewCourse" runat="server" Text="new course" CssClass="Button"></asp:button>&nbsp;
						<asp:Button id="cmdCalculateLogPoints" runat="server" CssClass="button" Text="calc. log points"></asp:Button>&nbsp;
						<asp:Button id="cmdImport" runat="server" CssClass="button" Text="import results" ToolTip="import courses &amp; results"></asp:Button></TD>
				</TR>
				<TR>
					<TD colSpan="2"><uc1:courselist id="CourseList" runat="server"></uc1:courselist></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
