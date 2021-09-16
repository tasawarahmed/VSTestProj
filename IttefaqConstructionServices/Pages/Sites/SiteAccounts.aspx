<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SiteAccounts.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SiteAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Site Accounts:&nbsp<small>Open site related accounts here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan6">
        <h3>Site Structure and Hierarchy</h3>
        <hr />
        <label>Primary Heads</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimary" OnSelectedIndexChanged="cmbPrimary_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <hr />
        <label>Secondary Heads</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondary_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Secondary Head" ID="txtSecondaryName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Secondary Head" ID="txtSecondaryDescription"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnSecondaryCreate" CssClass="button primary full-size" Text="OK" OnClick="btnSecondaryCreate_Click" />
        <hr />
        <label>Tertiary Heads</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbTertiary"></asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Tertiary Head" ID="txtTertiaryName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Tertiary Head" ID="txtTertiaryDescription"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnTertiaryCreate" CssClass="button primary full-size" Text="OK" OnClick="btnTertiaryCreate_Click" />
    </div>
</asp:Content>
