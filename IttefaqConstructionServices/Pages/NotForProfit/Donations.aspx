<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="Donations.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.Donations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Receive Donations:&nbsp<small>Receive donations here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan6">
        <h3>Details</h3>
        <hr />
        <label>Event Type</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbDonor"></asp:DropDownList>
        </div>
        <label>Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
        <label>Amount</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Event" ID="txtAmount"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnEventCreate" CssClass="button primary full-size" Text="OK" OnClick="btnEventCreate_Click" />
    </div>
</asp:Content>
