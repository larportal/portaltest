using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

                    lblTotalCP.Text = cChar.TotalCP.ToString("0.00");

                    DataSet dsCampaignSkills = new DataSet();
                    SortedList sParams = new SortedList();
                    sParams.Add("@CampaignID", cChar.CampaignID);

                    MethodBase lmth = MethodBase.GetCurrentMethod();
                    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                    dsCampaignSkills = Classes.cUtilities.LoadDataSet("uspGetCampaignSkills", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                    DataTable dtSkills = Classes.cUtilities.CreateDataTable(cChar.CharacterSkills);
                    if (dtSkills.Columns["FullDescription"] == null)
                        dtSkills.Columns.Add(new DataColumn("FullDescription", typeof(string)));
                    if (dtSkills.Columns["DisplaySkill"] == null)
                        dtSkills.Columns.Add(new DataColumn("DisplaySkill", typeof(Boolean)));

                    double CPCost;
                    double CPSpent = 0.0;

                    foreach (DataRow dSkillRow in dtSkills.Rows)
                    {
                        if (double.TryParse(dSkillRow["CPCostPaid"].ToString(), out CPCost))
                            CPSpent += CPCost;

                        dSkillRow["DisplaySkill"] = true;
                        DataView dvPreReqs = new DataView(dsCampaignSkills.Tables[1], "CampaignSkillsStandardID = " + dSkillRow["CampaignSkillsStandardID"].ToString() + 
                            " and SuppressPrerequisiteSkill = 1", "", DataViewRowState.CurrentRows);
                        foreach (DataRowView dPreReq in dvPreReqs)
                        {
                            DataRow[] drPreReq = dtSkills.Select("CampaignSkillsStandardID = " + dPreReq["PreRequisiteSkillID"].ToString());
                            foreach (DataRow d in drPreReq)
                                d["DisplaySkill"] = false;
                        }

                        string FullDesc = "";
                        bool bDisplay;
                        if (bool.TryParse(dSkillRow["CardDisplayDescription"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                string SkillDescription = "";
                                if (dSkillRow["SkillCardDescription"].ToString().Length > 0)
                                    SkillDescription = dSkillRow["SkillCardDescription"].ToString();
                                else
                                    SkillDescription = dSkillRow["SkillShortDescription"].ToString();
                                if (SkillDescription.Length > 0)
                                    FullDesc += SkillDescription + "; ";
                            }
                        }

                        if (dSkillRow["PlayerDescription"].ToString().Trim().Length > 0)
                            FullDesc += dSkillRow["PlayerDescription"].ToString().Trim() + "; ";

                        if (bool.TryParse(dSkillRow["CardDisplayIncant"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                string SkillIncant = "";
                                if (dSkillRow["SkillIncant"].ToString().Length > 0)
                                    SkillIncant = dSkillRow["SkillIncant"].ToString();
                                //else      Is there a card Incant?
                                //    SkillDescription = dRow["SkillShortDescription"].ToString();
                                if (SkillIncant.Length > 0)
                                    FullDesc += SkillIncant + "; ";
                            }
                        }

//                        string sPlayerIncant;
                        if (dSkillRow["PlayerIncant"].ToString().Trim().Length > 0)
                            FullDesc += "<b><i>" + dSkillRow["PlayerIncant"].ToString().Trim() + "</b></i>";

                        FullDesc = FullDesc.Trim();
                        if (FullDesc.EndsWith(";"))
                            FullDesc = FullDesc.Substring(0, FullDesc.Length - 1);

                        dSkillRow["FullDescription"] = FullDesc;
                    }

                    lblCPSpent.Text = CPSpent.ToString("0.00");
                    lblCPAvail.Text = (cChar.TotalCP - CPSpent).ToString("0.00");

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

                    DataView dvSkills = new DataView(dtSkills, "DisplaySkill = true", "DisplayOrder", DataViewRowState.CurrentRows);
                    gvSkills.DataSource = dvSkills;
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