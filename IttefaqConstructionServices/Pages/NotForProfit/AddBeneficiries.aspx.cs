using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace IttefaqConstructionServices.Pages.NotForProfit
{
    public partial class AddBeneficiries : System.Web.UI.Page
    {
        Utilities p = new Utilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                viewPhoto("defaultPhoto");
            }
        }

        private void showWarningMessage(string s)
        {
            lblWarning.Visible = true;
            lblWarning.Text = p.getWarningMessage(s);
        }

        private void showSuccessMessage(string s)
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = p.getSuccessMessage(s);
        }

        private void clearLabels()
        {
            lblSuccess.Visible = lblWarning.Visible = false;
        }

        private void loadGrid(Family f)
        {
            List<Family> members = getFamilyList("Normal");

            if (f.Name != null)
            {
                members.Add(f);
            }

            gridDataBind(members);
        }

        private List<Family> getFamilyList(string mode)
        {
            List<Family> members = new List<Family>();
            bool editStatus = false;

            for (int i = 0; i < gridFamily.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gridFamily.Rows[i].FindControl("chkChkBox");

                Label lName = (Label)gridFamily.Rows[i].FindControl("lblName");
                Label lAge = (Label)gridFamily.Rows[i].FindControl("lblAge");
                Label lRelationship = (Label)gridFamily.Rows[i].FindControl("lblRelationship");
                Label lEducation = (Label)gridFamily.Rows[i].FindControl("lblEducation");
                Label lOccupation = (Label)gridFamily.Rows[i].FindControl("lblOccupation");

                if (mode == "Normal")
                {
                    if (lName.Text != string.Empty)
                    {
                        members.Add(getFamilyObject(lName.Text, lAge.Text, lRelationship.Text, lEducation.Text, lOccupation.Text));
                    }
                }

                else if (mode == "Delete")
                {
                    if (!chk.Checked)
                    {
                        if (lName.Text != string.Empty)
                        {
                            members.Add(getFamilyObject(lName.Text, lAge.Text, lRelationship.Text, lEducation.Text, lOccupation.Text));
                        }
                    }
                }

                else
                {
                    if (!chk.Checked)
                    {
                        if (lName.Text != string.Empty)
                        {
                            members.Add(getFamilyObject(lName.Text, lAge.Text, lRelationship.Text, lEducation.Text, lOccupation.Text));
                        }
                    }
                    else if (editStatus == true)
                    {
                        if (lName.Text != string.Empty)
                        {
                            members.Add(getFamilyObject(lName.Text, lAge.Text, lRelationship.Text, lEducation.Text, lOccupation.Text));
                        }
                    }

                    else
                    {
                        txtFamilyName.Text = lName.Text;
                        txtFamilyAge.Text = lAge.Text;
                        cmbFamilyRelationship.SelectedValue = lRelationship.Text;
                        txtEducation.Text = lEducation.Text;
                        txtFamilyOccupation.Text = lOccupation.Text;
                        editStatus = true;
                    }
                }
            }

            return members;
        }

        private void gridDataBind(List<Family> members)
        {
            gridFamily.DataSource = null;
            gridFamily.DataBind();
            gridFamily.DataSource = members;
            gridFamily.DataBind();
            toggleVisibilityOfPanel(members.Count);
        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {
            string id = Session["UserName"].ToString();
            HttpPostedFile postedFile = Request.Files[0];

            if (postedFile != null && postedFile.ContentLength > 0)
            //if(true)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtenssion = Path.GetExtension(fileName);

                fileExtenssion = fileExtenssion.ToLower();

                if (fileExtenssion == ".jpg" || fileExtenssion == ".jpeg" || fileExtenssion == ".bmp" || fileExtenssion == ".png" || fileExtenssion == ".gif")
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] photo = binaryReader.ReadBytes((int)stream.Length);

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ToString();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = " DELETE FROM tblBeneficiaryPhoto WHERE id = @id  INSERT INTO  tblBeneficiaryPhoto(id, photo) VALUES (@id,@photo)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@photo", photo);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Connection = conn;
                            try
                            {
                                conn.Open();
                                int effectedRows = 0;
                                effectedRows = cmd.ExecuteNonQuery();

                                if (effectedRows > 0)
                                {
                                    //showExceptionMessage("Photo updated successfully");
                                    //txtStuIDView.Text = stuID.ToString();
                                    viewPhoto(id);
                                }
                                else
                                {
                                }

                            }
                            catch (Exception excptn)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                //showExceptionMessage("Select a file and a valid student ID.");
            }
        }

        private void viewPhoto(string id)
        {
            string base64Photo = string.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ToString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT photo FROM tblBeneficiaryPhoto WHERE id = @id";
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

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            Family f = new Family();

            f.Name = p.FixString(txtFamilyName.Text);
            f.Relationship = cmbFamilyRelationship.SelectedValue;
            f.Age = int.Parse(txtFamilyAge.Text);
            f.Education = p.FixString(txtEducation.Text);
            f.Occupation = p.FixString(txtFamilyOccupation.Text);

            loadGrid(f);
            txtFamilyName.Text = txtFamilyAge.Text = txtEducation.Text = txtFamilyOccupation.Text = string.Empty;
            cmbFamilyRelationship.SelectedIndex = 0;
        }

        protected void btnDelChecked_Click(object sender, EventArgs e)
        {
            List<Family> members = getFamilyList("Delete");
            gridDataBind(members);
        }

        private void toggleVisibilityOfPanel(int count)
        {
            if (count > 0)
            {
                pnlGrid.Visible = true;
            }
            else
            {
                pnlGrid.Visible = false;
            }
        }

        protected void btnEditChecked_Click(object sender, EventArgs e)
        {
            List<Family> members = getFamilyList("Edit");
            gridDataBind(members);
        }

        private Family getFamilyObject(string name, string age, string relationship, string education, string occupation)
        {
            Family f = new Family();
            f.Name = name;
            f.Age = int.Parse(age);
            f.Relationship = relationship;
            f.Education = education;
            f.Occupation = occupation;
            return f;
        }

        protected void btnAddBeneficiary_Click(object sender, EventArgs e)
        {
            clearLabels();
            try
            {
                string cnic = p.FixString(txtCNIC.Text);

                if (cnic.Length < 13)
                {
                    throw new Exception("Not seems to be a valid CNIC Number");
                }

                if (exists(cnic))
                {
                    throw new Exception("Beneficiary against given CNIC already exists");
                }

                if (cmbTitle.SelectedIndex > 0 && cmbEducation.SelectedIndex > 0 && cmbCity.SelectedIndex > 0 && cmbHouseStatus.SelectedIndex > 0 && cmbIstehqaqBasis.SelectedIndex > 0)
                {
                    string title = p.FixString(cmbTitle.SelectedValue);
                    string name = p.FixString(txtBeneficiaryName.Text);
                    string education = cmbEducation.SelectedValue;
                    string educationFurtherDetails = p.FixString(txtFurtherEduDetails.Text);
                    string telephone = p.FixString(txtPhone.Text);
                    DateTime dateOfBirth = DateTime.Parse(txtDate.Text);
                    string address = p.FixString(txtAddress.Text);
                    string city = cmbCity.SelectedValue;
                    string status = cmbIstehqaqBasis.SelectedValue;
                    Decimal salary = Decimal.Parse (Validator.ValidatePositiveNumber(txtMonthlyIncome.Text, "Monthly Income"));
                    string sourceOfIncome = p.FixString(txtIncomeSource.Text);
                    string fileNumber = p.FixString(txtFileReference.Text);
                    string specialIssue = p.FixString(txtSpecialIssue.Text);
                    bool hasHouse = (cmbHouseStatus.SelectedIndex == 1);
                    List<Family> familyList = getFamilyList("Normal");
                    int totalFamilyMembers = familyList.Count;
                    bool hasFather = false;
                    bool hasMother = false;
                    bool hasKids = false;
                    string remarks = p.FixString(txtRemarks.Text);
                    string familyQuery = string.Empty;

                    for (int i = 0; i < familyList.Count; i++)
                    {
                        if (familyList[i].Relationship == "Father")
                        {
                            hasFather = true;
                        }
                        else if (familyList[i].Relationship == "Mother")
                        {
                            hasMother = true;
                        }
                        else if (familyList[i].Relationship == "Kid")
                        {
                            hasKids = true;
                        }
                        else
                        {

                        }

                        string q = string.Format(" INSERT INTO tblBeneficiaryFamilyDetails(benefID, familyName, familyRelation, familyOccupation, familyAge, familyEducation) VALUES (BeneficiaryID,'{0}','{1}','{2}',{3},'{4}')", familyList[i].Name, familyList[i].Relationship, familyList[i].Occupation, familyList[i].Age, familyList[i].Education);
                        familyQuery += q;
                    }
                    string query = string.Format("INSERT INTO tblBeneficiaries " +
                        "(title, name, cnic, education, educationFurtherDetails, " +
                        "telephone, dateOfBirth, address, city, status, monthlyIncome, sourceOfIncome, " +
                        " specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks, fileReference) VALUES" +
                        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}','{15}','{16}',{17},'{18}', '{19}')",
                        title, name, cnic, education, educationFurtherDetails, telephone, dateOfBirth, address, city, status, salary, sourceOfIncome,
                        specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks, fileNumber);

                    p.ExecuteQuery(query);

                    string getIDQuery = string.Format("SELECT id FROM tblBeneficiaries WHERE name = '{0}' and cnic = '{1}' and address = '{2}'", name, cnic, address);
                    int ID = p.getIntValue(getIDQuery);
                    familyQuery = familyQuery.Replace("BeneficiaryID", ID.ToString());

                    string updatePhotoQuery = string.Format(" UPDATE tblBeneficiaryPhoto SET benefID = {0}, id = '{1}' WHERE id = '{2}'", ID, string.Empty, Session["UserName"].ToString());
                    familyQuery += updatePhotoQuery;

                    p.ExecuteTransactionQuery(familyQuery);

                    showSuccessMessage("Beneficiary Added Successfully.");
                }
                else
                {
                    throw new Exception("Select proper values from dropdowns and try again.");
                }

            }
            catch (Exception ex)
            {
                showWarningMessage(ex.Message);
            }
        }

        private bool exists(string cnic)
        {
            bool result = false;
            
            string query = string.Format("SELECT name FROM tblBeneficiaries where cnic = '{0}'", cnic);
            DataTable dt = p.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }
    }
}