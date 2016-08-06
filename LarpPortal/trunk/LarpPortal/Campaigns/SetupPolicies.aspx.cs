using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;

namespace LarpPortal.Campaigns
{
    public partial class SetupPolicies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSavedData();
            }
        }

        protected void LoadSavedData()
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            chkAllowCharacterRebuilds.Checked = Campaigns.AllowCharacterRebuild;
            chkAllowCPDonation.Checked = Campaigns.AllowCPDonation;
            tbCrossCampaignPosting.Text = Campaigns.CrossCampaignPosting;
            chkNPCApprovalRequired.Checked = Campaigns.NPCApprovalRequired;
            chkPCApprovalRequired.Checked = Campaigns.PlayerApprovalRequired;
            chkShareLocationUseNotes.Checked = Campaigns.ShareLocationUseNotes;
            chkUseCampaignCharacters.Checked = Campaigns.UseCampaignCharacters;
            tbEarliestCPApplicationYear.Text = Campaigns.EarliestCPApplicationYear.ToString();
            if (tbEarliestCPApplicationYear.Text == null)
            {
                tbEarliestCPApplicationYear.Text = string.Format("{0.yyyy}", DateTime.Today);
            } 
            tbEventCharacterCap.Text = Campaigns.EventCharacterCPCap.ToString();
            tbCrossCampaignPosting.Text = Campaigns.CrossCampaignPosting;
            tbMaximumCPPerYear.Text = Campaigns.MaximumCPPerYear.ToString();
            tbTotalCharacterCap.Text = Campaigns.TotalCharacterCPCap.ToString();
            LoadApprovalLevel("Character", Campaigns.CharacterApprovalLevel);
            LoadApprovalLevel("PEL", Campaigns.PELApprovalLevel);
        }

        protected void LoadApprovalLevel(string ApprovalTypeDescription, int CurrentLevel)
        {
            string strUserName = Session["UserName"].ToString();
            string stStoredProc = "uspGetApprovalLevels";
            string stCallingMethod = "SetupPolicies.aspx.LoadApprovalLevel";
            DataTable dtApproval = new DataTable();
            SortedList sParams = new SortedList();
            sParams.Add("@LevelTypeDescription", ApprovalTypeDescription);
            dtApproval = Classes.cUtilities.LoadDataTable(stStoredProc, sParams, "LARPortal", strUserName, stCallingMethod);
            switch (ApprovalTypeDescription)
            {
                case "PEL":
                    ddlPELApprovalLevel.DataTextField = "ApprovalLevelDescription";
                    ddlPELApprovalLevel.DataValueField = "CampaignApprovalLevel";
                    ddlPELApprovalLevel.DataSource = dtApproval;
                    ddlPELApprovalLevel.DataBind();
                    ddlPELApprovalLevel.SelectedValue = CurrentLevel.ToString();
                    break;

                case "Character":
                    ddlCharacterApprovalLevel.DataTextField = "ApprovalLevelDescription";
                    ddlCharacterApprovalLevel.DataValueField = "CampaignApprovalLevel";
                    ddlCharacterApprovalLevel.DataSource = dtApproval;
                    ddlCharacterApprovalLevel.DataBind();
                    ddlCharacterApprovalLevel.SelectedValue = CurrentLevel.ToString();
                    break;

                default:

                    break;

            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int iTemp = 0;
            int UserID = 0;
            int CampaignID = 0;
            string UserName = Session["UserName"].ToString();
            if (int.TryParse(Session["UserID"].ToString(), out iTemp))
                UserID = iTemp;
            if (int.TryParse(Session["CampaignID"].ToString(), out iTemp))
                CampaignID = iTemp;
            Classes.cCampaignBase Campaigns = new Classes.cCampaignBase(CampaignID, UserName, UserID);
            Campaigns.AllowCharacterRebuild = chkAllowCharacterRebuilds.Checked;
            Campaigns.AllowCPDonation = chkAllowCPDonation.Checked;
            Campaigns.ShareLocationUseNotes = chkShareLocationUseNotes.Checked;
            Campaigns.NPCApprovalRequired = chkNPCApprovalRequired.Checked;
            Campaigns.UseCampaignCharacters = chkUseCampaignCharacters.Checked;
            Campaigns.PlayerApprovalRequired = chkPCApprovalRequired.Checked;
            Campaigns.PELApprovalLevel = ddlPELApprovalLevel.SelectedValue.ToInt32();
            Campaigns.CharacterApprovalLevel = ddlCharacterApprovalLevel.SelectedValue.ToInt32();
            Campaigns.EarliestCPApplicationYear = tbEarliestCPApplicationYear.Text.ToInt32();
            Campaigns.EventCharacterCPCap = tbEventCharacterCap.Text.ToInt32();
            Campaigns.MaximumCPPerYear = tbMaximumCPPerYear.Text.ToInt32();
            Campaigns.TotalCharacterCPCap = tbTotalCharacterCap.Text.ToInt32();
            Campaigns.CrossCampaignPosting = tbCrossCampaignPosting.Text;
            Campaigns.Save();
            string jsString = "alert('Campaign policy changes have been saved.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
        }

        protected void tbEarlestCPApplicationYear_TextChanged(object sender, EventArgs e)
        {
            int iTemp = 0;
            string strCurrYear = string.Format("{0:yyyy}", DateTime.Today);
            int CurrYear = strCurrYear.ToInt32();
            if(int.TryParse(tbEarliestCPApplicationYear.Text, out iTemp))
            {
                if (iTemp < 1990 || iTemp > CurrYear + 5)
                {
                    string jsString = "alert('This does not appear to be a valid year.  Please try again');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                            "MyApplication",
                            jsString,
                            true);
                    tbEarliestCPApplicationYear.Text = CurrYear.ToString();
                }
            }
            else
            {
                string jsString = "alert('This has to be a four digit year.  Please try again.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                tbEarliestCPApplicationYear.Text = CurrYear.ToString();
            }

        }

        protected void tbEventCharacterCap_TextChanged(object sender, EventArgs e)
        {
            double dTemp = 0;
            if(double.TryParse(tbEventCharacterCap.Text, out dTemp))
            {

            }
            else
            {
                string jsString = "alert('Event Character Cap has to be a number.  Please try again.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                tbEventCharacterCap.Text = "0";
            }
        }

        protected void tbMaximumCPPerYear_TextChanged(object sender, EventArgs e)
        {
            double dTemp = 0;
            if (double.TryParse(tbMaximumCPPerYear.Text, out dTemp))
            {

            }
            else
            {
                string jsString = "alert('Maximum points per year has to be a number.  Please try again.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                tbMaximumCPPerYear.Text = "0";
            }
        }

        protected void tbTotalCharacterCap_TextChanged(object sender, EventArgs e)
        {
            double dTemp = 0;
            if (double.TryParse(tbTotalCharacterCap.Text, out dTemp))
            {

            }
            else
            {
                string jsString = "alert('Total character cap has to be a number.  Please try again.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                tbTotalCharacterCap.Text = "0";
            }
        }

    }
}