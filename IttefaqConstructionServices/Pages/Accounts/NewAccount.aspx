<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="NewAccount.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.NewAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>New Account:&nbsp<small>Open New Accounts Here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan6">
        <h3>Account Details and Hierarchy</h3>
        <hr />
        <label>Classification</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimary" OnSelectedIndexChanged="cmbPrimary_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <hr />
        <label>Categories</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondary_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Secondary Account" ID="txtSecondaryName"></asp:TextBox>
        </div>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbAccountType">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem>Cash</asp:ListItem>
                <asp:ListItem>Banks</asp:ListItem>
                <asp:ListItem>Investments</asp:ListItem>
                <asp:ListItem>Loans</asp:ListItem>
                <asp:ListItem>Capital</asp:ListItem>
                <asp:ListItem>Taxes</asp:ListItem>
                <asp:ListItem>Suppliers</asp:ListItem>
                <asp:ListItem>Site Liabilities</asp:ListItem>
                <asp:ListItem>Personal Ledgers</asp:ListItem>
                <asp:ListItem>Project Manager</asp:ListItem>
                <asp:ListItem>Others</asp:ListItem>                
            </asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Secondary Account" ID="txtSecondaryDescription"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnSecondaryCreate" CssClass="button primary full-size" Text="OK" OnClick="btnSecondaryCreate_Click" />
        <hr />
        <label>Accounts</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbTertiary"></asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Tertiary Account" ID="txtTertiaryName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Description of Tertiary Account" ID="txtTertiaryDescription"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnTertiaryCreate" CssClass="button primary full-size" Text="OK" OnClick="btnTertiaryCreate_Click" />
    </div>
</asp:Content>
