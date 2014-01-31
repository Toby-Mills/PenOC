<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MapTest.aspx.vb" Inherits="PenOC.MapTest"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>MapTest</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
        <script src="http://maps.google.com/maps?file=api&v=1&key=ABQIAAAAbvNN-32RphjOkMQVJ0j0YhT2yXp_ZAY8_ufC3CFXhHIE1NvwkxRKJt81PFbRzLPsxpuULEoqKI8i_A" type="text/javascript"></script>
  </head>

  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">
    <div id="map" style="width: 500px; height: 400px"></div>
    <script type="text/javascript">
    //<![CDATA[
    
    var map = new GMap(document.getElementById("map"));
    map.addControl(new GSmallMapControl());
    map.centerAndZoom(new GPoint(-122.1419, 37.4419), 4);
    
    //]]>
    </script>

    </form>

  </body>
</html>
