using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace BL
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Links.DataSource = GetAllPages();
            this.Links.DataBind();
        }

        public IList<string> GetAllPages()
        {
            int applicationFolderNumberOfCharacters = HttpRuntime.AppDomainAppPath.Length;
            List<string> pages = new List<string>();
            string[] files = Directory.GetFiles(HttpRuntime.AppDomainAppPath, "*.aspx", SearchOption.TopDirectoryOnly);

            List<string> ret = new List<string>();
            string thisPageFileName = Path.GetFileName(this.Context.Request.PhysicalPath).ToLower();
            string baseUrl = this.Context.Request.Url.Scheme + "://" + this.Context.Request.Url.Authority + this.Context.Request.ApplicationPath.TrimEnd('/') + "/";


            foreach (string f in files)
            {
                string fileName = Path.GetFileName(f);

                if (thisPageFileName != fileName.ToLower())
                {
                    ret.Add(baseUrl + fileName);
                }
            }

            ret.Sort();

            return ret;
        }

    }
}