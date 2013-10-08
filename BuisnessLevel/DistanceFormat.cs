using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL
{
    public static class DistanceFormat
    {
        public static string InKiloMeters(double val)
        {
            string ret = string.Empty;

            int kilometers = (int)(val / 1000);
            int meters = (int)(val % 1000);

            if (kilometers > 0)
            {
                if (meters > 0)
                {
                    ret = string.Format("{0} км. {1} м.", kilometers, meters);
                }
                else
                {
                    ret = string.Format("{0} км.", kilometers);
                }
            }
            else
            {
                if (meters > 0)
                {
                    ret = string.Format("{0} м.", meters);
                }
            }

            return ret;
        }
    }
}