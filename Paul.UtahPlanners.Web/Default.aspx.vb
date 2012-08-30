Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class _Default
    Inherits System.Web.UI.Page

    Public picId As Integer
    Public propId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As New PropertiesDBDataContext()

        Dim pics = (From p In db.Pictures _
               Where p.frontPage = 1 _
               Select p.id).ToList()

        picId = pics.Item((New Random().Next(0, pics.Count)))
        propId = (From p In db.Pictures _
                  Where p.id = picId _
                  Select p.property_id).First()
    End Sub
End Class
