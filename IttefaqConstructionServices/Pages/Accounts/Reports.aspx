<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Accounts Reports</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

<%--    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnActiveSites" CssClass="button primary full-size" Text="Info - Active Sites" OnClick="btnActiveSites_Click"/>
        <asp:Button runat="server" ID="btnClosedSites" CssClass="button primary full-size" Text="Info - Closed Sites" OnClick="btnClosedSites_Click"/>
    </div>
--%>
    <div class="cell colspan4">
        <asp:Panel runat="server" ID="pnlPriAccounts" Visible="true">
            <h3>Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbAccounts" AutoPostBack="true" OnSelectedIndexChanged="cmbAccounts_SelectedIndexChanged"></asp:DropDownList>
            </div>
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
                    <asp:BoundField DataField="Date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="Date" ItemStyle-Wrap="false" />
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
                    <asp:BoundField DataField="date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
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
    </div>
</asp:Content>