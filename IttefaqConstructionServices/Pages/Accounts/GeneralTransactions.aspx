<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="GeneralTransactions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.GeneralTransactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Routine Transactions:&nbsp<asp:Label runat="server" ID="lblHeading" Text=""></asp:Label>&nbsp<small>Enter routine transactions here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <div class="accordion" data-role="accordion" data-close-any="true">
            <div class="frame">
                <div class="heading">Cash Book Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnReceiveInCashBookGeneral" CssClass="button success full-size" Text="General - Receipts" OnClick="btnReceiveInCashBookGeneral_Click" />
                    <asp:Button runat="server" ID="btnPayFromCashBookGeneral" CssClass="button success full-size" Text="General - Payments" OnClick="btnPayFromCashBookGeneral_Click" />
                    <asp:Button runat="server" ID="btnReceiveInCashBookSite" CssClass="button success full-size" Text="Site - Receipts" OnClick="btnReceiveInCashBookSite_Click" />
                    <asp:Button runat="server" ID="btnPayFromCashBookSite" CssClass="button success full-size" Text="Site - Payments" OnClick="btnPayFromCashBookSite_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Other Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnJournalEntry" CssClass="button success full-size" Text="Journal Entry - Menus" OnClick="btnJournalEntry_Click" />
                    <asp:Button runat="server" ID="btnJournalEntryIDs" CssClass="button success full-size" Text="Journal Entry - IDs" OnClick="btnJournalEntryIDs_Click" />
                </div>
            </div>
<%--            <div class="frame">
                <div class="heading">Cheques Manager</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnChequesInHand" CssClass="button success full-size" Text="Receive/Issue Cheques" OnClick="btnChequesInHand_Click" />
                </div>
            </div>--%>
        </div>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlDebitSide" Visible="false">
            <h3>Debit Side</h3>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteDebit" Visible="false">
            <label>Active Sites</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteDebit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGeneralAccountsIDDebit" Visible="false">
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Account ID" ID="txtAccountIDDebit" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCashAndBankDebit" Visible="false">
            <label>CashBook Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCashAndBankDebit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGeneralAccountsDebit" Visible="false">
            <label>Secondary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondaryAccountsDebit" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondaryAccountsDebit_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Tertiary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiaryAccountsDebit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteAccountsDebit" Visible="false">
            <label>Secondary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondarySiteAccountsDebit" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondarySiteAccountsDebit_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Tertiary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiarySiteAccountsDebit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDateDebit" Visible="false">
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateDebit"></asp:TextBox>
            </div>
