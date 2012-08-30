<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Read.aspx.vb" Inherits="Properties_Read" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
function DeleteProperty(id)
{
    choice = confirm("Are you sure you want to delete property " + id + "?")
    if (choice == true) {
        window.location = "Delete.aspx?id=" + id
    }
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" SelectCommand="SELECT	'&lt;a href=&quot;Update.aspx?id=' + CAST(Properties.id AS NVARCHAR(MAX)) + '&quot;&gt;Edit&lt;/a&gt;' edit,
	'&lt;a href=&quot;javascript:DeleteProperty(' + CAST(Properties.id AS NVARCHAR(MAX)) + ');&quot;&gt;Delete&lt;/a&gt;' [delete],
	Properties.id,
	[Address].city,
	PropertyTypes.[description], 
	Properties.density, 
	Properties.units, 
	Properties.yearBuilt,
                   Properties.adminNotes,
	Properties.notFinished
FROM [Address] 
INNER JOIN Properties ON Address.id = Properties.id 
INNER JOIN PropertyTypes ON Properties.typeCode = PropertyTypes.id 
INNER JOIN StreetTypes ON Properties.streetCode = StreetTypes.id">
    </asp:SqlDataSource>


    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" AllowSorting="True" 
        HorizontalAlign="Center">
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:BoundField DataField="edit" HeaderText="Edit" ReadOnly="True" 
                SortExpression="edit" HtmlEncode="false" />
            <asp:BoundField DataField="delete" HeaderText="Delete" ReadOnly="True" 
                SortExpression="delete" HtmlEncode="false" />
            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
            <asp:BoundField DataField="description" HeaderText="Description" 
                SortExpression="description" />
            <asp:BoundField DataField="density" HeaderText="Density" 
                SortExpression="density" />
            <asp:BoundField DataField="units" HeaderText="Units" SortExpression="units" />
            <asp:BoundField DataField="yearBuilt" HeaderText="Year Built" 
                SortExpression="yearBuilt" />
            <asp:BoundField DataField="adminNotes" HeaderText="Admin Notes" 
                SortExpression="adminNotes" />
            <asp:BoundField DataField="notFinished" HeaderText="Not Finished" 
                SortExpression="notFinished" />
        </Columns>
        <HeaderStyle BackColor="#CCCCCC" HorizontalAlign="Center" 
            VerticalAlign="Middle" />
    </asp:GridView>
    <br />

</asp:Content>

