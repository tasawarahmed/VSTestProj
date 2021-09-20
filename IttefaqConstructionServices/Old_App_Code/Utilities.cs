using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.App_Code
{
    public class Utilities
    {
        Connection c = new Connection();
        List<int> inactiveStudentsIDs = new List<int>();

        public int ExecuteQuery(string queryStr)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(queryStr, connection);
            int rowsEffected = command.ExecuteNonQuery();

            connection.Close();
            return rowsEffected;
        }

        /// <summary>

        /// Executes a query. 

        /// Example query 1: DELETE FROM Table1

        /// Example query 2 (Pass ID & Name in parameters): DELETE FROM Table1 WHERE ID = {0} AND Name = {1}

        /// </summary>

        public int ExecuteParameterizedQuery(string text, string[] parameterNames, params object[] paramters)
        {
            using (var connection = new SqlConnection(c.connectionString()))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    if (paramters.Any())
                    {
                        command.CommandText = text;
                        //command.CommandText = String.Format(text, paramters.Select((_, i) => "@p" + i));

                        for (int i = 0; i < paramters.Length; i++)
                        {
                            //command.Parameters.AddWithValue("p" + i, paramters[i]);
                            command.Parameters.AddWithValue(parameterNames[i], paramters[i]);
                        }
                    }
                    else
                    {
                        command.CommandText = text;
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteInsertOrUpdateQuery(string selectQuery, string insertQuery, string updateQuery)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            DataTable dt = new DataTable();
            dt = GetDataTable(selectQuery);
            int rowsEffected = 0;

            if (dt.Rows.Count > 0)
            {
                rowsEffected = ExecuteQuery(updateQuery);
            }

            else
            {
                rowsEffected = ExecuteQuery(insertQuery);
            }

            connection.Close();
            return rowsEffected;
        }

        public int executeInsertQueryOnBasisOfSelectQuery(string selectQuery, string insertQuery)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            DataTable dt = new DataTable();
            dt = GetDataTable(selectQuery);
            int rowsEffected = 0;

            if (dt.Rows.Count > 0)
            {
            }

            else
            {
                rowsEffected = ExecuteQuery(insertQuery);
            }

            connection.Close();
            return rowsEffected;
        }

        public bool ifRecordsExist(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                connection.Close();
                return true;

            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public string FixString(string input)
        {
            input = input.Replace("'", "''");
            return input;
        }

        public string fixDate(int day, int month, int year)
        {
            string stringDay = day.ToString();
            string stringMonth = month.ToString();
            string stringYear = year.ToString();

            if (day < 10)
            {
                stringDay = "0" + stringDay;
            }
            if (month < 10)
            {
                stringMonth = "0" + stringMonth;
            }

            string date = stringMonth + "/" + stringDay + "/" + stringYear;
            return date;
        }

        public string fixDateWithMonthNames(int day, int month, int year)
        {
            string[] months = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string stringDay = day.ToString();
            string stringMonth = months[month - 1];
            string stringYear = year.ToString();

            if (day < 10)
            {
                stringDay = "0" + stringDay;
            }
            //if (month < 10)
            //{ 
            //    stringMonth = "0" + stringMonth;
            //}

            string date = stringDay + "-" + stringMonth + "-" + stringYear;
            return date;
        }

        public DateTime getDate(string date)
        {
            string[] parts = date.Split('/');

            DateTime newDate = new DateTime(int.Parse(parts[2]), int.Parse(parts[0]), int.Parse(parts[1]));
            return newDate;
        }

        public static Dictionary<int, string> getDictionary(string query)
        {
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();

            SqlConnection connection = new SqlConnection(Connection.getConnectionString());
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(query, connection);

            SqlDataReader dr = selectCommand.ExecuteReader();

            while (dr.Read())
            {

                int ID = 0;
                int.TryParse(dr[0].ToString(), out ID);

                string name = dr[1].ToString();

                myDictionary.Add(ID, name);
            }
            dr.Close();
            connection.Close();

            return myDictionary;
        }

        public static List<StringsDDList> getStringsDDList(string query)
        {
            List<StringsDDList> myDDList = new List<StringsDDList>();

            StringsDDList e = new StringsDDList();
            e.DisplayName = "Select One";

            myDDList.Add(e);

            SqlConnection connection = new SqlConnection(Connection.getConnectionString());
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(query, connection);

            SqlDataReader dr = selectCommand.ExecuteReader();

            if (dr.Read())
            {
                StringsDDList item = new StringsDDList();
                item.DisplayName = dr[0].ToString();

                myDDList.Add(item);
            }
            else
            {
                myDDList.Clear();
            }

            while (dr.Read())
            {
                StringsDDList d = new StringsDDList();
                d.DisplayName = dr[0].ToString();

                myDDList.Add(d);
            }
            connection.Close();
            return myDDList;
        }

        public static List<DDList> getDDList(string query)
        {
            List<DDList> myDDList = new List<DDList>();

            DDList e = new DDList();
            e.DisplayName = "Select One";
            e.Value = 0;

            myDDList.Add(e);

            SqlConnection connection = new SqlConnection(Connection.getConnectionString());
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(query, connection);

            SqlDataReader dr = selectCommand.ExecuteReader();

            if (dr.Read())
            {
                DDList item = new DDList();
                item.DisplayName = dr[0].ToString();
                item.Value = int.Parse(dr[1].ToString());

                myDDList.Add(item);
            }
            else
            {
                myDDList.Clear();
            }

            while (dr.Read())
            {
                DDList d = new DDList();
                d.DisplayName = dr[0].ToString();
                d.Value = int.Parse(dr[1].ToString());

                myDDList.Add(d);
            }
            connection.Close();
            return myDDList;
        }

        public static List<DDList> getInventoryItemsDDList(string query)
        {
            List<DDList> myDDList = new List<DDList>();

            DDList e = new DDList();
            e.DisplayName = "Select One";
            e.Value = 0;

            myDDList.Add(e);

            SqlConnection connection = new SqlConnection(Connection.getConnectionString());
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(query, connection);

            SqlDataReader dr = selectCommand.ExecuteReader();

            if (dr.Read())
            {
                if (int.Parse(dr[5].ToString()) > 0)
                {
                    DDList item = new DDList();
                    item.DisplayName = dr[0].ToString();
                    item.Value = int.Parse(dr[1].ToString());

                    myDDList.Add(item);
                }
            }
            else
            {
                myDDList.Clear();
            }

            while (dr.Read())
            {
                if (int.Parse(dr[5].ToString()) > 0)
                {
                    DDList d = new DDList();
                    d.DisplayName = dr[0].ToString();
                    d.Value = int.Parse(dr[1].ToString());

                    myDDList.Add(d);
                }
            }

            if (myDDList.Count == 1)
            {
                myDDList.Clear();
            }

            connection.Close();
            return myDDList;
        }

        private bool dataIntegrityOK(string[] IDsForAbsent, string[] IDsForLate, string[] IDsForShortLeave)
        {
            List<int> latesAndShortLeaves = new List<int>();

            foreach (var s in IDsForLate)
            {
                int ID = 0;
                if (int.TryParse(s, out ID))
                {
                    latesAndShortLeaves.Add(ID);
                }
            }

            foreach (var s in IDsForShortLeave)
            {
                int ID = 0;
                if (int.TryParse(s, out ID))
                {
                    latesAndShortLeaves.Add(ID);
                }
            }

            foreach (var s in IDsForAbsent)
            {
                int ID = 0;
                if (int.TryParse(s, out ID))
                {
                    if (latesAndShortLeaves.Contains(ID))
                    {
                        string message = "The ID: " + ID.ToString() + " reported in absent is also reported in late or short leave.";
                        throw new Exception(message);
                    }
                }
            }
            return true;
            //throw new NotImplementedException();
        }

        private void checkForInactivity(string[] IDs)
        {
            foreach (var s in IDs)
            {
                int ID = 0;
                if (int.TryParse(s, out ID))
                {
                    if (inactiveStudentsIDs.Contains(ID))
                    {
                        string message = "Your entries contains an inactive student with ID: " + ID.ToString() + ". Please remove that from list and try again.";
                        throw new Exception(message);
                    }
                }
            }
        }

        internal int GetSum(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            int days = 0;

            while (dr.Read())
            {
                int d = 0;
                if (int.TryParse(dr[0].ToString(), out d))
                {
                    days += d;
                }
            }

            connection.Close();
            return days;
        }

        internal int GetCount(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            int days = 0;

            while (dr.Read())
            {
                int d = 0;
                if (int.TryParse(dr[0].ToString(), out d))
                {
                    days += d;
                }
            }

            connection.Close();
            return days;
        }

        internal List<int> getIntList(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            List<int> entries = new List<int>();

            while (dr.Read())
            {
                int d = 0;
                if (int.TryParse(dr[0].ToString(), out d))
                {
                    entries.Add(d);
                }
            }

            connection.Close();
            return entries;
        }

        internal List<string> getStringList(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            List<string> entries = new List<string>();

            while (dr.Read())
            {
                string d = dr[0].ToString();
                entries.Add(d);
            }

            connection.Close();
            return entries;
        }

        internal Dictionary<int, DateTime> getIntDateDictionary(string p)
        {
            Dictionary<int, DateTime> dict = new Dictionary<int, DateTime>();

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(p, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                int d = 0;

                DateTime s = DateTime.Now;

                if (int.TryParse(dr[0].ToString(), out d) && DateTime.TryParse(dr[1].ToString(), out s))
                {
                    dict.Add(d, s);
                }
            }

            connection.Close();
            return dict;
            //throw new NotImplementedException();
        }

        internal Dictionary<int, string> getIntStringDictionary(string p)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(p, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                int d = int.Parse(dr[0].ToString());
                string s = dr[1].ToString();

                dict.Add(d, s);
            }

            connection.Close();
            return dict;
            //throw new NotImplementedException();
        }

        internal Dictionary<string, int> getStringIIntDictionary(string query)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                int d = int.Parse(dr[1].ToString());
                string s = dr[0].ToString();

                dict.Add(s, d);
            }

            connection.Close();
            return dict;
            //throw new NotImplementedException();
        }

        internal DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            string str = c.connectionString();
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
                {
                    da.Fill(dt);
                }
            }
            return dt;
            //throw new NotImplementedException();
        }

        internal Dictionary<int, int> getIntIntDictionary(string query)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                int d = int.Parse(dr[0].ToString());
                int s = int.Parse(dr[1].ToString());

                dict.Add(d, s);
            }

            connection.Close();
            return dict;
            //throw new NotImplementedException();
        }

        internal int getIntValue(string query)
        {
            int value = 0;

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                int.TryParse(dr[0].ToString(), out value);
            }

            connection.Close();
            return value;
        }

        internal string trimSpaces(string input)
        {
            input = input.Replace(" ", "");
            return input;
        }

        internal string getEncryptedString(string s)
        {
            string pw = s;
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pw);
            data = x.ComputeHash(data);
            string encryption = System.Text.Encoding.ASCII.GetString(data);

            return encryption;
        }

        public static string EncryptStringToHex(string s)
        {
            var provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(s);

            data = provider.ComputeHash(data);
            //string encryption = System.Text.Encoding.ASCII.GetString(data);

            string encrypted = String.Join(String.Empty, data.Select(b => b.ToString("X2")));

            return encrypted;
        }

        protected void ExportToPDF(GridView GridView1)
        {
            //System.Web.HttpResponse Response;

            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {

            //        //Hide the Column containing CheckBox
            //        //GridView1.Columns[0].Visible = false;
            //        foreach (GridViewRow row in GridView1.Rows)
            //        {
            //            if (row.RowType == DataControlRowType.DataRow)
            //            {
            //                //Hide the Row if CheckBox is not checked
            //                //row.Visible = (row.FindControl("chkSelect") as CheckBox).Checked;
            //            }
            //        }

            //        GridView1.RenderControl(hw);
            //        StringReader sr = new StringReader(sw.ToString());
            //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        htmlparser.Parse(sr);
            //        pdfDoc.Close();

            //        Response.ContentType = "application/pdf";
            //        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.Write(pdfDoc);
            //        Response.End();
            //    }
            //}
        }

        internal string getStringValue(string query)
        {
            string name = string.Empty;

            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                name = dr[0].ToString();
            }

            connection.Close();

            return name;
        }

        internal string getExceptionMessage(string p)
        {
            string message = "An error occurred: " + p + " Please try again later";
            return message;
        }

        internal string getOfficeName()
        {
            string name = string.Empty;
            string query = "SELECT schoolName FROM tblActivationInfo";
            name = getStringValue(query);
            return name;
        }

        internal string getOfficeAddress()
        {
            string name = string.Empty;
            string query = "SELECT schoolAddress FROM tblActivationInfo";
            name = getStringValue(query);
            return name;
        }

        internal string getOfficePhone()
        {
            string name = string.Empty;
            string query = "SELECT phone FROM tblGenParticulars";
            name = getStringValue(query);
            return name;
        }

        internal string getOfficeEmail()
        {
            string name = string.Empty;
            string query = "SELECT emailAddress FROM tblGenParticulars";
            name = getStringValue(query);
            return name;
        }

        internal string getOfficeWebsite()
        {
            string name = string.Empty;
            string query = "SELECT website FROM tblGenParticulars";
            name = getStringValue(query);
            return name;
        }

        internal string convertToParagraph(string p)
        {
            string newS = string.Empty;
            newS = "<p>" + p + "</p>";
            return newS;
        }

        internal string convertToHeadingH2(string p)
        {
            string newS = string.Empty;
            newS = "<h2>" + p + "</h2>";
            return newS;
        }

        internal string convertToHeadingH3(string p)
        {
            string newS = string.Empty;
            newS = "<h3>" + p + "</h3>";
            return newS;
        }

        internal string convertToStrong(string p)
        {
            string newS = string.Empty;
            newS = "<strong>" + p + "</strong>";
            return newS;
        }

        internal string getHeader(int stuID)
        {
            string header = string.Empty;
            string query = string.Format("SELECT tblStuMain.stuRegNumber, tblStuMain.stuName, tblStuMain.stuFatherName, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact, tblActivationInfo.schoolName, tblActivationInfo.schoolAddress, tblGenClasses.className FROM tblGenClasses INNER JOIN tblStuMain ON tblGenClasses.classID = tblStuMain.stuCurrentClass CROSS JOIN tblActivationInfo CROSS JOIN tblGenParticulars WHERE tblStuMain.stuID = {0}", stuID);
            //string query = string.Format("SELECT tblStuMain.stuRegNumber, tblStuMain.stuName, tblStuMain.stuFatherName, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact, tblActivationInfo.schoolName, tblActivationInfo.schoolAddress FROM tblActivationInfo CROSS JOIN tblGenParticulars CROSS JOIN tblStuMain WHERE tblStuMain.stuID = {0}", stuID);
            DataTable dt = GetDataTable(query);

            string regNum = string.Empty;
            string name = string.Empty;
            string father = string.Empty;
            string email = string.Empty;
            string website = string.Empty;
            string phone = string.Empty;
            string contactPerson = string.Empty;
            string schoolName = string.Empty;
            string schoolAddress = string.Empty;
            string className = string.Empty;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                regNum = dr[0].ToString();
                name = dr[1].ToString();
                father = dr[2].ToString();
                email = dr[3].ToString();
                website = dr[4].ToString();
                phone = dr[5].ToString();
                contactPerson = dr[6].ToString();
                schoolName = dr[7].ToString();
                schoolAddress = dr[8].ToString();
                className = dr[9].ToString();
            }

            string line = "-----------------------------------------------------------------";
            string date = convertToStrong("Dated: ") + fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            regNum = convertToStrong("Reg No: ") + regNum;
            name = convertToStrong("Name: ") + name;
            father = convertToStrong("Father Name: ") + father;
            className = convertToStrong("Class Name: ") + className;
            website = convertToStrong("Website: ") + website;
            email = convertToStrong("Email: ") + email;

            header = convertToHeadingH2(schoolName) + convertToParagraph(schoolAddress) + convertToParagraph(phone) + convertToParagraph(website) + convertToParagraph(email) + convertToParagraph(line) + convertToParagraph(date) + convertToParagraph(regNum) + convertToParagraph(name) + convertToParagraph(father) + convertToParagraph(className) + convertToParagraph(convertToStrong("Payment Details:"));

            return header;
        }

        internal string getHeader(string stuName, string yes)
        {
            string header = string.Empty;
            string query = "SELECT tblActivationInfo.schoolName, tblActivationInfo.schoolAddress, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact FROM tblActivationInfo CROSS JOIN tblGenParticulars";
            //string query = string.Format("SELECT tblStuMain.stuRegNumber, tblStuMain.stuName, tblStuMain.stuFatherName, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact, tblActivationInfo.schoolName, tblActivationInfo.schoolAddress FROM tblActivationInfo CROSS JOIN tblGenParticulars CROSS JOIN tblStuMain WHERE tblStuMain.stuID = {0}", stuID);
            DataTable dt = GetDataTable(query);

            string name = stuName;
            string email = string.Empty;
            string website = string.Empty;
            string phone = string.Empty;
            string contactPerson = string.Empty;
            string schoolName = string.Empty;
            string schoolAddress = string.Empty;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                schoolName = dr[0].ToString();
                schoolAddress = dr[1].ToString();
                email = dr[2].ToString();
                website = dr[3].ToString();
                phone = dr[4].ToString();
                contactPerson = dr[5].ToString();
            }

            string line = "-----------------------------------------------------------------";
            string date = convertToStrong("Dated: ") + fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            name = convertToStrong("Name: ") + name;
            website = convertToStrong("Website: ") + website;
            email = convertToStrong("Email: ") + email;

            header = convertToHeadingH2(schoolName) + convertToParagraph(schoolAddress) + convertToParagraph(phone) + convertToParagraph(website) + convertToParagraph(email) + convertToParagraph(line) + convertToParagraph(date) + convertToParagraph(name);

            return header;
        }

        internal string getHeader(string stuID)
        {
            string header = string.Empty;
            string query = string.Format("SELECT tblStuMain.stuFatherCNIC, tblStuMain.stuFatherName, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact, tblActivationInfo.schoolName, tblActivationInfo.schoolAddress FROM tblGenClasses INNER JOIN tblStuMain ON tblGenClasses.classID = tblStuMain.stuCurrentClass CROSS JOIN tblActivationInfo CROSS JOIN tblGenParticulars WHERE tblStuMain.stuFatherCNIC = {0}", stuID);
            //string query = string.Format("SELECT tblStuMain.stuRegNumber, tblStuMain.stuName, tblStuMain.stuFatherName, tblGenParticulars.emailAddress, tblGenParticulars.website, tblGenParticulars.phone, tblGenParticulars.contact, tblActivationInfo.schoolName, tblActivationInfo.schoolAddress FROM tblActivationInfo CROSS JOIN tblGenParticulars CROSS JOIN tblStuMain WHERE tblStuMain.stuID = {0}", stuID);
            DataTable dt = GetDataTable(query);

            string father = string.Empty;
            string email = string.Empty;
            string website = string.Empty;
            string phone = string.Empty;
            string contactPerson = string.Empty;
            string schoolName = string.Empty;
            string schoolAddress = string.Empty;
            string cnic = string.Empty;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                cnic = dr[0].ToString();
                father = dr[1].ToString();
                email = dr[2].ToString();
                website = dr[3].ToString();
                phone = dr[4].ToString();
                contactPerson = dr[5].ToString();
                schoolName = dr[6].ToString();
                schoolAddress = dr[7].ToString();
            }

            string line = "-----------------------------------------------------------------";
            string date = convertToStrong("Dated: ") + fixDateWithMonthNames(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            cnic = convertToStrong("CNIC No: ") + cnic;
            father = convertToStrong("Father Name: ") + father;
            website = convertToStrong("Website: ") + website;
            email = convertToStrong("Email: ") + email;

            header = convertToHeadingH2(schoolName) + convertToParagraph(schoolAddress) + convertToParagraph(phone) + convertToParagraph(website) + convertToParagraph(email) + convertToParagraph(line) + convertToParagraph(date) + convertToParagraph(cnic) + convertToParagraph(father) + convertToParagraph(convertToStrong("Payment Details:"));

            return header;
        }

        internal string getFooter()
        {
            string footer = string.Empty;

            string line = "-----------------------------------------------------------------";
            string message = "Fee received is non refundable";
            footer = convertToParagraph(line) + convertToParagraph(message);

            return footer;
        }

        internal string getFooter(string message)
        {
            string footer = string.Empty;

            string line = "-----------------------------------------------------------------";
            footer = convertToParagraph(line) + convertToParagraph(message);

            return footer;
        }

        internal static List<DDList> getDDList(DataTable dt)
        {
            List<DDList> myDDList = new List<DDList>();

            DDList e = new DDList();
            e.DisplayName = "Select One";
            e.Value = 0;

            myDDList.Add(e);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DDList item = new DDList();
                    item.DisplayName = dr[0].ToString();
                    item.Value = int.Parse(dr[1].ToString());

                    myDDList.Add(item);
                }

            }
            else
            {
                myDDList.Clear();
            }

            return myDDList;
        }

        internal List<string> getStringsList(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            List<string> entries = new List<string>();

            while (dr.Read())
            {
                string d = dr[0].ToString();
                entries.Add(d);
            }

            connection.Close();
            return entries;
        }

        internal List<DateTime> getDatesList(string query)
        {
            SqlConnection connection = new SqlConnection(c.connectionString());
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dr = command.ExecuteReader();

            List<DateTime> entries = new List<DateTime>();

            while (dr.Read())
            {
                DateTime d = new DateTime();

                if (DateTime.TryParse(dr[0].ToString(), out d))
                {
                    entries.Add(d);
                }
            }

            connection.Close();
            return entries;
        }
    }
}