using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal
{
    public partial class TestingResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ActiveTopNav"] = "TestingResults";
            Session["ActiveLeftNav"] = "TestingResults";
            Classes.cLogin TestingResults = new Classes.cLogin();
            TestingResults.getTestingResults();
            lblTestingResults.Text = TestingResults.TestingResultsText;
        }
    }
}