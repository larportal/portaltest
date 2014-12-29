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
    public partial class MemberDemographics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveLeftNav"] = "Demographics";
            //Load all the demographics information about the player
            Classes.cUser Demography = new Classes.cUser("ryulin", "Ricka0r0r2");
            Classes.cPlayer PLDemography = new Classes.cPlayer(2, "ryulin");
            string fn = Demography.FirstName;
            string mi = Demography.MiddleName;
            string ln = Demography.LastName;
            string gen = PLDemography.GenderStandared;
            string othergen = PLDemography.GenderOther;
            DateTime dob = PLDemography.DateOfBirth;
            string usernm;
            if(Session["UserName"] == null)
            {
                usernm = "";
            }
            else
            {
                usernm = Session["UserName"].ToString();
            }
            string nick = Demography.NickName;
            string pen = Demography.AuthorName;
            string forum = Demography.ForumUserName;
            string pict = PLDemography.UserPhoto;
            string emergencynm = PLDemography.EmergencyContactName;
            //Classes.cPhone EmergencyPhone = new Classes.cPhone();
            //string emergencyph = EmergencyPhone.PhoneNumber; // = PLDemography.EmergencyContactPhone; 
            // Need to define the list for Addresses
            // Need to define the list for Phone Numbers
            // Need to define the list for Email Addresses
        }
    }
}