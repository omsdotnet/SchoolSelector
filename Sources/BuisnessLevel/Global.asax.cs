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
using WFProc;

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

        public static string widgetNotFoundData = string.Empty;
        public static string widgetNotFoundAddressStyle = string.Empty;
        public static string widgetNotFoundAddressBody = string.Empty;


        public static string patternWidjetSchoolData = string.Empty;
        public static string patternWidjetSchoolDataStyle = string.Empty;
        public static string patternWidjetSchoolDataBody = string.Empty;

        public static string patternWidjetSchoolMap = string.Empty;


        public static string patternOlympicWidjet = string.Empty;
        public static string patternOlympicWidjetStyle = string.Empty;
        public static string patternOlympicWidjetBody = string.Empty;

        public static string patternOlympicWidjetFarmes;


        public static string patternWFWidjet = string.Empty;
        public static string patternWFWidjetStyle = string.Empty;
        public static string patternWFWidjetBody = string.Empty;

        public static Facade wfFacade = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            // Не стартуем пока ПОЛНОСТЬЮ не подготовим все данные !!!
            
            // wfFacade = new Facade();

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
            patternWidjetStyle = getWidgetStyle(patternWidjetSchoolList);
            patternWidjetRow = getWidgetRow(patternWidjetSchoolList);
            patternWidjetBody = getWidgetBody(patternWidjetSchoolList);

            widgetNotFoundData = File.ReadAllText(Server.MapPath("~\\widjets\\NoFoundData.html"));
            widgetNotFoundAddressStyle = getWidgetStyle(widgetNotFoundData);
            widgetNotFoundAddressBody = getWidgetBody(widgetNotFoundData);

            patternWidjetSchoolData = File.ReadAllText(Server.MapPath("~\\widjets\\schooldata.html"));
            patternWidjetSchoolDataStyle = getWidgetStyle(patternWidjetSchoolData);
            patternWidjetSchoolDataBody = getWidgetBody(patternWidjetSchoolData);


            patternWidjetSchoolMap = File.ReadAllText(Server.MapPath("~\\widjets\\schoolmap.html"));


            patternOlympicWidjet = File.ReadAllText(Server.MapPath("~\\widjets\\olympic.html"));
            patternOlympicWidjetStyle = getWidgetStyle(patternOlympicWidjet);
            patternOlympicWidjetBody = getWidgetBody(patternOlympicWidjet);

            patternOlympicWidjetFarmes = File.ReadAllText(Server.MapPath("~\\widjets\\olympicFrames.html"));


            patternWFWidjet = File.ReadAllText(Server.MapPath("~\\widjets\\WF.html"));
            patternWFWidjetStyle = getWidgetStyle(patternWFWidjet);
            patternWFWidjetBody = getWidgetBody(patternWFWidjet);
        
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





        public static void DisableBlock(ref string ret, string nameBlock)
        {
            int pozStart = ret.IndexOf("<!-- " + nameBlock);
            int pozEnd = ret.IndexOf(nameBlock + " -->", pozStart);

            string blk = ret.Substring(pozStart, pozEnd + nameBlock.Length + " -->".Length - pozStart);
            ret = ret.Replace(blk, string.Empty);
        }

        public static void EnableBlock(ref string ret, string nameBlock)
        {
            int pozStart = ret.IndexOf("<!-- " + nameBlock);
            int pozEnd = ret.IndexOf(nameBlock + " -->", pozStart);

            string blk = ret.Substring(pozStart, pozEnd + nameBlock.Length + " -->".Length - pozStart);
            string oldBlk = blk;
            blk = blk.Replace("<!-- " + nameBlock, string.Empty);
            blk = blk.Replace(nameBlock + " -->", string.Empty);

            ret = ret.Replace(oldBlk, blk);
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