using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class AdministrativeArea
    {
        public AdministrativeArea()
        {
            this.Locality = new Locality();
        }

        public string AdministrativeAreaName { get; set; }

        public Locality Locality { get; set; }
    }
}