<%--            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateDebit"></asp:TextBox>
            </div>--%>
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Amount" id="txtAmountDebit"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" />--%>
                <asp:TextBox runat="server" placeholder="Amount" ID="txtAmountDebit" ClientIDMode="Static" onkeyup="document.getElementById('txtAmountCredit').value = this.value" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Cheque Number if any" ID="txtChequeNumberDebit" ClientIDMode="Static" onkeyup="document.getElementById('txtChequeNumberCredit').value = this.value"></asp:TextBox>
            </div>
            <%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
                <asp:TextBox runat="server" placeholder="Description of Transaction" ClientIDMode="Static" ID="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Button runat="server" ID="btnGeneralDebit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnGeneralDebit_Click" />
        <asp:Button runat="server" ID="btnSiteDebit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnSiteDebit_Click" />
        <asp:Button runat="server" ID="btnCashBookDebit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnCashBookDebit_Click" />
        <asp:Button runat="server" ID="btnAccountIDDebit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnAccountIDDebit_Click" />
        <asp:Panel runat="server" ID="pnlTaxesDebit" Visible="false">
            <hr />
            <label>Tax Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTaxDebit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTaxesDebitInputs" Visible="false">
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Tax Amount" ID="txtTaxesDebitAmount" ClientIDMode="Static" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Tax Rate e.g. 3.5%" ID="txtTaxesRateDebit" ClientIDMode="Static"></asp:TextBox>
            </div>
            <%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
                <asp:TextBox runat="server" placeholder="Description" ClientIDMode="Static" ID="txtTaxesDebitDescription"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnTaxesDebit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnTaxesDebit_Click" />
        <asp:Panel ID="pnlChequesInHandReceiveDebitSide" Visible="true" runat="server">
            <h4>Cheque Particulars</h4>
            <hr />
            <label>Transaction Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtChequesInHandDate"></asp:TextBox>
            </div>
            <label>Cheque Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtChequesInHandChequeDate"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Cheque Number if any" ID="txtChequesInHandChequeNumber" ClientIDMode="Static"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Drawn on Bank..." ID="txtChequesInHandNameOfBank" ClientIDMode="Static"></asp:TextBox>
            </div>
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Amount" id="txtAmountDebit"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" />--%>
                <asp:TextBox runat="server" placeholder="Cheque Amount" ID="txtChequesInHandChequeAmount" ClientIDMode="Static" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
            </div>
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Amount" id="txtAmountDebit"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" />--%>
                <asp:TextBox runat="server" placeholder="Tax Rate" ID="txtChequesInHandTaxRate" ClientIDMode="Static"></asp:TextBox>
            </div>
            <label>Tax Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbChequesInHandTaxDebit"></asp:DropDownList>
            </div>
            <%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
                <asp:TextBox runat="server" placeholder="Description of Transaction" ClientIDMode="Static" ID="txtChequesInHandDescription"></asp:TextBox>
            </div>
            <asp:Button runat="server" ID="btnReceiveCheques" Visible="true" CssClass="button warning full-size" Text="Receive Cheque" OnClick="btnReceiveCheques_Click" />
        </asp:Panel>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlCreditSide" Visible="false">
            <h3>Credit Side</h3>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteCredit" Visible="false">
            <label>Active Sites</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteCredit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGeneralAccountsIDCredit" Visible="false">
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Account ID" ID="txtAccountIDCredit" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCashAndBankCredit" Visible="false">
            <label>CashBook Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCashAndBankCredit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGeneralAccountsCredit" Visible="false">
            <label>Secondary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondaryAccountsCredit" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondaryAccountsCredit_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Tertiary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiaryAccountsCredit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteAccountsCredit" Visible="false">
            <label>Secondary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondarySiteAccountsCredit" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondarySiteAccountsCredit_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label>Tertiary Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiarySiteAccountsCredit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDateCredit" Visible="false">
            <%--            <label>Date</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateCredit"></asp:TextBox>
            </div>
