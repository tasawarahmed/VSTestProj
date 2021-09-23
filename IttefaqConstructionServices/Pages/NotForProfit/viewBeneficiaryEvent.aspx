<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="viewBeneficiaryEvent.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.viewBeneficiaryEvent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan11">
                <h4>Select an Event</h4>
        <hr />

        <label>Event Name</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbEvent" OnSelectedIndexChanged="cmbEvent_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <asp:Panel runat="server" ID="pnlDetails" Visible="false">
            <div id="pDiv1">
                <h1><%= eventName %></h1>
                <h2>Registered Beneficiaries (Total: <%= totalBeneficiaries %>)</h2>
                <asp:GridView runat="server" ID="gridBeneficiaries" CssClass="table striped hovered border bordered"></asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Details" OnClientClick="printDiv('pDiv1')" />
        </asp:Panel>
    </div>

</asp:Content>
