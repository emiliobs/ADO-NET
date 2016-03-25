using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace achingdatasetinasp.net
{
    public partial class index : System.Web.UI.Page
    {

        private string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] == null)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter("Select * from tblProduct", con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        Cache["Data"] = ds;

                        gvProducts.DataSource = ds;
                        gvProducts.DataBind();

                    }
                    catch (Exception ex)
                    {

                        lblMessage.Text = "ERROR: " + ex.Message;
                    }

                }

                lblMessage.Text = "Data Load from the dataBase.....";

            }
            else
            {
                gvProducts.DataSource = (DataSet)Cache["Data"];
                gvProducts.DataBind();

                lblMessage.Text = "Data Loaded from the Cache....";
            }

           
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] != null)
            {
                Cache.Remove("Data");
                lblMessage.Text = "The dataSet is remove from the cache....";
            }
            else
            {
                lblMessage.Text = "There is nothing in the cache to be removed.";
            }
        }
    }
}