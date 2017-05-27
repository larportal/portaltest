using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharPlaces : System.Web.UI.Page
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
                oCharSelect_CharacterChanged(null, null);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            oCharSelect.LoadInfo();
            if (oCharSelect.CharacterID.HasValue)
                Session["PlaceCharacterID"] = oCharSelect.CharacterID.Value;
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
                    Session["PlaceCharacterID"] = oCharSelect.CharacterID.Value;
                    if ((oCharSelect.WhichSelected == controls.CharacterSelect.Selected.MyCharacters) &&
                        (oCharSelect.CharacterInfo.CharacterType != 1))
                        Session["CharPlaceReadOnly"] = "Y";
                    else
                        Session.Remove("CharPlaceReadOnly");

                }
                else
                    Session.Remove("PlaceCharacterID");
//                _Reload = true;
            }
        }
    }
}