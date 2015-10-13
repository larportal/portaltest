using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal.Classes
{
    public class cPoints
    {
        private int _UserID;
        private int _CampaignCPOpportunityID;
        private int _CampaignPlayerID;
        private int _CharacterID;
        private int _CampaignCPOpportunityDefaultID;
        private int _EventID;
        private string _Description;
        private string _OpportunityNotes;
        private string _ExampleURL;
        private int _ReasonID;
        private int _StatusID;
        private int _AddedByID;
        private double _CPValue;
        private int _ApprovedByID;
        private DateTime _ReceiptDate;
        private int _ReceivedByID;
        private DateTime _CPAssignmentDate;
        private string _StaffComments;
        private string _Comments;
        private double _MaximumCPPerYear;
        private bool _AllowCPDonation;
        private double _EventCharacterCap;
        private double _AnnualCharacterCap;
        private double _TotalCharacterCap;
        private int _EarliestCPApplicationYear;
        private int _CampaignID;
        private double _TotalCP;
        private int _PLAuditStatus;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int CampaignCPOpportunityID
        {
            get { return _CampaignCPOpportunityID; }
            set { _CampaignCPOpportunityID = value; }
        }
        public int CampaignPlayerID
        {
            get { return _CampaignPlayerID; }
            set { _CampaignPlayerID = value; }
        }
        public int CharacterID
        {
            get { return _CharacterID; }
            set { _CharacterID = value; }
        }
        public int CampaignCPOpportunityDefaultID
        {
            get { return _CampaignCPOpportunityDefaultID; }
            set { _CampaignCPOpportunityDefaultID = value; }
        }
        public int EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string OpportunityNotes
        {
            get { return _OpportunityNotes; }
            set { _OpportunityNotes = value; }
        }
        public string ExampleURL
        {
            get { return _ExampleURL; }
            set { _ExampleURL = value; }
        }
        public int ReasonID
        {
            get { return _ReasonID; }
            set { _ReasonID = value; }
        }
        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }
        public int AddedByID
        {
            get { return _AddedByID; }
            set { _AddedByID = value; }
        }
        public double CPValue
        {
            get { return _CPValue; }
            set { _CPValue = value; }
        }
        public int ApprovedByID
        {
            get { return _ApprovedByID; }
            set { _ApprovedByID = value; }
        }
        public DateTime ReceiptDate
        {
            get { return _ReceiptDate; }
            set { _ReceiptDate = value; }
        }
        public int ReceivedByID
        {
            get { return _ReceivedByID; }
            set { _ReceivedByID = value; }
        }
        public DateTime CPAssignmentDate
        {
            get { return _CPAssignmentDate; }
            set { _CPAssignmentDate = value; }
        }
        public string StaffComments
        {
            get { return _StaffComments; }
            set { _StaffComments = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }
        public double MaximumCPPerYear
        {
            get { return _MaximumCPPerYear; }
            set { _MaximumCPPerYear = value; }
        }
        public bool AllowCPDonation
        {
            get { return _AllowCPDonation; }
            set { _AllowCPDonation = value; }
        }
        public double EventCharacterCap
        {
            get { return _EventCharacterCap; }
            set { _EventCharacterCap = value; }
        }
        public double AnnualCharacterCap
        {
            get { return _AnnualCharacterCap; }
            set { _AnnualCharacterCap = value; }
        }
        public double TotalCharacterCap
        {
            get { return _TotalCharacterCap; }
            set { _TotalCharacterCap = value; }
        }
        public int EarliestCPApplicationYear
        {
            get { return _EarliestCPApplicationYear; }
            set { _EarliestCPApplicationYear = value; }
        }
        public int CampaignID
        {
            get { return _CampaignID; }
            set { _CampaignID = value; }
        }
        public double TotalCP
        {
            get { return _TotalCP; }
            set { _TotalCP = value; }
        }
        public int PLAuditStatus
        {
            get { return _PLAuditStatus; }
            set { _PLAuditStatus = value; }
        }

        /// <summary>
        /// This will load a CP Opportunity
        /// Must pass a CPOpportunityID
        /// </summary>
        public void LoadCPOpportunity(int UserID, int OpportunityID)
        {
            string stStoredProc = "uspGetCampaignPointOpportunity";
            string stCallingMethod = "cPoints.LoadCPOpportunity";
            int iTemp = 0;
            double dblTemp = 0;
            DateTime dtTemp;
            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignCPOpportunityID", OpportunityID);
            DataSet dsOpportunity = new DataSet();
            dsOpportunity = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            dsOpportunity.Tables[0].TableName = "CMCampaignCPOpportunities";
            foreach (DataRow dRow in dsOpportunity.Tables["CMCampaignCPOpportunities"].Rows)
            {
                if (int.TryParse(dRow["CampaignCPOpportunityID"].ToString(), out iTemp))
                    CampaignCPOpportunityID = iTemp;
                if (int.TryParse(dRow["CampaignPlayerID"].ToString(), out iTemp))
                    CampaignPlayerID = iTemp;
                if (int.TryParse(dRow["CharacterID"].ToString(), out iTemp))
                    CharacterID = iTemp;
                if (int.TryParse(dRow["CampaignCPOpportunityDefaultID"].ToString(), out iTemp))
                    CampaignCPOpportunityDefaultID = iTemp;
                if (int.TryParse(dRow["EventID"].ToString(), out iTemp))
                    EventID = iTemp;
                if (int.TryParse(dRow["ReasonID"].ToString(), out iTemp))
                    ReasonID = iTemp;
                if (int.TryParse(dRow["StatusID"].ToString(), out iTemp))
                    StatusID = iTemp;
                if (int.TryParse(dRow["AddedByID"].ToString(), out iTemp))
                    AddedByID = iTemp;
                if (int.TryParse(dRow["ApprovedByID"].ToString(), out iTemp))
                    ApprovedByID = iTemp;
                if (int.TryParse(dRow["ReceivedByID"].ToString(), out iTemp))
                    ReceivedByID = iTemp;
                if (double.TryParse(dRow["CPValue"].ToString(), out dblTemp))
                    CPValue = dblTemp;
                if (DateTime.TryParse(dRow["ReceiptDate"].ToString(), out dtTemp))
                    ReceiptDate = dtTemp;
                if (DateTime.TryParse(dRow["CPAssignmentDate"].ToString(), out dtTemp))
                    CPAssignmentDate = dtTemp;
                Description = dRow["Description"].ToString();
                OpportunityNotes = dRow["OpportunityNotes"].ToString();
                ExampleURL = dRow["ExampleURL"].ToString();
                StaffComments = dRow["StaffComments"].ToString();
                Comments = dRow["Comments"].ToString();
            }
        }

        /// <summary>
        /// This will "delete" a CP Opportunity
        /// Must pass a CPOpportunityID and UserID
        /// </summary>
        public void DeleteCPOpportunity(int UserID, int OpportunityID)
        {
            string stStoredProc = "uspDelCMCampaignCPOpportunities";
            //string stCallingMethod = "cPoints.DeleteCPOpportunity";
            SortedList slParameters = new SortedList();
            slParameters.Add("@RecordID", OpportunityID);
            slParameters.Add("@UserID", UserID);
            try
            {
                cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
            }
            catch
            {

            }
        }

        /// <summary>
        /// This will post points for a PEL
        /// Must pass a CPOpportunityID and UserID
        /// </summary>
        public void AssignPELPoints(int UserID, int CampaignPlayer, int Character, int CampaignCPOpportunityDefault, int Event,
            string EventDescription, int Reason, int Campaign, double CPVal, DateTime RecptDate)
        {
            string stStoredProc = "uspGetCampaignCPOpportunityByID";
            string stCallingMethod = "cPoints.AssignPELPoints.GetCPOpportunity";
            DataTable dtOppDefault = new DataTable();
            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignCPOpportunityDefaultID", CampaignCPOpportunityDefault);
            dtOppDefault = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            string strDescription = "";
            foreach (DataRow drow in dtOppDefault.Rows)
            {
                strDescription = drow["Description"].ToString();
            }

            // Check to see if the PEL was submitted on time to get CP
            // Take the eventID and go get its PEL deadline date
            stStoredProc = "uspGetEventInfoByID";
            stCallingMethod = "cPoints.AssignPELPoints.GetPELDeadline";
            string strEventName = "";
            DataTable dtEvent = new DataTable();
            slParameters.Clear();
            slParameters.Add("@EventID", Event);
            dtEvent = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            DateTime dtTemp;
            DateTime PELDeadline = DateTime.Today;
            DateTime dtEventDate = DateTime.Today;
            foreach (DataRow drow2 in dtEvent.Rows)
            {
                if (DateTime.TryParse(drow2["PELDeadlineDate"].ToString(), out dtTemp))
                    PELDeadline = dtTemp;
                if (DateTime.TryParse(drow2["StartDate"].ToString(), out dtTemp))
                    dtEventDate = dtTemp;
                strEventName = String.Format("{0:MM/dd/yyyy}", dtTemp);
                strEventName = strEventName + " - " + drow2["EventName"].ToString();
                EventDescription = strEventName;
            }

            PELDeadline = PELDeadline.AddDays(2);   // For now, give a 2 day grace period.  Eventually, put the grace period as a field in CMCampaigns
            if(RecptDate > PELDeadline)
            {
                CPVal = 0;
                strDescription = strDescription.Trim() + " - Late";
            }
            int iTemp = 0;
            // If Campaign = 0, go out and get campaign, assuming there's an EventID
            if (Campaign == 0  && Event != 0)
            {
                stStoredProc = "uspGetCampaignFromEvent";
                stCallingMethod = "cPoints.AssignPELPoints.GetCampaignFromEvent";
                DataTable dtCampaign = new DataTable();
                slParameters.Clear();
                slParameters.Add("@EventID", Event);
                dtCampaign = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
                if (dtCampaign.Rows.Count > 0)
                {
                    foreach (DataRow drow4 in dtCampaign.Rows)
                    {
                        if (int.TryParse(drow4["CampaignID"].ToString(), out iTemp))
                            Campaign = iTemp;
                    }
                }         
            }

            // If Character = 0, go out and get character for campaign.  If only one, set Character = that one.  If <> one character, leave at 0 and bank.
            if (Character == 0 && Campaign !=0)
            {
                stStoredProc = "uspGetCharacters";
                stCallingMethod = "cPoints.AssignPELPoints.GetCharacters";
                DataTable dtChars = new DataTable();
                slParameters.Clear();
                slParameters.Add("@CampaignID", Campaign);
                slParameters.Add("@CampaignPlayerID", CampaignPlayer);
                dtChars = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
                if (dtChars.Rows.Count == 1)
                {
                    foreach (DataRow drow3 in dtChars.Rows)
                    {
                        if (int.TryParse(drow3["CharacterID"].ToString(), out iTemp))
                            Character = iTemp;
                    }
                }
            }

            // Call the routine to add the opportunity.  Create it already assigned (last two parameters both = 1)
            InsUpdCPOpportunity(UserID, -1, CampaignPlayer, Character, CampaignCPOpportunityDefault, Event, strDescription, EventDescription, "", Reason, 21, UserID, CPVal, UserID, RecptDate, UserID, DateTime.Now, "", 1,1);

            // Call the routine to check if CP can be assigned to character and if appropriate assign the CP
            AddPointsToCharacter(Character, CPVal);

            // Call the routine to add the CP to the player CP audit log.  If assigned to character, create it spent otherwise
            //      create it banked (_PLPlayerAuditStatus)
            CreatePlayerCPLog(UserID, _CampaignCPOpportunityID, RecptDate, CPVal, Reason, CampaignPlayer, Character);
        }

        /// <summary>
        /// This will post points for a PEL
        /// Must pass a CPOpportunityID and UserID
        /// </summary>
        public void AssignHistoryPoints(int UserID, int CampaignPlayer, int Character, int CampaignCPOpportunityDefault,
            int Campaign, double CPVal, DateTime RecptDate)
        {
            int Event = 0;
            string EventDescription = "";
            int Reason = 24;
            string stStoredProc = "uspGetCampaignCPOpportunityByID";
            string stCallingMethod = "cPoints.AssignPELPoints";
            DataTable dtOppDefault = new DataTable();
            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignCPOpportunityDefaultID", CampaignCPOpportunityDefault);
            dtOppDefault = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            string strDescription = "";
            foreach (DataRow drow in dtOppDefault.Rows)
            {
                strDescription = drow["Description"].ToString();
            }

            // Call the routine to add the opportunity.  Create it already assigned (last two parameters both = 1)
            InsUpdCPOpportunity(UserID, -1, CampaignPlayer, Character, CampaignCPOpportunityDefault, Event, strDescription, EventDescription, "", Reason, 21, UserID, CPVal, UserID, RecptDate, UserID, DateTime.Now, "", 1, 1);

            // Call the routine to check if CP can be assigned to character and if appropriate assign the CP
            AddPointsToCharacter(Character, CPVal);

            // Call the routine to add the CP to the player CP audit log.  If assigned to character, create it spent otherwise
            //      create it banked (_PLPlayerAuditStatus)
            CreatePlayerCPLog(UserID, _CampaignCPOpportunityID, RecptDate, CPVal, Reason, CampaignPlayer, Character);
        }

        /// <summary>
        /// This will create an entry in the player CP audit log
        /// Must pass a CPOpportunityID and UserID
        /// </summary>
        public void CreatePlayerCPLog(int UserID, int OpportunityID, DateTime RecptDate, double CPVal, int Reason, 
            int CampaignPlayer, int CharacterID)
        {
            int iTemp = 0;
            int PlayerID = 0; // This is the MDBUserID of the player getting the points
            DataTable dtPlayer = new DataTable();
            string stStoredProc = "uspGetCampaignPlayerByID";
            string stCallingMethod = "cPoints.CreatePlayerCPLog";
            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignPlayerID", CampaignPlayer);
            dtPlayer = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            foreach (DataRow drow in dtPlayer.Rows)
            {
                if (int.TryParse(drow["UserID"].ToString(), out iTemp))
                    PlayerID = iTemp;
            }
            if (_PLAuditStatus == 60)
                CharacterID = 0;
            stStoredProc = "uspInsUpdPLPlayerCPAudit";
            DateTime datevar = DateTime.Today;
            int ThisYear = datevar.Year;
            //string stCallingMethod = "cPoints.DeleteCPOpportunity";
            slParameters.Clear();
            slParameters.Add("@UserID", UserID);
            slParameters.Add("@PlayerCPAuditID", -1);
            slParameters.Add("@PlayerID", PlayerID);
            slParameters.Add("@TransactionDate", RecptDate);
            slParameters.Add("@CPApprovedDate", DateTime.Today);
            slParameters.Add("@CPApprovedBy", UserID);
            slParameters.Add("@CPAmount", CPVal);
            // TODO - Rick - For now apply it to this year.  We'll worry about restrictions later.
            slParameters.Add("@YearAppliedTo", ThisYear);
            slParameters.Add("@ReasonID", Reason);
            slParameters.Add("@CampaignCPOpportunityID", OpportunityID);
            slParameters.Add("@AddedBy", UserID);
            slParameters.Add("@ReceivedFromCampaignID", _CampaignID);
            // TODO - Rick - For now assume this isn't from one player passing to another
            slParameters.Add("@ReceivedFromPlayerCPAuditID", 0);
            slParameters.Add("@SentToCampaignPlayerID", CampaignPlayer);
            slParameters.Add("@CharacterID", CharacterID);
            slParameters.Add("@StatusID", _PLAuditStatus);

            try
            {
                cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
            }
            catch
            {

            }
        }

        /// <summary>
        /// This will update an existing CP Opportunity
        /// </summary>
        public void UpdateCPOpportunity(int UserID, int CampaignCPOpportunityID, int CampaignPlayerID, int CharacterID,
            int CampaignOpportunityDefaultID, int EventID, string DescriptionText, string OppNotes, string URL, int ReasonID,
            int AddedByID, double CPVal, int ApprovedByID, DateTime RecptDate, int ReceivedByID, string StaffComments)
        {
            // Call the routine to update the opportunity.  Create it already assigned (last two parameters both = 1)
            InsUpdCPOpportunity(UserID, CampaignCPOpportunityID, CampaignPlayerID, CharacterID, CampaignOpportunityDefaultID, 
                EventID, DescriptionText, OppNotes, URL, ReasonID, 21, UserID, CPVal, UserID, RecptDate, UserID, 
                DateTime.Now, StaffComments, 1, 1);

            // Call the routine to check if CP can be assigned to character and if appropriate assign the CP
            AddPointsToCharacter(CharacterID, CPVal);

            // Call the routine to add the CP to the player CP audit log.  If assigned to character, create it spent otherwise
            //      create it banked (_PLPlayerAuditStatus)
            CreatePlayerCPLog(UserID, CampaignCPOpportunityID, RecptDate, CPVal, ReasonID, CampaignPlayerID, CharacterID);
        }

        /// <summary>
        /// This will add or update a CP Opportunity
        /// Must pass a CPOpportunityID
        /// </summary>
        public void InsUpdCPOpportunity(int UserID, int OpportunityID, 
            int CampaignPlayer, int Character, int CampaignOppDefault, int Event, string DescriptionText, string OpportunityNotesText,
            string URL, int Reason, int Status, int AddedBy, double CPVal, int ApprovedBy, DateTime RecptDate, int ReceivedBy, 
            DateTime CPAssignment, string StaffCommentsText, int PLAudit, int CharacterUpdate)
        {
            string stStoredProc = "uspInsUpdCMCampaignCPOpportunities";
            string stCallingMethod = "cPoints.InsUpdCPOpportunity";
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserID", UserID);
            slParameters.Add("@CPAssignmentDate", DateTime.Today);

            if(OpportunityID == -1) // Add a new opportunity
            {
                DataTable dtPoints = new DataTable();
                slParameters.Add("@CampaignCPOpportunityID", OpportunityID);
                slParameters.Add("@CampaignPlayerID", CampaignPlayer);
                slParameters.Add("@CharacterID", Character);
                slParameters.Add("@CampaignCPOpportunityDefaultID", CampaignOppDefault);
                slParameters.Add("@EventID", Event);
                slParameters.Add("@Description", DescriptionText);
                slParameters.Add("@OpportunityNotes", OpportunityNotesText);
                slParameters.Add("@ExampleURL", URL);
                slParameters.Add("@ReasonID", Reason);
                slParameters.Add("@StatusID", Status);
                slParameters.Add("@AddedByID", AddedBy);
                slParameters.Add("@CPValue", CPVal);
                slParameters.Add("@ApprovedByID", ApprovedBy);
                slParameters.Add("@ReceiptDate", RecptDate);
                slParameters.Add("@ReceivedByID", ReceivedBy);
                slParameters.Add("@StaffComments", StaffCommentsText);
                slParameters.Add("@PLAudit", PLAudit);
                slParameters.Add("@CharacterUpdate", CharacterUpdate);
                //cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
                dtPoints = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
                int iTemp = 0;
                foreach (DataRow drow in dtPoints.Rows)
                {
                    if (int.TryParse(drow["CampaignCPOpportunityID"].ToString(), out iTemp))
                        _CampaignCPOpportunityID = iTemp;
                }
            }
            else      // Change an existing
            {
                slParameters.Add("@CampaignCPOpportunityID", OpportunityID);
                slParameters.Add("@CampaignPlayerID", CampaignPlayer);
                slParameters.Add("@CharacterID", Character);
                slParameters.Add("@CampaignCPOpportunityDefaultID", CampaignOppDefault);
                slParameters.Add("@EventID", Event);
                slParameters.Add("@Description", DescriptionText);
                slParameters.Add("@OpportunityNotes", OpportunityNotesText);
                slParameters.Add("@ExampleURL", URL);
                slParameters.Add("@ReasonID", Reason);
                slParameters.Add("@AddedByID", AddedBy);
                slParameters.Add("@StatusID", Status);
                slParameters.Add("@CPValue", CPVal);
                slParameters.Add("@ApprovedByID", ApprovedBy);
                slParameters.Add("@ReceiptDate", RecptDate);
                slParameters.Add("@ReceivedByID", ReceivedBy);
                slParameters.Add("@StaffComments", StaffCommentsText);
                cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
            }  
        }

        /// <summary>
        /// This will add the points where appropriate to the character
        /// Must pass a CharacterID and CPValue
        /// </summary>
        public void AddPointsToCharacter(int CharacterID, double CPVal)
        {
            // Go get campaign character update parameters
            string stStoredProc = "uspGetCharacterUpdateParameters";
            DataTable dtUpdateParameters = new DataTable();
            string stCallingMethod = "cPoints.AddPointsToCharacter";
            SortedList slParameters = new SortedList();
            slParameters.Add("@CharacterID", CharacterID);
            //slParameters.Add("@CPValue", CPVal);
            try
            {
                dtUpdateParameters = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
                int iTemp = 0;
                bool btemp = true;
                double dbltemp = 0;
                foreach (DataRow drow in dtUpdateParameters.Rows)
                {
                    if (int.TryParse(drow["CampaignID"].ToString(), out iTemp))
                       _CampaignID = iTemp;
                    if (double.TryParse(drow["TotalCP"].ToString(), out dbltemp))
                        _TotalCP = dbltemp;
                    if (double.TryParse(drow["MaximumCPPerYear"].ToString(), out dbltemp))
                        _MaximumCPPerYear = dbltemp;
                    if (bool.TryParse(drow["AllowCPDonation"].ToString(), out btemp))
                        _AllowCPDonation = btemp;
                    if (double.TryParse(drow["EventCharacterCap"].ToString(), out dbltemp))
                        _EventCharacterCap = dbltemp;
                    if (double.TryParse(drow["AnnualCharacterCap"].ToString(), out dbltemp))
                        _AnnualCharacterCap = dbltemp;
                    if (double.TryParse(drow["TotalCharacterCap"].ToString(), out dbltemp))
                        _TotalCharacterCap = dbltemp;
                    if (int.TryParse(drow["EarliestCPApplicationYear"].ToString(), out iTemp))
                        _EarliestCPApplicationYear = iTemp;
                }
                if (_TotalCP >= _TotalCharacterCap)
                    _PLAuditStatus = 60;    // Character already at cap, bank it
                else
                {
                    // Apply it to the character not to exceed the cap
                    if (_TotalCP + CPVal <= _TotalCharacterCap)
                    {
                        _TotalCP = _TotalCP + CPVal;
                        _PLAuditStatus = 15;    // Status used and apply to character
                        stStoredProc = "uspUpdateCharacterCP";
                        slParameters.Clear();
                        slParameters.Add("@CharacterID", CharacterID);
                        slParameters.Add("@TotalCP", _TotalCP);
                        cUtilities.PerformNonQuery(stStoredProc, slParameters, "LARPortal", UserID.ToString());
                    }
                    else
                        _PLAuditStatus = 60;    // Would put character over cap, bank it
                }
            }
            catch
            {

            }
        }

    }
}