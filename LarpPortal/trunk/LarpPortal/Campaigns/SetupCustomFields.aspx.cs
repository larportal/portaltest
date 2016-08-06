using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Campaigns
{
    public partial class SetupCustomFields : System.Web.UI.Page
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
            tbCustomField1.Text = Campaigns.UserDefinedField1Value;
            tbCustomField2.Text = Campaigns.UserDefinedField2Value;
            tbCustomField3.Text = Campaigns.UserDefinedField3Value;
            tbCustomField4.Text = Campaigns.UserDefinedField4Value;
            tbCustomField5.Text = Campaigns.UserDefinedField5Value;
            chkUseField1.Checked = Campaigns.UserDefinedField1Use;
            chkUseField2.Checked = Campaigns.UserDefinedField2Use;
            chkUseField3.Checked = Campaigns.UserDefinedField3Use;
            chkUseField4.Checked = Campaigns.UserDefinedField4Use;
            chkUseField5.Checked = Campaigns.UserDefinedField5Use;
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
            Campaigns.UserDefinedField1Use = chkUseField1.Checked;
            Campaigns.UserDefinedField2Use = chkUseField2.Checked;
            Campaigns.UserDefinedField3Use = chkUseField3.Checked;
            Campaigns.UserDefinedField4Use = chkUseField4.Checked;
            Campaigns.UserDefinedField5Use = chkUseField5.Checked;
            Campaigns.UserDefinedField1Value = tbCustomField1.Text;
            Campaigns.UserDefinedField2Value = tbCustomField2.Text;
            Campaigns.UserDefinedField3Value = tbCustomField3.Text;
            Campaigns.UserDefinedField4Value = tbCustomField4.Text;
            Campaigns.UserDefinedField5Value = tbCustomField5.Text;
            Campaigns.Save();
            string jsString = "alert('Campaign character custom changes have been saved.');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "MyApplication",
                    jsString,
                    true);
        }

    }
}