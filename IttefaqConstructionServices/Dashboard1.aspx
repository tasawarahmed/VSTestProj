<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard1.aspx.cs" Inherits="IttefaqConstructionServices.Dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-colors.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-rtl.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/jquery.js"></script>
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/metro.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/jquery.dataTables.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContents" runat="server">
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan3">
        <asp:GridView runat="server" ID="GridView1" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Site Name" DataField="Name" />
<%--                <asp:BoundField HeaderText="Status" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />--%>
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument ='<%# Bind("description") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="cell colspan3">
        <asp:GridView runat="server" ID="GridView2" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound"  OnRowCommand="GridView_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Account Name" DataField="Name" />
<%--                <asp:BoundField HeaderText="Balance" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />--%>
                <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument ='<%# Bind("description") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="cell colspan3">
        <asp:GridView runat="server" ID="GridView3" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView3_RowDataBound"  OnRowCommand="GridView_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Account Name" DataField="Name" />
                <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument ='<%# Bind("description") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="cell colspan2">
        <asp:GridView runat="server" ID="GridView4" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView4_RowDataBound"  OnRowCommand="GridView_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Account Name" DataField="Name" />
                <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument ='<%# Bind("description") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
