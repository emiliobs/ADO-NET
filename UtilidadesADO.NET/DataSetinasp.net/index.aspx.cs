using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataSetinasp.net
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection (cs))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("spGetDate", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //otra opción
                    ds.Tables[0].TableName = "Product";
                    ds.Tables[1].TableName = "Category";

                    GridView1.DataSource = ds.Tables["Product"];//[0];
                    GridView1.DataBind();

                    GridView2.DataSource = ds.Tables["Category"];//[1]
                    GridView2.DataBind();

                    Response.Write("Datos leidos.....");

                }
                catch (Exception ex)
                {

                    Response.Write("ERROR: "  + ex.Message);
                }
            }
        }
    }
}