<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="CreateTypes.aspx.vb" Inherits="Types_CreateTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <label for="type" class="builddetail">Type:</label>
    <asp:DropDownList ID="type" runat="server">
        <asp:ListItem>Property Type</asp:ListItem>
        <asp:ListItem>Street Type</asp:ListItem>
        <asp:ListItem>Socio-Econ Type</asp:ListItem>
    </asp:DropDownList><br />
    
    <label for="description" class="builddetail">Description:</label>
    <asp:TextBox ID="description" runat="server"></asp:TextBox><br />
    <label for="button1" class="builddetail">&nbsp;</label><asp:Button ID="Button1" runat="server" Text="Submit" />
</asp:Content>

