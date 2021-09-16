<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Store/StoreMaster.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Store.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Reports:&nbsp<small>Check store reports here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnStockTotal" CssClass="button primary full-size" Text="Show Total Stock" OnClick="btnStockTotal_Click" />
        <asp:Button runat="server" ID="btnStockLocationWise" CssClass="button primary full-size" Text="Show Stock at Location" OnClick="btnStockLocationWise_Click" />
        <asp:Button runat="server" ID="btnTransactionsOfItem" CssClass="button primary full-size" Text="Movement - Item" OnClick="btnTransactionsOfItem_Click"/>
        <asp:Button runat="server" ID="btnTransactionsOfLocation" CssClass="button primary full-size" Text="Movements - Location" OnClick="btnTransactionsOfLocation_Click"/>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlCategories" Visible="false">
            <label>Categories</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCategories" AutoPostBack="true" OnSelectedIndexChanged="cmbCategories_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSubCategories" Visible="false">
            <label>Sub Categories</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSubCategories" AutoPostBack="true" OnSelectedIndexChanged="cmbSubCategories_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLocation" Visible="false">
            <label>Source Location</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSourceLocation" AutoPostBack="true" OnSelectedIndexChanged="cmbSourceLocation_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLocationForTransactions" Visible="false">
            <label>Source Location</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbLocationsForTransactions" AutoPostBack="true" OnSelectedIndexChanged="cmbLocationsForTransactions_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
    </div>

    <div class="cell colspan6">
        <asp:GridView runat="server" ID="gridInventory" CssClass="table striped hovered border bordered">
        </asp:GridView>
    </div> 
</asp:Content>