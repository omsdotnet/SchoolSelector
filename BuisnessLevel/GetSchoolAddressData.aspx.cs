using BL.objects;
using Geocoding;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BL
{
    public partial class GetSchoolAddressData : System.Web.UI.Page
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

            if (allKeys.Contains("addr") && 
                allKeys.Contains("userid") &&
                allKeys.Contains("callback") &&
                allKeys.Contains("cssreg") )
            {
                ret = Global.widgetNotFoundData;
                string addr = HttpUtility.UrlDecode(par["addr"]).Trim();
                string cssreg = HttpUtility.UrlDecode(par["cssreg"]).Trim();

                if (!string.IsNullOrWhiteSpace(addr)  && (1 < addr.Split(' ').Count()))
                {
                    GeoMatching gm = new GeoMatching();

                    if (!addr.StartsWith("омск", StringComparison.InvariantCultureIgnoreCase))
                    {
                        addr = addr.Insert(0, "Омск ");
                    }

                    Coordinates cc = gm.GetCoordinates(addr);

                    if (null != cc)
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";

                        GeoCoordinate source = new GeoCoordinate(Convert.ToDouble(cc.Lng, provider), Convert.ToDouble(cc.Lat, provider));

                        Dictionary<int, Tuple<double, Guid>> schoolDistance = new Dictionary<int, Tuple<double, Guid>>();
                        foreach (KeyValuePair<Guid, SchoolData> item in Global.schoolDict)
                        {
                            double dist = item.Value.pointOnMap.GetDistanceTo(source);

                            Tuple<double, Guid> dataExist;
                            if (schoolDistance.TryGetValue(item.Value.school, out dataExist))
                            {
                                if (dist < dataExist.Item1)
                                {
                                    schoolDistance[item.Value.school] = Tuple.Create<double, Guid>(dist, item.Key);
                                }
                            }
                            else
                            {
                                schoolDistance.Add(item.Value.school, Tuple.Create<double, Guid>(dist, item.Key));
                            }
                        }

                        schoolDistance = schoolDistance.OrderBy(x => x.Value.Item1).Take(5).ToDictionary(x => x.Key, x => x.Value);


                        string tableBody = string.Empty;

                        foreach (KeyValuePair<int, Tuple<double, Guid>> item in schoolDistance)
                        {
                            SchoolData sd = Global.schoolDict[item.Value.Item2];
                            RatingData rd = null;
                            Global.ratingDict.TryGetValue(item.Key, out rd);
                            GenerateWidgetRow(ref tableBody, Global.patternWidjetRow, sd, item.Value.Item1, rd);
                        }

                        if ("0" == cssreg)
                        {
                            ret = string.Empty;
                        }
                        else
                        {
                            ret = Global.patternWidjetStyle;
                        }

                        ret += Global.patternWidjetBody.Replace(Global.rowStart, string.Empty)
                                                       .Replace(Global.rowEnd, string.Empty)
                                                       .Replace(Global.patternWidjetRow, tableBody);
                    }
                }

                ret = ret.Replace(Environment.NewLine, string.Empty);

                string callBackName = par["callback"];
                ret = string.Concat(callBackName, ".Callback('", ret, "');");
            }

            return ret;
        }

        private void GenerateWidgetRow(ref string ret, string pattern, SchoolData sd, double? distance, RatingData rd)
        {
            string row = pattern;

            row = row.Replace("{school_num}", sd.school.ToString());

            string adr = string.Concat("ул. ", sd.street, " д.", sd.buildNum);
            row = row.Replace("{address}", adr);

            row = row.Replace("{distance}", distance.HasValue ? DistanceFormat.InKiloMeters(distance.Value) : string.Empty);

            if (null != rd)
            {
                row = row.Replace("{common_raiting}", rd.egCommonRating);
                row = row.Replace("{common_level}", rd.egCommomLevel);
                row = row.Replace("{common_raiting_level}", rd.egCommonState);

                string ratingStyle = string.Empty;
                switch (rd.egCommonState)
                {
                    case "Средний": { ratingStyle = "schoollist_item_data_middle_level"; break; }
                    case "Ниже среднего": { ratingStyle = "schoollist_item_data_low_middle_level"; break; }
                    case "Выше среднего": { ratingStyle = "schoollist_item_data_high_middle_level"; break; }
                    case "Самый высокий": { ratingStyle = "schoollist_item_data_high_level"; break; }
                    case "Самый низкий": { ratingStyle = "schoollist_item_data_low_level"; break; }
                }
                row = row.Replace("{common_raiting_level_style}", ratingStyle);

                Global.EnableBlock(ref row, "RAITING DATA");
                Global.DisableBlock(ref row, "NORAITING DATA");
            }
            else
            {
                Global.EnableBlock(ref row, "NORAITING DATA");
                Global.DisableBlock(ref row, "RAITING DATA");
            }

            ret = ret + row;
        }

    }
}