<%--            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDateCredit"></asp:TextBox>
            </div>--%>
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Amount" id="txtAmountCredit"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" />--%>
                <asp:TextBox runat="server" placeholder="Amount" ClientIDMode="Static" ID="txtAmountCredit" ondrop="return false;" onpaste="return false;" onkeyup="document.getElementById('txtAmountDebit').value = this.value" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Cheque Number if any" ID="txtChequeNumberCredit" ClientIDMode="Static" onkeyup="document.getElementById('txtChequeNumberDebit').value = this.value"></asp:TextBox>
            </div>
            <%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Description of Transaction" id="txtDescriptionCredit" onkeyup="document.getElementById('txtDescriptionDebit').value = this.value" />--%>
                <asp:TextBox runat="server" placeholder="Description of Transaction" ClientIDMode="Static" ID="txtDescriptionCredit" onkeyup="document.getElementById('txtDescriptionDebit').value = this.value"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Button runat="server" ID="btnGeneralCredit" Visible="false" CssClass="button success full-size" Text="Add to Transaction" OnClick="btnGeneralCredit_Click" />
        <asp:Button runat="server" ID="btnSiteCredit" Visible="false" CssClass="button success full-size" Text="Add to Transaction" OnClick="btnSiteCredit_Click" />
        <asp:Button runat="server" ID="btnCashBookCredit" Visible="false" CssClass="button success full-size" Text="Add to Transaction" OnClick="btnCashBookCredit_Click" />
        <asp:Button runat="server" ID="btnAccountIDCredit" Visible="false" CssClass="button success full-size" Text="Add to Transaction" OnClick="btnAccountIDCredit_Click" />
        <asp:Panel runat="server" ID="pnlTaxesCredit" Visible="false">
            <hr />
            <label>Tax Accounts</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTaxCredit"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTaxesCreditInputs" Visible="false">
            <%--            <label>Amount</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Tax Amount" ID="txtTaxesCreditAmount" ClientIDMode="Static" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
            </div>
            <%--            <label>Cheque Number</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" placeholder="Tax Rate e.g. 3.5%" ID="txtTaxesRateCredit" ClientIDMode="Static"></asp:TextBox>
            </div>
            <%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
                <asp:TextBox runat="server" placeholder="Description" ClientIDMode="Static" ID="txtTaxesCreditDescription"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnTaxesCredit" Visible="false" CssClass="button warning full-size" Text="Add to Transaction" OnClick="btnTaxesCredit_Click" />
        <asp:Panel runat="server" ID="pnlChequesInHandReceivedCreditSide" Visible="false">
            <h4>Cheque in the Name of:</h4>
            <hr />
            <label>Suppliers:</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbChequesInHandSuppliers"></asp:DropDownList>
            </div>
            <h4>Cheque Received from:</h4>
            <hr />
            <label class="input-control checkbox small-check">
                <asp:CheckBox runat="server" ID="chkSwap" Checked="true" AutoPostBack="true" OnCheckedChanged="chkSwap_CheckedChanged" />
                <span class="check"></span>
                <span class="caption">Site Accounts</span>
            </label>
            <asp:Panel runat="server" ID="pnlSitesChequeInHand" Visible="true">
                <label>Active Sites:</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbChequesInHandSites"></asp:DropDownList>
                </div>
                <label>Site Secondary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbChequesInHandSiteSecondaryAccounts" AutoPostBack="true" OnSelectedIndexChanged="cmbChequesInHandSiteSecondaryAccounts_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label>Site Tertiary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbChequesInHandSitesTertiaryAccounts"></asp:DropDownList>
                </div>
                <hr />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlGeneralChequesInHand" Visible="false">
                <label>Tertiary Accounts</label>
                <div class="input-control select full-size">
                    <asp:DropDownList runat="server" ID="cmbChequesInHandTertiaryAccounts"></asp:DropDownList>
                </div>
                <hr />
            </asp:Panel>
        </asp:Panel>
    </div>

    <div class="cell colspan5">
        <%--        <div class="input-control text textarea full-size"> 
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
        </div>--%>
        <asp:Panel runat="server" ID="pnlTransactionGrid" Visible="false">
            <h3>Your Transaction So Far:</h3>
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowDataBound="gridTransaction_RowDataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account" SortExpression="Account" />
                    <asp:TemplateField HeaderText="Debit" SortExpression="Debit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Debit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("Debit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="Credit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Credit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("Credit", "{0:n}") %>'></asp:Label>
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
        <asp:Panel ID="pnlChequesInHandIssue" Visible="true" runat="server">
            <label>Suppliers:</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbChequesInHandIssuedTo"></asp:DropDownList>
            </div>
            <hr />
            <h3>Current Status of Cheques in Hand</h3>
            <asp:GridView runat="server" ID="gridChequesInHand" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" AutoGenerateColumns="False" OnRowDataBound="gridChequesInHand_RowDataBound" ShowFooter="True">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbliD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is InHand" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Status") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkInHand" runat="server" Checked='<%# Bind("Status") %>' Enabled="true" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Date" HeaderText="Cheque Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="Date" />
                    <asp:BoundField DataField="Party Name" HeaderText="Party Name" SortExpression="Party Name" />
                    <asp:BoundField DataField="Bank" HeaderText="Bank" SortExpression="Bank" />
                    <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.isInHand AS Status, tblGenAccountsTransactions.chequeDate AS Date, tblGenAccounts.accountName AS [Party Name], tblGenAccountsTransactions.bankName AS Bank, tblGenAccountsTransactions.debitAmount AS Amount, tblGenAccountsTransactions.id FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.supplierID = tblGenAccounts.id WHERE (tblGenAccountsTransactions.isInHand = 1)"></asp:SqlDataSource>
            <hr />
            <asp:Button runat="server" ID="btnChequesInHandIssue" CssClass="button warning full-size" Text="Issue" OnClick="btnChequesInHandIssue_Click" />
        </asp:Panel>
    </div>
</asp:Content>
