<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SiteTransactions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SiteTransactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Routine Transactions:&nbsp<small><asp:Label runat="server" ID="lblHeading" Text="Enter routine transactions here."></asp:Label></small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <div class="accordion" data-role="accordion" data-close-any="true">
            <div class="frame">
                <div class="heading">Inputs - Cash & Bank</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnInputsCashAndBank" CssClass="button success full-size" Text="Inputs - Cash & Bank" OnClick="btnInputsCashAndBank_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Inputs - Suppliers</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnInputsSuppliers" CssClass="button success full-size" Text="Inputs - Suppliers" OnClick="btnInputsSuppliers_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Inputs - Others</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnTransferToSite" CssClass="button success full-size" Text="Inputs - Others" OnClick="btnTransferToSite_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Site Receipt - Cash & Bank</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnReceiptsCashAndBank" CssClass="button success full-size" Text="Outputs - Cash & Bank" OnClick="btnReceiptsCashAndBank_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Site Receipt - Others</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnTransferFromSite" CssClass="button success full-size" Text="Outputs - Others" OnClick="btnTransferFromSite_Click" />
                </div>
            </div>
            <div class="frame">
                <div class="heading">Site to Site Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnInterSiteTransfer" CssClass="button success full-size" Text="Site to Site Transfer" OnClick="btnInterSiteTransfer_Click" />
                </div>
            </div>
<%--            <div class="frame">
                <div class="heading">Within Site Entries</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnTransferWithinSite" CssClass="button success full-size" Text="Within Site Transfer" OnClick="btnTransferWithinSite_Click" />
                </div>
            </div>--%>
            <div class="frame">
                <div class="heading">Site Transactions - IDs</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnSiteTransactionsIDs" CssClass="button success full-size" Text="Transactions Using IDs" OnClick="btnSiteTransactionsIDs_Click" />
                </div>
            </div>
<%--            <div class="frame">
                <div class="heading">Misc Liabilities</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnMiscLiabilities" CssClass="button success full-size" Text="Misc Liabilities" OnClick="btnMiscLiabilities_Click" />
                </div>
            </div>--%>
            <div class="frame">
                <div class="heading">Import from Excel</div>
                <div class="content">
                    <asp:Button runat="server" ID="btnImportFromExcel" CssClass="button success full-size" Text="Import from Excel" OnClick="btnImportFromExcel_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="cell colspan9">
        <asp:Panel runat="server" ID="pnlFileUpload" Visible="false">
            <div class="input-control file" data-role="input">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <button class="button"><span class="mif-folder"></span></button>
            </div>        
                <asp:Button runat="server" ID="btnSubmitFile" CssClass="button success" Text="Get Data" OnClick="btnSubmitFile_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlExcelImport" Visible="false">
            <h3>Data Imported from Excel</h3>
            <asp:GridView runat="server" ID="GridView1" CssClass="table striped hovered border bordered" >
                </asp:GridView>
            
            <asp:GridView runat="server" ID="gridExcelData" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                <Columns>
