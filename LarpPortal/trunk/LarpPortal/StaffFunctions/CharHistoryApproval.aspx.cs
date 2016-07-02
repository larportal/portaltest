using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.StaffFunctions
{
    public partial class CharHistoryApproval : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SortedList slParameters = new SortedList();

                if (Request["CharID"] == null)
                    Response.Redirect("CharHistoryApprovalList.aspx", true);

                int iCharID;
                if (!int.TryParse(Request["CharID"], out iCharID))
                    Response.Redirect("CharHistoryApprovalList.aspx", true);

                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                if (cChar.AKA.Length > 0)
                    hidCharacterName.Value = cChar.AKA;
                else
                    hidCharacterName.Value = cChar.FirstName + " " + cChar.LastName;

                lblCharacter.Text = hidCharacterName.Value;

                hidPlayerName.Value = cChar.PlayerName;
                if (cChar.CharacterEmail.Length > 0)
                    hidPlayerEMail.Value = cChar.CharacterEmail;
                else if (cChar.PlayerEMail.Length > 0)
                    hidPlayerEMail.Value = cChar.PlayerEMail;
                else
                    hidPlayerEMail.Value = "";

                SortedList sParams = new SortedList();
                sParams.Add("@ReasonID", 24);
                sParams.Add("@CampaignID", cChar.CampaignID);
                string sSQL = "select * from CMCampaignCPOpportunityDefaults where ReasonID = @ReasonID and CampaignID = @CampaignID and DateDeleted is null";
                DataTable dtCampaignDefaults = Classes.cUtilities.LoadDataTable(sSQL, sParams, "LARPortal", Session["UserName"].ToString(),
                    "CharHistoryApproval.Page_Load.LoadDefaults", Classes.cUtilities.LoadDataTableCommandType.Text);

                foreach (DataRow dRow in dtCampaignDefaults.Rows)
                {
                    double dCPValue = 0.0;
                    if (double.TryParse(dRow["CPValue"].ToString(), out dCPValue))
                        tbCPAwarded.Text = string.Format("{0:0.00}", dCPValue);
                    hidCampaignCPOpportunityDefaultID.Value = dRow["CampaignCPOpportunityDefaultID"].ToString();
                }
                lblHistory.Text = cChar.CharacterHistory.Replace(Environment.NewLine, "<br>");

                // The have already approved it so change the functionality of the approve button.
                if (cChar.DateHistoryApproved != null)
                {
                    btnApprove.Text = "Done";
                    btnApprove.CommandArgument = "DONE";
                }
            }
        }

        //protected void CharacterHistoryEmailNotificaiton()
        //{
        //    if (hidPlayerEMail.Value.Length > 0)
        //    {
        //        string strSubject = "Character History Approved for Character " + lblCharacter.Text;
        //        string strBody = "Your character history for " + lblCharacter.Text + " has been approved.<br>";
        //        strBody += "You have been awarded " + tbCPAwarded.Text + " CP.<br><br>";
        //        strBody = strBody + "History Text:<br>" + lblHistory.Text;
        //        strBody = strBody.Replace(System.Environment.NewLine, "<br />");

        //        Classes.cEmailMessageService SubmitCharacterHistory = new Classes.cEmailMessageService();
        //        try
        //        {
        //            SubmitCharacterHistory.SendMail(strSubject, strBody, hidPlayerEMail.Value, "", "");
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}
        protected void btnApprove_Command(object sender, CommandEventArgs e)
        {
            int iCharID;
            if (int.TryParse(Request["CharID"], out iCharID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                if (e.CommandName == "APPROVE")
                {
                    cChar.DateHistoryApproved = DateTime.Now;
                    cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
                    lblCharApprovedMessage.Text = "The character history for " + cChar.AKA + " has been approved.";

                    Classes.cPoints Points = new Classes.cPoints();
                    int UserID = 0;
                    int CharacterID = 0;
                    int CampaignCPOpportunityDefaultID = 0;
                    double CPAwarded = 0.0;

                    int.TryParse(Session["UserID"].ToString(), out UserID);
                    int.TryParse(hidCampaignCPOpportunityDefaultID.Value, out CampaignCPOpportunityDefaultID);
                    double.TryParse(tbCPAwarded.Text, out CPAwarded);

                    Points.AssignHistoryPoints(UserID, cChar.PlayerID, CharacterID, CampaignCPOpportunityDefaultID, cChar.CampaignID, CPAwarded, DateTime.Now);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    if (hidPlayerEMail.Value.Length > 0)
                    {
                        string strSubject = "Character History Approved for Character " + lblCharacter.Text;
                        string strBody = "Your character history for " + lblCharacter.Text + " has been approved.<br>";
                        strBody += "You have been awarded " + tbCPAwarded.Text + " CP.<br><br>";
                        strBody = strBody + "History Text:<br>" + lblHistory.Text;
                        strBody = strBody.Replace(System.Environment.NewLine, "<br />");

                        Classes.cEmailMessageService SubmitCharacterHistory = new Classes.cEmailMessageService();
                        try
                        {
                            SubmitCharacterHistory.SendMail(strSubject, strBody, hidPlayerEMail.Value, "", "", "CharacterHistory", Session["Username"].ToString());
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
    }
}