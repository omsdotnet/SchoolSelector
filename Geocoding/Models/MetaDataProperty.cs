using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class MetaDataProperty
    {
        public MetaDataProperty()
        {
        }

        public void InitMetaData()
        {
            GeocoderMetaData = new GeocoderMetaData();
        }

        public void InitResponseMetaData()
        {
            GeocoderResponseMetaData = new GeocoderResponseMetaData();
        }

        [XmlElement(Namespace = "http://maps.yandex.ru/geocoder/1.x")]
        public GeocoderResponseMetaData GeocoderResponseMetaData {get; set;}
        [XmlElement(Namespace = "http://maps.yandex.ru/geocoder/1.x")]
        public GeocoderMetaData GeocoderMetaData { get; set; }
    }
}
