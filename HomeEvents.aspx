<%@ Register TagPrefix="uc1" TagName="EventList" Src="EventList.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HomeEvents.aspx.vb" Inherits="PenOC.HomeEvents" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EventsRecent_Small</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link id="lnkStylesheet" runat="server" type="text/css" rel="stylesheet"></link>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-17068661-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</head>
<body onbeforeunload="doHourglass()">
    <form id="Form1" method="post" runat="server">
    <table id="tblLinks" cellspacing="0" cellpadding="1" width="100%" class="Itembackground">
        <tr>
            <td class="SectionHeader">
                <asp:Label ID="Label3" runat="server" CssClass="SectionHeader">Club Information</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Text_10">
                <br />
                <ul>
                    <li>To <strong>join the club</strong>, complete this <a href="https://docs.google.com/spreadsheet/viewform?formkey=dFoycklwc1lEX2lCN0gtOVFjYlpUS2c6MQ"
                        target="_blank">Membership Form</a></li>
                    <li>Our email list will keep you informed of event details and results as they become
                        available.<br />
                        To subscribe, send an email to: <a href="mailto:penoc-subscribe@yahoogroups.com">penoc-subscribe@yahoogroups.com</a></li>
                    <li>For an introduction to Orienteering, see this beginner's guide: <a href="file.aspx?idFile=57">
                        Getting Started with Orienteering</a></li>
                </ul>
            </td>
        </tr>
    </table>
    <br />
    <table id="Table1" cellspacing="0" cellpadding="1" width="100%" class="Itembackground">
        <tr>
            <td class="SectionHeader">
                <asp:Label ID="Label1" runat="server" CssClass="SectionHeader">Recent Events</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:EventList ID="EventListRecent" runat="server"></uc1:EventList>
            </td>
        </tr>
    </table>
    <br>
    <table id="Table2" cellspacing="0" cellpadding="1" width="100%" border="0" class="ItemBackground">
        <tr>
            <td class="SectionHeader">
                <asp:Label ID="Label2" runat="server" CssClass="SectionHeader">Upcoming Events</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:EventList ID="EventListUpcoming" runat="server"></uc1:EventList>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
