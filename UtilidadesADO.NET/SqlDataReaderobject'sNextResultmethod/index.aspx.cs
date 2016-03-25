using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SqlDataReaderobject_sNextResultmethod
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection (cs))
            {

               // SqlCommand cmd = new SqlCommand("SELECT * FROM tblProduct", con);
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblProduct; SELECT* from tblCategory", con);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ProductsGridView.DataSource = dr;
                    ProductsGridView.DataBind();
                    while(dr.NextResult())
                    {
                        CategoriesGridView.DataSource = dr;
                        CategoriesGridView.DataBind();
                    }
                    
                }
            }
        }
    }
}