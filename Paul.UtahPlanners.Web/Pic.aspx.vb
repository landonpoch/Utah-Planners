Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Pic
    Inherits System.Web.UI.Page

    Public pic As Picture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As New PropertiesDBDataContext()
        Dim unavailable As Integer
        Dim id As Integer 'ID for the picture
        Dim pid As Integer 'ID for the project
        Dim first As Integer 'See if this has the primary image flag
        Dim second As Integer 'See if this has the secondary image flag

        unavailable = 398

        ' Initialize to default values
        id = 0
        pid = 0
        first = 0
        second = 0

        If Not Request.QueryString("id") Is Nothing Then id = Request.QueryString("id")
        If Not Request.QueryString("pid") Is Nothing Then pid = Request.QueryString("pid")
        If Not Request.QueryString("first") Is Nothing Then first = Request.QueryString("first")
        If Not Request.QueryString("second") Is Nothing Then second = Request.QueryString("second")

        If id <> 0 Then
            pic = (From p In db.Pictures _
                   Where p.id = id _
                   Select p).First()

            'Primary Image
        ElseIf pid <> 0 And first = 1 Then
            Dim pics = (From p In db.Pictures _
                        Where p.property_id = pid _
                        And p.mainPicture = 1 _
                        Select p).ToList()
            If pics.Count = 0 Then
                Dim pics2 = (From p In db.Pictures _
                            Where p.property_id = pid _
                            And p.secondaryPicture = 0 _
                            Select p).ToList()
                If pics2.Count = 0 Then
                    pic = (From p In db.Pictures _
                           Where p.id = unavailable _
                           Select p).First()
                Else
                    pic = pics2.First()
                End If
            Else
                pic = pics.First()
            End If

            ' Secondary Image
        ElseIf pid <> 0 And second = 1 Then
            Dim pics = (From p In db.Pictures _
                        Where p.property_id = pid _
                        And p.secondaryPicture = 1 _
                        Select p).ToList()
            If pics.Count = 0 Then
                pic = (From p In db.Pictures _
                       Where p.id = unavailable _
                       Select p).First()
            Else
                pic = pics.First()
            End If
        End If

        Response.ContentType = pic.mimeType
        Response.BinaryWrite(pic.binaryData.ToArray())
    End Sub
End Class
