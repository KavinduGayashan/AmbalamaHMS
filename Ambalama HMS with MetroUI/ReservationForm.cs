using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ambalama_HMS_with_MetroUI
{
    public partial class ReservationForm : MetroFramework.Forms.MetroForm
    {
        private String _guestid;
        private String _roomid;
        private String _checkindate;
        private String _checkoutdate;
        private String _guestname;
        private String _guestphone;
        private double advance = 0;
        private int discount=0, beds;
        private double roomPrice=0;
        private double totwithdis;
        private Room room = new Room();
        private Reception reception;
        double exbedprice = 0;
        double discountprice=0;
        double totalamt = 0;



        public ReservationForm(Reception window,String guestid,String roomid,String checkindate,String checkoutdate)
        {
            InitializeComponent();

            reception = window;

            _guestid = guestid;
            _roomid = roomid;
            _checkindate = checkindate;
            _checkoutdate = checkoutdate;

            resNofGuestsCombo.SelectedIndex = 0;
            resExtraBedCombo.SelectedIndex = 0;
            resPackageCombo.SelectedIndex = 0;
            resBedCateCombo.SelectedIndex = 0;

            room.Roomid = _roomid;
            room.SetRoomType(this);

            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);
                string sql = String.Format("select name,phone from Guests where guestid='{0}'",_guestid);
                SqlDataReader rdr = con.GetData(this, sql);

                if (rdr.Read())
                {
                    _guestname=rdr.GetString(0);
                    _guestphone = rdr.GetString(1);
                    resGPhoneTxt.Text = _guestphone;
                    resGNameTxt.Text = _guestname;
                    
                }

                con.CloseConnection();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,"Error occured! \nError:"+ex.Message,"Unexpected Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            resGIDTxt.Text = _guestid;
            resCheckinDate.Text = _checkindate;
            resCheckoutDate.Text = _checkoutdate;
            resRoomId.Text = _roomid;

            reserveBtn.Visible = false;
        }

        private void calcPriceBtn_Click(object sender, EventArgs e)
        {
            //creating a new room object to get the room price for calculations

            try
            {
                roomPrice = room.GetRoomPrice(this, resPackageCombo.SelectedItem.ToString(), resBedCateCombo.SelectedItem.ToString());
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, "Error occured while trying to get the room price. \n Error:" + ex.Message, "Error Occured!");
            }
            //to get the date difference
            DateTime checkin, checkout;

            DateTime.TryParseExact(_checkindate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkin);
            DateTime.TryParseExact(_checkoutdate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkout);

            double days = (checkout - checkin).TotalDays;
            int count=0;
            ;

            //to get the extra bed price
            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);

                string sql = "select perextrabed from packages where roomtype='" + room.RoomType + "'";

                SqlDataReader rdr = con.GetData(this, sql);

                if (rdr.Read())
                {
                    exbedprice=(double) rdr.GetSqlSingle(0);
                }

                rdr.Close();

                sql = "select count (resno) from reservations where guestid='"+resGIDTxt.Text+"' group by guestid" ;
                rdr = con.GetData(this, sql);
                if (rdr.Read())
                {
                    count=rdr.GetInt32(0);
                }

                con.CloseConnection();

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, "Error occured! \nError:" + ex.Message, "Unexpected Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            Int32.TryParse(resExtraBedCombo.SelectedItem.ToString(),out beds);
            Int32.TryParse(discountTxt.Text.Substring(0,2), out discount);
            Double.TryParse(advancePaymentTxt.Text,out advance);

            if (count >= 3)
            {
                discount = 10;
            }

            roomPrice *= days;
            exbedprice = exbedprice * beds;
            
            totalamt = roomPrice + exbedprice;
            discountprice = totalamt * (discount / 100.0);
            totwithdis = totalamt-discountprice-advance;
            

            totalAmtTxt.Text = "(Price of room) \r \t "+(double)roomPrice+"\r\n (Extra bed charges) \r \t"+exbedprice+"\r\n (Total) \r \t "+totalamt
                + "\r\n -(Discount "+discount+"%) \r \t "+discountprice+ "\r\n -(Advance Payment) \r \t " + advance+ "\r\n _______________\r\n (Amount to be paid) \r \t "+totwithdis;

            reserveBtn.Visible = true;
            reserveBtn.Enabled = true;

            
            
        }

        private void reserveBtn_Click(object sender, EventArgs e)
        {

            try
            {
                DatabaseConnection con = new DatabaseConnection();
                con.ConnectDB(this);

                DateTime today = DateTime.Now;
                string todaystr = today.ToString("yyyy-MM-dd HH:mm:ss");

                int noofguests;
                Int32.TryParse(resNofGuestsCombo.SelectedItem.ToString(), out noofguests);

                string sql = String.Format("insert into Reservations(resdate,guestid,roomid,package,restype,noofguests,checkin,checkout,advancepayment,discount,vehicleno,resstatus,totalamounttbp,roomprice,extrabeds,extrabedcharge,discountamount,totalprice) values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9},'{10}','{11}',{12},{13},{14},{15},{16},{17})",
                    todaystr,_guestid,_roomid,resPackageCombo.SelectedItem.ToString(),resBedCateCombo.SelectedItem.ToString(),noofguests,_checkindate,_checkoutdate,
                    advance,discount,resGVehicleTxt.Text,"Reserved",totwithdis,roomPrice,beds,exbedprice,discountprice,totalamt);

                if(con.InsertQuery(this, sql))
                {
                    MetroMessageBox.Show(this, "Reservation is successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    this.Close();
                }
                else
                {
                    MetroMessageBox.Show(this, "Reservation is not successful!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }


            }
            catch (Exception ex)
            {

            }
        
        }

        private void resExtraBedCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            reserveBtn.Enabled = false;
        }
    }
}
