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
        }
    }
}