using MetroFramework;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Ambalama_HMS_with_MetroUI
{
    public partial class Reception : MetroFramework.Forms.MetroForm
    {

        private Guest guest = new Guest();

        public Reception()
        {
            InitializeComponent();
            searchCombo.SelectedIndex = 0;
        }

        public void RefreshTables()
        {
            //******REFRESHING GUESTS INFO TABLE IN THE UPDATE TAB***********//
            DatabaseConnection con = new DatabaseConnection();
            con.ConnectDB(this);
            string sql = "select * from guests";
            guestsDatagrid.DataSource = con.PrepareTable(sql);
            con.CloseConnection();
            //*******END OF REFRESHING**********//
        }


        private void passportNo_TextChanged(object sender, EventArgs e)
        {
            addNIC.Text = "";
            registeredLbl.Text = "";
            if (String.IsNullOrWhiteSpace(passportNo.Text) || String.IsNullOrEmpty(passportNo.Text))
            {
                passportValLbl.ForeColor = Color.Red;
                passportValLbl.Text = "Provide a passport number";
            }
            else
            {
                passportValLbl.ForeColor = Color.Green;
                passportValLbl.Text = "Valid";
            }
        }

        private void addNIC_TextChanged(object sender, EventArgs e)
        {
            passportNo.Text = "";
            passportValLbl.Text = "";
            registeredLbl.ForeColor = Color.White;
            Validations val = new Validations();
            if (val.ValidateNIC(addNIC.Text))
            {
                registeredLbl.ForeColor = Color.Green;
                registeredLbl.Text = "Valid";
                try
                {
                    DatabaseConnection con = new DatabaseConnection();
                    con.ConnectDB(this);
                    string sql = "select * from guests where guestid='" + addNIC.Text + "'";
                    SqlDataReader rdr = con.GetData(this,sql);

                    if (rdr.Read())
                    {
                        registeredLbl.ForeColor = Color.SpringGreen;
                        registeredLbl.Text = "Guest is registered";
                    }
                }
                catch (Exception ex)
                {
                    MetroMessageBox.Show(this,"Database connection error occured \nError:" + ex.Message,"Database connection", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

            }
            else
            {
                registeredLbl.ForeColor = Color.Red;
                registeredLbl.Text = "Invalid";
            }
        }

        private void addName_TextChanged(object sender, EventArgs e)
        {
            Validations val = new Validations();
            if (val.ValidateName(addName.Text))
            {
                nameValLbl.ForeColor = Color.Green;
                nameValLbl.Text = "Valid";
            }
            else
            {
                nameValLbl.ForeColor = Color.Red;
                nameValLbl.Text = "Invalid";
            }
        }

        private void addAddr_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(addAddr.Text) || String.IsNullOrWhiteSpace(addAddr.Text))
            {
                addrValLbl.ForeColor = Color.Red;
                addrValLbl.Text = "Provide an address";
            }
            else
            {
                addrValLbl.ForeColor = Color.Green;
                addrValLbl.Text = "Valid";
            }
        }

        private void addCity_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(addCity.Text) || String.IsNullOrEmpty(addCity.Text))
            {
                cityValLbl.ForeColor = Color.Red;
                cityValLbl.Text = "Provide a city";
            }
            else
            {
                cityValLbl.ForeColor = Color.Green;
                cityValLbl.Text = "Valid";
            }
        }

        private void addPhone_TextChanged(object sender, EventArgs e)
        {
            Validations val = new Validations();
            if (val.ValidatePhone(addPhone.Text))
            {
                phoneValLbl.ForeColor = Color.Green;
                phoneValLbl.Text = "Valid";
            }
            else
            {
                phoneValLbl.ForeColor = Color.Red;
                phoneValLbl.Text = "Invalid";
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearAddGuestFields();
        }

        public void clearAddGuestFields()
        {
            addNIC.Clear();
            addName.Clear();
            addAddr.Clear();
            addCity.Clear();
            addPhone.Clear();
            passportNo.Clear();

            nameValLbl.Text = "";
            registeredLbl.Text = "";
            addrValLbl.Text = "";
            cityValLbl.Text = "";
            phoneValLbl.Text = "";
            passportValLbl.Text = "";
        }

        public void clearUpdateGuestFields()
        {
            guestIDTxt.Clear();
            updateNameTxt.Clear();
            updateAddrTxt.Clear();
            updateCityTxt.Clear();
            updatePhoneTxt.Clear();

            updateNameValLbl.Text = "";
            updateAddrValLbl.Text = "";
            updateCityValLbl.Text = "";
            updatePhoneValLbl.Text = "";
            
        }

        private void addGuestBtn_Click(object sender, EventArgs e)
        {

            

            //Checks all of the fields are validated
            if ((registeredLbl.Text == "Valid" || passportValLbl.Text == "Valid") && nameValLbl.Text == "Valid" && addrValLbl.Text == "Valid" && cityValLbl.Text == "Valid" && phoneValLbl.Text == "Valid")
            {
                if (registeredLbl.Text == "Valid")
                    guest.GID = addNIC.Text;
                else if (passportValLbl.Text == "Valid")
                    guest.GID = passportNo.Text;

                guest.Name = addName.Text;
                guest.Address = addAddr.Text;
                guest.City = addCity.Text;
                guest.Phone = addPhone.Text;

                if (guest.AddGuest(this))
                {
                    MetroMessageBox.Show(this, "Guest is registered successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    RefreshTables();
                    clearAddGuestFields();
                }
                else
                    MetroMessageBox.Show(this, "Error occured. \n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                guest.Reset();

            }
            else
                MetroMessageBox.Show(this,"All the fields are required to be filled correctly.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            
        }

        private void Reception_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotelmgmtsysDataSet.Guests' table. You can move, or remove it, as needed.
            this.guestsTableAdapter.Fill(this.hotelmgmtsysDataSet.Guests);

        }

        //Search field
        private void searchTxt_TextChanged(object sender, EventArgs e)
        {
            int searchType = searchCombo.SelectedIndex;
            string sql=null;
            DatabaseConnection con = new DatabaseConnection();
            con.ConnectDB(this);

            switch (searchType) {
                case 0:
                    sql = "select * from guests where guestid like '" + searchTxt.Text + "%'";

                    

                    guestsDatagrid.DataSource = con.PrepareTable(sql);
                    con.CloseConnection();
                    break;
                case 1:
                    sql = "select * from guests where name like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(sql);
                    con.CloseConnection();
                    break;
                case 2:
                    sql = "select * from guests where phone like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(sql);
                    con.CloseConnection();
                    break;
                case 3:
                    sql = "select * from guests where city like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(sql);
                    con.CloseConnection();
                    break;
            }

        }

        //Start of Validating UPDATE fields ****************

        private void updateNameTxt_TextChanged(object sender, EventArgs e)
        {
            Validations val = new Validations();
            if (val.ValidateName(updateNameTxt.Text))
            {
                updateNameValLbl.ForeColor = Color.White;
                updateNameValLbl.Text = "Valid";
            }
            else
            {
                updateNameValLbl.ForeColor = Color.Red;
                updateNameValLbl.Text = "Invalid";
            }
        }

        private void updateAddrTxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(updateAddrTxt.Text) || String.IsNullOrWhiteSpace(updateAddrTxt.Text))
            {
                updateAddrValLbl.ForeColor = Color.Red;
                updateAddrValLbl.Text = "Provide an address";
            }
            else
            {
                updateAddrValLbl.ForeColor = Color.White;
                updateAddrValLbl.Text = "Valid";
            }
        }

        private void updateCityTxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(updateCityTxt.Text) || String.IsNullOrEmpty(updateCityTxt.Text))
            {
                updateCityValLbl.ForeColor = Color.Red;
                updateCityValLbl.Text = "Provide a city";
            }
            else
            {
                updateCityValLbl.ForeColor = Color.White;
                updateCityValLbl.Text = "Valid";
            }
        }

        private void updatePhoneTxt_TextChanged(object sender, EventArgs e)
        {
            Validations val = new Validations();
            if (val.ValidatePhone(updatePhoneTxt.Text))
            {
                updatePhoneValLbl.ForeColor = Color.White;
                updatePhoneValLbl.Text = "Valid";
            }
            else
            {
                updatePhoneValLbl.ForeColor = Color.Red;
                updatePhoneValLbl.Text = "Invalid";
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(guestIDTxt.Text))
            {
                DialogResult result = MetroMessageBox.Show(this,"Do you want to delete the details of " + updateNameTxt.Text + "?", "Preparing to delete!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    guest.GID = guestIDTxt.Text;

                    if (guest.DeleteGuest(this))
                    {
                        RefreshTables();
                        clearUpdateGuestFields();
                        MetroMessageBox.Show(this,"Entry deleted successfully.", "Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MetroMessageBox.Show(this, "Error occured. \n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MetroMessageBox.Show(this,"Nothing to delete.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //End of Validating UPDATE fields***********

        private void guestsDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guestIDTxt.Text = guestsDatagrid.SelectedRows[0].Cells["GID"].Value.ToString();
            updateNameTxt.Text = guestsDatagrid.SelectedRows[0].Cells["GName"].Value.ToString();
            updatePhoneTxt.Text = guestsDatagrid.SelectedRows[0].Cells["GPhone"].Value.ToString();
            updateAddrTxt.Text = guestsDatagrid.SelectedRows[0].Cells["GAddress"].Value.ToString();
            updateCityTxt.Text = guestsDatagrid.SelectedRows[0].Cells["GCity"].Value.ToString();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {


            if (!String.IsNullOrEmpty(guestIDTxt.Text))
            {
                if (updateNameValLbl.Text == "Valid" && updateAddrValLbl.Text == "Valid" && updateCityValLbl.Text == "Valid" && updatePhoneValLbl.Text == "Valid")
                {
                    guest.GID = guestIDTxt.Text;
                    guest.Name = updateNameTxt.Text;
                    guest.Address = updateAddrTxt.Text;
                    guest.City = updateCityTxt.Text;
                    guest.Phone = updatePhoneTxt.Text;


                    if (guest.UpdateGuest(this))
                    {
                        MetroMessageBox.Show(this, "Entry is updated successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        //REFRESHING THE TABLE
                        RefreshTables();
                    }
                    else
                        MetroMessageBox.Show(this, "Error occured. \n", "Error while updating!", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    guest.Reset();
                }
                else
                    MetroMessageBox.Show(this, "All the fields are required to be filled correctly.\nPlease check again.", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MetroMessageBox.Show(this, "Select an entry in the table before updating.", "Want to update?");

        }
    }
}
