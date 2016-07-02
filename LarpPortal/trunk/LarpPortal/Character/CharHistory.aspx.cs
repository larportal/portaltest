using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SortedList slParameters = new SortedList();
                slParameters.Add("@intUserID", Session["UserID"].ToString());
                DataTable dtCharacters = LarpPortal.Classes.cUtilities.LoadDataTable("uspGetCharacterIDsByUserID", slParameters,
                    "LARPortal", "Character", "CharacterMaster.Page_Load");
                ddlCharacterSelector.DataTextField = "CharacterAKA";
                ddlCharacterSelector.DataValueField = "CharacterID";
                ddlCharacterSelector.DataSource = dtCharacters;
                ddlCharacterSelector.DataBind();

                if (ddlCharacterSelector.Items.Count > 0)
                {
                    ddlCharacterSelector.ClearSelection();

                    if (Session["SelectedCharacter"] != null)
                    {
                        DataRow[] drValue = dtCharacters.Select("CharacterID = " + Session["SelectedCharacter"].ToString());
                        foreach (DataRow dRow in drValue)
                        {
                            DateTime DateChanged;
                            if (DateTime.TryParse(dRow["DateChanged"].ToString(), out DateChanged))
                                lblUpdateDate.Text = DateChanged.ToShortDateString();
                            else
                                lblUpdateDate.Text = "Unknown";
                            lblCampaign.Text = dRow["CampaignName"].ToString();
                            Session["CharacterHistoryEmail"] = dRow["CharacterHistoryNotificationEmail"];
                            Session["PlayerName"] = dRow["PlayerName"];
                            Session["PlayerEmail"] = dRow["EmailAddress"];
                            if (Session["CampaignID"] == null)
                                Session["CampaignID"] = dRow["CampaignID"];
                            else
                            {
                                Session["CampaignID"] = dRow["CampaignID"];
                            }
                        }
                        string sCurrentUser = Session["SelectedCharacter"].ToString();
                        foreach (ListItem liAvailableUser in ddlCharacterSelector.Items)
                        {
                            if (sCurrentUser == liAvailableUser.Value)
                                liAvailableUser.Selected = true;
                            else
                                liAvailableUser.Selected = false;
                        }
                    }
                    else
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }

                    if (ddlCharacterSelector.SelectedIndex == 0)
                    {
                        ddlCharacterSelector.Items[0].Selected = true;
                        Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                    }
                    ddlCharacterSelector.Items.Add(new ListItem("Add a new character", "-1"));
                }
                else
                    Response.Redirect("CharAdd.aspx");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SelectedCharacter"] != null)
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        tbHistory.Text = cChar.CharacterHistory;
                        lblHistory.Text = cChar.CharacterHistory.Replace(Environment.NewLine, "<br>");

                        if (cChar.DateHistoryApproved != null)
                        {
                            mvButtons.SetActiveView(vwAlreadyApproved);
                            tbHistory.Visible = false;
                            lblHistory.Visible = true;
                        }
                        else if (cChar.DateHistorySubmitted != null)
                        {
                            mvButtons.SetActiveView(vwAlreadySubmitted);
                            tbHistory.Visible = false;
                            lblHistory.Visible = true;
                        }
                        else
                        {
                            mvButtons.SetActiveView(vwButtons);
                            tbHistory.Visible = true;
                            lblHistory.Visible = false;
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                cChar.CharacterHistory = tbHistory.Text;
                cChar.Comments = tbHistory.Text;
                cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                string jsString = "alert('Character " + cChar.AKA + " has been saved.');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "MyApplication",
                        jsString,
                        true);
                CharacterHistoryEmailNotificaiton();
            }
        }

        protected void CharacterHistoryEmailNotificaiton()
        {
            string strUsername = Session["Username"].ToString();
            string strTo = "";
            string strCampaignName = lblCampaign.Text;
            string strPlayerName = "";
            string strPlayerEmail = "";
            if (Session["CharacterHistoryEmail"] != null)
                strTo = Session["CharacterHistoryEmail"].ToString();
            if (Session["PlayerName"] != null)
                strPlayerName = Session["PlayerName"].ToString();
            if (Session["PlayerEmail"] != null)
                strPlayerEmail = Session["PlayerEmail"].ToString();
            string strSubject = "Character History submitted - Character: " + ddlCharacterSelector.SelectedItem.Text;
            string strBody = "A new character history has been submitted for campaign " + lblCampaign.Text + "<br><br>";
            strBody = strBody + "Player: " + strPlayerName + "<br>";
            strBody = strBody + "Player Email:" + strPlayerEmail + "<br>";
            strBody = strBody + "Character:" + ddlCharacterSelector.SelectedItem.Text + "<br><br>";
            strBody = strBody + "History Text:<br>" + tbHistory.Text;
            strBody = strBody.Replace(System.Environment.NewLine, "<br />");

            Classes.cEmailMessageService SubmitCharacterHistory = new Classes.cEmailMessageService();
            try
            {
                SubmitCharacterHistory.SendMail(strSubject, strBody, strTo, strPlayerEmail, "", "CharacterHistory", Session["Username"].ToString());
            }
            catch (Exception)
            {

            }
        }

        protected void ddlCharacterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCharacterSelector.SelectedValue == "-1")
                Response.Redirect("CharAdd.aspx");

            if (Session["SelectedCharacter"].ToString() != ddlCharacterSelector.SelectedValue)
            {
                Session["SelectedCharacter"] = ddlCharacterSelector.SelectedValue;
                Response.Redirect("CharInfo.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CharHistory.aspx", true);
        }

        protected void btnSubmitOrSave_Command(object sender, CommandEventArgs e)
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                cChar.CharacterHistory = tbHistory.Text;
                cChar.Comments = tbHistory.Text;
                if (e.CommandName == "SUBMIT")
                {
                    cChar.DateHistorySubmitted = DateTime.Now;
                    lblCharHistoryMessage.Text = "The character history for " + cChar.AKA + " has been submitted for staff approval.";
                }
                else
                    lblCharHistoryMessage.Text = "The character history for " + cChar.AKA + " has been saved.";

                cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //string jsString = "alert('Character " + cChar.AKA + " has been saved.');";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                //        "MyApplication",
                //        jsString,
                //        true);
//                CharacterHistoryEmailNotificaiton();
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        }
    }
}