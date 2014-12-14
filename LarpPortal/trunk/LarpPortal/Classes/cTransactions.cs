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
    public class cTransactions
    {
        private int _PlayerCPAuditID;
        public int AuditID
        {
            get { return _PlayerCPAuditID; }
            set { _PlayerCPAuditID = value; }
        }
        public List<cTransaction> lsTransactions = new List<cTransaction>();

        /// <summary>
        /// This will load the details of a particular player audit transaction
        /// Must pass a UserID
        /// EarnedAtCampaignID optional to return earnings at a speicific campaign, else 0
        /// SpentAtCampaignID optional to return earnings sent to a speicific campaign, else 0
        /// CharacterID optional to return earnings to a speicific character, else 0
        /// Status optional to return earnings at a speicific status, else 0
        /// ReasonID optional to return earnings for a particular reason, else 0
        /// StartDate optional to return earnings after a speicific start date (usually in conjuntion with end date for range), else 0 - Future Functionality
        /// EndDate optional to return earnings before a speicific end date (usually in conjunction with start date for range, else 0 - Future Functionality
        /// </summary>
        public void Load(int OwningPlayerID, int AuditID, int EarnAtCampaignID, int SpentAtCampaignID, int SpentOnCharacterID, string Status, int EarnDescriptionID)
        {
            string stStoredProc = "uspGetAuditRecords";
            string stCallingMethod = "cTransaction.Load";
            int iTemp;
            DateTime StartDate;
            StartDate = DateTime.Parse("1/1/1900");
            DateTime EndDate;
            EndDate = DateTime.Now;
            SortedList slParameters = new SortedList();
            slParameters.Add("@UserID", OwningPlayerID);
            slParameters.Add("@EarnedAtCampaignID", EarnAtCampaignID);
            slParameters.Add("@SpentAtCampaignID", SpentAtCampaignID);
            slParameters.Add("@CharacterID", SpentOnCharacterID);
            slParameters.Add("@StartDate", StartDate);
            slParameters.Add("@EndDate", EndDate);
            slParameters.Add("@Status", Status);
            slParameters.Add("@ReasonID", EarnDescriptionID);
            DataSet dsTransactions = new DataSet();
            dsTransactions = cUtilities.LoadDataSet(stStoredProc, slParameters, "LARPortal", OwningPlayerID.ToString(), stCallingMethod);
            foreach (DataRow dRow in dsTransactions.Tables[0].Rows)
            {
                if (int.TryParse(dRow["AuditID"].ToString(), out iTemp))
                {
                    cTransaction Transaction = new cTransaction();
                    Transaction.Load(OwningPlayerID, AuditID, EarnAtCampaignID, SpentAtCampaignID, SpentAtCampaignID, Status, EarnDescriptionID);
                    lsTransactions.Add(Transaction);
                }
            }
        }
    }
}