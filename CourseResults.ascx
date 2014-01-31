<%@ Register TagPrefix="uc1" TagName="ResultList" Src="ResultList.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CourseResults.ascx.vb" Inherits="PenOC.CourseResults" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table2" class="ItemBackground" cellSpacing="0" cellPadding="0" width="100%"
	border="0">
	<TR>
		<TD>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="SectionHeader" colSpan="4">
						<asp:label id="Label1" runat="server" CssClass="SectionHeader">Course:</asp:label>
						<asp:label id="lblCourseName" runat="server" CssClass="SectionHeaderText"></asp:label></TD>
					<TD align="right" class="SectionHeader">
						<asp:label id="Label3" runat="server" CssClass="SectionHeader">Length:</asp:label>
						<asp:label id="lblLength" runat="server" CssClass="SectionHeaderText"></asp:label>
						<asp:label id="Label4" runat="server" CssClass="SectionHeader">Climb:</asp:label>
						<asp:label id="lblClimb" runat="server" CssClass="SectionHeaderText"></asp:label>
						<asp:label id="lblTechnical" runat="server" CssClass="SectionHeaderText"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<asp:Label id="lblCourseSplits" CssClass="text_10" runat="server">Course Splits:</asp:Label>
						<asp:HyperLink id="lnkSplits" runat="server" Target="_blank">click here</asp:HyperLink></TD>
					<TD align="right"></TD>
				</TR>
				<TR>
					<TD colSpan="5">
						<uc1:ResultList id="ResultList" runat="server"></uc1:ResultList></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
