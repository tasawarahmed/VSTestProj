<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.Transactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
    <script src="../../Scripts/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            //selector: "textarea",
            mode: "textareas",
            plugins: "print",
            toolbar: "print",
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Routine Transactions:&nbsp<small>Enter routine transactions here.</small></h1>
    <h2><asp:Label runat="server" ID="lblHeading" Text=""></asp:Label></h2>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <div class="accordion" data-role="accordion" data-close-any="true">
            <div class="frame">
                <div class="heading">Journal Entry</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnJournalEntry" CssClass="button success full-size" Text="Journal Entry - Menus" OnClick="btnJournalEntry_Click" />
                    <asp:Button runat="server" ID="btnJournalEntryIDs" CssClass="button success full-size" Text="Journal Entry - IDs" OnClick="btnJournalEntryIDs_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Capital Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnPayToCapital" CssClass="button success full-size" Text="Pay to Capital" OnClick="btnPayToCapital_Click" />
                    <asp:Button runat="server" ID="btnReceiveFromCapital" CssClass="button success full-size" Text="Receive from Capital" OnClick="btnReceiveFromCapital_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Investor Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnPayToInvestor" CssClass="button success full-size" Text="Pay to Investor" OnClick="btnPayToInvestor_Click" />
                    <asp:Button runat="server" ID="btnReceiveFromIvestors" CssClass="button success full-size" Text="Receive from Investor" OnClick="btnReceiveFromIvestors_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Individual Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnPayToIndividual" CssClass="button success full-size" Text="Pay to Individual" OnClick="btnPayToIndividual_Click" />
                    <asp:Button runat="server" ID="btnReceiveFromIndividual" CssClass="button success full-size" Text="Receive from Individual" OnClick="btnReceiveFromIndividual_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Loan Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnGiveAwayLoan" CssClass="button success full-size" Text="Take Loan" OnClick="btnGiveAwayLoan_Click" />
                    <asp:Button runat="server" ID="btnReceiveBackLoan" CssClass="button success full-size" Text="Give Back Loan" OnClick="btnReceiveBackLoan_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Bank Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnDepositInBank" CssClass="button success full-size" Text="Deposit in Bank" OnClick="btnDepositInBank_Click" />
                    <asp:Button runat="server" ID="btnWithdrawalFromBank" CssClass="button success full-size" Text="Withdrawal from Bank" OnClick="btnWithdrawalFromBank_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Supplier Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnPayToSuppliers" CssClass="button success full-size" Text="Pay to Supplier" OnClick="btnPayToSuppliers_Click" />
                    <asp:Button runat="server" ID="btnReceiveFromSupplier" CssClass="button success full-size" Text="Receive from Supplier" OnClick="btnReceiveFromSupplier_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Income Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnReceiveIncome" CssClass="button success full-size" Text="Receive Income" OnClick="btnReceiveIncome_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Expenses Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnPayExpenses" CssClass="button success full-size" Text="Pay Expenses" OnClick="btnPayExpenses_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="cell colspan4">
        <asp:Panel runat="server" Visible="false" ID="pnlDate">
<%--            <label>Date</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
<%--            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>--%>
<%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Amount" ID="txtAmount"  ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
            </div>
<%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Cheque Number if any" ID="txtCheequeNumber"></asp:TextBox>
            </div>
