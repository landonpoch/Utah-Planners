Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Types_CreateTypes
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim db As New PropertiesDBDataContext()
        Dim objType As Integer

        objType = type.SelectedIndex

        If objType = 0 Then
            Dim propType As New PropertyType()
            propType.description = description.Text
            db.PropertyTypes.InsertOnSubmit(propType)
            db.SubmitChanges()
        ElseIf objType = 1 Then
            Dim strType As New StreetType()
            strType.description = description.Text
            db.StreetTypes.InsertOnSubmit(strType)
            db.SubmitChanges()
        ElseIf objType = 2 Then
            Dim socio As New SocioEconCode()
            socio.description = description.Text
            db.SocioEconCodes.InsertOnSubmit(socio)
            db.SubmitChanges()
        End If

        Response.Redirect("Admin.aspx")
    End Sub
End Class
