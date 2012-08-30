Imports System.Data.Linq.SqlClient
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Random

Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Index
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        txtID.Text = Session("id")
        txtCity.Text = Session("city")
        drpPropType.SelectedValue = Session("proptype")
        txtDensityLow.Text = Session("densitylow")
        txtDensityHigh.Text = Session("densityhigh")
        txtAreaLow.Text = Session("arealow")
        txtAreaHigh.Text = Session("areahigh")
        txtUnitsLow.Text = Session("unitslow")
        txtUnitsHigh.Text = Session("unitshigh")
        drpStreetType.SelectedValue = Session("streetType")
        txtYearBuiltLow.Text = Session("yearBuiltLow")
        txtYearBuiltHigh.Text = Session("yearBuiltHigh")
        drpSocioEcon.SelectedValue = Session("socioEcon")
        drpStreetSaftey.SelectedValue = Session("streetSaftey")
        drpBuildingEnclosure.SelectedValue = Session("buildingEnclosure")
        drpCommonAreas.SelectedValue = Session("commonAreas")
        drpStreetConn.SelectedValue = Session("streetConn")
        drpStreetWalk.SelectedValue = Session("streetWalk")
        txtWalkscoreLow.Text = Session("walkscorelow")
        txtWalkscoreHigh.Text = Session("walkscorehigh")
        drpNeighCondition.SelectedValue = Session("neighCondition")
        txt250SFLow.Text = Session("twoFiftySingleFamLow")
        txt250SFHigh.Text = Session("twoFiftySingleFamHigh")
        txt250AptsLow.Text = Session("twoFiftyAptsLow")
        txt250AptsHigh.Text = Session("twoFiftyAptsHigh")
        txtOverallLow.Text = Session("overallScoreLow")
        txtOverallHigh.Text = Session("overallScoreHigh")
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If Not IsPostBack Then
            Session("id") = Nothing
            Session("city") = Nothing
            Session("proptype") = Nothing
            Session("densitylow") = Nothing
            Session("densityhigh") = Nothing
            Session("arealow") = Nothing
            Session("areahigh") = Nothing
            Session("unitslow") = Nothing
            Session("unitshigh") = Nothing
            Session("streetType") = Nothing
            Session("yearBuiltLow") = Nothing
            Session("yearBuiltHigh") = Nothing
            Session("socioEcon") = Nothing
            Session("streetSaftey") = Nothing
            Session("buildingEnclosure") = Nothing
            Session("commonAreas") = Nothing
            Session("streetConn") = Nothing
            Session("streetWalk") = Nothing
            Session("walkscorelow") = Nothing
            Session("walkscorehigh") = Nothing
            Session("neighCondition") = Nothing
            Session("twoFiftySingleFamLow") = Nothing
            Session("twoFiftySingleFamHigh") = Nothing
            Session("twoFiftyAptsLow") = Nothing
            Session("twoFiftyAptsHigh") = Nothing
            Session("overallScoreLow") = Nothing
            Session("overallScoreHigh") = Nothing
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        setSearchVariables()
    End Sub

    Protected Sub btnMaps_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMaps.Click
        clearSearchVariables()
        Response.Redirect("~/Index.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim db As New PropertiesDBDataContext()
        Dim props As List(Of [Property])
        Dim geocodeUrl As String
        Dim geocodeUrlPrefix As String
        Dim geocodeUrlSuffix As String
        Dim geocodeAddress As String

        geocodeUrl = ""
        geocodeUrlPrefix = "http://maps.google.com/maps/api/geocode/xml?address="
        geocodeUrlSuffix = "&sensor=false"
        geocodeAddress = ""

        setSearchVariables()
        ' Write Google maps code here
        props = (From p In db.Properties _
                 Join a In db.Addresses _
                 On p.id Equals a.id _
                 Where p.id = CInt(IIf(IsNothing(Session("id")), p.id, CInt(Session("id")))) _
                 And SqlMethods.Like(a.city, CStr(IIf(IsNothing(Session("city")), "%", "%" & CStr(Session("city")) & "%"))) _
                 And p.typeCode = CInt(IIf(IsNothing(Session("propType")), CInt(p.typeCode), CInt(Session("propType")))) _
                 And p.density >= CDbl(IIf(IsNothing(Session("densitylow")), CDbl(p.density), CDbl(Session("densitylow")))) _
                 And p.density <= CDbl(IIf(IsNothing(Session("densityhigh")), CDbl(p.density), CDbl(Session("densityhigh")))) _
                 And p.area >= CInt(IIf(IsNothing(Session("arealow")), CInt(p.area), CInt(Session("arealow")))) _
                 And p.area <= CInt(IIf(IsNothing(Session("areahigh")), CInt(p.area), CInt(Session("areahigh")))) _
                 And p.units >= CInt(IIf(IsNothing(Session("unitslow")), CInt(p.units), CInt(Session("unitslow")))) _
                 And p.units <= CInt(IIf(IsNothing(Session("unitshigh")), CInt(p.units), CInt(Session("unitshigh")))) _
                 And p.streetCode = CInt(IIf(IsNothing(Session("streetType")), CInt(p.streetCode), CInt(Session("streetType")))) _
                 And p.yearBuilt >= CInt(IIf(IsNothing(Session("yearBuiltLow")), CInt(p.yearBuilt), CInt(Session("yearBuiltLow")))) _
                 And p.yearBuilt <= CInt(IIf(IsNothing(Session("yearBuiltHigh")), CInt(p.yearBuilt), CInt(Session("yearBuiltHigh")))) _
                 And p.socioEcon = CInt(IIf(IsNothing(Session("socioEcon")), CInt(p.socioEcon), CInt(Session("socioEcon")))) _
                 And p.streetSaftey = CInt(IIf(IsNothing(Session("streetSaftey")), CInt(p.streetSaftey), CInt(Session("streetSaftey")))) _
                 And p.buildingEnclosure = CInt(IIf(IsNothing(Session("buildingEnclosure")), CInt(p.buildingEnclosure), CInt(Session("buildingEnclosure")))) _
                 And p.commonAreas = CInt(IIf(IsNothing(Session("commonAreas")), CInt(p.commonAreas), CInt(Session("commonAreas")))) _
                 And p.streetConn = CInt(IIf(IsNothing(Session("streetConn")), CInt(p.streetConn), CInt(Session("streetConn")))) _
                 And p.streetWalk = CInt(IIf(IsNothing(Session("streetWalk")), CInt(p.streetWalk), CInt(Session("streetWalk")))) _
                 And p.walkscore >= CInt(IIf(IsNothing(Session("walkscorelow")), CInt(p.walkscore), CInt(Session("walkscorelow")))) _
                 And p.walkscore <= CInt(IIf(IsNothing(Session("walkscorehigh")), CInt(p.walkscore), CInt(Session("walkscorehigh")))) _
                 And p.neighCondition = CInt(IIf(IsNothing(Session("neighCondition")), CInt(p.neighCondition), CInt(Session("neighCondition")))) _
                 And p.twoFiftySingleFam >= CInt(IIf(IsNothing(Session("twoFiftySingleFamLow")), CInt(p.twoFiftySingleFam), CInt(Session("twoFiftySingleFamLow")))) _
                 And p.twoFiftySingleFam <= CInt(IIf(IsNothing(Session("twoFiftySingleFamHigh")), CInt(p.twoFiftySingleFam), CInt(Session("twoFiftySingleFamHigh")))) _
                 And p.twoFiftyApts >= CInt(IIf(IsNothing(Session("twoFiftyAptsLow")), CInt(p.twoFiftyApts), CInt(Session("twoFiftyAptsLow")))) _
                 And p.twoFiftyApts <= CInt(IIf(IsNothing(Session("twoFiftyAptsHigh")), CInt(p.twoFiftyApts), CInt(Session("twoFiftyAptsHigh")))) _
                 And _
                      CInt( _
                      ((CDbl(p.NeighborhoodCode.weight) / 6.0) * CDbl((From w In db.Weights Select w.neighCondition).First())) + _
                      ((CDbl(p.StreetwalkCode.weight) / 20.0) * CDbl((From w In db.Weights Select w.streetWalk).First())) + _
                      ((CDbl(p.CommonCode.weight) / 15.0) * CDbl((From w In db.Weights Select w.commonAreas).First())) + _
                      ((CDbl(p.StreetconnCode.weight) / 6.0) * CDbl((From w In db.Weights Select w.streetConn).First())) + _
                      ((CDbl(p.EnclosureCode.weight) / 4.0) * CDbl((From w In db.Weights Select w.buildingEnclosure).First())) + _
                      ((CDbl(p.StreetSafteyCode.weight) / 10.0) * CDbl((From w In db.Weights Select w.streetSaftey).First())) + _
                      ((CDbl(p.walkscore) / 100.0) * CDbl((From w In db.Weights Select w.walkscore).First())) + _
                      ((CDbl(IIf(p.twoFiftySingleFam > 35, 15, _
                                 IIf(p.twoFiftySingleFam > 25, 13, _
                                     IIf(p.twoFiftySingleFam > 15, 11, _
                                         IIf(p.twoFiftySingleFam > 10, 9, _
                                             IIf(p.twoFiftySingleFam > 6, 7, _
                                                 IIf(p.twoFiftySingleFam > 3, 4, _
                                                     IIf(p.twoFiftySingleFam > 0, 2, 0)))))))) / 15.0) * _
                                                     CDbl((From w In db.Weights Select w.twoFiftySingleFam).First())) + _
                      ((CDbl(IIf(p.twoFiftyApts > 30, 5, _
                                 IIf(p.twoFiftyApts > 10, 4, _
                                     IIf(p.twoFiftyApts > 0, 2, 0)))) / 5.0) * _
                                     CDbl((From w In db.Weights Select w.twoFiftyApts).First())) _
                      ) + 1 >= CInt(IIf(IsNothing(Session("overallScoreLow")), 0, CInt(Session("overallScoreLow")))) _
                 And _
                      CInt( _
                      ((CDbl(p.NeighborhoodCode.weight) / 6.0) * CDbl((From w In db.Weights Select w.neighCondition).First())) + _
                      ((CDbl(p.StreetwalkCode.weight) / 20.0) * CDbl((From w In db.Weights Select w.streetWalk).First())) + _
                      ((CDbl(p.CommonCode.weight) / 15.0) * CDbl((From w In db.Weights Select w.commonAreas).First())) + _
                      ((CDbl(p.StreetconnCode.weight) / 6.0) * CDbl((From w In db.Weights Select w.streetConn).First())) + _
                      ((CDbl(p.EnclosureCode.weight) / 4.0) * CDbl((From w In db.Weights Select w.buildingEnclosure).First())) + _
                      ((CDbl(p.StreetSafteyCode.weight) / 10.0) * CDbl((From w In db.Weights Select w.streetSaftey).First())) + _
                      ((CDbl(p.walkscore) / 100.0) * CDbl((From w In db.Weights Select w.walkscore).First())) + _
                      ((CDbl(IIf(p.twoFiftySingleFam > 35, 15, _
                                 IIf(p.twoFiftySingleFam > 25, 13, _
                                     IIf(p.twoFiftySingleFam > 15, 11, _
                                         IIf(p.twoFiftySingleFam > 10, 9, _
                                             IIf(p.twoFiftySingleFam > 6, 7, _
                                                 IIf(p.twoFiftySingleFam > 3, 4, _
                                                     IIf(p.twoFiftySingleFam > 0, 2, 0)))))))) / 15.0) * _
                                                     CDbl((From w In db.Weights Select w.twoFiftySingleFam).First())) + _
                      ((CDbl(IIf(p.twoFiftyApts > 30, 5, _
                                 IIf(p.twoFiftyApts > 10, 4, _
                                     IIf(p.twoFiftyApts > 0, 2, 0)))) / 5.0) * _
                                     CDbl((From w In db.Weights Select w.twoFiftyApts).First())) _
                      ) + 1 <= CInt(IIf(IsNothing(Session("overallScoreHigh")), 100, CInt(Session("overallScoreHigh")))) _
                 Select p).ToList()

        Dim random As New Random()
        Dim rndString As String = CStr(random.Next(0, 100))
        Dim fileName As String = Session.SessionID.Substring(0, 10) & rndString & ".kml"
        Dim writer As XmlWriter = XmlWriter.Create(Server.MapPath("~/kml/" & fileName))
        writer.WriteStartDocument()
        writer.WriteStartElement("kml")
        writer.WriteStartElement("Document")
        For Each prop In props
            If prop.latitude Is Nothing Then
                geocodeAddress = prop.Address.street1 & "+" & _
                prop.Address.city & "+" & prop.Address.state & "+" & _
                prop.Address.zip
                geocodeUrl = geocodeUrlPrefix + geocodeAddress + geocodeUrlSuffix
                geocodeUrl = geocodeUrl.Replace(" ", "+")
                Try
                    Dim xml = XDocument.Load(geocodeUrl)
                    prop.latitude = (From location In xml.Descendants("location") _
                                         Select location.Element("lat").Value).First().ToString()
                    prop.longitude = (From location In xml.Descendants("location") _
                                         Select location.Element("lng").Value).First().ToString()
                Catch ex As Exception
                    'skip the coordinates if google times out or if too many requests
                End Try
                geocodeUrl = ""
            End If
            If Not prop.Address.street1 Is Nothing And Not prop.Address.city.Equals("Unknown") Then
                writer.WriteStartElement("Placemark")
                writer.WriteStartElement("name")
                writer.WriteValue(prop.PropertyType.description)
                writer.WriteEndElement()
                writer.WriteStartElement("description")
                Dim html = "<img style=""width:150px; height:auto; float:left;"" src=""http://www.utahplanners.com/Pic.aspx?pid="
                html = html & CStr(prop.id) & "&first=1"" />"
                html = html & "<div style=""margin-left:160px;"">"
                html = html & "<h3><a href=""http://www.utahplanners.com/Home.aspx?id=" & CStr(prop.id) & """>Property #" & CStr(prop.id) & "</a></h3>"
                html = html & "Overall Score: " & CStr(calcScore(prop.id)) & "<br /><br />"
                html = html & prop.Address.street1 & "<br />"
                html = html & prop.Address.city & ", " & prop.Address.state & " " & prop.Address.zip
                html = html & "</div>"
                writer.WriteCData(html)
                writer.WriteEndElement()
                writer.WriteStartElement("Point")
                writer.WriteStartElement("coordinates")
                writer.WriteValue(prop.longitude & "," & prop.latitude)
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteEndElement()
            End If
        Next

        ' Submit any new geocode data
        db.SubmitChanges()

        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Flush()
        writer.Close()

        Response.Redirect("http://maps.google.com/?q=" & Request.Url.Scheme & "://" & Request.Url.Authority & Request.ApplicationPath.TrimEnd("/") & "/kml/" & fileName)
        clearSearchVariables()
    End Sub

    Sub setSearchVariables()
        If Not txtID.Text.Equals("") Then Session("id") = txtID.Text
        If Not txtCity.Text.Equals("") Then Session("city") = txtCity.Text
        If Not drpPropType.SelectedValue = Nothing Then Session("proptype") = drpPropType.SelectedValue
        If Not txtDensityLow.Text.Equals("") Then Session("densitylow") = txtDensityLow.Text
        If Not txtDensityHigh.Text.Equals("") Then Session("densityhigh") = txtDensityHigh.Text
        If Not txtAreaLow.Text.Equals("") Then Session("arealow") = txtAreaLow.Text
        If Not txtAreaHigh.Text.Equals("") Then Session("areahigh") = txtAreaHigh.Text
        If Not txtUnitsLow.Text.Equals("") Then Session("unitslow") = txtUnitsLow.Text
        If Not txtUnitsHigh.Text.Equals("") Then Session("unitshigh") = txtUnitsHigh.Text
        If Not drpStreetType.SelectedValue = Nothing Then Session("streetType") = drpStreetType.SelectedValue
        If Not txtYearBuiltLow.Text.Equals("") Then Session("yearBuiltLow") = txtYearBuiltLow.Text
        If Not txtYearBuiltHigh.Text.Equals("") Then Session("yearBuiltHigh") = txtYearBuiltHigh.Text
        If Not drpSocioEcon.SelectedValue = Nothing Then Session("socioEcon") = drpSocioEcon.SelectedValue
        If Not drpStreetSaftey.SelectedValue = Nothing Then Session("streetSaftey") = drpStreetSaftey.SelectedValue
        If Not drpBuildingEnclosure.SelectedValue = Nothing Then Session("buildingEnclosure") = drpBuildingEnclosure.SelectedValue
        If Not drpCommonAreas.SelectedValue = Nothing Then Session("commonAreas") = drpCommonAreas.SelectedValue
        If Not drpStreetConn.SelectedValue = Nothing Then Session("streetConn") = drpStreetConn.SelectedValue
        If Not drpStreetWalk.SelectedValue = Nothing Then Session("streetWalk") = drpStreetWalk.SelectedValue
        If Not txtWalkscoreLow.Text.Equals("") Then Session("walkscorelow") = txtWalkscoreLow.Text
        If Not txtWalkscoreHigh.Text.Equals("") Then Session("walkscorehigh") = txtWalkscoreHigh.Text
        If Not drpNeighCondition.SelectedValue = Nothing Then Session("neighCondition") = drpNeighCondition.SelectedValue
        If Not txt250SFLow.Text.Equals("") Then Session("twoFiftySingleFamLow") = txt250SFLow.Text
        If Not txt250SFHigh.Text.Equals("") Then Session("twoFiftySingleFamHigh") = txt250SFHigh.Text
        If Not txt250AptsLow.Text.Equals("") Then Session("twoFiftyAptsLow") = txt250AptsLow.Text
        If Not txt250AptsHigh.Text.Equals("") Then Session("twoFiftyAptsHigh") = txt250AptsHigh.Text
        If Not txtOverallLow.Text.Equals("") Then Session("overallScoreLow") = txtOverallLow.Text
        If Not txtOverallHigh.Text.Equals("") Then Session("overallScoreHigh") = txtOverallHigh.Text
    End Sub

    Sub clearSearchVariables()
        Session("id") = Nothing
        Session("city") = Nothing
        Session("proptype") = Nothing
        Session("densitylow") = Nothing
        Session("densityhigh") = Nothing
        Session("arealow") = Nothing
        Session("areahigh") = Nothing
        Session("unitslow") = Nothing
        Session("unitshigh") = Nothing
        Session("streetType") = Nothing
        Session("yearBuiltLow") = Nothing
        Session("yearBuiltHigh") = Nothing
        Session("socioEcon") = Nothing
        Session("streetSaftey") = Nothing
        Session("buildingEnclosure") = Nothing
        Session("commonAreas") = Nothing
        Session("streetConn") = Nothing
        Session("streetWalk") = Nothing
        Session("walkscorelow") = Nothing
        Session("walkscorehigh") = Nothing
        Session("neighCondition") = Nothing
        Session("twoFiftySingleFamLow") = Nothing
        Session("twoFiftySingleFamHigh") = Nothing
        Session("twoFiftyAptsLow") = Nothing
        Session("twoFiftyAptsHigh") = Nothing
        Session("overallScoreLow") = Nothing
        Session("overallScoreHigh") = Nothing
    End Sub

    Function calcScore(ByVal propId As Integer) As Integer
        Dim db As New PropertiesDBDataContext
        Dim weights = (From w In db.Weights _
                      Select w).First()
        Dim prop = (From p In db.Properties _
                    Where p.id = propId _
                    Select p).First()

        Dim neighborScore = (prop.NeighborhoodCode.weight / 6) * weights.neighCondition
        Dim streetwalkScore = (prop.StreetwalkCode.weight / 20) * weights.streetWalk
        Dim commonScore = (prop.CommonCode.weight / 15) * weights.commonAreas
        Dim streetConnScore = (prop.StreetconnCode.weight / 6) * weights.streetConn
        Dim enclosureScore = (prop.EnclosureCode.weight / 4) * weights.buildingEnclosure
        Dim strSafteyScore = (prop.StreetSafteyCode.weight / 10) * weights.streetSaftey
        Dim walkscoreScore = (prop.walkscore / 100) * weights.walkscore

        Dim twoFiftySF As Integer
        If prop.twoFiftySingleFam > 35 Then
            twoFiftySF = 15
        ElseIf prop.twoFiftySingleFam > 25 Then
            twoFiftySF = 13
        ElseIf prop.twoFiftySingleFam > 15 Then
            twoFiftySF = 11
        ElseIf prop.twoFiftySingleFam > 10 Then
            twoFiftySF = 9
        ElseIf prop.twoFiftySingleFam > 6 Then
            twoFiftySF = 7
        ElseIf prop.twoFiftySingleFam > 3 Then
            twoFiftySF = 4
        ElseIf prop.twoFiftySingleFam > 0 Then
            twoFiftySF = 2
        Else
            twoFiftySF = 0
        End If

        Dim twoFiftyApts As Integer
        If prop.twoFiftyApts > 30 Then
            twoFiftyApts = 5
        ElseIf prop.twoFiftyApts > 10 Then
            twoFiftyApts = 4
        ElseIf prop.twoFiftyApts > 0 Then
            twoFiftyApts = 2
        Else
            twoFiftyApts = 0
        End If

        Dim twoFiftySFScore = (twoFiftySF / 15) * weights.twoFiftySingleFam
        Dim twoFiftyAptsScore = (twoFiftyApts / 5) * weights.twoFiftyApts

        Dim overall = neighborScore + streetwalkScore + commonScore + _
        streetConnScore + enclosureScore + strSafteyScore + walkscoreScore + _
        twoFiftySFScore + twoFiftyAptsScore

        Return overall
    End Function
End Class
