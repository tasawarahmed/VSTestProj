<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" Inherits="IttefaqConstructionServices.Pages.NotForProfit.FinalizeEvent" CodeBehind="FinalizeEvent.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Finalize an Event:&nbsp<small>Finalize events after happening here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
        <h3>Details</h3>
        <hr />
    <div class="cell colspan6">
        <label>Event Name</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbEvent"></asp:DropDownList>
        </div>
    </div>
    <div class="cell colspan5">
        <label>Event Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
    </div>
        <asp:Button runat="server" ID="btnEventFinalize" CssClass="button primary full-size" Text="Finalize" />

</asp:Content>
