using System;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LarpPortal.Classes
{
    /// <summary>
    /// Processing error at a server. This trys to connect directly to the database.
    /// </summary>
    public class ErrorAtServer
    {

        #region StandardErrors

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsAddInfo">Any additional information to add to the record.</param>
        public void ProcessError(Exception pvException, string pvsLocation, string pvsAddInfo)
        {
            ProcessError(pvException, pvsLocation, pvsAddInfo, "");
        }

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsAddInfo">Any additional information to add to the record.</param>
        /// <param name="pvsSessionID">Unique Identifier to be able to group records.</param>
        public void ProcessError(Exception pvException, string pvsLocation, string pvsAddInfo, string pvsSessionID)
        {
            // First make the error string. We will always do this.
            string lsErrorText = "";
            string lsErrorType = pvException.GetType().ToString();

            ErrorRoutines lobjRoutines = new ErrorRoutines();
            lsErrorText = lobjRoutines.FormatError(lsErrorType, pvException, pvsLocation);

            if (pvsAddInfo.Length == 0)
                pvsAddInfo = pvException.StackTrace;

            HandleError(lsErrorType, lsErrorText, pvsLocation, pvException.StackTrace, pvsAddInfo, pvsSessionID);
        }

        #endregion



        #region SQLErrors

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvSQLException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsCmd">The SQL Command that was run.</param>
        /// <param name="pvsAddInfo">Any additional info you want displayed.</param>
        public void ProcessError(SqlException pvSQLException, string pvsLocation, SqlCommand pvsCmd, string pvsAddInfo)
        {
            ProcessError(pvSQLException, pvsLocation, pvsCmd, pvsAddInfo, "");
        }

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvSQLException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsCmd">The SQL Command that was run.</param>
        /// <param name="pvsAddInfo">Any additional info you want displayed.</param>
        public void ProcessError(SqlException pvSQLException, string pvsLocation, SqlCommand pvsCmd, string pvsAddInfo, string sSessionID)
        {
            // First make the error string. We will always do this.
            string lsErrorText = "";
            string lsErrorType = pvSQLException.GetType().ToString();

            ErrorRoutines lobjRoutines = new ErrorRoutines();
            lsErrorText = lobjRoutines.FormatError(lsErrorType, pvSQLException, pvsLocation);

            string lsSQLCmd = lobjRoutines.FormatSQLCmd(pvsCmd);

            if ((!String.IsNullOrEmpty(pvsAddInfo)) &&
                 (pvsAddInfo != ""))
            {
                lsSQLCmd += "<br>" + pvsAddInfo.Replace(Environment.NewLine, "<BR>");
            }
            HandleError(lsErrorType, lsErrorText, pvsLocation, pvSQLException.StackTrace, lsSQLCmd, sSessionID);
        }

        #endregion

        /// <summary>
        /// Handles the error once the error has been converted to a string.
        /// </summary>
        /// <param name="pvsErrorType">The type of error (usually the type.GetType of the exception.</param>
        /// <param name="pvsErrorText">The actual error text.</param>
        /// <param name="pvsLocation">The location in the program where the error happened.</param>
        /// <param name="pvsStackTrace">Stack trace to see where we are    JBradshaw 10/10/2015</param>
        /// <param name="pvsAddInfo">Any additional info needed.</param>
        /// <param name="pvsSessionID">Session ID to group records together.</param>
        public void HandleError ( string pvsErrorType, string pvsErrorText, string pvsLocation, string pvsStackTrace, string pvsAddInfo, string pvsSessionID )
        {
            using (SqlConnection ConnErrors = new SqlConnection(ConfigurationManager.ConnectionStrings["Audit"].ConnectionString))
            {
                using (SqlCommand lcmdAddErrorMessage = new SqlCommand("uspSystemErrorsIns", ConnErrors))
                {
                    try
                    {
                        ConnErrors.Open();

                        lcmdAddErrorMessage.CommandType = CommandType.StoredProcedure;
                        lcmdAddErrorMessage.Parameters.AddWithValue("@ErrorLocation", pvsLocation);
                        lcmdAddErrorMessage.Parameters.AddWithValue("@ErrorMessage", pvsErrorText);
                        lcmdAddErrorMessage.Parameters.AddWithValue("@ErrorType", pvsErrorType);
                        lcmdAddErrorMessage.Parameters.AddWithValue("@StackTrace", pvsStackTrace);
                        lcmdAddErrorMessage.Parameters.AddWithValue("@AddInfo", pvsAddInfo);
                        lcmdAddErrorMessage.Parameters.AddWithValue("@SessionID", pvsSessionID);

                        lcmdAddErrorMessage.ExecuteNonQuery();
                    }
                    catch // (Exception ex)
                    {
                        // Not much we can do so just leave it.....
                    }
                }
            }
        }
    }
}
