using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class GameSystemTest2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int UserID = 2;
            int GameSystemID = 1;

            //Game System Stuff
            Classes.cGameSystem GameSystem = new Classes.cGameSystem();
            GameSystem.Load(GameSystemID, UserID);
            int GSID = GameSystem.GameSystemID;
            string Desc = GameSystem.GameSystemName;
            string URL = GameSystem.GameSystemURL;
            string WPD = GameSystem.GameSystemWebPageDescription;

        }

        protected void btnDeleteGameSystem_Click(object sender, EventArgs e)
        {
            int i;
            Classes.cGameSystem GS = new Classes.cGameSystem();
            if (int.TryParse(tbGameSystemID.Text, out i))
                GS.GameSystemID = i;
            GS.Delete(2);
        }

        protected void btnAddGameSystem_Click(object sender, EventArgs e)
        {
            //int i;
            Classes.cGameSystem GS = new Classes.cGameSystem();
            GS.GameSystemID = -1;
            GS.GameSystemName = tbGameSystemName.Text;
            GS.GameSystemURL = tbGameSystemURL.Text;
            GS.GameSystemWebPageDescription = "New web page description to be defined when we get the functionality in place";
            GS.Save(2);
        }

        protected void btnUpdateGameSystem_Click(object sender, EventArgs e)
        {
            int i;
            Classes.cGameSystem GS = new Classes.cGameSystem();
            if (int.TryParse(tbGameSystemID.Text, out i))
                GS.GameSystemID = i;
            GS.GameSystemName = tbGameSystemName.Text;
            GS.GameSystemURL = tbGameSystemURL.Text;
            GS.GameSystemWebPageDescription = tbGameSystemWebPageDescription.Text;
            GS.Save(2);
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            int i;
            Classes.cGameSystem GS = new Classes.cGameSystem();
            if (int.TryParse(tbGameSystemID.Text, out i))
            GS.Load(i, 2);
            tbGameSystemName.Text = GS.GameSystemName;
            tbGameSystemURL.Text = GS.GameSystemURL;
            tbGameSystemWebPageDescription.Text = GS.GameSystemWebPageDescription;
            Classes.cPlayerRole PR = new Classes.cPlayerRole();
            if (int.TryParse(tbPlayerRoleID.Text, out i))
                PR.Load(2, 0, 0);
            }
    }
}