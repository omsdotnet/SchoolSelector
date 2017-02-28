using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class FeatureMember
    {
        public FeatureMember()
        {
            GeoObject = new GeoObject();
        }

        [XmlElement(Namespace = "http://maps.yandex.ru/ymaps/1.x")]
        public GeoObject GeoObject { get; set; }
    }
}
