Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Properties_Delete
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim db As New PropertiesDBDataContext()
        Dim id As Integer

        id = 0
        If Not Request.QueryString("id") Is Nothing Then id = Request.QueryString("id")

        Dim prop = (From p In db.Properties _
                     Where p.id = id _
                     Select p).First()

        db.Properties.DeleteOnSubmit(prop)
        db.Addresses.DeleteOnSubmit(prop.Address)
        db.Pictures.DeleteAllOnSubmit(prop.Pictures)
        db.SubmitChanges()

        Response.Redirect("Read.aspx")

    End Sub
End Class
