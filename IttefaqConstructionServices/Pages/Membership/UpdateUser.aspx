<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Membership/MembershipMaster.master" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Membership.UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Update User Information</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan3">
        <h3>Active Users:</h3>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbActiveUsers" OnSelectedIndexChanged="cmbActiveUsers_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="cell colspan4">
        <h3>Update Password:</h3>
        <hr />
        <%--            <label>Date</label>--%>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" TextMode="Password" placeholder="New Password" ID="txtPassword"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnUpdatePassword" CssClass="button primary full-size" Text="Update Password" OnClick="btnUpdatePassword_Click" />
        <hr />
        <h3>Existing Roles:</h3>
        <hr />
        <asp:GridView runat="server" ID="gridTerSitesInfo" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBx" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Role Name" SortExpression="Role Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("[Role Name]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRN" runat="server" Text='<%# Bind("[Role Name]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnUpdateRoles" CssClass="button primary full-size" Text="Update Roles" OnClick="btnUpdateRoles_Click" />
    </div>
    <div class="cell colspan4">
        <h3>Update Sites (Only PMs)</h3>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbPMs"></asp:DropDownList>
        </div>
        <asp:GridView runat="server" ID="gridRegions" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" DataSourceID="SqlDataSource1" data-role="datatable" data-seraching="false">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSite" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" SortExpression="Site ID" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnUpdateSites" CssClass="button primary full-size" Text="Update Sites" OnClick="btnUpdateSites_Click" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT id, siteName AS Name FROM tblGenSites WHERE (isactive = 'true') ORDER BY Name"></asp:SqlDataSource>
    </div>


</asp:Content>
