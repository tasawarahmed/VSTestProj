<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IttefaqConstructionServices.Default" %>
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

<%--===================
Page Layout Buttons
===================

    <div class ="cell colspan3">
        <asp:Button runat="server" ID="Button10" CssClass="button primary full-size" Text="Capital" />
        <asp:Button runat="server" ID="Button6" CssClass="button primary full-size" Text="Income" />
        <asp:Button runat="server" ID="Button7" CssClass="button primary full-size" Text="Expenses" />
        <asp:Button runat="server" ID="Button8" CssClass="button primary full-size" Text="T & P" />
        <asp:Button runat="server" ID="Button9" CssClass="button primary full-size" Text="Assets" />
    </div>
    <div class ="cell colspan3">
        <asp:Button runat="server" ID="btnActiveSites" CssClass="button success full-size" Text="Site Status" OnClick="btnActiveSites_Click" />
        <asp:Panel runat="server" ID="pnlTest" Visible="false">
            <asp:GridView runat="server" ID="grid1" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" data-role="datatable" data-seraching="true">
                <Columns>
                    <asp:BoundField HeaderText="Site Name" DataField="Name" />
                    <asp:BoundField HeaderText="Site Status" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
            </asp:Panel>
    </div>
    <div class ="cell colspan3">
        <asp:Button runat="server" ID="btnClosedSites" CssClass="button danger full-size" Text="PMs and Site Liabilities" />
        <asp:Button runat="server" ID="Button3" CssClass="button danger full-size" Text="Suppliers" />
        <asp:Button runat="server" ID="Button4" CssClass="button danger full-size" Text="Personal Ledgers" />
    </div>
    <div class ="cell colspan2">
        <asp:Button runat="server" ID="Button5" CssClass="button warning full-size" Text="Cash and Bank Accounts" OnClick="Button5_Click" />
        <asp:Panel runat="server" ID="pnlTest2" Visible="false">
            <asp:GridView runat="server" ID="GridView1" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" data-role="datatable" data-seraching="true">
                <Columns>
                    <asp:BoundField HeaderText="Account Name" DataField="Name" />
                    <asp:BoundField HeaderText="Balance" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
            </asp:Panel>
    </div>--%>
<%--    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnActiveSites" CssClass="button primary full-size" Text="Info - Active Sites" OnClick="btnActiveSites_Click"/>
        <asp:Button runat="server" ID="btnClosedSites" CssClass="button primary full-size" Text="Info - Closed Sites" OnClick="btnClosedSites_Click"/>
    </div>

<%--    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnActiveSites" CssClass="button primary full-size" Text="Info - Active Sites" OnClick="btnActiveSites_Click"/>
        <asp:Button runat="server" ID="btnClosedSites" CssClass="button primary full-size" Text="Info - Closed Sites" OnClick="btnClosedSites_Click"/>
    </div>
