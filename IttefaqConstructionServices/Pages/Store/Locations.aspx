<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Store/StoreMaster.master" AutoEventWireup="true" CodeBehind="Locations.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Store.Locations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Inventory Items Locations:&nbsp<small>Manage vatious locations here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan4">
        <h3>Locations</h3>
        <hr />
        <label>New Location</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtNewLocation"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddLocation" CssClass="button primary full-size" Text="Add" OnClick="btnAddLocation_Click" />
        <hr />
    </div>
    <div class="cell colspan6">
        <h3>Existing Locations</h3>
        <hr />
        <asp:GridView runat="server" ID="gridExistingLocations" CssClass="table striped hovered border bordered">
        </asp:GridView>
        <hr />
    </div>
</asp:Content>
