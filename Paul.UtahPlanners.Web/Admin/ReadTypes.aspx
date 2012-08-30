<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="ReadTypes.aspx.vb" Inherits="Types_ReadTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="PropertyTypes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [PropertyTypes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))" 
        InsertCommand="INSERT INTO [PropertyTypes] ([description]) VALUES (@description)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [PropertyTypes]" 
        UpdateCommand="UPDATE [PropertyTypes] SET [description] = @description WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StreetTypes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [StreetTypes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))" 
        InsertCommand="INSERT INTO [StreetTypes] ([description]) VALUES (@description)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [StreetTypes]" 
        UpdateCommand="UPDATE [StreetTypes] SET [description] = @description WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SocioEconCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [SocioEconCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))" 
        InsertCommand="INSERT INTO [SocioEconCodes] ([description]) VALUES (@description)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [SocioEconCodes]" 
        UpdateCommand="UPDATE [SocioEconCodes] SET [description] = @description WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>PropertyTypes</asp:ListItem>
        <asp:ListItem>StreetTypes</asp:ListItem>
        <asp:ListItem>SocioEconCodes</asp:ListItem>
    </asp:DropDownList>
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Select Different Type" />
    <br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="PropertyTypes">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                        CommandName="Delete" Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this entry?");'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="description" HeaderText="Description" 
                SortExpression="description" />
        </Columns>
    </asp:GridView>
</asp:Content>