<%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Description of Transaction" ID="txtDescription"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlAccountHierarchy">
            <h3>Accounts Hierarchy</h3>
            <hr />
            <label>Primary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimary" OnSelectedIndexChanged="cmbPrimary_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Secondary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondary_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Tertiary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiary"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlCashAndBanks">
            <h3>Cash and Bank Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCashAndBank"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlCapital">
            <h3>Capital Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbCapital"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlInvestor">
            <h3>Investor Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbInvestor"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlIndividual">
            <h3>Individual Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbIndividual"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlBank">
            <h3>Bank Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbBank"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSuppliers">
            <h3>Supplier Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbSupplier"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlLoans">
            <h3>Loan Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbLoans"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayLoan">
            <asp:Button runat="server" ID="btnGiveAwayLoan1" CssClass="button primary full-size" Text="Give Back Loan" OnClick="btnGiveAwayLoan1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveLoan">
            <asp:Button runat="server" ID="btnReceiveBackLoan1" CssClass="button primary full-size" Text="Take Loan" OnClick="btnReceiveBackLoan1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayToCapital">
            <asp:Button runat="server" ID="btnPayToCapital1" CssClass="button primary full-size" Text="Pay to Capital Account" OnClick="btnPayToCapital1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveFromCapital">
            <asp:Button runat="server" ID="btnReceiveFromCapital1" CssClass="button primary full-size" Text="Receive from Capital Account" OnClick="btnReceiveFromCapital1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayToInvestor">
            <asp:Button runat="server" ID="btnPayToInvestor1" CssClass="button primary full-size" Text="Pay to Investor" OnClick="btnPayToInvestor1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveFromInvestor">
            <asp:Button runat="server" ID="btnReceiveFromIvestors1" CssClass="button primary full-size" Text="Receive from Investor" OnClick="btnReceiveFromIvestors1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayToIndividual">
            <asp:Button runat="server" ID="btnPayToIndividual1" CssClass="button primary full-size" Text="Pay to Individual" OnClick="btnPayToIndividual1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveFromIndividual">
            <asp:Button runat="server" ID="btnReceiveFromIndividual1" CssClass="button primary full-size" Text="Receive from Individual" OnClick="btnReceiveFromIndividual1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlDepositInBank">
            <asp:Button runat="server" ID="btnDepositInBank1" CssClass="button primary full-size" Text="Deposit in Bank" OnClick="btnDepositInBank1_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlWithdrawalFromBank">
            <asp:Button runat="server" ID="btnWithdrawalFromBank1" CssClass="button primary full-size" Text="Withdrawal from Bank" OnClick="btnWithdrawalFromBank1_Click"  />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayToSupplier">
            <asp:Button runat="server" ID="btnPayToSuppliers1" CssClass="button primary full-size" Text="Pay to Supplier" OnClick="btnPayToSuppliers1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveFromSupplier">
            <asp:Button runat="server" ID="btnReceiveFromSupplier1" CssClass="button primary full-size" Text="Receive from Supplier" OnClick="btnReceiveFromSupplier1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlPayExpenses">
            <h3>Expenses Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbExpenses"></asp:DropDownList>
            </div>

            <asp:Button runat="server" ID="btnPayExpenses1" CssClass="button primary full-size" Text="Pay Expenses" OnClick="btnPayExpenses1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlReceiveIncome">
            <h3>Income Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbIncome"></asp:DropDownList>
            </div>

            <asp:Button runat="server" ID="btnReceiveIncome1" CssClass="button primary full-size" Text="Receive Income" OnClick="btnReceiveIncome1_Click" />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlJournalEntry">
            <asp:Panel runat="server" ID="pnlJEAccountHierarcy" Visible="false">
                <h3>Accounts Hierarchy</h3>
                <hr />
                <label>Primary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimaryJournal" OnSelectedIndexChanged="cmbPrimaryJournal_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label>Secondary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbSecondaryJournal" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondaryJournal_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label>Tertiary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbTertiaryJournal"></asp:DropDownList>
                </div>
                <hr />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlJEAccountID" Visible="false">
                <div class="input-control text full-size">
                    <asp:TextBox runat="server" placeholder="account ID" ID="txtAccountID" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </div>
            </asp:Panel>
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateJournal"></asp:TextBox>
            </div>
<%--            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateJournal"></asp:TextBox>
            </div>--%>
<%--            <label>Amount</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtDebitJournal"  ondrop="return false;" onpaste="return false;" onkeyup="sync('txtDebitJournal', 'txtCreditJournal')" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
                <span class="label">Amount</span>
                <span class="informer">Please enter debit amount</span>
                <span class="placeholder">Debit</span>
            </div>
<%--            <label>Amount</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtCreditJournal"  ondrop="return false;" onpaste="return false;" onkeyup="sync('txtCreditJournal', 'txtDebitJournal')" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
                <span class="label">Amount</span>
                <span class="informer">Please enter credit amount</span>
                <span class="placeholder">Credit</span>
            </div>
<%--            <label>Cheque Number</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtChequeJournal"></asp:TextBox>
                <span class="label">Cheque Number</span>
                <span class="informer">Please enter cheque number</span>
                <span class="placeholder">Cheque Number</span>
            </div>
<%--            <label>Description</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtDescriptionJournal"></asp:TextBox>
                <span class="label">Description</span>
                <span class="informer">Please enter description</span>
                <span class="placeholder">Description</span>
            </div>
            <hr />

            <asp:Button runat="server" ID="btnJournalEntry1" CssClass="button primary full-size" Text="Add to Transaction" Visible="false" OnClick="btnJournalEntry1_Click" />
            <asp:Button runat="server" ID="btnJournalEntryIDs1" CssClass="button primary full-size" Text="Add to Transaction" Visible="false" OnClick="btnJournalEntryIDs1_Click" />
        </asp:Panel>
    </div>

    <div class="cell colspan5">
<%--        <div class="input-control text textarea full-size"> 
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
        </div>--%>
        <asp:Panel runat="server" ID="pnlTransactionGrid" Visible="false">
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowDataBound="gridTransaction_RowDataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account" SortExpression="Account" />
                    <asp:TemplateField HeaderText="Debit" SortExpression="Debit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="Credit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("Credit", "{0:n}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccounts.accountName AS Account, tblGenAccountsTmpTransactions.debitAmount AS Debit, tblGenAccountsTmpTransactions.creditAmount AS Credit FROM tblGenAccountsTmpTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTmpTransactions.accountID = tblGenAccounts.id"></asp:SqlDataSource>
            <asp:Button runat="server" ID="btnJournalEntry2" CssClass="button success full-size" Text="Confirm Transaction" OnClick="btnJournalEntry2_Click" />
            <asp:Button runat="server" ID="btnJournalEntry3" CssClass="button warning full-size" Text="Discard Transaction" OnClick="btnJournalEntry3_Click" />
        </asp:Panel>

<%--            <h3>Accounts</h3>--%>
        <asp:Panel runat="server" ID="pnlAccountIDsGrid" Visible="false">
            <asp:GridView runat="server" ID="gridAccounts" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                    <asp:BoundField DataField="mainHead" HeaderText="Head" SortExpression="mainHead" />
                    <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div> 
</asp:Content>
