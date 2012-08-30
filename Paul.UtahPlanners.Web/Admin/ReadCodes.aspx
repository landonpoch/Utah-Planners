<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="ReadCodes.aspx.vb" Inherits="Codes_ReadCodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="CommonCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [CommonCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [CommonCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [CommonCodes]" 
        UpdateCommand="UPDATE [CommonCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="EnclosureCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [EnclosureCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [EnclosureCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [EnclosureCodes]" 
        UpdateCommand="UPDATE [EnclosureCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="NeighborhoodCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [NeighborhoodCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [NeighborhoodCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [NeighborhoodCodes]" 
        UpdateCommand="UPDATE [NeighborhoodCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StreetconnCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [StreetconnCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [StreetconnCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [StreetconnCodes]" 
        UpdateCommand="UPDATE [StreetconnCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StreetSafteyCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [StreetSafteyCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [StreetSafteyCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [StreetSafteyCodes]" 
        UpdateCommand="UPDATE [StreetSafteyCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StreetwalkCodes" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [StreetwalkCodes] WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))" 
        InsertCommand="INSERT INTO [StreetwalkCodes] ([description], [weight]) VALUES (@description, @weight)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [StreetwalkCodes]" 
        UpdateCommand="UPDATE [StreetwalkCodes] SET [description] = @description, [weight] = @weight WHERE [id] = @original_id AND (([description] = @original_description) OR ([description] IS NULL AND @original_description IS NULL)) AND (([weight] = @original_weight) OR ([weight] IS NULL AND @original_weight IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_description" Type="String" />
            <asp:Parameter Name="original_weight" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="description" Type="String" />
            <asp:Parameter Name="weight" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>CommonCodes</asp:ListItem>
        <asp:ListItem>EnclosureCodes</asp:ListItem>
        <asp:ListItem>NeighborhoodCodes</asp:ListItem>
        <asp:ListItem>StreetconnCodes</asp:ListItem>
        <asp:ListItem>StreetSafteyCodes</asp:ListItem>
        <asp:ListItem>StreetwalkCodes</asp:ListItem>
    </asp:DropDownList><br /><br />

    <asp:Button ID="Button1" runat="server" Text="Change Code Type" /><br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="CommonCodes">
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
            <asp:BoundField DataField="weight" HeaderText="Weight" 
                SortExpression="weight" />
        </Columns>
    </asp:GridView>
    
        
</asp:Content>

