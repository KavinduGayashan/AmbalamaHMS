using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MetroFramework;
using System.Windows.Forms;

namespace Ambalama_HMS_with_MetroUI
{
    class Room
    {
        private String _roomid;
        private String _capacity;
        private Boolean _ac;
        private Boolean _hotwater;
        private Boolean _dtv;
        private String _status;
        private String _roomtype;

        public String Roomid
        {
            get { return _roomid; }
            set { _roomid = value; }
        }

        public String Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public Boolean AC
        {
            get { return _ac; }
            set { _ac = value; }
        }

        public Boolean Hotwater
        {
            get { return _hotwater; }
            set { _hotwater = value; }
        }

        public Boolean DialogTV
        {
            get { return _dtv; }
            set { _dtv = value; }
        }

        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public String RoomType
        {
            get { return _roomtype; }
            set { _roomtype = value; }
        }

        public double GetRoomPrice(ReservationForm window,String package,String bed)
        {
            string packbed = null;

            if(package.Equals("RoomOnly"))
            {

                switch (bed)
                {
                    case "Single":
                        packbed = "RoomOnlySGL";
                        break;
                    case "Double":
                        packbed = "RoomOnlyDBL";
                        break;
                    case "Triple":
                        packbed = "RoomOnlyTPL";
                        break;
                    case "4 Pax":
                        packbed = "RoomOnlyFour";
                        break;
                    case "5 Pax":
                        packbed = "RoomOnlyFive";
                        break;
                    case "10 Pax":
                        packbed = "RoomOnlyTen";
                        break;
                }

            }
            else if(package.Equals("BednBreakfast"))
            {
                switch (bed)
                {
                    case "Single":
                        packbed = "BednBreakfastSGL";
                        break;
                    case "Double":
                        packbed = "BednBreakfastDBL";
                        break;
                    case "Triple":
                        packbed = "BednBreakfastTPL";
                        break;
                    case "4 Pax":
                        packbed = "BednBreakfastFour";
                        break;
                    case "5 Pax":
                        packbed = "BednBreakfastFive";
                        break;
                    case "10 Pax":
                        packbed = "BednBreakfastTen";
                        break;
                }
            }
            else if(package.Equals("HalfBoard"))
            {
                switch (bed)
                {
                    case "Single":
                        packbed = "HalfBoardSGL";
                        break;
                    case "Double":
                        packbed = "HalfBoardDBL";
                        break;
                    case "Triple":
                        packbed = "HalfBoardTPL";
                        break;
                    case "4 Pax":
                        packbed = "HalfBoardFour";
                        break;
                    case "5 Pax":
                        packbed = "HalfBoardFive";
                        break;
                    case "10 Pax":
                        packbed = "HalfBoardTen";
                        break;
                }
            }
            else if (package.Equals("FullBoard"))
            {
                switch (bed)
                {
                    case "Single":
                        packbed = "FullBoardSGL";
                        break;
                    case "Double":
                        packbed = "FullBoardDBL";
                        break;
                    case "Triple":
                        packbed = "FullBoardTPL";
                        break;
                    case "4 Pax":
                        packbed = "FullBoardFour";
                        break;
                    case "5 Pax":
                        packbed = "FullBoardFive";
                        break;
                    case "10 Pax":
                        packbed = "FullBoardTen";
                        break;
                }
            }


            try
            {
                DatabaseConnection con = new DatabaseConnection();

                con.ConnectDB(window);
                string sql = String.Format("select {0} from packages where roomtype='{1}'",packbed,_roomtype);
                
                SqlDataReader rdr= con.GetData(window,sql);

                if (rdr.Read())
                {
                    double price=(double)rdr.GetSqlSingle(0);
                    return price;
                }

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(window, "Error Occured while connecting to the database.\nError:" + ex.Message, "Error!", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            return -1;
        }

        public void SetRoomType(ReservationForm window)
        {
            if (!String.IsNullOrEmpty( _roomid))
            {
                try
                {
                    DatabaseConnection con = new DatabaseConnection();

                    con.ConnectDB(window);
                    string sql = String.Format("select roomtype from rooms where roomid='{0}'", _roomid);

                    SqlDataReader rdr = con.GetData(window, sql);

                    if (rdr.Read())
                    {
                        _roomtype = rdr.GetString(0);

                    }

                }
                catch (Exception ex)
                {
                    MetroMessageBox.Show(window, "Error Occured while connecting to the database.\nError:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MetroMessageBox.Show(window, "Set a Room ID first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }
    }
}
