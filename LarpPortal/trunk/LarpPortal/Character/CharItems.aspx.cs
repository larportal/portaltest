using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharItems : System.Web.UI.Page
    {
        public string PictureDirectory = "../Pictures";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["CurrentCharacter"] == null)
                ViewState["CurrentCharacter"] = "";
            if (Session["Items"] == null)
                Session["Items"] = "";
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Type comes from Chcharactertype - type
            // select * from mdbStatus for Status

            // 
            if (Session["SelectedCharacter"] != null)
            {
                string sCurrent = ViewState["CurrentCharacter"].ToString();
                string sSelected = Session["SelectedCharacter"].ToString();
                if ((!IsPostBack) || (ViewState["CurrentCharacter"].ToString() != Session["SelectedCharacter"].ToString()))
                {
                    int iCharID;
                    if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                    {
                        Classes.cCharacter cChar = new Classes.cCharacter();
                        cChar.LoadCharacter(iCharID);

                        taCostume.InnerText = cChar.Costuming;
                        taWeapons.InnerText = cChar.Weapons;
                        taMakeup.InnerText = cChar.Makeup;
                        taItemsOther.InnerText = cChar.Items;

                        DataTable dtPictures = new DataTable();
                        dtPictures = Classes.cUtilities.CreateDataTable(cChar.Pictures);     // CreateDataTable(cChar.Pictures);
                        Session["Items"] = cChar.Pictures;

                        string sFilter = "RecordStatus = '" + Classes.RecordStatuses.Active.ToString() + "'";
                        DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                        dlItems.DataSource = dvPictures;
                        dlItems.DataBind();
                    }
                    ViewState["CurrentCharacter"] = Session["SelectedCharacter"];
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuItem.HasFile)
            {
                string sUser = Session["LoginName"].ToString();
                Classes.cPicture NewPicture = new Classes.cPicture();
                NewPicture.CreateNewPictureRecord(sUser);
                string sExtension = Path.GetExtension(fuItem.FileName);
                string filename = "CI" + NewPicture.PictureID.ToString("D10") + sExtension;

                if (!Directory.Exists(PictureDirectory))
                    Directory.CreateDirectory(PictureDirectory);

                string FinalFileName = Path.Combine(Server.MapPath(PictureDirectory), filename);
                fuItem.SaveAs(FinalFileName);

                NewPicture.PictureFileName = filename;
                NewPicture.Save(sUser);

                List<Classes.cPicture> Items = new List<Classes.cPicture>();
                Items = Session["Items"] as List<Classes.cPicture>;
                Items.Add(NewPicture);
                Session["Items"] = Items;

                DataTable dtPictures = Classes.cUtilities.CreateDataTable(Items);

                string sFilter = "RecordStatus = '" + Classes.RecordStatuses.Active.ToString() + "'";
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Classes.cCharacter cChar = new Classes.cCharacter();

            int iCharNum;

            if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharNum))
            {
                cChar.LoadCharacter(iCharNum);

                cChar.Costuming = taCostume.InnerText;
                cChar.Makeup = taMakeup.InnerText;
                cChar.Weapons = taWeapons.InnerText;
                cChar.Items = taItemsOther.InnerText;
                cChar.Pictures = Session["Items"] as List<Classes.cPicture>;

                cChar.SaveCharacter(Session["LoginName"].ToString());
            }
        }

        protected void dlItems_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            int PictureID;
            if (int.TryParse(e.CommandArgument.ToString(), out PictureID))
            {
                List<Classes.cPicture> Items = new List<Classes.cPicture>();
                Items = Session["Items"] as List<Classes.cPicture>;

                foreach (Classes.cPicture Pict in Items)
                {
                    if (Pict.PictureID == PictureID)
                        Pict.RecordStatus = Classes.RecordStatuses.Delete;
                }
                Session["Items"] = Items;
                DataTable dtPictures = Classes.cUtilities.CreateDataTable(Items);
                string sFilter = "RecordStatus = '" + Classes.RecordStatuses.Active.ToString() + "'";
                DataView dvPictures = new DataView(dtPictures, sFilter, "", DataViewRowState.CurrentRows);
                dlItems.DataSource = dvPictures;
                dlItems.DataBind();
            }
        }
    }
}
