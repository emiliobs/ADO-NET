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
namespace Loadxmldataintosqlservertableusingsqlbulkcopy
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/Data.xml"));

                DataTable dtDepartment = ds.Tables["department"];
                DataTable dtEmployee = ds.Tables["Employee"];

                con.Open();

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Department";
                    bc.ColumnMappings.Add("id", "DepartmentId");
                    bc.ColumnMappings.Add("name", "Name");
                    bc.ColumnMappings.Add("location", "Location");
                    bc.WriteToServer(dtDepartment);
                }


                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "tblEmployees";
                    bc.ColumnMappings.Add("Id", "EmployeeId");
                    bc.ColumnMappings.Add("Name", "Name");
                    bc.ColumnMappings.Add("Gender", "Gender");
                    bc.ColumnMappings.Add("salary", "Salary");
                    bc.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    bc.WriteToServer(dtEmployee);
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string csSource = ConfigurationManager.ConnectionStrings["SourceDBCS"].ConnectionString;
            string csDestination = ConfigurationManager.ConnectionStrings["DestinationDBCS"].ConnectionString;

            using (SqlConnection consource = new SqlConnection(csSource))
            {
                SqlCommand cmd = new SqlCommand("Select * from Department", consource);
                consource.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    using (SqlConnection conDestination = new SqlConnection(csDestination))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(conDestination))
                        {
                            bc.DestinationTableName = "Department";
                            conDestination.Open();
                            bc.WriteToServer(dr);
                        }
                    }
                }

                    cmd = new SqlCommand("Select * from tblEmployees", consource);
                    //consource.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        using (SqlConnection conDestination = new SqlConnection(csDestination))
                        {
                            using (SqlBulkCopy bc = new SqlBulkCopy(conDestination))
                            {
                                bc.DestinationTableName = "tblEmployees";
                                conDestination.Open();
                                bc.WriteToServer(dr);
                            }
                        }
                    }

                }
            }
        }
    }


    
