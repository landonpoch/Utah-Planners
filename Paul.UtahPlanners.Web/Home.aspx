﻿<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.2.min.js"></script>
<script src="scripts/fancybox/jquery.fancybox-1.3.1.pack.js" type="text/javascript"></script>
<link href="scripts/fancybox/jquery.fancybox-1.3.1.css" rel="stylesheet" type="text/css" />

<!-- jQuery fire fancybox plugin -->
<script type="text/javascript">
    $(document).ready(function() {

        /* This is basic - uses default settings */

        $("a#single_image").fancybox({
            'type': 'image'
        });

        /* Using custom settings */

        $("a#inline").fancybox({
            'hideOnContentClick': true
        });

        /* Apply fancybox to multiple items */

        $("a.group").fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'speedIn': 600,
            'speedOut': 200,
            'overlayShow': true,
            'type': 'image',
            'showNavArrows': true
        });

    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="title">
        <h1>Property #<%=prop.id%></h1>
        <h2><%  Response.Write(IIf(prop.Address.city Is Nothing, "Unknown City", prop.Address.city) & " - " & prop.PropertyType.description)%></h2>
    </div>
    <div id="top">
        <div class="leftpicture">
        <a class="group" rel="group1" href="Pic.aspx?pid=<%=prop.id %>&first=1">
            <img class="resize" alt="" src="Pic.aspx?pid=<%=prop.id %>&first=1" />
        </a>
        </div>
        <div class="right">
            <h3>Overall Score: <%=score%></h3>
            <br />
            <br />
            <label class="scoredetail" for="streetsafe">Street Saftey:</label><span id="streetsafe"><%=prop.StreetSafteyCode.description%></span><br />
            <label class="scoredetail" for="enclosure">Building Enclosure:</label><span id="enclosure"><%=prop.EnclosureCode.description%></span><br />
            <label class="scoredetail" for="common">Common Areas:</label><span id="common"><%=prop.CommonCode.description%></span><br />
            <label class="scoredetail" for="streetconn">Street Connectivity:</label><span id="streetconn"><%=prop.StreetconnCode.description%></span><br />
            <label class="scoredetail" for="streetwalk">Street Walkability:</label><span id="streetwalk"><%=prop.StreetwalkCode.description%></span><br />
            <label class="scoredetail" for="walkscore">Walkscore:</label><span id="walkscore"><%=prop.walkscore%></span><br />
            <label class="scoredetail" for="neigh">Neighborhood Condition:</label><span id="neigh"><%=prop.NeighborhoodCode.description%></span><br />
            <label class="scoredetail" for="tfsf">250 Single Family:</label><span id="tfsf"><%=prop.twoFiftySingleFam%></span><br />
            <label class="scoredetail" for="tfaps">250 Apartments:</label><span id="tfaps"><%=prop.twoFiftyApts%></span><br />
            <br /><br />
            <div class="center">
                <%  Dim x = 0
                    For Each picId In picIds
                        If x = 0 Then
                            Response.Write("<a class=""group"" rel=""group1"" href=""Pic.aspx?id=" & picId & """>Click to view all pictures</a><br />")
                        Else
                            Response.Write("<a class=""group"" rel=""group1"" href=""Pic.aspx?id=" & picId & """ />")
                        End If
                        x = x + 1
                    Next%>
                <a href="http://maps.google.com?q=<%=walkString %>&z=19" target="_blank">View in Google maps</a><br />
                <a href="http://www.walkscore.com/get-score.php?street=<%=walkString %>&go=Go" target="_blank">Walkscore index details</a>
            </div>
        </div>
    </div>
    <div id="bottom">
        <div class="left">
            <h3>Building Details</h3><br />
            <label class="builddetail" for="proptype">Property Type:</label><span id="proptype"><%=prop.PropertyType.description%></span><br />
            <label class="builddetail" for="addr">Address:</label><span id="addr"><%=prop.Address.street1%></span><br />
            <label class="builddetail" for="addr2">&nbsp;</label><span id="addr2"><%=prop.Address.city & ", " & prop.Address.state & " " & prop.Address.zip%></span><br />
            <label class="builddetail" for="density">Density:</label><span id="density"><%=prop.density%></span><br />
            <label class="builddetail" for="area">Area:</label><span id="area"><%=prop.area%></span><br />
            <label class="builddetail" for="units">Units:</label><span id="units"><%=prop.units%></span><br />
            <label class="builddetail" for="strType">Street Type:</label><span id="strType"><%=prop.StreetType.description%></span><br />
            <label class="builddetail" for="yearBuilt">Year Built:</label><span id="yearBuilt"><%=prop.yearBuilt%></span><br />
            <label class="builddetail" for="socioEcon">Socio Econ:</label><span id="socioEcon"><%=prop.SocioEconCode.description%></span><br />
            <br />
            <h3>Notes:</h3><br />
            <%=prop.notes%>
        </div>
        <div class="rightpicture">
            <a id="single_image" href="Pic.aspx?pid=<%=prop.id%>&second=1">
                <img class="resize" alt="" src="Pic.aspx?pid=<%=prop.id%>&second=1" />
            </a>
        </div>
    </div>
</asp:Content>

