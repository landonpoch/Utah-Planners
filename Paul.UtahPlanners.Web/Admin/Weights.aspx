<%@ Page Title="" Language="VB" MasterPageFile="~/NoSidebar.master" AutoEventWireup="false" CodeFile="Weights.aspx.vb" Inherits="Admin_Weights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:Properties2ConnectionString %>" 
        DeleteCommand="DELETE FROM [Weights] WHERE [id] = @original_id AND (([streetWalk] = @original_streetWalk) OR ([streetWalk] IS NULL AND @original_streetWalk IS NULL)) AND (([walkscore] = @original_walkscore) OR ([walkscore] IS NULL AND @original_walkscore IS NULL)) AND (([twoFiftySingleFam] = @original_twoFiftySingleFam) OR ([twoFiftySingleFam] IS NULL AND @original_twoFiftySingleFam IS NULL)) AND (([buildingEnclosure] = @original_buildingEnclosure) OR ([buildingEnclosure] IS NULL AND @original_buildingEnclosure IS NULL)) AND (([streetConn] = @original_streetConn) OR ([streetConn] IS NULL AND @original_streetConn IS NULL)) AND (([commonAreas] = @original_commonAreas) OR ([commonAreas] IS NULL AND @original_commonAreas IS NULL)) AND (([streetSaftey] = @original_streetSaftey) OR ([streetSaftey] IS NULL AND @original_streetSaftey IS NULL)) AND (([neighCondition] = @original_neighCondition) OR ([neighCondition] IS NULL AND @original_neighCondition IS NULL)) AND (([twoFiftyApts] = @original_twoFiftyApts) OR ([twoFiftyApts] IS NULL AND @original_twoFiftyApts IS NULL))" 
        InsertCommand="INSERT INTO [Weights] ([streetWalk], [walkscore], [twoFiftySingleFam], [buildingEnclosure], [streetConn], [commonAreas], [streetSaftey], [neighCondition], [twoFiftyApts]) VALUES (@streetWalk, @walkscore, @twoFiftySingleFam, @buildingEnclosure, @streetConn, @commonAreas, @streetSaftey, @neighCondition, @twoFiftyApts)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT * FROM [Weights]" 
        UpdateCommand="UPDATE [Weights] SET [streetWalk] = @streetWalk, [walkscore] = @walkscore, [twoFiftySingleFam] = @twoFiftySingleFam, [buildingEnclosure] = @buildingEnclosure, [streetConn] = @streetConn, [commonAreas] = @commonAreas, [streetSaftey] = @streetSaftey, [neighCondition] = @neighCondition, [twoFiftyApts] = @twoFiftyApts WHERE [id] = @original_id AND (([streetWalk] = @original_streetWalk) OR ([streetWalk] IS NULL AND @original_streetWalk IS NULL)) AND (([walkscore] = @original_walkscore) OR ([walkscore] IS NULL AND @original_walkscore IS NULL)) AND (([twoFiftySingleFam] = @original_twoFiftySingleFam) OR ([twoFiftySingleFam] IS NULL AND @original_twoFiftySingleFam IS NULL)) AND (([buildingEnclosure] = @original_buildingEnclosure) OR ([buildingEnclosure] IS NULL AND @original_buildingEnclosure IS NULL)) AND (([streetConn] = @original_streetConn) OR ([streetConn] IS NULL AND @original_streetConn IS NULL)) AND (([commonAreas] = @original_commonAreas) OR ([commonAreas] IS NULL AND @original_commonAreas IS NULL)) AND (([streetSaftey] = @original_streetSaftey) OR ([streetSaftey] IS NULL AND @original_streetSaftey IS NULL)) AND (([neighCondition] = @original_neighCondition) OR ([neighCondition] IS NULL AND @original_neighCondition IS NULL)) AND (([twoFiftyApts] = @original_twoFiftyApts) OR ([twoFiftyApts] IS NULL AND @original_twoFiftyApts IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_streetWalk" Type="Int32" />
            <asp:Parameter Name="original_walkscore" Type="Int32" />
            <asp:Parameter Name="original_twoFiftySingleFam" Type="Int32" />
            <asp:Parameter Name="original_buildingEnclosure" Type="Int32" />
            <asp:Parameter Name="original_streetConn" Type="Int32" />
            <asp:Parameter Name="original_commonAreas" Type="Int32" />
            <asp:Parameter Name="original_streetSaftey" Type="Int32" />
            <asp:Parameter Name="original_neighCondition" Type="Int32" />
            <asp:Parameter Name="original_twoFiftyApts" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="streetWalk" Type="Int32" />
            <asp:Parameter Name="walkscore" Type="Int32" />
            <asp:Parameter Name="twoFiftySingleFam" Type="Int32" />
            <asp:Parameter Name="buildingEnclosure" Type="Int32" />
            <asp:Parameter Name="streetConn" Type="Int32" />
            <asp:Parameter Name="commonAreas" Type="Int32" />
            <asp:Parameter Name="streetSaftey" Type="Int32" />
            <asp:Parameter Name="neighCondition" Type="Int32" />
            <asp:Parameter Name="twoFiftyApts" Type="Int32" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_streetWalk" Type="Int32" />
            <asp:Parameter Name="original_walkscore" Type="Int32" />
            <asp:Parameter Name="original_twoFiftySingleFam" Type="Int32" />
            <asp:Parameter Name="original_buildingEnclosure" Type="Int32" />
            <asp:Parameter Name="original_streetConn" Type="Int32" />
            <asp:Parameter Name="original_commonAreas" Type="Int32" />
            <asp:Parameter Name="original_streetSaftey" Type="Int32" />
            <asp:Parameter Name="original_neighCondition" Type="Int32" />
            <asp:Parameter Name="original_twoFiftyApts" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="streetWalk" Type="Int32" />
            <asp:Parameter Name="walkscore" Type="Int32" />
            <asp:Parameter Name="twoFiftySingleFam" Type="Int32" />
            <asp:Parameter Name="buildingEnclosure" Type="Int32" />
            <asp:Parameter Name="streetConn" Type="Int32" />
            <asp:Parameter Name="commonAreas" Type="Int32" />
            <asp:Parameter Name="streetSaftey" Type="Int32" />
            <asp:Parameter Name="neighCondition" Type="Int32" />
            <asp:Parameter Name="twoFiftyApts" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <h4>Edit these values to change the weighting of the scoring variables:</h4><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" HorizontalAlign="Center">
        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="streetWalk" HeaderText="Street Walkability" 
                SortExpression="streetWalk" />
            <asp:BoundField DataField="walkscore" HeaderText="Walkscore" 
                SortExpression="walkscore" />
            <asp:BoundField DataField="twoFiftySingleFam" HeaderText="250 Single Fam." 
                SortExpression="twoFiftySingleFam" />
            <asp:BoundField DataField="buildingEnclosure" HeaderText="Building Enclosure" 
                SortExpression="buildingEnclosure" />
            <asp:BoundField DataField="streetConn" HeaderText="Street Connectivity" 
                SortExpression="streetConn" />
            <asp:BoundField DataField="commonAreas" HeaderText="Common Areas" 
                SortExpression="commonAreas" />
            <asp:BoundField DataField="streetSaftey" HeaderText="Street Saftey" 
                SortExpression="streetSaftey" />
            <asp:BoundField DataField="neighCondition" HeaderText="Neighborhood Cond" 
                SortExpression="neighCondition" />
            <asp:BoundField DataField="twoFiftyApts" HeaderText="250 Apartments" 
                SortExpression="twoFiftyApts" />
        </Columns>
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:GridView>
    <br />
</asp:Content>

