<%@ Register TagPrefix="uc1" TagName="CourseList" Src="CourseList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogList" Src="LogList.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminLogs.aspx.vb" Inherits="PenOC.AdminLogs" EnableEventValidation="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminLogs</title>
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
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" class="ItemBackground">
				<TR class="SectionHeader">
					<TD>
						<asp:Label id="Label6" runat="server">Logs</asp:Label></TD>
				</TR>
				<TR>
					<TD>
						<asp:Panel id="pnlLogList" runat="server">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD></TD>
									<TD align="right">
										<asp:Button id="cmdNewLog" runat="server" CssClass="button" Text="new log"></asp:Button></TD>
								</TR>
								<TR>
									<TD colSpan="2">
										<uc1:LogList id="LogList" runat="server"></uc1:LogList></TD>
								</TR>
							</TABLE>
						</asp:Panel><asp:panel id="pnlEdit" runat="server">
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<asp:TextBox id="txtID" runat="server" Width="32px"></asp:TextBox></TD>
									<TD align="right">
										<asp:Button id="cmdSave" runat="server" CssClass="button" Text="save" Width="50px"></asp:Button>
										<asp:Button id="cmdCancel" runat="server" CssClass="button" Text="cancel"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label1" runat="server" CssClass="text_10">year:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtYear" runat="server" CssClass="textbox" Width="50px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label3" runat="server" CssClass="text_10">name:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 2px">
										<asp:Label id="Label2" runat="server" CssClass="text_10">disregard:</asp:Label></TD>
									<TD style="HEIGHT: 2px" colSpan="5">
										<asp:TextBox id="txtDisregard" runat="server" CssClass="textbox" Width="50px"></asp:TextBox>
										<asp:Label id="Label5" runat="server" CssClass="text_10">events</asp:Label></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 2px">
										<asp:Label id="Label4" runat="server" CssClass="text_10">courses:</asp:Label></TD>
									<TD style="HEIGHT: 2px" align="right" colSpan="5">
										<asp:TextBox id="txtAddEventID" runat="server" Width="50px" AutoPostBack="True"></asp:TextBox>
										<asp:Button id="cmdAddEvent" runat="server" CssClass="button" Text="add event"></asp:Button></TD>
								</TR>
							</TABLE>
							<uc1:CourseList id="CourseListLog" runat="server"></uc1:CourseList>
						</asp:panel></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
