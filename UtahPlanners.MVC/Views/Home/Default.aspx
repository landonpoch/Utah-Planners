<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:HyperLink ID="MainPictureHyperLink" runat="server" NavigateUrl="/"><img class="resizeMainPicture" src="<%: ViewData["MainImageUrl"] %>" alt="" /></asp:HyperLink>
    <br /><br />
	<p>Presented here are over 300 examples of properties throughout Salt Lake, Davis and Utah counties. For each property, aerial and ground photos are shown as well as certain qualitative factors.</p>
    <p>Properties are searchable by factors such as age, density, number of units, size and others. A search may be performed with only one factor or with many factors combined.</p>
</asp:Content>
