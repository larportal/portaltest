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
    public class SkillInfoService : System.Web.Services.WebService
    {
        /// <summary>
        /// Give a skill ID, this will return the string to display about the skill.
        /// </summary>
        /// <param name="CampaignID">The campaign ID to get the information about.</param>
        /// <returns>HTML formatted string about the skill that can be put in a div to display to the user.</returns>
        [WebMethod(Description = "Get the corresponding description for a skill by node ID.")]
        public string getSkillNodeInfo(int SkillNodeID)
        {
            string sSkillNodeInfo = "";

            using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LARPortal"].ConnectionString))
            {
                using (SqlCommand Cmd = new SqlCommand("uspGetCampaignSkillByNodeID", Conn))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@SkillNodeID", SkillNodeID);
                    SqlDataAdapter SDAGetData = new SqlDataAdapter(Cmd);
                    DataTable dtResults = new DataTable();

                    Conn.Open();
                    SDAGetData.Fill(dtResults);
                    Conn.Close();

                    foreach (DataRow dRow in dtResults.Rows)
                    {
                        // If you want to display different information about the skill, this is where you would change it.
                        sSkillNodeInfo = "<b>" + dRow["SkillName"].ToString() + "</b><br>" +
                            dRow["SkillShortDescription"].ToString() + "<br><br>" +
                            "Cost: ";

                        sSkillNodeInfo += @"<span style=""color: " + dRow["DisplayColor"].ToString() + @""">" + dRow["SkillCPCost"].ToString();
                        bool bDefault = false;
                        if (bool.TryParse(dRow["DefaultPool"].ToString(), out bDefault))
                            if (!bDefault)
                                sSkillNodeInfo += " " + dRow["PoolDescription"].ToString();

                        sSkillNodeInfo += "</span>";
                        sSkillNodeInfo += "<br><br>" + dRow["SkillLongDescription"].ToString();
                    }
                }
            }
            return sSkillNodeInfo;
        }
    }
}