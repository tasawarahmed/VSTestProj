<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SubRegions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SubRegions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Sub Regions:&nbsp<small>Add new sub regions here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>Add New Sub Regions</h3>
        <label>Region</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbRegions" AutoPostBack="true" OnSelectedIndexChanged="cmbRegions_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Sub Region Name" ID="txtRegionName"></asp:TextBox>
        </div>
        <label>Description</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Sub Region" ID="txtDescription"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnAddRegion" CssClass="button primary full-size" Text="Add" OnClick="btnAddRegion_Click" />
    </div>

    <div class="cell colspan5">
        <h3>Existing Sub Regions for Selected Region</h3>
        <asp:GridView runat="server" ID="gridRegions" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT subRegionName AS Name, description AS Description FROM tblGenSubRegions WHERE (regionID = @regionID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="cmbRegions" Name="regionID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
