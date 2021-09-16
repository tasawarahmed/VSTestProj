using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.HumanResources
{
    public partial class StaffAllocation : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        Dictionary<int, Single> currentAllocation = new Dictionary<int, Single>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (p.Authenticated(Session["UserName"].ToString(), "hr, deo", Session["userRoles"].ToString()))
            {
                if (!IsPostBack)
                {
                    loadStaffMembers();
                }
            }
            else
            {
                Response.Redirect(@"~\UnauthorizedAccess.aspx");
            }
        }

        private void loadStaffMembers()
        {
            string query = "SELECT staffName + ' - ' + staffFather AS Staff, id FROM tblGenStaff ORDER BY Staff";

            List<DDList> staff = new List<DDList>();
            staff = p.getDDList(query);

            cmbStaffMember.DataSource = staff;
            cmbStaffMember.DataTextField = "DisplayName";
            cmbStaffMember.DataValueField = "Value";
            cmbStaffMember.DataBind();

        }

        protected void cmbStaffMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearLabels();

            if (cmbStaffMember.SelectedIndex > 0)
            {
                try
                {
                    int staffID = int.Parse(cmbStaffMember.SelectedValue);
                    gridAllocationPlaces.DataBind();
                    populateStaffDictionary(staffID);
                    updateGrid();
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
        }

        private void updateGrid()
        {
            for (int i = 0; i < gridAllocationPlaces.Rows.Count; i++)
            {
                Label lbl = (Label)gridAllocationPlaces.Rows[i].FindControl("lblID");

                int locID = int.Parse(lbl.Text);
                Single allocatedTime = 0f;

                if (currentAllocation.TryGetValue(locID, out allocatedTime))
                {
                    TextBox txt = (TextBox)gridAllocationPlaces.Rows[i].FindControl("txtAllocatedTime");
                    txt.Text = allocatedTime.ToString();
                }
            }
        }

        private void populateStaffDictionary(int staffID)
        {
            currentAllocation.Clear();

            string query = string.Format("SELECT locationID, allocatedTime FROM tblGenStaffAllocations where staffid = {0}", staffID);
            currentAllocation = p.getIntSingleDictionary(query);
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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            clearLabels();
            Single totalTime = 0f;

            if (cmbStaffMember.SelectedIndex > 0)
            {
                try
                {
                    int staffID = int.Parse(cmbStaffMember.SelectedValue);
                    string query = string.Empty;

                    for (int i = 0; i < gridAllocationPlaces.Rows.Count; i++)
                    {
                        Label lbl = (Label)gridAllocationPlaces.Rows[i].FindControl("lblID");
                        TextBox txt = (TextBox)gridAllocationPlaces.Rows[i].FindControl("txtAllocatedTime");

                        Single allocatedTime = 0f;

                        if (Single.TryParse(txt.Text.Replace('%', ' ').Trim(), out allocatedTime) && allocatedTime > 0f)
                        {
                            int placeID = int.Parse(lbl.Text);
                            totalTime += allocatedTime;
                            string insertQuery = string.Format(" INSERT INTO tblGenStaffAllocations(staffID, locationID, allocatedTime) VALUES ({0},{1},{2})", staffID, placeID, allocatedTime);
                            query += insertQuery;
                        }                        
                    }

                    if (!query.Equals(string.Empty) && totalTime == 100f)
                    {
                        string deleteQuery = string.Format(" DELETE FROM tblGenStaffAllocations WHERE staffID = {0}", staffID);

                        string finalQuery = deleteQuery + query;

                        p.ExecuteTransactionQuery(finalQuery);
                        showSuccessMessage("Staff information entered successfully.");
                    }
                    else
                    {
                        showWarningMessage("Please ensure that total allocated time is equal to 100 and try again.");
                    }
                }
                catch (Exception ex)
                {
                    showWarningMessage(ex.Message);
                }
            }
            else
            {
                showWarningMessage("Select a staff member first.");
            }
        }
    }
}