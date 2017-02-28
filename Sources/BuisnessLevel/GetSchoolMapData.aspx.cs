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
    public partial class GetSchoolMapData : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            HttpRequest rq = this.Request;

            NameValueCollection par = rq.QueryString;

            HttpContext ctx = HttpContext.Current;


            string ret = Processing(par);

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

            string baseUrl = GetBaseUrl(this.Request);

            if (allKeys.Contains("userid") &&
                allKeys.Contains("callback") &&
                allKeys.Contains("cssreg"))
            {
                ret = Global.patternWidjetSchoolMap;
                string cssreg = HttpUtility.UrlDecode(par["cssreg"]).Trim();

                ret = ret.Replace("<%= baseUrl %>", baseUrl);
                ret = ret.Replace(Environment.NewLine, string.Empty);

                string callBackName = par["callback"];
                ret = string.Concat(callBackName, ".Callback('", ret, "');");
            }

            return ret;
        }


        private string GetBaseUrl(HttpRequest rq)
        {
            string ret = rq.Url.GetLeftPart(UriPartial.Authority) + rq.ApplicationPath;

            if (!ret.EndsWith("/"))
            {
                ret += "/";
            }

            return ret;
        }


    }
}