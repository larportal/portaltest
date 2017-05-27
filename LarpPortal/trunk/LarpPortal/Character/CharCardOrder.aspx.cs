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

namespace LarpPortal.Character
{
    public partial class CharCardOrder : System.Web.UI.Page
    {
        private DataTable _dtPlaces = new DataTable();
        private string _UserName = "";
        private int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                _UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
            oCharSelect.CharacterChanged += oCharSelect_CharacterChanged;

            if (!IsPostBack)
            {
                Session.Remove("SkillList");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (!IsPostBack)
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                ShowGrid();
            }
        }

        protected void btnSaveCharacter_Click(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();
            if (oCharSelect.CharacterID.HasValue)
            {
                foreach (GridViewRow dRow in gvSkills.Rows)
                {
                    HiddenField hidSkillID = (HiddenField)dRow.FindControl("hidSkillID");
                    TextBox tbSortOrder = (TextBox)dRow.FindControl("tbSortOrder");
                    if ((hidSkillID != null) &&
                         (tbSortOrder != null))
                    {
                        SortedList sParams = new SortedList();
                        sParams.Add("@CharacterSkillID", hidSkillID.Value);
                        sParams.Add("@SortOrder", tbSortOrder.Text);
                        Classes.cUtilities.PerformNonQuery("uspInsUpdCHCharacterSkills", sParams, "LARPortal", _UserName);      // Session["UserName"].ToString());
                    }
                }
                ShowGrid();
                lblmodalMessage.Text = "Character " + oCharSelect.CharacterInfo.AKA + " has been saved.";
                btnCloseMessage.Attributes.Add("data-dismiss", "modal");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MyApplication", "openMessage();", true);
            }
        }

        //protected void gvSkills_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //        {
        //            DataRowView dRow = e.Row.DataItem as DataRowView;

        //            string t = dRow["CardDisplayIncant"].ToString();
        //        }
        //    }
        //}

        private void ShowGrid()
        {
            if (oCharSelect.CharacterID.HasValue)
            {
                DataTable dtCharactersForCampaign = new DataTable();
                SortedList sParam = new SortedList();
                sParam.Add("@CharacterID", oCharSelect.CharacterID.Value);      // iCharID);
                dtCharactersForCampaign = Classes.cUtilities.LoadDataTable("uspGetCharCardSkills", sParam, "LARPortal", Session["LoginName"].ToString(), "");
                gvSkills.DataSource = dtCharactersForCampaign;
                gvSkills.DataBind();
            }
        }

        protected void oCharSelect_CharacterChanged(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();

            if (oCharSelect.CharacterInfo != null)
            {
                if (oCharSelect.CharacterID.HasValue)
                {
                    Classes.cUser UserInfo = new Classes.cUser(_UserName, "PasswordNotNeeded");
                    UserInfo.LastLoggedInCampaign = oCharSelect.CharacterInfo.CampaignID;
                    UserInfo.LastLoggedInCharacter = oCharSelect.CharacterID.Value;
                    UserInfo.LastLoggedInMyCharOrCamp = (oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters ? "M" : "C");
                    UserInfo.Save();
                }
                ShowGrid();
            }
        }
    }
}