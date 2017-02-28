using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;

namespace BL.objects
{
    public class SchoolData
    {
        // from json
        public string street;
        public string buildNum;
        public double lattitude;
        public double longtitude;        
        public int school;

        // calculated fileld
        public GeoCoordinate pointOnMap;
    }
}