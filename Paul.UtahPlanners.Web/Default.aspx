<%@ Page Title="" Language="VB" MasterPageFile="~/Sidebar.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <a href="Home.aspx?id=<%=propId %>"><img alt="" class="resizeMainPicture" src="Pic.aspx?id=<%=picId %>" /></a>
    <br /><br />
    <p>
    Presented here are over 300 examples of properties throughout Salt Lake, Davis and Utah counties. For each property, aerial and ground photos are shown as well as certain qualitative factors.
    </p>
    <p>
    Properties are searchable by factors such as age, density, number of units, size and others. A search may be performed with only one factor or with many factors combined.
    </p>
</asp:Content>

