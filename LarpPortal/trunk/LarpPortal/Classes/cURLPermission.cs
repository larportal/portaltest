using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LarpPortal.Classes;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace LarpPortal.Classes
{
    public class cURLPermission
    {
        public bool _PagePermission;
        public string _DefaultUnauthorizedURL;
        public void GetURLPermissions(string URL, int UserID, string RoleString)
        {
            int iTemp = 0;
            string stStoredProc = "uspGetURLPermissions";
            string stCallingMethod = "cURLPermission.GetURLPermissions";
            string URLPermissions = "";
            _PagePermission = false;
            int URLPermissionID = 0;
            SortedList slParameters = new SortedList();
            slParameters.Add("@URL", URL);
            DataTable dtURLPermissions = new DataTable();
            dtURLPermissions = cUtilities.LoadDataTable(stStoredProc, slParameters, "LARPortal", UserID.ToString(), stCallingMethod);
            foreach (DataRow drow in dtURLPermissions.Rows)
            {
                if (int.TryParse(drow["RoleID"].ToString(), out iTemp))
                {
                    URLPermissionID = iTemp;
                    if(iTemp == -1)  // We'll have a -1 entry for pages that anyone can get to
                    {
                        _PagePermission = true;
                    }
                }

                _DefaultUnauthorizedURL = drow["DefaultUnauthorizedURL"].ToString();
                URLPermissions = "/" + URLPermissionID.ToString() + "/";
                if(RoleString.Contains(URLPermissions))
                    _PagePermission = true;

            }
        }
    }
}