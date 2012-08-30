<%@ Page Title="" Language="VB" MasterPageFile="~/Sidebar.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <h4>USER DETAILS</h4><br />
    <label for="" class="builddetail">First Name:</label>
    <asp:TextBox ID="txtFname" runat="server"></asp:TextBox>
    <br />
    
    <label for="" class="builddetail">Last Name:</label>
    <asp:TextBox ID="txtLname" runat="server"></asp:TextBox>
    <br />
    
    <label for="" class="builddetail">User Name:</label>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    
    <label for="" class="builddetail">Email:</label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Must be a valid email." ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]+[-._+&amp;])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$"></asp:RegularExpressionValidator>
    <br />
    
    <label for="" class="builddetail">Role:</label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    
    <label for="dropdownlist1" class="builddetail">Theme:</label>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>Default</asp:ListItem>
        <asp:ListItem>Orange</asp:ListItem>
        <asp:ListItem>Red</asp:ListItem>
        <asp:ListItem>Blue</asp:ListItem>
        <asp:ListItem>Green</asp:ListItem>
    </asp:DropDownList><br />
    
    <label for="Button1" class="builddetail">&nbsp;</label>
    <asp:Button ID="Button1" runat="server" Text="Submit" /><br /><br />
    
    
    
    <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="#F7F7DE" 
        BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="10pt">
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:ChangePassword>
</asp:Content>

