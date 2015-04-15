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
using System.Text.RegularExpressions;

namespace LarpPortal
{
    public partial class MemberDemographics : System.Web.UI.Page
    {
        private Classes.cUser Demography = null;
        private Classes.cPlayer PLDemography = null;

        private bool isValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
        }

        private bool isValidPhoneNumber(string strPhone)
        {
            if (string.IsNullOrWhiteSpace(strPhone))
                return false;

            string phone = string.Join(string.Empty, strPhone.Where(x => Char.IsDigit(x)).ToArray());

            //800s, 900, and zero digits on first position are not okay
            if (phone.StartsWith("8") || phone.StartsWith("9") || phone.StartsWith("0"))
                return false;

            // Get all the digits from the string and make sure we have ten numeric value
            return (phone.Length == 10);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            Session["DefaultPlayerProfilePath"] = "img/player/";
            Session["ActiveLeftNav"] = "Demographics";
            string uName = "";
            int uID = 0;
            if (Session["Username"] != null)
                uName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                uID = (Session["UserID"].ToString().ToInt32());
            Demography = new Classes.cUser(uName,"Password");
            PLDemography = new Classes.cPlayer(uID, uName);
            string fn = Demography.FirstName;
            string mi = "";
            if(Demography.MiddleName.Length > 0)
                mi = Demography.MiddleName.Substring(0, 1);
            string ln = Demography.LastName;
            string gen = PLDemography.GenderStandared.ToUpper(); 
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
            string emergencyContactPhone = string.Empty;
            if (PLDemography.EmergencyContactPhone != null)
                emergencyContactPhone = PLDemography.EmergencyContactPhone.PhoneNumber;
            //Classes.cPhone EmergencyPhone = new Classes.cPhone();
            //string emergencyph = EmergencyPhone.PhoneNumber; // = PLDemography.EmergencyContactPhone; 
            // Need to define the list for Addresses
            // Need to define the list for Phone Numbers
            // Need to define the list for Email Addresses

            txtGenderOther.Visible = false;
            if (!IsPostBack)
            {
                txtFirstName.Text = fn;
                txtMI.Text= mi;
                txtLastName.Text = ln;
                
                if (gen.Length>0 && "MFO".Contains(gen))
                    ddlGender.SelectedValue = gen;

                txtGenderOther.Text = othergen;
                txtDOB.Text = strdob;
                txtEmergencyName.Text = emergencynm;
                txtEmergencyPhone.Text = emergencyContactPhone;
                txtUsername.Text = uName;
                txtNickname.Text = nick;
                txtPenname.Text = pen;
                txtForumname.Text = forum;
            }
        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlGender.SelectedValue == "O")
            {
                txtGenderOther.Visible = true;                
                txtGenderOther.Focus();
            }
            else
            {
                txtGenderOther.Text = string.Empty; //Clear value
                txtGenderOther.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFirstName.Text))
                Demography.FirstName = txtFirstName.Text.Trim();

            Demography.MiddleName = txtMI.Text.Trim(); //I should be able to remove my middle initial if I want

            if (!string.IsNullOrWhiteSpace(txtLastName.Text))
                Demography.LastName = txtLastName.Text.Trim();

            if (ddlGender.SelectedIndex != -1)
                PLDemography.GenderStandared = ddlGender.SelectedValue;

            PLDemography.GenderOther = txtGenderOther.Text; //We shall trust this value since the select event clears the text when needed

            Demography.NickName = txtNickname.Text;
            
            DateTime dob;
            if (DateTime.TryParse(txtDOB.Text, out dob))
                PLDemography.DateOfBirth = dob;

            PLDemography.AuthorName = txtPenname.Text;
            Demography.ForumUserName = txtForumname.Text;

            PLDemography.EmergencyContactName = txtEmergencyName.Text;

            /*
             * 1)  Figurate out saving addresses, phones, etc. grids first 
             * Jeff mention keep my eye on the enum to flag deletes properly
             */
            
            //I am not sure how to handle emergency info
            /* 2)
            if (PLDemography.EmergencyContactPhone == null)
                PLDemography.EmergencyContactPhone = new cPhone();

            if (PLDemography.EmergencyContactPhone != txtEmergencyPhone.Text)
            {
                PLDemography.EmergencyContactPhone = txtEmergencyPhone.Text;
                PLDemography.EmergencyContactPhoneID = -1;
            }
            */

            /* 3) handle picture update/add.
             */


            /* As I validate I will place the new values on the component prior to saving the values*/
            Demography.Save();
            PLDemography.Save();

            lblMessage.Text = "Changes saved successfully.";
        }

        
    }
}