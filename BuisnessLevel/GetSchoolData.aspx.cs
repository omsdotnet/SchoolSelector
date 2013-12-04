using BL.objects;
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
    public partial class GetSchoolData : System.Web.UI.Page
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

            if (allKeys.Contains("num") &&
                allKeys.Contains("userid") &&
                allKeys.Contains("callback") &&
                allKeys.Contains("cssreg"))
            {
                ret = Global.widgetNotFoundData;
                string num = HttpUtility.UrlDecode(par["num"]).Trim();
                string cssreg = HttpUtility.UrlDecode(par["cssreg"]).Trim();

                int schoolNum;
                if (int.TryParse(num, out schoolNum))
                {
                    RatingData rd = null;
                    Global.ratingDict.TryGetValue(schoolNum, out rd);

                    //IEnumerable<SchoolData> schoolList = Global.schoolDict.Where(x => x.Value.school == schoolNum)
                    //                                                    .Select(x => x.Value);

                    List<SchoolData> schoolList = new List<SchoolData>();
                    foreach (var item in Global.schoolDict)
                    {
                        if (item.Value.school == schoolNum)
                        {
                            schoolList.Add(item.Value);
                        }
                    }



                    string wigetPattern = string.Copy(Global.patternWidjetSchoolDataBody);
                    GenerateWidgetData(ref wigetPattern, schoolNum, schoolList, rd);

                    if ("0" == cssreg)
                    {
                        ret = string.Empty;
                    }
                    else
                    {
                        ret = Global.patternWidjetSchoolDataStyle;
                    }

                    ret += wigetPattern;
                }

                ret = ret.Replace(Environment.NewLine, string.Empty);

                string callBackName = par["callback"];
                ret = string.Concat(callBackName, ".Callback('", ret, "');");
            }

            return ret;
        }

        private void GenerateWidgetData(ref string wigetPattern, int schoolNum, IEnumerable<SchoolData> schoolList, RatingData rd)
        {
            wigetPattern = wigetPattern.Replace("{school_num}", schoolNum.ToString());

            string addrList = string.Empty;
            foreach (SchoolData sd in schoolList)
            {
                addrList += string.Concat("ул. ", sd.street, " д.", sd.buildNum, "<br/>");
            }
            if (string.IsNullOrWhiteSpace(addrList))
            {
                addrList = "Нет данных";
            }
            wigetPattern = wigetPattern.Replace("{address}", addrList);


            if (null != rd)
            {
                wigetPattern = wigetPattern.Replace("{common_raiting}", rd.egCommonRating);
                wigetPattern = wigetPattern.Replace("{common_level}", rd.egCommomLevel);
                wigetPattern = wigetPattern.Replace("{common_raiting_level}", rd.egCommonState);

                string ratingStyle = string.Empty;
                switch (rd.egCommonState)
                {
                    case "Средний": { ratingStyle = "schoollist_item_data_middle_level"; break; }
                    case "Ниже среднего": { ratingStyle = "schoollist_item_data_low_middle_level"; break; }
                    case "Выше среднего": { ratingStyle = "schoollist_item_data_high_middle_level"; break; }
                    case "Самый высокий": { ratingStyle = "schoollist_item_data_high_level"; break; }
                    case "Самый низкий": { ratingStyle = "schoollist_item_data_low_level"; break; }
                }
                wigetPattern = wigetPattern.Replace("{common_raiting_level_style}", ratingStyle);

                Global.EnableBlock(ref wigetPattern, "RAITING DATA");
                Global.DisableBlock(ref wigetPattern, "NORAITING DATA");
            }
            else
            {
                Global.EnableBlock(ref wigetPattern, "NORAITING DATA");
                Global.DisableBlock(ref wigetPattern, "RAITING DATA");
            }




        }

    }
}