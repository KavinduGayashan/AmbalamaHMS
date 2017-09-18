using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MetroFramework;

namespace Ambalama_HMS_with_MetroUI
{
    class DatabaseConnection
    {
                
            private String conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
            private SqlConnection con = null;

            
        public SqlConnection ConnectDB(Reception window)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window,"Error occured while connecting to the database!\n"+ex.Message, "Error!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            return con;
        }

        public SqlConnection ConnectDB(ReservationForm window)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured while connecting to the database!\n" + ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return con;
        }

        public SqlConnection ConnectDB(Login window)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured while connecting to the database!\n" + ex.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return con;
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public SqlDataReader GetData(Reception window,String sql)
        {
            SqlCommand cmd=null;
            SqlDataReader rdr=null;
            try
            {
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(window,"Error getting data from the database! \n Error:" + ex.Message,"Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return rdr;

        }

        public SqlDataReader GetData(Login window, String sql)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error getting data from the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return rdr;

        }

        public SqlDataReader GetData(ReservationForm window, String sql)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error getting data from the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return rdr;

        }

        //this executes a query given
        public Boolean InsertQuery(Reception window,String sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured while executing command on the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        public Boolean InsertQuery(ReservationForm window, String sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured while executing command on the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }


        //this function returns a DataTable structure for data viewing purposes like datagrid
        public DataTable PrepareTable(Reception window,String sql)
        {
            SqlDataAdapter ada=null;
            SqlCommand cmd=null;

            try
            {
                cmd = new SqlCommand(sql, con);
                ada = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //filling the datatable with the data in the adapter
                ada.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error getting data from the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return null;
            
        }


       

    }
}
