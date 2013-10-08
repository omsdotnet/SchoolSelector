using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class Locality
    {
        public Locality()
        {
            this.Thoroughfare = new Thoroughfare();
        }

        public string LocalityName { get; set; }

        public Thoroughfare Thoroughfare { get; set; }
    }
}
