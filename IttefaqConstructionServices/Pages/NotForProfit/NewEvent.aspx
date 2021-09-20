<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" Inherits="IttefaqConstructionServices.Pages.NotForProfit.NewEvent" Codebehind="NewEvent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Add a New Event:&nbsp<small>Add new events here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan6">
        <h3>Details</h3>
        <hr />
        <label>Event Type</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrograms"></asp:DropDownList>
        </div>
        <label>Event Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Event" ID="txtEventName"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnEventCreate" CssClass="button primary full-size" Text="OK" OnClick="btnEventCreate_Click" />
    </div>

</asp:Content>
