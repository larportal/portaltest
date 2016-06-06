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

namespace LarpPortal.Testing
{
    public partial class TestCallToClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInstructions.Text = "Reasons: 1-NPC Entire / 7-NPC Partial / 17-NPC Setup/Cleanup / 3-PC Entire /";
            lblInstructions.Text = lblInstructions.Text + " 8-PC Partial / 10-Prepay / 9-Preregister / 12-Staff Event";
            //int UserID, int CampaignID, int RoleAlignment, int CharacterID, int ReasonID, int EventID, int RegistrationID
            lblValue01.Text = "UserID-n/a: ";
            lblValue02.Text = "CampaignID: ";
            lblValue03.Text = "RoleAlignmentID: ";
            lblValue04.Text = "CharacterID: ";
            lblValue05.Text = "ReasonID: ";
            lblValue06.Text = "EventID: ";
            lblValue07.Text = "RegistrationID: ";
            lblValue08.Text = "";
            lblValue09.Text = "";
            lblValue10.Text = "";
            lblValue11.Text = "";
            lblValue12.Text = "";
            lblValue13.Text = "";
            lblValue14.Text = "";
            lblValue15.Text = "";
        }

        protected void btnRunTest_Click(object sender, EventArgs e)
        {
            // Procedure to test - Change variable types accordingly
            //Test CP Opportunity Add from registration
            //int UserID, int CampaignID, int RoleAlignment, int CharacterID, int ReasonID, int EventID, int RegistrationID
            int Value01 = 0;
                Int32.TryParse(Session["UserID"].ToString(), out Value01);
            int Value02 = 0;
                Int32.TryParse(txtValue02.Text, out Value02);
            int Value03 = 0;
                Int32.TryParse(txtValue03.Text, out Value03);
            int Value04 = 0;
                Int32.TryParse(txtValue04.Text, out Value04);
            int Value05 = 0;
                Int32.TryParse(txtValue05.Text, out Value05);
            int Value06 = 0;
                Int32.TryParse(txtValue06.Text, out Value06);
            int Value07 = 0;
                Int32.TryParse(txtValue07.Text, out Value07);
            string Value08 = txtValue08.Text;
            string Value09 = txtValue09.Text;
            string Value10 = txtValue10.Text;
            string Value11 = txtValue11.Text;
            string Value12 = txtValue12.Text;
            string Value13 = txtValue13.Text;
            string Value14 = txtValue14.Text;
            string Value15 = txtValue15.Text;

            Classes.cPoints Points = new Classes.cPoints();
            Points.CreateRegistrationCPOpportunity(Value01, Value02, Value03, Value04, Value05, Value06, Value07);
        }
    }
}