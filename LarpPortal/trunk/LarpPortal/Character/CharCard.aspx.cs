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

                    //lblTotalCP.Text = cChar.TotalCP.ToString("0.00");

                    DataTable dtCharacterSkills = new DataTable();
                    SortedList sParams = new SortedList();
                    sParams.Add("@CharacterID", cChar.CharacterID);

                    MethodBase lmth = MethodBase.GetCurrentMethod();
                    string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;

                    dtCharacterSkills = Classes.cUtilities.LoadDataTable("uspGetCharCardSkills", sParams, "LARPortal", Session["UserName"].ToString(), lsRoutineName);

                    //DataTable dtSkills = Classes.cUtilities.CreateDataTable(cChar.CharacterSkills);
                    if (dtCharacterSkills.Columns["FullDescription"] == null)
                        dtCharacterSkills.Columns.Add(new DataColumn("FullDescription", typeof(string)));
                    //if (dtSkills.Columns["DisplaySkill"] == null)
                    //{
                    //    DataColumn dcDisplaySkill = new DataColumn();       // Need to define the column and then attributes because want the default value to be true.
                    //    dcDisplaySkill.ColumnName = "DisplaySkill";
                    //    dcDisplaySkill.DataType = typeof(Boolean);
                    //    dcDisplaySkill.DefaultValue = true;
                    //    dtSkills.Columns.Add(dcDisplaySkill);
                    //}

                    Classes.cSkillPool skDefault = cChar.SkillPools.Find(x => x.DefaultPool == true);
                    object oCPSpent;
                    double dCPSpent;
                    oCPSpent = dtCharacterSkills.Compute("sum(cpcostpaid)", "CampaignSkillPoolID = " + skDefault.PoolID.ToString());
                    double.TryParse(oCPSpent.ToString(), out dCPSpent);

                    TableRow dTotalRow = new TableRow();
                    TableCell dcCell = new TableCell();
                    dcCell.CssClass = "TableLabel";
                    dcCell.Text = "Total CP:";
                    dTotalRow.Cells.Add(dcCell);

                    dcCell = new TableCell();
                    dcCell.Text = cChar.TotalCP.ToString("0.00");
                    dTotalRow.Cells.Add(dcCell);

                    dcCell = new TableCell();
                    dcCell.CssClass = "TableLabel";
                    dcCell.Text = "Total Spent:";
                    dTotalRow.Cells.Add(dcCell);

                    dcCell = new TableCell();
                    dcCell.Text = dCPSpent.ToString("0.00");
                    dTotalRow.Cells.Add(dcCell);

                    dcCell = new TableCell();
                    dcCell.CssClass = "TableLabel";
                    dcCell.Text = "Total Avail:";
                    dTotalRow.Cells.Add(dcCell);

                    dcCell = new TableCell();
                    dcCell.Text = (cChar.TotalCP - dCPSpent).ToString("0.00");
                    dTotalRow.Cells.Add(dcCell);
                    tblCharInfo.Rows.Add(dTotalRow);


                    List<Classes.cSkillPool> skNotDefault = cChar.SkillPools.FindAll(x => x.DefaultPool == false).OrderBy(x => x.PoolDescription).ToList();

                    foreach (Classes.cSkillPool PoolNotDefault in skNotDefault)
                    {
                        oCPSpent = dtCharacterSkills.Compute("sum(cpcostpaid)", "CampaignSkillPoolID = " + PoolNotDefault.PoolID.ToString());
                        double.TryParse(oCPSpent.ToString(), out dCPSpent);
                        double TotalPoints = cChar.SkillPools.Find(x => x.PoolID == PoolNotDefault.PoolID).TotalPoints;

                        dTotalRow = new TableRow();
                        dTotalRow.ForeColor = System.Drawing.Color.FromName(PoolNotDefault.PoolDisplayColor);

                        dcCell = new TableCell();
                        dcCell.CssClass = "TableLabel";
                        dcCell.Text = PoolNotDefault.PoolDescription + " Points:";
                        dTotalRow.Cells.Add(dcCell);

                        dcCell = new TableCell();
                        dcCell.Text = TotalPoints.ToString("0.00");
                        dTotalRow.Cells.Add(dcCell);

                        dcCell = new TableCell();
                        dcCell.CssClass = "TableLabel";
                        dcCell.Text = "Total Spent:";
                        dTotalRow.Cells.Add(dcCell);

                        dcCell = new TableCell();
                        dcCell.Text = dCPSpent.ToString("0.00");
                        dTotalRow.Cells.Add(dcCell);

                        dcCell = new TableCell();
                        dcCell.CssClass = "TableLabel";
                        dcCell.Text = "Total Avail:";
                        dTotalRow.Cells.Add(dcCell);

                        dcCell = new TableCell();
                        dcCell.Text = (TotalPoints - dCPSpent).ToString("0.00");
                        dTotalRow.Cells.Add(dcCell);
                        tblCharInfo.Rows.Add(dTotalRow);
                    }




                    double CPCost;
                    double CPSpent = 0.0;

                    foreach (DataRow dSkillRow in dtCharacterSkills.Rows)
                    {
                        if (double.TryParse(dSkillRow["CPCostPaid"].ToString(), out CPCost))
                            CPSpent += CPCost;

                    //    DataView dvPreReqs = new DataView(dsCampaignSkills.Tables[1], "CampaignSkillsStandardID = " + dSkillRow["CampaignSkillsStandardID"].ToString() + 
                    //        " and SuppressPrerequisiteSkill = 1", "", DataViewRowState.CurrentRows);
                    //    foreach (DataRowView dPreReq in dvPreReqs)
                    //    {
                    //        DataRow[] drPreReq = dtSkills.Select("CampaignSkillsStandardID = " + dPreReq["PreRequisiteSkillID"].ToString());
                    //        foreach (DataRow d in drPreReq)
                    //            d["DisplaySkill"] = false;
                    //    }

                        string FullDesc = "";
                        bool bDisplay;
                        if (bool.TryParse(dSkillRow["CardDisplayDescription"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                if (dSkillRow["SkillCardDescription"].ToString().Trim().Length > 0)
                                    FullDesc += dSkillRow["SkillCardDescription"].ToString().Trim() + "; ";
                            }
                        }

                        if (dSkillRow["PlayerDescription"].ToString().Trim().Length > 0)
                            FullDesc += dSkillRow["PlayerDescription"].ToString().Trim() + "; ";

                        if (bool.TryParse(dSkillRow["CardDisplayIncant"].ToString(), out bDisplay))
                        {
                            if (bDisplay)
                            {
                                if (dSkillRow["SkillIncant"].ToString().Trim().Length > 0)
                                    FullDesc += "<i>" + dSkillRow["SkillIncant"].ToString().Trim() + "</i>; ";
                            }
                        }

                        if (dSkillRow["PlayerIncant"].ToString().Trim().Length > 0)
                            FullDesc += "<b><i>" + dSkillRow["PlayerIncant"].ToString().Trim() + "</b></i>";

                        FullDesc = FullDesc.Trim();
                        if (FullDesc.EndsWith(";"))
                            FullDesc = FullDesc.Substring(0, FullDesc.Length - 1);

                        dSkillRow["FullDescription"] = FullDesc;
                    }

                    //lblCPSpent.Text = CPSpent.ToString("0.00");
                    //lblCPAvail.Text = (cChar.TotalCP - CPSpent).ToString("0.00");

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

                    DataView dvSkills = new DataView(dtCharacterSkills, "", "DisplayOrder", DataViewRowState.CurrentRows);
                    gvSkills.DataSource = dvSkills;
                    gvSkills.DataBind();

                    gvNonCost.DataSource = NonCost;
                    gvNonCost.DataBind();

                    Classes.cUser User = new Classes.cUser(Session["Username"].ToString(), "PasswordNotNeeded");
                    lblPlayerName.Text = User.FirstName + " " + User.MiddleName + " " + User.LastName;
                }
            }
        }
    }
}