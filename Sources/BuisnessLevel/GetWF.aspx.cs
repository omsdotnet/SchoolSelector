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
    public partial class GetWF : System.Web.UI.Page
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

            int reg = 0;
            if (allKeys.Contains("cssreg"))
            {
                int.TryParse(par["cssreg"], out reg);
            }

            ret = Global.wfFacade.GetWeatherNow(reg);

            if (allKeys.Contains("userid") &&
                allKeys.Contains("callback"))
            {
                string callBackName = par["callback"];
                
                ret = ret.Replace("'", "\"");
                ret = ret.Replace(Environment.NewLine, string.Empty);

                ret = string.Concat(callBackName, ".Callback('", ret, "');");
            }

            return ret;
        }

    }
}