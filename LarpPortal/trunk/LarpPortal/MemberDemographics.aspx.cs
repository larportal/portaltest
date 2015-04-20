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
                emergencyContactPhone = PLDemography.EmergencyContactPhone;
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
                
                List<cAddress> _addresses = new List<cAddress>(); 
                if (Demography.UserAddresses != null)
                    _addresses = Demography.UserAddresses.ToList();
                _addresses.Add(new cAddress()); //always add an empty one in case they need to insert a new one
                
                Session["dem_Addresses"] = _addresses;

                List<cPhone> _phones = new List<cPhone>();
                if (Demography.UserPhones != null)
                    _phones = Demography.UserPhones.ToList();
                _phones.Add(new cPhone());
                
                Session["dem_Phones"] = _phones;

                List<cEMail> _emails = new List<cEMail>();
                if (Demography.UserEmails != null)
                    _emails = Demography.UserEmails.ToList();
                _emails.Add(new cEMail());

                Session["dem_Emails"] = _emails;

                BindAllGrids();
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
            //Since no matter what happens in this routine we want to preserve the state of the grids, we bind them now
            BindAllGrids();

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
            else 
            {
                lblMessage.Text = "Please enter a valid date";
                txtDOB.Focus();
                return;
            }

            PLDemography.AuthorName = txtPenname.Text;
            Demography.ForumUserName = txtForumname.Text;

            PLDemography.EmergencyContactName = txtEmergencyName.Text;

            // Using the inital records merge result with the new ones.
            if (AddressesChangesValidate() == false)
                return;

            if (PhoneNumbersChangesValidate() == false)
                return;

            if (EmailsChangesValidate() == false)
                return;

            if (!cPhone.isValidPhoneNumber(txtEmergencyPhone.Text, 10))
            {
                lblMessage.Text = cPhone.ErrorDescription;
                txtEmergencyPhone.Focus();
                return;
            }

            if (PLDemography.EmergencyContactPhone != txtEmergencyPhone.Text)
            {
                PLDemography.EmergencyContactPhone = txtEmergencyPhone.Text;
            }
            
            /* 3) handle picture update/add.
             */

            //At this point all validation must have been done so it is time to merge lists 
            AddressesChangesUpdate();
            PhonesChangesUpdate();
            EmailsChangesUpdate();

            /* As I validate I will place the new values on the component prior to saving the values*/
            Demography.Save();
            PLDemography.Save();

            lblMessage.Text = "Changes saved successfully.";
        }

        private void PhonesChangesUpdate()
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
                    a.SaveUpdate(userId, a1 == null);
                }
            }

            a1 = phones.FirstOrDefault(x => x.PhoneNumberID <= 0);
            if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
            {
                a1.SaveUpdate(userId);
            }
        }

        private void EmailsChangesUpdate()
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
                    a.SaveUpdate(userId, a1 == null);
                }
            }

            a1 = emails.FirstOrDefault(x => x.EMailID <= 0);
            if (a1 != null && a1.IsValid()) // If new and valid, let push it to the database
            {
                a1.SaveUpdate(userId);
            }
        }
        
        private void AddressesChangesUpdate()
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
                    a1 = addresses.FirstOrDefault(x => x.IntAddressID == a.IntAddressID); //If record not found in memory that means it was deleted
                    a.SaveUpdate(userId, a1 == null);                    
                }
            }

            a1 = addresses.FirstOrDefault(x => x.IntAddressID <= 0);
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

        private bool AddressesChangesValidate()
        {
            // First check if a new valid record was inserted
            if (Session["dem_Addresses"] != null)
            {                
                List<cAddress> addresses = Session["dem_Addresses"] as List<cAddress>;

                foreach (cAddress a in addresses)
                {
                    if (a.IntAddressID > 0 && !a.IsValid()) //if new records are invalid that is okay they will not make it to the database.
                    {
                        lblMessage.Text = a.strErrorDescription;
                        return false;
                    }                    
                }
            }

            return true;
        }
                
        protected void gv_Address_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<cAddress> addresses = null;
            int gindex = -1;
            cAddress address = null;
            if (int.TryParse(e.CommandArgument.ToString(), out gindex))
            {
                addresses = Session["dem_Addresses"] as List<Classes.cAddress>;
                if (gindex < addresses.Count())
                    address = addresses[gindex];
            }
            else
            {
                //maybe send error on the screen to notify that the record does not exists
                return;
            }

            switch (e.CommandName.ToUpper())
            {
                //case "DELETEITEM":
                //    {
                //        if (address != null) //We need to have at least one record so we adding can be possible
                //        {
                //            if (addresses.Count > 0)
                //            {
                //                addresses.Remove(address);
                //            }
                //            else //If they want to delete the only item we have, we still need an empty element to add information.....
                //            {
                //                addresses = new List<cAddress>() { new cAddress() };
                //            }
                //            Session["dem_Addresses"] = addresses;
                //            BindAllGrids();
                //        }
                //        break;
                //    }

                case "EDIT":
                case "EDITITEM":
                    {
                        if (address != null)
                        {
                            gv_Address.EditIndex = gindex;
                        }
                        BindAllGrids();
                        break;
                    }
            }
        }

        private void BindAllGrids()
        {
            gv_Address.DataSource = Session["dem_Addresses"] as List<cAddress>;
            gv_Address.DataBind();

            gv_PhoneNums.DataSource = Session["dem_Phones"] as List<cPhone>;
            gv_PhoneNums.DataBind();

            gv_Emails.DataSource = Session["dem_Emails"] as List<cEMail>;
            gv_Emails.DataBind();
        }

        protected void gv_Address_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = (GridView)sender;
            // Change the row state
            gv.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;
        }

        protected void gv_Address_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {            
            List<cAddress> addresses = null;
            int gindex = e.RowIndex;
            cAddress address = new cAddress();
            GridView gv = (GridView)sender;
            addresses = Session["dem_Addresses"] as List<Classes.cAddress>;
            if (gindex < addresses.Count())
            {

                address.StrAddress1 = (e.NewValues["StrAddress1"] + string.Empty).ToString().Trim();
                address.StrAddress2 = (e.NewValues["StrAddress2"] + string.Empty).ToString().Trim();
                address.StrCity = (e.NewValues["StrCity"] + string.Empty).ToString().ToUpper().Trim();
                address.StrStateID = (e.NewValues["StrStateID"] + string.Empty).ToString().Trim();
                address.StrPostalCode = (e.NewValues["StrPostalCode"] + string.Empty).ToString().Trim();
                address.StrCountry = (e.NewValues["StrCountry"] + string.Empty).ToString().ToUpper().Trim();
                int iRetVal = 0;
                int.TryParse((gv.Rows[gindex].FindControl("ddAddressType") as DropDownList).SelectedValue, out iRetVal); //only native types can be returned so temp variable
                address.IntAddressTypeID = iRetVal;
                address.IsPrimary = (gv.Rows[gindex].FindControl("rbtnPrimary") as RadioButton).Checked;
                if (address.IsValid())
                {
                    addresses[gindex].StrAddress1 = address.StrAddress1;
                    addresses[gindex].StrAddress2 = address.StrAddress2;
                    addresses[gindex].StrCity = address.StrCity;
                    addresses[gindex].StrStateID = address.StrStateID;
                    addresses[gindex].StrPostalCode = address.StrPostalCode;
                    addresses[gindex].StrCountry = address.StrCountry;
                    addresses[gindex].IntAddressTypeID = address.IntAddressTypeID;
                    addresses[gindex].IsPrimary = address.IsPrimary;
                    if (addresses[gindex].IntAddressID < 1)//Always make sure that there is an extra records to edit, since there is not option to add records
                        addresses.Add(new cAddress());

                    Session["dem_Addresses"] = addresses;
                    gv.Rows[gindex].RowState = DataControlRowState.Normal;
                    gv_Address.EditIndex = -1;
                }
                else
                {
                    lblMessage.Text = address.strErrorDescription;
                    e.Cancel = true;
                }
            }
            BindAllGrids();
        }

        protected void gv_Address_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Address.EditIndex = -1;
            BindAllGrids();
        }
        
        protected void gv_Address_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cAddress> addresses = Session["dem_Addresses"] as List<Classes.cAddress>;
            if (e.RowIndex < addresses.Count)
            {
                addresses.RemoveAt(e.RowIndex); //always delete because it they are tring to delete the empty row it is better to delete and add the row, rather than to clear all properties
            }
                
            if (addresses.Count == 0)   //Always make sure that there is one empty row for users to add information
                addresses.Add(new cAddress()); 

            Session["dem_Addresses"] = addresses;

            BindAllGrids();
        }

        protected void gv_Address_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCategories = e.Row.FindControl("ddAddressType") as DropDownList;
                if (ddlCategories != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    ddlCategories.SelectedValue = (e.Row.DataItem as cAddress).IntAddressTypeID.ToString();
                    ddlCategories.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        ddlCategories.Enabled = true;
                }

                RadioButton rbtn = e.Row.FindControl("rbtnPrimary") as RadioButton;
                if (rbtn != null)
                {
                    rbtn.Checked = (e.Row.DataItem as cAddress).IsPrimary;
                    rbtn.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        rbtn.Enabled = true;
                }
            }
        }

        protected void gv_PhoneNums_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<cPhone> phones = null;
            int gindex = -1;
            cPhone phone = null;
            if (int.TryParse(e.CommandArgument.ToString(), out gindex))
            {
                phones = Session["dem_Phones"] as List<cPhone>;
                if (gindex < phones.Count())
                    phone = phones[gindex];
            }
            else
            {
                return;
            }
            switch (e.CommandName.ToUpper())
            {
                case "EDIT":
                case "EDITITEM":
                    {
                        if (phones != null)
                        {
                            gv_PhoneNums.EditIndex = gindex;
                        }
                        BindAllGrids();
                        break;
                    }
            }
        }

        protected void gv_PhoneNums_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox gv_PhoneNumber = e.Row.FindControl("gv_txtPhoneNumber") as TextBox;
                if (gv_PhoneNumber != null)
                {
                    gv_PhoneNumber.Text =((e.Row.DataItem as cPhone).PhoneNumber + string.Empty);
                    Int32 iValue = 0;
                    // + string.Empty is a trick to avoid blow ups for null values. it defaults to empty string, 
                    if (Int32.TryParse(gv_PhoneNumber.Text.Trim().Replace("-", string.Empty), out iValue))
                    {
                        gv_PhoneNumber.Text = String.Format("{0:###-####}", iValue);
                    }

                    gv_PhoneNumber.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                    {
                        gv_PhoneNumber.Enabled = true;
                    }                    
                }

                DropDownList ddlCategories = e.Row.FindControl("ddPhoneNumber") as DropDownList;
                if (ddlCategories != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    ddlCategories.SelectedValue = (e.Row.DataItem as cPhone).PhoneTypeID.ToString();
                    ddlCategories.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        ddlCategories.Enabled = true;
                }

                RadioButton rbtn = e.Row.FindControl("rbtnPrimary1") as RadioButton;
                if (rbtn != null)
                {
                    rbtn.Checked = (e.Row.DataItem as cPhone).IsPrimary;
                    rbtn.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        rbtn.Enabled = true;
                }
            }
        }

        protected void gv_PhoneNums_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = (GridView)sender;
            // Change the row state
            gv.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;
        }

        protected void gv_PhoneNums_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<cPhone> phones = null;
            int gindex = e.RowIndex;
            cPhone phone = new cPhone(); ;
            GridView gv = (GridView)sender;
            phones = Session["dem_Phones"] as List<Classes.cPhone>;
            if (gindex < phones.Count())
            {

                phone.AreaCode = (e.NewValues["AreaCode"] + string.Empty).ToString().Trim();
                phone.PhoneNumber = (e.NewValues["PhoneNumber"] + string.Empty).ToString().Trim();
                phone.Extension = (e.NewValues["Extension"] + string.Empty).ToString().ToUpper().Trim();
                int iRetVal = 0;
                int.TryParse((gv.Rows[gindex].FindControl("ddPhoneNumber") as DropDownList).SelectedValue, out iRetVal); //only native types can be returned so temp variable
                phone.PhoneTypeID = iRetVal;
                phone.IsPrimary = (gv.Rows[gindex].FindControl("rbtnPrimary1") as RadioButton).Checked;
                if (phone.IsValid())
                {
                    phones[gindex].AreaCode = phone.AreaCode;
                    phones[gindex].PhoneNumber = phone.PhoneNumber;
                    phones[gindex].Extension = phone.Extension;
                    phones[gindex].PhoneTypeID = phone.PhoneTypeID;
                    phones[gindex].IsPrimary = phone.IsPrimary;
                    if (phones[gindex].PhoneNumberID < 1)
                        phones.Add(new cPhone());

                    Session["dem_Phones"] = phones;
                    gv.Rows[gindex].RowState = DataControlRowState.Normal;
                    gv_PhoneNums.EditIndex = -1;
                }
                else
                {
                    lblMessage.Text = phone.strErrorDescription;
                    e.Cancel = true;
                }
            }
            BindAllGrids();

        }

        protected void gv_PhoneNums_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_PhoneNums.EditIndex = -1;
            BindAllGrids();
        }

        protected void gv_PhoneNums_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cPhone> phones = Session["dem_Phones"] as List<Classes.cPhone>;
            if (e.RowIndex < phones.Count)
            {
                phones.RemoveAt(e.RowIndex); //always delete because it they are tring to delete the empty row it is better to delete and add the row, rather than to clear all properties
            }

            if (phones.Count == 0)   //Always make sure that there is one empty row for users to add information
                phones.Add(new cPhone());

            Session["dem_Phones"] = phones;

            BindAllGrids();
        }

        protected void gv_Emails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<cEMail> emails = null;
            int gindex = -1;
            cEMail email = null;
            if (int.TryParse(e.CommandArgument.ToString(), out gindex))
            {
                emails = Session["dem_Emails"] as List<cEMail>;
                if (gindex < emails.Count())
                    email = emails[gindex];
            }
            else
            {
                return;
            }
            switch (e.CommandName.ToUpper())
            {
                case "EDIT":
                case "EDITITEM":
                    {
                        if (emails != null)
                        {
                            gv_Emails.EditIndex = gindex;
                        }
                        BindAllGrids();
                        break;
                    }
            }
        }

        protected void gv_Emails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCategories = e.Row.FindControl("ddEmailTypeId") as DropDownList;
                if (ddlCategories != null)
                {
                    //Get the data from DB and bind the dropdownlist
                    ddlCategories.SelectedValue = (e.Row.DataItem as cEMail).EmailTypeID.ToString();
                    ddlCategories.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        ddlCategories.Enabled = true;
                }

                RadioButton rbtn = e.Row.FindControl("rbtnPrimary2") as RadioButton;
                if (rbtn != null)
                {
                    rbtn.Checked = (e.Row.DataItem as cEMail).IsPrimary;
                    rbtn.Enabled = false;
                    if (e.Row.RowState.ToString().Contains(DataControlRowState.Edit.ToString()))
                        rbtn.Enabled = true;
                }
            }
        }

        protected void gv_Emails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;
        }

        protected void gv_Emails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<cEMail> emails = null;
            int gindex = e.RowIndex;
            cEMail email = new cEMail();
            GridView gv = (GridView)sender;
            emails = Session["dem_Emails"] as List<cEMail>;
            if (gindex < emails.Count())
            {
                email.EmailAddress = (e.NewValues["EmailAddress"] + string.Empty).ToString().Trim();
                int iRetVal = 0;
                int.TryParse((gv.Rows[gindex].FindControl("ddEmailTypeId") as DropDownList).SelectedValue, out iRetVal); //only native types can be returned so temp variable
                email.EmailTypeID = iRetVal;
                email.IsPrimary = (gv.Rows[gindex].FindControl("rbtnPrimary2") as RadioButton).Checked;
                if (email.IsValid())
                {
                    emails[gindex].EmailAddress = email.EmailAddress;
                    emails[gindex].EmailTypeID = email.EmailTypeID;
                    emails[gindex].IsPrimary = email.IsPrimary;
                    if (emails[gindex].EMailID < 1)
                        emails.Add(new cEMail());

                    Session["dem_Emails"] = emails;
                    gv.Rows[gindex].RowState = DataControlRowState.Normal;
                    gv_Emails.EditIndex = -1;
                }
                else
                {
                    lblMessage.Text = email.strErrorMessage;
                    e.Cancel = true;
                }
                BindAllGrids();
            }
        }

        protected void gv_Emails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Emails.EditIndex = -1;
            BindAllGrids();
        }

        protected void gv_Emails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<cEMail> emails = Session["dem_Emails"] as List<Classes.cEMail>;
            if (e.RowIndex < emails.Count)
            {
                emails.RemoveAt(e.RowIndex);
            }

            if (emails.Count == 0)
            {
                emails.Add(new cEMail());
            }

            Session["dem_Emails"] = emails;

            BindAllGrids();
        }        
    }
}