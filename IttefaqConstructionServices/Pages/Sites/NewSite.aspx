<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="NewSite.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.NewSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Sites:&nbsp<small>Create new sites here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan3">
        <h3>New Site</h3>
        <hr />
        <label>Active Sites</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbActiveSites" AutoPostBack="true" OnSelectedIndexChanged="cmbActiveSites_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>Site Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtSiteName"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnNewSite" CssClass="button primary full-size" Text="Add" OnClick="btnNewSite_Click"/>
        <h3>Site Info</h3>
        <hr />
        <label>Address</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtSiteAddress"></asp:TextBox>
        </div>
        <label>Contact Number</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtSiteContacts"></asp:TextBox>
        </div>
        <label>Region</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbRegions" AutoPostBack="true" OnSelectedIndexChanged="cmbRegions_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>Sub Region</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbSubRegions"></asp:DropDownList>
        </div>
        <label>Project Manager Name</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtPMName"></asp:TextBox>
        </div>
        <label>Notes</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
        </div>
        <br />
        <asp:Button runat="server" ID="btnEditSite" CssClass="button primary full-size" Text="OK" OnClick="btnEditSite_Click"/>
        <hr />
    </div>
    <div class="cell colspan8">
        <h3>Existing Sites</h3>
        <hr />
        <asp:GridView runat="server" ID="gridRegions" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" DataSourceID="SqlDataSource1" data-role="datatable" data-seraching="false" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" SortExpression="Site ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Manager" HeaderText="Manager" SortExpression="Manager" />
                <asp:BoundField DataField="Contacts" HeaderText="Contacts" SortExpression="Contacts" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT id, siteName AS Name, siteAddress AS Address, siteProjectManagerName AS Manager, siteContacts AS Contacts FROM tblGenSites WHERE (isactive = 'true') ORDER BY Name"></asp:SqlDataSource>
    </div>
</asp:Content>
