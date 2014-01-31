<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminUsers.aspx.vb" Inherits="PenOC.AdminUsers"%>
<%@ Register TagPrefix="uc1" TagName="UserList" Src="UserList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompetitorList" Src="CompetitorList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminUsers</title>
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
			<TABLE class="ItemBackground" id="Table3" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR class="SectionHeader">
					<TD><asp:label id="Label2" runat="server">Users</asp:label></TD>
				</TR>
				<TR>
					<TD><asp:panel id="pnlEdit" runat="server">
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD></TD>
									<TD align="right">
										<asp:Button id="cmdSave" runat="server" Width="50px" Text="save" CssClass="button"></asp:Button>
										<asp:Button id="cmdCancel" runat="server" Text="cancel" CssClass="button"></asp:Button></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2">
										<asp:Label id="lblMessage" runat="server" CssClass="text_10" ForeColor="Red" Font-Bold="True"></asp:Label></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label1" runat="server" CssClass="text_10">User:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtUser" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
										<asp:ImageButton id="cmdUserSearch" runat="server" ImageUrl="images/person.bmp"></asp:ImageButton>
										<asp:TextBox id="txtUserID" runat="server" Width="47px" CssClass="textbox"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label3" runat="server" CssClass="text_10">User name:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtUserName" runat="server" CssClass="textbox"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label4" runat="server" CssClass="text_10">Enabled:</asp:Label></TD>
									<TD>
										<asp:CheckBox id="chkEnabled" runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label5" runat="server" CssClass="text_10">Administrator:</asp:Label></TD>
									<TD>
										<asp:CheckBox id="chkAdministrator" runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD>
										<asp:CheckBox id="chkResetPassword" runat="server" Text="Reset Password:" CssClass="text_10"></asp:CheckBox></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label6" runat="server" CssClass="text_10">New password:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<asp:Label id="Label7" runat="server" CssClass="text_10">Confirm new password:</asp:Label></TD>
									<TD>
										<asp:TextBox id="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnlUserList" runat="server">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD></TD>
									<TD align="right">
										<asp:Button id="cmdNewUser" runat="server" Text="new user" CssClass="button"></asp:Button></TD>
								</TR>
								<TR>
									<TD colSpan="2">
										<uc1:userlist id="UserList" runat="server"></uc1:userlist></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
