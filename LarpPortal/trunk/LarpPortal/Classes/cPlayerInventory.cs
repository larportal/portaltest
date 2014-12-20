using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;


namespace LarpPortal.Classes
{
    public class cPlayerInventory
    {

        private string _UserName = "";
        private Int32 _UserID = -1;
        private Int32 _PlayerInventoryID = -1;
        private Int32 _PlayerProfileID = -1;
        private string _Description = "";
        private Int32 _InventoryType = -1;
        private string _Quantity = "";
        private string _Size = "";
        private string _PowerNeeded = "";
        private Int32 _WillShare = -1;
        private string _InventoryNotes = "";
        private string _InventoryImage = "";
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public Int32 PlayerInventoryID
        {
            get { return _PlayerInventoryID; }
        }
        public Int32 PlayerProfileID
        {
            get { return _PlayerProfileID; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Int32 InventoryType
        {
            get { return _InventoryType; }
            set { _InventoryType = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        public string PowerNeeded
        {
            get { return _PowerNeeded; }
            set { _PowerNeeded = value; }
        }
        public Int32 WillShare
        {
            get { return _WillShare; }
            set { _WillShare = value; }
        }
        public string InventoryNotes
        {
            get { return _InventoryNotes; }
            set { _InventoryNotes = value; }
        }
        public string InventoryImage
        {
            get { return _InventoryImage; }
            set { _InventoryImage = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public DateTime DateAdded
        {
            get { return _DateAdded; }
        }
        public DateTime DateChanged
        {
            get { return _DateChanged; }
        }



        private cPlayerInventory()
        {

        }

        public cPlayerInventory(Int32 intPlayerInventoryID, Int32 intPlayerProfileID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerInventoryID = intPlayerInventoryID;
            _PlayerProfileID = intPlayerProfileID;
            _UserName = strUserName;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerInventoryID", _PlayerInventoryID);
            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetPlayerInventoryByID", slParams, "LarpPortal", _UserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _PlayerProfileID = ldt.Rows[0]["PlayerProfileID"].ToString().ToInt32();
                    _Description = ldt.Rows[0]["Description"].ToString();
                    _InventoryType = ldt.Rows[0]["InventoryTypeID"].ToString().ToInt32();
                    _Quantity = ldt.Rows[0]["Quantity"].ToString();
                    _Size = ldt.Rows[0]["Size"].ToString();
                    _PowerNeeded = ldt.Rows[0]["PowerNeeded"].ToString();
                    _WillShare = ldt.Rows[0]["WillShare"].ToString().ToInt32();
                    _InventoryNotes = ldt.Rows[0]["InventoryNotes"].ToString();
                    _InventoryImage = ldt.Rows[0]["InventoryImage"].ToString();
                    _Comments = ldt.Rows[0]["Comments"].ToString();
                    _DateAdded = Convert.ToDateTime(ldt.Rows[0]["DateAdded"].ToString());
                    _DateChanged = Convert.ToDateTime(ldt.Rows[0]["DateChanged"].ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
            }
        }
        public Boolean Save()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            Boolean blnReturn = false;
            try
            {
                SortedList slParams = new SortedList();
                slParams.Add("@UserID", _UserID);
                slParams.Add("@PlayerInventoryID", _PlayerInventoryID);
                slParams.Add("@PlayerProfileID", _PlayerProfileID);
                slParams.Add("@Description", _Description);
                slParams.Add("@InventoryTypeID", _InventoryType);
                slParams.Add("@Quantity", _Quantity);
                slParams.Add("@Size", _Size);
                slParams.Add("@PowerNeeded", _PowerNeeded);
                slParams.Add("@WillShare", _WillShare);
                slParams.Add("@InventoryNotes", _InventoryNotes);
                slParams.Add("@InventoryImage", _InventoryImage);
                slParams.Add("@Comments", _Comments);
                blnReturn = cUtilities.PerformNonQueryBoolean("uspInsUpdPLPlayerInventory", slParams, "LarpPoral", _UserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                blnReturn = false;
            }
            return blnReturn;
        }
       

    }


}