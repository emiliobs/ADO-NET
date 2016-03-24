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

namespace SqlDataAdapterinADO.NET
{
    public partial class index : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    //SqlDataAdapter da = new SqlDataAdapter("spGetProductByID", con);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand("spGetProductByID", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@ProductId", TextBox1.Text);


                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                }
            }
        }
    }
}