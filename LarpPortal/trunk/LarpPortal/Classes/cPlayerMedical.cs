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
    public class cPlayerMedical
    {

        private Int32 _PlayerMedicalID = -1;
        private Int32 _PlayerProfileID = -1;
        private string _UserName = "";
        private Int32 _MedicalTypeID = -1;
        private string _MedicalTypeDescription = "";
        private string _Description = "";
        private string _Medication = "";
        private Boolean _ShareInfo = false;
        private Boolean _PrintOnCard = false;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private string _Comments = "";
        private DateTime _DateAdded;
        private DateTime _DateChanged;

        public DateTime DateChanged
        {
            get { return _DateChanged; }
            set { _DateChanged = value; }
        }      
        public DateTime DateAdded
        {
            get { return _DateAdded; }
            set { _DateAdded = value; }
        }  
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }      
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }   
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }     
        public Boolean PrintOnCard
        {
            get { return _PrintOnCard; }
            set { _PrintOnCard = value; }
        }      
        public Boolean ShareInfo 
        {
            get { return _ShareInfo; }
            set { _ShareInfo = value; }
        }    
        public string Medication
        {
            get { return _Medication; }
            set { _Medication = value; }
        }       
        public string Description
        {
            get { return _Description = ""; }
            set { _Description = value; }
        } 
        public string MedicalTypeDescription 
        {
            get { return _MedicalTypeDescription; }
        }       
	    public int MedicalTypeID
	    {
		    get { return _MedicalTypeID;}
		    set { _MedicalTypeID = value;}
	    }
        public int PlayerProfileID
        {
            get{return _PlayerProfileID;}
            set{_PlayerProfileID = value;}
        }
	    public int PlayerMedicalID
	    {
		    get { return _PlayerMedicalID;}
		    set { _PlayerMedicalID = value;}
	    }
	
        



        private cPlayerMedical()
        {

        }

        public cPlayerMedical(Int32 intPlayerMedicalID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _PlayerMedicalID = intPlayerMedicalID;

            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
            slParams.Add("@PlayerMedicalID", _PlayerMedicalID);

            try
            {
                DataTable ldt = cUtilities.LoadDataTable("uspGetSomeData", slParams, "DefaultSQLConnection", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _someValue = ldt.Rows[0]["SomeValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + lsRoutineName);
            }
        }
       

    }


}