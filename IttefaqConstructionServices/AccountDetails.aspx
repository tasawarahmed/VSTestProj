<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="IttefaqConstructionServices.AccountDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Session["accountDetails"].ToString() %></title>
    <link href="Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-colors.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-rtl.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/jquery.js"></script>
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/metro.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/jquery.dataTables.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid">
            <div class="row cells12">
                <div class="cell colspan4">
                    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
                    <br />
                    <h1><%= Session["accountDetails"].ToString() %></h1>
                    <asp:Panel runat="server" ID="pnlPriSiteInfo" Visible="false">
                        <div id="pDiv1">
                            <h2>Cover Page</h2>
                            <h3>Summary:</h3>
                            <hr />
                            <asp:GridView runat="server" ID="gridPriSitesInfo" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" OnRowCommand="gridPriSitesInfo_RowCommand" OnRowDataBound="gridPriSitesInfo_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                        <EditItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status", "{0:0,0.00}") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="viewSecDetails" Text='<%# Bind("Status", "{0:0,0.00}") %>' CommandName="view" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--            <asp:Button runat="server" ID="btnPntAccStmnt" CssClass="button primary full-size" Text="Print Cover Page" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv1')" />
            <hr />--%>
                        <br />
                    </asp:Panel>
                </div>
                <div class="cell colspan8">
                    <h2>Further Information</h2>
                    <asp:Panel runat="server" ID="pnlSecSiteInfo" Visible="false">
                        <div id="pDiv2">
                            <h2>Transaction Details: <%= Session["accountName"].ToString() %></h2>
                            <hr />
                            <asp:GridView runat="server" ID="gridSecSitesInfo" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False" OnRowDataBound="gridSecSitesInfo_RowDataBound" OnRowCommand="gridSecSitesInfo_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="Date" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="TransID" SortExpression="transactionID">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("transactionID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="viewTransaction" Text='<%# Bind("transactionID") %>' CommandName="viewTransaction" CommandArgument='<%# Bind("transactionID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="Cheque" HeaderText="Cheque" Visible="false" SortExpression="Cheque">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Debit" SortExpression="debitAmount">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("debitAmount") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("debitAmount", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit" SortExpression="creditAmount">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("creditAmount") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("creditAmount", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Account Statement" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv2')" />
            <hr />--%>
                        <br />
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlTransaction" Visible="false">
                        <h3>Transaction Details:</h3>
                        <hr />
                        <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                                <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                <asp:BoundField DataField="chequeNumber" HeaderText="Cheque" Visible="false" SortExpression="chequeNumber" />
                                <asp:BoundField DataField="debitAmount" HeaderText="Debit" DataFormatString="{0:n}" SortExpression="debitAmount">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="creditAmount" HeaderText="Credit" DataFormatString="{0:n}" SortExpression="creditAmount">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id"></asp:SqlDataSource>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
