Imports com.landonpoch.entities
Imports com.landonpoch.data

Partial Class Home
    Inherits System.Web.UI.Page

    Public prop As [Property]
    Public walkString As String
    Public picIds As List(Of Integer)
    Public score As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As New PropertiesDBDataContext()
        Dim addr As Address
        Dim pid As Integer

        pid = 1
        If Not Request.QueryString("id") Is Nothing Then pid = Request.QueryString("id")

        prop = (From p In db.Properties _
                Where p.id = pid _
                Select p).First()
        addr = prop.Address
        ' Check for NULLs!! This is throwing exceptions
        Try
            walkString = addr.street1.Replace(" ", "+") & "+"
            walkString = walkString & addr.city & "+"
            walkString = walkString & addr.state & "+"
            walkString = walkString & addr.zip
        Catch ex As Exception
            walkString = ""
        End Try

        picIds = (From p In db.Pictures _
                  Where p.property_id = prop.id _
                  And p.secondaryPicture = 0 _
                  And p.mainPicture = 0 _
                  Select p.id).ToList()

        score = calcScore()
        
    End Sub

    Function calcScore() As Integer
        Dim db As New PropertiesDBDataContext
        Dim weights = (From w In db.Weights _
                      Select w).First()

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
