<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HumanResources/HumanResourcesMaster.master" AutoEventWireup="true" CodeBehind="AllocationPlaces.aspx.cs" Inherits="IttefaqConstructionServices.Pages.HumanResources.AllocationPlaces" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Staff Member Allocation Places:&nbsp<small>Where a staff can be allocated.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>New Allocation Place</h3>
        <hr />
            <label class="input-control checkbox small-check">
                <asp:CheckBox runat="server" ID="chkSwap" Checked="true" AutoPostBack="true" OnCheckedChanged="chkSwap_CheckedChanged" />
                <span class="check"></span>
                <span class="caption">Sites</span>
            </label>
        <asp:Panel runat="server" ID="pnlSites" Visible="true">
            <label>Active Sites</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSites"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlOthers" Visible="false">
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Allocation Place" ID="txtAllocationPlace"></asp:TextBox>
        </div>
        </asp:Panel>
        <hr />
        <asp:Button runat="server" ID="btnCreate" CssClass="button primary full-size" Text="OK" OnClick="btnCreate_Click"  />
        <hr />
    </div>
    <div class="cell colspan5">
        <asp:Panel runat="server" ID="pnlAllocationPlaces" Visible="false">
            <h3>Allocation Places:</h3>
            <asp:GridView runat="server" ID="gridAllocationPlaces" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="allocationPlaceName" HeaderText="Allocation Place Name" SortExpression="allocationPlaceName" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT allocationPlaceName FROM tblGenStaffAllocationPlaces"></asp:SqlDataSource>
        </asp:Panel>
    </div>
</asp:Content>
