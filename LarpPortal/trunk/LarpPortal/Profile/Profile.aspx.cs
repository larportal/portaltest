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
    public partial class Profile : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";
        public string _UserName = "";
        public int _UserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            if (Session["Username"] != null)
                _UserName = Session["Username"].ToString();
            if (Session["UserID"] != null)
                int.TryParse(Session["UserID"].ToString(), out _UserID);

            ddlEnterPhoneType.Attributes.Add("onchange", "DisplayPhoneProvider(this);");
            btnClosePhoneNumber.Attributes.Add("data-dismiss", "modal");
            btnCancelDeletePhoneNumber.Attributes.Add("data-dismiss", "modal");
            btnCloseEnterEMail.Attributes.Add("data-dismiss", "modal");
            btnCancelDeleteEMail.Attributes.Add("data-dismiss", "modal");
            btnCloseEnterAddress.Attributes.Add("data-dismiss", "modal");
            btnCancelDeleteAddress.Attributes.Add("data-dismiss", "modal");

            if (!IsPostBack)
            {
                ViewState["CurrentCharacter"] = "";
                tbFirstName.Attributes.Add("Placeholder", "First Name");
                tbLastName.Attributes.Add("Placeholder", "Last Name");
                lblMessage.Text = "";
                ViewState["NewRecCounter"] = -1;

                SortedList sParam = new SortedList();
                DataTable dtPhoneTypes = cUtilities.LoadDataTable("uspGetPhoneTypes", sParam, "LARPortal", _UserName, lsRoutineName);
                DataView dvPhoneTypes = new DataView(dtPhoneTypes, "", "SortOrder", DataViewRowState.CurrentRows);
                ddlEnterPhoneType.DataSource = dtPhoneTypes;
                ddlEnterPhoneType.DataTextField = "PhoneType";
                ddlEnterPhoneType.DataValueField = "PhoneTypeID";
                ddlEnterPhoneType.DataBind();

                sParam = new SortedList();
                DataTable dtPhoneProvider = cUtilities.LoadDataTable("uspGetPhoneProviders", sParam, "LARPortal", _UserName, lsRoutineName + ".uspGetPhoneProviders");
                ddlEnterProvider.DataSource = dtPhoneProvider;
                ddlEnterProvider.DataTextField = "ProviderName";
                ddlEnterProvider.DataValueField = "PhoneProviderID";
                ddlEnterProvider.DataBind();

                sParam = new SortedList();
                DataTable dtEMailTypes = cUtilities.LoadDataTable("uspGetEMailTypes", sParam, "LARPortal", _UserName, lsRoutineName + ".uspGetEMailTypes");
                ddlEnterEMailType.DataSource = dtEMailTypes;
                ddlEnterEMailType.DataTextField = "EMailType";
                ddlEnterEMailType.DataValueField = "EMailTypeID";
                ddlEnterEMailType.DataBind();

                sParam = new SortedList();
                DataTable dtAddressTypes = cUtilities.LoadDataTable("uspGetAddressTypes", sParam, "LARPortal", _UserName, lsRoutineName + ".uspGetAddressTypes");
                ddlEnterAddressType.DataSource = dtAddressTypes;
                ddlEnterAddressType.DataTextField = "AddressType";
                ddlEnterAddressType.DataValueField = "AddressTypeID";
                ddlEnterAddressType.DataBind();

                sParam = new SortedList();
                DataTable dtStateList = cUtilities.LoadDataTable("uspGetStateList", sParam, "LARPortal", _UserName, lsRoutineName + ".uspGetStateList");
                ddlEnterState.DataSource = dtStateList;
                ddlEnterState.DataTextField = "DisplayState";
                ddlEnterState.DataValueField = "StateID";
                ddlEnterState.DataBind();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

            Classes.cUser Demography = null;
            Classes.cPlayer PLDemography = null;

            Demography = new Classes.cUser(_UserName, "Password");
            PLDemography = new Classes.cPlayer(_UserID, _UserName);

            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
                Session["ActiveLeftNav"] = "Demographics";

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
                tbUserName.Text = _UserName;
                tbNickName.Text = Demography.NickName;
                tbPenName.Text = PLDemography.AuthorName;
                tbForumName.Text = Demography.ForumUserName;

//                Session["dem_Addresses"] = Demography.UserAddresses;
//                Session["dem_Phones"] = Demography.UserPhones;
//                Session["dem_Emails"] = Demography.UserEmails;

//                BindAddresses();
//                BindPhoneNumbers();
//                BindEmails();

                ddlGender.Attributes.Add("onchange", "DisplaySexOther(this);");
            }

            lblErrorMessage1.Text = "";
            lblErrorMessage2.Text = "";
            btnSave1.Enabled = true;
            btnSave2.Enabled = true;

            // Now we check to make sure that there is a least a single record and that it's valid.
            //List<cAddress> AllAddresses = Session["dem_Addresses"] as List<cAddress>;
            //int iBadAddresses = AllAddresses.Count(x => !x.IsValid());

            //List<cEMail> AllEmail = Session["dem_Emails"] as List<cEMail>;
            //int iBadEmail = AllEmail.Count(x => !x.IsValid());

            //List<cPhone> AllPhone = Session["dem_Phones"] as List<cPhone>;
            //int iBadPhone = AllPhone.Count(x => !x.IsValid());

            hidNumOfPhones.Value = Demography.UserPhones.Count.ToString();
            gvPhoneNumbers.DataSource = Demography.UserPhones;
            gvPhoneNumbers.DataBind();

            hidNumOfEMails.Value = Demography.UserEmails.Count.ToString();
            gvEmails.DataSource = Demography.UserEmails;
            gvEmails.DataBind();

            hidNumOfAddresses.Value = Demography.UserAddresses.Count.ToString();
            gvAddresses.DataSource = Demography.UserAddresses;
            gvAddresses.DataBind();

            if (    //(iBadAddresses > 0) ||
                //(iBadEmail > 0) ||
                //(iBadPhone > 0) ||
                (gvEmails.Rows.Count == 0))
            {
                //if (iBadAddresses > 0)
                //    lblErrorMessage1.Text = "* At least one of your addresses is not valid. ";
                if (gvEmails.Rows.Count == 0)
                    lblErrorMessage1.Text += "* You must have at least one valid email address. ";
                //if (iBadEmail > 0)
                //    lblErrorMessage1.Text += "* At least one of your email addresses is not valid. ";
                //if (iBadPhone > 0)
                //    lblErrorMessage1.Text += "* At least one of your phone numbers is not valid. ";
                lblErrorMessage1.Text = lblErrorMessage1.Text.Trim();
                btnSave1.Enabled = false;
            }

            lblErrorMessage2.Text = lblErrorMessage1.Text;
            btnSave2.Enabled = btnSave1.Enabled;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cUser Demography = new Classes.cUser(_UserName, "Password");
            cPlayer PLDemography = new Classes.cPlayer(_UserID, _UserName);

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
            //if (AddressesChangesValidate() == false)
            //    return;

            //if (PhoneNumbersChangesValidate() == false)
            //    return;

            //if (EmailsChangesValidate() == false)
            //    return;

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
           // AddressesChangesUpdate(Demography);
           // PhonesChangesUpdate(Demography);
           // EmailsChangesUpdate(Demography);

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

        //private void AddressesChangesUpdate(cUser Demography)
        //{
        //    List<cAddress> addresses = Session["dem_Addresses"] as List<cAddress>;

        //    //int userId = (int)Session["UserID"];
        //    //For each element of the original list perform one of the following:
        //    //If update, update information
        //    //If delete, mark record for delete
        //    //If new, mark record for adding
        //    cAddress a1 = null;

        //    if (Demography.UserAddresses != null)
        //    {
        //        foreach (cAddress a in Demography.UserAddresses) //state in database
        //        {
        //            a1 = addresses.FirstOrDefault(x => x.AddressID == a.AddressID); //If record not found in memory that means it was deleted
        //            if (a1 == null)
        //                a.SaveUpdate(_UserID, true);
        //            else
        //                a1.SaveUpdate(_UserID);
        //        }
        //    }

        //    a1 = addresses.FirstOrDefault(x => x.AddressID <= 0);
        //    if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
        //    {
        //        a1.SaveUpdate(_UserID);
        //    }
        //}

        //private bool AddressesChangesValidate()
        //{
        //    // First check if a new valid record was inserted
        //    if (Session["dem_Addresses"] != null)
        //    {
        //        List<cAddress> addresses = Session["dem_Addresses"] as List<cAddress>;

        //        foreach (cAddress a in addresses)
        //        {
        //            if (a.AddressID > 0 && !a.IsValid()) //if new records are invalid that is okay they will not make it to the database.
        //            {
        //                lblMessage.Text = a.strErrorDescription;
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        //private void BindAddresses()
        //{
        //    MethodBase lmth = MethodBase.GetCurrentMethod();
        //    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

        //    List<cAddress> Addresses = Session["dem_Addresses"] as List<cAddress>;
        //    gvAddresses.DataSource = Addresses;
        //    gvAddresses.DataBind();
        //}

        //protected void gvAddresses_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvAddresses.EditIndex = e.NewEditIndex;
        //    BindAddresses();
        //}

        //protected void gvAddresses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    List<cAddress> clAddresses = Session["dem_Addresses"] as List<cAddress>;

        //    if (e.RowIndex < clAddresses.Count)
        //    {
        //        cAddress UpdatedAddress = clAddresses[e.RowIndex];

        //        TextBox tbAddress1 = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbAddress1");
        //        if (tbAddress1 != null)
        //            UpdatedAddress.Address1 = tbAddress1.Text;
        //        TextBox tbCity = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbCity");
        //        if (tbCity != null)
        //            UpdatedAddress.City = tbCity.Text;
        //        TextBox tbPostalCode = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbPostalCode");
        //        if (tbPostalCode != null)
        //            UpdatedAddress.PostalCode = tbPostalCode.Text;

        //        if (!UpdatedAddress.IsValid())
        //        {
        //            lblMessage.Text = UpdatedAddress.strErrorDescription;
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            TextBox tbAddress2 = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbAddress2");
        //            if (tbAddress2 != null)
        //                UpdatedAddress.Address2 = tbAddress2.Text;
        //            TextBox tbState = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbState");
        //            if (tbState != null)
        //                UpdatedAddress.StateID = tbState.Text;
        //            TextBox tbCountry = (TextBox)gvAddresses.Rows[e.RowIndex].FindControl("tbCountry");
        //            if (tbCountry != null)
        //                UpdatedAddress.Country = tbCountry.Text;
        //            DropDownList ddlAddressType = (DropDownList)gvAddresses.Rows[e.RowIndex].FindControl("ddlAddressType");
        //            if (ddlAddressType != null)
        //            {
        //                UpdatedAddress.AddressTypeID = ddlAddressType.SelectedValue.ToInt32();
        //            }
        //            RadioButton rbPrimary = (RadioButton)gvAddresses.Rows[e.RowIndex].FindControl("rbPrimary");
        //            if (rbPrimary != null)
        //            {
        //                if (rbPrimary.Checked)
        //                {
        //                    clAddresses.ForAll(x => x.IsPrimary = false);
        //                    UpdatedAddress.IsPrimary = true;
        //                }
        //            }
        //            Session["dem_Addresses"] = clAddresses;
        //            gvAddresses.EditIndex = -1;
        //        }
        //        BindAddresses();
        //    }
        //    else
        //        e.Cancel = true;
        //}

        //protected void gvAddresses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvAddresses.EditIndex = -1;
        //    BindAddresses();
        //}

        //protected void gvAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    List<cAddress> clAddresses = Session["dem_Addresses"] as List<cAddress>;

        //    if (e.RowIndex < clAddresses.Count)
        //    {
        //        clAddresses.RemoveAt(e.RowIndex);
        //    }

        //    Session["dem_Addresses"] = clAddresses;

        //    BindAddresses();
        //}

        protected void gvAddresses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hidNumOfAddresses.Value == "1")
                {
                    ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
                    if (ibtnDelete != null)
                        ibtnDelete.Attributes.Add("style", "display:none");
                }
            }
        }

        protected void btnSaveAddress_Click(object sender, EventArgs e)
        {
            int iAddressID;
            if (int.TryParse(hidAddressID.Value, out iAddressID))
            {
                cAddress UpdateAddress = new cAddress(iAddressID, _UserName, _UserID);
                UpdateAddress.AddressID = iAddressID;
                UpdateAddress.Address1 = tbEnterAddress1.Text;
                UpdateAddress.Address2 = tbEnterAddress2.Text;
                UpdateAddress.City = tbEnterCity.Text;
                UpdateAddress.StateID = ddlEnterState.SelectedValue;
                UpdateAddress.PostalCode = tbEnterZipCode.Text;
                UpdateAddress.Country = tbEnterCountry.Text;
                UpdateAddress.IsPrimary = cbxEnterAddressPrimary.Checked;

                int iTemp;
                if (int.TryParse(ddlEnterAddressType.SelectedValue, out iTemp))
                    UpdateAddress.AddressTypeID = iTemp;

                UpdateAddress.SaveUpdate(_UserID);
            }
        }

        protected void btnDeleteAddress_Click(object sender, EventArgs e)
        {
            int iAddressID;
            if (int.TryParse(hidDeleteAddressID.Value, out iAddressID))
            {
                cAddress UpdateAddress = new cAddress(iAddressID, _UserName, _UserID);
                UpdateAddress.SaveUpdate(_UserID, true);
            }
        }

        #endregion

        #region PhoneNumbers

        protected void gvPhoneNumbers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hidNumOfPhones.Value == "1")
                {
                    ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
                    if (ibtnDelete != null)
                        ibtnDelete.Attributes.Add("style", "display:none");
                }
            }
        }

        protected void btnSavePhone_Click(object sender, EventArgs e)
        {
            int iPhoneNumberID;
            if (int.TryParse(hidEnterPhoneID.Value, out iPhoneNumberID))
            {
                cPhone UpdatePhone = new cPhone(iPhoneNumberID, _UserID, _UserName);
                UpdatePhone.PhoneNumberID = iPhoneNumberID;
                UpdatePhone.AreaCode = tbEnterAreaCode.Text;
                UpdatePhone.PhoneNumber = tbEnterPhoneNumber.Text;
                UpdatePhone.Extension = tbEnterExtension.Text;
                UpdatePhone.IsPrimary = cbxEnterPrimary.Checked;

                int iTemp;
                if (int.TryParse(ddlEnterPhoneType.SelectedValue, out iTemp))
                    UpdatePhone.PhoneTypeID = iTemp;
                if (int.TryParse(ddlEnterProvider.SelectedValue, out iTemp))
                    UpdatePhone.ProviderID = iTemp;

                UpdatePhone.SaveUpdate(_UserID);
            }
        }

        protected void btnDeletePhone_Click(object sender, EventArgs e)
        {
            int iPhoneNumberID;
            if (int.TryParse(hidDeletePhoneID.Value, out iPhoneNumberID))
            {
                cPhone UpdatePhone = new cPhone(iPhoneNumberID, _UserID, _UserName);
                UpdatePhone.SaveUpdate(_UserID, true);
            }
        }

        #endregion

        #region Emails

        protected void gvEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hidNumOfEMails.Value == "1")
                {
                    ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
                    if (ibtnDelete != null)
                        ibtnDelete.Attributes.Add("style", "display:none");
                }
            }
        }

        protected void btnSaveEMail_Click(object sender, EventArgs e)
        {
            int iEMailID;
            if (int.TryParse(hidEnterEMailID.Value, out iEMailID))
            {
                cEMail UpdateEMail = new cEMail(iEMailID, _UserName, _UserID);
                UpdateEMail.EMailID = iEMailID;
                UpdateEMail.EmailAddress = tbEnterEMailAddress.Text;

                int iTemp;
                if (int.TryParse(ddlEnterEMailType.SelectedValue, out iTemp))
                    UpdateEMail.EmailTypeID = iTemp;
                UpdateEMail.IsPrimary = cbxEnterEMailPrimary.Checked;

                UpdateEMail.SaveUpdate(_UserID);
            }
        }

        protected void btnDeleteEMail_Click(object sender, EventArgs e)
        {
            int iEMailID;
            if (int.TryParse(hidEnterEMailID.Value, out iEMailID))
            {
                cEMail UpdateEMail = new cEMail(iEMailID, _UserName, _UserID);
                UpdateEMail.SaveUpdate(_UserID, true);
            }
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
