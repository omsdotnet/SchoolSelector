using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class GeoObject
    {
        public GeoObject()
        {          
            Point = new Point();
        }

        public void InitMetaDataProperty()
        {
            metaDataProperty = new MetaDataProperty();
        }

        public void InitBounded()
        {
            this.boundedBy = new BoundedBy();
        }

        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public MetaDataProperty metaDataProperty { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public string name { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public string description { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public BoundedBy boundedBy { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public Point Point { get; set; }
    }
}
