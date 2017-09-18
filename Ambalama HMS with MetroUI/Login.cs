using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ambalama_HMS_with_MetroUI
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();

            con.ConnectDB(this);
            String uname=null, password=null,role=null;

            uname = unameTxt.Text;
            string sql = "select password,role from users where username='" + unameTxt.Text+"'";
            SqlDataReader rdr= con.GetData(this, sql);

            if (rdr.Read())
            {
                password = rdr.GetString(0);
                role = rdr.GetString(1);

                if (password.Equals(passwordTxt.Text))
                {
                    //Managing the windows for users
                    switch (role)
                    {
                        case "Receptionist":
                            new Reception(this,"Receptionist").Show();
                            this.Hide();
                            unameTxt.Clear();
                            passwordTxt.Clear();
                            break;
                            
                    }

                }
                else
                {
                    MessageBox.Show("Incorrect password. Please check and try again.","Incorrect password",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }


            }
            else
            {
                MessageBox.Show("Username is invalid! Please check username again.","Invalid username!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);  
            }



        }


        private void passwordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn_Click(sender,e);
            }
        }
    }
}
