using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class Country
    {
        public Country()
        {
            
        }

        public void InitAdministrativeArea()
        {
            this.AdministrativeArea = new AdministrativeArea();
        }

        public void InitLocality()
        {
            this.Locality = new Locality();
        }

        public string CountryNameCode { get; set; }
        public string CountryName { get; set; }
        public AdministrativeArea AdministrativeArea {get; set;}
        public Locality Locality { get; set; }
    }
}
