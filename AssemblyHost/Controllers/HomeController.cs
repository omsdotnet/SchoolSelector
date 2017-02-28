using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssemblyHost.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var virtualPath = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)
                          + HttpRuntime.AppDomainAppVirtualPath;

            string curUrl = HttpContext.Request.Url.ToString().Replace(virtualPath, String.Empty);

            ViewBag.Message = String.Empty;

            IEnumerable<string> treeList = MvcApplication.asProc.GetListTree(curUrl);


            String pageUrl = HttpContext.Request.Url.ToString();
            if (!pageUrl.EndsWith("/"))
            {
                pageUrl += "/";
            }


            foreach (string item in treeList)
            {
                ViewBag.Message += "<li><a href='" + pageUrl + item + "'>" + item + "</a></li>";
            }

            


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
