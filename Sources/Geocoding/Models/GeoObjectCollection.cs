using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class GeoObjectCollection
    {
        public GeoObjectCollection()
        {
            featureMember = new List<FeatureMember>();
        }

        public void InitFeatureMembers()
        {
            featureMembers = new FeatureMembers();
        }

        public void InitMetaDataProperty()
        {
            metaDataProperty = new MetaDataProperty();
        }

        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public string name { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public MetaDataProperty metaDataProperty { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public FeatureMembers featureMembers { get; set; }
        [XmlElement(Namespace = "http://www.opengis.net/gml")]
        public List<FeatureMember> featureMember { get; set; }

    }
}
