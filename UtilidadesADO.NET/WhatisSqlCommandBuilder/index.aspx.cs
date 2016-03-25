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
using System.Drawing;

namespace WhatisSqlCommandBuilder
{
    public partial class index : System.Web.UI.Page
    {
        private string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(cs);
                string sqlQuery = "Select * from tblStudents where ID = " + TextBoxStudnetID.Text;
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "tblStudents");

                ViewState["SQL_QUERY"] = sqlQuery;
                ViewState["DATASET"] = ds;

                if (ds.Tables["tblStudents"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["tblStudents"].Rows[0];
                    TextBoxName.Text = dr["Name"].ToString();
                    DropDownListGender.SelectedValue = dr["Gender"].ToString();
                    TextBoxTotalMark.Text = dr["TotalMarks"].ToString();
                }
                else
                {
                    LabelStatus.ForeColor = Color.Red;
                    LabelStatus.Text = "No Students record with ID = " + TextBoxStudnetID.Text;
                }

            }
            catch (Exception ex)
            {
                
                LabelStatus.Text = "ERROR: " + ex.Message;
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection (cs))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter((String)ViewState["SQL_QUERY"], con);
                    //da.SelectCommand = new SqlCommand();
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);

                    DataSet ds =(DataSet) ViewState["DATASET"];
                    DataRow dr = ds.Tables["tblStudents"].Rows[0];
                    dr["Name"] = TextBoxName.Text;
                    dr["Gender"] = DropDownListGender.SelectedValue;
                    dr["TotalMarks"] = TextBoxTotalMark.Text;
                    //dr["ID"] = TextBoxStudnetID.Text;

                    int rowsUpdate = da.Update(ds, "tblStudents");
                    if (rowsUpdate == 0)
                    {
                        LabelStatus.ForeColor = Color.Red;
                        LabelStatus.Text = "Not rows updated..";

                    }
                    else
                    {
                        LabelStatus.ForeColor = Color.Green;
                        LabelStatus.Text = rowsUpdate.ToString() + "row(0) updated";
                    }

                    lblInsert.Text = builder.GetInsertCommand().CommandText;
                    lblUpdate.Text = builder.GetUpdateCommand().CommandText;
                    lblDelete.Text = builder.GetDeleteCommand().CommandText;
                }
                catch (Exception ex)
                {

                    LabelStatus.Text = "ERROR: " + ex.Message;
                }
            }
        }
    }
}