using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace sqlbulkcopynotifyafterexample
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection conSource = new SqlConnection (cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from ProductsSource", conSource);
                conSource.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    using (SqlConnection conDestination = new SqlConnection(cs))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy (conDestination))
                        {
                            bc.BatchSize = 10000;
                            bc.NotifyAfter = 5000;
                            /* bc.SqlRowsCopied += new SqlRowsCopiedEventHandler(bc_SqlRowsCopied);  */
                            bc.SqlRowsCopied += (sender, EventArgs) =>
                            {
                                Console.WriteLine(EventArgs.RowsCopied + " Loaded..... ");
                            };

                            bc.DestinationTableName = "ProductsDestination";
                            conDestination.Open();
                            bc.WriteToServer(dr); 
                        }
                    }
                }
            }

            Console.ReadKey();
        }

    //    private static void bc_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
    //    {
    //        Console.WriteLine(e.RowsCopied + " Loaded..... ");



    //        //Console.ReadKey();
    //    }
    //}
}
