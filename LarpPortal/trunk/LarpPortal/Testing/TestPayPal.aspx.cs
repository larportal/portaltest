using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LarpPortal.Testing
{
    public partial class TestPayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblRegistrationFormStart.Text = "<form action=\"https://secure.paypal.com/cgi-bin/webscr\" target=\"_blank\" method=\"POST\"><input name" + 
            //    "=\"cmd\" value=\"_xclick\" type=\"hidden\"> <input name=\"business\" value=\"rciccolini@gmail.com\" type=\"hidden\"> <input name=\"return\" value=" +
            //    "=\"http://www.larp.com/madrigal/\" type=\"hidden\"> <input name=\"item_name\" value=\"Madrigal Adventure Weekend\" type=\"hidden\"> <input name" +
            //    "=\"item_number\" value=\"001\" type=\"hidden\"> <input name=\"amount\" value=\"80.00\" type=\"hidden\"> <input src" +
            //    "=\"https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif\" name=\"submit\" alt=\"Make payments with PayPal - it's fast, free and secure!\" type" +
            //    "=\"image\"></form>";


          //   <form action="/web/20151105031059/https://secure.paypal.com/cgi-bin/webscr" target="_blank" method="POST"><input name=
          //   "cmd" value="_xclick" type="hidden"> <input name="business" value="rciccolini@gmail.com" type="hidden"> <input name="return" value
          //       ="http://www.larp.com/madrigal/" type="hidden"> <input name="item_name" value="Madrigal Adventure Weekend" type="hidden"> <input name
          //           ="item_number" value="001" type="hidden"> <input name="amount" value="80.00" type="hidden"> 
          //  <input src="images/x-click-but02.gif" name="submit" alt="Make payments with PayPal - it's fast, free and secure!" height="31" type="image" width="62">
          //</form>
                
          //      "<input name="cmd" value="_xclick" type="hidden">
          //                                    <input name="business" value="rciccolini@gmail.com" type="hidden">
          //                                    <input name="item_name" value="EVENTNAME - Character: CHARACTERNAME or NPC/STAFF" type="hidden">
          //                                    <input name="item_number" value="001" type="hidden">
          //                                    <input name="currency_code" value="USD" type="hidden">
          //                                    <input name="amount" value="80.00" type="hidden">
          //                                    <input name="cn" value="Include character name and event date" type="hidden">
          //                                    <input name="no_note" value="0" type="hidden">
          //                                    <input name="custom" value="Zephyr - September 2016" type="hidden">
          //                                    <asp:ImageButton ID="btnRegistration" runat="server" ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" PostBackUrl="https://secure.paypal.com/cgi-bin/webscr" OnClick="btnRegistration_Click" />";





        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
        }

        protected void btnRegistration_Click(object sender, ImageClickEventArgs e)
        {
                   
        }

        protected void btnSaturdayBrunch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSaturdayDinner_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSundayBrunch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnAllMeals_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}