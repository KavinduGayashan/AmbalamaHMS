using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ambalama_HMS_with_MetroUI
{
    class Guest
    {

        private String _gid=null;
        private String _name = null;
        private String _address = null;
        private String _city = null;
        private String _phone = null;

        public Guest()
        {

        }

        public Guest(String nic,String name,String address, String city, String phone)
        {
            this._gid = nic;
            this._name = name;
            this._address = address;
            this._city = city;
            this._phone = phone;
        }

        public String GID {
            get{ return _gid; }
            set { this._gid = value; }
        }

        public String Name
        {
            get { return _name; }
            set { this._name = value; }
        }

        public String Address
        {
            get { return _address; }
            set { this._address = value; }
        }

        public String City
        {
            get { return _city; }
            set { this._city = value; }
        }

        public String Phone
        {
            get { return _phone; }
            set { this._phone = value; }
        }


        public Boolean AddGuest(Reception window)
        {
            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(window);
                string sql = "insert into Guests(GuestID,Name,Phone,Address,City) values ('" + _gid + "','" + _name + "','" + _phone + "','" + _address + "','" + _city + "');";

                if (con.InsertQuery(sql))
                {
                    con.CloseConnection();
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured. \n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return false;
        }

        public Boolean UpdateGuest(Reception window)
        {
            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(window);
                string sql = "update Guests set Name='" + _name + "', Phone='" + _phone + "', Address='" + _address + "', City='" + _city + "' where GuestID='" + _gid + "'";

                if (con.InsertQuery(sql))
                {
                    con.CloseConnection();
                    return true;
                }

                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured. \n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }


        public Boolean DeleteGuest(Reception window)
        {
            try { 
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(window);
                string sql = "delete from guests where guestid='" + _gid + "'";

                if (con.DeleteQuery(sql))
                {
                    con.CloseConnection();
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(window, "Error occured. \n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public void Reset()
        {
            _gid = null;
            _name = null;
            _address = null;
            _city = null;
            _phone = null;
        }

    }
}
