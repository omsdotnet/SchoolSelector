using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class GeocoderResponseMetaData
    {
        public string request { get; set; }
        public int found { get; set; }
        public int results { get; set; }
    }
}
