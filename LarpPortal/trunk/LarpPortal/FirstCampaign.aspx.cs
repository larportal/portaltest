using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.General
{
    public partial class FirstCampaign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int UserID = 0;
            if(Session["UserID"] != null)
            {
                int.TryParse(Session["UserID"].ToString(), out UserID);
            }
            Classes.cLogin FirstCampaignWelcome = new Classes.cLogin();
            FirstCampaignWelcome.getFirstCampaignWelcome();
            lblWelcome.Text = FirstCampaignWelcome.FirstCampaignWelcomeText;
            ReloadtvCampaign(UserID);
        }

        protected void tvCampaign_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void ReloadtvCampaign(int UserID)
        {
            
            int StatusID = 0;
            string CampaignName = "";
            int iTemp;
            TreeNode CampaignNode;
            tvCampaign.Nodes.Clear();
            // Start Session Variable Definition for calling class
            string EndDate = DateTime.Now.ToShortDateString();
            int GameSystemFilter = 0;
            int CampaignFilter = 0;
            int GenreFilter = 0;
            int StyleFilter = 0;
            int TechLevelFilter = 0;
            int SizeFilter = 0;
            string ZipCodeFilter = "";
            int RadiusFilter = 0;
            Classes.cCampaignSelection Campaigns = new Classes.cCampaignSelection();
            DataTable dtCampaigns = new DataTable();
            dtCampaigns = Campaigns.LoadCampaigns(UserID, EndDate, GameSystemFilter, CampaignFilter, GenreFilter, StyleFilter, TechLevelFilter, SizeFilter, ZipCodeFilter, RadiusFilter);
            if (dtCampaigns.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtCampaigns.Rows)
                {
                    if (int.TryParse(dRow["CampaignID"].ToString(), out iTemp))
                        CampaignFilter = iTemp;
                    if (int.TryParse(dRow["StatusID"].ToString(), out iTemp))
                        StatusID = iTemp;
                    CampaignName = dRow["CampaignName"].ToString();
                    if (StatusID != 4)
                    {
                        CampaignNode = new TreeNode(CampaignName, CampaignFilter.ToString());
                        CampaignNode.Selected = false;
                        CampaignNode.NavigateUrl = "";
                        tvCampaign.Nodes.Add(CampaignNode);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                CampaignNode = new TreeNode("No Campaigns Available", "0");
                CampaignNode.Selected = false;
                CampaignNode.NavigateUrl = "";
                tvCampaign.Nodes.Add(CampaignNode);
            }
        }

    }
}