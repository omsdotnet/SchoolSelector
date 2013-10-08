using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Web.Script.Serialization;
using BL.objects;
using System.Device.Location;

namespace BL
{
    public class Global : System.Web.HttpApplication
    {
        public static Dictionary<int, RatingData> ratingDict = new Dictionary<int, RatingData>();
        public static Dictionary<Guid, SchoolData> schoolDict = new Dictionary<Guid, SchoolData>();
        
        public static string patternWidjetSchoolList = string.Empty;
        public static string patternWidjetStyle = string.Empty;
        public static string patternWidjetBody = string.Empty;
        public static string patternWidjetRow = string.Empty;

        public static string widgetNotFoundAddress = string.Empty;
        public static string widgetNotFoundAddressStyle = string.Empty;
        public static string widgetNotFoundAddressBody = string.Empty;

        protected void Application_Start(object sender, EventArgs e)
        {
            // Не стартуем пока ПОЛНОСТЬЮ не подготовим все данные !!!
            
            string line;
            JavaScriptSerializer js = new JavaScriptSerializer();
            
            StreamReader file = new StreamReader(Server.MapPath("~\\data\\SchoolData.txt"));
            using(file)
            {
                while ((line = file.ReadLine()) != null)
                {
                        SchoolData sc = js.Deserialize<SchoolData>(line);
                        sc.pointOnMap = new GeoCoordinate(sc.lattitude, sc.longtitude);
                        schoolDict.Add(Guid.NewGuid(), sc);
                }
            }


            
            file = new StreamReader(Server.MapPath("~\\data\\RatingData.txt"));
            using(file)
            {
                while ((line = file.ReadLine()) != null)
                {
                        RatingData rd = js.Deserialize<RatingData>(line);
                        
                        ratingDict.Add(rd.school, rd);
                }
            }

            
            patternWidjetSchoolList = File.ReadAllText(Server.MapPath("~\\widjets\\schoollistrow.html"));

            int rowPozStart = patternWidjetSchoolList.IndexOf(rowStart);
            int rowPozEnd = patternWidjetSchoolList.IndexOf(rowEnd, rowPozStart);

            patternWidjetStyle = getWidgetStyle(patternWidjetSchoolList);
            patternWidjetRow = getWidgetRow(patternWidjetSchoolList);
            patternWidjetBody = getWidgetBody(patternWidjetSchoolList);

            widgetNotFoundAddress = File.ReadAllText(Server.MapPath("~\\widjets\\NoFoundAddress.html"));
            widgetNotFoundAddressStyle = getWidgetStyle(widgetNotFoundAddress);
            widgetNotFoundAddressBody = getWidgetBody(widgetNotFoundAddress);
        }

        private string getWidgetStyle(string widgetPattern)
        {
            int stylePozStart = widgetPattern.IndexOf(styleStart);
            int stylePozEnd = widgetPattern.IndexOf(styleEnd, stylePozStart);

            string ret = widgetPattern.Substring(stylePozStart + styleStart.Length, stylePozEnd - (stylePozStart + styleStart.Length));

            return ret;
        }

        private string getWidgetRow(string widgetPattern)
        {
            int rowPozStart = patternWidjetSchoolList.IndexOf(rowStart);
            int rowPozEnd = patternWidjetSchoolList.IndexOf(rowEnd, rowPozStart);

            string ret = widgetPattern.Substring(rowPozStart + rowStart.Length, rowPozEnd - (rowPozStart + rowStart.Length));

            return ret;
        }

        private string getWidgetBody(string widgetPattern)
        {
            string widgetStyle = getWidgetStyle(widgetPattern);
            
            string ret = widgetPattern.Replace(styleStart + widgetStyle + styleEnd, string.Empty);
            return ret;
        }


        private string styleStart = "<!-- STYLES START -->";
        private string styleEnd = "<!-- STYLES END -->";
        public static string rowStart = "<!-- REPEATER START -->";
        public static string rowEnd = "<!-- REPEATER END -->";

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpRequest rq = this.Request;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}