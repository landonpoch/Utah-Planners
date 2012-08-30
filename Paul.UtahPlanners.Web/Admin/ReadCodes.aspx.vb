
Partial Class Codes_ReadCodes
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        GridView1.DataSourceID = DropDownList1.SelectedItem.Text
    End Sub
End Class
