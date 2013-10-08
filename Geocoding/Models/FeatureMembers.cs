using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class FeatureMembers
    {
        public FeatureMembers()
        {
            GeoObject = new List<GeoObject>();
        }
        [XmlElement(Namespace = "http://maps.yandex.ru/ymaps/1.x")]
        public List<GeoObject> GeoObject { get; set; }
    }
}
