<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="ChequesInHandDetails.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.ChequesInHandDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h3>Details of Cheques in Hand Account</h3>
    <asp:GridView runat="server" ID="gridPriSitesInfo" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true">
        <Columns>
            <asp:CheckBoxField DataField="In Hand Status" HeaderText="In Hand Status" SortExpression="In Hand Status" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="Cheque Number" HeaderText="Cheque Number" SortExpression="Cheque Number" />
            <asp:BoundField DataField="Cheque Date" HeaderText="Cheque Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="Cheque Date" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Bank Name" HeaderText="Bank Name" SortExpression="Bank Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Tax Rate" HeaderText="Tax Rate" SortExpression="Tax Rate">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Received From" HeaderText="Received From" SortExpression="Received From" />
            <asp:BoundField DataField="Issued To" HeaderText="Issued To" SortExpression="Issued To" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.chequeNumber AS [Cheque Number], tblGenAccountsTransactions.debitAmount AS Amount, tblGenAccountsTransactions.description AS Description, tblGenAccountsTransactions.taxRate AS [Tax Rate], tblGenAccountsTransactions.isInHand AS [In Hand Status], tblGenAccounts.accountName AS [Received From], tblGenAccountsTransactions.bankName AS [Bank Name], tblGenAccountsTransactions.chequeDate AS [Cheque Date], tblGenAccounts_1.accountName AS [Issued To] FROM tblGenAccounts INNER JOIN tblGenAccountsTransactions ON tblGenAccounts.id = tblGenAccountsTransactions.supplierID INNER JOIN tblGenAccounts AS tblGenAccounts_1 ON tblGenAccountsTransactions.chequeIssuedTo = tblGenAccounts_1.id"></asp:SqlDataSource>

</asp:Content>
