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
        public void ProcessError ( Exception pvException, string pvsLocation )
        {
            ProcessError(pvException, pvsLocation, "");
        }

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsAddInfo">Any additional information to add to the record.</param>
        public void ProcessError(Exception pvException, string pvsLocation, string pvsAddInfo)
        {
            // First make the error string. We will always do this.
            string lsErrorText = "";
            string lsErrorType = pvException.GetType().ToString();

            ErrorRoutines lobjRoutines = new ErrorRoutines();
            lsErrorText = lobjRoutines.FormatError(Environment.MachineName, lsErrorType, pvException, pvsLocation);

            HandleError(lsErrorType, lsErrorText, pvsLocation, pvsAddInfo);
        }

        #endregion



        #region SQLErrors

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvSQLException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        public void ProcessError(SqlException pvSQLException, string pvsLocation)
        {
            ProcessError(pvSQLException, pvsLocation, null, "");
        }


        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvSQLException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsCmd">The SQL Command that was run.</param>
        public void ProcessError(SqlException pvSQLException, string pvsLocation, SqlCommand pvsCmd)
        {
            ProcessError(pvSQLException, pvsLocation, pvsCmd, "");
        }

        /// <summary>
        /// Given the error, this will attempt to format it and save it into the database.
        /// </summary>
        /// <param name="pvSQLException">The actual exception that happened.</param>
        /// <param name="pvsLocation">The location of the error.</param>
        /// <param name="pvsCmd">The SQL Command that was run.</param>
        /// <param name="pvsAddInfo">Any additional info you want displayed.</param>
        public void ProcessError(SqlException pvSQLException, string pvsLocation, SqlCommand pvsCmd, string pvsAddInfo)
        {
            // First make the error string. We will always do this.
            string lsErrorText = "";
            string lsErrorType = pvSQLException.GetType().ToString();

            ErrorRoutines lobjRoutines = new ErrorRoutines();
            lsErrorText = lobjRoutines.FormatError(Environment.MachineName, lsErrorType, pvSQLException, pvsLocation);

            string lsSQLCmd = lobjRoutines.FormatSQLCmd(pvsCmd);

            if ((!String.IsNullOrEmpty(pvsAddInfo)) &&
                 (pvsAddInfo != ""))
            {
                lsSQLCmd += "<br>" + pvsAddInfo.Replace(Environment.NewLine, "<BR>");
            }
            HandleError(lsErrorType, lsErrorText, pvsLocation, lsSQLCmd);
        }

        #endregion

        /// <summary>
        /// Handles the error once the error has been converted to a string.
        /// </summary>
        /// <param name="pvsErrorType">The type of error (usually the type.GetType of the exception.</param>
        /// <param name="pvsErrorText">The actual error text.</param>
        /// <param name="pvsLocation">The location in the program where the error happened.</param>
        /// <param name="pvsAddInfo">Any additional info needed.</param>
        public void HandleError(string pvsErrorType, string pvsErrorText, string pvsLocation, string pvsAddInfo)
        {
            HandleError(pvsErrorType, pvsErrorText, pvsLocation, pvsAddInfo, Environment.MachineName, "");
        }


        /// <summary>
        /// Handles the error once the error has been converted to a string.
        /// </summary>
        /// <param name="pvsErrorType">The type of error (usually the type.GetType of the exception.</param>
        /// <param name="pvsErrorText">The actual error text.</param>
        /// <param name="pvsLocation">The location in the program where the error happened.</param>
        /// <param name="pvsAddInfo">Any additional info needed.</param>
        /// <param name="pvsMachineName">The name of the machine it happened on.</param>
        public void HandleError(string pvsErrorType, string pvsErrorText, string pvsLocation, string pvsAddInfo, string pvsMachineName)
        {
            HandleError(pvsErrorType, pvsErrorText, pvsLocation, pvsAddInfo, pvsMachineName, "");
            //SqlConnection ConnErrors = new SqlConnection();
            //try
            //{
            //    // Just in case somebody forgot to put it in the app.config line.
            //    string lsConnStr = "data source=sql-prod-1;user id=sa; pwd=Ashes13;Initial Catalog=Errors";
            //    if (ConfigurationManager.ConnectionStrings["Errors"] != null)
            //        lsConnStr = ConfigurationManager.ConnectionStrings["Errors"].ConnectionString;

            //    ConnErrors = new SqlConnection(lsConnStr);
            //    ConnErrors.Open();

            //    SqlCommand lcmdAddErrorMessage = new SqlCommand("uspSystemErrorsIns", ConnErrors);
            //    lcmdAddErrorMessage.CommandType = CommandType.StoredProcedure;
            //    lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorLocation", pvsLocation));
            //    lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorMessage", pvsErrorText));
            //    lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorType", pvsErrorType));
            //    lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@AddInfo", pvsAddInfo));
            //    lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@MachineErrorHappenedOn", pvsMachineName));

            //    lcmdAddErrorMessage.ExecuteNonQuery();

            //    ConnErrors.Close();
            //}
            //catch // (Exception ex)
            //{
            //    // Not much we can do so just leave it.....
            //}
        }

        /// <summary>
        /// Handles the error once the error has been converted to a string.
        /// </summary>
        /// <param name="pvsErrorType">The type of error (usually the type.GetType of the exception.</param>
        /// <param name="pvsErrorText">The actual error text.</param>
        /// <param name="pvsLocation">The location in the program where the error happened.</param>
        /// <param name="pvsAddInfo">Any additional info needed.</param>
        /// <param name="pvsMachineName">The name of the machine it happened on.</param>
        /// <param name="pvsApplicationVersion">The version of the application that had the error.</param>
        public void HandleError ( string pvsErrorType, string pvsErrorText, string pvsLocation, 
                string pvsAddInfo, string pvsMachineName, string pvsApplicationVersion)
        {
            SqlConnection ConnErrors = new SqlConnection();
            try
            {
                // Just in case somebody forgot to put it in the app.config line.
                string lsConnStr = ConfigurationManager.ConnectionStrings["Errors"].ConnectionString;
                if (ConfigurationManager.ConnectionStrings["Errors"] != null)
                    lsConnStr = ConfigurationManager.ConnectionStrings["Errors"].ConnectionString;

                ConnErrors = new SqlConnection(lsConnStr);
                ConnErrors.Open();

                SqlCommand lcmdAddErrorMessage = new SqlCommand("uspSystemErrorsIns", ConnErrors);
                lcmdAddErrorMessage.CommandType = CommandType.StoredProcedure;
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorLocation", pvsLocation));
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorMessage", pvsErrorText));
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ErrorType", pvsErrorType));
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@AddInfo", pvsAddInfo));
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@ApplicationVersion", pvsApplicationVersion));
                lcmdAddErrorMessage.Parameters.Add(new SqlParameter("@MachineErrorHappenedOn", pvsMachineName));

                lcmdAddErrorMessage.ExecuteNonQuery();

                ConnErrors.Close();
            }
            catch // (Exception ex)
            {
                // Not much we can do so just leave it.....
            }
        }
    }
}
