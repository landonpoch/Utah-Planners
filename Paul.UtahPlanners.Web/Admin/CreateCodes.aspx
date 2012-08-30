<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="CreateCodes.aspx.vb" Inherits="Codes_CreateCodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <label for="codes" class="builddetail">Codes:</label>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>Common Codes</asp:ListItem>
        <asp:ListItem>Enclosure Codes</asp:ListItem>
        <asp:ListItem>Neighborhood Codes</asp:ListItem>
        <asp:ListItem>Streetconn Codes</asp:ListItem>
        <asp:ListItem>Street Saftey Codes</asp:ListItem>
        <asp:ListItem>Streetwalk Codes</asp:ListItem>
        </asp:DropDownList><br />
    
    <label for="desc" class="builddetail">Description:</label>
    <asp:TextBox ID="desc" runat="server"></asp:TextBox><br />
    
    <label for="weigh" class="builddetail">Weight:</label>
    <asp:TextBox ID="weigh" runat="server"></asp:TextBox><br />
    
    <label for="Button1" class="builddetail">&nbsp;</label>
    <asp:Button ID="Button1" runat="server" Text="Submit" />
</asp:Content>

