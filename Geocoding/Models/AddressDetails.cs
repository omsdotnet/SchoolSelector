using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class AddressDetails
    {
        public AddressDetails()
        {
            Country = new Country();
        }

        public Country Country { get; set; }

    }
}
