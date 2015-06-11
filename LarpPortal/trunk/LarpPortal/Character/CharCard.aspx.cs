using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Character
{
    public partial class CharCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SelectedCharacter"] != null)
            {
                Classes.cCharacter cChar = new Classes.cCharacter();
                int iCharID = 0;
                if (int.TryParse(Session["SelectedCharacter"].ToString(), out iCharID))
                {
                    cChar.LoadCharacter(iCharID);
                    lblCharName.Text = cChar.AKA + " - " + cChar.CampaignName;
                    lblAKA.Text = cChar.AKA;
                    lblFullName.Text = cChar.FirstName;
                    if (cChar.MiddleName.Trim().Length > 0)
                        lblFullName.Text += " " + cChar.MiddleName;
                    if (cChar.LastName.Trim().Length > 0)
                        lblFullName.Text += " " + cChar.LastName;
                    lblFullName.Text.Trim();

                    lblRace.Text = cChar.Race.FullRaceName;
                    lblOrigin.Text = cChar.WhereFrom;
                    lblPlayerName.Text = "";

                    DataTable dtSkills = Classes.cUtilities.CreateDataTable(cChar.CharacterSkills);
                    if (dtSkills.Columns["FullDescription"] == null)
                        dtSkills.Columns.Add(new DataColumn("FullDescription", typeof(string)));

                    foreach (DataRow dRow in dtSkills.Rows)
                    {
                        string FullDesc = "";
                        bool bDisplay;
                        if (bool.TryParse(dRow["CardDisplayDescription"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                string SkillDescription = "";
                                if (dRow["SkillCardDescription"].ToString().Length > 0)
                                    SkillDescription = dRow["SkillCardDescription"].ToString();
                                else
                                    SkillDescription = dRow["SkillShortDescription"].ToString();
                                if (SkillDescription.Length > 0)
                                    FullDesc += SkillDescription + "<br>";
                            }
                        }

                        if (dRow["PlayerDescription"].ToString().Trim().Length > 0)
                            FullDesc += dRow["PlayerDescription"].ToString().Trim() + "<br>";

                        if (bool.TryParse(dRow["CardDisplayIncant"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                string SkillIncant = "";
                                if (dRow["SkillIncant"].ToString().Length > 0)
                                    SkillIncant = dRow["SkillIncant"].ToString();
                                //else      Is there a card Incant?
                                //    SkillDescription = dRow["SkillShortDescription"].ToString();
                                if (SkillIncant.Length > 0)
                                    FullDesc += SkillIncant + "<br>";
                            }
                        }

                        string sPlayerIncant;
                        if (dRow["PlayerIncant"].ToString().Trim().Length > 0)
                            FullDesc += dRow["PlayerIncant"].ToString().Trim();

                        if (FullDesc.EndsWith("<br>"))
                            FullDesc = FullDesc.Substring(0, FullDesc.Length - 4);

                        dRow["FullDescription"] = FullDesc;
                    }

                    Dictionary<string, string> NonCost = new Dictionary<string, string>();

                    foreach (Classes.cDescriptor Desc in cChar.Descriptors)
                    {
                        if (NonCost.ContainsKey(Desc.CharacterDescriptor))
                            NonCost[Desc.CharacterDescriptor] += Desc.DescriptorValue + ", ";
                        else
                            NonCost.Add(Desc.CharacterDescriptor, Desc.DescriptorValue + ", ");
                    }

                    foreach (string KeyValue in NonCost.Keys.ToList())
                    {
                        if (NonCost[KeyValue].EndsWith(", "))
                            NonCost[KeyValue] = NonCost[KeyValue].Substring(0, NonCost[KeyValue].Length - 2);
                    }

                    gvSkills.DataSource = dtSkills;
                    gvSkills.DataBind();

                    //short Desc Or Card Desc
                    //player description

                    gvNonCost.DataSource = NonCost;
                    gvNonCost.DataBind();

                    Classes.cUser User = new Classes.cUser(Session["Username"].ToString(), "PasswordNotNeeded");
                    lblPlayerName.Text = User.FirstName + " " + User.MiddleName + " " + User.LastName;
                }
            }
        }
    }
}