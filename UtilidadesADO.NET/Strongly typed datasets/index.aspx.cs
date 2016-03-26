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

using Strongly_typed_datasets.Models;
namespace Strongly_typed_datasets
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    SqlConnection con = new SqlConnection(cs);
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents", con);

                    DataSet ds = new DataSet();
                    da.Fill(ds, "Students");
                    Session["DATASET"] = ds;

                    GridView1.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                           select new Student
                                           {
                                               ID = Convert.ToInt32(dataRow["ID"]),
                                               Name = dataRow["Name"].ToString(),
                                               Gender = dataRow["Gender"].ToString(),
                                               TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                           };

                    GridView1.DataBind();


                }
                catch (Exception ex)
                {

                    Response.Write("ERROR: " + ex.Message);
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["DATASET"];
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                GridView1.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                       select new Student
                                       {
                                           ID =Convert.ToInt32(dataRow["ID"]),
                                           Name = dataRow["Name"].ToString(),
                                           Gender = dataRow["Gender"].ToString(),
                                           TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                        };

                GridView1.DataBind();

                TextBox1.Focus();
                return;
            }  
            else
            {
                GridView1.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                       where dataRow["Name"].ToString().ToUpper().StartsWith(TextBox1.Text.ToUpper())
                                       select new Student
                                       {
                                           ID = Convert.ToInt32(dataRow["ID"]),
                                           Name = dataRow["Name"].ToString(),
                                           Gender = dataRow["Gender"].ToString(),
                                           TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                       };

                GridView1.DataBind();
            }
        }
    }
}