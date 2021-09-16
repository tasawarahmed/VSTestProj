<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Membership/MembershipMaster.master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Membership.NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>New User</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan5">
        <h3>New User</h3>
        <hr />
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size" >
                <asp:TextBox runat="server" placeholder="User Name" ID="txtUserRole"></asp:TextBox>
            </div>
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size" >
                <asp:TextBox runat="server" TextMode="Password" placeholder="Password" ID="txtPassword"></asp:TextBox>
            </div>
            <h3>Add User to Role:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTerSitesInfo"  CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select">
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
                            <asp:Label ID="lblRllName" runat="server" Text='<%# Bind("[Role Name]") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT id, roleName AS [Role Name] FROM tblUserRoles ORDER BY [Role Name]"></asp:SqlDataSource>

            <asp:Button runat="server" ID="btnCreateUser" CssClass="button primary full-size" Text="Create" OnClick="btnCreateUser_Click" />

        </div>
    <div class="cell colspan6">
        <h3>Existing Users</h3>
            <asp:GridView runat="server" ID="gridUsers"  CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="userName" HeaderText="User Name" SortExpression="userName" />
                </Columns>
                </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT [userName] FROM [tblUsers] ORDER BY [userName]"></asp:SqlDataSource>
        </div>
</asp:Content>