<%--                    <asp:TemplateField HeaderText="Date" SortExpression="Date">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Cash Book A/c" SortExpression="Account">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("cashBookAccount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccount" runat="server" Text='<%# Bind("cashBookAccount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Other Account" SortExpression="PaidBy">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("otherAccount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("otherAccount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site" SortExpression="Site">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SiteID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSite" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit" SortExpression="Debit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Debit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" style="text-align: right" Text='<%# Bind("Debit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="Credit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Credit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" style="text-align: right" Text='<%# Bind("Credit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnExcelDataSubmit" CssClass="button success full-size" Text="Submit" OnClick="btnExcelDataSubmit_Click" />
            <hr />
        </asp:Panel>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlSite" Visible="false">
            <h3>Active Sites</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbActiveSites" AutoPostBack="True" OnSelectedIndexChanged="cmbActiveSites_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteDebit" Visible="false">
            <h3>Debit</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbActiveSites3"></asp:DropDownList>
            </div>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" ID="txtSiteDebitAccountID" placeholder="Debit Account ID"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSiteAccountHirarchy">
            <h3>Site Accounts</h3>
            <hr />
<%--            <label>Primary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbSitePrimary" OnSelectedIndexChanged="cmbSitePrimary_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Secondary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSiteSecondary_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Tertiary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteTertiary"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel ID="pnlSiteInputs" runat="server" Visible="false">
            <h3>Site Inputs</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondaryInput" AutoPostBack ="true" OnSelectedIndexChanged="cmbSecondaryInput_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiaryInput"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteOutputs" Visible="false">
            <h3>Site Outputs</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondaryOutput" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondaryOutput_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiaryOutput"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSiteDate">
<%--            <label>Amount</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtSiteAmount" ClientIDMode="Static" onkeyup="document.getElementById('txtSite2Amount').value = this.value" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
                <span class="label">Amount</span>
                <span class="informer">Please enter amount</span>
                <span class="placeholder">Amount</span>
            </div>
<%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" type ="text" ID="txtSiteDescription" ClientIDMode="Static"  onkeyup="document.getElementById('txtSite2Description').value = this.value"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteOK" Visible="false">
            <asp:Button runat="server" ID="btnSiteDebit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteDebit_Click"/>
            <asp:Button runat="server" ID="btnSiteCredit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteCredit_Click"/>
            <asp:Button runat="server" ID="btnSiteInputs" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteInputs_Click"/>
            <asp:Button runat="server" ID="btnSiteOutputs" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteOutputs_Click"/>
            <asp:Button runat="server" ID="btnMiscLiabilitiesCreate" Visible="false" CssClass="button primary full-size" Text="Add Liability" OnClick="btnMiscLiabilitiesCreate_Click"/>
            <asp:Button runat="server" ID="btnImportLiabilityFromExcel" Visible="false" CssClass="button primary full-size" Text="Import From Excel" OnClick="btnImportLiabilitiesFromExcel_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteDebitOK" Visible="false">
            <asp:Button runat="server" ID="btnSiteDebitOK" Visible="true" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteDebitOK_Click"/>
        </asp:Panel>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlSite2" Visible="false">
            <h3>Active Sites</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbActiveSites2"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteCredit" Visible="false">
            <h3>Credit</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbActiveSites4"></asp:DropDownList>
            </div>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" ID="txtSiteCreditID" placeholder="Credit Account ID" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSite2AccountHierarcy">
            <h3>Site Accounts</h3>
            <hr />
<%--            <label>Primary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbSitePrimary2" OnSelectedIndexChanged="cmbSitePrimary2_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Secondary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteSecondary2" AutoPostBack="true" OnSelectedIndexChanged="cmbSiteSecondary2_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Tertiary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSiteTertiary2"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSuppliers">
            <h3>Supplier Accounts</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server"   ID="cmbSupplier"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlCashAndBanks">
            <h3>Cash & Banks</h3>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCashAndBank"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlSite2Date">
<%--            <label>Amount</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtSite2Amount" ClientIDMode="Static" onkeyup="document.getElementById('txtSiteAmount').value = this.value" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
                <span class="label">Amount</span>
                <span class="informer">Please enter amount</span>
                <span class="placeholder">Amount</span>
            </div>
<%--            <label>Description</label>--%>
            <div class="input-control text full-size">
                <asp:TextBox runat="server" type ="text" ID="txtSite2Description" ClientIDMode="Static" onkeyup="document.getElementById('txtSiteDescription').value = this.value"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSite2OK" Visible="false">
            <asp:Button runat="server" ID="btnSiteDebit2" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteDebit2_Click"/>
            <asp:Button runat="server" ID="btnSiteCredit2" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteCredit2_Click"/>
            <asp:Button runat="server" ID="btnSuppliers" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSuppliers_Click"/>
            <asp:Button runat="server" ID="btnCashAndBankDebit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnCashAndBankDebit_Click"/>
            <asp:Button runat="server" ID="btnCashAndBankCredit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnCashAndBankCredit_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlGenAccountHierarchy">
            <h3>General Accounts</h3>
            <hr />
<%--            <label>Primary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbPrimary" OnSelectedIndexChanged="cmbPrimary_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Secondary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSecondary" AutoPostBack="true" OnSelectedIndexChanged="cmbSecondary_SelectedIndexChanged"></asp:DropDownList>
            </div>
<%--            <label>Tertiary Accounts</label>--%>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbTertiary"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlGenDate">
<%--            <label>Amount</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtGenAmount"  ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" ></asp:TextBox>
                <span class="label">Amount</span>
                <span class="informer">Please enter amount</span>
                <span class="placeholder">Amount</span>
            </div>
<%--            <label>Cheque Number</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtGenChequeNumber"></asp:TextBox>
                <span class="label">Cheque Number</span>
                <span class="informer">Please enter cheque number</span>
                <span class="placeholder">Cheque Number</span>
            </div>
<%--            <label>Description</label>--%>
            <div class="input-control modern text">
                <asp:TextBox runat="server" type ="text" ID="txtGenDescription"></asp:TextBox>
                <span class="label">Description</span>
                <span class="informer">Please enter description</span>
                <span class="placeholder">Description</span>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlGenOK" Visible="false">
            <asp:Button runat="server" ID="btnGenDebit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnGenDebit_Click"/>
            <asp:Button runat="server" ID="btnGenCredit" Visible="false" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnGenCredit_Click"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSiteCreditOK" Visible="false">
            <asp:Button runat="server" ID="btnSiteCreditOK" Visible="true" CssClass="button primary full-size" Text="Add to Transaction" OnClick="btnSiteCreditOK_Click"/>
        </asp:Panel>
    </div> 

    <div class="cell colspan5">
        <asp:Panel runat="server" ID="pnlDate" Visible="false">
<%--            <label>Date</label>--%>
<%--            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>--%>
            <div class="input-control text full-size" >
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="pnlTransaction">
            <h3>Transaction Detail</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowDataBound="gridTransaction_RowDataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Account" HeaderText="Account" SortExpression="Account" />
                    <asp:TemplateField HeaderText="Debit" SortExpression="Debit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Debit", "{0:n}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("Debit", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit" SortExpression="Credit">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Credit", "{0:n}") %>'></asp:TextBox>
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
            <hr />
            <asp:Button runat="server" ID="btnTransactionOK" CssClass="button primary full-size" Text="Confirm Transaction" OnClick="btnTransactionOK_Click"/>        
            <asp:Button runat="server" ID="btnCancelTransaction" CssClass="button primary full-size" Text="Cancel Transaction" OnClick="btnCancelTransaction_Click"/>        
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAccountIDsGrid" Visible="false">
            <asp:GridView runat="server" ID="gridAccounts" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
                    <asp:BoundField DataField="mainHead" HeaderText="Head" SortExpression="mainHead" />
                    <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMiscLiabilities" Visible="false">
            <asp:GridView runat="server" ID="gridMiscLiabilities" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridMiscLiabilities_RowDataBound" >
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" SortExpression="isSettled">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkSettled" runat="server" Checked='<%# Bind("isSettled") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSettled" runat="server" Checked='<%# Bind("isSettled") %>' Enabled="true" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="siteName" HeaderText="Site" SortExpression="siteName" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:TemplateField HeaderText="Amount" SortExpression="amount">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLiabilityAmount" runat="server" Text='<%# Bind("amount", "{0:n}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <div class="input-control text full-size">
                                <asp:TextBox ID="txtLiabilityAmount" runat="server" style="text-align: right" Text='<%# Bind("amount", "{0:n}") %>'></asp:TextBox>
                            </div>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnMiscLiabilitiesUpdate" Visible="true" CssClass="button primary full-size" Text="Update Status" OnClick="btnMiscLiabilitiesUpdate_Click"/>
        </asp:Panel>
<%--        <div class="input-control text textarea full-size"> 
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
        </div>--%>
    </div> 
</asp:Content>