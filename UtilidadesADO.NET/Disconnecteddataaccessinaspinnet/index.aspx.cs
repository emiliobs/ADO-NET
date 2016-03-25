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

namespace Disconnecteddataaccessinaspinnet
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

       public void GetDataFromDB()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents", con);

                    DataSet ds = new DataSet();
                    da.Fill(ds, "Students");

                    ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["ID"] };
                    Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

                    gvStudents.DataSource = ds;
                    gvStudents.DataBind();

                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    lblMessage.Text = "Data Loaded from Database....";

                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                    lblMessage.Text = "ERROR: " + ex.Message;
                }
            }

        }

        
        private void GetDataFromCahe()
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];

                gvStudents.DataSource = ds;
                gvStudents.DataBind();

                lblMessage.Text = "Data Load from Cache....";
            }
        }

        private void btnGetDataFromDB_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnGetDataFromDB_Click1(object sender, EventArgs e)
        {
            GetDataFromDB();
        }

        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;

            GetDataFromCahe();
        }

        protected void gvStudents_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds  = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);

                dr["Name"] = e.NewValues["Name"];
                dr["Gender"] = e.NewValues["Gender"];
                dr["TotalMarks"] = e.NewValues["TotalMarks"];
                
                 Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

                gvStudents.EditIndex = -1;

                GetDataFromCahe();
            }
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);

                dr.Delete();

                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

               

                GetDataFromCahe();
            }
        }

        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            GetDataFromCahe();
        }

        protected void btnUpdateDB_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection (cs))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents", con);

                    DataSet ds = (DataSet)Cache["DATASET"];

                    string sqlUpdate = "update tblStudents set Name = @Name, Gender = @Gender, TotalMarks = @TotalMarks where ID = @ID";
                    SqlCommand upsateCommand = new SqlCommand(sqlUpdate,con);
                    upsateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
                    upsateCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 50, "Gender");
                    upsateCommand.Parameters.Add("@TotalMarks", SqlDbType.Int, 0, "TotalMarks");
                    upsateCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");

                    da.UpdateCommand = upsateCommand;

                    string sqlDelete = "Delete from tblStudents where ID = @ID";
                    SqlCommand deleteCommand = new SqlCommand(sqlDelete,con);
                    deleteCommand.Parameters.Add("@ID",SqlDbType.Int,0,"ID");

                    da.DeleteCommand = deleteCommand;

                    da.Update(ds,"Students");

                    lblMessage.Text = ("Database table Updated");
                }
                catch (Exception ex)
                {


                    lblMessage.Text = "ERROR: " + ex.Message;
                }
            }
        }
    }
}