﻿using System;
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
        private Int32 _UserID = -1;
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
        private Int32 _PrimaryOwnerID = -1;
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
        private string _UserDefinedField5Value = "";
        private Boolean _UserDefinedField5Use = false;
        private string _UserName = "";
        private Int32 _ProjectedNumberOfEvents = 0;
        private Int32 _ActualNumberOfEvents = 0;
        private Int32 _MarketingCampaignSize = 0;
        private Boolean _UseCampaignCharacters = false;
        private Int32 _CampaignAddressID = -1;
        private string _CSSFile = "";
        private string _Comments = "";


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
        public Int32 PrimaryOwnerID
        {
            get { return _PrimaryOwnerID; }
            set { _PrimaryOwnerID = value; }
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
        public string UserDefinedField5Value
        {
            get { return _UserDefinedField5Value; }
            set { _UserDefinedField5Value = value; }
        }
        public Boolean UserDefinedField5Use
        {
            get { return _UserDefinedField5Use; }
            set { _UserDefinedField5Use = value; }
        }
        public Int32 ProjectedNumberOfEvents
        {
            get { return _ProjectedNumberOfEvents; }
            set {_ProjectedNumberOfEvents = value;}
        }
        public Int32 ActualNumberOfEvents
        {
            get { return _ActualNumberOfEvents; }
            set { _ActualNumberOfEvents = value; }
        }
        public Int32 MarketingCampaignSize
        {
            get { return _MarketingCampaignSize; }
            set { _MarketingCampaignSize = value; }
        }
        public Boolean UseCampaignCharacters
        {
            get { return _UseCampaignCharacters; }
            set { _UseCampaignCharacters = value; }
        }
        public Int32 CampaignAddressID
        {
            get { return _CampaignAddressID; }
            set { _CampaignAddressID = value; }
        }
        public string CSSFile
        {
            get { return _CSSFile; }
            set { _CSSFile = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }


        private cCampaignBase()
        {

        }

        public cCampaignBase(Int32 intCampaignID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserName = strUserName;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2
            

            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@CampaignID", _CampaignID);

                DataTable ldt = cUtilities.LoadDataTable("uspGetCampaignByCampaignID", slParams, "DefaultSQLConnection", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    _ActualEndDate = Convert.ToDateTime(ldt.Rows[0]["ActualEndDate"].ToString().Trim());
                    _ActualNumberOfEvents = ldt.Rows[0]["ActualNumberOfEvents"].ToString().Trim().ToInt32();
                    _AllowCPDonation = ldt.Rows[0]["AllowCPDonation"].ToString().Trim().ToBoolean();
                    _AnnualCharacterCPCap = ldt.Rows[0]["AnnualCharacterCap"].ToString().Trim().ToInt32();
                    _CampaignAddressID = ldt.Rows[0]["CampaignAddress"].ToString().Trim().ToInt32();
                    _CampaignName = ldt.Rows[0]["CampaignName"].ToString();
                    _CancellationPolicy = ldt.Rows[0]["CancellationPolicy"].ToString().Trim();
                    _CharacterApprovalLevel = ldt.Rows[0]["CharacterApprovalLevel"].ToString().Trim().ToInt32();
                    _CharacterGeneratorURL = ldt.Rows[0]["CharacterGeneratorURL"].ToString().Trim();
                    _CharacterHistoryApprovalLevel = ldt.Rows[0]["CharacterHistoryApprovalLevel"].ToString().Trim().ToInt32();
                    _CharacterHistoryNotificationDeliverPref = ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString().Trim().ToInt32();
                    _CharacterHistoryNotificationEmail = ldt.Rows[0]["CharacterHistoryNotificationEmail"].ToString().Trim();
                    _CharacterHistoryURL = ldt.Rows[0]["CharacterHistoryURL"].ToString().Trim();
                    _CharacterNotificationDeliveryPref = ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString().Trim().ToInt32();
                    _CharacterNotificationEMail = ldt.Rows[0]["CharacterNotificationEmail"].ToString().Trim();
                    _CharacterHistoryURL = ldt.Rows[0]["CharacterHistoryURL"].ToString().Trim();
                    _CharacterNotificationDeliveryPref = ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString().Trim().ToInt32();
                    _CharacterNotificationEMail = ldt.Rows[0]["CharacterNotificationEmail"].ToString().Trim();
                    _Comments = ldt.Rows[0]["Comments"].ToString().Trim();
                    _CPNotificationEmail = ldt.Rows[0]["CPNotificationEmail"].ToString().Trim();
                    _CPNotificationPreferenceDescription = ldt.Rows[0]["CPNotificationDeliveryPreference"].ToString().Trim();
                    _CPNotificationPreferenceID = -1; //JRV TODO
                    _CrossCampaignPosting = ldt.Rows[0]["CrossCampaignPosting"].ToString().Trim();
                    _CSSFile = ldt.Rows[0]["CampaignCSSFile"].ToString().Trim();
                    _EmergencyEventContact = ldt.Rows[0]["EmergencyEventContactInfo"].ToString().Trim();
                    _EventCharacterCPCap = ldt.Rows[0]["EventCharacterCap"].ToString().Trim().ToInt32();
                    _GameSystemID = ldt.Rows[0]["GameSystemID"].ToString().Trim().ToInt32();
                    _GameSystemName = "GetFromGameSystemClass"; //JRV TODO
                    _InfoRequestEmail = ldt.Rows[0]["InfoRequestEmail"].ToString().Trim();
                    _InfoSkillApprovalLevel = ldt.Rows[0]["InfoSkillApprovalLevel"].ToString().Trim().ToInt32();
                    _InfoSkillDeliveryPref = ldt.Rows[0]["InfoSkillDeliveryPreference"].ToString().Trim().ToInt32();
                    _InfoSkillEMail = ldt.Rows[0]["InfoSkillEmail"].ToString().Trim();
                    _InfoSkillURL = ldt.Rows[0]["InfoSkillURL"].ToString().Trim();
                    _JoinRequestEmail = ldt.Rows[0]["JoinRequestEmail"].ToString().Trim();
                    _Logo = ldt.Rows[0]["CampaignLogo"].ToString().Trim();
                    _MarketingCampaignSize = ldt.Rows[0]["MarketingCampaignSize"].ToString().Trim().ToInt32();
                    _MaximumCPPerYear = ldt.Rows[0]["MaximumCPPerYear"].ToString().Trim().ToInt32();
                    _MaxNumberOfGenres = 0; //JRV ToDO
                    _MembershipFee = Convert.ToDouble(ldt.Rows[0]["MembershipFee"].ToString().Trim());
                    _MembershipFeeFrequency = ldt.Rows[0]["MembershipFeeFrequency"].ToString().Trim();
                    _MinimumAge = ldt.Rows[0]["MinimumAge"].ToString().Trim().ToInt32();
                    _MinimumAgeWithSupervision = ldt.Rows[0]["MinimumAgeWithSupervision"].ToString().Trim().ToInt32();
                    _PELApprovalLevel = ldt.Rows[0]["PELApprovalLevel"].ToString().Trim().ToInt32();
                    _PelNotificationDeliveryPref = ldt.Rows[0]["PELNotificationDeliveryPreference"].ToString().Trim().ToInt32();
                    _PELNotificationEMail = ldt.Rows[0]["PELNotificationEmail"].ToString().Trim();
                    _PELSubmissionURL = ldt.Rows[0]["PELSubmissionURL"].ToString().Trim();
                    _PlayerApprovalRequired = ldt.Rows[0]["PlayerApprovalRequired"].ToString().Trim().ToBoolean();
                    _PortalAccessDescription = ""; //JRV TODO
                    _PortalAccessType = ldt.Rows[0]["PortalAccessType"].ToString().Trim();
                    _PrimaryOwnerID = ldt.Rows[0]["PrimaryOwnerID"].ToString().Trim().ToInt32();
                    _ProductionSkillEMail = ldt.Rows[0]["ProductionSkillEmail"].ToString().Trim();
                    _ProductionSkillURL = ldt.Rows[0]["ProductionSkillURL"].ToString().Trim();
                    _ProjectedEndDate = Convert.ToDateTime( ldt.Rows[0]["ProductionSkillURL"].ToString().Trim());
                    _ProjectedNumberOfEvents = ldt.Rows[0]["ProjectedNumberOfEvents"].ToString().Trim().ToInt32();
                    _RulesFile = ldt.Rows[0]["CampaignRulesFile"].ToString().Trim();
                    _RulesURL = ldt.Rows[0]["CampaignRulesURL"].ToString().Trim();
                    _ShareLocationUseNotes = ldt.Rows[0]["ShareLocationUseNotes"].ToString().Trim();
                    _StartDate = Convert.ToDateTime(ldt.Rows[0]["CampaignStartDate"].ToString().Trim());
                    _StatusDescription = ""; //JRV TODO
                    _StatusID = ldt.Rows[0]["StatusID"].ToString().Trim().ToInt32();
                    _StyleDescription = "";
                    _StyleID = ldt.Rows[0]["StyleID"].ToString().Trim().ToInt32();
                    _TechLevelID = ldt.Rows[0]["TechLevelID"].ToString().Trim().ToInt32();
                    _TechLevelName = "";// JRV TODO
                    _TotalCharacterCPCap = ldt.Rows[0]["TotalCharacterCap"].ToString().Trim().ToInt32();
                    _URL = ldt.Rows[0]["CampaignURL"].ToString().Trim();
                    _UseCampaignCharacters = ldt.Rows[0]["UseCampaignCharacters"].ToString().Trim().ToBoolean();
                    _UserDefinedField1Use = ldt.Rows[0]["UseUserDefinedField1"].ToString().Trim().ToBoolean();
                    _UserDefinedField1Value = ldt.Rows[0]["UserDefinedField1"].ToString().Trim();
                    _UserDefinedField2Use = ldt.Rows[0]["UseUserDefinedField2"].ToString().Trim().ToBoolean();
                    _UserDefinedField2Value = ldt.Rows[0]["UserDefinedField2"].ToString().Trim();
                    _UserDefinedField3Use = ldt.Rows[0]["UseUserDefinedField3"].ToString().Trim().ToBoolean();
                    _UserDefinedField3Value = ldt.Rows[0]["UserDefinedField3"].ToString().Trim();
                    _UserDefinedField4Use = ldt.Rows[0]["UseUserDefinedField4"].ToString().Trim().ToBoolean();
                    _UserDefinedField4Value = ldt.Rows[0]["UserDefinedField4"].ToString().Trim();
                    _UserDefinedField5Use = ldt.Rows[0]["UseUserDefinedField5"].ToString().Trim().ToBoolean();
                    _UserDefinedField5Value = ldt.Rows[0]["UserDefinedField5"].ToString().Trim();
                    _WebPageDescription = ldt.Rows[0]["CampaignWebPageDescription"].ToString().Trim();
                    _WebPageSelectionComments = ldt.Rows[0]["CampaignWebPageSelectionComments"].ToString().Trim();
                    _WorldID = -1; //JRV TODO
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
                slParams.Add("@UserID", -_UserID);
                slParams.Add("@CampaignID", _CampaignID);
                slParams.Add("@CampaignName", _CampaignName);
                slParams.Add("@CampaignStartDate", _StartDate);
                slParams.Add("@ProjectedEndDate", _ProjectedEndDate);
                slParams.Add("@ActualEndDate", _ActualEndDate);
                slParams.Add("@ProjectedNumberOfEvents", _ProjectedNumberOfEvents);
                slParams.Add("@ActualNumberOfEvents", _ActualNumberOfEvents);
                slParams.Add("@GameSystemID", _GameSystemID);
                slParams.Add("@MarketingCampaignSize", _MarketingCampaignSize);
                slParams.Add("@TechLevelID", _TechLevelID);
                slParams.Add("@StyleID", _StyleID);
                slParams.Add("@UseCampaignCharacters", _UseCampaignCharacters);
                slParams.Add("@PortalAccessType", _PortalAccessType);
                slParams.Add("@PrimaryOwnerID", _PrimaryOwnerID);
                slParams.Add("@CampaignAddress", _CampaignAddressID);
                slParams.Add("@CampaignWebPageDescription", _WebPageDescription);
                slParams.Add("@CampaignURL", _URL);
                slParams.Add("@CampaignLogo", _Logo);
                slParams.Add("@CampaignCSSFile", _CSSFile);
                slParams.Add("@CPNotificationEmail", _CPNotificationEmail);
                slParams.Add("@CPNotificationDeliveryPreference", _CPNotificationPreferenceID);
                slParams.Add("@MembershipFee", _MembershipFee);
                slParams.Add("@MembershipFeeFrequency", _MembershipFeeFrequency);
                slParams.Add("@EmergencyEventContactInfo", _EmergencyEventContact);
                slParams.Add("@InfoRequestEmail", _InfoRequestEmail);
                slParams.Add("@PlayerApprovalRequired", _PlayerApprovalRequired);
                slParams.Add("@JoinRequestEmail", _JoinRequestEmail);
                slParams.Add("@MinimumAge", _MinimumAge);
                slParams.Add("@MinimumAgeWithSupervision", _MinimumAgeWithSupervision);
                slParams.Add("@MaximumCPPerYear", _MaximumCPPerYear);
                slParams.Add("@AllowCPDonation", _AllowCPDonation);
                slParams.Add("@PELApprovalLevel", _PELApprovalLevel);
                slParams.Add("@PELNotificationEmail", _PELNotificationEMail);
                slParams.Add("@PELNotificationDeliveryPreference", _PelNotificationDeliveryPref);
                slParams.Add("@PELSubmissionURL", _PELSubmissionURL);
                slParams.Add("@CharacterApprovalLevel", _CharacterApprovalLevel);
                slParams.Add("@CharacterNotificationEmail", _CharacterNotificationEMail);
                slParams.Add("@CharacterNotificationDeliveryPreference", _CharacterNotificationDeliveryPref);
                slParams.Add("@CharacterHistoryApprovalLevel", _CharacterHistoryApprovalLevel);
                slParams.Add("@CharacterHistoryNotificationEmail", _CharacterHistoryNotificationEmail);
                slParams.Add("@CharacterHistoryNotificationDeliveryPreference", _CharacterHistoryNotificationDeliverPref);
                slParams.Add("@CharacterHistoryURL", _CharacterHistoryURL);
                slParams.Add("@InfoSkillEmail", _InfoSkillEMail);
                slParams.Add("@InfoSkillDeliveryPreference", _InfoSkillDeliveryPref);
                slParams.Add("@InfoSkillApprovalLevel", _InfoSkillApprovalLevel);
                slParams.Add("@InfoSkillURL", _InfoSkillURL);
                slParams.Add("@ProductionSkillEmail", _ProductionSkillEMail);
                slParams.Add("@ProductionSkillURL", _ProductionSkillURL);
                slParams.Add("@CampaignWebPageSelectionComments", _WebPageSelectionComments);
                slParams.Add("@CampaignRulesURL", _RulesURL);
                slParams.Add("@CampaignRulesFile", _RulesFile);
                slParams.Add("@CharacterGeneratorURL", _CharacterGeneratorURL);
                slParams.Add("@ShareLocationUseNotes", _ShareLocationUseNotes);
                slParams.Add("@StatusID", _StatusID);
                slParams.Add("@CancellationPolicy", _CancellationPolicy);
                slParams.Add("@CrossCampaignPosting", _CrossCampaignPosting);
                slParams.Add("@EventCharacterCap", _EventCharacterCPCap);
                slParams.Add("@AnnualCharacterCap", _AnnualCharacterCPCap);
                slParams.Add("@TotalCharacterCap", _TotalCharacterCPCap);
                slParams.Add("@UserDefinedField1", _UserDefinedField1Value);
                slParams.Add("@UseUserDefinedField1", _UserDefinedField1Use);
                slParams.Add("@UserDefinedField2", _UserDefinedField2Value);
                slParams.Add("@UseUserDefinedField2", _UserDefinedField2Use);
                slParams.Add("@UserDefinedField3", _UserDefinedField3Value);
                slParams.Add("@UseUserDefinedField3", _UserDefinedField3Use);
                slParams.Add("@UserDefinedField4", _UserDefinedField4Value);
                slParams.Add("@UseUserDefinedField4", _UserDefinedField4Use);
                slParams.Add("@UserDefinedField5", _UserDefinedField5Value);
                slParams.Add("@UseUserDefinedField5", _UserDefinedField5Use);
                slParams.Add("@Comments", _Comments);
                cUtilities.PerformNonQuery("uspInsUpdCMCampaigns",slParams,"LARPortal",_UserName);
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