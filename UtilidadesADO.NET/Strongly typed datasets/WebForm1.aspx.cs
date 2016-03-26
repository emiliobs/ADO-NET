using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Strongly_typed_datasets
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    StudentDataSetTableAdapters.StudentsTableAdapter studentsTableAdapte = new StudentDataSetTableAdapters.StudentsTableAdapter();

                    StudentDataSet.StudentsDataTable studentsDataTable = new StudentDataSet.StudentsDataTable();

                    studentsTableAdapte.Fill(studentsDataTable);

                    Session["DATATABLE"] = studentsDataTable;

                    GridView1.DataSource = from student in studentsDataTable
                                           select new
                                           {
                                               student.ID,
                                               student.Name,
                                               student.Gender,
                                               student.TotalMarks
                                           };

                    GridView1.DataBind();
                }

                
            }
            catch (Exception ex)
            {

                Response.Write("ERROR: " + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            StudentDataSet.StudentsDataTable studentsTableAdapte = (StudentDataSet.StudentsDataTable) Session["DATATABLE"];
            try
            {
                if (string.IsNullOrEmpty(TextBox1.Text))
                {
                    GridView1.DataSource = from student in studentsTableAdapte
                                           select new
                    {
                          student.ID,
                          student.Name,
                          student.Gender,
                          student.TotalMarks
                    };

                    GridView1.DataBind();

                    TextBox1.Focus();
                    return;
                }
                else
                {
                    GridView1.DataSource = from student in studentsTableAdapte
                                           where student.Name.ToUpper().StartsWith(TextBox1.Text.ToUpper())
                                           select new
                                           {
                                               student.ID,
                                               student.Name,
                                               student.Gender,
                                               student.TotalMarks
                                           };

                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {

                Response.Write("ERROR: " + ex.Message);
            }   
        }
    }
}