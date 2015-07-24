using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal
{
    public partial class SilverfireRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@UserID", Session["UserID"].ToString());

            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            DataTable dtUser = Classes.cUtilities.LoadDataTable("uspGetSilverfireCharacter", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

            mvPlayerInfo.SetActiveView(vwNoPlayer);

            foreach (DataRow dRow in dtUser.Rows)
            {
                mvPlayerInfo.SetActiveView(vwPlayerInfo);
                if (dRow["NickName"].ToString().Trim().Length > 0)
                    lblPlayerName.Text = dRow["NickName"].ToString().Trim() + " ( " + dRow["FirstName"].ToString().Trim() + " " + dRow["LastName"].ToString().Trim() + " )";
                else
                    lblPlayerName.Text = dRow["FirstName"].ToString().Trim() + " " + dRow["LastName"].ToString().Trim();

                lblCharacterAKA.Text = dRow["CharacterAKA"].ToString();

                ddlPaymentType.SelectedIndex = -1;

                if (dRow["EventPaymentTypeID"] != DBNull.Value)
                {
                    lblAlreadyRegistered.Visible = true;
                    btnRegister.Visible = false;

                    foreach (ListItem li in ddlPaymentType.Items)
                        if (dRow["EventPaymentTypeID"].ToString() == li.Value)
                            li.Selected = true;
                }
                else
                {
                    lblAlreadyRegistered.Visible = false;
                    btnRegister.Visible = true;
                }

                tbComment.Text = dRow["CommentToStaff"].ToString();
                hidEventID.Value = dRow["EventID"].ToString();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SortedList sParam = new SortedList();
            sParam.Add("@RegistrationID", "-1");
            sParam.Add("@UserID", Session["UserID"].ToString());
            sParam.Add("@EventID", hidEventID.Value);
            sParam.Add("@RoleAlignmentID", "1");
            sParam.Add("@CharacterID", hidCharacterID.Value);
            sParam.Add("@DateRegistered", DateTime.Now);
            sParam.Add("@EventPaymentTypeID", ddlPaymentType.SelectedValue);
            sParam.Add("@PlayerCommentsToStaff", tbComment.Text.Trim());

            try
            {
                MethodBase lmth = MethodBase.GetCurrentMethod();
                string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
                DataTable dtUser = Classes.cUtilities.LoadDataTable("uspInsUpdCMRegistrations", sParam, "LARPortal", Session["UserName"].ToString(), lsRoutineName);
                mvPlayerInfo.SetActiveView(vwRegistered);
            }
            catch
            {
                mvPlayerInfo.SetActiveView(vwError);
            }
        }
    }
}