<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.2.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {

            //Hide (Collapse) the toggle containers on load
            <% 
            if Not Request.UrlReferrer Is Nothing Then
                If Not Request.UrlReferrer.Equals(Request.Url) Then 
                    Response.Write("$("".toggle_container"").hide();") 
                End if
            else
                Response.Write("$("".toggle_container"").hide();") 
            end if
            %>

            //Switch the "Open" and "Close" state per click then slide up/down (depending on open/close state)
            $("h2.trigger").click(function() {
                $(this).toggleClass("active").next().slideToggle("slow");
            });

        });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:SqlDataSource ID="sqlPropertyType" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [description], [id] FROM [PropertyTypes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlStreetType" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [StreetTypes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlSocioEcon" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [SocioEconCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlStreetSafety" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [StreetSafteyCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlBuildingEnclosure" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [EnclosureCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlCommonAreas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [CommonCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlStreetConn" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [StreetconnCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlStreetWalk" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [StreetwalkCodes]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlNeighCondition" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        SelectCommand="SELECT [id], [description] FROM [NeighborhoodCodes]"></asp:SqlDataSource>
    
    <h2 class="trigger"><a href="#">Index Filter</a></h2>
    <div class="toggle_container">
    <div class="block">
    
    <div class="searchLeft">
        <h3 style="margin-left:50px;">Property Details:</h3>
        
        <asp:Label ID="Label1" runat="server" CssClass="searchLabel">Property Number:</asp:Label>
        <asp:TextBox ID="txtID" runat="server" CssClass="newwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label2" runat="server" CssClass="searchLabel">City:</asp:Label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="newwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label3" runat="server" CssClass="searchLabel">Property Type:</asp:Label>
        <asp:DropDownList ID="drpPropType" runat="server" CssClass="newwidth" 
            DataSourceID="sqlPropertyType" DataTextField="description"
            DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label4" runat="server" CssClass="searchLabel">Density Between:</asp:Label>
        <asp:TextBox ID="txtDensityLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtDensityHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label5" runat="server" CssClass="searchLabel">Area Between:</asp:Label>
        <asp:TextBox ID="txtAreaLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtAreaHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label6" runat="server" CssClass="searchLabel">Units Between:</asp:Label>
        <asp:TextBox ID="txtUnitsLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtUnitsHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label7" runat="server" CssClass="searchLabel">Street Type:</asp:Label>
        <asp:DropDownList ID="drpStreetType" runat="server" CssClass="newwidth" 
            DataSourceID="sqlStreetType" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label8" runat="server" CssClass="searchLabel">Year Built Between:</asp:Label>
        <asp:TextBox ID="txtYearBuiltLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtYearBuiltHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label9" runat="server" CssClass="searchLabel">Socio Econ:</asp:Label>
        <asp:DropDownList ID="drpSocioEcon" runat="server" CssClass="newwidth" 
            DataSourceID="sqlSocioEcon" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br /><br /><br />
    </div>
    
    <div class="searchRight">
        <h3>Rating Details:</h3>
        <asp:Label ID="Label10" runat="server" CssClass="searchLabel">Overall Score Between:</asp:Label> 
        <asp:TextBox ID="txtOverallLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtOverallHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label11" runat="server" CssClass="searchLabel">Street Safety:</asp:Label> 
        <asp:DropDownList ID="drpStreetSaftey" runat="server" CssClass="newwidth" 
            DataSourceID="sqlStreetSafety" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label12" runat="server" CssClass="searchLabel">Building Enclosure:</asp:Label> 
        <asp:DropDownList ID="drpBuildingEnclosure" runat="server" CssClass="newwidth" 
            DataSourceID="sqlBuildingEnclosure" DataTextField="description" 
            DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label13" runat="server" CssClass="searchLabel">Common Areas:</asp:Label> 
        <asp:DropDownList ID="drpCommonAreas" runat="server" CssClass="newwidth" 
            DataSourceID="sqlCommonAreas" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label14" runat="server" CssClass="searchLabel">Street Connectivity:</asp:Label> 
        <asp:DropDownList ID="drpStreetConn" runat="server" CssClass="newwidth" 
            DataSourceID="sqlStreetConn" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label15" runat="server" CssClass="searchLabel">Street Walkability:</asp:Label> 
        <asp:DropDownList ID="drpStreetWalk" runat="server" CssClass="newwidth" 
            DataSourceID="sqlStreetWalk" DataTextField="description" DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label16" runat="server" CssClass="searchLabel">Walkscore Between:</asp:Label> 
        <asp:TextBox ID="txtWalkscoreLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txtWalkscoreHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label17" runat="server" CssClass="searchLabel">Neighborhood Condition:</asp:Label> 
        <asp:DropDownList ID="drpNeighCondition" runat="server" CssClass="newwidth" 
            DataSourceID="sqlNeighCondition" DataTextField="description" 
            DataValueField="id" AppendDataBoundItems="True">
            <asp:ListItem Selected="True"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:Label ID="Label18" runat="server" CssClass="searchLabel">250 SF Between:</asp:Label> 
        <asp:TextBox ID="txt250SFLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txt250SFHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
        
        <asp:Label ID="Label19" runat="server" CssClass="searchLabel">250 Apts Between:</asp:Label> 
        <asp:TextBox ID="txt250AptsLow" runat="server" CssClass="smallwidth"></asp:TextBox>
        and <asp:TextBox ID="txt250AptsHigh" runat="server" CssClass="smallwidth"></asp:TextBox><br />
    </div>
    
    <div style="text-align:center; margin-bottom:10px;">
        <asp:Button ID="btnMaps" runat="server" Text="Clear Filter" />
        <asp:Button ID="btnSearch" runat="server" Text="Filter Properties" />
    </div>
    
    <div style="text-align:center; margin-bottom:20px;">
        <asp:Button ID="Button1" runat="server" Text="View Results in Google Maps" />
    </div>
    
    </div>
    </div>
    
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" SelectCommand="SELECT	it.id,
		it.overall,
		it.city,
		it.description,
		it.density,
		it.units,
		it.yearBuilt,
		it.streetType,
		it.streetWalk,
		it.walkscore,
		it.socioEcon,
		it.twoFiftySingleFam
FROM
(SELECT	CASE 
			WHEN	'&lt;a alt=&quot;' + a.city + '&quot; href=&quot;Home.aspx?id=' + 
					CAST(p.id AS NVARCHAR(MAX)) + '&quot;&gt;' + 
					a.city + '&lt;/a&gt;' IS NULL
			THEN	'&lt;a alt=&quot;Unknown&quot; href=&quot;Home.aspx?id=' + CAST(p.id AS NVARCHAR(MAX)) + 
					'&quot;&gt;Unknown&lt;/a&gt;'
			ELSE	'&lt;a alt=&quot;' + a.city + '&quot; href=&quot;Home.aspx?id=' + 
					CAST(p.id AS NVARCHAR(MAX)) + '&quot;&gt;' + 
					a.city + '&lt;/a&gt;'
		END AS city,
		pt.description, 
		p.density, 
		p.units, 
		p.yearBuilt, 
		st.description AS streetType, 
		swc.[description] AS streetWalk, 
		p.walkscore, 
		sec.[description] AS socioEcon, 
		p.twoFiftySingleFam, 
		p.id,
		CAST(
		(
		((CAST(nc.weight AS FLOAT) / 6.0) * CAST((SELECT neighCondition FROM Weights) AS FLOAT)) +
		((CAST(swc.weight AS FLOAT) / 20.0) * CAST((SELECT streetWalk FROM Weights) AS FLOAT)) +
		((CAST(cc.weight AS FLOAT) / 15.0) * CAST((SELECT commonAreas FROM Weights) AS FLOAT)) +
		((CAST(scc.weight AS FLOAT) / 6.0) * CAST((SELECT streetConn FROM Weights) AS FLOAT)) +
		((CAST(ec.weight AS FLOAT) / 4.0) * CAST((SELECT buildingEnclosure FROM Weights) AS FLOAT)) +
		((CAST(ssc.weight AS FLOAT) / 10.0) * CAST((SELECT streetSaftey FROM Weights) AS FLOAT)) +
		((CAST(p.walkscore AS FLOAT) / 100.0) * CAST((SELECT walkscore FROM Weights) AS FLOAT)) +
		((CAST((CASE
			WHEN p.twoFiftySingleFam &gt; 35
			THEN 15
			ELSE CASE
				WHEN p.twoFiftySingleFam &gt; 25
				THEN 13
				ELSE CASE
					WHEN p.twoFiftySingleFam &gt; 15
					THEN 11
					ELSE CASE
						WHEN p.twoFiftySingleFam &gt; 10
						THEN 9
						ELSE CASE
							WHEN p.twoFiftySingleFam &gt; 6
							THEN 7
							ELSE CASE
								WHEN p.twoFiftySingleFam &gt; 3
								THEN 4
								ELSE CASE 
									WHEN p.twoFiftySingleFam &gt; 0
									THEN 2
									ELSE 0
								END
							END
						END
					END
				END
			END
		END) AS FLOAT) / 15.0) * CAST((SELECT twoFiftySingleFam FROM Weights) AS FLOAT)) +
		((CAST((CASE
			WHEN p.twoFiftyApts &gt; 30
			THEN 5
			ELSE CASE
				WHEN p.twoFiftyApts &gt; 10
				THEN 4
				ELSE CASE
					WHEN p.twoFiftyApts &gt; 0
					THEN 2
					ELSE 0
				END
			END
		END) AS FLOAT) / 5.0) * CAST((SELECT twoFiftyApts FROM Weights) AS FLOAT))		
		) AS INT) + 1 AS overall
FROM [Address] a
INNER JOIN Properties p ON a.id = p.id 
INNER JOIN PropertyTypes pt ON p.typeCode = pt.id 
INNER JOIN StreetTypes st ON p.streetCode = st.id
INNER JOIN StreetwalkCodes swc On p.streetWalk = swc.id
INNER JOIN SocioEconCodes sec On p.socioEcon = sec.id
INNER JOIN StreetSafteyCodes ssc ON p.streetSaftey = ssc.id
INNER JOIN NeighborhoodCodes nc ON p.neighCondition = nc.id
INNER JOIN CommonCodes cc ON p.commonAreas = cc.id
INNER JOIN StreetconnCodes scc ON p.streetConn = scc.id
INNER JOIN EnclosureCodes ec ON p.buildingEnclosure = ec.id
WHERE (pt.description LIKE @proptypeFilter)
AND (p.density &gt;= @lowdensityFilter)
AND (p.density &lt;= @highdensityFilter)
AND (p.walkscore &gt;= @lowwalkFilter)
AND (p.walkscore &lt;= @highwalkFilter)
AND (p.id = ISNULL(@id, p.id))
AND (a.city LIKE ISNULL('%' + @city + '%', '%'))
AND (p.typeCode LIKE ISNULL(@proptype, p.typeCode))
AND (p.density &gt;= ISNULL(@densitylow, p.density))
AND (p.density &lt;= ISNULL(@densityhigh, p.density))
AND (p.area &gt;= ISNULL(@arealow, p.area))
AND (p.area &lt;= ISNULL(@areahigh, p.area))
AND (p.units &gt;= ISNULL(@unitslow, p.units))
AND (p.units &lt;= ISNULL(@unitshigh, p.units))
AND (p.streetCode = ISNULL(@streetType, p.streetCode))
AND (p.yearBuilt &gt;= ISNULL(@yearBuiltLow, p.yearBuilt))
AND (p.yearBuilt &lt;= ISNULL(@yearBuiltHigh, p.yearBuilt))
AND (p.socioEcon = ISNULL(@socioEcon, p.socioEcon))
AND (p.streetSaftey = ISNULL(@streetSaftey, p.streetSaftey))
AND (p.buildingEnclosure = ISNULL(@buildingEnclosure, p.buildingEnclosure))
AND (p.commonAreas = ISNULL(@commonAreas, p.commonAreas))
AND (p.streetConn = ISNULL(@streetConn, p.streetConn))
AND (p.streetWalk = ISNULL(@streetWalk, p.streetWalk))
AND (p.walkscore &gt;= ISNULL(@walkscorelow, p.walkscore))
AND (p.walkscore &lt;= ISNULL(@walkscorehigh, p.walkscore))
AND (p.neighCondition = ISNULL(@neighCondition, p.neighCondition))
AND (p.twoFiftySingleFam &gt;= ISNULL(@twoFiftySingleFamLow, p.twoFiftySingleFam))
AND (p.twoFiftySingleFam &lt;= ISNULL(@twoFiftySingleFamHigh, p.twoFiftySingleFam))
AND (p.twoFiftyApts &gt;= ISNULL(@twoFiftyAptsLow, p.twoFiftyApts))
AND (p.twoFiftyApts &lt;= ISNULL(@twoFiftyAptsHigh, p.twoFiftyApts))) it
WHERE (it.overall &gt;= ISNULL(@overallScoreLow, it.overall))
AND (it.overall &lt;= ISNULL(@overallScoreHigh, it.overall))
ORDER BY id" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="%" Name="proptypeFilter" 
                QueryStringField="proptype" />
            <asp:QueryStringParameter DefaultValue="0" Name="lowdensityFilter" 
                QueryStringField="lowdensity" />
            <asp:QueryStringParameter DefaultValue="1000" Name="highdensityFilter" 
                QueryStringField="highdensity" />
            <asp:QueryStringParameter DefaultValue="0" Name="lowwalkFilter" 
                QueryStringField="lowwalk" />
            <asp:QueryStringParameter DefaultValue="100" Name="highwalkFilter" 
                QueryStringField="highwalk" />
            <asp:SessionParameter Name="id" SessionField="id" Type="Int64" 
                ConvertEmptyStringToNull="False" />
            <asp:SessionParameter Name="city" SessionField="city" Type="String" />
            <asp:SessionParameter Name="proptype" SessionField="proptype" Type="Int64" />
            <asp:SessionParameter Name="densitylow" SessionField="densitylow" Type="Double" />
            <asp:SessionParameter Name="densityhigh" SessionField="densityhigh" Type="Double" />
            <asp:SessionParameter Name="arealow" SessionField="arealow" Type="Int64" />
            <asp:SessionParameter Name="areahigh" SessionField="areahigh" Type="Int64" />
            <asp:SessionParameter Name="unitslow" SessionField="unitslow" Type="Int64" />
            <asp:SessionParameter Name="unitshigh" SessionField="unitshigh" Type="Int64" />
            <asp:SessionParameter Name="streetType" SessionField="streetType" Type="Int64" />
            <asp:SessionParameter Name="yearBuiltLow" SessionField="yearBuiltLow" />
            <asp:SessionParameter Name="yearBuiltHigh" SessionField="yearBuiltHigh" />
            <asp:SessionParameter Name="socioEcon" SessionField="socioEcon" Type="Int64" />
            <asp:SessionParameter Name="streetSaftey" SessionField="streetSaftey" Type="Int64" />
            <asp:SessionParameter Name="buildingEnclosure" 
                SessionField="buildingEnclosure" Type="Int64" />
            <asp:SessionParameter Name="commonAreas" SessionField="commonAreas" Type="Int64" />
            <asp:SessionParameter Name="streetConn" SessionField="streetConn" Type="Int64" />
            <asp:SessionParameter Name="streetWalk" SessionField="streetWalk" Type="Int64" />
            <asp:SessionParameter Name="walkscorelow" SessionField="walkscoreLow" Type="Int64" />
            <asp:SessionParameter Name="walkscorehigh" SessionField="walkscoreHigh" Type="Int64" />
            <asp:SessionParameter Name="neighCondition" SessionField="neighCondition" Type="Int64" />
            <asp:SessionParameter Name="twoFiftySingleFamLow" 
                SessionField="twoFiftySingleFamLow" Type="Int64" />
            <asp:SessionParameter Name="twoFiftySingleFamHigh" 
                SessionField="twoFiftySingleFamHigh" Type="Int64" />
            <asp:SessionParameter Name="twoFiftyAptsLow" SessionField="twoFiftyAptsLow" Type="Int64" />
            <asp:SessionParameter Name="twoFiftyAptsHigh" SessionField="twoFiftyAptsHigh" Type="Int64" />
            <asp:SessionParameter Name="overallScoreLow" SessionField="overallScoreLow" Type="Int64" />
            <asp:SessionParameter Name="overallScoreHigh" SessionField="overallScoreHigh" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="id" 
        DataSourceID="SqlDataSource1" HorizontalAlign="Center">
        <RowStyle Height="35px" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="city" HeaderText="City" HtmlEncode="False" 
                ReadOnly="True" SortExpression="city" />
            <asp:BoundField DataField="id" HeaderText="Property Number" 
                InsertVisible="False" ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="overall" HeaderText="Overall Score" 
                SortExpression="overall" />
            <asp:BoundField DataField="description" HeaderText="Type" 
                SortExpression="description" />
            <asp:BoundField DataField="density" HeaderText="Density" 
                SortExpression="density" />
            <asp:BoundField DataField="units" HeaderText="Units" SortExpression="units" />
            <asp:BoundField DataField="yearBuilt" HeaderText="Year Built"
                SortExpression="yearBuilt" />
            <asp:BoundField DataField="streetType" HeaderText="Street Type" 
                SortExpression="streetType" />
            <asp:BoundField DataField="streetWalk" HeaderText="Walkability" 
                SortExpression="streetWalk" />
            <asp:BoundField DataField="walkscore" HeaderText="Walkscore" 
                SortExpression="walkscore" />
            <asp:BoundField DataField="socioEcon" HeaderText="Socio Econ" 
                SortExpression="socioEcon" />
            <asp:BoundField DataField="twoFiftySingleFam" HeaderText="250 Single Fam" 
                SortExpression="twoFiftySingleFam" />
        </Columns>
        <HeaderStyle BorderStyle="Solid" BackColor="#CCCCCC" HorizontalAlign="Center" />
    </asp:GridView>
    
    </asp:Content>

