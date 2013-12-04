using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BL
{
    public partial class GetJSProxy : System.Web.UI.Page
    {
        public string baseUrl = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            baseUrl = GetBaseUrl(Request);

            Response.ContentType = "text/javascript";
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