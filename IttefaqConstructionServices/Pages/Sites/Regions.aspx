<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="Regions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.Regions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Regions:&nbsp<small>Add new regions here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>Add New Regions</h3>
        <label>Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Region Name" ID="txtRegionName"></asp:TextBox>
        </div>
        <label>Description</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Region" ID="txtDescription"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnAddRegion" CssClass="button primary full-size" Text="Add New Region" OnClick="btnAddRegion_Click" />
    </div>

    <div class="cell colspan5">
        <h3>Existing Regions</h3>
        <asp:GridView runat="server" ID="gridRegions" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="regionName" HeaderText="Region Name" SortExpression="regionName" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT [regionName], [remarks] FROM [tblGenRegions] ORDER BY [regionName]"></asp:SqlDataSource>
    </div>

</asp:Content>
