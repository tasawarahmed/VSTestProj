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
    public partial class Webcam : System.Web.UI.Page
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
            clearLabels();
            string id = Session["UserName"].ToString();
            HttpPostedFile postedFile = Request.Files[0];

            try
            {
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
                                    lblWarning.Text = p.getExceptionMessage(excptn.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Only .jpg, .jpeg, .bmp, .png, and .gif files are acceptable.");
                    }
                }
                else
                {
                    throw new Exception("Select a file and retry.");
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getWarningMessage(ex.Message);
                lblWarning.Visible = true;
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
                        lblWarning.Text = p.getExceptionMessage(excptn.Message);
                    }
                }
            }
        }

        private void viewPhoto(int id)
        {
            string base64Photo = string.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ICSConnectionString"].ToString();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT photo FROM tblBeneficiaryPhoto WHERE benefID = @id";
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
                        lblWarning.Text = p.getExceptionMessage(excptn.Message);
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

                if (cmbTitle.SelectedIndex > 0 && cmbEducation.SelectedIndex > 0 && cmbCity.SelectedIndex > 0 && cmbHouseStatus.SelectedIndex > 0 && cmbIstehqaqBasis.SelectedIndex > 0 && cmbBenefStatus.SelectedIndex > 0)
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
                    Decimal salary = Decimal.Parse(Validator.ValidatePositiveNumber(txtMonthlyIncome.Text, "Monthly Income"));
                    string sourceOfIncome = p.FixString(txtIncomeSource.Text);
                    string fileNumber = p.FixString(txtFileReference.Text);
                    string beneficiaryStatus = cmbBenefStatus.SelectedValue;
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

                    string query = string.Format("UPDATE tblBeneficiaries " +
                        "SET title = '{0}', name = '{1}', education = '{2}', educationFurtherDetails = '{3}', telephone = '{4}', dateOfBirth = '{5}', address = '{6}', " +
                        "city ='{7}', status ='{8}', monthlyIncome ={9}, sourceOfIncome ='{10}', specialIssue ='{11}', hasHouse ='{12}', hasFather ='{13}', " +
                        "hasMother ='{14}', hasKids ='{15}', totalFamilyMembers = {16}, remarks = '{17}', fileReference = '{18}', beneficiaryStatus ='{19}' WHERE (cnic = '{20}')",
                        title, name, education, educationFurtherDetails, telephone, dateOfBirth, address, city, status, salary, sourceOfIncome,
                        specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks, fileNumber, beneficiaryStatus, cnic);
                    //string query = string.Format("INSERT INTO tblBeneficiaries " +
                    //    "(title, name, cnic, education, educationFurtherDetails, " +
                    //    "telephone, dateOfBirth, address, city, status, monthlyIncome, sourceOfIncome, " +
                    //    " specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks, fileReference) VALUES" +
                    //    "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}','{15}','{16}',{17},'{18}', '{19}')",
                    //    title, name, cnic, education, educationFurtherDetails, telephone, dateOfBirth, address, city, status, salary, sourceOfIncome,
                    //    specialIssue, hasHouse, hasFather, hasMother, hasKids, totalFamilyMembers, remarks, fileNumber);

                    p.ExecuteQuery(query);

                    string getIDQuery = string.Format("SELECT id FROM tblBeneficiaries WHERE name = '{0}' and cnic = '{1}' and address = '{2}'", name, cnic, address);
                    int ID = p.getIntValue(getIDQuery);
                    familyQuery = familyQuery.Replace("BeneficiaryID", ID.ToString());
                    string deleteFamilyQuery = string.Format("  DELETE FROM tblBeneficiaryFamilyDetails WHERE benefID = {0}  ", ID);
                    familyQuery = deleteFamilyQuery + familyQuery;

                    string updatePhotoQuery = string.Format(" UPDATE tblBeneficiaryPhoto SET benefID = {0}, id = '{1}' WHERE id = '{2}'", ID, string.Empty, Session["UserName"].ToString());

                    if (alreadyHasPhoto(ID, Session["UserName"].ToString()))
                    {
                        string deletePhotoQuery = string.Format("  DELETE FROM tblBeneficiaryPhoto WHERE benefID = {0}  ", ID);
                        updatePhotoQuery = deletePhotoQuery + updatePhotoQuery;
                    }

                    familyQuery += updatePhotoQuery;

                    p.ExecuteTransactionQuery(familyQuery);

                    showSuccessMessage("Beneficiary Updated Successfully.");
                    resetForm();
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

        private void resetForm()
        {
            clearLabels();
            txtAddress.Text = txtBeneficiaryName.Text = txtCNIC.Text = txtDate.Text = txtEducation.Text = txtFamilyAge.Text = txtFamilyName.Text = txtFamilyOccupation.Text =
                txtFileReference.Text = txtFurtherEduDetails.Text = txtIncomeSource.Text = txtMonthlyIncome.Text = txtPhone.Text = txtRemarks.Text = txtSpecialIssue.Text = string.Empty;
            cmbBenefStatus.SelectedIndex = cmbCity.SelectedIndex = cmbEducation.SelectedIndex = cmbFamilyRelationship.SelectedIndex = cmbHouseStatus.SelectedIndex =
                cmbIstehqaqBasis.SelectedIndex = cmbTitle.SelectedIndex = 0;
            gridFamily.DataSource = null;
            gridFamily.DataBind();
            myImage.Src = null;
            pnlGrid.Visible = false;
            txtCNIC.Enabled = true;
            txtCNIC.Focus();
        }

        private bool alreadyHasPhoto(int benefID, string userID)
        {
            bool hasPhoto = false;
            string userIDQuery = string.Format("SELECT id, photo, benefID FROM tblBeneficiaryPhoto WHERE id = '{0}'", userID);
            string bnefIDQuery = string.Format("SELECT id, photo, benefID FROM tblBeneficiaryPhoto WHERE benefID = {0}", benefID);
            DataTable dtUser = p.GetDataTable(userIDQuery);
            DataTable dtBenef = p.GetDataTable(bnefIDQuery);

            if (dtUser.Rows.Count > 0 && dtBenef.Rows.Count > 0)
            {
                hasPhoto = true;
            }

            return hasPhoto;
        }

        protected void btnGetBeneficiary_Click(object sender, EventArgs e)
        {
            clearLabels();

            try
            {
                int benefID = 0;
                string cnic = p.FixString(txtCNIC.Text);
                string query = string.Format("SELECT id, title, name, cnic, education, educationFurtherDetails,telephone, dateOfBirth, address, city, status, monthlyIncome, " +
                "sourceOfIncome, specialIssue, remarks, fileReference, beneficiaryStatus FROM tblBeneficiaries WHERE cnic = '{0}'", cnic);

                DataTable dt = p.GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    txtCNIC.Enabled = false;
                    DataRow dr = dt.Rows[0];
                    benefID = int.Parse(dr[0].ToString());
                    //Populate Personal Details Controls
                    cmbTitle.SelectedIndex = cmbTitle.Items.IndexOf(cmbTitle.Items.FindByValue(dr[1].ToString()));
                    txtBeneficiaryName.Text = dr[2].ToString();
                    cmbEducation.SelectedIndex = cmbEducation.Items.IndexOf(cmbEducation.Items.FindByValue(dr[4].ToString()));
                    txtFurtherEduDetails.Text = dr[5].ToString();
                    txtPhone.Text = dr[6].ToString();
                    DateTime date;
                    DateTime.TryParse(dr[7].ToString(), out date);
                    txtDate.Text = p.fixDateWithMonthNames(date.Day, date.Month, date.Year);
                    txtAddress.Text = dr[8].ToString();
                    cmbCity.SelectedIndex = cmbCity.Items.IndexOf(cmbCity.Items.FindByValue(dr[9].ToString()));

                    //Populate Istehqaq Controls
                    bool hasHouse = false;
                    Boolean.TryParse(dr[14].ToString(), out hasHouse);
                    string houseStatus = hasHouse ? "Own House" : "Rented House";
                    cmbHouseStatus.SelectedIndex = cmbHouseStatus.Items.IndexOf(cmbHouseStatus.Items.FindByValue(houseStatus));
                    cmbIstehqaqBasis.SelectedIndex = cmbIstehqaqBasis.Items.IndexOf(cmbIstehqaqBasis.Items.FindByValue(dr[10].ToString()));
                    txtMonthlyIncome.Text = dr[11].ToString();
                    txtIncomeSource.Text = dr[12].ToString();
                    txtSpecialIssue.Text = dr[13].ToString();
                    txtRemarks.Text = dr[14].ToString();
                    txtFileReference.Text = dr[15].ToString();
                    cmbBenefStatus.SelectedIndex = cmbBenefStatus.Items.IndexOf(cmbBenefStatus.Items.FindByValue(dr[16].ToString()));
                    viewPhoto(benefID);
                    string familyQuery = string.Format("SELECT familyName, familyRelation, familyOccupation, familyAge, familyEducation FROM tblBeneficiaryFamilyDetails WHERE (benefID = {0})", benefID);
                    DataTable dtFamily = p.GetDataTable(familyQuery);
                    List<Family> lFamily = new List<Family>();

                    if (dtFamily.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtFamily.Rows.Count; i++)
                        {
                            DataRow drFamily = dtFamily.Rows[i];
                            Family f = new Family();
                            f.Name = drFamily[0].ToString();
                            f.Relationship = drFamily[1].ToString();
                            f.Occupation = drFamily[2].ToString();
                            f.Age = int.Parse(drFamily[3].ToString());
                            f.Education = dr[4].ToString();
                            lFamily.Add(f);
                        }
                    }
                    if (lFamily.Count > 0)
                    {
                        gridFamily.DataSource = lFamily;
                        gridFamily.DataBind();
                        pnlGrid.Visible = true;
                    }
                }
                else
                {
                    if (cnic.Length == 13)
                    {
                        string insertQuery = string.Format("INSERT INTO tblBeneficiaries(cnic, beneficiaryStatus) VALUES ('{0}', 'New')", cnic);
                        p.ExecuteQuery(insertQuery);
                        txtCNIC.Enabled = false;
                    }
                    else
                    {
                        throw new Exception("CNIC must be valid.");
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getWarningMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }
    }
}