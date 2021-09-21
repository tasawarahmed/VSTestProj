<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="NewDonor.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.NewDonor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Add a New Donor:&nbsp<small>Add new donor here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan6">
        <h3>Details</h3>
        <hr />
        <label>Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Donor" ID="txtName"></asp:TextBox>
        </div>
        <label>Address</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Address of Donor" ID="txtAddress"></asp:TextBox>
        </div>
        <label>Organization</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Organization of Donor" ID="txtOrganization"></asp:TextBox>
        </div>
        <label>Contact(s)</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Comma Separated Contacts" ID="txtContact"></asp:TextBox>
        </div>
        <label>Email</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Email" ID="txtEmail"></asp:TextBox>
        </div>
        <label>Remarks</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Remarks" ID="txtRemarks"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnDonorCreate" CssClass="button primary full-size" Text="OK" OnClick="btnDonorCreate_Click" />
    </div>
</asp:Content>
