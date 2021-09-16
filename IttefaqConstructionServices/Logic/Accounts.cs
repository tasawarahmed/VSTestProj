using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IttefaqConstructionServices.Logic
{
    public class Accounts
    {
        public int accountID { get; set; }
        public DateTime date { get; set; }
        public string accountDescription { get; set; }
        public Decimal debit { get; set; }
        public Decimal credit { get; set; }
        public Decimal runningBalance { get; set; }
        public int reportingOrder { get; set; }
        public int type { get; set; }
        List<Account> accountsForPLS = new List<Account>();
        Utilities p = new Utilities();
        public int transID { get; set; }
        public string chequeNumber { get; set; }
        List<AccountStructure> listTransaction = new List<AccountStructure>();
        Decimal totalDebits = 0;
        Decimal totalCredits = 0;
        Single totalChequesInHand = 0f;
        string companyName = string.Empty;
        string companyAddress = string.Empty;
        string companyContacts = string.Empty;
        string companyWebsite = string.Empty;
        string companyEmail = string.Empty;
        Dictionary<int, string> accountNamesDictionary = new Dictionary<int, string>();
        Dictionary<int, string> siteNameDictionary = new Dictionary<int, string>();
        Single totalDebitsForFooter = 0.0f;
        Single totalCreditsForFooter = 0.0f;


        int accountType = 0;

        private void populateSiteNamesDictionary()
        {
            siteNameDictionary.Clear();

            siteNameDictionary = p.getIntStringDictionary("SELECT id, siteName FROM tblGenSites");
        }

        private void populateAccountNamesDictionary()
        {
            accountNamesDictionary.Clear();

            accountNamesDictionary = p.getIntStringDictionary("SELECT id, accountName FROM tblGenAccounts");
        }

        private void populateParticulars()
        {
            companyEmail = companyAddress = companyContacts = companyWebsite = companyName = string.Empty;

            string query = "SELECT companyName, companyAddress, companyContacts, companyWebsite, companyEmail FROM tblGenActivationInfo";

            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                companyName = dr[0].ToString();
                companyAddress = dr[1].ToString();
                companyContacts = dr[2].ToString();
                companyWebsite = dr[3].ToString();
                companyEmail = dr[4].ToString();
            }
        }

        internal void getAccountStatement(int accountID, DateTime start, DateTime end, int parentAccountID)
        {
            /*
             * 1. Delete everything from tblRptAccountStatement
             * 2. Get opening balance and set its reporting order to 1
             * 3. Get transactions arranged by date and set their reporting order to 2
             * 4. Get closing balance and set its reporting order to 3
             */

            List<Accounts> accountStatement = new List<Accounts>();

            Accounts openingBalance = getOpeningBalance(accountID, start, parentAccountID);
            accountStatement.Add(openingBalance);
            List<Accounts> transactions = getTransactions(accountID, start, end);
            Decimal closingBalance = 0;
            closingBalance = openingBalance.runningBalance;

            for (int i = 0; i < transactions.Count; i++)
            {
                Accounts a = new Accounts();

                a.date = transactions[i].date;
                a.accountDescription = transactions[i].accountDescription;
                a.debit = transactions[i].debit;
                a.credit = transactions[i].credit;
                a.transID = transactions[i].transID;
                a.chequeNumber = transactions[i].chequeNumber;

                if (accountType == 1 || accountType == 6 || accountType == 7)//if account type is assets or expenditures.
                {
                    a.runningBalance = accountStatement[i].runningBalance + a.debit - a.credit;
                    closingBalance = a.runningBalance;
                }
                else
                {
                    a.runningBalance = accountStatement[i].runningBalance + a.credit - a.debit;
                    closingBalance = a.runningBalance;
                }

                a.reportingOrder = 2;
                a.type = accountType;
                a.accountID = accountID;
                accountStatement.Add(a);
            }

            Accounts cb = new Accounts();
            cb.date = end;
            cb.debit = 0;
            cb.credit = 0;
            cb.type = accountType;
            cb.accountID = accountID;
            cb.accountDescription = "Closing Balance";
            cb.reportingOrder = 3;
            cb.runningBalance = closingBalance;

            accountStatement.Add(cb);

            if (accountStatement.Count > 0)
            {
                writeAccountStatement(accountStatement);
            }
        }

        private void writeAccountStatement(List<Accounts> accountStatement)
        {
            string query = string.Empty;
            string deleteQuery = " DELETE FROM tblRptAccountStatement";
            query += deleteQuery;

            for (int i = 0; i < accountStatement.Count; i++)
            {
                string q = string.Format("  INSERT INTO tblRptAccountStatement (accountID, accountType, date, description, debit, credit, reportingOrder, runningBalance, transactionID, cheque) VALUES ({0},{1},'{2}',N'{3}',{4},{5},{6},{7},{8},N'{9}')", accountStatement[i].accountID, accountStatement[i].type, accountStatement[i].date, p.FixString(accountStatement[i].accountDescription), accountStatement[i].debit, accountStatement[i].credit, accountStatement[i].reportingOrder, accountStatement[i].runningBalance, accountStatement[i].transID, accountStatement[i].chequeNumber);
                query += q;
            }

            p.ExecuteTransactionQuery(query);
        }

        private List<Accounts> getTransactions(int accountID, DateTime start, DateTime end)
        {
            List<Accounts> transactions = new List<Accounts>();

            string query = string.Format("SELECT date, debitAmount, creditAmount, description, transactionID, chequeNumber  FROM tblGenAccountsTransactions WHERE accountID = {0} AND date >= '{1}' AND date <= '{2}' ORDER BY date, transactionID", accountID, start, end);
            DataTable dt = p.GetDataTable(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Accounts a = new Accounts();
                DataRow dr = dt.Rows[i];
                DateTime date = new DateTime(2000, 1, 1);
                Decimal debit = 0;
                Decimal credit = 0;
                int transID = 0;
                string chequeNumber = dr[5].ToString(); ;

                DateTime.TryParse(dr[0].ToString(), out date);
                Decimal.TryParse(dr[1].ToString(), out debit);
                Decimal.TryParse(dr[2].ToString(), out credit);
                int.TryParse(dr[4].ToString(), out transID);
                string description = dr[3].ToString();

                a.date = date;
                a.accountDescription = description;
                a.debit = debit;
                a.credit = credit;
                a.chequeNumber = chequeNumber;
                a.transID = transID;

                transactions.Add(a);
            }

            return transactions;
        }

        private Accounts getOpeningBalance(int accountID, DateTime start, int parentAccountID)
        {
            Accounts acc = new Accounts();
            string query = string.Format("SELECT date, debitAmount, creditAmount FROM tblGenAccountsTransactions WHERE accountID = {0} AND date < '{1}'", accountID, start);
            DataTable dt = p.GetDataTable(query);

            int debit = 0;
            int credit = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int d = 0;
                int c = 0;

                int.TryParse(dr[1].ToString(), out d);
                int.TryParse(dr[2].ToString(), out c);

                debit += d;
                credit += c;
            }

            string accountTypeQuery = string.Format("SELECT parentAccountID FROM tblGenAccounts WHERE id = {0}", parentAccountID);
            accountType = p.getIntValue(accountTypeQuery);//Check from this line and make some alternative arrangement.

            if (accountType == 1 || accountType == 6 || accountType == 7)//if account type is assets or expenditures.
            {
                acc.runningBalance = debit - credit;
            }
            else
            {
                acc.runningBalance = credit - debit;
            }

            acc.date = start;
            acc.accountDescription = "Opening Balance";
            acc.accountID = accountID;
            acc.reportingOrder = 1;
            acc.type = accountType;
            return acc;
        }

        internal void getTrialBalance(DateTime asOn)
        {
            string deleteQuery = "DELETE FROM tblRptTrialBalance";
            p.ExecuteQuery(deleteQuery);
            string accountsQuery = "SELECT id FROM tblGenAccounts WHERE (tertiaryAccountPrefix <> '000') AND (isActive = 'true')";
            string accountsTypeQuery = string.Format("SELECT id, parentAccountID FROM tblGenAccounts WHERE isActive = '{0}'", true);

            List<int> accounts = p.getIntList(accountsQuery);
            Dictionary<int, int> accountsTypes = p.getIntIntDictionary(accountsTypeQuery);
            List<Accounts> trialBalance = new List<Accounts>();

            for (int i = 0; i < accounts.Count; i++)
            {
                int accountID = accounts[i];
                int parentAccountID = 0;
                accountsTypes.TryGetValue(accountID, out parentAccountID);

                int accountType = 0;
                accountsTypes.TryGetValue(parentAccountID, out accountType);

                Accounts item = getAccountBalanceItem(accountID, accountType, asOn);
                item.date = asOn;
                item.accountType = accountType;

                if (item.debit != 0 || item.credit != 0)
                {
                    trialBalance.Add(item);
                }
            }

            if (trialBalance.Count > 0)
            {
                writeTrialBalance(trialBalance);
            }
        }

        private void writeTrialBalance(List<Accounts> trialBalance)
        {
            string query = "DELETE FROM tblRptTrialBalance";

            for (int i = 0; i < trialBalance.Count; i++)
            {
                string q = string.Format(" INSERT INTO tblRptTrialBalance (date, accountID, debit, credit, accountTypeID) VALUES ('{0}',{1},{2},{3},{4})", trialBalance[i].date, trialBalance[i].accountID, trialBalance[i].debit, trialBalance[i].credit, trialBalance[i].accountType);
                query += q;
            }

            p.ExecuteTransactionQuery(query);
        }

        private Accounts getAccountBalanceItem(int accountID, int accountType, DateTime asOn)
        {
            Accounts item = new Accounts();

            string query = string.Format("SELECT date, debitAmount, creditAmount FROM tblGenAccountsTransactions WHERE accountID = {0} AND date <= '{1}'", accountID, asOn);
            DataTable dt = p.GetDataTable(query);

            Decimal debit = 0;
            Decimal credit = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Decimal d = 0;
                Decimal c = 0;

                Decimal.TryParse(dr[1].ToString(), out d);
                Decimal.TryParse(dr[2].ToString(), out c);

                debit += d;
                credit += c;
            }

            if (accountType == 1 || accountType == 6 || accountType == 7)//if account type is assets or expenditures.
            {
                item.debit = debit - credit;
                item.credit = 0;
            }
            else
            {
                item.debit = 0;
                item.credit = credit - debit;
            }

            item.accountID = accountID;
            return item;
        }

        internal List<Account> getProfitAndLoss(List<Account> startingList, List<Account> endingList)
        {
            accountsForPLS.Clear();

            accountsForPLS = endingList;

            for (int i = 0; i < startingList.Count; i++)
            {
                Account a = new Account();
                a.Name = startingList[i].Name;

                if (contains(a.Name, accountsForPLS))
                {
                    adjustAccountsList(a.Name, startingList);
                }

                else
                {
                    a.typeName = startingList[i].typeName;
                    a.Debit = startingList[i].Debit;
                    a.Credit = startingList[i].Credit;

                    accountsForPLS.Add(a);
                }
            }

            return accountsForPLS;
        }

        private void adjustAccountsList(string p, List<Account> startingList)
        {
            Decimal finalDrBalance = 0;
            Decimal finalCrBalance = 0;
            Decimal openingDrBalance = 0;
            Decimal openingCrBalance = 0;
            string accountType = string.Empty;
            int accountTypeID = 0;
            int index = 0;
            Decimal DrBalance = 0;
            Decimal CrBalance = 0;

            for (int i = 0; i < startingList.Count; i++)
            {
                if (startingList[i].Name.Equals(p))
                {
                    openingDrBalance = startingList[i].Debit;
                    openingCrBalance = startingList[i].Credit;
                    break;
                }
            }

            for (int i = 0; i < accountsForPLS.Count; i++)
            {
                if (accountsForPLS[i].Name.Equals(p))
                {
                    finalDrBalance = accountsForPLS[i].Debit;
                    finalCrBalance = accountsForPLS[i].Credit;
                    index = i;
                    accountTypeID = accountsForPLS[i].accountTypeID;
                    break;
                }
            }

            if (accountTypeID == 4 || accountTypeID == 5)
            {
                CrBalance = finalCrBalance - openingCrBalance;
            }

            else
            {
                DrBalance = finalDrBalance - openingDrBalance;
            }

            accountsForPLS.RemoveAt(index);

            Account a = new Account();
            a.Name = p;
            a.typeName = accountType;
            a.Debit = DrBalance;
            a.Credit = CrBalance;

            accountsForPLS.Add(a);
        }

        private bool contains(string p, List<Account> accounts)
        {
            bool result = false;

            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Name.Equals(p))
                {
                    result = true;
                }
            }

            return result;
        }

        internal float getDebitAccountBalance(int accountID, DateTime date)
        {
            Single balance = 0f;
            
            string query = string.Format("SELECT SUM(debitAmount) AS Expr1, SUM(creditAmount) AS Expr2 FROM tblGenAccountsTransactions WHERE (accountID = {0}) AND (date <= '{1}')", accountID, date);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                balance = Single.Parse(dr[0].ToString()) - Single.Parse(dr[1].ToString());
            }

            return balance;
        }

        private void populateListOfAccounts(string userName)
        {
            listTransaction.Clear();
            totalCredits = totalDebits = 0;

            string query = string.Format("SELECT accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, taxRate, isInHand, supplierID, bankName, chequeDate, chequeIssuedTo, siteID FROM tblGenAccountsTmpTransactions WHERE operator = '{0}'", userName);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    AccountStructure AS = new AccountStructure();
                    AS.accountID = int.Parse(dr[0].ToString());
                    AS.date = DateTime.Parse(dr[1].ToString());
                    AS.chequeNumber = dr[2].ToString();
                    AS.quantity = Single.Parse(dr[3].ToString());
                    AS.UoM = dr[4].ToString();
                    AS.rate = Single.Parse(dr[5].ToString());
                    AS.debit = Decimal.Parse(dr[6].ToString());
                    AS.credit = Decimal.Parse(dr[7].ToString());
                    AS.description = dr[8].ToString();
                    AS.taxRate = Single.Parse(dr[9].ToString());
                    AS.isInHand = bool.Parse(dr[10].ToString());
                    AS.supplierID = int.Parse(dr[11].ToString());
                    AS.bankName = dr[12].ToString();
                    DateTime chDate = new DateTime(2000, 1, 1);
                    DateTime.TryParse(dr[13].ToString(), out chDate);
                    AS.chequeDate = chDate;
                    AS.chequeIssuedTo = int.Parse(dr[14].ToString());
                    int siteID = 0;
                    int.TryParse(dr[15].ToString(), out siteID);
                    AS.siteID = siteID;
                    listTransaction.Add(AS);

                    totalDebits += Decimal.Parse(dr[6].ToString());
                    totalCredits += Decimal.Parse(dr[7].ToString());
                }
            }

            if (totalCredits != totalDebits)
            {                
                throw new Exception("Debits and credits are not equal. Please try again.");
            }
        }

        internal void finalizeTransaction(string userName)
        {
            populateListOfAccounts(userName);
            populateParticulars();
            populateAccountNamesDictionary();
            populateSiteNamesDictionary();

            string startTransQuery = "Begin Transaction ";
            string midQuery = string.Empty;
            int transID = p.getTransactionID("tblGenAccountsTransactions");

            for (int i = 0; i < listTransaction.Count; i++)
            {
                int siteID = 0;
                int.TryParse(listTransaction[i].siteID.ToString(), out siteID);

                int accountID = listTransaction[i].accountID;
                string accountName = string.Empty;
                accountNamesDictionary.TryGetValue(accountID, out accountName);

                string siteName = string.Empty;
                siteNameDictionary.TryGetValue(siteID, out siteName);

                int voucherID = p.getVoucherID("tblGenAccountsTransactions");
                string voucher = p.getVoucher(listTransaction[i], voucherID, companyName, companyAddress, companyContacts, companyWebsite, companyEmail, accountName, siteName, transID);
                string debitString = transID.ToString() + accountID.ToString() + listTransaction[i].chequeNumber + listTransaction[i].quantity + listTransaction[i].UoM + listTransaction[i].rate + listTransaction[i].debit + listTransaction[i].credit + listTransaction[i].description + "HeatFoodWithElectricity";

                string debitAccountChecksum = p.EncryptStringToHex(debitString);

                string debitTransQuery = string.Format("   INSERT INTO tblGenAccountsTransactions (transactionID, accountID, date, chequeNumber, quantity, UoM, rate, debitAmount, creditAmount, description, checksum, voucherID, taxRate, isInHand, supplierID, bankName, chequeDate, chequeIssuedTo, operator, siteID) VALUES ({0},{1},'{2}','{3}',{4},'{5}',{6},{7},{8},'{9}','{10}', {11}, {12}, '{13}', {14}, '{15}', '{16}', {17},'{18}', {19})", transID, listTransaction[i].accountID, listTransaction[i].date, listTransaction[i].chequeNumber, listTransaction[i].quantity, listTransaction[i].UoM, listTransaction[i].rate, listTransaction[i].debit, listTransaction[i].credit, listTransaction[i].description, debitAccountChecksum, voucherID, listTransaction[i].taxRate, listTransaction[i].isInHand, listTransaction[i].supplierID, listTransaction[i].bankName, listTransaction[i].chequeDate, listTransaction[i].chequeIssuedTo, userName, listTransaction[i].siteID);
                string voucherQuery = string.Format("  INSERT INTO tblGenVouchers(voucherID, voucherText) VALUES ({0},'{1}')", voucherID, voucher);
                midQuery += debitTransQuery;
                midQuery += voucherQuery;
            }

            string endTransQuery = " Commit Transaction";
            string transQuery = startTransQuery + midQuery + endTransQuery;
            p.ExecuteQuery(transQuery);

            deleteTempTransactions();
        }

        private void deleteTempTransactions()
        {
            string query = "DELETE FROM tblGenAccountsTmpTransactions";
            p.ExecuteQuery(query);
        }

        internal Single checkStatus(int accountID)
        {
            Single status = 0f;
            string query = string.Format("SELECT SUM(dbo.tblGenAccountsTransactions.creditAmount) - SUM(dbo.tblGenAccountsTransactions.debitAmount) AS accStatus FROM dbo.tblGenAccountsTransactions  WHERE (dbo.tblGenAccountsTransactions.accountID = {0})", accountID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Single.TryParse(dr[0].ToString(), out status);
            }

            return status;
        }

        internal float checkSiteStatus(int accountID)
        {
            Single status = 0f;
            string query = string.Format("SELECT SUM(dbo.tblGenAccountsTransactions.creditAmount) - SUM(dbo.tblGenAccountsTransactions.debitAmount) AS accStatus FROM dbo.tblGenAccountsTransactions  WHERE (dbo.tblGenAccountsTransactions.siteID = {0})", accountID);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Single.TryParse(dr[0].ToString(), out status);
            }

            return status;
        }

    }
}