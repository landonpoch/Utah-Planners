
Partial Class Profile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtFname.Text = Profile.fname
            txtLname.Text = Profile.lname
            txtEmail.Text = Membership.GetUser().Email
            Label1.Text = Membership.GetUser().UserName
            Dim txtUserRoles As String
            Dim userRoles As String()
            txtUserRoles = ""

            ' Figure out how to show the roles
            userRoles = Roles.GetRolesForUser()
            For Each r In userRoles
                txtUserRoles = txtUserRoles + r + ", "
            Next
            Try
                txtUserRoles = txtUserRoles.Remove(txtUserRoles.Length - 2, 2)
            Catch ex As Exception
                txtUserRoles = "user"
            End Try
            Label2.Text = txtUserRoles

            'Initially set to default theme
            If Profile.Theme.Equals("Default") Then
                Profile.Theme = "Default"
            End If
            DropDownList1.SelectedValue = Profile.Theme
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Profile.fname = txtFname.Text
        Profile.lname = txtLname.Text
        ' Figure out how to set the email
        Dim user As MembershipUser
        user = Membership.GetUser()
        user.Email = txtEmail.Text
        Profile.Theme = DropDownList1.SelectedValue
        Membership.UpdateUser(user)
    End Sub
End Class
