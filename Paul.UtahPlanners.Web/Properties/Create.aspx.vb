Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class admin_Create
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim db = New PropertiesDBDataContext()
            Dim prop = New [Property]()
            Dim addr = New Address()

            Session("db") = db
            Session("prop") = prop
            Session("addr") = addr

            Dim sesid = Session.SessionID

            Console.WriteLine("breakpoint")
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim db = New PropertiesDBDataContext()
        Dim prop As [Property]
        Dim addr = New Address()

        Dim sesid = Session.SessionID

        db = Session("db")
        prop = Session("prop")
        addr = Session("addr")

        addr.street1 = txtStreet1.Text
        addr.street2 = txtStreet2.Text
        addr.city = txtCity.Text
        addr.state = txtState.Text
        addr.zip = txtZip.Text

        prop.density = txtDensity.Text
        prop.area = txtArea.Text
        prop.units = txtUnits.Text
        prop.yearBuilt = txtYearBuilt.Text
        prop.walkscore = txtWalkscore.Text
        prop.twoFiftySingleFam = txt250sf.Text
        prop.twoFiftyApts = txt250apts.Text
        prop.typeCode = drpProptertyType.SelectedValue
        prop.streetCode = drpStreetType.SelectedValue
        prop.socioEcon = drpSocioEcon.SelectedValue
        prop.streetSaftey = drpStreetSaftey.SelectedValue
        prop.buildingEnclosure = drpBuildingEnclosure.SelectedValue
        prop.commonAreas = drpCommonAreas.SelectedValue
        prop.streetConn = drpStreetConn.SelectedValue
        prop.streetWalk = drpStreetWalk.SelectedValue
        prop.neighCondition = drpNeighCond.SelectedValue
        prop.notes = txtNotes.Text
        prop.adminNotes = txtAdminNotes.Text
        prop.walkscoreNotes = txtWalkscoreNotes.Text
        prop.notFinished = txtNotFinished.Text

        prop.Address = addr
        db.Properties.InsertOnSubmit(prop)
        db.SubmitChanges()
        Session("newPic") = Nothing
        If IsPostBack Then
            Response.Redirect("Read.aspx")
        End If

    End Sub
End Class
