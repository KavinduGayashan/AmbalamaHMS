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
        private Login login;
        string role;

        public Reception(Login window,String role)
        {
            InitializeComponent();
            searchCombo.SelectedIndex = 0;
            roomCapacityCombo.SelectedIndex = 0;
            searchCheckinCombo.SelectedIndex=0;
            searchCheckoutCombo.SelectedIndex = 0;
            checkoutBtn.Visible = false;

            login = window;
            this.role = role;

        }

        public void RefreshTables()
        {
            //******REFRESHING GUESTS INFO TABLE IN THE UPDATE TAB***********//
            DatabaseConnection con = new DatabaseConnection();
            con.ConnectDB(this);
            string sql = "select * from guests";
            guestsDatagrid.DataSource = con.PrepareTable(this,sql);
            con.CloseConnection();

            con.ConnectDB(this);
            sql = "select * from reservations";
            reservationsGrid.DataSource = con.PrepareTable(this, sql);

            sql = "select * from reservations where resstatus='reserved'";
            checkinGrid.DataSource= con.PrepareTable(this, sql);
            

            sql = "select * from reservations where resstatus='checkedin'";
            checkoutGrid.DataSource = con.PrepareTable(this, sql);
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

                    con.CloseConnection();
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
            // TODO: This line of code loads data into the 'hMSUpdatedDatasetReservations.Reservations' table. You can move, or remove it, as needed.
            this.reservationsTableAdapter1.Fill(this.hMSUpdatedDatasetReservations.Reservations);
            // TODO: This line of code loads data into the 'hotelmgmtsysDataSetwithType.Guests' table. You can move, or remove it, as needed.
            this.guestsTableAdapter.Fill(this.hotelmgmtsysDataSetwithType.Guests);
            // TODO: This line of code loads data into the 'hotelmgmtsysDataSetwithType.Reservations' table. You can move, or remove it, as needed.
            //this.reservationsTableAdapter.Fill(this.hotelmgmtsysDataSetwithType.Reservations);
            // TODO: This line of code loads data into the 'hotelmgmtsysDataSetwithType.Rooms' table. You can move, or remove it, as needed.
            this.roomsTableAdapter.Fill(this.hotelmgmtsysDataSetwithType.Rooms);
            RefreshTables();


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

                    

                    guestsDatagrid.DataSource = con.PrepareTable(this,sql);
                    con.CloseConnection();
                    break;
                case 1:
                    sql = "select * from guests where name like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(this,sql);
                    con.CloseConnection();
                    break;
                case 2:
                    sql = "select * from guests where phone like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(this,sql);
                    con.CloseConnection();
                    break;
                case 3:
                    sql = "select * from guests where city like '" + searchTxt.Text + "%'";

                    guestsDatagrid.DataSource = con.PrepareTable(this,sql);
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

        private void findRoomsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Get selected values for searching

                String cap, ac, dtv, hw, sql;

                cap = roomCapacityCombo.Text;
                ac = acCheckbox.Checked ? "1" : "0";
                dtv = tvCheckbox.Checked ? "1" : "0";
                hw = hotwaterCheckbox.Checked ? "1" : "0";

                sql = "select * from Rooms where capacity>=" + cap + " and ac=" + ac + " and dialogtv=" + dtv + " and hotwater=" + hw;

                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);

                findRoomsGrid.DataSource = con.PrepareTable(this, sql);
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                //MetroMessageBox.Show(this, "Error getting data from the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void showAllBtn_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "select * from Rooms";

                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);

                findRoomsGrid.DataSource = con.PrepareTable(this, sql);

                con.CloseConnection();
            }
            catch (Exception ex)
            {
                //MetroMessageBox.Show(this, "Error getting data from the database! \n Error:" + ex.Message, "Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void findRoomsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            roomIDTxt.Text= findRoomsGrid.SelectedRows[0].Cells["RID"].Value.ToString();
        }

        private void checkAvlBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(roomIDTxt.Text))
            {
                DateTime checkin, checkout, testdate;
                DateTime.TryParseExact(checkinDTPicker.Text, "dd-MMM-yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkin);
                DateTime.TryParseExact(checkoutDTPicker.Text, "dd-MMM-yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkout);

                //Firstly checks the provided dates are valid and a future booking

                if ((checkin < checkout) && ( checkin >= DateTime.Today) )
                {
                    string checkinstr = checkin.ToString("yyyy-MM-dd HH:mm:ss");
                    string checkoutstr = checkout.ToString("yyyy-MM-dd HH:mm:ss");
                    string roomid = roomIDTxt.Text;
                    //to check that if any other reservation is in between C.I. and C.O.
                    string sql = String.Format("select * from reservations where ((checkin between '{0}' and '{1}') or (checkout between '{0}' and '{1}') ) and roomid='{2}'", checkinstr, checkoutstr, roomid);

                    try
                    {
                        DatabaseConnection con = new DatabaseConnection();
                        con.ConnectDB(this);
                        SqlDataReader rdr = con.GetData(this, sql);

                        //Checks if there is a reservation in between the checkin and checkout dates
                        if (rdr.Read())
                        {

                            tickLbl.ForeColor = Color.Red;
                            tickLbl.Text = "X Not Available";
                            rdr.Close();
                        }
                        else
                        {
                            rdr.Close();
                            //setting the date to a previous date which is not much near to the checkin
                            DateTime weeksbfr = checkin.AddDays(-14);
                            string testdatestr;
                            string weeksbfrstr = weeksbfr.ToString("yyyy-MM-dd HH:mm:ss");

                            sql = String.Format("select top 1 checkout from Reservations where roomid='{0}' and checkin between '{1}' and '{2}' order by checkout desc ", roomid, weeksbfrstr, checkinstr);
                            //executing command and receive values to a reader
                            rdr = con.GetData(this, sql);

                            if (rdr.Read())
                            {

                                testdatestr = rdr.GetDateTime(0).ToString();

                                //converting the received date into datetime format
                                DateTime.TryParseExact(testdatestr, "M/dd/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out testdate);

                                //compare the received checkout date with the checkin date of current reservation
                                int result = DateTime.Compare(testdate, checkin);

                                if (result == 0 || result > 0) //if the test date occurs after the checkin date the reservation cannot be done
                                {
                                    tickLbl.ForeColor = Color.Red;
                                    tickLbl.Text = "X Not Available";
                                }
                                else //else, reservation can be placed
                                {
                                    tickLbl.ForeColor = Color.Green;
                                    tickLbl.Text = "✓ Available";
                                }

                            }
                            else
                            {
                                tickLbl.ForeColor = Color.Green;
                                tickLbl.Text = "✓ Available";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, "An error occured while checking availability \nError : " + ex.Message, "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    tickLbl.ForeColor = Color.Red;
                    tickLbl.Text = "X Can't Reserve";
                }


            }
            else
                MetroMessageBox.Show(this, "Please select a room in the table", "Want to check availability?");
        }

        private void guestIDTxt_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(guestID.Text))
            {
                try
                {
                    DatabaseConnection con = new DatabaseConnection();
                    con.ConnectDB(this);
                    string sql = "select * from guests where guestid='" + guestID.Text + "'";
                    SqlDataReader rdr = con.GetData(this, sql);

                    if (rdr.Read())
                    {
                        gidRegisteredLbl.ForeColor = Color.Green;
                        gidRegisteredLbl.Text = "Guest is registered";
                    }
                    else
                    {
                        gidRegisteredLbl.ForeColor = Color.Red;
                        gidRegisteredLbl.Text = "Guest is not registered";
                    }

                    con.CloseConnection();
                }
                catch (Exception ex)
                {
                    MetroMessageBox.Show(this, "Database connection error occured \nError:" + ex.Message, "Database connection", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
                gidRegisteredLbl.Text = "Provide an Identification number";

        }

        private void reserveBtn_Click(object sender, EventArgs e)
        {

            if(gidRegisteredLbl.Text.Equals("Guest is not registered"))
            {
                DialogResult result= MetroMessageBox.Show(this, "The Guest is not registered yet. Do you want to register?\n click Yes to continue to Registration page.","First things first",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    mainRTabControl.SelectedTab = addGuestTab;
                }

            }
            else if (String.IsNullOrEmpty(guestID.Text))
            {
                MetroMessageBox.Show(this, "Please enter the guest's ID to continue.");
            }
            else if(tickLbl.Text.Equals("X Not Available") || tickLbl.Text.Equals("X Can't Reserve"))
            {
                MetroMessageBox.Show(this,"Selected dates are not available for reservation. Please check again.","Dates are not available...");
            }
            else if (gidRegisteredLbl.Text.Equals("Guest is registered") && tickLbl.Text.Equals("✓ Available"))
            {
                this.WindowState = FormWindowState.Maximized;
                string rid, gid; 
                DateTime checkin, checkout;

                DateTime.TryParseExact(checkinDTPicker.Text, "dd-MMM-yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkin);
                DateTime.TryParseExact(checkoutDTPicker.Text, "dd-MMM-yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkout);

                gid = guestID.Text;
                rid = roomIDTxt.Text;
                string checkinstr = checkin.ToString("yyyy-MM-dd HH:mm:ss");
                string checkoutstr = checkout.ToString("yyyy-MM-dd HH:mm:ss");


                ReservationForm res = new ReservationForm(this,gid,rid,checkinstr,checkoutstr);
                res.ShowDialog(this);
                RefreshTables();
               
            }


        }

        private void checkinDTPicker_ValueChanged(object sender, EventArgs e)
        {
            tickLbl.Text = "";
        }

        private void checkoutDTPicker_ValueChanged(object sender, EventArgs e)
        {
            tickLbl.Text = "";
        }

        private void amtPayingTxt_TextChanged(object sender, EventArgs e)
        {
            double amtTBP=0, updatedAmtTBP=0,amtPaying;

            Double.TryParse(amtTBPTxt.Text, out amtTBP);
            Double.TryParse(maskedTextBox1.Text, out amtPaying);

            updatedAmtTBP = amtTBP - amtPaying;

            updatedATBPTxt.Text = updatedAmtTBP.ToString();

        }

        private void searchCheckin_TextChanged(object sender, EventArgs e)
        {
            int searchType = searchCheckinCombo.SelectedIndex;
            string sql = null;
            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);
                if (!String.IsNullOrEmpty(searchCheckin.Text))
                {
                    switch (searchType)
                    {
                        case 0:
                            sql = "select * from reservations where guestid like '" + searchCheckin.Text + "%' and resstatus='Reserved'";
                            
                            checkinGrid.DataSource = con.PrepareTable(this, sql);
                            con.CloseConnection();
                            break;
                        case 1:
                            sql = "select * from reservations where roomid like '" + searchCheckin.Text + "%' and resstatus='Reserved'";

                            checkinGrid.DataSource = con.PrepareTable(this, sql);
                            con.CloseConnection();
                            break;

                    }
                }
                else
                {
                    sql = "select * from reservations where resstatus='Reserved'";

                    checkinGrid.DataSource = con.PrepareTable(this, sql);
                    con.CloseConnection();
                }


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,"Database error occured when searching \nError:"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }


        }

        private void checkinGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            checkinGIDTxt.Text = checkinGrid.SelectedRows[0].Cells["RguestID"].Value.ToString();
            checkinRIDTxt.Text = checkinGrid.SelectedRows[0].Cells["RroomID"].Value.ToString();
            amtTBPTxt.Text= checkinGrid.SelectedRows[0].Cells["amtTBP"].Value.ToString();
            reservationID.Text= checkinGrid.SelectedRows[0].Cells["resNo"].Value.ToString();

        }

        private void checkinBtn_Click(object sender, EventArgs e)
        {
            double upAmtTBP = 0,checkinpayment=0;
            if (!String.IsNullOrEmpty(reservationID.Text))
            {

                int resNo;
                Int32.TryParse(reservationID.Text, out resNo);

                Double.TryParse(maskedTextBox1.Text, out checkinpayment);

                if (!String.IsNullOrEmpty(updatedATBPTxt.Text))
                {
                    Double.TryParse(updatedATBPTxt.Text,out upAmtTBP);

                    try
                    {
                        DatabaseConnection con = new DatabaseConnection();
                        con.ConnectDB(this);
                        string sql = "update reservations set totalamounttbp="+upAmtTBP+" , checkinpayment="+checkinpayment +" , resstatus='CheckedIn' where resno="+resNo;

                        if(con.InsertQuery(this, sql))
                        {
                            MetroMessageBox.Show(this, "Successfully checked-in.", "Checked-In!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            RefreshTables();
                        }
                        else
                            MetroMessageBox.Show(this, "Couldn't check-in.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    catch(Exception ex)
                    {
                        MetroMessageBox.Show(this,"Error occured while trying to Check-In \nError:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else //when user has not entered a value to the advance payment textbox
                {
                    try
                    {
                        DatabaseConnection con = new DatabaseConnection();
                        con.ConnectDB(this);
                        string sql = "update reservations set totalamounttbp=" + amtTBPTxt.Text + " , checkinpayment=0 , resstatus='CheckedIn' where resno=" + resNo;

                        if (con.InsertQuery(this, sql))
                        {
                            MetroMessageBox.Show(this, "Successfully checked-in.", "Checked-In!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            RefreshTables();
                        }
                        else
                            MetroMessageBox.Show(this, "Couldn't check-in.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, "Error occured while trying to Check-In \nError:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            else
                MetroMessageBox.Show(this,"Please select a reservation from the table to Check-In","First things first");


        }

        private void searchCheckout_TextChanged(object sender, EventArgs e)
        {
            int searchType = searchCheckoutCombo.SelectedIndex;
            string sql = null;
            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);
                if (!String.IsNullOrEmpty(searchCheckout.Text))
                {
                    switch (searchType)
                    {
                        case 0:
                            sql = "select * from reservations where guestid like '" + searchCheckout.Text + "%' and resstatus='checkedin'";



                            checkoutGrid.DataSource = con.PrepareTable(this, sql);
                            con.CloseConnection();
                            break;
                        case 1:
                            sql = "select * from reservations where roomid like '" + searchCheckout.Text + "%' and resstatus='checkedin'";

                            checkoutGrid.DataSource = con.PrepareTable(this, sql);
                            con.CloseConnection();
                            break;

                    }
                }
                else //for empty textbox
                {
                    sql = "select * from reservations where resstatus='checkedin'";

                    checkoutGrid.DataSource = con.PrepareTable(this, sql);
                    con.CloseConnection();
                    
                }


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "Database error occured when searching \nError:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkoutGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            checkoutGIDTxt.Text = checkoutGrid.SelectedRows[0].Cells["COguestID"].Value.ToString();
            checkoutRoomIDTxt.Text = checkoutGrid.SelectedRows[0].Cells["COroomID"].Value.ToString();
            checkoutTotAmtTBPTxt.Text = checkoutGrid.SelectedRows[0].Cells["COtotamtTBP"].Value.ToString();
            checkoutResIDTxt.Text = checkoutGrid.SelectedRows[0].Cells["COresNo"].Value.ToString();
            checkoutMealsTxt.Text= checkoutGrid.SelectedRows[0].Cells["COmeal"].Value.ToString();
            
        }

        private void checkoutResIDTxt_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(checkoutResIDTxt.Text))
            {

                double latefee = 0,hours=0;
                DateTime checkout;

                try
                {
                    DatabaseConnection con = new DatabaseConnection();
                    con.ConnectDB(this);

                    //to get the late fee rate
                    string sql = String.Format("select p.PerExtraHour from Packages p,Rooms r where r.RoomID='{0}' and r.RoomType=p.RoomType",checkoutRoomIDTxt.Text);

                    SqlDataReader rdr= con.GetData(this, sql);

                    if (rdr.Read())
                    {
                        Double.TryParse( rdr.GetSqlSingle(0).ToString(),out latefee);
                      
                    }

                    rdr.Close();

                    //to get the checkout date for late fee calculation
                    sql = "select checkout from reservations where resno=" + checkoutResIDTxt.Text;

                    rdr = con.GetData(this, sql);
                    if (rdr.Read())
                    {
                        checkout=rdr.GetDateTime(0);
                        //DateTime.TryParseExact(checkoutDTPicker.Text, "dd-MMM-yyyy  hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkout);

                        //checking if the checkout time is due
                        if (checkout < DateTime.Now)
                        {
                            hours = (DateTime.Now - checkout).TotalHours;

                            latefee = latefee * hours;
                            latefee = Math.Round(latefee / 10, MidpointRounding.AwayFromZero) * 10.0;
                            checkoutLatefeeTxt.Text = latefee.ToString();
                        }
                        else
                            checkoutLatefeeTxt.Text = "0";
                        
                    }

                }
                catch (Exception ex)
                {

                }

            }


        }

        private void calcbillBtn_Click(object sender, EventArgs e)
        {

            Double exmeals = 0,exbeds=0, latefees = 0,roomprice=0, totalamttbp=0, advance=0,discount=0,checkinpayment=0,totprice=0;
            double damages = 0;

            if (!String.IsNullOrEmpty(checkoutResIDTxt.Text))
            {
                
                try
                {
                    
                    //DatabaseConnection con = new DatabaseConnection();
                    //con.ConnectDB(this);

                    //string sql = "SELECT advancePayment,latefee,TotalAmountTBP,RoomPrice,ExtraBedCharge,ExtraMealCharge,DiscountAmount,CheckinPayment,TotalPrice FROM Reservations where resno=" + checkoutResIDTxt.Text;

                    //SqlDataReader rdr = con.GetData(this, sql);

                    //if (rdr.Read())
                    //{
                    //    MessageBox.Show("inside read");
                    //    advance = (float)rdr["advancePayment"];
                    //    MessageBox.Show("end of assigning");
                    //    latefees = (float)rdr.GetFloat(1);
                    //    totalamttbp = (float)rdr.GetFloat(2);
                    //    roomprice = (float)rdr.GetFloat(3);
                    //    exbeds = (float)rdr.GetFloat(4);
                    //    exmeals = (float)rdr.GetFloat(5);
                    //    discount = (float)rdr.GetFloat(6);
                    //    checkinpayment = (float)rdr.GetFloat(7);
                    //    totprice = (float)rdr.GetFloat(8);

                    //}

                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COadvancePayment"].Value.ToString(),out advance);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COtotamtTBP"].Value.ToString(), out totalamttbp);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COroomPrice"].Value.ToString(), out roomprice);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COextraBedCharge"].Value.ToString(), out exbeds);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COmeal"].Value.ToString(), out exmeals);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COdiscountAmount"].Value.ToString(), out discount);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COcheckinPayment"].Value.ToString(), out checkinpayment);
                    Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COtotPrice"].Value.ToString(), out totprice);
                    Double.TryParse(checkoutLatefeeTxt.Text, out latefees);
                    Double.TryParse(damageChargesTxt.Text, out damages);

                    

                    double amttbp = (double)(roomprice + exbeds - discount - advance - checkinpayment);
                    

                    string structure = String.Format("(Room Price)\t\t{0}" +
                                                     "\r\n(Extra Beds)\t\t{1}" +
                                                     "\r\n(Discount)\t\t-{2}" +
                                                     "\r\n(Advance)\t\t-{3}" +
                                                     "\r\n(Paid when checked in)\t-{4}" +
                                                     "\r\n(Total)\t\t{5}" +
                                                     "\r\nAdditional charges \t" +
                                                     "\r\n(Extra Meals/Liquor)\t{6}" +
                                                     "\r\n(Damage recovery)\t\t{7}" +
                                                     "\r\n(Late fees)\t\t{8}" +
                                                     "\r\n\t___________________" +
                                                     "\r\nAmount to be paid\t\t{9}",roomprice,exbeds,discount,advance,checkinpayment,amttbp,exmeals,damages,latefees,(amttbp+exmeals+damages+latefees));

                    
                    billTxt.Text = structure;

                    checkoutBtn.Visible = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MetroMessageBox.Show(this, "Please select a reservation from the table to calculate bill.", "First things first");
            }
        }

        private void damageChargesTxt_TextChanged(object sender, EventArgs e)
        {
            checkoutBtn.Visible = false;
        }

        private void checkoutBtn_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(checkoutResIDTxt.Text))
            {
                double total=0,latefee=0,damage=0;

                Double.TryParse(checkoutLatefeeTxt.Text, out latefee);
                Double.TryParse(damageChargesTxt.Text, out damage);
                Double.TryParse(checkoutGrid.SelectedRows[0].Cells["COtotPrice"].Value.ToString(), out total);
                
                //to calculate the updated total price
                total = total + latefee + damage;

                try
                {
                    DatabaseConnection con = new DatabaseConnection();
                    con.ConnectDB(this);

                    string sql = String.Format("update reservations set totalprice={0}, latefee={1}, damagecharges={2}, resstatus='CheckedOut', totalamounttbp=0,paymentstatus='Paid' where resno='{3}'",total,latefee,damage,checkoutResIDTxt.Text);

                    if (con.InsertQuery(this, sql))
                    {
                        MetroMessageBox.Show(this,"Check-Out successfull!","Success",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                        RefreshTables();
                    }

                }
                catch(Exception ex)
                {

                }

            }
            else
            {
                MetroMessageBox.Show(this, "Please select a reservation from the table, click on Calculate bill then Checkout to proceed Check-Out.", "First things first");
            }

        }

        private void Reception_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Dispose();
        }
    }
}
