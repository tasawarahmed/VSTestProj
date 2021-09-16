<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Membership/MembershipMaster.master" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Membership.UserRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>User Roles</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan4">
        <h3>New User Role</h3>
        <hr />
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size" >
                <asp:TextBox runat="server" placeholder="User Role" ID="txtUserRole"></asp:TextBox>
            </div>

            <asp:Button runat="server" ID="btnCreateUser" CssClass="button primary full-size" Text="Create" OnClick="btnCreateUser_Click" />

        </div>
    <div class="cell colspan7">
            <h3>Existing Roles:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTerSitesInfo"  CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Role Name" HeaderText="Role Name" SortExpression="Role Name" />
                </Columns>

            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT roleName AS [Role Name] FROM tblUserRoles ORDER BY [Role Name]"></asp:SqlDataSource>
        </div>
</asp:Content>
