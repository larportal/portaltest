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
            Session["DefaultPlayerProfilePath"] = "img/player/";
            Session["ActiveLeftNav"] = "Demographics";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            Classes.cUser Demography = new Classes.cUser(uName,"Password");
            Classes.cPlayer PLDemography = new Classes.cPlayer(uID, uName);
            string fn = Demography.FirstName;
            string mi = "";
            if(Demography.MiddleName.Length > 0)
                mi = Demography.MiddleName.Substring(0, 1);
            string ln = Demography.LastName;
            string gen = PLDemography.GenderStandared;
            string othergen = PLDemography.GenderOther;
            DateTime dob = PLDemography.DateOfBirth;
            string strdob = dob.ToString("MM/dd/yyyy");
            string nick = Demography.NickName;
            string pen = PLDemography.AuthorName;
            string forum = Demography.ForumUserName;
            string pict = PLDemography.UserPhoto;
            if (pict == "")
                imgPlayerImage.ImageUrl = "http://placehold.it/150x150";
            else
                imgPlayerImage.ImageUrl = "img/player/" + pict;
            string emergencynm = PLDemography.EmergencyContactName;
            //Classes.cPhone EmergencyPhone = new Classes.cPhone();
            //string emergencyph = EmergencyPhone.PhoneNumber; // = PLDemography.EmergencyContactPhone; 
            // Need to define the list for Addresses
            // Need to define the list for Phone Numbers
            // Need to define the list for Email Addresses
            txtGenderOther.Visible = false;
            if (!IsPostBack)
            {
                txtFirstName.Attributes.Add("Placeholder", fn);
                txtMI.Attributes.Add("Placeholder", mi);
                txtLastName.Attributes.Add("Placeholder", ln);
                if(gen != null)
                {
                    switch (gen)
                    {
                        case "M":
                            ddlGender.Items.Add(new ListItem("Male","M"));
                            ddlGender.Items.Add(new ListItem("Female","F"));
                            ddlGender.Items.Add(new ListItem("Other","O"));
                            break;
                        case "F":
                            ddlGender.Items.Add(new ListItem("Female","F"));
                            ddlGender.Items.Add(new ListItem("Male","M"));
                            ddlGender.Items.Add(new ListItem("Other","O"));
                            break;
                        case "O":
                            ddlGender.Items.Add(new ListItem("Other","O"));
                            ddlGender.Items.Add(new ListItem("Male","M"));
                            ddlGender.Items.Add(new ListItem("Female","F"));
                            txtGenderOther.Visible = true;
                            break;
                        default:
                            ddlGender.Items.Add(new ListItem("Gender", "Enabled=false"));
                            ddlGender.Items.Add(new ListItem("Male","M"));
                            ddlGender.Items.Add(new ListItem("Female","F"));
                            ddlGender.Items.Add(new ListItem("Other","O"));
                            break;

                    }
                }
                else
                {
                    ddlGender.Items.Add(new ListItem("Gender", "Enabled=false"));
                    ddlGender.Items.Add(new ListItem("Male","M"));
                    ddlGender.Items.Add(new ListItem("Female","F"));
                    ddlGender.Items.Add(new ListItem("Other","O"));
                }
                txtGenderOther.Attributes.Add("Placeholder", othergen);
                txtDOB.Attributes.Add("Placeholder", strdob);
                txtEmergencyName.Attributes.Add("Placeholder", emergencynm);
                txtEmergencyPhone.Attributes.Add("Placeholder", "EmNum");
                txtUsername.Attributes.Add("Placeholder", uName);
                txtNickname.Attributes.Add("Placeholder", nick);
                txtPenname.Attributes.Add("Placeholder", pen);
                txtForumname.Attributes.Add("Placeholder", forum);
            }
        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string otherdesc = txtGenderOther.Text;
            if(ddlGender.SelectedValue == "O")
            {
                txtGenderOther.Visible = true;
                if (otherdesc == "")
                    otherdesc = "Enter description";
                txtGenderOther.Attributes.Add("Placeholder", otherdesc);
                txtGenderOther.Focus();
            }
            else
            {
                txtGenderOther.Visible = false;
            }
        }
    }
}