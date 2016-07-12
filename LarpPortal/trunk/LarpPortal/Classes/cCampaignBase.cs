﻿using System;
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
        private Int32 _TechLevelID = -1;    //TODO-Rick-1 Redo - Should not be in Campaigns as it is multiply defined for any given campaign
        private string _TechLevelName = "";
        private Int32 _StyleID = -1;
        private string _StyleDescription = "";
        private Int32 _MaxNumberOfGenres = 1;
        private Int32 _MaxNumberOfPeriods = 1;
        private string _PortalAccessType = "";
        private string _PortalAccessDescription = "";
        private Int32 _PrimaryOwnerID = -1;
        private string _PrimaryOwnerName = "";
        private string _WebPageDescription = "";
        private string _URL = "";
        private string _Logo = ""; // stored the file location for the logo image
        private int _LogoHeight = 0;
        private int _LogoWidth = 0;
        private string _CPNotificationEmail = "";
        private Int32 _CPNotificationPreferenceID = -1;
        private string _CPNotificationPreferenceDescription = "";
        private double _MembershipFee = 0.00;
        private string _MembershipFeeFrequency = "";
        private string _EmergencyEventContact = "";
        private string _InfoRequestEmail = "";
        private Boolean _PlayerApprovalRequired = false;
        private Boolean _NPCApprovalRequired = false;
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
        private bool _AllowCharacterRebuild = true;
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
        private string _CampaignStyle = "";
        private string _GenreList = "";
        private string _PeriodList = "";
        private string _TechLevelList = "";
        private string _MarketingLocation = "";
        private DateTime? _NextEventDate = null;
        private int _CampaignRoleID = 0;
        private int _RoleID = 0;
        private string _RoleDescription = "";
        private Boolean _AutoApprove;
        private DateTime? _DateChanged = null;

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
        public Int32 MaxNumberOfPeriods
        {
            get { return _MaxNumberOfPeriods; }
            set { _MaxNumberOfPeriods = value; }
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
        public string PrimaryOwnerName
        {
            get { return _PrimaryOwnerName; }
            set { _PrimaryOwnerName = value; }
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
        public int LogoHeight
        {
            get { return _LogoHeight; }
            set { _LogoHeight = value; }
        }
        public int LogoWidth
        {
            get { return _LogoWidth; }
            set { _LogoWidth = value; }
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
        public Boolean NPCApprovalRequired
        {
            get { return _NPCApprovalRequired; }
            set { _NPCApprovalRequired = value; }
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
        public bool AllowCharacterRebuild
        {
            get { return _AllowCharacterRebuild; }
            set { _AllowCharacterRebuild = value; }
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
            set { _ProjectedNumberOfEvents = value; }
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
        public string CampaignStyle
        {
            get { return _CampaignStyle; }
            set { _CampaignStyle = value; }
        }
        public string GenreList
        {
            get { return _GenreList;  }
            set { _GenreList = value; }
        }
        public string PeriodList
        {
            get { return _PeriodList; }
            set { _PeriodList = value; }
        }
        public string TechLevelList
        {
            get { return _TechLevelList; }
            set { _TechLevelList = value; }
        }

        public string CampaignSizeRange { get; set; }

        public string MarketingLocation
        {
            get { return _MarketingLocation; }
            set { _MarketingLocation = value; }
        }

        public DateTime? NextEventDate
        {
            get { return _NextEventDate; }
            set { _NextEventDate = value; }
        }

        public DateTime? DateChanged
        {
            get { return _DateChanged; }
            set { _DateChanged = value; }
        }

        private int CampaignRoleID
        {
            get { return _CampaignRoleID; }
            set { _CampaignRoleID = value; }
        }

        private int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        private string RoleDescription
        {
            get { return _RoleDescription; }
            set { _RoleDescription = value; }
        }

        private Boolean AutoApprove
        {
            get { return _AutoApprove; }
            set { _AutoApprove = value; }
        }

        private cCampaignBase()
        {

        }

        public cCampaignBase(Int32 intCampaignID, string strUserName, Int32 intUserID)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();   // this is where we use refelection to store the name of the method and class to use it to report errors
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            _UserName = strUserName;
            _CampaignID = intCampaignID;
            _UserID = intUserID;
            //so lets say we wanted to load data into this class from a sql query called uspGetSomeData thats take two paraeters @Parameter1 and @Parameter2

            try
            {
                SortedList slParams = new SortedList(); // I use a sortedlist  wich is a C# hash table to store the paramter and value
                slParams.Add("@CampaignID", _CampaignID);

                DataTable ldt = cUtilities.LoadDataTable("uspGetCampaignByCampaignID", slParams, "LARPortal", strUserName, lsRoutineName);
                if (ldt.Rows.Count > 0)
                {
                    DateTime dtTemp;
                    bool bTemp;
                    int iTemp;
                    double dTemp;
                    if (DateTime.TryParse(ldt.Rows[0]["ActualEndDate"].ToString(), out dtTemp))
                        _ActualEndDate = dtTemp;
                    if (DateTime.TryParse(ldt.Rows[0]["NextEventDate"].ToString(), out dtTemp))
                        _NextEventDate = dtTemp;
                    if (int.TryParse(ldt.Rows[0]["ActualNumberOfEvents"].ToString(), out iTemp))
                        _ActualNumberOfEvents = iTemp;
                    if (bool.TryParse(ldt.Rows[0]["AllowCPDonation"].ToString(), out bTemp))
                        _AllowCPDonation = bTemp;
                    if (int.TryParse(ldt.Rows[0]["AnnualCharacterCap"].ToString(), out iTemp))
                        _AnnualCharacterCPCap = iTemp;
                    if (int.TryParse(ldt.Rows[0]["CampaignAddress"].ToString(), out iTemp))
                        _CampaignAddressID = iTemp;
                    _CampaignName = ldt.Rows[0]["CampaignName"].ToString();
                    _CancellationPolicy = ldt.Rows[0]["CancellationPolicy"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["CharacterApprovalLevel"].ToString(), out iTemp))
                        _CharacterApprovalLevel = iTemp;
                    _CharacterGeneratorURL = ldt.Rows[0]["CharacterGeneratorURL"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["CharacterHistoryApprovalLevel"].ToString(), out iTemp))
                        _CharacterHistoryApprovalLevel = iTemp;
                    if (int.TryParse(ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString(), out iTemp))
                        _CharacterHistoryNotificationDeliverPref = iTemp;
                    _CharacterHistoryNotificationEmail = ldt.Rows[0]["CharacterHistoryNotificationEmail"].ToString().Trim();
                    _CharacterHistoryURL = ldt.Rows[0]["CharacterHistoryURL"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString(), out iTemp))
                        _CharacterNotificationDeliveryPref = iTemp;
                    _CharacterNotificationEMail = ldt.Rows[0]["CharacterNotificationEmail"].ToString().Trim();
                    _CharacterHistoryURL = ldt.Rows[0]["CharacterHistoryURL"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["CharacterHistoryNotificationDeliveryPreference"].ToString(), out iTemp))
                        _CharacterNotificationDeliveryPref = iTemp;
                    _CharacterNotificationEMail = ldt.Rows[0]["CharacterNotificationEmail"].ToString().Trim();
                    _Comments = ldt.Rows[0]["Comments"].ToString().Trim();
                    _CPNotificationEmail = ldt.Rows[0]["CPNotificationEmail"].ToString().Trim();
                    _CPNotificationPreferenceDescription = ldt.Rows[0]["CPNotificationDeliveryPreference"].ToString().Trim();
                    _CPNotificationPreferenceID = -1; //TODO-Rick-2 Campaign Notification Preference definition
                    _CrossCampaignPosting = ldt.Rows[0]["CrossCampaignPosting"].ToString().Trim();
                    _CSSFile = ldt.Rows[0]["CampaignCSSFile"].ToString().Trim();
                    _EmergencyEventContact = ldt.Rows[0]["EmergencyEventContactInfo"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["EventCharacterCap"].ToString(), out iTemp))
                        _EventCharacterCPCap = iTemp;
                    if (int.TryParse(ldt.Rows[0]["GameSystemID"].ToString(), out iTemp))
                        _GameSystemID = iTemp;
                    _GameSystemName = ldt.Rows[0]["GameSystemName"].ToString().Trim();
                    _InfoRequestEmail = ldt.Rows[0]["InfoRequestEmail"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["InfoSkillApprovalLevel"].ToString(), out iTemp))
                        _InfoSkillApprovalLevel = iTemp;
                    if (int.TryParse(ldt.Rows[0]["InfoSkillDeliveryPreference"].ToString(), out iTemp))
                        _InfoSkillDeliveryPref = iTemp;
                    _InfoSkillEMail = ldt.Rows[0]["InfoSkillEmail"].ToString().Trim();
                    _InfoSkillURL = ldt.Rows[0]["InfoSkillURL"].ToString().Trim();
                    _JoinRequestEmail = ldt.Rows[0]["JoinRequestEmail"].ToString().Trim();
                    _Logo = ldt.Rows[0]["CampaignLogo"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["CampaignLogoHeight"].ToString(), out iTemp))
                        _LogoHeight = iTemp;
                    if (int.TryParse(ldt.Rows[0]["CampaignLogoWidth"].ToString(), out iTemp))
                        _LogoWidth = iTemp;
                    if (int.TryParse(ldt.Rows[0]["MarketingCampaignSize"].ToString(), out iTemp))
                        _MarketingCampaignSize = iTemp;
                    CampaignSizeRange = ldt.Rows[0]["CampaignSizeRange"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["MaximumCPPerYear"].ToString(), out iTemp))
                        _MaximumCPPerYear = iTemp;
                    _MaxNumberOfGenres = 4; //TODO-Rick-4 Limit campaigns to number of genres parameter
                    _MaxNumberOfPeriods = 4; //TODO-Rick-4 Limit campaigns to number of periods parameter
                    if (double.TryParse(ldt.Rows[0]["MembershipFee"].ToString(), out dTemp))
                        _MembershipFee = dTemp;
                    _MembershipFeeFrequency = ldt.Rows[0]["MembershipFeeFrequency"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["MinimumAge"].ToString(), out iTemp))
                        _MinimumAge = iTemp;
                    if (int.TryParse(ldt.Rows[0]["MinimumAgeWithSupervision"].ToString(), out iTemp))
                        _MinimumAgeWithSupervision = iTemp;
                    if (int.TryParse(ldt.Rows[0]["PELApprovalLevel"].ToString(), out iTemp))
                        _PELApprovalLevel = iTemp;
                    if (int.TryParse(ldt.Rows[0]["PELNotificationDeliveryPreference"].ToString(), out iTemp))
                        _PelNotificationDeliveryPref = iTemp;
                    _PELNotificationEMail = ldt.Rows[0]["PELNotificationEmail"].ToString().Trim();
                    _PELSubmissionURL = ldt.Rows[0]["PELSubmissionURL"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["PlayerApprovalRequired"].ToString(), out bTemp))
                        _PlayerApprovalRequired = bTemp;
                    if (bool.TryParse(ldt.Rows[0]["NPCApprovalRequired"].ToString(), out bTemp))
                        _NPCApprovalRequired = bTemp;
                    _PortalAccessDescription = ""; //TODO-Rick-4 Get portal access description from table - LOW priority because we "know" what it is and don't currently display it
                    _PortalAccessType = ldt.Rows[0]["PortalAccessType"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["PrimaryOwnerID"].ToString(), out iTemp))
                        _PrimaryOwnerID = iTemp;
                    _PrimaryOwnerName = ldt.Rows[0]["PrimaryOwnerName"].ToString().Trim();
                    _ProductionSkillEMail = ldt.Rows[0]["ProductionSkillEmail"].ToString().Trim();
                    _ProductionSkillURL = ldt.Rows[0]["ProductionSkillURL"].ToString().Trim();
                    if (DateTime.TryParse(ldt.Rows[0]["ProjectedEndDate"].ToString(), out dtTemp))
                        _ProjectedEndDate = dtTemp;
                    if (int.TryParse(ldt.Rows[0]["ProjectedNumberOfEvents"].ToString(), out iTemp))
                        _ProjectedNumberOfEvents = iTemp;
                    _RulesFile = ldt.Rows[0]["CampaignRulesFile"].ToString().Trim();
                    _RulesURL = ldt.Rows[0]["CampaignRulesURL"].ToString().Trim();
                    _ShareLocationUseNotes = ldt.Rows[0]["ShareLocationUseNotes"].ToString().Trim();
                    if (DateTime.TryParse(ldt.Rows[0]["CampaignStartDate"].ToString(), out dtTemp))
                        _StartDate = dtTemp;
                    if (DateTime.TryParse(ldt.Rows[0]["DateChanged"].ToString(), out dtTemp))
                        _DateChanged = dtTemp;
                    _StatusDescription = ldt.Rows[0]["StatusDescription"].ToString().Trim(); 
                    if (int.TryParse(ldt.Rows[0]["StatusID"].ToString(), out iTemp))
                        _StatusID = iTemp;
                    _StyleDescription = ldt.Rows[0]["StyleName"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["StyleID"].ToString(), out iTemp))
                        _StyleID = iTemp;
                    if (bool.TryParse(ldt.Rows[0]["AllowCharacterRebuild"].ToString(), out bTemp))      //  JLB 7/28/2015
                        _AllowCharacterRebuild = bTemp;
                    _CampaignStyle = ldt.Rows[0]["StyleName"].ToString().Trim();
                    if (int.TryParse(ldt.Rows[0]["TechLevelID"].ToString(), out iTemp))
                        _TechLevelID = iTemp;
                    _TechLevelName = "";
                    if (int.TryParse(ldt.Rows[0]["TotalCharacterCap"].ToString(), out iTemp))
                        _TotalCharacterCPCap = iTemp;
                    _URL = ldt.Rows[0]["CampaignURL"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["UseCampaignCharacters"].ToString(), out bTemp))
                        _UseCampaignCharacters = bTemp;
                    if (bool.TryParse(ldt.Rows[0]["UseUserDefinedField1"].ToString(), out bTemp))
                        _UserDefinedField1Use = bTemp;
                    _UserDefinedField1Value = ldt.Rows[0]["UserDefinedField1"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["UseUserDefinedField2"].ToString(), out bTemp))
                        _UserDefinedField2Use = bTemp;
                    _UserDefinedField2Value = ldt.Rows[0]["UserDefinedField2"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["UseUserDefinedField3"].ToString(), out bTemp))
                        _UserDefinedField3Use = bTemp;
                    _UserDefinedField3Value = ldt.Rows[0]["UserDefinedField3"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["UseUserDefinedField4"].ToString(), out bTemp))
                        _UserDefinedField4Use = bTemp;
                    _UserDefinedField4Value = ldt.Rows[0]["UserDefinedField4"].ToString().Trim();
                    if (bool.TryParse(ldt.Rows[0]["UseUserDefinedField5"].ToString(), out bTemp))
                        _UserDefinedField5Use = bTemp;
                    _UserDefinedField5Value = ldt.Rows[0]["UserDefinedField5"].ToString().Trim();
                    _WebPageDescription = ldt.Rows[0]["CampaignWebPageDescription"].ToString().Trim();
                    _WebPageSelectionComments = ldt.Rows[0]["CampaignWebPageSelectionComments"].ToString().Trim();
                    _MarketingLocation = ldt.Rows[0]["MarketingLocation"].ToString().Trim();
                    GetGenres(strUserName);
                    GetPeriods(strUserName);
                    GetTechLevels(strUserName);
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
                slParams.Add("@NPCApprovalRequired", _NPCApprovalRequired);
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
                cUtilities.PerformNonQuery("uspInsUpdCMCampaigns", slParams, "LARPortal", _UserName);
                blnReturn = true;
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, _UserName + lsRoutineName);
                blnReturn = false;
            }
            return blnReturn;
        }

        public void GetGenres(string strUserName)
        {
            int i = 1;
            string lsRoutineName = "cCampaignBase.GetGenres";
            string stStoredProc = "uspGetCampaignGenresByCampaignID";
            _UserName = strUserName;
            _GenreList = "";
            SortedList slParams = new SortedList();
            slParams.Add("@CampaignID", _CampaignID);
            DataTable dtGenres = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);

            foreach (DataRow dRow in dtGenres.Rows)
            {
                if (i == 1)
                    _GenreList = _GenreList + dRow["GenreName"].ToString();
                else
                    _GenreList = _GenreList + ", " + dRow["GenreName"].ToString();
                i = 2;
            }
        }

        public void GetPeriods(string strUserName)
        {
            int i = 1;
            string lsRoutineName = "cCampaignBase.GetPeriods";
            string stStoredProc = "uspGetCampaignPeriodsByCampaignID";
            _UserName = strUserName;
            _PeriodList = "";
            SortedList slParams = new SortedList();
            slParams.Add("@CampaignID", _CampaignID);
            DataTable dtPeriods = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);

            foreach (DataRow dRow in dtPeriods.Rows)
            {
                if (i == 1)
                    _PeriodList = _PeriodList + dRow["PeriodName"].ToString();
                else
                    _PeriodList = _PeriodList + ", " + dRow["PeriodName"].ToString();
                i = 2;
            }
        }

        private void GetTechLevels(string strUserName)
        {
            int i = 1;
            string lsRoutineName = "cCampaignBase.GetTechLevels";
            string stStoredProc = "uspGetCampaignTechLevelsByCampaignID";
            _UserName = strUserName;
            SortedList slParams = new SortedList();
            slParams.Add("@CampaignID", _CampaignID);
            DataTable dtTechLevels = cUtilities.LoadDataTable(stStoredProc, slParams, "LARPortal", strUserName, lsRoutineName);
            foreach (DataRow dRow in dtTechLevels.Rows)
            {
                if (i == 1)
                    _TechLevelList = _TechLevelList + dRow["TechLevelName"].ToString();
                else
                    _TechLevelList = _TechLevelList + ", " + dRow["TechLevelName"].ToString();
                i = 2;
            }
        }

        public DataTable GetCampaignRequestableRoles(int CampaignID, int UserID)
        {
            string stStoredProc = "uspGetCampaignRequestableRoles";
            string stCallingMethod = "cCampaignBase.GetCampaignRequestableRoles";
            int iTemp;
            bool bTemp;
            SortedList slParameters = new SortedList();
            slParameters.Add("@CampaignID", CampaignID);
            DataTable dtCampaignRoles = new DataTable();
            DataSet dsCampaignRoles = new DataSet();
            dtCampaignRoles = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", "Usename", stCallingMethod);
            foreach (DataRow dRow in dtCampaignRoles.Rows)
            {
                if (int.TryParse(dRow["CampaignRoleID"].ToString(), out iTemp))
                    CampaignRoleID = iTemp;
                if (int.TryParse(dRow["RoleID"].ToString(), out iTemp))
                    RoleID = iTemp;
                if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                    CampaignID = iTemp;
                RoleDescription = dRow["RoleDescription"].ToString();
                if (bool.TryParse(dRow["AutoApprove"].ToString(), out bTemp))
                    AutoApprove = bTemp;
            }
            return dtCampaignRoles;
        }


    }
}