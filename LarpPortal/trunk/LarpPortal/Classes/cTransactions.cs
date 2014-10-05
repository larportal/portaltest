//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data;
//using LarpPortal.Classes;
//using System.Reflection;
//using System.Collections;
//using System.Configuration;
//using System.Data.SqlClient;

//namespace LarpPortal.Classes
//{
//    public class cTransactions
//    {
//        private int _CurrentUserID;
//        public int CurrentUserID { get; set; }
//        //Date Earned
//        private DateTime? _dateEarned;
//        public DateTime? DateEarned { get; set; }
//        //Earn Type (e.g. Event, Campaign Role, Item Donation, etc) & ID
//        private int _EarnTypeID;
//        public int EarnTypeID;
//        private string _EarnType;
//        public string EarnType { get; set; }
//        //Earn Description (the specific thing that earned the points)
//        private string _EarnDescription;
//        public string EarnDescription { get; set; }
//        //Source Value
//        private double _SourceValue;
//        public double SourceValue { get; set; }
//        //Points
//        private double _Points;
//        public double Points { get; set; }
//        //Status & ID
//        private int _StatusID;
//        public int StatusID { get; set; }
//        private string _Status;
//        public string Status { get; set; }
//        //Date Spent
//        private DateTime? _DateSpent;
//        public DateTime? DateSpent { get; set; }
//        //Earn At Campaign & ID
//        private int _EarnAtCampaignID;
//        public int EarnAtCampaignID { get; set; }
//        private string _EarnAtCampaign;
//        public string EarnAtCampaign { get; set; }
//        //Earn By Character & ID
//        private int _EarnByCharacterID;
//        public int EarnByCharacterID { get; set; }
//        private string _EarnByCharacter;
//        public string EarnByCharacter;
//        //Spend At Campaign & ID
//        private int _SpendAtCampaignID;
//        public int SpendAtCampaignID { get; set; }
//        private string _SpentAtCampaign;
//        public string SpendAtCampaign;
//        ////Spent On Character & ID
//        //private int _SpentAtCampaignID;
//        //public int SpentAtCampaignID { get; set; }
//        //private string _SpentAtCampaign;
//        //public string SpentAtCampaign { get; set; }
//        //Transfered To Player & ID
//        private int _TransferredToPlayerID;
//        public int TransferredToPlayerID { get; set; }
//        private string _TransferredToPlayer;
//        public string TransferredToPlayer { get; set; }
//        //Approved Date
//        private DateTime? _ApprovedDate;
//        public DateTime? ApprovedDate { get; set; }
//        //campaign list
//        public List<cUserCampaign> MyCampaigns = new List<cUserCampaign>();
//        //character list
//        public List<cUserCharacter> MyCharacters = new List<cUserCharacter>();

//        public int LoadMyTransactions(int UserToLoad)
//        {
//            int inumTransactionRecords = 0;



//            return inumTransactionRecords;
//        }

//        //=====================================
//        //private int _addedBy;
//        //private int _addedByID;
//        //private Boolean _allowCPDonation;
//        //private int _annualCharacterCap;
//        //private int _approvalLevelID;
//        //private int _approvalLevelType;
//        //private int _approvalLevelTypeID;
//        //private int _approvedByID;
//        //private Boolean _auditAdd;
//        //private Boolean _auditChange;
//        //private Boolean _auditDelete;
//        //private int _auditRequirementsID;
//        //private Boolean _backApply;
//        //private int _campaignCPOpportunityID;
//        //private int _campaignExchangeID;
//        //private int _campaignFromID;
//        //private int _campaignID;
//        //private string _campaignName;
//        //private int _campaignPlayerID;
//        //private int _campaignPlayerRoleID;
//        //private int _campaignToID;
//        //private double _cPAmount;
//        //private int _cPApprovedBy;
//        //private DateTime _cPApprovedDate;
//        //private DateTime _cPAssignmentDate;
//        //private double _cPCostPaid;
//        //private Boolean _cPEarnedForRole;
//        //private int _cPNotificationDeliveryPreference;
//        //private string _cPNotificationEmail;
//        //private double _cPQuantityEarnedPerEvent;
//        //private double _cPValue;
//        //private DateTime _dateAdded;
//        //private DateTime _dateApproved;
//        //private DateTime _dateChanged;
//        //private DateTime _dateDeleted;
//        //private DateTime _dateSubmitted;
//        //private Boolean _disableExchange;
//        //private DateTime _exchangeEndDate;
//        //private decimal _exchangeMultiplier;
//        //private DateTime _exchangeStartDate;
//        //private string _opportunityNotes;
//        //private Boolean _overrideAnnualCap;
//        //private Boolean _overrideEventCap;
//        //private int _playerCPAuditID;
//        //private int _reasonID;
//        //private DateTime _receiptDate;
//        //private int _receivedByID;
//        //private int _receivedFromCampaignID;
//        //private int _roleAlignmentID;
//        //private int _roleID;
//        //private int _sentToCampaignPlayerID;
//        //private string _staffNotes;
//        //private int _status;
//        //private int _statusID;
//        //private string _statusName;
//        //private string _statusType;
//        //private int _totalCharacterCap;
//        //private double _totalCP;
//        //private DateTime _transactionDate;
//        //public RecordStatuses RecordStatus { get; set; }

