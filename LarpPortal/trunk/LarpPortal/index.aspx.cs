using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LarpPortal.Classes;

namespace LarpPortal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Make all theses things visible for real release
            txtFirstName.Visible = false;
            txtLastName.Visible = false;
            txtEmail.Visible = false;
            txtPasswordNew.Visible = false;
            txtPasswordNewRetype.Visible = false;
            chkTermsOfUse.Visible = false;
            //      
            Session["LoginName"] = "Guest";                // Until login changes it
            Session["UserID"] = 0;                         // Until login changes it
            Session["SecurityRole"] = 0;                   // Until login changes it
            Session["WebPage"] = "~/publicCampaigns.aspx"; // Until login changes it
            lblInvalidLogin.Visible = false;
            string SiteOpsMode;
            Classes.cLogin OpsMode = new Classes.cLogin();
            OpsMode.SetSiteOperationalMode();
            SiteOpsMode = OpsMode.SiteOperationalMode;
            //Session["OperationalMode"] = SiteOpsMode;
            txtName.Visible = false;
            txtLastLocation.Visible = false;
            txtUserID.Visible = false;
            if (SiteOpsMode == "Test")
            {
                txtLastLocation.Attributes.Add("Placeholder", "Last Location in the Portal");
                txtName.Attributes.Add("Placeholder", "Member Name");
                txtUserID.Attributes.Add("Placeholder", "UserID");
                txtName.Visible = true;
                txtLastLocation.Visible = true;
                txtUserID.Visible = true;
            }
            if (!IsPostBack)
            {
                txtUserName.Attributes.Add("Placeholder", "Username");
                //txtUserName.Attributes.Add("Required","");
                txtUserName.Focus();
                txtPassword.Attributes.Add("Placeholder", "Password");
                txtEmail.Attributes.Add("Placeholder", "Email");
                txtFirstName.Attributes.Add("Placeholder", "First Name");
                txtLastName.Attributes.Add("Placeholder", "Last Name");
                txtPasswordNew.Attributes.Add("Placeholder", "Password");
                txtPasswordNewRetype.Attributes.Add("Placeholder", "Retype Password");
                btnSignUp.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string WhereAreYouGoing;
            if (Page.IsValid)
            {
                int intUserID;
                Classes.cLogin Login = new Classes.cLogin();
                Login.Load(txtUserName.Text, txtPassword.Text);
                if (Login.MemberID == 0)  // Invalid UserID Password combination
                {
                    // Go on for now with a guest security role but eventually...
                    // Put up an invalid UserID Password combination message and put the focus back on the UserID field
                     Session["SecurityRole"] = 0;
                     lblInvalidLogin.Visible = true;

                }
                else
                {
                    Session["SecurityRole"] = Login.SecurityRoleID;
                    txtName.Text = Login.FirstName + " " + Login.LastName;
                    txtLastLocation.Text = Login.LastLoggedInLocation;
                    txtUserID.Text = Login.MemberID.ToString();
                    intUserID = Login.MemberID;
                    Session["LoginName"] = Login.FirstName;
                    Session["Username"] = Login.Username;
                    Session["UserID"] = Login.UserID;
                    // Write login entry to UserLoginAudit table

                    // Go to the default or last page visited
                    if(Session["WebPage"] == null)
                    {
                        Session["WebPage"] = "~/MemberDemographics.aspx";
                    }
                    else
                    {
                        if (txtLastLocation.Text == "")
                            txtLastLocation.Text = "Preferences.aspx";
                        Session["WebPage"] = "~/" + txtLastLocation.Text;
                    }
                    WhereAreYouGoing = Session["WebPage"].ToString();
                    Response.Redirect(Session["WebPage"].ToString());
                }
            }
            else
            {
                // Put up a something's wrong message - put up a label and make it visible / invisible at the appropriate time.
            }
            

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Validate values to site standards
            }
            else
            {
                // Put up a something's wrong message
            }

        }

        protected void chkTermsOfUse_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTermsOfUse.Checked)
            {
                btnSignUp.Visible = true;
            }
            else
            {
                btnSignUp.Visible = false;
            }
        }
    }
}