<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Admin.aspx.vb" Inherits="Admin_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="float:left">
    <h3>Manage Properties</h3>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Properties/Create.aspx">Create New Property</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Properties/Read.aspx">Edit Properties</asp:HyperLink><br /><br />

    <h3>Manage Types</h3>
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/CreateTypes.aspx">Create New Type</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/ReadTypes.aspx">Edit Types</asp:HyperLink><br /><br />

    <h3>Manage Codes</h3>
    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Admin/CreateCodes.aspx">Create New Codes</asp:HyperLink><br />
    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Admin/ReadCodes.aspx">Edit Codes</asp:HyperLink><br /><br />

    <h3>Manage Weights</h3>
    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Admin/Weights.aspx">Edit Weight Values</asp:HyperLink><br /><br />
    
    <h3>Download Spreadsheet</h3>
    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Admin/Spreadsheet.ashx">Property Details.csv</asp:HyperLink><br /><br />
</div>
<div class="floatright">
<img alt="" src="../Pic.aspx?id=4503" />
</div>
</asp:Content>

