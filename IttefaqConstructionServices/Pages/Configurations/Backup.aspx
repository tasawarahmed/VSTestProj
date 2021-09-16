<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Configurations/ConfigurationsMaster.master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Configurations.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Backup:&nbsp<small>Take backup of your database.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>Backup</h3>
        <label>Path</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Backup Path" ID="txtPath"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Database Name" ID="txtName"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnTakeBackup" CssClass="button primary full-size" Text="Backup Now" OnClick="btnTakeBackup_Click" />
    </div>
</asp:Content>
