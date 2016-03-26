﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace LarpPortal
{
    /// <summary>
    /// CampaignInfo is a web service with assorted routines for getting information about campaigns.
    /// </summary>
    [WebService(Namespace = "http://www.LARPortal.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CampaignInfo1 : System.Web.Services.WebService
    {
        /// <summary>
        /// Give a skill ID, this will return the string to display about the skill.
        /// </summary>
        /// <param name="CampaignID">The campaign ID to get the information about.</param>
        /// <returns>HTML formatted string about the skill that can be put in a div to display to the user.</returns>
        [WebMethod(Description = "Get the corresponding description for a skill by ID.")]
        public string GetCampaignInfo(int CampaignID)
        {
            string sCampaignInfo = "";

            using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("uspGetCampaignSkillByNodeID", Conn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@SkillNodeID", CampaignID);
                    SqlDataAdapter SDAGetData = new SqlDataAdapter(Cmd);
                    DataSet dsResults = new DataSet();

                    Conn.Open();
                    SDAGetData.Fill(dsResults);
                    Conn.Close();

                    foreach (DataRow dRow in dsResults.Tables[0].Rows)
                    {
                        // If you want to display different information about the skill, this is where you would change it.
                        sCampaignInfo = "<b>" + dRow["SkillName"].ToString() + "</b><br>" +
                            dRow["SkillShortDescription"].ToString() + "<br><br>" +
                            "Cost: ";

                        sCampaignInfo += @"<span style=""color: " + dRow["DisplayColor"].ToString() + @""">";

                        if (dRow["SkillCPCost"] != DBNull.Value)
                            sCampaignInfo += dRow["SkillCPCost"].ToString();

                        bool bDefault = false;
                        if (bool.TryParse(dRow["DefaultPool"].ToString(), out bDefault))
                            if (!bDefault)
                                sCampaignInfo += " " + dRow["PoolDescription"].ToString();

                        sCampaignInfo += "</span>";
                        sCampaignInfo += "<br><br>" + dRow["SkillLongDescription"].ToString();
                    }

                    DataView dvSkills = new DataView(dsResults.Tables[1], "PrerequisiteSkillNodeID is not null and ExcludeFromPurchase = false", "", DataViewRowState.CurrentRows);
                    if (dvSkills.Count > 0)
                    {
                        sCampaignInfo += "<br><br>";
                        for (int i = 0; i < dvSkills.Count; i++)
                        {
                            if (i == 0)
                                sCampaignInfo += "This skill requires you already have " + dvSkills[i]["SkillName"].ToString();
                            else
                                sCampaignInfo += ", " + dvSkills[i]["SkillName"].ToString();
                        }
                    }

                    dvSkills = new DataView(dsResults.Tables[1], "PrerequisiteSkillNodeID is not null and ExcludeFromPurchase = true", "", DataViewRowState.CurrentRows);
                    if (dvSkills.Count > 0)
                    {
                        sCampaignInfo += "<br><br>";
                        for (int i = 0; i < dvSkills.Count; i++)
                        {
                            if (i == 0)
                                sCampaignInfo += "You cannot buy this skill if you already have " + dvSkills[i]["SkillName"].ToString();
                            else
                                sCampaignInfo += ", " + dvSkills[i]["SkillName"].ToString();
                        }
                    }

                    dvSkills = new DataView(dsResults.Tables[1], "PrerequisiteGroupID is not null", "", DataViewRowState.CurrentRows);
                    if (dvSkills.Count > 0)
                    {
                        foreach (DataRowView dGroupRow in dvSkills)
                        {
                            int iGroupID;
                            int iNumItems;
                            int.TryParse(dGroupRow["PrerequisiteGroupID"].ToString(), out iGroupID);
                            int.TryParse(dGroupRow["NumGroupSkillsRequired"].ToString(), out iNumItems);

                            DataView dvGroupSkills = new DataView(dsResults.Tables[2], "PrerequisiteGroupID = " + iGroupID.ToString(), "DisplayOrder", DataViewRowState.CurrentRows);
                            if (dvGroupSkills.Count > 0)
                            {
                                sCampaignInfo += "<br><br>";
                                for (int i = 0; i < dvGroupSkills.Count; i++)
                                {
                                    if (i == 0)
                                        sCampaignInfo += "You have to have " + iNumItems.ToString() + " of the following skills to purchase this: " + dvGroupSkills[i]["SkillName"].ToString();
                                    else
                                        sCampaignInfo += ", " + dvGroupSkills[i]["SkillName"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            return sCampaignInfo;
        }
    }
}
