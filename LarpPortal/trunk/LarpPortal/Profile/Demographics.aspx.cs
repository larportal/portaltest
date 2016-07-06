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
    public partial class Demographics : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";
                tbFirstName.Attributes.Add("Placeholder", "First Name");
                tbLastName.Attributes.Add("Placeholder", "Last Name");
                lblMessage.Text = "";
                ViewState["NewRecCounter"] = -1;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
                Session["ActiveLeftNav"] = "Demographics";
                string uName = "";
                int uID = 0;
                if (Session["Username"] != null)
                    uName = Session["Username"].ToString();
                if (Session["UserID"] != null)
                    int.TryParse(Session["UserID"].ToString(), out uID);

                Classes.cUser Demography = null;
                Classes.cPlayer PLDemography = null;

                Demography = new Classes.cUser(uName, "Password");
                PLDemography = new Classes.cPlayer(uID, uName);

                string gen = PLDemography.GenderStandared.ToUpper();
                string othergen = PLDemography.GenderOther;

                string pict = PLDemography.UserPhoto;

                if (PLDemography.HasPicture)
                {
                    imgPlayerImage.ImageUrl = PLDemography.Picture.PictureURL;
                    ViewState["UserIDPicture"] = PLDemography.Picture.PictureID;
                }
                else
                {
                    if (PLDemography.UserPhoto.Length > 0)
                    {
                        imgPlayerImage.ImageUrl = PLDemography.UserPhoto;
                        ViewState.Remove("UserIDPicture");
                    }
                    else
                    {
                        imgPlayerImage.ImageUrl = "http://placehold.it/150x150";
                        ViewState.Remove("UserIDPicture");
                    }
                }

                string emergencyContactPhone = string.Empty;
                if (PLDemography.EmergencyContactPhone != null)
                {
                    emergencyContactPhone = PLDemography.EmergencyContactPhone;
                    Int32 iPhone;
                    if (Int32.TryParse(emergencyContactPhone.Replace("(", "").Replace(")", "").Replace("-", ""), out iPhone))
                    {
                        emergencyContactPhone = iPhone.ToString("(###)###-####");
                    }
                }

                tbFirstName.Text = Demography.FirstName;
                if (Demography.MiddleName.Length > 1)
                    tbMiddleInit.Text = Demography.MiddleName.Substring(0, 1);
                else
                    tbMiddleInit.Text = "";
                tbLastName.Text = Demography.LastName;

                tbGenderOther.Style.Add("visibility", "hidden");
                tbGenderOther.Text = othergen;

                if (gen.Length > 0)
                {
                    if ("MFO".Contains(gen))
                        ddlGender.SelectedValue = gen;
                    if (gen == "O")
                        tbGenderOther.Style.Add("visibility", "visible");
                }
                tbDOB.Text = PLDemography.DateOfBirth.ToString("MM/dd/yyyy");
                tbEmergencyName.Text = PLDemography.EmergencyContactName;
                tbEmergencyPhone.Text = emergencyContactPhone;
                tbUserName.Text = uName;
                tbNickName.Text = Demography.NickName;
                tbPenName.Text = PLDemography.AuthorName;
                tbForumName.Text = Demography.ForumUserName;

                Session["dem_Addresses"] = Demography.UserAddresses;
                Session["dem_Phones"] = Demography.UserPhones;
                Session["dem_Emails"] = Demography.UserEmails;

                BindAddresses();
                BindPhoneNumbers();
                BindEmails();

                ddlGender.Attributes.Add("onchange", "DisplaySexOther(this);");
            }

            lblErrorMessage1.Text = "";
            lblErrorMessage2.Text = "";
            btnSave1.Enabled = true;
            btnSave2.Enabled = true;

            // Now we check to make sure that there is a least a single record and that it's valid.
            List<cAddress> AllAddresses = Session["dem_Addresses"] as List<cAddress>;
            int iBadAddresses = AllAddresses.Count(x => !x.IsValid());

            List<cEMail> AllEmail = Session["dem_Emails"] as List<cEMail>;
            int iBadEmail = AllEmail.Count(x => !x.IsValid());

            List<cPhone> AllPhone = Session["dem_Phones"] as List<cPhone>;
            int iBadPhone = AllPhone.Count(x => !x.IsValid());

            if ((iBadAddresses > 0) ||
                (iBadEmail > 0) ||
                (iBadPhone > 0) ||
                (gvEmails.Rows.Count == 0))
            {
                if (iBadAddresses > 0)
                    lblErrorMessage1.Text = "* At least one of your addresses is not valid. ";
                if (gvEmails.Rows.Count == 0)
                    lblErrorMessage1.Text += "* You must have at least one valid email address. ";
                if (iBadEmail > 0)
                    lblErrorMessage1.Text += "* At least one of your email addresses is not valid. ";
                if (iBadPhone > 0)
                    lblErrorMessage1.Text += "* At least one of your phone numbers is not valid. ";
                lblErrorMessage1.Text = lblErrorMessage1.Text.Trim();
                btnSave1.Enabled = false;
            }

            lblErrorMessage2.Text = lblErrorMessage1.Text;
            btnSave2.Enabled = btnSave1.Enabled;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string UserName = "";
            int iUserID = 0;
            if (Session["Username"] != null)
                UserName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out iUserID);

            cUser Demography = new Classes.cUser(UserName, "Password");
            cPlayer PLDemography = new Classes.cPlayer(iUserID, UserName);

            if (!string.IsNullOrWhiteSpace(tbFirstName.Text))
                Demography.FirstName = tbFirstName.Text.Trim();

            Demography.MiddleName = tbMiddleInit.Text.Trim(); //I should be able to remove my middle initial if I want

            if (!string.IsNullOrWhiteSpace(tbLastName.Text))
                Demography.LastName = tbLastName.Text.Trim();

            if (ddlGender.SelectedIndex != -1)
                PLDemography.GenderStandared = ddlGender.SelectedValue;

            PLDemography.GenderOther = tbGenderOther.Text; //We shall trust this value since the select event clears the text when needed

            Demography.NickName = tbNickName.Text;

            if (string.IsNullOrWhiteSpace(tbUserName.Text)) //If left empty set back to original setting...They may not remember it....
            {
                tbUserName.Text = Demography.LoginName;
            }

            // 1 - No duplicate usernames allowed
            Classes.cLogin Login = new Classes.cLogin();
            Login.CheckForExistingUsername(tbUserName.Text);
            if (Login.MemberID != 0 && Login.MemberID != Demography.UserID)  // UserID is taken
            {
                lblMessage.Text = "This username is already in use.  Please select a different one.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                tbUserName.Focus();
                return;
            }
            else
            {
                Demography.LoginName = tbUserName.Text.Trim();
            }

            DateTime dob;
            if (DateTime.TryParse(tbDOB.Text, out dob))
                PLDemography.DateOfBirth = dob;
            else
            {
                lblMessage.Text = "Please enter a valid date";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                tbDOB.Focus();
                return;
            }

            PLDemography.AuthorName = tbPenName.Text;
            Demography.ForumUserName = tbForumName.Text;

            PLDemography.EmergencyContactName = tbEmergencyName.Text;

            // Using the inital records merge result with the new ones.
            if (AddressesChangesValidate() == false)
                return;

            if (PhoneNumbersChangesValidate() == false)
                return;

            if (EmailsChangesValidate() == false)
                return;

            if (!cPhone.isValidPhoneNumber(tbEmergencyPhone.Text, 10))
            {
                lblMessage.Text = cPhone.ErrorDescription;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                tbEmergencyPhone.Focus();
                return;
            }
            else
                PLDemography.EmergencyContactPhone = tbEmergencyPhone.Text;

            /* 3) handle picture update/add.
             */

            //At this point all validation must have been done so it is time to merge lists 
            AddressesChangesUpdate(Demography);
            PhonesChangesUpdate(Demography);
            EmailsChangesUpdate(Demography);

            PLDemography.PictureID = -1;

            if (ViewState["UserIDPicture"] != null)
            {
                int iTemp;
                if (int.TryParse(ViewState["UserIDPicture"].ToString(), out iTemp))
                    PLDemography.PictureID = iTemp;
                else
                    PLDemography.PictureID = -1;
                //PLDemography.UserPhoto = Session["dem_Img_Url"].ToString();
                //imgPlayerImage.ImageUrl = Session["dem_Img_Url"].ToString();
                //Session["dem_Img_Url"] = "";
                //Session.Remove("dem_Img_Id");

                //Classes.cPicture NewPicture = new Classes.cPicture();
                //int iPictureId =0;
                //if (Session["dem_Img_Id"] != null && Int32.TryParse(Session["dem_Img_Id"].ToString(), out iPictureId))
                //{
                //    //This code will be enabled once the stored procedure is created
                //    string userID = Session["UserID"].ToString();
                //    //NewPicture.Load(iPictureId, userID);
                //    //NewPicture.PictureFileName = NewPicture.PictureFileName.Replace("_2", "_1");
                //    //NewPicture.Save(userID);
                //    //Time to trash the old main picture with the picture in memory                    
                //    //PLDemography.UserPhoto = NewPicture.PictureFileName;                    
                //}
            }
            else
                PLDemography.PictureID = -1;

            Demography.Save();
            PLDemography.Save();
            Session["Username"] = Demography.LoginName;

            lblMessage.Text = "Changes saved successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }


        #region AddressProcessing

        private void AddressesChangesUpdate(cUser Demography)
        {
            List<cAddress> addresses = Session["dem_Addresses"] as List<cAddress>;

            int userId = (int)Session["UserID"];
            //For each element of the original list perform one of the following:
            //If update, update information
            //If delete, mark record for delete
            //If new, mark record for adding
            cAddress a1 = null;

            if (Demography.UserAddresses != null)
            {
                foreach (cAddress a in Demography.UserAddresses) //state in database
                {
                    a1 = addresses.FirstOrDefault(x => x.AddressID == a.AddressID); //If record not found in memory that means it was deleted
                    if (a1 == null)
                        a.SaveUpdate(userId, true);
                    else
                        a1.SaveUpdate(userId);
                }
            }

            a1 = addresses.FirstOrDefault(x => x.AddressID <= 0);
            if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
            {
                a1.SaveUpdate(userId);
            }
        }

        private bool AddressesChangesValidate()
        {
            // First check if a new valid record was inserted
            if (Session["dem_Addresses"] != null)
            {
                List<cAddress> addresses = Session["dem_Addresses"] as List<cAddress>;

                foreach (cAddress a in addresses)
                {
                    if (a.AddressID > 0 && !a.IsValid()) //if new records are invalid that is okay they will not make it to the database.
                    {
                        lblMessage.Text = a.strErrorDescription;
                        return false;
                    }
                }
            }

            return true;
        }

        private void BindAddresses()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            List<cAddress> Addresses = Session["dem_Addresses"] as List<cAddress>;
            gvAddresses.DataSource = Addresses;
            gvAddresses.DataBind();
        }

        protected void gvAddresses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAddresses.EditIndex = e.NewEditIndex;
            BindAddresses();
        }

        protected void gvAddresses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<cAddress> clAddresses = Session["dem_Addresses"] as List<cAddress>;

            if (e.RowIndex < clAddresses.Count)
            {
                cAddress UpdatedAddress = clAddresses[e.RowIndex];

                TextBox tbAddress1 = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbAddress1");
                if (tbAddress1 != null)
                    UpdatedAddress.Address1 = tbAddress1.Text;
                TextBox tbCity = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbCity");
                if (tbCity != null)
                    UpdatedAddress.City = tbCity.Text;
                TextBox tbPostalCode = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbPostalCode");
                if (tbPostalCode != null)
                    UpdatedAddress.PostalCode = tbPostalCode.Text;

                if (!UpdatedAddress.IsValid())
                {
                    lblMessage.Text = UpdatedAddress.strErrorDescription;
                    e.Cancel = true;
                }
                else
                {
                    TextBox tbAddress2 = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbAddress2");
                    if (tbAddress2 != null)
                        UpdatedAddress.Address2 = tbAddress2.Text;
                    TextBox tbState = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbState");
                    if (tbState != null)
                        UpdatedAddress.StateID = tbState.Text;
                    TextBox tbCountry = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbCountry");
                    if (tbCountry != null)
                        UpdatedAddress.Country = tbCountry.Text;
                    DropDownList ddlAddressType = (DropDownList)gvAddresses.Rows[e.RowIndex].FindControl("ddlAddressType");
                    if (ddlAddressType != null)
                    {
                        UpdatedAddress.AddressTypeID = ddlAddressType.SelectedValue.ToInt32();
                    }
                    RadioButton rbPrimary = (RadioButton)gvAddresses.Rows[e.RowIndex].FindControl("rbPrimary");
                    if (rbPrimary != null)
                    {
                        if (rbPrimary.Checked)
                        {
                            clAddresses.ForAll(x => x.IsPrimary = false);
                            UpdatedAddress.IsPrimary = true;
                        }
                    }
                    Session["dem_Addresses"] = clAddresses;
                    gvAddresses.EditIndex = -1;
                }
                BindAddresses();
            }
            else
                e.Cancel = true;
        }

        protected void gvAddresses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAddresses.EditIndex = -1;
            BindAddresses();
        }

        protected void gvAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cAddress> clAddresses = Session["dem_Addresses"] as List<cAddress>;

            if (e.RowIndex < clAddresses.Count)
            {
                clAddresses.RemoveAt(e.RowIndex);
            }

            Session["dem_Addresses"] = clAddresses;

            BindAddresses();
        }

        protected void gvAddresses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    List<cAddress> clAddress = Session["dem_Addresses"] as List<cAddress>;
                    DropDownList ddlAddressType = (DropDownList)e.Row.FindControl("ddlAddressType");
                    if (ddlAddressType != null)
                    {
                        if (clAddress.Count > 0)
                        {
                            ddlAddressType.DataTextField = "AddressType";
                            ddlAddressType.DataValueField = "AddressTypeID";

                            ddlAddressType.DataSource = clAddress[0].AddressTypes;
                            ddlAddressType.DataBind();

                            ddlAddressType.ClearSelection();

                            cAddress SourceAddress = e.Row.DataItem as cAddress;

                            foreach (ListItem lItem in ddlAddressType.Items)
                            {
                                if (lItem.Value == SourceAddress.AddressTypeID.ToString())
                                {
                                    ddlAddressType.ClearSelection();
                                    lItem.Selected = true;
                                }
                            }
                        }
                    }

                    TextBox tbTemp;

                    tbTemp = (TextBox)e.Row.FindControl("tbAddress1");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Address 1");
                    tbTemp = (TextBox)e.Row.FindControl("tbAddress2");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Address 2");
                    tbTemp = (TextBox)e.Row.FindControl("tbCity");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "City");
                    tbTemp = (TextBox)e.Row.FindControl("tbState");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "State Abbrev");
                    tbTemp = (TextBox)e.Row.FindControl("tbPostalCode");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Postal Code");
                    tbTemp = (TextBox)e.Row.FindControl("tbCountry");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Country");
                }
            }
        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            List<cAddress> clAddress = Session["dem_Addresses"] as List<cAddress>;
            cAddress NewAddress = new cAddress();
            clAddress.Add(NewAddress);
            Session["dem_Addresses"] = clAddress;
            BindAddresses();

            gvAddresses.EditIndex = gvAddresses.Rows.Count - 1;
            BindAddresses();
            GridViewRow dRow = gvAddresses.Rows[gvAddresses.EditIndex];
            if (dRow != null)
            {
                TextBox tbAddress1 = (TextBox)dRow.FindControl("tbAddress1");
                if (tbAddress1 != null)
                    tbAddress1.Focus();
            }
        }

        #endregion

        #region PhoneNumbers

        private void PhonesChangesUpdate(cUser Demography)
        {
            List<cPhone> phones = Session["dem_Phones"] as List<cPhone>;

            int userId = (int)Session["UserID"];
            //For each element of the original list perform one of the following:
            //If update, update information
            //If delete, mark record for delete
            //If new, mark record for adding
            cPhone a1 = null;

            if (Demography.UserPhones != null)
            {
                foreach (cPhone a in Demography.UserPhones) //state in database
                {
                    a1 = phones.FirstOrDefault(x => x.PhoneNumberID == a.PhoneNumberID); //If record not found in memory that means it was deleted
                    if (a1 == null)
                        a.SaveUpdate(userId, true);
                    else
                        a1.SaveUpdate(userId);
                }
            }

            a1 = phones.FirstOrDefault(x => x.PhoneNumberID <= 0);
            if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
            {
                a1.SaveUpdate(userId);
            }
        }

        private bool PhoneNumbersChangesValidate()
        {
            // First check if a new valid record was inserted
            if (Session["dem_Phones"] != null)
            {
                List<cPhone> phones = Session["dem_Phones"] as List<cPhone>;

                foreach (cPhone a in phones)
                {
                    if (a.PhoneNumberID > 0 && !a.IsValid()) //if new records are invalid that is okay they will not make it to the database.
                    {
                        lblMessage.Text = a.strErrorDescription;
                        return false;
                    }
                }
            }

            return true;

        }

        private void BindPhoneNumbers()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            List<cPhone> PhoneNumbers = Session["dem_Phones"] as List<cPhone>;
            gvPhoneNumbers.DataSource = PhoneNumbers;
            gvPhoneNumbers.DataBind();
        }

        protected void gvPhoneNumbers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPhoneNumbers.EditIndex = e.NewEditIndex;
            BindPhoneNumbers();
        }

        protected void gvPhoneNumbers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<cPhone> clPhoneNumbers = Session["dem_Phones"] as List<cPhone>;

            if (e.RowIndex < clPhoneNumbers.Count)
            {
                cPhone UpdatedPhone = clPhoneNumbers[e.RowIndex];

                TextBox tbAreaCode = (TextBox)gvPhoneNumbers.Rows[e.RowIndex].FindControl("tbAreaCode");
                if (tbAreaCode != null)
                    UpdatedPhone.AreaCode = tbAreaCode.Text;

                TextBox tbPhoneNumber = (TextBox)gvPhoneNumbers.Rows[e.RowIndex].FindControl("tbPhoneNumber");
                if (tbPhoneNumber != null)
                    UpdatedPhone.PhoneNumber = tbPhoneNumber.Text;

                if (!UpdatedPhone.IsValid())
                {
                    lblMessage.Text = UpdatedPhone.strErrorDescription;
                    e.Cancel = true;
                }
                else
                {
                    TextBox tbExtension = (TextBox)gvPhoneNumbers.Rows[e.RowIndex].FindControl("tbExtension");
                    if (tbExtension != null)
                        UpdatedPhone.Extension = tbExtension.Text;

                    DropDownList ddlPhoneType = (DropDownList)gvPhoneNumbers.Rows[e.RowIndex].FindControl("ddlPhoneType");
                    if (ddlPhoneType != null)
                    {
                        UpdatedPhone.PhoneTypeID = ddlPhoneType.SelectedValue.ToInt32();
                    }
                    RadioButton rbPrimary = (RadioButton)gvPhoneNumbers.Rows[e.RowIndex].FindControl("rbPrimary");
                    if (rbPrimary != null)
                    {
                        if (rbPrimary.Checked)
                        {
                            clPhoneNumbers.ForAll(x => x.IsPrimary = false);
                            UpdatedPhone.IsPrimary = true;
                        }
                    }
                    Session["dem_Phones"] = clPhoneNumbers;
                    gvPhoneNumbers.EditIndex = -1;
                }
                BindPhoneNumbers();
            }
            else
                e.Cancel = true;
        }

        protected void gvPhoneNumbers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPhoneNumbers.EditIndex = -1;
            BindPhoneNumbers();
        }

        protected void gvPhoneNumbers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cPhone> clPhoneNumbers = Session["dem_Phones"] as List<cPhone>;

            if (e.RowIndex < clPhoneNumbers.Count)
            {
                clPhoneNumbers.RemoveAt(e.RowIndex);
            }

            Session["dem_Phones"] = clPhoneNumbers;

            BindPhoneNumbers();
        }

        protected void gvPhoneNumbers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    List<cPhone> clPhoneNumbers = Session["dem_Phones"] as List<cPhone>;
                    DropDownList ddlPhoneType = (DropDownList)e.Row.FindControl("ddlPhoneType");
                    if (ddlPhoneType != null)
                    {
                        if (clPhoneNumbers.Count > 0)
                        {
                            ddlPhoneType.DataTextField = "PhoneType";
                            ddlPhoneType.DataValueField = "PhoneTypeID";

                            ddlPhoneType.DataSource = clPhoneNumbers[0].PhoneTypes;
                            ddlPhoneType.DataBind();

                            ddlPhoneType.ClearSelection();

                            cPhone SourcePhone = e.Row.DataItem as cPhone;

                            foreach (ListItem lItem in ddlPhoneType.Items)
                            {
                                if (lItem.Value == SourcePhone.PhoneTypeID.ToString())
                                {
                                    ddlPhoneType.ClearSelection();
                                    lItem.Selected = true;
                                }
                            }
                        }
                    }

                    TextBox tbTemp;

                    tbTemp = (TextBox)e.Row.FindControl("tbAreaCode");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Area Code");
                    tbTemp = (TextBox)e.Row.FindControl("tbPhoneNumber");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Phone Number");
                    tbTemp = (TextBox)e.Row.FindControl("tbExtension");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Extension");
                }
            }
        }

        protected void btnAddPhoneNumber_Click(object sender, EventArgs e)
        {
            List<cPhone> clPhone = Session["dem_Phones"] as List<cPhone>;
            cPhone NewPhone = new cPhone();
            clPhone.Add(NewPhone);
            Session["dem_Phones"] = clPhone;
            BindPhoneNumbers();

            gvPhoneNumbers.EditIndex = gvPhoneNumbers.Rows.Count - 1;
            BindPhoneNumbers();
        }

        #endregion

        #region Emails

        private void EmailsChangesUpdate(cUser Demography)
        {
            List<cEMail> emails = Session["dem_Emails"] as List<cEMail>;

            int userId = (int)Session["UserID"];
            //For each element of the original list perform one of the following:
            //If update, update information
            //If delete, mark record for delete
            //If new, mark record for adding
            cEMail a1 = null;

            if (Demography.UserEmails != null)
            {
                foreach (cEMail a in Demography.UserEmails) //state in database
                {
                    a1 = emails.FirstOrDefault(x => x.EMailID == a.EMailID); //If record not found in memory that means it was deleted
                    if (a1 == null)
                        a.SaveUpdate(userId, true);
                    else
                        a1.SaveUpdate(userId);
                }
            }

            a1 = emails.FirstOrDefault(x => x.EMailID <= 0);
            if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
            {
                a1.SaveUpdate(userId);
            }
        }

        private bool EmailsChangesValidate()
        {
            // First check if a new valid record was inserted
            if (Session["dem_Emails"] != null)
            {
                List<cEMail> emails = Session["dem_Emails"] as List<cEMail>;

                foreach (cEMail a in emails)
                {
                    if (a.EMailID > 0 && !a.IsValid()) //if new records are invalid that is okay they will not make it to the database.
                    {
                        lblMessage.Text = a.strErrorMessage;
                        return false;
                    }
                }
            }

            return true;
        }

        private void BindEmails()
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            List<cEMail> Emails = Session["dem_Emails"] as List<cEMail>;
            gvEmails.DataSource = Emails;
            gvEmails.DataBind();

            if (Emails.Count == 1)
            {
                Button btnDelete = (Button)gvEmails.Rows[0].FindControl("btnDelete");
                if (btnDelete != null)
                    btnDelete.Visible = false;
            }
            else
            {
                foreach (GridViewRow gvRow in gvEmails.Rows)
                {
                    Button btnDelete = (Button)gvRow.FindControl("btnDelete");
                    if (btnDelete != null)
                        btnDelete.Visible = true;
                }
            }
        }

        protected void gvEmails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmails.EditIndex = e.NewEditIndex;
            BindEmails();
        }

        protected void gvEmails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<cEMail> clEmails = Session["dem_Emails"] as List<cEMail>;

            if (e.RowIndex < clEmails.Count)
            {
                cEMail UpdatedEmail = clEmails[e.RowIndex];

                TextBox tbEmailAddress = (TextBox)gvEmails.Rows[e.RowIndex].FindControl("tbEmailAddress");
                if (tbEmailAddress != null)
                    UpdatedEmail.EmailAddress = tbEmailAddress.Text;

                DropDownList ddlEmailType = (DropDownList)gvEmails.Rows[e.RowIndex].FindControl("ddlEmailType");
                if (ddlEmailType != null)
                {
                    UpdatedEmail.EmailTypeID = ddlEmailType.SelectedValue.ToInt32();
                }

                if (!UpdatedEmail.IsValid())
                {
                    lblMessage.Text = UpdatedEmail.strErrorMessage;
                    e.Cancel = true;
                }
                else
                {
                    RadioButton rbPrimary = (RadioButton)gvEmails.Rows[e.RowIndex].FindControl("rbPrimary");
                    if (rbPrimary != null)
                    {
                        if (rbPrimary.Checked)
                        {
                            clEmails.ForAll(x => x.IsPrimary = false);
                            UpdatedEmail.IsPrimary = true;
                        }
                    }
                    Session["dem_Emails"] = clEmails;
                    gvEmails.EditIndex = -1;
                }
                BindEmails();
            }
            else
                e.Cancel = true;
        }

        protected void gvEmails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmails.EditIndex = -1;
            BindEmails();
        }

        protected void gvEmails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cEMail> clEmails = Session["dem_Emails"] as List<cEMail>;

            if (e.RowIndex < clEmails.Count)
            {
                clEmails.RemoveAt(e.RowIndex);
            }

            Session["dem_Emails"] = clEmails;

            BindEmails();
        }

        protected void gvEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    List<cEMail> clEmails = Session["dem_Emails"] as List<cEMail>;
                    DropDownList ddlEmailType = (DropDownList)e.Row.FindControl("ddlEmailType");
                    if (ddlEmailType != null)
                    {
                        if (clEmails.Count > 0)
                        {
                            ddlEmailType.DataTextField = "EmailType";
                            ddlEmailType.DataValueField = "EmailTypeID";

                            ddlEmailType.DataSource = clEmails[0].EmailTypes;
                            ddlEmailType.DataBind();

                            ddlEmailType.ClearSelection();

                            cEMail SourceEmail = e.Row.DataItem as cEMail;

                            foreach (ListItem lItem in ddlEmailType.Items)
                            {
                                if (lItem.Value == SourceEmail.EmailTypeID.ToString())
                                {
                                    ddlEmailType.ClearSelection();
                                    lItem.Selected = true;
                                }
                            }
                        }
                    }

                    TextBox tbTemp;

                    tbTemp = (TextBox)e.Row.FindControl("tbEmail");
                    if (tbTemp != null)
                        tbTemp.Attributes.Add("PlaceHolder", "Email Address");
                }
            }
        }

        protected void btnAddEmail_Click(object sender, EventArgs e)
        {
            List<cEMail> clEMail = Session["dem_Emails"] as List<cEMail>;
            cEMail NewEmail = new cEMail();
            clEMail.Add(NewEmail);
            Session["dem_Emails"] = clEMail;
            BindEmails();

            gvEmails.EditIndex = gvEmails.Rows.Count - 1;
            BindEmails();
        }

        #endregion

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    if (ulFile.HasFile)
        //    {
        //        try
        //        {
        //            //If file is not an image do not let it in the system
        //            System.Drawing.Imaging.ImageFormat imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
        //            string fExt = ulFile.FileName.Substring(ulFile.FileName.IndexOf(".") + 1).ToUpper();
        //            string[] validImageExt = { "BMP", "GIF", "ICO", "JPG", "JPEG", "PNG" };

        //            if (!validImageExt.Contains(fExt))
        //            {
        //                lblMessage.Text = "Invalid File time. Supported types are " + string.Join(", ", validImageExt);
        //                return;
        //            }
        //            else
        //            {
        //                if (fExt == "BMP")
        //                    imgFormat = System.Drawing.Imaging.ImageFormat.Bmp;
        //                if (fExt == "GIF")
        //                    imgFormat = System.Drawing.Imaging.ImageFormat.Gif;
        //                if (fExt == "ICO")
        //                    imgFormat = System.Drawing.Imaging.ImageFormat.Icon;
        //                if (fExt == "PNG")
        //                    imgFormat = System.Drawing.Imaging.ImageFormat.Png;
        //            }

        //            string sUser = Session["LoginName"].ToString();
        //            Classes.cPicture NewPicture = new Classes.cPicture();
        //            NewPicture.PictureType = Classes.cPicture.PictureTypes.Player;
        //            NewPicture.CreateNewPictureRecord(sUser);
        //            string sExtension = Path.GetExtension(ulFile.FileName);
        //            ////The new picture should always be userId + '_2' so when we save the information we finalize it as userId + '_1'
        //            //NewPicture.PictureFileName = Demography.UserID.ToString() + "_2" + sExtension;
        //            //While the stored procedure is created I will safe the picture with the users name
        //            //                    NewPicture.PictureFileName = Demography.UserID.ToString() + "_" + ulFile.FileName;

        //            //Not sure I understand what this means
        //            //int iCharacterID = 0;
        //            //int.TryParse(Session["dem_Img"].ToString(), out iCharacterID);
        //            //NewPicture.CharacterID = iCharacterID;

        //            string LocalName = NewPicture.PictureLocalName;

        //            if (!Directory.Exists(Path.GetDirectoryName(NewPicture.PictureLocalName)))
        //                Directory.CreateDirectory(Path.GetDirectoryName(NewPicture.PictureLocalName));

        //            ulFile.SaveAs(NewPicture.PictureLocalName);
        //            NewPicture.Save(sUser);

        //            Session["UserIDPicture"] = NewPicture.PictureID;
        //            //Session["dem_Img_Id"] = NewPicture.PictureID;
        //            //Session["dem_Img_Url"] = NewPicture.PictureURL;
        //            imgPlayerImage.ImageUrl = NewPicture.PictureURL;

        //            /* If picture size if greater than half a megabyte we need to resize the image to 150 x 150 pixels*/
        //            //FileInfo fInfo = new FileInfo(NewPicture.PictureLocalName);
        //            //if (fInfo.Length > 500000)
        //            //{
        //            //    System.Drawing.Image image = System.Drawing.Bitmap.FromFile(NewPicture.PictureLocalName);
        //            //    System.Drawing.Image newImage = ScaleImage(image, 150, 150);
        //            //    newImage.Save(NewPicture.PictureURL, imgFormat);
        //            //}

        //        }
        //        catch (Exception ex)
        //        {
        //            lblMessage.Text = ex.Message + "<br>" + ex.StackTrace;
        //        }
        //    }
        //}

        private System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new System.Drawing.Bitmap(newWidth, newHeight);
            System.Drawing.Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        protected void btnSavePicture_Click(object sender, EventArgs e)
        {
            if (ulFile.HasFile)
            {
                try
                {
                    string sUser = Session["LoginName"].ToString();
                    Classes.cPicture NewPicture = new Classes.cPicture();
                    NewPicture.PictureType = Classes.cPicture.PictureTypes.Profile;
                    NewPicture.CreateNewPictureRecord(sUser);
                    string sExtension = Path.GetExtension(ulFile.FileName);
                    NewPicture.PictureFileName = "PL" + NewPicture.PictureID.ToString("D10") + sExtension;

                    int iCharacterID = 0;
                    int.TryParse(ViewState["CurrentCharacter"].ToString(), out iCharacterID);
                    NewPicture.CharacterID = iCharacterID;

                    string LocalName = NewPicture.PictureLocalName;

                    if (!Directory.Exists(Path.GetDirectoryName(NewPicture.PictureLocalName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(NewPicture.PictureLocalName));

                    ulFile.SaveAs(NewPicture.PictureLocalName);
                    NewPicture.Save(sUser);

                    ViewState["UserIDPicture"] = NewPicture.PictureID;
                    ViewState.Remove("PictureDeleted");

                    imgPlayerImage.ImageUrl = NewPicture.PictureURL;
                    imgPlayerImage.Visible = true;
                    btnClearPicture.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message + "<br>" + ex.StackTrace;
                }
            }
        }

        protected void btnClearPicture_Click(object sender, EventArgs e)
        {
            if (ViewState["UserIDPicture"] != null)
                ViewState.Remove("UserIDPicture");

            imgPlayerImage.Visible = false;
            btnClearPicture.Visible = false;

            //SortedList sParam = new SortedList();
            //sParam.Add("@CharacterID", Session["SelectedCharacter"].ToString());

            //Classes.cUtilities.LoadDataTable("uspClearCharacterProfilePicture", sParam, "LARPortal", Session["UserID"].ToString(), "CharInfo.btnClearPicture");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        }
    }
}
