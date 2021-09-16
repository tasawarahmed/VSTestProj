using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.NotForProfit
{
    public partial class viewBeneficiaries : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Dictionary<string, object> photos = new Dictionary<string, object>();
        public Beneficiary beneficiary = new Beneficiary();
        public string dob = string.Empty;
        public string income = string.Empty;
        public string houseStatus = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataBindPhotos();
                pnlDetails.Visible = false;
            }
        }

        private void dataBindPhotos()
        {
            string imageSrc = string.Empty;
            string query = "SELECT benefID, photo FROM tblBeneficiaryPhoto";
            //string dtQuery = ("SELECT tblBeneficiaryPhoto.photo AS [Photo], tblBeneficiaryPhoto.benefID AS [id], tblBeneficiaries.name AS [Name], tblBeneficiaries.address AS [Address], tblBeneficiaries.city AS [City] FROM tblBeneficiaryPhoto INNER JOIN tblBeneficiaries ON tblBeneficiaryPhoto.benefID = tblBeneficiaries.id");
            string dtQuery = ("SELECT id AS id, name AS Name, address AS Address, city AS City FROM tblBeneficiaries");
            photos = p.getIntObjectDictionary(query);
            DataTable dt = p.GetDataTable(dtQuery);
            gridPhotos.DataSource = dt;
            gridPhotos.DataBind();

            for (int i = 0; i < gridPhotos.Rows.Count; i++)
            {
                Label lName = (Label)gridPhotos.Rows[i].FindControl("lblID");
                string id = lName.Text;
                object data = new object();
                if (photos.TryGetValue(id, out data))
                {
                    byte[] bytesPhoto = (byte[])data;
                    string base64Photo = Convert.ToBase64String(bytesPhoto);
                    imageSrc = "data:image/jpeg; base64," + base64Photo;
                }
                else
                {
                    imageSrc = p.getImageSourceOfDefaultPhoto();
                }

                HtmlImage image = (HtmlImage)gridPhotos.Rows[i].FindControl("dbImage");
                image.Src = imageSrc;
            }

            refreshGrid();
        }

        private void refreshGrid()
        {
            gridPhotos.UseAccessibleHeader = true;
            gridPhotos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void gridPhotos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            pnlDetails.Visible = 
                pnlFamilyDetails.Visible = false;
            try
            {
                int benefID = int.Parse(e.CommandArgument.ToString());
                showBeneficiary(benefID);
                pnlDetails.Visible = true;
                refreshGrid();
            }
            catch (Exception ex)
            {
                //showWarningMessage(ex.Message);
            }
        }

        private void showBeneficiary(int id)
        {
            string query = string.Format("SELECT  id, title, name, cnic, education, educationFurtherDetails, telephone, dateOfBirth, address, city, status, monthlyIncome, sourceOfIncome, specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks FROM tblBeneficiaries WHERE id = {0}", id);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                viewPhoto(id);
                beneficiary.benefName = dr[1].ToString() + " " + dr[2].ToString();
                beneficiary.address = dr[8].ToString();
                beneficiary.contact = dr[6].ToString();
                beneficiary.city = dr[9].ToString();
                beneficiary.primeDisability = "Prime Disability: " + dr[10].ToString();
                beneficiary.education = dr[4].ToString() + ": " + dr[5].ToString();
                beneficiary.cnic = dr[3].ToString();
                dob = DateTime.Parse(dr[7].ToString()).ToString("dd-MMM-yyyy");
                beneficiary.income = Decimal.Parse(dr[11].ToString());
                income = beneficiary.income.ToString("n");
                beneficiary.incomeSource = dr[12].ToString();
                beneficiary.specialIssue = dr[13].ToString();
                beneficiary.hasHouse = bool.Parse(dr[14].ToString());
                beneficiary.remarks = dr[19].ToString();
                beneficiary.totalFamily = int.Parse(dr[18].ToString());

                if (beneficiary.hasHouse)
                {
                    houseStatus = "Own House";
                }
                else
                {
                    houseStatus = "Rented House";
                }
            }
            List<Family> members = getFamilyList(id);
            if (members.Count > 0)
            {
                gridFamily.DataSource = null;
                gridFamily.DataBind();
                gridFamily.DataSource = members;
                gridFamily.DataBind();
                pnlFamilyDetails.Visible = true;
            }

        }

        private List<Family> getFamilyList(int id)
        {
            List<Family> members = new List<Family>();
            string query = string.Format("SELECT benefID, familyName, familyRelation, familyOccupation, familyAge, familyEducation FROM tblBeneficiaryFamilyDetails where benefID = {0}", id);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    Family f = new Family();
                    f.Name = dr[1].ToString();
                    f.Relationship = dr[2].ToString();
                    f.Occupation = dr[3].ToString();
                    f.Age = int.Parse(dr[4].ToString());
                    f.Education = dr[5].ToString();
                    members.Add(f);
                }
            }
                return members;
        }

        private void viewPhoto(int id)
        {
            string base64Photo = string.Empty;
            string query = string.Format("SELECT photo FROM tblBeneficiaryPhoto WHERE benefid = {0}", id);
            DataTable dt = p.GetDataTable(query);
            if (dt.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT photo FROM tblBeneficiaryPhoto WHERE benefid = @id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Connection = conn;
                        try
                        {
                            conn.Open();
                            byte[] bytesPhoto = (byte[])cmd.ExecuteScalar();
                            base64Photo = Convert.ToBase64String(bytesPhoto);

                            string imageSrc = "data:image/jpeg; base64," + base64Photo;
                            myImage.Src = imageSrc;
                        }
                        catch (Exception excptn)
                        {
                            //showExceptionMessage(excptn.Message);
                        }
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT photo FROM tblBeneficiaryPhoto WHERE id = @id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", "defaultPhoto");
                        cmd.Connection = conn;
                        try
                        {
                            conn.Open();
                            byte[] bytesPhoto = (byte[])cmd.ExecuteScalar();
                            base64Photo = Convert.ToBase64String(bytesPhoto);

                            string imageSrc = "data:image/jpeg; base64," + base64Photo;
                            myImage.Src = imageSrc;
                        }
                        catch (Exception excptn)
                        {
                            //showExceptionMessage(excptn.Message);
                        }
                    }
                }
            }

        }
    }
}