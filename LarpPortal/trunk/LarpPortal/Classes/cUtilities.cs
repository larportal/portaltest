﻿using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.UI.WebControls ;
using System.Web;
using System.Web.UI;
using System.IO;
using System.DirectoryServices;
using LarpPortal.Classes;
using Excel;


namespace LarpPortal.Classes
{
    [Serializable()]
    public class cUtilities
    {
        public cUtilities()
        {
        }

        public static DataTable LoadDataTable(string strStoredProc, SortedList slParameters, string strLConn, string strUserName, string strCallingMethod)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings[strLConn].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = strStoredProc;
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;   
            lcmd.Connection = lconn;
            if (slParameters.Count > 0) 
            {
                for (int i = 0; i < slParameters.Count; i++)
                {
                    lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), slParameters.GetByIndex(i).ToString().Trim()));
                }
            }
            SqlDataAdapter ldsa = new SqlDataAdapter(lcmd);
            DataTable ldt = new DataTable();
            
            try
            {
                lconn.Open();
                ldsa.Fill(ldt);
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName + ":" + strStoredProc, lcmd, strUserName + strCallingMethod);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + strCallingMethod);
            }
            finally
            {
                lconn.Close();
            }

            return ldt;


        }

        public static DataSet ReturnDataTableFromExcelWorksheet(string strSheetLocation, string strSheetName, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            DataSet dsUnUpdated = new DataSet();
            try
            {
                 IExcelDataReader iExcelDataReader = null;

                FileStream oStream = File.Open(strSheetLocation, FileMode.Open, FileAccess.Read);

                iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(oStream);

                iExcelDataReader.IsFirstRowAsColumnNames = true;

                dsUnUpdated = iExcelDataReader.AsDataSet();

                iExcelDataReader.Close();

               
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName);
            }
            return dsUnUpdated;
        }

        public static string ReturnStringFromSQL(string strStoredProc, string strReturnValue, SortedList slParameters, string strLConn, string strUserName, string strCallingMethod)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            string strParam = "";
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings[strLConn].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = strStoredProc;
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;
            lcmd.Connection = lconn;
            if (slParameters.Count > 0)
            {
                for (int i = 0; i < slParameters.Count; i++)
                {
                    if (slParameters.GetKey(i).ToString().Trim().Substring(0, 2) == "dt")
                    {
                        if (slParameters.GetByIndex(i).ToString().Trim().Substring(0, 10) == "01/01/1900")
                        {
                            lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), DBNull.Value));
                        }
                    }
                    else
                    {
                        lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), slParameters.GetByIndex(i).ToString().Trim()));
                    }
                }
            }
            SqlDataAdapter ldsa = new SqlDataAdapter(lcmd);
            DataTable ldt = new DataTable();
            String strReturn = "";
           

            try
            {
                lconn.Open();
                ldsa.Fill(ldt);
                if (ldt.Rows.Count > 0)
                {
                    strReturn = ldt.Rows[0][strReturnValue].ToString().Trim();
                }
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName, lcmd, strUserName + strCallingMethod);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + strCallingMethod);
            }
            finally
            {
                lconn.Close();
            }

            return strReturn;

        }

        public static void PerformNonQuery(string strStoredProc, SortedList slParameters, string strLConn, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings[strLConn].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = strStoredProc;
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;
            lcmd.Connection = lconn;
            if (slParameters.Count > 0)
            {
                for (int i = 0; i < slParameters.Count; i++)
                {
                    lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), slParameters.GetByIndex(i).ToString().Trim()));
                }
            }
            try
            {
                lconn.Open();
                lcmd.ExecuteNonQuery();
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName, lcmd, strUserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName);
            }
            finally
            {
                lconn.Close();
            }
        }

        public static Boolean PerformNonQueryBoolean(string strStoredProc, SortedList slParameters, string strLConn, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings[strLConn].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = strStoredProc;
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;
            lcmd.Connection = lconn;
            Boolean blnReturn = false;
            if (slParameters.Count > 0)
            {
                for (int i = 0; i < slParameters.Count; i++)
                {
                    lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), slParameters.GetByIndex(i).ToString().Trim()));
                }
            }
            try
            {
                lconn.Open();
                lcmd.ExecuteNonQuery();
                blnReturn = true;
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName, lcmd, strUserName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName);
            }
            finally
            {
                lconn.Close();
            }
            return blnReturn;
        }

        public static ArrayList SeperateStrings(string strString, string strSeperator, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            ArrayList alList = new ArrayList();
            int intIndex = 0;
            strString = strString.Trim();
            int intLength = strString.Length;
            try
            {
                do
                {
                    intIndex = strString.IndexOf(strSeperator);
                    if (intIndex > 2)
                    {
                        alList.Add(strString.Substring(0, intIndex));
                    }
                    else
                    {
                        if (intIndex == -1)
                        {
                            alList.Add(strString);
                            break;
                        }
                    }
                    strString = strString.Remove(0, intIndex + 1);
                    strString = strString.Trim();
                    intLength = strString.Length;
                } while (intLength != 0);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName);
            }
            finally
            {
            }
            return alList;
        }

        public static void LoadDropDownList(DropDownList ddlList, string strStoredProc, SortedList slParam, string strTextValue, string strDataValue, string strLConn, string strUserName, string strCallingRoutine)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            DataTable ldt = new DataTable();
            try
            {
                ldt = LoadDataTable(strStoredProc, slParam, strLConn, strUserName, lsRoutineName + '-' + strCallingRoutine);
                if (ldt.Rows.Count > 0)
                {
                    ddlList.DataSource = ldt;
                    ddlList.DataTextField = strTextValue;
                    ddlList.DataValueField = strDataValue;
                    ddlList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName);
            }
            finally
            {
            }
        }

        public static string Right(string strString, int length)
        {
            string strReturn = strString.Substring(strString.Length - length);
            return strReturn;
        }

        public static Int32 ReturnIntFromSQL(string strStoredProc, string strReturnValue, SortedList slParameters, string strLConn, string strUserName, string strCallingMethod)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            string strParam = "";
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings[strLConn].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = strStoredProc;
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;
            lcmd.Connection = lconn;

            try
            {
                if (slParameters.Count > 0)
                {
                    for (int i = 0; i < slParameters.Count; i++)
                    {
                        if (slParameters.GetKey(i).ToString().Trim().Substring(0, 2) == "dt" )
                        {
                            if (slParameters.GetByIndex(i).ToString().Trim().Substring(0, 10) == "01/01/1900")
                            {
                                lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), DBNull.Value));
                            }
                        }
                        else
                        {
                            lcmd.Parameters.Add(new SqlParameter(slParameters.GetKey(i).ToString().Trim(), slParameters.GetByIndex(i).ToString().Trim()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + strCallingMethod);
            }
            SqlDataAdapter ldsa = new SqlDataAdapter(lcmd);
            DataTable ldt = new DataTable();
            String strReturn = "";
            Boolean blnCanConvert = false;
            Int32 intReturn = 0;
            long number1 = 0;
            try
            {
                lconn.Open();
                ldsa.Fill(ldt);
                if (ldt.Rows.Count > 0)
                {
                    strReturn = ldt.Rows[0][strReturnValue].ToString().Trim();
                }
                blnCanConvert = long.TryParse(strReturn, out number1);
                if (blnCanConvert)
                {
                    intReturn = Convert.ToInt32(strReturn);
                }
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName, lcmd, strUserName + strCallingMethod);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + strCallingMethod);
            }
            finally
            {
                lconn.Close();
            }

            return intReturn;

        }

        public static void ShowAlertMessage(string error)
        {

            Page page = HttpContext.Current.Handler as Page;

            if (page != null)
            {

                error = error.Replace("'", "\\'");

                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);

            }

        }

        public static DateTime ParseStringToDateTime(string strPassed)
        {
            DateTime temp;
            DateTime dtReturn = DateTime.Now;
            if (DateTime.TryParse(strPassed, out temp))
            {
                dtReturn = temp;
            }

            return dtReturn;
        }

        public static Int32 ParseStringToInt32(string strPassed)
        {
            Int32 temp;
            Int32 intReturn = -1;
            if (Int32.TryParse(strPassed, out temp))
            {
                intReturn = temp ;
            }

            return intReturn;

        }

        public static string FormatFileNameForSave(string strPassed)
        {

            string strReturn = strPassed.Replace(" ", "");
            strReturn = strReturn.Replace("(", "");
            strReturn = strReturn.Replace(")", "");
            strReturn = strReturn.Replace("#", "");
            strReturn = strReturn.Replace("-", "");
            strReturn = strReturn.Replace("_", "");
            strReturn = strReturn.Replace("!", "");
            strReturn = strReturn.Replace("@", "");
            strReturn = strReturn.Replace("$", "");
            strReturn = strReturn.Replace("%", "");
            strReturn = strReturn.Replace("^", "");
            strReturn = strReturn.Replace("&", "");
            strReturn = strReturn.Replace("*", "");
            strReturn = strReturn.Replace("{", "");
            strReturn = strReturn.Replace("}", "");
            strReturn = strReturn.Replace("|", "");
            strReturn = strReturn.Replace("[", "");
            strReturn = strReturn.Replace("]", "");
            strReturn = strReturn.Replace("'", "");
            strReturn = strReturn.Replace(@"""", "");
            return strReturn;

        }

        public static string replaceUNCPath(string strPath)
        {
            string strReturn = strPath.ToUpper();
            string strSharesPath = ReturnDefaultValue("SharesPath", "");
            if (strReturn.IndexOf("TMC-ERPDPLY-1") > -1)
            {
                strReturn = @"/ECSMT/HTMLUPload/";
            }
            else
            {
                strReturn = strReturn.ToUpper().Replace(@"\\TMC-OFFICE-6\SHARES\","/");
                strReturn = strReturn.ToUpper().Replace(@"\\\\TMC-OFFICE-6\\SHARES\\", "/");
                strReturn = strReturn.Replace(@"\", @"/");
            }
            return strReturn;
        }

        public static string ReturnDefaultValue(string strValue, string strUserName)
        {
            MethodBase lmth = MethodBase.GetCurrentMethod();
            string lsRoutineName = lmth.DeclaringType + "." + lmth.Name;
            SqlConnection lconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MarlinCommon"].ConnectionString);
            SqlCommand lcmd = new SqlCommand();
            lcmd.CommandText = "uspGetDefaultValues";
            lcmd.CommandType = CommandType.StoredProcedure;
            lcmd.CommandTimeout = 0;
            lcmd.Connection = lconn;
            lcmd.Parameters.Add(new SqlParameter("@strDefaultCode", strValue));
            SqlDataAdapter ldsa = new SqlDataAdapter(lcmd);
            DataTable ldt = new DataTable();
            String strReturn = "";
            try
            {
                lconn.Open();
                ldsa.Fill(ldt);
                if (ldt.Rows.Count > 0)
                {
                    strReturn = ldt.Rows[0]["DefaultValue"].ToString().Trim();
                }
            }
            catch (SqlException exSQL)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(exSQL, lsRoutineName, lcmd, strUserName + lsRoutineName);
            }
            catch (Exception ex)
            {
                ErrorAtServer lobjError = new ErrorAtServer();
                lobjError.ProcessError(ex, lsRoutineName, strUserName + lsRoutineName);
            }
            finally
            {
                lconn.Close();
            }

            return strReturn;


        }

        public static string replaceUNCPathForOrderEntry(string strPath)
        {
            string strReturn = strPath.ToUpper();
            string strSharesPath = ReturnDefaultValue("SharesPath", "");
            if (strReturn.IndexOf("TMC-ERPDPLY-1") > -1)
            {
                strReturn = @"/ECSMT/HTMLUPload/";
            }
            else
            {
                strReturn = strReturn.ToUpper().Replace(@"\\TMC-OFFICE-6\SHARES\ORDERENTRY\", "/");
                strReturn = strReturn.ToUpper().Replace(@"\\\\TMC-OFFICE-6\\SHARES\\ORDERENTRY\\", "/");
                strReturn = strReturn.Replace(@"\", @"/");
            }
            return strReturn;
        }

        public static Int16 ReplaceBooleanWithBit(Boolean blnValue)
        {
            Int16 intReturn = 1;
            if (blnValue)
            {
                intReturn = 1;
            }
            else
            {
                intReturn = 0;
            }

            return intReturn;
        }

        public static Boolean ReplaceBitWithBoolean(Int32 intBit)
        {
            Boolean blnReturn = false;
            if (intBit == 1)
            {
                blnReturn = true;
            }
            else
            {
                blnReturn = false;
            }

            return blnReturn;
        }

 
    }
}