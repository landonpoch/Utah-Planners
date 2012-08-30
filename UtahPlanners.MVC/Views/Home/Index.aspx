<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Base.Master" Inherits="System.Web.Mvc.ViewPage<List<Property>>" %>
<%@ Import Namespace="Property=UtahPlanners.Repository.Property" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="PageContent" runat="server">
    
    <table cellspacing="0" align="center" rules="all" border="1">
    <tbody>
        
        <%
            foreach (Property property in Model)
            {
                this.Writer.Write("<tr align='center' valign='middle' style='height: 35px;'>");
                this.Writer.Write("<td>");
                this.Writer.Write(property.id + "<br />");
                this.Writer.Write("</td>");
                this.Writer.Write("</tr>");
            }    
        %>
        
    </tbody>
    </table>
</asp:Content>
