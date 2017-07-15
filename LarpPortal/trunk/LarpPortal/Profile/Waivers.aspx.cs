using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

using LarpPortal.Classes;

namespace LarpPortal.Profile
{
    public partial class Waivers : System.Web.UI.Page
    {
        protected int _UserID = 0;
        protected string _UserName = "";
        protected int _SelectedID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["Username"] != null)
                _UserName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            Classes.cPlayer PlayerInfo = new cPlayer(_UserID, _UserName);
//            hidPlayerProfileID.Value = PlayerInfo.PlayerProfileID.ToString();

            gvWaivers.DataSource = PlayerInfo.PlayerWaivers;
            gvWaivers.DataBind();

            string postBackControlName = Request.Params.Get("__EVENTTARGET");
            string eventArgument = Request.Params.Get("__EVENTARGUMENT");

            _SelectedID = -1;

            if (postBackControlName != null)
                if (postBackControlName.ToUpper().Contains("GVWAIVERS"))
                    if (!int.TryParse(eventArgument, out _SelectedID))
                        _SelectedID = -1;

            if (_SelectedID == -1)
                if (!int.TryParse(hidRowSelected.Value, out _SelectedID))
                    _SelectedID = -1;

            if (gvWaivers.Rows.Count != 0)
            {
                if (_SelectedID != -1)
                {
                    LoadRecord(_SelectedID);
                }
                else
                {
                    if (!int.TryParse(hidRowSelected.Value, out _SelectedID))
                        _SelectedID = 0;
                    if (_SelectedID < gvWaivers.Rows.Count)
                        LoadRecord(_SelectedID);
                    else
                        LoadRecord(0);
                }
            }
        }

        protected void btnSaveComments_Click(object sender, EventArgs e)
        {
//            Classes.cPlayer PlayerInfo = new cPlayer(_UserID, _UserName);
            //PlayerInfo.MedicalComments = tbMedicalComments.Text;
            //PlayerInfo.Allergies = tbAllergies.Text;
//            PlayerInfo.Save();
            int iPlayerWaiverID;
            if (int.TryParse(hidPlayerWaiverID.Value, out iPlayerWaiverID))
            {
                cPlayerWaiver Waiver = new cPlayerWaiver(iPlayerWaiverID, _UserName);
                Waiver.PlayerNotes = tbWaiverComment.Text;
                Waiver.AcceptedDate = null;
                Waiver.DeclinedDate = null;

                if (cbxAgree.Checked)
                    Waiver.AcceptedDate = DateTime.Today;
                else if (cbxDisagree.Checked)
                    Waiver.DeclinedDate = DateTime.Today;
                Waiver.Save(_UserName, _UserID);
            }
            lblModalMessage.Text = "Your information has been saved.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMessage();", true);
        }

        //protected void gvWaivers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow g = gvWaivers.SelectedRow;
        //}

        //protected void gvWaivers_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    GridViewRow g = gvWaivers.SelectedRow;
        //}

        protected void gvWaivers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                cPlayerWaiver Waiver = e.Row.DataItem as cPlayerWaiver;
                e.Row.Attributes.Add("onclick", "javascript:__doPostBack('gvWaivers','" + e.Row.RowIndex.ToString() + "')");
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
            }
        }

        public void LoadRecord(int iRowID)
        {
            hidRowSelected.Value = "";
            GridViewRow gvRow = gvWaivers.Rows[iRowID];
            if (gvRow != null)
            {
                gvWaivers.SelectedIndex = iRowID;
                HiddenField PlayerWaiverID = gvRow.FindControl("hidPlayerWaiverID") as HiddenField;
                if (PlayerWaiverID != null)
                {
                    int iWaiverID;
                    if (int.TryParse(PlayerWaiverID.Value, out iWaiverID))
                    {
                        hidPlayerWaiverID.Value = iWaiverID.ToString();
                        cPlayerWaiver Waiver = new cPlayerWaiver(iWaiverID, _UserName);
                        lblWaiverText.Text = Waiver.WaiverText;
                        tbWaiverComment.Text = Waiver.PlayerNotes;
                        cbxAgree.Checked = false;
                        cbxDisagree.Checked = false;
                        if (Waiver.AcceptedDate.HasValue)
                            cbxAgree.Checked = true;
                        else if (Waiver.DeclinedDate.HasValue)
                            cbxDisagree.Checked = true;
                        foreach (TableCell tCell in gvRow.Cells)
                        {
                            tCell.Font.Bold = true;
                            tCell.BackColor = System.Drawing.Color.LightBlue;
                        }
                        hidRowSelected.Value = iRowID.ToString();
                    }
                }
            }
        }
    }
}