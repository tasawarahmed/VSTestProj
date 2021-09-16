<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Store/StoreMaster.master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Store.Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Inventory Items Management:&nbsp<small>Manage categories and items here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan4">
        <h3>Categories</h3>
        <hr />
        <label>Select a Category</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbCategories" OnSelectedIndexChanged="cmbCategories_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>New</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtNewCategory"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddCetegory" CssClass="button primary full-size" Text="Add" OnClick="btnAddCetegory_Click" />
        <hr />
        <h3>Items</h3>
        <hr />
        <label>Items under Selected Category</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbItems"></asp:DropDownList>
        </div>
        <label>New Item</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtNewItem"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddItem" CssClass="button primary full-size" Text="Add" OnClick="btnAddItem_Click" />
        <hr />
    </div>
    <div class="cell colspan6">
        <h3>Existing Categories</h3>
        <hr />
        <asp:GridView runat="server" ID="gridExistingCategories" CssClass="table striped hovered border bordered">
        </asp:GridView>
        <hr />
    </div>
</asp:Content>
