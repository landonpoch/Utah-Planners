Imports System.Web.UI.WebControls

Imports com.landonpoch.data
Imports com.landonpoch.entities

Partial Class Properties_Update
    Inherits System.Web.UI.Page

   

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim db As New PropertiesDBDataContext()
        Dim prop As [Property]
        Dim pics As List(Of Picture)

        Dim pid As Integer
        pid = 1

        If Not Request.QueryString("id") Is Nothing Then pid = Request.QueryString("id")
        prop = (From p In db.Properties _
                Where p.id = pid _
                Select p).First()

        Dim arr As New List(Of Integer())
        'pics = (From p In db.Pictures _
        '        Where p.property_id = pid _
        '        Select New Integer() {p.id, _
        '                              p.mainPicture, _
        '                              p.secondaryPicture, _
        '                              p.frontPage}).ToList()

        pics = (From p In db.Pictures _
                Where p.property_id = pid _
                Select p).ToList()

        ' Address population
        txtStreet1.Text = prop.Address.street1
        txtStreet2.Text = prop.Address.street2
        txtCity.Text = prop.Address.city
        txtState.Text = prop.Address.state
        txtZip.Text = prop.Address.zip
        txtCountry.Text = prop.Address.country

        ' General Attributes population
        txtDensity.Text = prop.density
        txtArea.Text = prop.area
        txtUnits.Text = prop.units
        txtYearBuilt.Text = prop.yearBuilt
        txtWalkscore.Text = prop.walkscore
        txt250sf.Text = prop.twoFiftySingleFam
        txt250apts.Text = prop.twoFiftyApts
        drpProptertyType.SelectedValue = prop.typeCode
        drpStreetType.SelectedValue = prop.streetCode
        drpSocioEcon.SelectedValue = prop.socioEcon
        drpStreetSaftey.SelectedValue = prop.streetSaftey
        drpBuildingEnclosure.SelectedValue = prop.buildingEnclosure
        drpCommonAreas.SelectedValue = prop.commonAreas
        drpStreetConn.SelectedValue = prop.streetConn
        drpStreetWalk.SelectedValue = prop.streetWalk
        drpNeighCond.SelectedValue = prop.neighCondition
        txtNotes.Text = prop.notes

        'Hidden Attributes
        txtAdminNotes.Text = prop.adminNotes
        txtWalkscoreNotes.Text = prop.walkscoreNotes
        txtNotFinished.Text = prop.notFinished

        For Each p In pics
            Dim picId = p.id.ToString()
            Dim img As New HtmlImage()
            Dim cb1 As New HtmlInputCheckBox()
            Dim cb2 As New HtmlInputCheckBox()
            Dim cb3 As New HtmlInputCheckBox()
            Dim cb4 As New HtmlInputCheckBox()

            img.Src = "../Pic.aspx?id=" + picId
            img.Width = 250

            cb1.ID = "primary" + picId
            cb2.ID = "second" + picId
            cb3.ID = "front" + picId
            cb4.ID = "delete" + picId
            cb1.Checked = IIf(p.mainPicture = 1, True, False)
            cb2.Checked = IIf(p.secondaryPicture = 1, True, False)
            cb3.Checked = IIf(p.frontPage = 1, True, False)
            cb1.EnableViewState = True
            cb2.EnableViewState = True
            cb3.EnableViewState = True
            cb4.EnableViewState = True

            Panel1.Controls.Add(img)
            Panel1.Controls.Add(New LiteralControl("<br />"))
            Panel1.Controls.Add(cb1)
            Panel1.Controls.Add(New LiteralControl("<label>&nbsp;Primary Image</label>"))
            Panel1.Controls.Add(New LiteralControl("<br />"))
            Panel1.Controls.Add(cb2)
            Panel1.Controls.Add(New LiteralControl("<label>&nbsp;Secondary Image</label>"))
            Panel1.Controls.Add(New LiteralControl("<br />"))
            Panel1.Controls.Add(cb3)
            Panel1.Controls.Add(New LiteralControl("<label>&nbsp;Front Page</label>"))
            Panel1.Controls.Add(New LiteralControl("<br />"))
            Panel1.Controls.Add(cb4)
            Panel1.Controls.Add(New LiteralControl("<label>&nbsp;Delete Picture</label>"))
            Panel1.Controls.Add(New LiteralControl("<br />"))
        Next

        Session("db") = db
        Session("prop") = prop
        Session("pics") = pics

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim db As PropertiesDBDataContext
        Dim prop As [Property]
        Dim pics As List(Of Picture)

        db = Session("db")
        prop = Session("prop")
        pics = Session("pics")

        If Not Session("newPic") Is Nothing Then
            prop.Pictures.Add(Session("newPic"))
        End If

        prop.Address.street1 = txtStreet1.Text
        prop.Address.street2 = txtStreet2.Text
        prop.Address.city = txtCity.Text
        prop.Address.state = txtState.Text
        prop.Address.zip = txtZip.Text

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

        For Each p In pics
            Dim picId = p.id.ToString()
            Dim cbid = "primary" + picId
            If Not Panel1.FindControl(cbid) Is Nothing Then
                Dim chkBox As HtmlInputCheckBox
                chkBox = Panel1.FindControl(cbid)
                p.mainPicture = IIf(chkBox.Checked, CShort(1), CShort(0))
            End If
            cbid = "second" + picId
            If Not Panel1.FindControl(cbid) Is Nothing Then
                Dim chkBox As HtmlInputCheckBox
                chkBox = Panel1.FindControl(cbid)
                p.secondaryPicture = IIf(chkBox.Checked, CShort(1), CShort(0))
            End If
            cbid = "front" + picId
            If Not Panel1.FindControl(cbid) Is Nothing Then
                Dim chkBox As HtmlInputCheckBox
                chkBox = Panel1.FindControl(cbid)
                p.frontPage = IIf(chkBox.Checked, CShort(1), CShort(0))
            End If
            cbid = "delete" + picId
            If Not Panel1.FindControl(cbid) Is Nothing Then
                Dim chkBox As HtmlInputCheckBox
                chkBox = Panel1.FindControl(cbid)
                If chkBox.Checked Then
                    db.Pictures.DeleteOnSubmit(p)
                End If
            End If
        Next


        db.SubmitChanges()
        Session("newPic") = Nothing

        If IsPostBack Then
            Response.Redirect("Read.aspx")
        End If

    End Sub
End Class
