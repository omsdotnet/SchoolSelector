using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class GeocoderMetaData
    {
        public GeocoderMetaData()
        {
            AddressDetails = new AddressDetails();
        }

        public string kind { get; set; }
        public string text { get; set; }
        public string precision {get; set;}

        [XmlElement(Namespace = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0")]
        public AddressDetails AddressDetails { get; set; }
    }
}
