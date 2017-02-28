using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplicationCatalog
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Links.DataSource = GetAllApplications();
            this.Links.DataBind();
        }

        public IList<string> GetAllApplications()
        {
            List<string> pages = new List<string>();
            string[] files = Directory.GetDirectories(HttpRuntime.AppDomainAppPath);

            List<string> ret = new List<string>();
            string baseUrl = this.Context.Request.Url.Scheme + "://" + this.Context.Request.Url.Authority + this.Context.Request.ApplicationPath.TrimEnd('/') + "/";


            foreach (string f in files)
            {
                if (PatchIsApplication(f))
                {
                    ret.Add(baseUrl + Path.GetFileName(f));
                }
            }

            ret.Sort();

            return ret;
        }

        private bool PatchIsApplication(string patchFolder)
        {
            bool ret = false;
            
            if (!ret) ret = File.Exists(patchFolder + "\\web.config");
            if (!ret) ret = File.Exists(patchFolder + "\\index.html");
            if (!ret) ret = File.Exists(patchFolder + "\\index.htm");

            return ret;
        }

    }
}