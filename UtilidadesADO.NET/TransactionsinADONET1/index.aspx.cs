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

namespace TransactionsinADONET1
{
    public partial class index : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDate();
            }
        }

        private void GetDate()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection (cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Accounts", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    if (dr["AccountNumber"].ToString() == "A1")
                    {
                        lblAccountNumber.Text = "A1";
                        lblbalance.Text = dr["Balance"].ToString();
                        lblName.Text = dr["CustomerName"].ToString();
                    }
                    else
                    {
                        lblAccountNumber2.Text = "A2";
                        lblBalance1.Text = dr["Balance"].ToString();
                        lblName2.Text = dr["CustomerName"].ToString();
                    }
                }
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection (cs))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("update Accounts set Balance = Balance - 10 where AccountNumber = 'A1' ", con,transaction);
                   
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("update Accounts set Balance = Balance + 10 where AccountNumber = 'A2'", con, transaction);
                    
                    cmd.ExecuteNonQuery();

                    transaction.Commit();

                    lblMensaje.Text = "Transaction Successful.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMensaje.Text = "Transaction Failed.";
                    lblMensaje.ForeColor = System.Drawing.Color.Black;
                    //lblMensaje.Text = "ERROR: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }

                GetDate();
            }
        }
    }
}