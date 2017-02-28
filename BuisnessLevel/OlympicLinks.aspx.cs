using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BL
{
    public partial class OlympicLinks : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            HttpRequest rq = this.Request;

            NameValueCollection par = rq.QueryString;

            HttpContext ctx = HttpContext.Current;


            string ret = Processing(par);

            Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.AddHeader("Access-Control-Allow-Methods", "*");
            Response.AddHeader("Access-Control-Allow-Headers", "*");

            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(ret);
            Response.End();
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string Processing(NameValueCollection par)
        {
            string ret = string.Empty;

            string[] allKeys = par.AllKeys;

            ret = Global.patternOlympicWidjetFarmes;
            
            return ret;
        }

    }
}