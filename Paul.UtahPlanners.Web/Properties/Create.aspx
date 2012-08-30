<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Create.aspx.vb" Inherits="admin_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="../scripts/uploadify/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <link href="../scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<!-- ASP SQL DATASOURCES -->
<asp:SqlDataSource ID="PropertyTypes" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [PropertyTypes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="StreetTypes" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [StreetTypes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SocioEcon" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [SocioEconCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="StreetSaftey" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [StreetSafteyCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="BuildingEnclosure" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [EnclosureCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="CommonAreas" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [CommonCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="StreetConn" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [StreetconnCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="StreetWalk" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [StreetwalkCodes]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="NeighborhoodCond" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
    SelectCommand="SELECT [id], [description] FROM [NeighborhoodCodes]">
</asp:SqlDataSource>
<!-- ASP SQL DATASOURCES -->    

<h2>Create New Property</h2>
<br />
<h3>Address:</h3>
<asp:Label ID="lblStreet1" runat="server" CssClass="newproperty">Street1:</asp:Label>
<asp:TextBox ID="txtStreet1" runat="server" CssClass="newwidth"></asp:TextBox><br />

<asp:Label ID="lblStreet2" runat="server" CssClass="newproperty">Street2:</asp:Label>
<asp:TextBox ID="txtStreet2" runat="server" CssClass="newwidth"></asp:TextBox><br />

<asp:Label ID="lblCity" runat="server" CssClass="newproperty">City:</asp:Label>
<asp:TextBox ID="txtCity" runat="server" CssClass="newwidth"></asp:TextBox><br />

<asp:Label ID="lblState" runat="server" CssClass="newproperty">State:</asp:Label>
<asp:TextBox ID="txtState" runat="server" CssClass="newwidth"></asp:TextBox><br />

<asp:Label ID="lblZip" runat="server" CssClass="newproperty">Zip:</asp:Label>
<asp:TextBox ID="txtZip" runat="server" CssClass="newwidth"></asp:TextBox><br />

<asp:Label ID="lblCountry" runat="server" CssClass="newproperty">Country:</asp:Label>
<asp:TextBox ID="txtCountry" runat="server" CssClass="newwidth"></asp:TextBox><br /><br />

<h3>General Attributes:</h3>
<asp:Label ID="lblDensity" runat="server" CssClass="newproperty">Density:</asp:Label>
<asp:TextBox ID="txtDensity" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txtDensity" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$" Display="Dynamic"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This is a required field" ControlToValidate="txtDensity" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lblArea" runat="server" CssClass="newproperty">Area:</asp:Label>
<asp:TextBox ID="txtArea" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txtArea" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This is a required field" ControlToValidate="txtArea" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lblUnits" runat="server" CssClass="newproperty">Units:</asp:Label>
<asp:TextBox ID="txtUnits" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txtUnits" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="This is a required field" ControlToValidate="txtUnits" Display="Dynamic"></asp:RequiredFieldValidator>
<br />

<asp:Label ID="lblYearBuilt" runat="server" CssClass="newproperty">Year Built:</asp:Label>
<asp:TextBox ID="txtYearBuilt" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txtYearBuilt" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This is a required field" ControlToValidate="txtYearBuilt" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lblWalkscore" runat="server" CssClass="newproperty">Walkscore:</asp:Label>
<asp:TextBox ID="txtWalkscore" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txtWalkscore" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="This is a required field" ControlToValidate="txtWalkscore" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lbl250sf" runat="server" CssClass="newproperty">250 Single Fam:</asp:Label>
<asp:TextBox ID="txt250sf" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txt250sf" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="This is a required field" ControlToValidate="txt250sf" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lbl250apts" runat="server" CssClass="newproperty">250 Apartments:</asp:Label>
<asp:TextBox ID="txt250apts" runat="server" CssClass="newwidth"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Please enter in a numeric value" ControlToValidate="txt250apts" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="This is a required field" ControlToValidate="txt250apts" Display="Dynamic"></asp:RequiredFieldValidator><br />

<asp:Label ID="lblPropertyType" runat="server" CssClass="newproperty">Property Type:</asp:Label>
<asp:DropDownList ID="drpProptertyType" runat="server" DataSourceID="PropertyTypes" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblStreetType" runat="server" CssClass="newproperty">Street Type:</asp:Label>
<asp:DropDownList ID="drpStreetType" runat="server" DataSourceID="StreetTypes" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblSocioEcon" runat="server" CssClass="newproperty">Socio-Econ:</asp:Label>
<asp:DropDownList ID="drpSocioEcon" runat="server" DataSourceID="SocioEcon" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblStreetSaftey" runat="server" CssClass="newproperty">Street Saftey:</asp:Label>
<asp:DropDownList ID="drpStreetSaftey" runat="server" DataSourceID="StreetSaftey" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblBuildingEnclosure" runat="server" CssClass="newproperty">Building Enclosure:</asp:Label>
<asp:DropDownList ID="drpBuildingEnclosure" runat="server" 
CssClass="newwidth" DataSourceID="BuildingEnclosure" DataTextField="description" 
DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblCommonAreas" runat="server" CssClass="newproperty">Common Areas:</asp:Label>
<asp:DropDownList ID="drpCommonAreas" runat="server" DataSourceID="CommonAreas" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblStreetConn" runat="server" CssClass="newproperty">Street Connectivity:</asp:Label>
<asp:DropDownList ID="drpStreetConn" runat="server" DataSourceID="StreetConn" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />
        
<asp:Label ID="lblStreetWalk" runat="server" CssClass="newproperty">Street Walkability:</asp:Label>
<asp:DropDownList ID="drpStreetWalk" runat="server" DataSourceID="StreetWalk" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblNeighCond" runat="server" CssClass="newproperty">Neighborhood Cond:</asp:Label>
<asp:DropDownList ID="drpNeighCond" runat="server" DataSourceID="NeighborhoodCond" 
CssClass="newwidth" DataTextField="description" DataValueField="id"></asp:DropDownList><br />

<asp:Label ID="lblNotes" runat="server" CssClass="newproperty">Notes:</asp:Label>
<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br /><br />

<h3>Hidden Attributes:</h3>
<asp:Label ID="lblAdminNotes" runat="server" CssClass="newproperty">Admin Notes:</asp:Label>
<asp:TextBox ID="txtAdminNotes" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br />

<asp:Label ID="lblWalkscoreNotes" runat="server" CssClass="newproperty">Walkscore Notes:</asp:Label>
<asp:TextBox ID="txtWalkscoreNotes" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br />

<asp:Label ID="lblNotFinished" runat="server" CssClass="newproperty">Not Finished:</asp:Label>
<asp:TextBox ID="txtNotFinished" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br /><br />

<h3>Pictures:</h3><br />
<input type="checkbox" id="checkbox1" /><label for="checkbox1">Primary Image</label><br />
<input type="checkbox" id="checkbox2" /><label for="checkbox2">Secondary Image</label><br />
<input type="checkbox" id="checkbox3" /><label for="checkbox3">Front Page</label><br />
    

<!-- File Upload Control -->
    <input id="fileInput" name="fileInput" type="file" />
    <script type="text/javascript">
        // <![CDATA[

        $(document).ready(function() {

            var mainPicture = 0;
            var secondaryPicture = 0;
            var frontPage = 0;

            var auth = "<%=IIf(Request.Cookies(FormsAuthentication.FormsCookieName) Is Nothing, "", Request.Cookies(FormsAuthentication.FormsCookieName).Value)%>";
            var ASPSESSID = "<%=Session.SessionID%>";

            $("#checkbox1").click(function() {
                mainPicture = (mainPicture == 1) ? 0 : 1;
                $("#fileInput").uploadifySettings('scriptData',
                    { 'mainPicture': mainPicture, 'secondaryPicture': secondaryPicture, 'frontPage': frontPage });
            });
            $("#checkbox2").click(function() {
                secondaryPicture = (secondaryPicture == 1) ? 0 : 1;
                $("#fileInput").uploadifySettings('scriptData',
                { 'mainPicture': mainPicture, 'secondaryPicture': secondaryPicture, 'frontPage': frontPage });
            });
            $("#checkbox3").click(function() {
                frontPage = (frontPage == 1) ? 0 : 1;
                $("#fileInput").uploadifySettings('scriptData',
                { 'mainPicture': mainPicture, 'secondaryPicture': secondaryPicture, 'frontPage': frontPage });
            });


            $('#fileInput').uploadify({
                'uploader': '../scripts/uploadify/uploadify.swf',
                'script': '../scripts/uploadify/Upload.ashx',
                'cancelImg': '../scripts/uploadify/cancel.png',
                'auto': true,
                'folder': '../scripts/uploadify/',
                'scriptData': { ASPSESSID: ASPSESSID, AUTHID: auth }
            });

        });
        // ]]>
    </script>
<!-- End File Upload Control -->

<br />
<br />
<asp:Button ID="Button1" runat="server" Text="Create New Property" />
</asp:Content>

