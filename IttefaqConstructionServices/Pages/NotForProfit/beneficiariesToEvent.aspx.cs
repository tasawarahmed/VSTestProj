using IttefaqConstructionServices.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IttefaqConstructionServices.Pages.NotForProfit
{
    public partial class beneficiariesToEvent : System.Web.UI.Page
    {
        Utilities p = new Utilities();
        int benefCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                //loadGrid();
                loadEvents();
            }
        }

        private void loadEvents()
        {
            string ddQuery = "SELECT eventName, id FROM tblGenEvents WHERE (isFinalized = 'false') ORDER BY eventName";

            List<DDList> ddItems = new List<DDList>();
            ddItems = p.getDDList(ddQuery);

            cmbEvent.DataSource = ddItems;
            cmbEvent.DataTextField = "DisplayName";
            cmbEvent.DataValueField = "Value";
            cmbEvent.DataBind();
        }

        private void loadGrid(string criteria)
        {
            string dtQuery = string.Format("SELECT id AS id, name AS Name, address AS Address, city AS City FROM tblBeneficiaries {0}", criteria);
            DataTable dt = p.GetDataTable(dtQuery);

            Gridview1.DataSource = dt;
            Gridview1.DataBind();

            if (Gridview1.Rows.Count > 0)
            {
                refreshGrid();
            }
        }

        private void refreshGrid()
        {
            Gridview1.UseAccessibleHeader = true;
            Gridview1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnGetBeneficiaries_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;
            try
            {
                if (cmbEvent.SelectedIndex > 0)
                {
                    string area = txtAreas.Text;
                    string[] areas = area.Split(',');
                    string whereClause = "WHERE ";
                    int areaCount = 0;

                    foreach (var areaItem in areas)
                    {
                        string temp = areaItem.Trim();
                        if (temp != "")
                        {
                            areaCount++;
                            whereClause += "city = '" + areaItem.Trim() + "' OR ";
                        }
                    }

                    if (areaCount == 0)
                    {
                        whereClause = string.Empty;
                    }
                    else
                    {
                        whereClause = whereClause.Remove(whereClause.Length - 3);
                    }

                    loadGrid(whereClause);
                    showIndication();
                    txtCount.Text = benefCount.ToString();
                }
                else
                {
                    throw new Exception("Please select an event.");
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = p.getWarningMessage(ex.Message);
                lblWarning.Visible = true;
            }
        }

        private void showIndication()
        {
            benefCount = 0;
            int eventID = int.Parse(cmbEvent.SelectedValue);
            string query = string.Format("SELECT beneficiaryID FROM tblGenBeneficiariesInEvents WHERE eventID = {0}", eventID);
            List<int> beneficiries = p.getIntList(query);

            if (beneficiries.Count > 0)
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    CheckBox check = (CheckBox)Gridview1.Rows[i].FindControl("cb_count");
                    Label benID = (Label)Gridview1.Rows[i].FindControl("lblID");
                    int benefID = 0;

                    if (check != null && benID != null)
                    {
                        int.TryParse(benID.Text, out benefID);

                        if (beneficiries.Contains(benefID))
                        {
                            check.Checked = true;
                            benefCount++;
                        }
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblSuccess.Visible = lblWarning.Visible = false;

            string query = string.Empty;
            try
            {
                if (cmbEvent.SelectedIndex > 0)
                {
                    int eventID = int.Parse(cmbEvent.SelectedValue);

                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)Gridview1.Rows[i].FindControl("cb_count");
                        if (chk.Checked)
                        {
                            Label lbl = (Label)Gridview1.Rows[i].FindControl("lblID");
                            int benefID = int.Parse(lbl.Text);
                            if (query == string.Empty)
                            {
                                string deleteQuery = string.Format("  DELETE FROM tblGenBeneficiariesInEvents WHERE eventID = {0}", eventID);
                                query = query + deleteQuery;
                            }

                            string insertQuery = string.Format("  INSERT INTO tblGenBeneficiariesInEvents(beneficiaryID, eventID) VALUES ({0},{1})", benefID, eventID);
                            query = query + insertQuery;
                        }
                    }

                    if (query == string.Empty)
                    {
                        throw new Exception("You must select at least one beneficiary for an event.");
                    }

                    else
                    {
                        p.ExecuteQuery(query);
                        lblSuccess.Text = p.getSuccessMessage("Beneficiaries added successfully");
                        lblSuccess.Visible = true;
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