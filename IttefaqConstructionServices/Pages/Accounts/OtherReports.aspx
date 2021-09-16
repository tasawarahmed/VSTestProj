<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="OtherReports.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.OtherReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>General Accounts Reports</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
                        </div>
                    <div class="row cells12">


    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnShowAbdullahSbClosing" CssClass="button primary full-size" Text="Main Closing" OnClick="btnShowAbdullahSbClosing_Click" />
        <asp:Button runat="server" ID="btnCoverSheet" CssClass="button primary full-size" Text="Cover Sheet" OnClick="btnCoverSheet_Click"/>
        <hr />
        <asp:Button runat="server" ID="btnActiveAccounts" CssClass="button primary full-size" Text="Account Statement - Active" OnClick="btnActiveAccounts_Click"/>
        <asp:Button runat="server" ID="btnClosedAccounts" CssClass="button primary full-size" Text="Account Statement - Inactive" OnClick="btnClosedAccounts_Click"/>
        <asp:Button runat="server" ID="btnTransactionsDate" CssClass="button primary full-size" Text="Transactions - Date" OnClick="btnTransactionsDate_Click"  />
        <asp:Button runat="server" ID="btnTransactionRange" CssClass="button primary full-size" Text="Transactions - Range" OnClick="btnTransactionRange_Click" />
        <hr />
        <asp:Button runat="server" ID="btnAccountReconciliationStatement" CssClass="button primary full-size" Text="Reconcile Account" OnClick="btnAccountReconciliationStatement_Click" />
        <hr />
        <asp:Button runat="server" ID="btnChartOfAccounts" CssClass="button primary full-size" Text="Chart of Accounts" OnClick="btnChartOfAccounts_Click" />
        <hr />
        <asp:Button runat="server" ID="btnTrialBalance" CssClass="button primary full-size" Text="Trial Balance" OnClick="btnTrialBalance_Click" />
        <asp:Button runat="server" ID="btnPLS" CssClass="button primary full-size" Text="Profit & Loss" OnClick="btnPLS_Click" />
        <asp:Button runat="server" ID="btnBS" CssClass="button primary full-size" Text="Balance Sheet" OnClick="btnBS_Click" />
        <asp:Button runat="server" ID="btnBSCondensed" CssClass="button primary full-size" Text="Condensed Balance Sheet" OnClick="btnBSCondensed_Click"  />
        <hr />
    </div>

    <div class="cell colspan9">
        <asp:Panel runat="server" ID ="pnlChartOfAccounts" Visible="false">
            <asp:GridView runat="server" ID="gridCoA" CssClass="table striped hovered border bordered" data-role="datatable">

            </asp:GridView>
        </asp:Panel>
        <asp:Panel runat="server" ID ="pnlCoverPage" Visible="false">
            <asp:GridView runat="server" ID="gridCoverPage" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" data-role="datatable" OnRowDataBound="gridCoverPage_RowDataBound">
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
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMainClosing" Visible="false">
            <div id="pMainClosing">
            <h2><%= Session["companyName"].ToString() %></h2>
            <h3>Main Closing</h3>
            <hr />
            <asp:GridView runat="server" ID="gridMainClosing" AutoGenerateColumns="False" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true" OnRowDataBound="gridMainClosing_RowDataBound" ShowFooter="False">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Item Name" SortExpression="Name" />
                    <asp:TemplateField HeaderText="Debit" SortExpression="Debit">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:0,0.00}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Debit", "{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="Creit">
                        <EditItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Credit", "{0:0,0.00}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Credit", "{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button3" CssClass="button primary full-size" Text="Print Cover Page" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pMainClosing')" />
        </asp:Panel>
    </div>

    <div class="cell colspan3">
        <asp:Panel runat="server" ID="pnlPriAccounts" Visible="false">
            <h3>Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbAccounts" AutoPostBack="true" OnSelectedIndexChanged="cmbAccounts_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlBanks" Visible="false">
            <h3>Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimary" OnSelectedIndexChanged="cmbPrimary_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondary_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiary" AutoPostBack ="true" OnSelectedIndexChanged="cmbTertiary_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDate" Visible="false">
            <label>Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlStartEndDate" Visible="false">
            <label>Start Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtStartDate"></asp:TextBox>
            </div>
            <label>End Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtEndDate"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel ID="pnlTrialBalanceOK" runat="server" visible="false">
            <asp:Button runat="server" ID="btnTrialBalanceOK" CssClass="button primary full-size" Text="Get Trial Balance" OnClick="btnTrialBalanceOK_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlReconciliationOK" runat="server" visible="false">
            <asp:Button runat="server" ID="btnReonciliationOK" CssClass="button primary full-size" Text="Get Reconciliation Statement" OnClick="btnReonciliationOK_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlPLSOK" runat="server" visible="false">
            <asp:Button runat="server" ID="btnPLSOK" CssClass="button primary full-size" Text="Get Profit and Loss" OnClick="btnPLSOK_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlBSOK" runat="server" visible="false">
            <asp:Button runat="server" ID="btnBSOK" CssClass="button primary full-size" Text="Get Balance Sheet" OnClick="btnBSOK_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlBSCondOK" runat="server" visible="false">
            <asp:Button runat="server" ID="btnBSCondOK" CssClass="button primary full-size" Text="Get Condensed Balance Sheet" OnClick="btnBSCondOK_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTransactionsDateOK" Visible="false">
            <asp:Button runat="server" ID="btnTransactionsDateOK" CssClass="button primary full-size" Text="Get Transactions" OnClick="btnTransactionsDateOK_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTransactionsRangeOK" Visible="false">
            <asp:Button runat="server" ID="btnTransactionsRangeOK" CssClass="button primary full-size" Text="Get Transactions" OnClick="btnTransactionsRangeOK_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAccCoverPage" Visible="false">
            <div id="pDiv3">
            <h2><%= Session["companyName"].ToString() %></h2>
            <h2><%=cmbAccounts.SelectedItem.ToString() %>: Cover Page</h2>
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
            <asp:Button runat="server" ID="btnPntSummary" CssClass="button primary full-size" Text="Print Cover Page" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv3')" />
        </asp:Panel>
    </div> 

    <div class="cell colspan7">
        <asp:Panel runat="server" ID="pnlReconciliation" Visible="false">
            <h3>Unreconciled Entries:</h3>
            <asp:GridView runat="server" ID="gridReconciliation" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" >
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIDRecon" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="isReconciled">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isReconciled") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkReconciledRecon" runat="server" Checked='<%# Bind("isReconciled") %>' Enabled="true" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="chequeNumber" HeaderText="Chq #" SortExpression="chequeNumber" /> 
                    <asp:BoundField DataField="debitAmount" HeaderText="Debit" SortExpression="debitAmount" DataFormatString ="{0:0,0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="creditAmount" HeaderText="Credit" SortExpression="creditAmount" DataFormatString ="{0:0,0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnUpdateReconciliation" CssClass="button primary full-size" Text="Update Reconcilation Status" OnClick="btnUpdateReconciliation_Click" />
            <hr />
            <h3>Reconciled Entries:</h3>
            <asp:GridView runat="server" ID="gridUnreconciliation" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" >
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIDUnrecon" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="isReconciled">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isReconciled") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkReconciledUnrecon" runat="server" Checked='<%# Bind("isReconciled") %>' Enabled="true" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="chequeNumber" HeaderText="Chq #" SortExpression="chequeNumber" />
                    <asp:BoundField DataField="debitAmount" HeaderText="Debit" SortExpression="debitAmount" DataFormatString ="{0:0,0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="creditAmount" HeaderText="Credit" SortExpression="creditAmount" DataFormatString ="{0:0,0.00}">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnUnreconciliation" CssClass="button primary full-size" Text="Update UnReconcilation Status" OnClick="btnUnreconciliation_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReconciliationStatement" Visible="false">
            <div id="pDiv6">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3>Account Reconciliation: <%=cmbTertiary.SelectedItem.ToString() %></h3>
                <h3>As On: <%=txtDate.Text %></h3>
                <label>Account Balance as per Ledger:</label>
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtBalance"></asp:TextBox>
                </div>
                <label>Total Unreconciled Debits in Bank Statement:</label>
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtUnreconciledDebits"></asp:TextBox>
                </div>
                <asp:GridView runat="server" ID="gridUnreconciledDebits" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                        <asp:BoundField DataField="chequeNumber" HeaderText="Cheque" SortExpression="chequeNumber" />
                        <asp:BoundField DataField="debitAmount" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debitAmount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <label>Total Unreconciled Credits in Bank Statement:</label>
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtUnreconciledCredits"></asp:TextBox>
                </div>
                <asp:GridView runat="server" ID="gridUnreconciledCredits" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                        <asp:BoundField DataField="chequeNumber" HeaderText="Cheque" SortExpression="chequeNumber" />
                        <asp:BoundField DataField="creditAmount" HeaderText="Credit" DataFormatString="{0:0,0.00}" SortExpression="creditAmount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <label>Expected Balance of Bank Statement:</label>
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtBankStatementBalance"></asp:TextBox>
                </div>
            </div>
            <asp:Button runat="server" ID="btnPrintReconciliation" CssClass="button primary full-size" Text="Print Reconciliation" OnClientClick="printDiv('pDiv6')" OnClick="btnPntTrialBalance_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAccountStatement" Visible="false">
            <div id="pDiv1">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3>Account Statement: &nbsp <%=Session["AccountNameForStatement"].ToString() %> </h3>
                <h4>From: <%=txtStartDate.Text %> &nbsp&nbspTo: <%=txtEndDate.Text %></h4>
                <hr />
<%--                <asp:GridView runat="server" ID="GridView1" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False" OnRowCommand="gridAccountStatement_RowCommand" OnRowDataBound="gridAccountStatement_RowDataBound">--%>
                <asp:GridView runat="server" ID="gridAccountStatement" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowCommand="gridAccountStatement_RowCommand" OnRowDataBound="gridAccountStatement_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Date" SortExpression="Date" ItemStyle-Wrap="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="viewTransactionByDate" runat="server" CommandArgument='<%# Bind("Date") %>' CommandName="viewTransactionByDate" Text='<%# Bind("Date", "{0:dd-MM-yyyy}") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trans ID" SortExpression="transactionID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("transactionID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="viewTransaction" runat="server" CommandArgument='<%# Bind("transactionID") %>' CommandName="viewTransaction" Text='<%# Bind("transactionID") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="Cheque" HeaderText="Cheque" Visible="false" SortExpression="Cheque">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Debit" SortExpression="debitAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Debit") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("Debit", "{0:0,0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit" SortExpression="creditAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Credit") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("Credit", "{0:0,0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance" SortExpression="balanceAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Balance") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance", "{0:0,0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnPntAccStmnt" CssClass="button primary full-size" Text="Print Account Statement" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv1')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTransaction" Visible="false">
            <div id="pDiv7">
            <h3>Transaction Details:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" OnRowDataBound="gridTransaction_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="siteName" HeaderText="Site" SortExpression="siteName" />
                    <asp:BoundField DataField="date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="Debit" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debitAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Credit" HeaderText="Credit" DataFormatString ="{0:0,0.00}" SortExpression="creditAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="oprator" HeaderText="Operator" SortExpression="operator" />
                </Columns>
            </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnPntTransactionDetails" CssClass="button primary full-size" Text="Print Transaction Details"  OnClientClick="printDiv('pDiv7')" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id"></asp:SqlDataSource>
        </asp:Panel>
        <asp:Panel ID="pnlTrialBalance" runat="server" Visible="false">
            <div id="pDiv2">
            <h2><%= Session["companyName"].ToString() %></h2>
            <h3>Trial Balance as on: &nbsp <%=txtDate.Text %></h3>
            <hr />
            <asp:GridView runat="server" ID="gridTrialBalance" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowDataBound="gridTrialBalance_RowDataBound" ShowFooter="True" >
                <Columns>
                    <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                    <asp:TemplateField HeaderText="Debit" SortExpression="debit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"  Text='<%# Bind("debit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("debit", "{0:0,0.00}") %>'  ></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="credit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("credit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("credit", "{0:0,0.00}") %>' ></asp:Label>
                        </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnPntTrialBalance" CssClass="button primary full-size" Text="Print Trial Balance" OnClientClick="printDiv('pDiv2')" OnClick="btnPntTrialBalance_Click" />
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
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPLS" Visible="false">
            <div id="pDiv4">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3>Profit and Lost Account</h3>
                <h3>From: <%=txtStartDate.Text %>  To: <%=txtEndDate.Text %></h3>
                <label>Profit for the Period</label>
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPLS"></asp:TextBox>
                </div>
                <hr />
                <asp:GridView runat="server" ID="gridPLS" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowDataBound="gridPLS_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                        <asp:BoundField DataField="Debit" HeaderText="Debit" SortExpression="Debit" DataFormatString="{0:0,0.00}">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Credit" HeaderText="Credit" SortExpression="Credit" DataFormatString="{0:0,0.00}">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Profit and Loss" OnClientClick="printDiv('pDiv4')" OnClick="btnPntTrialBalance_Click" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlBS" Visible="false">
            <div id="pDiv5">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3>Balance Sheet</h3>
                <h3>As On: <%=txtDate.Text %></h3>
                <hr />
                <asp:GridView runat="server" ID="gridBS" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridBS_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Date" />
                    <asp:BoundField DataField="Debit" HeaderText="Debit" SortExpression="Debit" DataFormatString="{0:0,0.00}" >
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Credit" HeaderText="Credit" SortExpression="Credit" DataFormatString="{0:0,0.00}" >
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button2" CssClass="button primary full-size" Text="Print Balance Sheet" OnClientClick="printDiv('pDiv5')" OnClick="btnPntTrialBalance_Click" />
        </asp:Panel>
    </div>
</asp:Content>