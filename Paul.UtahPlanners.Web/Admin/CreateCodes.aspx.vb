Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Codes_CreateCodes
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim db As New PropertiesDBDataContext()

        If DropDownList1.SelectedIndex = 0 Then
            Dim comm As New CommonCode()
            comm.description = desc.Text
            comm.weight = weigh.Text
            db.CommonCodes.InsertOnSubmit(comm)
            db.SubmitChanges()
        ElseIf DropDownList1.SelectedIndex = 1 Then
            Dim enc As New EnclosureCode()
            enc.description = desc.Text
            enc.weight = weigh.Text
            db.EnclosureCodes.InsertOnSubmit(enc)
            db.SubmitChanges()
        ElseIf DropDownList1.SelectedIndex = 2 Then
            Dim neigh As New NeighborhoodCode()
            neigh.description = desc.Text
            neigh.weight = weigh.Text
            db.NeighborhoodCodes.InsertOnSubmit(neigh)
            db.SubmitChanges()
        ElseIf DropDownList1.SelectedIndex = 3 Then
            Dim strConn As New StreetconnCode()
            strConn.description = desc.Text
            strConn.weight = weigh.Text
            db.StreetconnCodes.InsertOnSubmit(strConn)
            db.SubmitChanges()
        ElseIf DropDownList1.SelectedIndex = 4 Then
            Dim strSafe As New StreetSafteyCode()
            strSafe.description = desc.Text
            strSafe.weight = weigh.Text
            db.StreetSafteyCodes.InsertOnSubmit(strSafe)
            db.SubmitChanges()
        ElseIf DropDownList1.SelectedIndex = 5 Then
            Dim strWalk As New StreetwalkCode()
            strWalk.description = desc.Text
            strWalk.weight = weigh.Text
            db.StreetwalkCodes.InsertOnSubmit(strWalk)
            db.SubmitChanges()
        End If

        Response.Redirect("Admin.aspx")

    End Sub
End Class
