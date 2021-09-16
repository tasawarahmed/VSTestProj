<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Capital/CapitalMaster.master" AutoEventWireup="true" CodeBehind="NewCapitalAccount.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Capital.NewCapitalAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Capital:&nbsp<small>Create new capital account here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan5">
        <h3>Account Info</h3>
        <hr />
        <label>Name</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbAccount" OnSelectedIndexChanged="cmbAccount_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>Address</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtAddress"></asp:TextBox>
        </div>
        <label>ID Card</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtSpecialNotes"></asp:TextBox>
        </div>
        <label>Contact</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtContact"></asp:TextBox>
        </div>
        <label>Email</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
        </div>
        <label>Notes</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
        </div>
        <br />
        <asp:Button runat="server" ID="btnOK" CssClass="button primary full-size" Text="OK" OnClick="btnOK_Click" />
        <hr />
    </div>
    <div class="cell colspan5">
        <h3>Existing Accounts</h3>
        <hr />
        <asp:GridView runat="server" ID="gridRegions" CssClass="table striped hovered border bordered">
        </asp:GridView>
    </div>
</asp:Content>