//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int addedBy
//        //{
//        //    get { return _addedBy; }
//        //    set { _addedBy = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int addedByID
//        //{
//        //    get { return _addedByID; }
//        //    set { _addedByID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean allowCPDonation
//        //{
//        //    get { return _allowCPDonation; }
//        //    set { _allowCPDonation = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int annualCharacterCap
//        //{
//        //    get { return _annualCharacterCap; }
//        //    set { _annualCharacterCap = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int approvalLevelID
//        //{
//        //    get { return _approvalLevelID; }
//        //    set { _approvalLevelID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int approvalLevelType
//        //{
//        //    get { return _approvalLevelType; }
//        //    set { _approvalLevelType = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int approvalLevelTypeID
//        //{
//        //    get { return _approvalLevelTypeID; }
//        //    set { _approvalLevelTypeID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int approvedByID
//        //{
//        //    get { return _approvedByID; }
//        //    set { _approvedByID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean auditAdd
//        //{
//        //    get { return _auditAdd; }
//        //    set { _auditAdd = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean auditChange
//        //{
//        //    get { return _auditChange; }
//        //    set { _auditChange = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean auditDelete
//        //{
//        //    get { return _auditDelete; }
//        //    set { _auditDelete = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int auditRequirementsID
//        //{
//        //    get { return _auditRequirementsID; }
//        //    set { _auditRequirementsID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean backApply
//        //{
//        //    get { return _backApply; }
//        //    set { _backApply = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignCPOpportunityID
//        //{
//        //    get { return _campaignCPOpportunityID; }
//        //    set { _campaignCPOpportunityID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignExchangeID
//        //{
//        //    get { return _campaignExchangeID; }
//        //    set { _campaignExchangeID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignFromID
//        //{
//        //    get { return _campaignFromID; }
//        //    set { _campaignFromID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignID
//        //{
//        //    get { return _campaignID; }
//        //    set { _campaignID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string campaignName
//        //{
//        //    get { return _campaignName; }
//        //    set { _campaignName = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignPlayerID
//        //{
//        //    get { return _campaignPlayerID; }
//        //    set { _campaignPlayerID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignPlayerRoleID
//        //{
//        //    get { return _campaignPlayerRoleID; }
//        //    set { _campaignPlayerRoleID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int campaignToID
//        //{
//        //    get { return _campaignToID; }
//        //    set { _campaignToID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public double cPAmount
//        //{
//        //    get { return _cPAmount; }
//        //    set { _cPAmount = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int cPApprovedBy
//        //{
//        //    get { return _cPApprovedBy; }
//        //    set { _cPApprovedBy = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime cPApprovedDate
//        //{
//        //    get { return _cPApprovedDate; }
//        //    set { _cPApprovedDate = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime cPAssignmentDate
//        //{
//        //    get { return _cPAssignmentDate; }
//        //    set { _cPAssignmentDate = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public double cPCostPaid
//        //{
//        //    get { return _cPCostPaid; }
//        //    set { _cPCostPaid = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean cPEarnedForRole
//        //{
//        //    get { return _cPEarnedForRole; }
//        //    set { _cPEarnedForRole = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int cPNotificationDeliveryPreference
//        //{
//        //    get { return _cPNotificationDeliveryPreference; }
//        //    set { _cPNotificationDeliveryPreference = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string cPNotificationEmail
//        //{
//        //    get { return _cPNotificationEmail; }
//        //    set { _cPNotificationEmail = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public double cPQuantityEarnedPerEvent
//        //{
//        //    get { return _cPQuantityEarnedPerEvent; }
//        //    set { _cPQuantityEarnedPerEvent = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public double cPValue
//        //{
//        //    get { return _cPValue; }
//        //    set { _cPValue = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime dateAdded
//        //{
//        //    get { return _dateAdded; }
//        //    set { _dateAdded = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime dateApproved
//        //{
//        //    get { return _dateApproved; }
//        //    set { _dateApproved = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime dateChanged
//        //{
//        //    get { return _dateChanged; }
//        //    set { _dateChanged = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime dateDeleted
//        //{
//        //    get { return _dateDeleted; }
//        //    set { _dateDeleted = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime dateSubmitted
//        //{
//        //    get { return _dateSubmitted; }
//        //    set { _dateSubmitted = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean disableExchange
//        //{
//        //    get { return _disableExchange; }
//        //    set { _disableExchange = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime exchangeEndDate
//        //{
//        //    get { return _exchangeEndDate; }
//        //    set { _exchangeEndDate = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public decimal exchangeMultiplier
//        //{
//        //    get { return _exchangeMultiplier; }
//        //    set { _exchangeMultiplier = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime exchangeStartDate
//        //{
//        //    get { return _exchangeStartDate; }
//        //    set { _exchangeStartDate = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string opportunityNotes
//        //{
//        //    get { return _opportunityNotes; }
//        //    set { _opportunityNotes = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean overrideAnnualCap
//        //{
//        //    get { return _overrideAnnualCap; }
//        //    set { _overrideAnnualCap = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public Boolean overrideEventCap
//        //{
//        //    get { return _overrideEventCap; }
//        //    set { _overrideEventCap = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int playerCPAuditID
//        //{
//        //    get { return _playerCPAuditID; }
//        //    set { _playerCPAuditID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int reasonID
//        //{
//        //    get { return _reasonID; }
//        //    set { _reasonID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime receiptDate
//        //{
//        //    get { return _receiptDate; }
//        //    set { _receiptDate = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int receivedByID
//        //{
//        //    get { return _receivedByID; }
//        //    set { _receivedByID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int receivedFromCampaignID
//        //{
//        //    get { return _receivedFromCampaignID; }
//        //    set { _receivedFromCampaignID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int roleAlignmentID
//        //{
//        //    get { return _roleAlignmentID; }
//        //    set { _roleAlignmentID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int roleID
//        //{
//        //    get { return _roleID; }
//        //    set { _roleID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int sentToCampaignPlayerID
//        //{
//        //    get { return _sentToCampaignPlayerID; }
//        //    set { _sentToCampaignPlayerID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string staffNotes
//        //{
//        //    get { return _staffNotes; }
//        //    set { _staffNotes = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int status
//        //{
//        //    get { return _status; }
//        //    set { _status = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int statusID
//        //{
//        //    get { return _statusID; }
//        //    set { _statusID = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string statusName
//        //{
//        //    get { return _statusName; }
//        //    set { _statusName = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public string statusType
//        //{
//        //    get { return _statusType; }
//        //    set { _statusType = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public int totalCharacterCap
//        //{
//        //    get { return _totalCharacterCap; }
//        //    set { _totalCharacterCap = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public double totalCP
//        //{
//        //    get { return _totalCP; }
//        //    set { _totalCP = value; }
//        //}
//        ///// <summary>
//        ///// Comment
//        ///// </summary>
//        //public DateTime transactionDate
//        //{
//        //    get { return _transactionDate; }
//        //    set { _transactionDate = value; }
//        //}
//        /// <summary>
//        /// This is called to retrieve a list of all point transactions.
//        /// </summary>
//        /// <returns>
//        /// Returns a data table of all point transactions.
//        /// </returns>
//        public DataTable GetMyTransactions(int UserID, int CampaignID, DateTime StartingDate, DateTime EndingDate, int CharacterID)
//        {
//            SortedList slStoredProcParameters = new SortedList();
//            // User ID is required
//            // Campaign ID of 0 will return all transactions for all campaigns
//            // Start Date of null will return all transactions from the beginning through the end date
//            // End Date of null will return all transactions from the start date to current
//            // Character ID of 0 will return all transactions for all characters
//            slStoredProcParameters.Add("@UserID", UserID);
//            slStoredProcParameters.Add("@CampaignID", CampaignID);
//            slStoredProcParameters.Add("@StartingDate", StartingDate);
//            slStoredProcParameters.Add("@EndingDate", EndingDate);
//            slStoredProcParameters.Add("@CharacterID", CharacterID);
//            DataTable dtMyPoints = new DataTable();
//            dtMyPoints = cUtilities.LoadDataTable("uspGetMyTransactions", slStoredProcParameters, "LARPortal", "rpierce", "GetMyTransactions");
//            return dtMyPoints;
//        }
//    }
//}