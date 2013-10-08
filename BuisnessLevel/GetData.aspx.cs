using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Specialized;
using System.Globalization;
using System.Device.Location;
using BL.objects;

namespace BL
{
    public partial class GetData : System.Web.UI.Page
    {
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            HttpRequest rq = this.Request;

            NameValueCollection par = rq.QueryString;
            string ret = Processing(par);

            Response.Write(ret);
            Response.End();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }


        private string Processing(NameValueCollection par)
        {
            string ret = "alert('не найдено');";
            string[] allKeys = par.AllKeys;

            if (allKeys.Contains("la") && allKeys.Contains("lg"))
            {
                double la = Convert.ToSingle(par["la"], CultureInfo.InvariantCulture.NumberFormat);
                double lg = Convert.ToSingle(par["lg"], CultureInfo.InvariantCulture.NumberFormat);

                GeoCoordinate source = new GeoCoordinate(la, lg);

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

                schoolDistance = schoolDistance.OrderBy(x => x.Value.Item1).Take(1).ToDictionary(x => x.Key, x => x.Value);


                foreach (KeyValuePair<int, Tuple<double, Guid>> item in schoolDistance)
                {
                    SchoolData sd = Global.schoolDict[item.Value.Item2];

                    if (Global.ratingDict.ContainsKey(sd.school))
                    {
                        RatingData rd = Global.ratingDict[sd.school];
                        ret = string.Format("alert('школа - {0} \\r\\n расстояние - {1} \\r\\n общий рейтинг - {2} \\r\\n место по городу - {3} \\r\\n общий уровень - {4}');", 
                                            rd.school, 
                                            item.Value.Item1,
                                            rd.egCommonRating,
                                            rd.egCommomLevel,
                                            rd.egCommonState);
                    }
                    else
                    {
                        ret = string.Format("alert('школа - {0} \\r\\n расстояние - {1} \\r\\n в рейтинге отсутствует');", sd.school, item.Value.Item1);
                    }
                }

            }
            
            return ret;
        }
        


        public static bool IsPointInPolygon4(IList<PointF> polygon, PointF testPoint)
        {
            bool result = false;

            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }
    }
}