<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SiteStatus.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SiteStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Change Site Status:&nbsp<small>Activate and Deactivate Site.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>Active Sites</h3>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbActiveAccounts"></asp:DropDownList>
        </div>
        <asp:Button runat="server" ID="btnClose" CssClass="button primary full-size" Text="Close" OnClick="btnClose_Click" />
        <hr />
    </div>
    <div class="cell colspan5">
        <h3>Inactive Sites</h3>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbInactive"></asp:DropDownList>
        </div>
        <asp:Button runat="server" ID="btnReOpen" CssClass="button primary full-size" Text="Re Open" OnClick="btnReOpen_Click" />
    </div> 
</asp:Content>
