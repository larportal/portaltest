using System;
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

                        lblHeader.Text = "Character History - " + cChar.AKA + " - " + cChar.CampaignName;

                        taHistory.InnerText = cChar.CharacterHistory;

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int iCharID;
            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                cChar.LoadCharacter(iCharID);

                cChar.CharacterHistory = taHistory.InnerText;
                cChar.Comments = taHistory.InnerText;
                cChar.SaveCharacter(Session["UserName"].ToString(), (int)Session["UserID"]);
            }
        }
    }
}