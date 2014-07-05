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
    public class cCampaignBase
    {

        private Int32 _CampaignID = -1;
        private string _CampaignName = "";
        private DateTime? _StartDate = null;
        private DateTime? _ProjectedEndDate = null;
        private DateTime? _ActualEndDate = null;
        private Int32 _GameSystemID = -1;
        private string _GameSystemName = "";
        private Int32 _TechLevelID = -1;
        private string _TechLevelName = "";
        private Int32 _StyleID = -1;
        private string _StyleDescription = "";
        private Int32 _MaxNumberOfGenres = 1;
        private string _PortalAccessType = "";
        private string _PortalAccessDescription = "";
        private Int32 _PrimaryUserID = -1;
        private string _WebPageDescription = "";
        private string _URL = "";
        private string _Logo = ""; // stored the file location for the logo image
        private string _CPNotificationEmail = "";
        private Int32 _CPNotificationPreferenceID = -1;
        private string _CPNotificationPreferenceDescription = "";
        private double _MembershipFee = 0.00;
        private string _MembershipFeeFrequency = "";
        private string _EmergencyEventContact = "";
        private string _InfoRequestEmail = "";
        private Boolean _PlayerApprovalRequired = false;
        private string _JoinRequestEmail = "";
        private Int32 _MinimumAge = -1;
        private Int32 _MinimumAgeWithSupervision = -1;
        private Int32 _MaximumCPPerYear = -1;
        private Boolean _AllowCPDonation = false;
        private Int32 _PELApprovalLevel = -1;
        private string _PELNotificationEMail = "";
        private Int32 _PelNotificationDeliveryPref = -1;
        private string _PELSubmissionURL = "";
        private Int32 _CharacterApprovalLevel = -1;
        private string _CharacterNotificationEMail = "";
        private Int32 _CharacterNotificationDeliveryPref = -1;
        private Int32 _CharacterHistoryApprovalLevel = -1;
        private string _CharacterHistoryNotificationEmail = "";
        private Int32 _CharacterHistoryNotificationDeliverPref = -1;
        private string _CharacterHistoryURL = "";
        private string _InfoSkillEMail = "";
        private Int32 _InfoSkillDeliveryPref = -1;
        private Int32 _InfoSkillApprovalLevel = -1;
        private string _InfoSkillURL = "";
        private string _ProductionSkillURL = "";
        private string _ProductionSkillEMail = "";
        private string _WebPageSelectionComments = "";
        private string _RulesURL = "";
        private string _RulesFile = "";
        private string _CharacterGeneratorURL = "";
        private string _ShareLocationUseNotes = "";
        private Int32 _WorldID = -1;
        private Int32 _StatusID = -1;
        private string _StatusDescription = "";
        private string _CancellationPolicy = "";
        private string _CrossCampaignPosting = "";
        private decimal _EventCharacterCPCap = 0;
        private decimal _AnnualCharacterCPCap = 0;
        private decimal _TotalCharacterCPCap = 0;
        private string _UserDefinedField1Value = "";
        private Boolean _UserDefinedField1Use = false;
        private string _UserDefinedField2Value = "";
        private Boolean _UserDefinedField2Use = false;
        private string _UserDefinedField3Value = "";
        private Boolean _UserDefinedField3Use = false;
        private string _UserDefinedField4Value = "";
        private Boolean _UserDefinedField4Use = false;
        private string _UserName = "";

        public Int32 CampaignID
        {
          get { return _CampaignID; }
          set { _CampaignID = value; }
        }
        public string CampaignName
        {
          get { return _CampaignName; }
          set { _CampaignName = value; }
        }
        public DateTime? StartDate
        {
          get { return _StartDate; }
          set { _StartDate = value; }
        }
        public DateTime? ProjectedEndDate
        {
          get { return _ProjectedEndDate; }
          set { _ProjectedEndDate = value; }
        }
        public DateTime? ActualEndDate
        {
          get { return _ActualEndDate; }
          set { _ActualEndDate = value; }
        }
        public Int32 GameSystemID
        {
            get { return _GameSystemID; }
            set { _GameSystemID = value; }
        }
        public string GameSystemName
        {
            get { return _GameSystemName; }
            set { _GameSystemName = value; }
        }
        public Int32 TechLevelID
        {
            get { return _TechLevelID; }
            set { _TechLevelID = value; }
        }
        public string TechLevelName
        {
            get { return _TechLevelName; }
            set { _TechLevelName = value; }
        }
        public Int32 StyleID
        {
            get { return _StyleID; }
            set { _StyleID = value; }
        }
        public string StyleDescription
        {
            get { return _StyleDescription; }
            set { _StyleDescription = value; }
        }
        public Int32 MaxNumberOfGenres
        {
            get { return _MaxNumberOfGenres; }
            set { _MaxNumberOfGenres = value; }
        }
        public string PortalAccessType
        {
            get { return _PortalAccessType; }
            set { _PortalAccessType = value; }
        }
        public string PortalAccessDescription
        {
            get { return _PortalAccessDescription; }
            set { _PortalAccessDescription = value; }
        }
        public Int32 PrimaryUserID
        {
            get { return _PrimaryUserID; }
            set { _PrimaryUserID = value; }
        }
        public string WebPageDescription
        {
            get { return _WebPageDescription; }
            set { _WebPageDescription = value; }
        }
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }
        public string CPNotificationEmail
        {
            get { return _CPNotificationEmail; }
            set { _CPNotificationEmail = value; }
        }
        public Int32 CPNotificationPreferenceID
        {
            get { return _CPNotificationPreferenceID; }
            set { _CPNotificationPreferenceID = value; }
        }
        public string CPNotificationPreferenceDescription
        {
            get { return _CPNotificationPreferenceDescription; }
            set { _CPNotificationPreferenceDescription = value; }
        }
        public double MembershipFee
        {
            get { return _MembershipFee; }
            set { _MembershipFee = value; }
        }
        public string MembershipFeeFrequency
        {
            get { return _MembershipFeeFrequency; }
            set { _MembershipFeeFrequency = value; }
        }
        public string EmergencyEventContact
        {
            get { return _EmergencyEventContact; }
            set { _EmergencyEventContact = value; }
        }
        public string InfoRequestEmail
        {
            get { return _InfoRequestEmail; }
            set { _InfoRequestEmail = value; }
        }
        public Boolean PlayerApprovalRequired
        {
            get { return _PlayerApprovalRequired; }
            set { _PlayerApprovalRequired = value; }
        }
        public string JoinRequestEmail
        {
            get { return _JoinRequestEmail; }
            set { _JoinRequestEmail = value; }
        }
        public Int32 MinimumAge
        {
            get { return _MinimumAge; }
            set { _MinimumAge = value; }
        }
        public Int32 MinimumAgeWithSupervision
        {
            get { return _MinimumAgeWithSupervision; }
            set { _MinimumAgeWithSupervision = value; }
        }
        public Int32 MaximumCPPerYear
        {
            get { return _MaximumCPPerYear; }
            set { _MaximumCPPerYear = value; }
        }
        public Boolean AllowCPDonation
        {
            get { return _AllowCPDonation; }
            set { _AllowCPDonation = value; }
        }
        public Int32 PELApprovalLevel
        {
            get { return _PELApprovalLevel; }
            set { _PELApprovalLevel = value; }
        }
        public string PELNotificationEMail
        {
            get { return _PELNotificationEMail; }
            set { _PELNotificationEMail = value; }
        }
        public Int32 PelNotificationDeliveryPref
        {
            get { return _PelNotificationDeliveryPref; }
            set { _PelNotificationDeliveryPref = value; }
        }
        public string PELSubmissionURL
        {
            get { return _PELSubmissionURL; }
            set { _PELSubmissionURL = value; }
        }
        public Int32 CharacterApprovalLevel
        {
            get { return _CharacterApprovalLevel; }
            set { _CharacterApprovalLevel = value; }
        }
        public string CharacterNotificationEMail
        {
            get { return _CharacterNotificationEMail; }
            set { _CharacterNotificationEMail = value; }
        }
        public Int32 CharacterNotificationDeliveryPref
        {
            get { return _CharacterNotificationDeliveryPref; }
            set { _CharacterNotificationDeliveryPref = value; }
        }
        public Int32 CharacterHistoryApprovalLevel
        {
            get { return _CharacterHistoryApprovalLevel; }
            set { _CharacterHistoryApprovalLevel = value; }
        }
        public string CharacterHistoryNotificationEmail
        {
            get { return _CharacterHistoryNotificationEmail; }
            set { _CharacterHistoryNotificationEmail = value; }
        }
        public Int32 CharacterHistoryNotificationDeliverPref
        {
            get { return _CharacterHistoryNotificationDeliverPref; }
            set { _CharacterHistoryNotificationDeliverPref = value; }
        }
        public string CharacterHistoryURL
        {
            get { return _CharacterHistoryURL; }
            set { _CharacterHistoryURL = value; }
        }
        public string InfoSkillEMail
        {
            get { return _InfoSkillEMail; }
            set { _InfoSkillEMail = value; }
        }
        public Int32 InfoSkillDeliveryPref
        {
            get { return _InfoSkillDeliveryPref; }
            set { _InfoSkillDeliveryPref = value; }
        }
        public Int32 InfoSkillApprovalLevel
        {
            get { return _InfoSkillApprovalLevel; }
            set { _InfoSkillApprovalLevel = value; }
        }
        public string InfoSkillURL
        {
            get { return _InfoSkillURL; }
            set { _InfoSkillURL = value; }
        }
        public string ProductionSkillURL
        {
            get { return _ProductionSkillURL; }
            set { _ProductionSkillURL = value; }
        }
        public string ProductionSkillEMail
        {
            get { return _ProductionSkillEMail; }
            set { _ProductionSkillEMail = value; }
        }
        public string WebPageSelectionComments
        {
            get { return _WebPageSelectionComments; }
            set { _WebPageSelectionComments = value; }
        }
        public string RulesURL
        {
            get { return _RulesURL; }
            set { _RulesURL = value; }
        }
        public string RulesFile
        {
            get { return _RulesFile; }
            set { _RulesFile = value; }
        }
        public string CharacterGeneratorURL
        {
            get { return _CharacterGeneratorURL; }
            set { _CharacterGeneratorURL = value; }
        }
        public string ShareLocationUseNotes
        {
            get { return _ShareLocationUseNotes; }
            set { _ShareLocationUseNotes = value; }
        }
        public Int32 WorldID
        {
            get { return _WorldID; }
            set { _WorldID = value; }
        }
        public Int32 StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }
        public string StatusDescription
        {
            get { return _StatusDescription; }
            set { _StatusDescription = value; }
        }
        public string CancellationPolicy
        {
            get { return _CancellationPolicy; }
            set { _CancellationPolicy = value; }
        }
        public string CrossCampaignPosting
        {
            get { return _CrossCampaignPosting; }
            set { _CrossCampaignPosting = value; }
        }
        public decimal EventCharacterCPCap
        {
            get { return _EventCharacterCPCap; }
            set { _EventCharacterCPCap = value; }
        }
        public decimal AnnualCharacterCPCap
        {
            get { return _AnnualCharacterCPCap; }
            set { _AnnualCharacterCPCap = value; }
        }
        public decimal TotalCharacterCPCap
        {
            get { return _TotalCharacterCPCap; }
            set { _TotalCharacterCPCap = value; }
        }
        public string UserDefinedField1Value
        {
            get { return _UserDefinedField1Value; }
            set { _UserDefinedField1Value = value; }
        }
        public Boolean UserDefinedField1Use
        {
            get { return _UserDefinedField1Use; }
            set { _UserDefinedField1Use = value; }
        }
        public string UserDefinedField2Value
        {
            get { return _UserDefinedField2Value; }
            set { _UserDefinedField2Value = value; }
        }
        public Boolean UserDefinedField2Use
        {
            get { return _UserDefinedField2Use; }
            set { _UserDefinedField2Use = value; }
        }
        public string UserDefinedField3Value
        {
            get { return _UserDefinedField3Value; }
            set { _UserDefinedField3Value = value; }
        }
        public Boolean UserDefinedField3Use
        {
            get { return _UserDefinedField3Use; }
            set { _UserDefinedField3Use = value; }
        }
        public string UserDefinedField4Value
        {
            get { return _UserDefinedField4Value; }
            set { _UserDefinedField4Value = value; }
        }
        public Boolean UserDefinedField4Use
        {
            get { return _UserDefinedField4Use; }
            set { _UserDefinedField4Use = value; }
        }




        private cCampaignBase()
        {

        }

        public cCampaignBase(Int32 intCampaignID, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserName = strUserName;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            

            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@intCampaignID", _CampaignID);

                DataTable ldt = cUtilities.LoadDataTable("uspGetCampaign", slParams, "DefaultSQLConnection", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                   // _someValue = ldt.Rows[0]["SomeValue"].ToString();
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
               // slParams.Add("@Parmeter1", strParameter1)


                blnReturn = true;
            }
            catch(Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                blnReturn = false;

            }

            return blnReturn;
        }
       

    }


}