--%>
<%--    <div class="cell colspan4">
        <asp:Panel runat="server" ID="pnlPriAccounts" Visible="true">
            <h3>Cover Pages</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbAccounts" OnSelectedIndexChanged="cmbAccounts_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:Button runat="server" ID="btnAccountsOK" CssClass="button primary full-size" Text="OK" OnClick="btnAccountsOK_Click" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPriSiteInfo" Visible="false">
            <div id="pDiv1">
            <h2><%= Session["companyName"].ToString() %></h2>
            <h2><%=cmbAccounts.SelectedItem.ToString() %> : Cover Page</h2>
            <h3>Summary:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridPriSitesInfo" AutoGenerateColumns="False" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  OnRowCommand="gridPriSitesInfo_RowCommand" OnRowDataBound="gridPriSitesInfo_RowDataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status", "{0:0,0.00}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="viewSecDetails" Text='<%# Bind("Status", "{0:0,0.00}") %>' CommandName="view" CommandArgument ='<%# Bind("id") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                </div>
            <asp:Button runat="server" ID="btnPntAccStmnt" CssClass="button primary full-size" Text="Print Cover Page" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv1')" />
            <hr />
        </asp:Panel>
    </div> 

    <div class="cell colspan7">
        <asp:Panel runat="server" ID="pnlSecSiteInfo" Visible="false">
            <div id="pDiv2">
            <h2><%= Session["companyName"].ToString() %></h2>
            <h2>Transaction Details: <%= Session["accountName"].ToString() %></h2>
            <hr />
            <asp:GridView runat="server" ID="gridSecSitesInfo" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False" OnRowDataBound="gridSecSitesInfo_RowDataBound" ShowFooter="True" OnRowCommand="gridSecSitesInfo_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="Date" />
                    <asp:TemplateField HeaderText="TransID" SortExpression="transactionID">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("transactionID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="viewTransaction" Text='<%# Bind("transactionID") %>' CommandName="viewTransaction" CommandArgument ='<%# Bind("transactionID") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="Cheque" HeaderText="Cheque" SortExpression="Cheque">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Debit" SortExpression="debitAmount">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("debitAmount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("debitAmount", "{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="creditAmount">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("creditAmount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("creditAmount", "{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Account Statement" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv2')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTransaction" Visible="false">
            <h3>Transaction Details:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="date" />
                    <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="chequeNumber" HeaderText="Cheque" SortExpression="chequeNumber" />
                    <asp:BoundField DataField="debitAmount" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debitAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="creditAmount" HeaderText="Credit" DataFormatString ="{0:0,0.00}" SortExpression="creditAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id"></asp:SqlDataSource>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTerSiteInfo" Visible="false">
            <h3>Further Details:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTerSitesInfo"  CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">

                <Columns>
                    <asp:BoundField DataField="date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="date" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="chequeNumber" HeaderText="Cheque #" SortExpression="chequeNumber" Visible="False" />
                    <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UoM" HeaderText="UoM" SortExpression="UoM" />
                    <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate" />
                    <asp:BoundField DataField="debitAmount" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debitAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="creditAmount" HeaderText="Credit" DataFormatString="{0:0,0.00}" SortExpression="creditAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>

            </asp:GridView>
            <asp:Button runat="server" ID="Button2" CssClass="button primary full-size" Text="Print Account Statement" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv1')" />
            <hr />
        </asp:Panel>
    </div>--%>
<%-----------------------
PAGE LAYOUT VERSION 2
-----------------------%>
<%--    <div class="cell colspan4">
        <asp:GridView runat="server" ID="gridCoverPage" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" OnRowDataBound="gridCoverPage_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Item Name" SortExpression="Name" />
                <asp:TemplateField HeaderText="Amount in Rs." SortExpression="Debit">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="cell colspan7">
    </div>--%>
<%-----------------------
PAGE LAYOUT VERSION 1
-----------------------%>
    <div class="cell colspan12">
        <h1 class="leader"><%=Session["companyName"].ToString() %> - Accounts Management</h1>
        <hr />
        <br />
        <ul class="step-list">
            <li>
                <h2 class="no-margin-top">Manage Sites</h2>
                <hr class="bg-green" />
                <div>Manage your sites at grassroot level with ease and user friendly interface.</div>
                <br />
            </li>
            <li>
                <h2 class="no-margin-top">Manage Investments</h2>
                <hr class="bg-green" />
                <div>Manage your investments at grassroot level with ease and user friendly interface.</div>
                <br />
            </li>
            <li>
                <h2 class="no-margin-top">Manage Individual Acconts</h2>
                <hr class="bg-green" />
                <div>Manage your individual accounts at grassroot level with ease and user friendly interface.</div>
                <br />
            </li>
            <li>
                <h2 class="no-margin-top">Get a Full View of Your Business</h2>
                <hr class="bg-green" />
                <div>Get a complete view of your business with ease and user friendly interface.</div>
                <br />
            </li>
            <li>
                <h2 class="no-margin-top">Configurable for Expanding Business Needs</h2>
                <hr class="bg-green" />
                <div>Configurable and customizable to meet your needs.</div>
                <br />
            </li>
        </ul>
    </div>

</asp:Content>
